using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CipherShield.Models;
using CipherShield.Helpers;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Text;
using System.Runtime.InteropServices;



namespace CipherShield
{
    public partial class MainForm : Form
    {
        private List<string> selectedFiles1 = new List<string>();
        private List<string> selectedFiles2 = new List<string>();
        private readonly byte[] backupKey = { 132, 42, 53, 84, 75, 96, 37, 28, 99, 10, 11, 12, 13, 14, 15, 16 };
        private DatabaseHelper db;
        private int counter = 0;
        string[] hints = { "It's always a great idea to backup your files to the cloud and to an external drive.", "Always backup your passwords to different safe places.", "The more backups you do, the easier to restore.", "Consider backup your important files by printing them.", "Daily system backup to an external drive is your best choice.", "A stitch in time saves nine." };

        public MainForm()
        {
            InitializeComponent();
            this.MakeDraggable(); // Make the form draggable
            this.menuBarLbl.MakeDraggable(this); // Make the form draggable
            timer1.Start(); // timer for the hintlabel in help tab

            // Initialize SQLitePCL to use SQLCipher
            SQLitePCL.Batteries_V2.Init();
            PasswordManagerDGV.SelectionChanged += DataGridView1_SelectionChanged;

            // Attach the Load event handler
            this.Load += new EventHandler(this.MainForm_Load);
        }

        // if no registered passwrd, show the register form, else show the login form
        private void MainForm_Load(object sender, EventArgs e)
        {
            SetControlsEnabled(false); // Method to disable controls
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cipher Shield");
            string filePath = Path.Combine(appDataPath, "Master-Password.dat");
            if (!File.Exists(filePath))
            {
                RegisterMasterPasswordForm registerMasterPasswordForm = new RegisterMasterPasswordForm();
                registerMasterPasswordForm.ShowDialog();
                if (registerMasterPasswordForm.DialogResult == DialogResult.OK)
                {
                    string password = registerMasterPasswordForm.RegisterMasterPwdTxtBox.Text; // Ensure you have a public Password property
                    SecureStorage.SavePassword(password);
                    db = new DatabaseHelper(password); // Pass the password to DatabaseHelper constructor
                    SetControlsEnabled(true);
                    LoadData();
                }
                else
                {
                    Close();
                }
            }
            else
            {
                LoginForm loginForm = new LoginForm();
                loginForm.ShowDialog();
                if (loginForm.DialogResult == DialogResult.OK)
                {
                    string password = loginForm.LoginMasterPwdTxtBox.Text;
                    db = new DatabaseHelper(password); // Pass the password to DatabaseHelper constructor
                    SetControlsEnabled(true);
                    LoadData();
                }
                else
                {
                    Close();
                }
            }
        }

        // Method to disable controls
        private void SetControlsEnabled(bool enabled)
        {
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = enabled;
            }
        }

        // timer for the hintlabel in help tab

        private void timer1_Tick(object sender, EventArgs e)
        {
            homeCarousselLbl.Text = hints[counter];
            counter = (counter + 1) % hints.Length;
        }

