using ClinicManagementSystem.Logic;
using ClinicManagementSystem.UI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicManagementSystem.UI.UserForms
{
    public partial class frmAddUpdateUser : Form
    {
        private int _UserID;
        private clsUser _User;

        enum Mode { Add = 1,  Update = 2 }
        private Mode _Mode = Mode.Add;

        public frmAddUpdateUser()
        {
            InitializeComponent();
            _UserID = -1;
            _User = new clsUser();
            _User.PersonInfo = new clsPerson();
        }

        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            _Mode = Mode.Update;
        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            if (_Mode == Mode.Update)
            {
                _User = clsUser.FindUserByID(_UserID);

                if (_User == null)
                {
                    MessageBox.Show("User not found !");
                    this.Close();
                    return;
                }

                _FillUserInfoToForm();

            }
            else
            {
                _ResetFormToEmpty();
            }
        }

        private void _ResetFormToEmpty ()
        {
            txtFirstName.Focus();
            this.Text = "Add new User";
            lblTitle.Text = "Add new User";
            lblUserId.Text = "User ID : --";
            txtFirstName.Text = string.Empty;
            txtSecondName.Text = string.Empty;
            txtLastName.Text = string.Empty;

            txtUsername.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhoneNumber.Text = string.Empty;
            rbMale.Checked = true;
            cbIsActive.Checked = true;

            lblSetPassword.Visible = true;
            txtSetPassword.Visible = true;
            txtSetPassword.Text = string.Empty;

            btnClear.Text = "Clear";
        }
        private void _FillUserInfoToForm()
        {
            this.Text = "Update User";
            lblTitle.Text = "Update User";
            lblUserId.Text = "User ID : " + Convert.ToString(_User.UserID);

            txtFirstName.Focus();

            txtFirstName.Text = _User.PersonInfo.FirstName;
            txtSecondName.Text = _User.PersonInfo.SecondName;
            txtLastName.Text = _User.PersonInfo.LastName;

            txtUsername.Text = _User.UserName;
            dtpDateOfBirth.Value = _User.PersonInfo.DateOfBirth;
            txtEmail.Text = _User.PersonInfo.Email;
            txtPhoneNumber.Text = _User.PersonInfo.PhoneNumber;

            if (_User.PersonInfo.Gender == 1)
            {
                rbMale.Checked = true;
            }
            else
            {
                rbFemale.Checked = true;
            }

            cbIsActive.Checked = _User.IsActive;

            lblSetPassword.Visible = false;
            txtSetPassword.Visible = false;

            btnClear.Text = "Reset";
        }
        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("First Name required !", "Error First name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Last Name required !", "Error Last name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLastName.Focus();
                return false;
            }

            if (_Mode == Mode.Add)
            {
                if (clsUser.IsUserNameTaken(txtUsername.Text.Trim()))
                {

                    MessageBox.Show("Username is already used !", "Error UserName", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username is required !", "Error Username", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Focus();
                return false;
            }

            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Enter a valid Email !", "Error Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email is required !", "Error Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (_Mode == Mode.Add && string.IsNullOrWhiteSpace(txtSetPassword.Text))
            {
                MessageBox.Show("Password is required for new users", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSetPassword.Focus();
                return false;
            }

                return true;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (_Mode == Mode.Add)
            {
                _ResetFormToEmpty();
            }
            else 
            {
                _FillUserInfoToForm();
            }
        }
        private void _FillUserObjectWithInfo()
        {
            _User.PersonInfo.FirstName = txtFirstName.Text.Trim();
            if (txtSecondName.Text == string.Empty)
            {
                _User.PersonInfo.SecondName = string.Empty;
            }
            else
            {
                _User.PersonInfo.SecondName = txtSecondName.Text.Trim();
            }

            _User.PersonInfo.LastName = txtLastName.Text.Trim();

            _User.UserName = txtUsername.Text.Trim();
            _User.PersonInfo.DateOfBirth = dtpDateOfBirth.Value;

            _User.PersonInfo.Email = txtEmail.Text.Trim();
            _User.PersonInfo.PhoneNumber = txtPhoneNumber.Text.Trim();

            _User.PersonInfo.Gender = rbMale.Checked ? 1 : 0;
            _User.IsActive = cbIsActive.Checked;

            if (_Mode == Mode.Add)
            {
                _User.Password = txtSetPassword.Text.Trim();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            if (_User == null) _User = new clsUser();
            if (_User.PersonInfo == null) _User.PersonInfo = new clsPerson();

            _FillUserObjectWithInfo();

            if (_User.Save())
            {
                if (_Mode == Mode.Add)
                {
                    MessageBox.Show("User Add successfully with UserID : " + _User.UserID, "Added successfully",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _Mode = Mode.Update;
                    _UserID = _User.UserID;
                    _User = clsUser.FindUserByID(_UserID);
                    if (_User != null) _FillUserInfoToForm();

                }
                else
                {
                    MessageBox.Show("User Updated successfully with UserID : " + _User.UserID, "Updated successfully",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _FillUserInfoToForm();
                }
            }
            else
            {
                MessageBox.Show(_Mode == Mode.Add ? "Error while adding new user" : "Error while updating user",
                  "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFemale.Checked)
            {
                pbUser.Image = Resources.woman;
            }
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMale.Checked)
            {
                pbUser.Image = Resources.man;

            }
        }
    }
}
