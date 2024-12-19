using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CipherSync.Models;
using CipherSync.Helpers;


namespace CipherSync
{
    public partial class Form1 : Form
    {
        private List<string> selectedFiles1 = new List<string>();
        private List<string> selectedFiles2 = new List<string>();
        private readonly byte[] backupKey = { 132, 42, 53, 84, 75, 96, 37, 28, 99, 10, 11, 12, 13, 14, 15, 16 };
        private readonly DatabaseHelper db;

        public Form1()
        {
            InitializeComponent();
            // Initialize SQLitePCL to use SQLCipher
            SQLitePCL.Batteries_V2.Init();
            db = new DatabaseHelper();
            LoadData();
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;

        }

        private void LoadData()
        {
            var entries = db.GetAllEntries().ToList();
            dataGridView1.DataSource = entries;
            if (dataGridView1.Columns["Id"] != null)
            {
                dataGridView1.Columns["Id"].Visible = false;
            }

        }
        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) { DataGridViewRow selectedRow = dataGridView1.SelectedRows[0]; websiteTextBox.Text = selectedRow.Cells["Website"].Value?.ToString(); usernameTextBox.Text = selectedRow.Cells["Username"].Value?.ToString(); passwordTextBox.Text = selectedRow.Cells["Password"].Value?.ToString(); }
        }

        private void BrowseFiles_Click(object sender, EventArgs e)
        {
            FilesNumber.Clear();
            filesListBox.Items.Clear();
            passwordTextBox.Clear();
            selectedFiles1.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (filesListBox != null)
                {
                    foreach (string file in ofd.FileNames)
                    {
                        if (!selectedFiles1.Contains(file))
                        {
                            selectedFiles1.Add(file);
                            filesListBox.Items.Add(Path.GetFileName(file));
                        }
                    }
                }
                if (FilesNumber != null)
                    FilesNumber.Text = $"{selectedFiles1.Count} files selected";
            }
        }

        private string GeneratePassword(int length = 16)
        {
            const string uppercase = "NZAYBXCWDVEUFTGSHRIJPKOLM";
            const string lowercase = "mlokpjirhsgtfuevdwcxbyazn";
            const string numbers = "73281564";
            const string special = "!@)#}$%>&*(+=]{<?[";

            // Ensure length is divisible by 4
            length = length - (length % 4);
            if (length < 16) length = 16; // Minimum length of 8 to ensure 2 chars of each type

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



        private void GeneratePasswordButton_Click(object sender, EventArgs e)
        {
            passwordTextBox.Text = GeneratePassword();
        }

        private void BackupPasswordButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(passwordTextBox.Text))
            {
                MessageBox.Show("Please enter a password to backup.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            sfd.Filter = "Password files (*.pwd)|*.pwd|All files (*.*)|*.*";
            sfd.FilterIndex = 1;
            sfd.DefaultExt = "pwd";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    byte[] encryptedPassword = EncryptPassword(passwordTextBox.Text);
                    File.WriteAllBytes(sfd.FileName, encryptedPassword);
                    MessageBox.Show("Password backup has been saved. Keep this file safe!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving password backup: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void LoadBackupButton_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Filter = "Password files (*.pwd)|*.pwd|All files (*.*)|*.*";
            ofd.FilterIndex = 1;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    byte[] encryptedPassword = File.ReadAllBytes(ofd.FileName);
                    string decryptedPassword = DecryptPassword(encryptedPassword);
                    if (passwordTextBox != null)
                    {
                        passwordTextBox.Text = decryptedPassword;
                    }
                    MessageBox.Show("Password has been loaded from backup.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading password backup: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                MessageBox.Show("Please select files first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(passwordTextBox.Text))
            {
                MessageBox.Show("Please enter a password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            await ProcessFileInPlace(file, passwordTextBox.Text, true);

                            progressBar.Invoke(new Action(() => UpdateProgress(++progressBar.Value, selectedFiles1.Count)));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error encrypting {Path.GetFileName(file)}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    });
                });

                MessageBox.Show("All files encrypted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during encryption: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Please select files first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(passwordTextBox.Text))
            {
                MessageBox.Show("Please enter a password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            await ProcessFileInPlace(file, passwordTextBox.Text, false);

                            progressBar.Invoke(new Action(() => UpdateProgress(++progressBar.Value, selectedFiles1.Count)));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error decrypting {Path.GetFileName(file)}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    });
                });

                MessageBox.Show("All files decrypted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during decryption: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (passwordTextBox.Text != null)
                passwordTextBox.Enabled = false;
        }

        private void EnableControls()
        {
            foreach (var button in Controls.OfType<Button>())
            {
                button.Enabled = true;
            }
            if (passwordTextBox.Text != null)
                passwordTextBox.Enabled = true;
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
            FilesNumber.Clear();
            filesListBox.Items.Clear();
            passwordTextBox.Clear();
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

            counter += (int)incrementByNumeric.Value;
            return newName;
        }

        private void BrowseFilesBtnToRename_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Filter = "All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                selectedFiles2.Clear();
                filesListView.Items.Clear();

                try
                {
                    foreach (string filePath in ofd.FileNames)
                    {
                        var fileInfo = new FileInfo(filePath);
                        selectedFiles2.Add(fileInfo.FullName);
                        var item = new ListViewItem(new[] { fileInfo.Name, fileInfo.Name });
                        filesListView.Items.Add(item);
                    }

                    previewBtn.Enabled = selectedFiles2.Count > 0;
                    statusLabel.Text = $"Loaded {selectedFiles2.Count} files";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading files: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void previewBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(patternTextBox.Text))
            {
                MessageBox.Show("Please enter a regex pattern.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                filesListView.Items.Clear();
                bool hasChanges = false;

                // Initialize counter
                int counter = 1;
                if (useIncrementCheckBox.Checked && int.TryParse(startNumberNumeric.Text, out int startNumber))
                {
                    counter = startNumber;
                }

                foreach (var file in selectedFiles2)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    string newName = useIncrementCheckBox.Checked
                        ? GetNewFileName(fileInfo, patternTextBox.Text, replacementTextBox.Text, ref counter)
                        : new Regex(patternTextBox.Text).Replace(fileInfo.Name, replacementTextBox.Text);

                    var item = new ListViewItem(new[] { fileInfo.Name, newName });
                    filesListView.Items.Add(item);

                    if (fileInfo.Name != newName)
                        hasChanges = true;
                }

                renameButton.Enabled = hasChanges;
                statusLabel.Text = "Preview generated. Click 'Rename Files' to apply changes.";
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
            if (useIncrementCheckBox.Checked && int.TryParse(startNumberNumeric.Text, out int startNumber))
            {
                counter = startNumber;
            }

            foreach (var file in selectedFiles2.ToList())
            {
                FileInfo fileInfo = new FileInfo(file);
                try
                {

                    string newName = useIncrementCheckBox.Checked
                        ? GetNewFileName(fileInfo, patternTextBox.Text, replacementTextBox.Text, ref counter)
                        : new Regex(patternTextBox.Text).Replace(fileInfo.Name, replacementTextBox.Text);

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
                filesListView.Items.Clear();
                System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
                foreach (string filePath in ofd.FileNames)
                {
                    var fileInfo = new FileInfo(filePath);
                    string fileName = fileInfo.Name;
                    selectedFiles2.Add(fileName);
                    var item = new ListViewItem(new[] { fileInfo.Name, fileInfo.Name });
                    filesListView.Items.Add(item);
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

            statusLabel.Text = $"Renamed {successCount} files. {errors.Count} errors.";
            renameButton.Enabled = false;

        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            resultsTextBox.Clear();
            int count = (int)countNumeric.Value;
            int length = (int)lengthNumeric.Value;

            for (int i = 0; i < count; i++)
            {
                string password = GeneratePassword(length); // Corrected: now returns a string
                resultsTextBox.AppendText($"Password {i + 1}: {password}\r\n");
            }

            // Enable buttons if passwords were generated
            bool hasPasswords = resultsTextBox.Text.Length > 0;
            copyButton.Enabled = hasPasswords;
            exportButton.Enabled = hasPasswords;
            clearButton.Enabled = hasPasswords;
        }
        private void addButton_Click_1(object sender, EventArgs e)
        {
            var entry = new PasswordEntry
            {
                Website = websiteTextBox.Text,
                Username = usernameTextBox.Text,
                Password = passwordTxtBox.Text
            };
            db.AddEntry(entry);
            LoadData();
            ClearInputFields();
        }

        private void editButton_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {

                int id = (int)dataGridView1.CurrentRow.Cells["Id"].Value;
                var entry = db.GetAllEntries().FirstOrDefault(e => e.Id == id);
                if (entry != null)
                {
                    entry.Website = websiteTextBox.Text;
                    entry.Username = usernameTextBox.Text;
                    entry.Password = passwordTextBox.Text;
                    db.UpdateEntry(entry);
                    MessageBox.Show($"Updating Id: {id}, Website: {entry.Website}, Username: {entry.Username}, Password: {entry.Password}");

                    LoadData();
                    ClearInputFields();
                }
            }
        }

        private void deleteButton_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                db.DeleteEntry(id);
                LoadData();
                ClearInputFields();
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearInputFields();
        }

        private void ClearInputFields()
        {
            websiteTextBox.Clear();
            usernameTextBox.Clear();
            passwordTxtBox.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
        }
    }
}


