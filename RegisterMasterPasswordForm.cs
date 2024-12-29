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
    public partial class RegisterMasterPasswordForm : Form
    {
        public string Password { get; private set; }

        public RegisterMasterPasswordForm()
        {
            InitializeComponent();
        }

        private void SubmitMasterPwdBtn_Click(object sender, EventArgs e)
        {
            Password = RegisterMasterPwdTxtBox.Text;
            SecureStorage.SavePassword(Password);
            MessageBox.Show("Master password registered successfully!");
            this.DialogResult = DialogResult.OK; // Set DialogResult to OK
            Close();
        }

        private void CancelRegisterMasterPwdBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Set DialogResult to Cancel
            Close();
        }
    }

}