        // close the form
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        // minimize the form
        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            ActiveForm.WindowState = FormWindowState.Minimized;
        }

        // change master password
        private void SubmitNewPasswordBtn_Click(object sender, EventArgs e)
        {

            string currentPassword = SecureStorage.GetPassword();
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cipher Shield");
            Directory.CreateDirectory(appDataPath); // Ensure the directory exists
            string dbFilePath = Path.Combine(appDataPath, "credentials.db");

            if (oldPasswordTxtBox.Text != currentPassword)
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Current Password Is Incorrect")
                    .Show();
                return;
            }

            if (NewPasswordTxtBox.Text.Length == 0)
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)

                    .AddText("Please Enter a New Password")
                    .Show();
                return;
            }

            if (NewPasswordTxtBox.Text != RepeatNewPasswordTxtBox.Text)
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)

                    .AddText("New Passwords Do Not Match")
                    .Show();
                return;
            }

            if (NewPasswordTxtBox.Text.Length < 8)
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)

                    .AddText("New Password Must Be At Least 8 Characters Long")
                    .Show();
                return;
            }
            SecureStorage.ChangeDatabasePassword(dbFilePath, currentPassword, RepeatNewPasswordTxtBox.Text);
            SecureStorage.UpdatePassword(RepeatNewPasswordTxtBox.Text);
            string successIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "success.png");
            Uri successUri = new Uri($"file:///{successIcon}");
            new ToastContentBuilder()
                .AddAppLogoOverride(successUri, ToastGenericAppLogoCrop.Default)

                .AddText("The Master Password Has Been Successfully Changed" +
                Environment.NewLine + " Cipher Shield Will Be Restarted")

                .Show();
            Application.Restart();
        }

        // method to generate a password
        private string GeneratePassword(int length = 16)
        {
            const string uppercase = "NZAYBXCWDVEUFTGSHRIJPKOLM";
            const string lowercase = "mlokpjirhsgtfuevdwcxbyazn";
            const string numbers = "73281564";
            const string special = "]!@-)#}=/$%>|&*(+{<?[";

            // Ensure length is divisible by 4
            length = length - (length % 4);

            var chars = new char[length];
            var randomBytes = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            int quarterLength = length / 4;

            // Fill exactly 1/4 with uppercase
            for (int i = 0; i < quarterLength; i++)
            {
                chars[i] = uppercase[randomBytes[i] % uppercase.Length];
            }

            // Fill exactly 1/4 with numbers
            for (int i = 0; i < quarterLength; i++)
            {
                chars[2 * quarterLength + i] = numbers[randomBytes[2 * quarterLength + i] % numbers.Length];
            }

            // Fill exactly 1/4 with lowercase
            for (int i = 0; i < quarterLength; i++)
            {
                chars[quarterLength + i] = lowercase[randomBytes[quarterLength + i] % lowercase.Length];
            }

            // Fill exactly 1/4 with special characters
            for (int i = 0; i < quarterLength; i++)
            {
                chars[3 * quarterLength + i] = special[randomBytes[3 * quarterLength + i] % special.Length];
            }

            // Shuffle the entire password to mix the characters
            return new string(chars.OrderBy(x => randomBytes[new Random().Next(randomBytes.Length)]).ToArray());
        }

        #region Password Generator Tab

        // generate passwords
        private void GenerateButton_Click(object sender, EventArgs e)
        {
            PasswordGeneratorGeneratedPwdTextBox.Clear();
            int count = (int)PasswordGeneratorCountNumeric.Value;
            int Length = (int)PasswordGeneratorLengthNumeric.Value;

            for (int i = 0; i < count; i++)
            {
                string password = GeneratePassword(Length);
                PasswordGeneratorGeneratedPwdTextBox.AppendText($"Password {i + 1}: {password}\r\n");
            }

            // Enable buttons if passwords were generated
            bool hasPasswords = PasswordGeneratorGeneratedPwdTextBox.Text.Length > 0;
            PasswordGeneratorCopyPwdBtn.Enabled = hasPasswords;
            PasswordGeneratorExportPwdBtn.Enabled = hasPasswords;
            PasswordGeneratorClearPwdGenBtn.Enabled = hasPasswords;
        }

        // copy passwords - updated method
        private void copyPwdBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(PasswordGeneratorGeneratedPwdTextBox.Text))
            {
                try
                {
                    Clipboard.Clear(); // Clear the clipboard first
                    Clipboard.SetDataObject(PasswordGeneratorGeneratedPwdTextBox.Text, true); // Use the copy retry option

                    PasswordGeneratorCountNumeric.Value = PasswordGeneratorCountNumeric.Minimum;
                    PasswordGeneratorLengthNumeric.Value = PasswordGeneratorLengthNumeric.Minimum;

                    string successIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "info.png");
                    Uri successUri = new Uri($"file:///{successIcon}");
                    new ToastContentBuilder()
                        .AddAppLogoOverride(successUri, ToastGenericAppLogoCrop.Default)
                        .AddText("Passwords Copied")
                        .Show();
                }
                catch (ExternalException ex)
                {
                    // Handle clipboard exception
                    string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                    Uri errorUri = new Uri($"file:///{errorIcon}");
                    new ToastContentBuilder()
                        .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                        .AddText("Clipboard Operation Failed: " + ex.Message)
                        .Show();
                }
            }
            else
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("There Is No Passwords To Copy")
                    .Show();
            }
        }


        // export passwords
        private void exportPwdBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (!string.IsNullOrEmpty(PasswordGeneratorGeneratedPwdTextBox.Text) && saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                {
                    sw.Write(PasswordGeneratorGeneratedPwdTextBox.Text);
                    string successIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "success.png");
                    Uri successUri = new Uri($"file:///{successIcon}");
                    new ToastContentBuilder()
                        .AddAppLogoOverride(successUri, ToastGenericAppLogoCrop.Default)
                        .AddText("The Passwords have been successfully exported to TXT file.")
                        .Show();
                    PasswordGeneratorCountNumeric.Value = PasswordGeneratorCountNumeric.Minimum;
                    PasswordGeneratorLengthNumeric.Value = PasswordGeneratorLengthNumeric.Minimum;

                }
            }

        }

        // clear inputs
        private void clearPwdGenBtn_Click(object sender, EventArgs e)
        {
            PasswordGeneratorCountNumeric.Value = PasswordGeneratorCountNumeric.Minimum;
            PasswordGeneratorLengthNumeric.Value = PasswordGeneratorLengthNumeric.Minimum;
            PasswordGeneratorGeneratedPwdTextBox.Clear();
        }

        #endregion

        #region Password Manager Tab

        // load the data into datagridview
        private void LoadData()
        {
            var entries = db.GetAllEntries().ToList();
            PasswordManagerDGV.DataSource = entries;
            if (PasswordManagerDGV.Columns["Id"] != null)
            {
                PasswordManagerDGV.Columns["Id"].Visible = false;
            }

        }

        // method to select an entry and insert it into the textboxes
        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (PasswordManagerDGV.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = PasswordManagerDGV.SelectedRows[0];
                websiteTxtBox.Text = selectedRow.Cells["Website"].Value?.ToString();
                usernameTxtBox.Text = selectedRow.Cells["Username"].Value?.ToString();
                passwordTxtBox.Text = selectedRow.Cells["Password"].Value?.ToString();
            }
        }

        // add entry
        private void addButton_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(websiteTxtBox.Text) || string.IsNullOrEmpty(usernameTxtBox.Text) || string.IsNullOrEmpty(passwordTxtBox.Text))
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Please Fill In All Fields")
                    .Show();
                return;
            }
            else
            {
                PasswordEntry entry = new PasswordEntry
                {
                    Website = websiteTxtBox.Text,
                    Username = usernameTxtBox.Text,
                    Password = passwordTxtBox.Text
                };
                db.AddEntry(entry);
                LoadData();
                ClearInputFields();
                string successIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "success.png");
                Uri successUri = new Uri($"file:///{successIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(successUri, ToastGenericAppLogoCrop.Default)
                    .AddText("The Entry Has Been Successfully Added ")
                    .Show();
            }

        }

        // select entry and edit/update it
        private void editButton_Click_1(object sender, EventArgs e)
        {
            if (PasswordManagerDGV.CurrentRow != null)
            {

                int id = (int)PasswordManagerDGV.CurrentRow.Cells["Id"].Value;
                var entry = db.GetAllEntries().FirstOrDefault(e => e.Id == id);
                if (entry != null)
                {
                    entry.Website = websiteTxtBox.Text;
                    entry.Username = usernameTxtBox.Text;
                    entry.Password = passwordTxtBox.Text;
                    db.UpdateEntry(entry);
                    string successIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "success.png");
                    Uri successUri = new Uri($"file:///{successIcon}");
                    new ToastContentBuilder()
                        .AddAppLogoOverride(successUri, ToastGenericAppLogoCrop.Default)
                        .AddText("Successfully Updated:" + Environment.NewLine + $"Website: {entry.Website}" + Environment.NewLine + $"Username: {entry.Username}" + Environment.NewLine + $"Password: {entry.Password}")
                        .Show();
                    LoadData();
                    ClearInputFields();
                }
            }
            else
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Please Select An Entry To Edit")
                    .Show();
            }
        }

        // delete entry
        private void deleteButton_Click_1(object sender, EventArgs e)
        {
            if (PasswordManagerDGV.CurrentRow != null)
            {
                int id = (int)PasswordManagerDGV.CurrentRow.Cells[0].Value;
                db.DeleteEntry(id);
                LoadData();
                ClearInputFields();
                string infoIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "info.png");
                Uri infoUri = new Uri($"file:///{infoIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(infoUri, ToastGenericAppLogoCrop.Default)
                    .AddText("The Entry Has Been Deleted")
                    .Show();
            }
            else
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Please Select An Entry To Delete!")
                    .Show();
            }
        }

        // clear inputs
        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearInputFields();
        }

        // clear inputs
        private void ClearInputFields()
        {
            websiteTxtBox.Clear();
            usernameTxtBox.Clear();
            passwordTxtBox.Clear();
            LoadData();
        }

        // visualize an entry
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            PasswordManagerDGV.SelectionChanged += DataGridView1_SelectionChanged;
        }

        private void PasswordManagerPrintBtn_Click(object sender, EventArgs e)
        {
            if (PasswordManagerDGV.Rows.Count == 0)
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("There Is No Data To Print!")
                    .Show();
                return;
            }

            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;

            PrintDialog printDialog = new PrintDialog { Document = printDocument };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog { Document = printDocument };
                printPreviewDialog.ShowDialog();
                
            }
        }

        // print all credentials
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs ev)
        {
            int y = ev.MarginBounds.Top;
            int x = ev.MarginBounds.Left;
            int columnWidth = 200;
            int rowHeight = 30;

            // Set up colors
            Brush headerBrush = new SolidBrush(SystemColors.Highlight);
            Pen linePen = new Pen(Color.MidnightBlue);
            Brush textBrush = Brushes.Black;
            Brush headerFontBrush = Brushes.White;

            // Set fonts
            Font printFont = new Font("Verdana", 10);
            Font headerFont = new Font("Verdana", 10, FontStyle.Bold);

            // Print headers
            foreach (DataGridViewColumn column in PasswordManagerDGV.Columns)
            {
                if (column.Visible)
                {
                    // Draw header background
                    ev.Graphics.FillRectangle(headerBrush, x, y, columnWidth, rowHeight);
                    // Measure the width of the header text
                    SizeF headerTextSize = ev.Graphics.MeasureString(column.HeaderText, headerFont);
                    // Calculate the X position to center the text
                    float textX = x + (columnWidth - headerTextSize.Width) / 2;
                    // Draw header text
                    ev.Graphics.DrawString(column.HeaderText, headerFont, headerFontBrush, textX, y + 5);
                    // Draw header border
                    ev.Graphics.DrawRectangle(linePen, x, y, columnWidth, rowHeight);
                    x += columnWidth;
                }
            }

            y += rowHeight;
            x = ev.MarginBounds.Left;

            // Print rows
            foreach (DataGridViewRow row in PasswordManagerDGV.Rows)
            {
                x = ev.MarginBounds.Left;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (PasswordManagerDGV.Columns[cell.ColumnIndex].Visible)
                    {
                        // Draw cell border
                        ev.Graphics.DrawRectangle(linePen, x, y, columnWidth, rowHeight);
                        // Draw cell text
                        ev.Graphics.DrawString(cell.Value?.ToString(), printFont, textBrush, x + 5, y + 5);
                        x += columnWidth;
                    }
                }
                y += rowHeight;
            }
        }


        // export all credentials to csv
        private void PasswordManagerExportBtn_Click(object sender, EventArgs e)
        {
            if (PasswordManagerDGV.Rows.Count == 0)
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("There Is No Data To Export")
                    .Show();
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv";
                sfd.Title = "Save an Export File";
                sfd.ShowDialog();

                if (!string.IsNullOrEmpty(sfd.FileName))
                {
                    ExportToCsv(sfd.FileName);
                    string successIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "success.png");
                    Uri successUri = new Uri($"file:///{successIcon}");
                    new ToastContentBuilder()
                        .AddAppLogoOverride(successUri, ToastGenericAppLogoCrop.Default)
                        .AddText("The Data Has Been Successfully Exported")
                        .Show();
                }
            }
        }

        // method to export as csv
        private void ExportToCsv(string filePath)
        {
            var sb = new StringBuilder();

            var headers = PasswordManagerDGV.Columns.Cast<DataGridViewColumn>()
                            .Where(c => c.Visible)
                            .Select(column => $"\"{column.HeaderText}\"");
            sb.AppendLine(string.Join(",", headers));

            foreach (DataGridViewRow row in PasswordManagerDGV.Rows)
            {
                var cells = row.Cells.Cast<DataGridViewCell>()
                            .Where(c => PasswordManagerDGV.Columns[c.ColumnIndex].Visible)
                            .Select(cell => $"\"{cell.Value?.ToString()}\"");
                sb.AppendLine(string.Join(",", cells));
            }

            File.WriteAllText(filePath, sb.ToString());
        }

        #endregion

        #region Files Encryptor Tab

        // backup the files encryption password to a file then encrypt it
        private void BackupPasswordButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FilesEncryptionEnterPwdTxtBox.Text))
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Please Enter a Password To Backup")
                    .Show();
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Password files (*.pwd)|*.pwd|All files (*.*)|*.*";
            sfd.FilterIndex = 1;
            sfd.DefaultExt = "pwd";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    byte[] encryptedPassword = EncryptPassword(FilesEncryptionEnterPwdTxtBox.Text);
                    File.WriteAllBytes(sfd.FileName, encryptedPassword);
                    string successIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "success.png");
                    Uri successUri = new Uri($"file:///{successIcon}");
                    new ToastContentBuilder()
                        .AddAppLogoOverride(successUri, ToastGenericAppLogoCrop.Default)
                        .AddText("The Password Backup File Has Been Saved" + Environment.NewLine + "Keep This File Safe")
                        .Show();
                }
                catch (Exception ex)
                {
                    string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                    Uri errorUri = new Uri($"file:///{errorIcon}");
                    new ToastContentBuilder()
                        .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                        .AddText($"Error Saving Password Backup File: {ex.Message}")
                        .Show();
                }
            }

        }

        // browse files to encrypt
        private void BrowseFiles_Click(object sender, EventArgs e)
        {
            FileEncryptionFilesNumberTxtBox.Clear();
            FilesEncryptionFilesListBox.Items.Clear();
            selectedFiles1.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (FilesEncryptionFilesListBox != null)
                {
                    foreach (string file in ofd.FileNames)
                    {
                        if (!selectedFiles1.Contains(file))
                        {
                            selectedFiles1.Add(file);
                            FilesEncryptionFilesListBox.Items.Add(Path.GetFileName(file));
                        }
                    }
                }
                if (FileEncryptionFilesNumberTxtBox != null)
                    FileEncryptionFilesNumberTxtBox.Text = $"{selectedFiles1.Count} files selected";
            }
        }

        // load encryption password from backup file
        private void LoadBackupButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Password files (*.pwd)|*.pwd|All files (*.*)|*.*";
            ofd.FilterIndex = 1;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    byte[] encryptedPassword = File.ReadAllBytes(ofd.FileName);
                    string decryptedPassword = DecryptPassword(encryptedPassword);
                    if (FilesEncryptionEnterPwdTxtBox != null)
                    {
                        FilesEncryptionEnterPwdTxtBox.Text = decryptedPassword;
                    }
                    string successIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "success.png");
                    Uri successUri = new Uri($"file:///{successIcon}");
                    new ToastContentBuilder()
                        .AddAppLogoOverride(successUri, ToastGenericAppLogoCrop.Default)
                        .AddText("The Password Has Been Successfully Loaded")
                        .Show();
                }
                catch (Exception ex)
                {
                    string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                    Uri errorUri = new Uri($"file:///{errorIcon}");
                    new ToastContentBuilder()
                        .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                        .AddText($"Error Loading The Password Backup: {ex.Message}")
                        .Show();
                }
            }
            else
            {
                string infoIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "info.png");
                Uri infoUri = new Uri($"file:///{infoIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(infoUri, ToastGenericAppLogoCrop.Default)
                    .AddText("No File Selected!")
                    .Show();
            }

        }

        // method to encrypt the file encryption password
        private byte[] EncryptPassword(string password)
        {
            Aes aes = Aes.Create();
            aes.Key = backupKey;
            aes.GenerateIV(); // Generate a new IV for each encryption
            byte[] iv = aes.IV;

            MemoryStream msEncrypt = new MemoryStream();
            // Write the IV to the beginning of the file
            msEncrypt.Write(iv, 0, iv.Length);

            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, aes.CreateEncryptor(), CryptoStreamMode.Write))
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(password);
            }

            return msEncrypt.ToArray();
        }

        // method to decrypt the the file encryption password
        private string DecryptPassword(byte[] encryptedData)
        {
            Aes aes = Aes.Create();
            aes.Key = backupKey;

            // Get the IV from the beginning of the encrypted data
            byte[] iv = new byte[16];
            Array.Copy(encryptedData, 0, iv, 0, iv.Length);
            aes.IV = iv;

            // Get the encrypted password (skip the IV)
            MemoryStream msDecrypt = new MemoryStream(encryptedData, iv.Length, encryptedData.Length - iv.Length);
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, aes.CreateDecryptor(), CryptoStreamMode.Read);
            StreamReader srDecrypt = new StreamReader(csDecrypt);

            return srDecrypt.ReadToEnd();
        }

        // method to encrypt the selected files
        private async void EncryptButton_Click_1(object sender, EventArgs e)
        {

            if (selectedFiles1.Count == 0)
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Please Select The Files First")
                    .Show();
                return;
            }

            if (string.IsNullOrEmpty(FilesEncryptionEnterPwdTxtBox.Text))
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Please Enter a Password")
                    .Show();
                return;
            }

            DisableControls();
            UpdateProgress(0, selectedFiles1.Count);

            try
            {
                progressBar.Visible = true;
                await Task.Run(async () =>
                {
                    await Parallel.ForEachAsync(selectedFiles1, async (file, cancellationToken) =>
                    {
                        try
                        {
                            await ProcessFileInPlace(file, FilesEncryptionEnterPwdTxtBox.Text, true);

                            progressBar.Invoke(new Action(() => UpdateProgress(++progressBar.Value, selectedFiles1.Count)));
                        }
                        catch (Exception ex)
                        {
                            string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                            Uri errorUri = new Uri($"file:///{errorIcon}");
                            new ToastContentBuilder()
                                .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                                .AddText($"Error encrypting {Path.GetFileName(file)}: {ex.Message}")
                                .Show();
                        }
                    });
                });
                string successIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "success.png");
                Uri successUri = new Uri($"file:///{successIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(successUri, ToastGenericAppLogoCrop.Default)
                    .AddText("All Files Have Been Successfully Encrypted")
                    .Show();
            }
            catch (Exception ex)
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText($"Error during encryption: {ex.Message}")
                    .Show();
            }
            finally
            {
                EnableControls();
            }
        }

        // method to decrypt the selected files
        private async void DecryptButton_Click_1(object sender, EventArgs e)
        {
            if (selectedFiles1.Count == 0)
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Please Select The Files First")
                    .Show();
                return;
            }

            if (string.IsNullOrEmpty(FilesEncryptionEnterPwdTxtBox.Text))
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Please Enter a Password")
                    .Show();
                return;
            }

            DisableControls();
            UpdateProgress(0, selectedFiles1.Count);

            try
            {
                progressBar.Visible = true;
                await Task.Run(async () =>
                {
                    await Parallel.ForEachAsync(selectedFiles1, async (file, cancellationToken) =>
                    {
                        try
                        {
                            await ProcessFileInPlace(file, FilesEncryptionEnterPwdTxtBox.Text, false);

                            progressBar.Invoke(new Action(() => UpdateProgress(++progressBar.Value, selectedFiles1.Count)));
                        }
                        catch (Exception ex)
                        {
                            string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                            Uri errorUri = new Uri($"file:///{errorIcon}");
                            new ToastContentBuilder()
                                .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                                .AddText($"Error decrypting {Path.GetFileName(file)}: {ex.Message}")
                                .Show();
                        }
                    });
                });

                string successIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "success.png");
                Uri successUri = new Uri($"file:///{successIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(successUri, ToastGenericAppLogoCrop.Default)
                    .AddText("All Files Have Been Successfully Decrypted")
                    .Show();
            }
            catch (Exception ex)
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText($"Error during decryption: {ex.Message}")
                    .Show();
            }
            finally
            {
                EnableControls();
            }
        }

        private void DisableControls()
        {
            foreach (var button in Controls.OfType<Button>())
            {
                button.Enabled = false;
            }
            if (FilesEncryptionEnterPwdTxtBox.Text != null)
                FilesEncryptionEnterPwdTxtBox.Enabled = false;
        }

        private void EnableControls()
        {
            foreach (var button in Controls.OfType<Button>())
            {
                button.Enabled = true;
            }
            if (FilesEncryptionEnterPwdTxtBox.Text != null)
                FilesEncryptionEnterPwdTxtBox.Enabled = true;
        }

        // asyncronous method to encrypt/decrypt multiple files in parallel
        private async Task ProcessFileInPlace(string filePath, string password, bool encrypt)
        {
            string tempFile = Path.GetTempFileName();
            const int bufferSize = 81920; // 80 KB buffer size

            try
            {
                byte[] salt = new byte[32];
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                if (!encrypt)
                {
                    using (FileStream fsInput = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, true))
                    {
                        await fsInput.ReadAsync(salt, 0, salt.Length);
                    }
                }
                else
                {
                    salt = GenerateRandomSalt();
                }

                using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(passwordBytes, salt, 100000, HashAlgorithmName.SHA256))
                {
                    byte[] key = pbkdf2.GetBytes(32);
                    byte[] iv = pbkdf2.GetBytes(16);

                    using (Aes aes = Aes.Create())
                    {
                        aes.Key = key;
                        aes.IV = iv;
                        aes.Padding = PaddingMode.PKCS7;

                        if (encrypt)
                        {
                            using (FileStream fsInput = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, true))
                            using (FileStream fsTemp = new FileStream(tempFile, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize, true))
                            {
                                await fsTemp.WriteAsync(salt, 0, salt.Length);

                                using (CryptoStream cs = new CryptoStream(fsTemp, aes.CreateEncryptor(), CryptoStreamMode.Write))
                                {
                                    byte[] buffer = new byte[bufferSize];
                                    int bytesRead;
                                    while ((bytesRead = await fsInput.ReadAsync(buffer, 0, buffer.Length)) > 0)
                                    {
                                        await cs.WriteAsync(buffer, 0, bytesRead);
                                    }
                                    await cs.FlushFinalBlockAsync();
                                }
                            }
                        }
                        else
                        {
                            using (FileStream fsInput = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, true))
                            using (FileStream fsTemp = new FileStream(tempFile, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize, true))
                            {
                                fsInput.Seek(salt.Length, SeekOrigin.Begin);

                                using (CryptoStream cs = new CryptoStream(fsInput, aes.CreateDecryptor(), CryptoStreamMode.Read))
                                {
                                    byte[] buffer = new byte[bufferSize];
                                    int bytesRead;
                                    while ((bytesRead = await cs.ReadAsync(buffer, 0, buffer.Length)) > 0)
                                    {
                                        await fsTemp.WriteAsync(buffer, 0, bytesRead);
                                    }
                                    await fsTemp.FlushAsync();
                                }
                            }
                        }
                    }
                }

                File.Delete(filePath);
                File.Move(tempFile, filePath);
            }
            finally
            {
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
        }

        // clear inputs
        private void clear_Click(object sender, EventArgs e)
        {
            FileEncryptionFilesNumberTxtBox.Clear();
            FilesEncryptionFilesListBox.Items.Clear();
            FilesEncryptionEnterPwdTxtBox.Clear();
            selectedFiles1.Clear();
            progressBar.Visible = false;
        }

        // generate random salt for encryption key
        private byte[] GenerateRandomSalt()
        {
            byte[] salt = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        // method to update progress bar 
        private void UpdateProgress(int value, int maximum)
        {
            if (progressBar.InvokeRequired)
            {

                progressBar.Invoke(new Action<int, int>(UpdateProgress), value, maximum);
            }
            else
            {
                if (progressBar != null)
                {
                    progressBar.Maximum = maximum;
                    progressBar.Value = Math.Min(value, maximum);
                }
            }
        }

        // generate random password
        private void GeneratePasswordButton_Click(object sender, EventArgs e)
        {
            FilesEncryptionEnterPwdTxtBox.Text = GeneratePassword();
        }

        #endregion

        #region Files Renamer Tab

        // browse files to rename
        private void BrowseFilesBtnToRename_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                selectedFiles2.Clear();
                RegexFilesListView.Items.Clear();

                try
                {
                    foreach (string filePath in ofd.FileNames)
                    {
                        var fileInfo = new FileInfo(filePath);
                        selectedFiles2.Add(fileInfo.FullName);
                        var item = new ListViewItem(new[] { fileInfo.Name, fileInfo.Name });
                        RegexFilesListView.Items.Add(item);
                    }

                    RegexPreviewChangesBtn.Enabled = selectedFiles2.Count > 0;
                }
                catch (Exception ex)
                {
                    string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                    Uri errorUri = new Uri($"file:///{errorIcon}");
                    new ToastContentBuilder()
                        .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                        .AddText($"Error loading the files: {ex.Message}")
                        .Show();
                }
            }
            else
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("No Files Selected")
                    .Show();

            }
        }

        // regex method
        private string GetNewFileName(FileInfo file, string pattern, string replacement, ref int counter)
        {
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(file.Name);
            string extension = file.Extension;
            string newName;

            if (pattern == "^")
            {
                // Add prefix
                newName = $"{replacement.Replace("{n}", counter.ToString())}{nameWithoutExtension}{extension}";
            }
            else if (pattern == "$")
            {
                // For suffix, we don't use regex replacement at all
                string suffix = replacement.Replace("{n}", counter.ToString());
                if (suffix.StartsWith("_"))
                {
                    newName = $"{nameWithoutExtension}{suffix}{extension}";
                }
                else
                {
                    newName = $"{nameWithoutExtension}_{suffix}{extension}";
                }
            }
            else if (pattern == @"^.*(\..+)$")
            {
                // Replace entire name but keep extension
                newName = $"{replacement.Replace("{n}", counter.ToString())}{extension}";
            }
            else if (pattern == ".+")
            {
                // Replace entire name including extension
                newName = replacement.Replace("{n}", counter.ToString());
            }
            else
            {
                // Custom pattern - apply regex only once
                var regex = new Regex(pattern);
                string replacementWithCounter = replacement.Replace("{n}", counter.ToString());
                newName = regex.Replace(file.Name, replacementWithCounter, 1);
            }

            // Ensure the new name is valid
            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                newName = newName.Replace(invalidChar, '_');
            }

            counter += (int)RegexIncrementNumeric.Value;
            return newName;
        }

        // method to preview the renaming
        private void previewBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RegexPatternTxtBox.Text))
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Please Enter a Regex Pattern")
                    .Show();
                return;
            }
            
            if (RegexFilesListView.Items.Count == 0)
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Please Select The Files To Rename")
                    .Show();
                return;
            }

            try
            {
                RegexFilesListView.Items.Clear();
                bool hasChanges = false;

                // Initialize counter
                int counter = 1;
                if (RegexUseIncrementCheckBox.Checked && int.TryParse(RegexStartFromNumeric.Text, out int startNumber))
                {
                    counter = startNumber;
                }

                foreach (var file in selectedFiles2)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    string newName = RegexUseIncrementCheckBox.Checked
                        ? GetNewFileName(fileInfo, RegexPatternTxtBox.Text, RegexReplacementTxtBox.Text, ref counter)
                        : new Regex(RegexPatternTxtBox.Text).Replace(fileInfo.Name, RegexReplacementTxtBox.Text);

                    var item = new ListViewItem(new[] { fileInfo.Name, newName });
                    RegexFilesListView.Items.Add(item);

                    if (fileInfo.Name != newName)
                        hasChanges = true;                   
                }

                string successIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "success.png");
                Uri successUri = new Uri($"file:///{successIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(successUri, ToastGenericAppLogoCrop.Default)
                    .AddText("New Files Names Have Been Previewed")
                    .Show();

                RegexRenameFilesBtn.Enabled = hasChanges;

            }
            catch (ArgumentException ex)
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText($"Invalid regex pattern: {ex.Message}")
                    .Show();
            }
        }

        // method to rename the files
        private void renameButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RegexPatternTxtBox.Text))
            {
                string ErrorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri ErrorUri = new Uri($"file:///{ErrorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(ErrorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Please Enter a Regex Pattern!")
                    .Show();
                return;
            }

            if (RegexFilesListView.Items.Count == 0)
            {
                string ErrorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri ErrorUri = new Uri($"file:///{ErrorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(ErrorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Please Select The Files To Rename!")
                    .Show();
                return;
            }
            int successCount = 0;
            List<string> errors = new List<string>();

            // Initialize counter
            int counter = 1;
            if (RegexUseIncrementCheckBox.Checked && int.TryParse(RegexStartFromNumeric.Text, out int startNumber))
            {
                counter = startNumber;
            }

            foreach (var file in selectedFiles2.ToList())
            {
                FileInfo fileInfo = new FileInfo(file);
                try
                {

                    string newName = RegexUseIncrementCheckBox.Checked
                        ? GetNewFileName(fileInfo, RegexPatternTxtBox.Text, RegexReplacementTxtBox.Text, ref counter)
                        : new Regex(RegexPatternTxtBox.Text).Replace(fileInfo.Name, RegexReplacementTxtBox.Text);

                    if (fileInfo.Name != newName)
                    {
                        string newPath = Path.Combine(fileInfo.DirectoryName, newName);

                        // Check if target file already exists
                        if (File.Exists(newPath))
                        {
                            errors.Add($"{fileInfo.Name}: Cannot rename - target file already exists: {newName}");
                            continue;
                        }

                        fileInfo.MoveTo(newPath);
                        successCount++;
                    }
                }
                catch (Exception ex)
                {
                    errors.Add($"{fileInfo.Name}: {ex.Message}");
                }
            }

            // Refresh the file list
            if (successCount > 0)
            {
                selectedFiles2.Clear();
                RegexFilesListView.Items.Clear();
                OpenFileDialog ofd = new OpenFileDialog();
                foreach (string filePath in ofd.FileNames)
                {
                    var fileInfo = new FileInfo(filePath);
                    string fileName = fileInfo.Name;
                    selectedFiles2.Add(fileName);
                    var item = new ListViewItem(new[] { fileInfo.Name, fileInfo.Name });
                    RegexFilesListView.Items.Add(item);
                }
            }

            // Show results
            string message = $"Successfully renamed {successCount} files.";
            if (errors.Any())
            {
                message += $"\n\nErrors occurred with {errors.Count} files:\n" + string.Join("\n", errors);
            }
            string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
            Uri errorUri = new Uri($"file:///{errorIcon}");
            new ToastContentBuilder()
                .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                .AddText($"Renaming results: {errors.Any()}")
                .Show();

            RegexRenameFilesBtn.Enabled = false;

        }

        // clear inputs
        private void RegexClearBtn_Click(object sender, EventArgs e)
        {
            RegexPatternTxtBox.Clear();
            RegexReplacementTxtBox.Clear();
            RegexFilesListView.Items.Clear();
            RegexUseIncrementCheckBox.Checked = false;
            RegexStartFromNumeric.Value = 1;
            RegexIncrementNumeric.Value = 1;
        }

        private void RegexHelpBtn_Click(object sender, EventArgs e)
        {
            string url = "https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference";
            Process.Start(new ProcessStartInfo(url)
            {
                UseShellExecute = true
            }
            );
        }

        #endregion

    }
}


