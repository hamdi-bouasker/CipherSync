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

        // Submit the master password for login
        private void SubmitLoginPwdBtn_Click(object sender, EventArgs e)
        {
            Password = LoginMasterPwdTxtBox.Text;
            string infoIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "info.png");
            Uri infoUri = new Uri($"file:///{infoIcon}");

            if (Password == SecureStorage.GetPassword())
            {
                // Display a toast notification for successful login
                new ToastContentBuilder()
                    .AddAppLogoOverride(infoUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Login successful!")
                    .AddText("Welcome back to Cipher Shield!")
                    .Show();
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                // Display a toast notification for failed login
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                     .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Login failed!")
                    .AddText("Check your password or load the backup file: Master-Password.dat.")
                    .Show();
                return;
            }
        }

        // Cancel the login process
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
            }

            catch
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                     .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("No backup file found!")
                    .Show();
                return;
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
