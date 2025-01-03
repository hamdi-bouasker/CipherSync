namespace CipherShield
{
    partial class RegisterMasterPasswordForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterMasterPasswordForm));
            SubmitMasterPwdBtn = new System.Windows.Forms.Button();
            RegisterMasterPwdTxtBox = new System.Windows.Forms.TextBox();
            CancelRegisterMasterPwdBtn = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            RegisterMasterPwdConfirmTxtBox = new System.Windows.Forms.TextBox();
            SuspendLayout();
            // 
            // SubmitMasterPwdBtn
            // 
            SubmitMasterPwdBtn.BackColor = System.Drawing.Color.PowderBlue;
            SubmitMasterPwdBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            SubmitMasterPwdBtn.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            SubmitMasterPwdBtn.Location = new System.Drawing.Point(21, 122);
            SubmitMasterPwdBtn.Name = "SubmitMasterPwdBtn";
            SubmitMasterPwdBtn.Size = new System.Drawing.Size(158, 33);
            SubmitMasterPwdBtn.TabIndex = 3;
            SubmitMasterPwdBtn.Text = "Submit";
            SubmitMasterPwdBtn.UseVisualStyleBackColor = false;
            SubmitMasterPwdBtn.Click += SubmitMasterPwdBtn_Click;
            // 
            // RegisterMasterPwdTxtBox
            // 
            RegisterMasterPwdTxtBox.BackColor = System.Drawing.Color.FromArgb(41, 42, 45);
            RegisterMasterPwdTxtBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            RegisterMasterPwdTxtBox.Location = new System.Drawing.Point(197, 20);
            RegisterMasterPwdTxtBox.Name = "RegisterMasterPwdTxtBox";
            RegisterMasterPwdTxtBox.Size = new System.Drawing.Size(197, 26);
            RegisterMasterPwdTxtBox.TabIndex = 1;
            RegisterMasterPwdTxtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CancelRegisterMasterPwdBtn
            // 
            CancelRegisterMasterPwdBtn.BackColor = System.Drawing.Color.PowderBlue;
            CancelRegisterMasterPwdBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            CancelRegisterMasterPwdBtn.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            CancelRegisterMasterPwdBtn.Location = new System.Drawing.Point(236, 122);
            CancelRegisterMasterPwdBtn.Name = "CancelRegisterMasterPwdBtn";
            CancelRegisterMasterPwdBtn.Size = new System.Drawing.Size(158, 33);
            CancelRegisterMasterPwdBtn.TabIndex = 4;
            CancelRegisterMasterPwdBtn.Text = "Cancel";
            CancelRegisterMasterPwdBtn.UseVisualStyleBackColor = false;
            CancelRegisterMasterPwdBtn.Click += CancelRegisterMasterPwdBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(21, 20);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(155, 18);
            label1.TabIndex = 11;
            label1.Text = "Register Lock Password:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(23, 71);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(153, 18);
            label2.TabIndex = 13;
            label2.Text = "Confirm Lock Password:";
            // 
            // RegisterMasterPwdConfirmTxtBox
            // 
            RegisterMasterPwdConfirmTxtBox.BackColor = System.Drawing.Color.FromArgb(41, 42, 45);
            RegisterMasterPwdConfirmTxtBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            RegisterMasterPwdConfirmTxtBox.Location = new System.Drawing.Point(199, 71);
            RegisterMasterPwdConfirmTxtBox.Name = "RegisterMasterPwdConfirmTxtBox";
            RegisterMasterPwdConfirmTxtBox.Size = new System.Drawing.Size(197, 26);
            RegisterMasterPwdConfirmTxtBox.TabIndex = 2;
            RegisterMasterPwdConfirmTxtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // RegisterMasterPasswordForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackColor = System.Drawing.Color.FromArgb(32, 33, 36);
            ClientSize = new System.Drawing.Size(419, 169);
            Controls.Add(label2);
            Controls.Add(RegisterMasterPwdConfirmTxtBox);
            Controls.Add(label1);
            Controls.Add(CancelRegisterMasterPwdBtn);
            Controls.Add(SubmitMasterPwdBtn);
            Controls.Add(RegisterMasterPwdTxtBox);
            Font = new System.Drawing.Font("Comic Sans MS", 9.75F);
            ForeColor = System.Drawing.SystemColors.ControlLightLight;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RegisterMasterPasswordForm";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Cypher Shield";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button SubmitMasterPwdBtn;
        private System.Windows.Forms.Button CancelRegisterMasterPwdBtn;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox RegisterMasterPwdTxtBox;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox RegisterMasterPwdConfirmTxtBox;
    }
}