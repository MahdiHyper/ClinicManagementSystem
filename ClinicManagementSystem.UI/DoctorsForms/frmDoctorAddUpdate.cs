using ClinicManagementSystem.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ClinicManagementSystem.UI.DoctorsForms
{
    public partial class frmDoctorAddUpdate : Form
    {
        enum enMode { AddNew =  0, Update = 1 }
        enMode _Mode = enMode.AddNew;

        private Dictionary<int, string> _specs;
        private clsDoctor _Doctor;
        private int _DoctorID;

        public frmDoctorAddUpdate()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmDoctorAddUpdate(int DoctorID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _DoctorID = DoctorID;
        }

        private void frmDoctorAddUpdate_Load(object sender, EventArgs e)
        {
            _LoadSpecializations();

            if (_Mode == enMode.Update)
            {
                _Doctor = clsDoctor.GetDoctorByID(_DoctorID);

                if (_Doctor == null) return;

                _FillInfo();

            } 
            else
            {
                _Doctor = new clsDoctor();

                if (_Doctor == null)
                {
                    MessageBox.Show("Doctor not found.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                _ClearForm();
            }
        }

        private void _LoadSpecializations()
        {
            _specs = clsDoctor.GetAllSpecializations();
            cbSpecDoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSpecDoc.DataSource = new BindingSource(_specs, null);
            cbSpecDoc.DisplayMember = "Value";  
            cbSpecDoc.ValueMember = "Key"; 
            cbSpecDoc.SelectedIndex = -1;
        }
        private void _ConvertInfoToObject()
        {
            _Doctor.PersonInfo.FirstName = txtFirstName.Text.Trim();
            _Doctor.PersonInfo.SecondName = txtSecondName.Text.Trim();
            _Doctor.PersonInfo.LastName = txtLastName.Text.Trim();

            _Doctor.PersonInfo.DateOfBirth = dtpDateOfBirth.Value;
            _Doctor.PersonInfo.PhoneNumber = txtPhoneNumber.Text.Trim();

            _Doctor.PersonInfo.Email = txtEmail.Text.Trim();
            _Doctor.PersonInfo.Gender = (rbMale.Checked ? 1 : 0);

            _Doctor.SpecializationID = (cbSpecDoc.SelectedValue is int id) ? id : -1;
            
            if (double.TryParse(txtConFee.Text.Trim(), out var fee))
                _Doctor.ConsultationFee = fee;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnClearOrReset_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNew)
            {
                _ClearForm();
            }else
            {
                _FillInfo();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!Validating()) return;

            _ConvertInfoToObject();

            if (_Doctor.Save())
            {
                if (_Mode == enMode.Update)
                {
                    MessageBox.Show("Doctor Updated successfully",
                        "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Doctor Added successfully",
                        "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _Mode = enMode.Update;
                    _DoctorID = _Doctor.DoctorID;
                    _Doctor = clsDoctor.GetDoctorByID(_DoctorID);
                    _FillInfo();
                }
            }
        }

        private void _FillInfo()
        {
            lblTitle.Text = "Update Doctor";
            this.Text = "Update";
            btnClearOrReset.Text = "Reset";

            lblID.Text = "Doctor ID : " +_DoctorID.ToString();

            txtFirstName.Text = _Doctor.PersonInfo.FirstName;
            txtSecondName.Text = _Doctor.PersonInfo.SecondName;
            txtLastName.Text = _Doctor.PersonInfo.LastName;

            dtpDateOfBirth.Value = _Doctor.PersonInfo.DateOfBirth;
            txtPhoneNumber.Text = _Doctor.PersonInfo.PhoneNumber;

            txtEmail.Text = _Doctor.PersonInfo.Email;
            if (_Doctor.PersonInfo.Gender == 1)
            {
                rbMale.Checked = true;
            }
            else
            {
                rbFemale.Checked = true;
            }

            cbSpecDoc.SelectedValue = _Doctor.SpecializationID;

            txtConFee.Text = _Doctor.ConsultationFee.ToString();
        }
        private void _ClearForm()
        {
            lblTitle.Text = "Add New Doctor";
            this.Text = "Add";
            btnClearOrReset.Text = "Clear";

            lblID.Text = "New ID";

            txtFirstName.Clear();
            txtSecondName.Clear();
            txtLastName.Clear();

            dtpDateOfBirth.Value = DateTime.Today;
            txtPhoneNumber.Clear();

            txtEmail.Clear();
            rbMale.Checked = true;

            cbSpecDoc.SelectedIndex = -1;
            
            txtConFee.Clear();
        }

        private new bool Validating()
        {
            if (string.IsNullOrEmpty(txtFirstName.Text) || txtFirstName.Text == "")
            {
                MessageBox.Show("First Name is requierd", "Error First name",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFirstName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtLastName.Text) || txtLastName.Text == "")
            {
                MessageBox.Show("Last Name is requierd", "Error Last name",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLastName.Focus();
                return false;
            }

            if (!txtEmail.Text.Contains('@') || !txtEmail.Text.Contains('.'))
            {
                MessageBox.Show("Email is wrong , try another one", "Error Email",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPhoneNumber.Text) || txtPhoneNumber.Text == "")
            {
                MessageBox.Show("Phone number is requierd", "Error Phone number",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhoneNumber.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtConFee.Text) || txtConFee.Text == "")
            {
                MessageBox.Show("Consultation Fee is requierd", "Error Consultation Fee",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConFee.Focus();
                return false;
            }

            if (!double.TryParse(txtConFee.Text.Trim(), out var fee) || fee < 0)
            {
                MessageBox.Show("Consultation Fee must be a positive number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConFee.Focus();
                return false;
            }

            if (cbSpecDoc.SelectedValue == null)
            {
                MessageBox.Show("Specialization is required", "Error Specialization",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbSpecDoc.Focus();
                return false;
            }

            return true;

        }

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar)) return;
            if (char.IsControl(e.KeyChar)) return;

            if ((e.KeyChar == '+')) return;

            e.Handled = true;
        }
        private void txtConFee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

           
            if (char.IsDigit(e.KeyChar))
                return;

            
            if (e.KeyChar == '.' && !txtConFee.Text.Contains("."))
                return;

            
            e.Handled = true;

        }

        
    }
}
