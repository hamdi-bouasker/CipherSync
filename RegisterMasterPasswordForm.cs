using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Toolkit.Uwp.Notifications;

namespace CipherShield
{
    public partial class RegisterMasterPasswordForm : Form
    {
        public string Password { get; private set; }

        public RegisterMasterPasswordForm()
        {
            InitializeComponent();
        }

        // Submit the master password for registration
        private void SubmitMasterPwdBtn_Click(object sender, EventArgs e)
        {
            if (RegisterMasterPwdTxtBox.Text.Length == 0)
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)

                    .AddText("Please enter a password!")
                    .Show();
                return;
            }
            if (RegisterMasterPwdTxtBox.Text != RegisterMasterPwdConfirmTxtBox.Text)
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)

                    .AddText("Passwords do not match!")
                    .Show();
                return;
            }
            if (RegisterMasterPwdTxtBox.Text.Length < 8)
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)

                    .AddText("Password must be at least 8 characters long!")
                    .Show();
                return;
            }
            if (RegisterMasterPwdTxtBox.Text == RegisterMasterPwdConfirmTxtBox.Text)
            {
                Password = RegisterMasterPwdTxtBox.Text;
                SecureStorage.SavePassword(Password);
                string successIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "success.png");
                Uri successUri = new Uri($"file:///{successIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(successUri, ToastGenericAppLogoCrop.Default)

                    .AddText("Master password registered successfully.")
                    .Show();
                this.DialogResult = DialogResult.OK; // Set DialogResult to OK
                Close();
            }

        }

        // Cancel the registration process
        private void CancelRegisterMasterPwdBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Set DialogResult to Cancel
            Close();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

}
