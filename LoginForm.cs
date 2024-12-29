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
        public LoginForm()
        {
            InitializeComponent();
        }

        private void SubmitLoginPwdBtn_Click(object sender, EventArgs e)
        {
            if (LoginMasterPwdTxtBox.Text == SecureStorage.GetPassword())
            {
                MessageBox.Show("Login successful!");
                Close();
            }
            else
            {
                MessageBox.Show("Login failed! Check your password or load the backup file: Master-Password.dat!");
                return;
            }
        }

        private void CancelLoginMasterPwdBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoginPwdLoadBackupBtn_Click(object sender, EventArgs e)
        {
            
            LoginMasterPwdTxtBox.Text = SecureStorage.GetPassword();
        }
    }
}
