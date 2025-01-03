namespace CipherSync
{
    partial class LoginForm
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
            label1 = new System.Windows.Forms.Label();
            CancelLoginMasterPwdBtn = new System.Windows.Forms.Button();
            SubmitLoginPwdBtn = new System.Windows.Forms.Button();
            LoginMasterPwdTxtBox = new System.Windows.Forms.TextBox();
            LoginPwdLoadBackupBtn = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(23, 33);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(138, 18);
            label1.TabIndex = 15;
            label1.Text = "Enter Lock Password:";
            // 
            // CancelLoginMasterPwdBtn
            // 
            CancelLoginMasterPwdBtn.BackColor = System.Drawing.Color.PowderBlue;
            CancelLoginMasterPwdBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            CancelLoginMasterPwdBtn.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            CancelLoginMasterPwdBtn.Location = new System.Drawing.Point(228, 147);
            CancelLoginMasterPwdBtn.Name = "CancelLoginMasterPwdBtn";
            CancelLoginMasterPwdBtn.Size = new System.Drawing.Size(158, 33);
            CancelLoginMasterPwdBtn.TabIndex = 4;
            CancelLoginMasterPwdBtn.Text = "Cancel";
            CancelLoginMasterPwdBtn.UseVisualStyleBackColor = false;
            CancelLoginMasterPwdBtn.Click += CancelLoginMasterPwdBtn_Click;
            // 
            // SubmitLoginPwdBtn
            // 
            SubmitLoginPwdBtn.BackColor = System.Drawing.Color.PowderBlue;
            SubmitLoginPwdBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            SubmitLoginPwdBtn.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            SubmitLoginPwdBtn.Location = new System.Drawing.Point(228, 81);
            SubmitLoginPwdBtn.Name = "SubmitLoginPwdBtn";
            SubmitLoginPwdBtn.Size = new System.Drawing.Size(158, 33);
            SubmitLoginPwdBtn.TabIndex = 2;
            SubmitLoginPwdBtn.Text = "Submit";
            SubmitLoginPwdBtn.UseVisualStyleBackColor = false;
            SubmitLoginPwdBtn.Click += SubmitLoginPwdBtn_Click;
            // 
            // LoginMasterPwdTxtBox
            // 
            LoginMasterPwdTxtBox.BackColor = System.Drawing.Color.FromArgb(41, 42, 45);
            LoginMasterPwdTxtBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            LoginMasterPwdTxtBox.Location = new System.Drawing.Point(199, 33);
            LoginMasterPwdTxtBox.Name = "LoginMasterPwdTxtBox";
            LoginMasterPwdTxtBox.Size = new System.Drawing.Size(197, 26);
            LoginMasterPwdTxtBox.TabIndex = 1;
            LoginMasterPwdTxtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LoginPwdLoadBackupBtn
            // 
            LoginPwdLoadBackupBtn.BackColor = System.Drawing.Color.PowderBlue;
            LoginPwdLoadBackupBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            LoginPwdLoadBackupBtn.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            LoginPwdLoadBackupBtn.Location = new System.Drawing.Point(23, 81);
            LoginPwdLoadBackupBtn.Name = "LoginPwdLoadBackupBtn";
            LoginPwdLoadBackupBtn.Size = new System.Drawing.Size(158, 33);
            LoginPwdLoadBackupBtn.TabIndex = 3;
            LoginPwdLoadBackupBtn.Text = "Load Backup";
            LoginPwdLoadBackupBtn.UseVisualStyleBackColor = false;
            LoginPwdLoadBackupBtn.Click += LoginPwdLoadBackupBtn_Click;
            // 
            // LoginForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackColor = System.Drawing.Color.FromArgb(32, 33, 36);
            ClientSize = new System.Drawing.Size(419, 206);
            Controls.Add(LoginPwdLoadBackupBtn);
            Controls.Add(label1);
            Controls.Add(CancelLoginMasterPwdBtn);
            Controls.Add(SubmitLoginPwdBtn);
            Controls.Add(LoginMasterPwdTxtBox);
            Font = new System.Drawing.Font("Comic Sans MS", 9.75F);
            ForeColor = System.Drawing.SystemColors.ControlLightLight;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "CipherSync";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CancelLoginMasterPwdBtn;
        private System.Windows.Forms.Button SubmitLoginPwdBtn;
        private System.Windows.Forms.Button LoginPwdLoadBackupBtn;
        public System.Windows.Forms.TextBox LoginMasterPwdTxtBox;
    }
}