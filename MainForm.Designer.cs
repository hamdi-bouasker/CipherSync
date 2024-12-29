namespace CipherSync
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            filesListBox = new System.Windows.Forms.ListBox();
            progressBar = new System.Windows.Forms.ProgressBar();
            label1 = new System.Windows.Forms.Label();
            passwordTextBox = new System.Windows.Forms.TextBox();
            DecryptButton = new System.Windows.Forms.Button();
            EncryptButton = new System.Windows.Forms.Button();
            clear = new System.Windows.Forms.Button();
            LoadBackupButton = new System.Windows.Forms.Button();
            BackupPasswordButton = new System.Windows.Forms.Button();
            GeneratePasswordButton = new System.Windows.Forms.Button();
            BrowseFiles = new System.Windows.Forms.Button();
            FilesNumber = new System.Windows.Forms.TextBox();
            tabPage2 = new System.Windows.Forms.TabPage();
            statusLabel = new System.Windows.Forms.Label();
            filesListView = new System.Windows.Forms.ListView();
            columnHeader1 = new System.Windows.Forms.ColumnHeader();
            columnHeader2 = new System.Windows.Forms.ColumnHeader();
            button11 = new System.Windows.Forms.Button();
            renameButton = new System.Windows.Forms.Button();
            previewBtn = new System.Windows.Forms.Button();
            BrowseFilesBtnToRename = new System.Windows.Forms.Button();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            incrementByNumeric = new System.Windows.Forms.NumericUpDown();
            startNumberNumeric = new System.Windows.Forms.NumericUpDown();
            useIncrementCheckBox = new System.Windows.Forms.CheckBox();
            replacementTextBox = new System.Windows.Forms.TextBox();
            patternTextBox = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            tabPage3 = new System.Windows.Forms.TabPage();
            clearButton = new System.Windows.Forms.Button();
            GenerateButton = new System.Windows.Forms.Button();
            copyButton = new System.Windows.Forms.Button();
            exportButton = new System.Windows.Forms.Button();
            resultsTextBox = new System.Windows.Forms.TextBox();
            lengthNumeric = new System.Windows.Forms.NumericUpDown();
            countNumeric = new System.Windows.Forms.NumericUpDown();
            label8 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            tabPage4 = new System.Windows.Forms.TabPage();
            label10 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            passwordTxtBox = new System.Windows.Forms.TextBox();
            websiteTextBox = new System.Windows.Forms.TextBox();
            usernameTextBox = new System.Windows.Forms.TextBox();
            ClearBtn = new System.Windows.Forms.Button();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            editButton = new System.Windows.Forms.Button();
            deleteButton = new System.Windows.Forms.Button();
            addButton = new System.Windows.Forms.Button();
            tabPage5 = new System.Windows.Forms.TabPage();
            tabPage6 = new System.Windows.Forms.TabPage();
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)incrementByNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)startNumberNumeric).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)lengthNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)countNumeric).BeginInit();
            tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Controls.Add(tabPage6);
            tabControl1.HotTrack = true;
            tabControl1.Location = new System.Drawing.Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(760, 552);
            tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = System.Drawing.Color.FromArgb(32, 33, 36);
            tabPage1.Controls.Add(filesListBox);
            tabPage1.Controls.Add(progressBar);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(passwordTextBox);
            tabPage1.Controls.Add(DecryptButton);
            tabPage1.Controls.Add(EncryptButton);
            tabPage1.Controls.Add(clear);
            tabPage1.Controls.Add(LoadBackupButton);
            tabPage1.Controls.Add(BackupPasswordButton);
            tabPage1.Controls.Add(GeneratePasswordButton);
            tabPage1.Controls.Add(BrowseFiles);
            tabPage1.Controls.Add(FilesNumber);
            tabPage1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            tabPage1.Location = new System.Drawing.Point(4, 27);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(752, 521);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Files Encryption";
            // 
            // filesListBox
            // 
            filesListBox.BackColor = System.Drawing.Color.FromArgb(41, 42, 45);
            filesListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            filesListBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            filesListBox.FormattingEnabled = true;
            filesListBox.ItemHeight = 18;
            filesListBox.Location = new System.Drawing.Point(22, 73);
            filesListBox.Name = "filesListBox";
            filesListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            filesListBox.Size = new System.Drawing.Size(493, 234);
            filesListBox.TabIndex = 12;
            // 
            // progressBar
            // 
            progressBar.BackColor = System.Drawing.Color.FromArgb(32, 33, 36);
            progressBar.ForeColor = System.Drawing.Color.FromArgb(41, 42, 45);
            progressBar.Location = new System.Drawing.Point(22, 489);
            progressBar.Name = "progressBar";
            progressBar.Size = new System.Drawing.Size(493, 23);
            progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            progressBar.TabIndex = 11;
            progressBar.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(215, 335);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(106, 18);
            label1.TabIndex = 10;
            label1.Text = "Enter Password:";
            // 
            // passwordTextBox
            // 
            passwordTextBox.BackColor = System.Drawing.Color.FromArgb(41, 42, 45);
            passwordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            passwordTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            passwordTextBox.Location = new System.Drawing.Point(22, 379);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new System.Drawing.Size(493, 19);
            passwordTextBox.TabIndex = 9;
            passwordTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DecryptButton
            // 
            DecryptButton.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            DecryptButton.ForeColor = System.Drawing.SystemColors.Desktop;
            DecryptButton.Image = (System.Drawing.Image)resources.GetObject("DecryptButton.Image");
            DecryptButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            DecryptButton.Location = new System.Drawing.Point(280, 438);
            DecryptButton.Name = "DecryptButton";
            DecryptButton.Size = new System.Drawing.Size(235, 33);
            DecryptButton.TabIndex = 8;
            DecryptButton.Text = "Decrypt";
            DecryptButton.UseVisualStyleBackColor = true;
            DecryptButton.Click += DecryptButton_Click_1;
            // 
            // EncryptButton
            // 
            EncryptButton.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            EncryptButton.ForeColor = System.Drawing.SystemColors.Desktop;
            EncryptButton.Image = (System.Drawing.Image)resources.GetObject("EncryptButton.Image");
            EncryptButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            EncryptButton.Location = new System.Drawing.Point(22, 438);
            EncryptButton.Name = "EncryptButton";
            EncryptButton.Size = new System.Drawing.Size(235, 33);
            EncryptButton.TabIndex = 7;
            EncryptButton.Text = "Encrypt";
            EncryptButton.UseVisualStyleBackColor = true;
            EncryptButton.Click += EncryptButton_Click_1;
            // 
            // clear
            // 
            clear.BackColor = System.Drawing.Color.PowderBlue;
            clear.FlatStyle = System.Windows.Forms.FlatStyle.System;
            clear.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            clear.Location = new System.Drawing.Point(565, 438);
            clear.Name = "clear";
            clear.Size = new System.Drawing.Size(158, 33);
            clear.TabIndex = 6;
            clear.Text = "Clear";
            clear.UseVisualStyleBackColor = false;
            clear.Click += clear_Click;
            // 
            // LoadBackupButton
            // 
            LoadBackupButton.BackColor = System.Drawing.Color.PowderBlue;
            LoadBackupButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            LoadBackupButton.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            LoadBackupButton.Location = new System.Drawing.Point(565, 277);
            LoadBackupButton.Name = "LoadBackupButton";
            LoadBackupButton.Size = new System.Drawing.Size(158, 33);
            LoadBackupButton.TabIndex = 5;
            LoadBackupButton.Text = "Load Backup Password";
            LoadBackupButton.UseVisualStyleBackColor = false;
            LoadBackupButton.Click += LoadBackupButton_Click;
            // 
            // BackupPasswordButton
            // 
            BackupPasswordButton.BackColor = System.Drawing.Color.PowderBlue;
            BackupPasswordButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            BackupPasswordButton.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            BackupPasswordButton.Location = new System.Drawing.Point(565, 173);
            BackupPasswordButton.Name = "BackupPasswordButton";
            BackupPasswordButton.Size = new System.Drawing.Size(158, 33);
            BackupPasswordButton.TabIndex = 4;
            BackupPasswordButton.Text = "Backup Password";
            BackupPasswordButton.UseVisualStyleBackColor = false;
            BackupPasswordButton.Click += BackupPasswordButton_Click;
            // 
            // GeneratePasswordButton
            // 
            GeneratePasswordButton.BackColor = System.Drawing.Color.PowderBlue;
            GeneratePasswordButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            GeneratePasswordButton.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            GeneratePasswordButton.Location = new System.Drawing.Point(565, 73);
            GeneratePasswordButton.Name = "GeneratePasswordButton";
            GeneratePasswordButton.Size = new System.Drawing.Size(158, 33);
            GeneratePasswordButton.TabIndex = 3;
            GeneratePasswordButton.Text = "Generate Password";
            GeneratePasswordButton.UseVisualStyleBackColor = false;
            GeneratePasswordButton.Click += GeneratePasswordButton_Click;
            // 
            // BrowseFiles
            // 
            BrowseFiles.BackColor = System.Drawing.Color.PowderBlue;
            BrowseFiles.FlatStyle = System.Windows.Forms.FlatStyle.System;
            BrowseFiles.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            BrowseFiles.Location = new System.Drawing.Point(565, 17);
            BrowseFiles.Name = "BrowseFiles";
            BrowseFiles.Size = new System.Drawing.Size(158, 33);
            BrowseFiles.TabIndex = 1;
            BrowseFiles.Text = "Browse";
            BrowseFiles.UseVisualStyleBackColor = false;
            BrowseFiles.Click += BrowseFiles_Click;
            // 
            // FilesNumber
            // 
            FilesNumber.BackColor = System.Drawing.Color.FromArgb(41, 42, 45);
            FilesNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            FilesNumber.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            FilesNumber.Location = new System.Drawing.Point(22, 24);
            FilesNumber.Name = "FilesNumber";
            FilesNumber.ReadOnly = true;
            FilesNumber.Size = new System.Drawing.Size(493, 19);
            FilesNumber.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = System.Drawing.Color.FromArgb(32, 33, 36);
            tabPage2.Controls.Add(statusLabel);
            tabPage2.Controls.Add(filesListView);
            tabPage2.Controls.Add(button11);
            tabPage2.Controls.Add(renameButton);
            tabPage2.Controls.Add(previewBtn);
            tabPage2.Controls.Add(BrowseFilesBtnToRename);
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(incrementByNumeric);
            tabPage2.Controls.Add(startNumberNumeric);
            tabPage2.Controls.Add(useIncrementCheckBox);
            tabPage2.Controls.Add(replacementTextBox);
            tabPage2.Controls.Add(patternTextBox);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(label2);
            tabPage2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            tabPage2.Location = new System.Drawing.Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(3);
            tabPage2.Size = new System.Drawing.Size(752, 524);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Regex Files Renamer";
            // 
            // statusLabel
            // 
            statusLabel.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            statusLabel.Location = new System.Drawing.Point(574, 474);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new System.Drawing.Size(133, 26);
            statusLabel.TabIndex = 15;
            statusLabel.Text = "Ready";
            statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // filesListView
            // 
            filesListView.BackColor = System.Drawing.Color.FromArgb(32, 33, 36);
            filesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnHeader1, columnHeader2 });
            filesListView.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            filesListView.Location = new System.Drawing.Point(33, 224);
            filesListView.Name = "filesListView";
            filesListView.Size = new System.Drawing.Size(506, 276);
            filesListView.TabIndex = 14;
            filesListView.UseCompatibleStateImageBehavior = false;
            filesListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Current Name";
            columnHeader1.Width = 250;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "New Name";
            columnHeader2.Width = 250;
            // 
            // button11
            // 
            button11.ForeColor = System.Drawing.SystemColors.Desktop;
            button11.Location = new System.Drawing.Point(577, 343);
            button11.Name = "button11";
            button11.Size = new System.Drawing.Size(130, 26);
            button11.TabIndex = 13;
            button11.Text = "Regex Help";
            button11.UseVisualStyleBackColor = true;
            // 
            // renameButton
            // 
            renameButton.ForeColor = System.Drawing.SystemColors.Desktop;
            renameButton.Location = new System.Drawing.Point(577, 224);
            renameButton.Name = "renameButton";
            renameButton.Size = new System.Drawing.Size(130, 26);
            renameButton.TabIndex = 12;
            renameButton.Text = "Rename Files";
            renameButton.UseVisualStyleBackColor = true;
            renameButton.Click += renameButton_Click;
            // 
            // previewBtn
            // 
            previewBtn.ForeColor = System.Drawing.SystemColors.Desktop;
            previewBtn.Location = new System.Drawing.Point(577, 82);
            previewBtn.Name = "previewBtn";
            previewBtn.Size = new System.Drawing.Size(130, 26);
            previewBtn.TabIndex = 11;
            previewBtn.Text = "Preview Changes";
            previewBtn.UseVisualStyleBackColor = true;
            previewBtn.Click += previewBtn_Click;
            // 
            // BrowseFilesBtnToRename
            // 
            BrowseFilesBtnToRename.ForeColor = System.Drawing.SystemColors.Desktop;
            BrowseFilesBtnToRename.Location = new System.Drawing.Point(577, 21);
            BrowseFilesBtnToRename.Name = "BrowseFilesBtnToRename";
            BrowseFilesBtnToRename.Size = new System.Drawing.Size(130, 26);
            BrowseFilesBtnToRename.TabIndex = 10;
            BrowseFilesBtnToRename.Text = "Browse Files";
            BrowseFilesBtnToRename.UseVisualStyleBackColor = true;
            BrowseFilesBtnToRename.Click += BrowseFilesBtnToRename_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(367, 150);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(70, 18);
            label5.TabIndex = 8;
            label5.Text = "Increment";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(147, 148);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(74, 18);
            label4.TabIndex = 7;
            label4.Text = "Start From";
            // 
            // incrementByNumeric
            // 
            incrementByNumeric.Location = new System.Drawing.Point(472, 148);
            incrementByNumeric.Name = "incrementByNumeric";
            incrementByNumeric.Size = new System.Drawing.Size(67, 26);
            incrementByNumeric.TabIndex = 6;
            // 
            // startNumberNumeric
            // 
            startNumberNumeric.Location = new System.Drawing.Point(257, 148);
            startNumberNumeric.Name = "startNumberNumeric";
            startNumberNumeric.Size = new System.Drawing.Size(67, 26);
            startNumberNumeric.TabIndex = 5;
            // 
            // useIncrementCheckBox
            // 
            useIncrementCheckBox.AutoSize = true;
            useIncrementCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            useIncrementCheckBox.Location = new System.Drawing.Point(26, 148);
            useIncrementCheckBox.Name = "useIncrementCheckBox";
            useIncrementCheckBox.Size = new System.Drawing.Size(101, 22);
            useIncrementCheckBox.TabIndex = 4;
            useIncrementCheckBox.Text = "Use Counter";
            useIncrementCheckBox.UseVisualStyleBackColor = true;
            // 
            // replacementTextBox
            // 
            replacementTextBox.BackColor = System.Drawing.Color.FromArgb(41, 42, 45);
            replacementTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            replacementTextBox.Location = new System.Drawing.Point(150, 82);
            replacementTextBox.Name = "replacementTextBox";
            replacementTextBox.Size = new System.Drawing.Size(389, 26);
            replacementTextBox.TabIndex = 3;
            // 
            // patternTextBox
            // 
            patternTextBox.BackColor = System.Drawing.Color.FromArgb(41, 42, 45);
            patternTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            patternTextBox.Location = new System.Drawing.Point(150, 21);
            patternTextBox.Name = "patternTextBox";
            patternTextBox.Size = new System.Drawing.Size(389, 26);
            patternTextBox.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(30, 85);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(87, 18);
            label3.TabIndex = 1;
            label3.Text = "Replacement:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(30, 24);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(97, 18);
            label2.TabIndex = 0;
            label2.Text = "Regex Pattern:";
            // 
            // tabPage3
            // 
            tabPage3.BackColor = System.Drawing.Color.FromArgb(32, 33, 36);
            tabPage3.Controls.Add(clearButton);
            tabPage3.Controls.Add(GenerateButton);
            tabPage3.Controls.Add(copyButton);
            tabPage3.Controls.Add(exportButton);
            tabPage3.Controls.Add(resultsTextBox);
            tabPage3.Controls.Add(lengthNumeric);
            tabPage3.Controls.Add(countNumeric);
            tabPage3.Controls.Add(label8);
            tabPage3.Controls.Add(label7);
            tabPage3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            tabPage3.Location = new System.Drawing.Point(4, 27);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new System.Windows.Forms.Padding(3);
            tabPage3.Size = new System.Drawing.Size(752, 521);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Password Generator";
            // 
            // clearButton
            // 
            clearButton.ForeColor = System.Drawing.SystemColors.Desktop;
            clearButton.Location = new System.Drawing.Point(615, 340);
            clearButton.Name = "clearButton";
            clearButton.Size = new System.Drawing.Size(119, 31);
            clearButton.TabIndex = 8;
            clearButton.Text = "Clear";
            clearButton.UseVisualStyleBackColor = true;
            // 
            // GenerateButton
            // 
            GenerateButton.ForeColor = System.Drawing.SystemColors.Desktop;
            GenerateButton.Location = new System.Drawing.Point(190, 428);
            GenerateButton.Name = "GenerateButton";
            GenerateButton.Size = new System.Drawing.Size(236, 31);
            GenerateButton.TabIndex = 7;
            GenerateButton.Text = "Generate Passwords";
            GenerateButton.UseVisualStyleBackColor = true;
            GenerateButton.Click += GenerateButton_Click;
            // 
            // copyButton
            // 
            copyButton.ForeColor = System.Drawing.SystemColors.Desktop;
            copyButton.Location = new System.Drawing.Point(615, 90);
            copyButton.Name = "copyButton";
            copyButton.Size = new System.Drawing.Size(119, 31);
            copyButton.TabIndex = 6;
            copyButton.Text = "Copy";
            copyButton.UseVisualStyleBackColor = true;
            // 
            // exportButton
            // 
            exportButton.ForeColor = System.Drawing.SystemColors.Desktop;
            exportButton.Location = new System.Drawing.Point(615, 213);
            exportButton.Name = "exportButton";
            exportButton.Size = new System.Drawing.Size(119, 31);
            exportButton.TabIndex = 5;
            exportButton.Text = "Export";
            exportButton.UseVisualStyleBackColor = true;
            // 
            // resultsTextBox
            // 
            resultsTextBox.BackColor = System.Drawing.Color.FromArgb(41, 42, 45);
            resultsTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            resultsTextBox.Location = new System.Drawing.Point(44, 90);
            resultsTextBox.Multiline = true;
            resultsTextBox.Name = "resultsTextBox";
            resultsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            resultsTextBox.Size = new System.Drawing.Size(553, 281);
            resultsTextBox.TabIndex = 4;
            resultsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lengthNumeric
            // 
            lengthNumeric.Location = new System.Drawing.Point(517, 26);
            lengthNumeric.Minimum = new decimal(new int[] { 16, 0, 0, 0 });
            lengthNumeric.Name = "lengthNumeric";
            lengthNumeric.Size = new System.Drawing.Size(80, 26);
            lengthNumeric.TabIndex = 3;
            lengthNumeric.Value = new decimal(new int[] { 16, 0, 0, 0 });
            // 
            // countNumeric
            // 
            countNumeric.Location = new System.Drawing.Point(214, 20);
            countNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            countNumeric.Name = "countNumeric";
            countNumeric.Size = new System.Drawing.Size(80, 26);
            countNumeric.TabIndex = 2;
            countNumeric.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(377, 28);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(113, 18);
            label8.TabIndex = 1;
            label8.Text = "Password Length:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(41, 28);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(144, 18);
            label7.TabIndex = 0;
            label7.Text = "Number of passwords:";
            // 
            // tabPage4
            // 
            tabPage4.BackColor = System.Drawing.Color.FromArgb(32, 33, 36);
            tabPage4.Controls.Add(label10);
            tabPage4.Controls.Add(label9);
            tabPage4.Controls.Add(label6);
            tabPage4.Controls.Add(passwordTxtBox);
            tabPage4.Controls.Add(websiteTextBox);
            tabPage4.Controls.Add(usernameTextBox);
            tabPage4.Controls.Add(ClearBtn);
            tabPage4.Controls.Add(dataGridView1);
            tabPage4.Controls.Add(editButton);
            tabPage4.Controls.Add(deleteButton);
            tabPage4.Controls.Add(addButton);
            tabPage4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            tabPage4.Location = new System.Drawing.Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new System.Windows.Forms.Padding(3);
            tabPage4.Size = new System.Drawing.Size(752, 524);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Passwords Manager";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(73, 210);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(69, 18);
            label10.TabIndex = 21;
            label10.Text = "Password:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(73, 121);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(62, 18);
            label9.TabIndex = 20;
            label9.Text = "Website:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(73, 172);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(176, 18);
            label6.TabIndex = 19;
            label6.Text = "Username or Email address:";
            // 
            // passwordTxtBox
            // 
            passwordTxtBox.BackColor = System.Drawing.Color.FromArgb(41, 42, 45);
            passwordTxtBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            passwordTxtBox.Location = new System.Drawing.Point(284, 207);
            passwordTxtBox.Name = "passwordTxtBox";
            passwordTxtBox.Size = new System.Drawing.Size(389, 26);
            passwordTxtBox.TabIndex = 18;
            // 
            // websiteTextBox
            // 
            websiteTextBox.BackColor = System.Drawing.Color.FromArgb(41, 42, 45);
            websiteTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            websiteTextBox.Location = new System.Drawing.Point(284, 113);
            websiteTextBox.Name = "websiteTextBox";
            websiteTextBox.Size = new System.Drawing.Size(389, 26);
            websiteTextBox.TabIndex = 17;
            // 
            // usernameTextBox
            // 
            usernameTextBox.BackColor = System.Drawing.Color.FromArgb(41, 42, 45);
            usernameTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            usernameTextBox.Location = new System.Drawing.Point(284, 164);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new System.Drawing.Size(389, 26);
            usernameTextBox.TabIndex = 16;
            // 
            // ClearBtn
            // 
            ClearBtn.ForeColor = System.Drawing.SystemColors.Desktop;
            ClearBtn.Location = new System.Drawing.Point(543, 65);
            ClearBtn.Name = "ClearBtn";
            ClearBtn.Size = new System.Drawing.Size(130, 26);
            ClearBtn.TabIndex = 15;
            ClearBtn.Text = "Clear";
            ClearBtn.UseVisualStyleBackColor = true;
            ClearBtn.Click += ClearBtn_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(41, 42, 45);
            dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new System.Drawing.Point(73, 249);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new System.Drawing.Size(600, 254);
            dataGridView1.TabIndex = 14;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // editButton
            // 
            editButton.ForeColor = System.Drawing.SystemColors.Desktop;
            editButton.Location = new System.Drawing.Point(227, 65);
            editButton.Name = "editButton";
            editButton.Size = new System.Drawing.Size(130, 26);
            editButton.TabIndex = 13;
            editButton.Text = "Update";
            editButton.UseVisualStyleBackColor = true;
            editButton.Click += editButton_Click_1;
            // 
            // deleteButton
            // 
            deleteButton.ForeColor = System.Drawing.SystemColors.Desktop;
            deleteButton.Location = new System.Drawing.Point(384, 65);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new System.Drawing.Size(130, 26);
            deleteButton.TabIndex = 12;
            deleteButton.Text = "Delete";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += deleteButton_Click_1;
            // 
            // addButton
            // 
            addButton.ForeColor = System.Drawing.SystemColors.Desktop;
            addButton.Location = new System.Drawing.Point(73, 65);
            addButton.Name = "addButton";
            addButton.Size = new System.Drawing.Size(130, 26);
            addButton.TabIndex = 11;
            addButton.Text = "Add";
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += addButton_Click_1;
            // 
            // tabPage5
            // 
            tabPage5.Location = new System.Drawing.Point(4, 24);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new System.Windows.Forms.Padding(3);
            tabPage5.Size = new System.Drawing.Size(752, 524);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "Help";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            tabPage6.Location = new System.Drawing.Point(4, 24);
            tabPage6.Name = "tabPage6";
            tabPage6.Padding = new System.Windows.Forms.Padding(3);
            tabPage6.Size = new System.Drawing.Size(752, 524);
            tabPage6.TabIndex = 5;
            tabPage6.Text = "About";
            tabPage6.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Multiselect = true;
            // 
            // openFileDialog2
            // 
            openFileDialog2.FileName = "openFileDialog2";
            // 
            // Form1
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackColor = System.Drawing.Color.FromArgb(32, 33, 36);
            ClientSize = new System.Drawing.Size(784, 576);
            Controls.Add(tabControl1);
            Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            Margin = new System.Windows.Forms.Padding(4);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "P-GEN";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)incrementByNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)startNumberNumeric).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)lengthNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)countNumeric).EndInit();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Button BrowseFiles;
        private System.Windows.Forms.TextBox FilesNumber;
        private System.Windows.Forms.Button DecryptButton;
        private System.Windows.Forms.Button EncryptButton;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.Button LoadBackupButton;
        private System.Windows.Forms.Button BackupPasswordButton;
        private System.Windows.Forms.Button GeneratePasswordButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.CheckBox useIncrementCheckBox;
        private System.Windows.Forms.TextBox replacementTextBox;
        private System.Windows.Forms.TextBox patternTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown incrementByNumeric;
        private System.Windows.Forms.NumericUpDown startNumberNumeric;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button renameButton;
        private System.Windows.Forms.Button previewBtn;
        private System.Windows.Forms.Button BrowseFilesBtnToRename;
        private System.Windows.Forms.ListView filesListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.NumericUpDown lengthNumeric;
        private System.Windows.Forms.NumericUpDown countNumeric;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.Button copyButton;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.TextBox resultsTextBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ListBox filesListBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button ClearBtn;
        private System.Windows.Forms.TextBox passwordTxtBox;
        private System.Windows.Forms.TextBox websiteTextBox;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
    }
}

