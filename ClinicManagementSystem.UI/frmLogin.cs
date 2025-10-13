using ClinicManagementSystem.Logic;
using ClinicManagementSystem.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicManagmentSystem.UI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            lblUncoreectUP.Visible = false;
            FillRememberedUsernameAndPassword();
        }
        private void FillRememberedUsernameAndPassword ()
        {
            string Username = "" , Password  = "";
            bool FoundUser = clsHelper.GetRememberedUsernameAndPassword (ref Username, ref Password);

            if (FoundUser)
            {
                txtUsername.Text = Username;
                txtPassword.Text = Password;
                cbRemember.Checked = true;
            }
            else
            {
                _ResetInfoToEmpty();
            }
        }
        private void _ResetInfoToEmpty ()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            lblUncoreectUP.Visible = false;
            cbRemember.Checked = false;
            txtUsername.Focus();

        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool ValidatingForm ()
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show(
                    "Username is requierd",
                    "Required fields",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show(
                    "Password is requierd",
                    "Required fields",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }

            return true;
        }      
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!ValidatingForm())
            {
                return;
            }

            string Username = txtUsername.Text.Trim();
            string Password = txtPassword.Text.Trim();

            string HashedPassword = clsHelper.ComputeHash(Password);

            bool CorrectUserInfo = clsUser.Login(Username, HashedPassword);

            if (CorrectUserInfo)
            {
                clsUser _User = clsUser.FindUserByUsername(Username);

                if (!_User.IsActive)
                {
                    MessageBox.Show("You account is not active contact your Admin",
                        "Not active account",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                if (cbRemember.Checked)
                {
                    clsHelper.RememberUsernameAndPassword(Username, txtPassword.Text.Trim());
                }
                else
                {
                    _ResetInfoToEmpty();
                    clsHelper.RememberUsernameAndPassword("", "");
                }

                clsHelper.CurrentUser = _User;
                lblUncoreectUP.Visible = false;

                frmMainScreen frm = new frmMainScreen(this);
                frm.Show();
                this.Hide();
            }
            else
            {
                _ResetInfoToEmpty();
                lblUncoreectUP.Visible = true;

            }

        }
    }
}
