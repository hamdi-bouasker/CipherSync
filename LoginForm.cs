using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Microsoft.Toolkit.Uwp.Notifications;

namespace CipherShield
{
    public partial class LoginForm : Form
    {
        public string Password { get; private set; }
        public LoginForm()
        {
            InitializeComponent();
        }

        private void SubmitLoginPwdBtn_Click(object sender, EventArgs e)
        {
            Password = LoginMasterPwdTxtBox.Text;
            string iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "icons8-user-shield-48.png");
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "icons8-security-lock-48.png");
            Uri iconUri = new Uri($"file:///{iconPath}");

            if (Password == SecureStorage.GetPassword())
            {
                // Display a toast notification for successful login
                new ToastContentBuilder()
                    .AddAppLogoOverride(iconUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Cipher Shield")
                    .AddText("Login successful!")
                    .AddText("Welcome back to Cipher Shield!")
                    .Show();

                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                // Display a toast notification for failed login
                new ToastContentBuilder()
                     .AddAppLogoOverride(iconUri, ToastGenericAppLogoCrop.Default)
                     .AddText("Cipher Shield")
                    .AddText("Login failed!")
                    .AddText("Check your password or load the backup file: Master-Password.dat.")
                    .Show();

                return;
            }
        }


        //private void SubmitLoginPwdBtn_Click(object sender, EventArgs e)
        //{
        //    Password = LoginMasterPwdTxtBox.Text;
        //    if (Password == SecureStorage.GetPassword())
        //    {

        //        MessageBox.Show("Login successful!", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        this.DialogResult = DialogResult.OK;
        //        Close();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Login failed! Check your password or load the backup file: Master-Password.dat!", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }
        //}

        private void CancelLoginMasterPwdBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void LoginPwdLoadBackupBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var password = SecureStorage.GetPassword();
                LoginMasterPwdTxtBox.Text = password;
                MessageBox.Show("Backup file loaded successfully!", "Cipher Shield", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch 
            {
                MessageBox.Show("No backup file found!", "Cipher Shield", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
        }
    }
}
