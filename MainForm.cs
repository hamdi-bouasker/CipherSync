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
            timer1.Start();
            // Initialize SQLitePCL to use SQLCipher
            SQLitePCL.Batteries_V2.Init();
            PasswordManagerDGV.SelectionChanged += DataGridView1_SelectionChanged;

            // Attach the Load event handler
            this.Load += new EventHandler(this.MainForm_Load);
        }



        private void MainForm_Load(object sender, EventArgs e)
        {
            SetControlsEnabled(false); // Method to disable controls

            if (!File.Exists("encrypted_pwd_database.db"))
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
                    string password = loginForm.LoginMasterPwdTxtBox.Text; // Ensure you have a public Password property
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

        private void SetControlsEnabled(bool enabled)
        {
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = enabled;
            }
        }

        private void LoadData()
        {
            var entries = db.GetAllEntries().ToList();
            PasswordManagerDGV.DataSource = entries;
            if (PasswordManagerDGV.Columns["Id"] != null)
            {
                PasswordManagerDGV.Columns["Id"].Visible = false;
            }

        }
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

        #region Password Generator Tab


        private void GenerateButton_Click(object sender, EventArgs e)
        {
            PasswordGeneratorGeneratedPwdTextBox.Clear();
            int count = (int)PasswordGeneratorCountNumeric.Value;
            int length = (int)PasswordGeneratorLengthNumeric.Value;

            for (int i = 0; i < count; i++)
            {
                string password = GeneratePassword(length);
                PasswordGeneratorGeneratedPwdTextBox.AppendText($"Password {i + 1}: {password}\r\n");
            }

            // Enable buttons if passwords were generated
            bool hasPasswords = PasswordGeneratorGeneratedPwdTextBox.Text.Length > 0;
            PasswordGeneratorCopyPwdBtn.Enabled = hasPasswords;
            PasswordGeneratorExportPwdBtn.Enabled = hasPasswords;
            PasswordGeneratorClearPwdGenBtn.Enabled = hasPasswords;
        }


        private void copyPwdBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(PasswordGeneratorGeneratedPwdTextBox.Text))
            {
                Clipboard.SetText(PasswordGeneratorGeneratedPwdTextBox.Text);
                PasswordGeneratorCountNumeric.Value = PasswordGeneratorCountNumeric.Minimum;
                PasswordGeneratorLengthNumeric.Value = PasswordGeneratorLengthNumeric.Minimum;

                MessageBox.Show("Passwords Copied!", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("There is no passwords to copy!", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportPwdBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (!string.IsNullOrEmpty(PasswordGeneratorGeneratedPwdTextBox.Text) && saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                {
                    sw.Write(PasswordGeneratorGeneratedPwdTextBox.Text);
                    MessageBox.Show("Passwords successfully exported to TXT file!", "CypherSync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PasswordGeneratorCountNumeric.Value = PasswordGeneratorCountNumeric.Minimum;
                    PasswordGeneratorLengthNumeric.Value = PasswordGeneratorLengthNumeric.Minimum;

                }
            }
            else
            {
                MessageBox.Show("There is no passwords to export!", "CypherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void clearPwdGenBtn_Click(object sender, EventArgs e)
        {
            PasswordGeneratorCountNumeric.Value = PasswordGeneratorCountNumeric.Minimum;
            PasswordGeneratorLengthNumeric.Value = PasswordGeneratorLengthNumeric.Minimum;
            PasswordGeneratorGeneratedPwdTextBox.Clear();
        }

        #endregion

        #region Password Manager Tab

        private void addButton_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(websiteTxtBox.Text) || string.IsNullOrEmpty(usernameTxtBox.Text) || string.IsNullOrEmpty(passwordTxtBox.Text))
            {
                MessageBox.Show("Please fill in all fields.", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Entry added successfully!", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

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
                    MessageBox.Show("Successfully updated:" + Environment.NewLine + $"Website: {entry.Website}" + Environment.NewLine + $"Username: {entry.Username}" + Environment.NewLine + $"Password: {entry.Password}");

                    LoadData();
                    ClearInputFields();
                }
            }
            else MessageBox.Show("Please select an entry to edit.", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void deleteButton_Click_1(object sender, EventArgs e)
        {
            if (PasswordManagerDGV.CurrentRow != null)
            {
                int id = (int)PasswordManagerDGV.CurrentRow.Cells[0].Value;
                db.DeleteEntry(id);
                LoadData();
                ClearInputFields();
            }
            else MessageBox.Show("Please select an entry to delete.", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearInputFields();
        }

        private void ClearInputFields()
        {
            websiteTxtBox.Clear();
            usernameTxtBox.Clear();
            passwordTxtBox.Clear();
            LoadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            PasswordManagerDGV.SelectionChanged += DataGridView1_SelectionChanged;
        }

        #endregion

        #region Files Encryptor Tab

        private void BackupPasswordButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FilesEncryptionEnterPwdTxtBox.Text))
            {
                MessageBox.Show("Please enter a password to backup.", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Password backup has been saved. Keep this file safe!", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving password backup: {ex.Message}", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

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
                    MessageBox.Show("Password has been loaded from backup.", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading password backup: {ex.Message}", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No file selected.", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

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


        private async void EncryptButton_Click_1(object sender, EventArgs e)
        {

            if (selectedFiles1.Count == 0)
            {
                MessageBox.Show("Please select files first.", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(FilesEncryptionEnterPwdTxtBox.Text))
            {
                MessageBox.Show("Please enter a password.", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBox.Show($"Error encrypting {Path.GetFileName(file)}: {ex.Message}", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    });
                });

                MessageBox.Show("All files encrypted successfully!", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during encryption: {ex.Message}", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EnableControls();
            }
        }

        private async void DecryptButton_Click_1(object sender, EventArgs e)
        {
            if (selectedFiles1.Count == 0)
            {
                MessageBox.Show("Please select files first.", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(FilesEncryptionEnterPwdTxtBox.Text))
            {
                MessageBox.Show("Please enter a password.", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBox.Show($"Error decrypting {Path.GetFileName(file)}: {ex.Message}", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    });
                });

                MessageBox.Show("All files decrypted successfully!", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during decryption: {ex.Message}", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private async Task ProcessFileInPlace(string filePath, string password, bool encrypt)
        {
            string tempFile = Path.GetTempFileName();
            const int bufferSize = 81920; // 80 KB buffer size

            try
            {
                byte[] salt = new byte[32];
                byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

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
        private void clear_Click(object sender, EventArgs e)
        {
            FileEncryptionFilesNumberTxtBox.Clear();
            FilesEncryptionFilesListBox.Items.Clear();
            FilesEncryptionEnterPwdTxtBox.Clear();
            selectedFiles1.Clear();
            progressBar.Visible = false;
        }
        private byte[] GenerateRandomSalt()
        {
            byte[] salt = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
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

        private void GeneratePasswordButton_Click(object sender, EventArgs e)
        {
            FilesEncryptionEnterPwdTxtBox.Text = GeneratePassword();
        }

        #endregion

        #region Files Renamer Tab

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
                    MessageBox.Show($"Error loading files!", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {

                MessageBox.Show("No files selected!", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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



        private void previewBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RegexPatternTxtBox.Text))
            {
                MessageBox.Show("Please enter a regex pattern.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                RegexRenameFilesBtn.Enabled = hasChanges;

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Invalid regex pattern: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void renameButton_Click(object sender, EventArgs e)
        {
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
                System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
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

            MessageBox.Show(message, "Rename Results",
                MessageBoxButtons.OK, errors.Any() ? MessageBoxIcon.Warning : MessageBoxIcon.Information);


            RegexRenameFilesBtn.Enabled = false;

        }

        private void RegexClearBtn_Click(object sender, EventArgs e)
        {
            RegexPatternTxtBox.Clear();
            RegexReplacementTxtBox.Clear();
            RegexFilesListView.Items.Clear();
            RegexUseIncrementCheckBox.Checked = false;
            RegexStartFromNumeric.Value = 0;
            RegexIncrementNumeric.Value = 0;
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




        private string GeneratePassword(int length = 16)
        {
            const string uppercase = "NZAYBXCWDVEUFTGSHRIJPKOLM";
            const string lowercase = "mlokpjirhsgtfuevdwcxbyazn";
            const string numbers = "73281564";
            const string special = "!@)#}$%>&*(+=]{<?[";

            // Ensure length is divisible by 4
            length = length - (length % 4);
            if (length < 16) length = 16; // Minimum length of 16 to ensure 4 chars of each type

            var random = new Random();
            var chars = new char[length];
            int quarterLength = length / 4;

            // Fill exactly 1/4 with lowercase
            for (int i = 0; i < quarterLength; i++)
            {
                chars[i] = lowercase[random.Next(lowercase.Length)];
            }

            // Fill exactly 1/4 with numbers
            for (int i = quarterLength * 2; i < quarterLength * 3; i++)
            {
                chars[i] = numbers[random.Next(numbers.Length)];
            }

            // Fill exactly 1/4 with uppercase
            for (int i = quarterLength; i < quarterLength * 2; i++)
            {
                chars[i] = uppercase[random.Next(uppercase.Length)];
            }

            // Fill exactly 1/4 with special characters
            for (int i = quarterLength * 3; i < length; i++)
            {
                chars[i] = special[random.Next(special.Length)];
            }

            // Shuffle the entire password
            return new string(chars.OrderBy(x => random.Next()).ToArray());
        }

        private void MainFormCloseBtn_MouseDown(object sender, MouseEventArgs e)
        {
            BackColor = System.Drawing.Color.GhostWhite;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            homeCarousselLbl.Text = hints[counter];
            counter = (counter + 1) % hints.Length;
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            ActiveForm.WindowState = FormWindowState.Minimized;
        }

        private void SubmitNewPasswordBtn_Click(object sender, EventArgs e)
        {
            string currentPassword = SecureStorage.GetPassword();
            string databaseFilePath = "C:/Users/hamdi/source/repos/P-GEN/bin/Debug/net8.0-windows10.0.17763.0/encrypted_pwd_database.db";

            if (oldPasswordTxtBox.Text != currentPassword)
            {
                MessageBox.Show("Current password is incorrect!", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (NewPasswordTxtBox.Text.Length == 0)
            {
                MessageBox.Show("Please enter a new password!", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (NewPasswordTxtBox.Text != RepeatNewPasswordTxtBox.Text)
            {
                MessageBox.Show("New passwords do not match!", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (NewPasswordTxtBox.Text.Length < 8)
            {
                MessageBox.Show("New password must be at least 8 characters long!", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SecureStorage.ChangeDatabasePassword(databaseFilePath, currentPassword, RepeatNewPasswordTxtBox.Text);
            SecureStorage.UpdatePassword(RepeatNewPasswordTxtBox.Text);
            MessageBox.Show("Password changed successfully! Application will be restarted.", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Restart();
        }
    }
}


