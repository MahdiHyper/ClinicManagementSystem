using ClinicManagementSystem.Logic;
using ClinicManagementSystem.UI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicManagementSystem.UI.Settings
{
    public partial class frmSettings : Form
    {
        public delegate void UpdatePasswordHandler(bool PasswordUpdated);
        public event UpdatePasswordHandler PasswordUpdated;

        clsUser _CurrentUser;
        public frmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            _LoadForm();
        }

        private void _LoadForm()
        {
            _CurrentUser = clsHelper.CurrentUser;

            if (_CurrentUser == null)
            {
                MessageBox.Show("User info not found. Please try again later.",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            _LoadUserInfo();
            btnSave.Enabled = false;
        }
        private void _LoadUserInfo()
        {
            lblUserID.Text = _CurrentUser.UserID.ToString();
            lblUserFullName.Text = _CurrentUser.PersonInfo.FullName;
            lblUsername.Text = _CurrentUser.UserName.ToString();
            lblEmail.Text = _CurrentUser.PersonInfo.Email.ToString();
            lblPhoneNumber.Text = _CurrentUser.PersonInfo?.PhoneNumber.ToString();
            lblGender.Text = _CurrentUser.PersonInfo.GenderName.ToString();
            cbIsActive.Checked = _CurrentUser.IsActive;

            var gender = _CurrentUser.PersonInfo?.Gender;
            if (gender == 1) Pic.Image = Resources.man;
            else if (gender == 0) Pic.Image = Resources.woman;
            else Pic.Image = null;

        }

        private bool _Validation()
        {
            if (!clsUser.Login(_CurrentUser.UserName , Convert.ToString(clsHelper.ComputeHash(txtCurrentPassword.Text.Trim())))){

                MessageBox.Show("The current password is incorrect.",
                "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            if (Convert.ToString(txtNewPassword.Text.Trim()) != Convert.ToString(txtConfirmPassword.Text.Trim()))
            {
                MessageBox.Show("The new password and confirmation do not match.",
                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (Convert.ToString(txtNewPassword.Text.Trim()) == Convert.ToString(txtCurrentPassword.Text.Trim()))
            {
                MessageBox.Show("The new password must be different from the current one.",
                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (Convert.ToString(txtNewPassword.Text.Trim()).Length < 8)
            {
                MessageBox.Show("The new password must be at least 8 characters long.",
                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_Validation()) { return; }

           if ( !_CurrentUser.UpdatePassword(txtNewPassword.Text.Trim()))
           {
                MessageBox.Show("An error occurred while updating the password. Please try again later.",
                "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
           }

            MessageBox.Show("Password updated successfully.",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            _ClearPasswordsBoxes();

            MessageBox.Show("Please sign in again.",
                "Session Required", MessageBoxButtons.OK, MessageBoxIcon.Information);

            PasswordUpdated?.Invoke(true);

            this.Close();

        }

        private void _ClearPasswordsBoxes()
        {
            txtNewPassword.Clear();
            txtCurrentPassword.Clear();
            txtConfirmPassword.Clear();
        }
        private void btnClearOrReset_Click(object sender, EventArgs e)
        {
            _ClearPasswordsBoxes();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _UpdateSaveStateAndEyeIcon()
        {
            bool anyFilled =
                 !string.IsNullOrEmpty(txtCurrentPassword.Text) ||
                 !string.IsNullOrEmpty(txtNewPassword.Text) ||
                 !string.IsNullOrEmpty(txtConfirmPassword.Text);

            pbPass.Image = anyFilled ? Resources.MonkeyClose : Resources.MonkeyOpen;

            bool basicReady =
                !string.IsNullOrEmpty(txtCurrentPassword.Text) &&
                !string.IsNullOrEmpty(txtNewPassword.Text) &&
                !string.IsNullOrEmpty(txtConfirmPassword.Text);

            btnSave.Enabled = basicReady;
        }

        private void txtCurrentPassword_TextChanged(object sender, EventArgs e)
        {
            _UpdateSaveStateAndEyeIcon();
        }
        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            _UpdateSaveStateAndEyeIcon();
        }
        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            _UpdateSaveStateAndEyeIcon();
        }
    }
}
