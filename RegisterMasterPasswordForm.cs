using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CipherShield
{
    public partial class RegisterMasterPasswordForm : Form
    {
        public string Password { get; private set; }

        public RegisterMasterPasswordForm()
        {
            InitializeComponent();
        }

        private void SubmitMasterPwdBtn_Click(object sender, EventArgs e)
        {
            if (RegisterMasterPwdTxtBox.Text.Length == 0)
            {
                MessageBox.Show("Please enter a password!", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (RegisterMasterPwdTxtBox.Text != RegisterMasterPwdConfirmTxtBox.Text)
            {
                MessageBox.Show("Passwords do not match!", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (RegisterMasterPwdTxtBox.Text.Length < 8 )
            {
                MessageBox.Show("Password must be at least 8 characters long!", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (RegisterMasterPwdTxtBox.Text == RegisterMasterPwdConfirmTxtBox.Text)
            {
                Password = RegisterMasterPwdTxtBox.Text;
                SecureStorage.SavePassword(Password);
                MessageBox.Show("Master password registered successfully!", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Set DialogResult to OK
                Close();
            }
            
        }

        private void CancelRegisterMasterPwdBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Set DialogResult to Cancel
            Close();
        }
    }

}
