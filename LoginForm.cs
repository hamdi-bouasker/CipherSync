using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CipherSync
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
            if (Password == SecureStorage.GetPassword())
            {
               
                MessageBox.Show("Login successful!", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Login failed! Check your password or load the backup file: Master-Password.dat!", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

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
                MessageBox.Show("Backup file loaded successfully!", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch 
            {
                MessageBox.Show("No backup file found!", "CipherSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
        }
    }
}
