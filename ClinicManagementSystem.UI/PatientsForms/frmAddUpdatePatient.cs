using ClinicManagementSystem.Business;
using ClinicManagementSystem.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicManagementSystem.UI.PatientsForms
{
    public partial class frmAddUpdatePatient : Form
    {
        private int _PatientID;
        private clsPatient _Patient;
        private DataTable _dtAllBloodTypes;
        enum enMode { AddNewPatient = 0 , UpdatePatient = 1};
        enMode _Mode = enMode.AddNewPatient;

        public frmAddUpdatePatient()
        {
            InitializeComponent();
            _Mode = enMode.AddNewPatient;
        }
        public frmAddUpdatePatient(int PatientID)
        {
            InitializeComponent();
            _PatientID = PatientID;
            _Mode = enMode.UpdatePatient;
        }
        private void frmAddUpdatePatient_Load(object sender, EventArgs e)
        {
            _LoadAllBloodTypes();

            if (_Mode == enMode.AddNewPatient)
            {
                _ClearForm();

                _PatientID = -1;
                _Patient = new clsPatient();
                _Patient.PersonInfo = new clsPerson();
            }
            else
            {
                _Patient = clsPatient.GetPatientByID(_PatientID);

                if (_Patient == null)
                {
                    MessageBox.Show("Patient Not Found !",
                        "Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    this.Close();
                    return;
                }

                _FillFormWithPatientInfo();
            }
        }

        private void _LoadAllBloodTypes ()
        {
            _dtAllBloodTypes = clsPatient.GetAllBloodTypes();

            cbBloodType.DataSource = _dtAllBloodTypes;
            cbBloodType.ValueMember = "BloodTypeID";
            cbBloodType.DisplayMember = "BloodTypeName";
            cbBloodType.SelectedIndex = -1;
        }
        private void _ClearForm ()
        {
            this.Text = "Add New Patient";
            lblTitle.Text = "Add New Patient";
            lblID.Text = "New ID";

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtLastName.Text = "";

            dtpDateOfBirth.Value = DateTime.Now;
            txtPhone.Text = "";

            txtEmail.Text = "";
            txtNotes.Text = "";

            cbBloodType.SelectedIndex = -1;
            rbMale.Checked = true;

            btnClearOrReset.Text = "Clear";
        }
        private void _FillFormWithPatientInfo()
        {
            this.Text = "Update Patient";
            lblTitle.Text = "Update Patient";
            lblID.Text = "Patient ID : " + _PatientID.ToString();

            txtFirstName.Text = _Patient.PersonInfo.FirstName.ToString();
            
            if (_Patient.PersonInfo.SecondName == null)
            {
                txtSecondName.Text = "";
            }
            else
            {
                txtSecondName.Text = _Patient.PersonInfo.SecondName.ToString();
            }

            txtLastName.Text = _Patient.PersonInfo.LastName.ToString();

            dtpDateOfBirth.Value = _Patient.PersonInfo.DateOfBirth;
            txtPhone.Text = _Patient.PersonInfo.PhoneNumber.ToString();

            txtEmail.Text = _Patient.PersonInfo.Email.ToString();
            
            if (_Patient.Notes == null)
            {
                txtNotes.Text = "";
            }
            else
            {
                txtNotes.Text = _Patient.Notes.ToString();
            }

            if (_Patient.PersonInfo.Gender == 1) rbMale.Checked = true;
            else rbFemale.Checked = true;

            cbBloodType.SelectedValue = Convert.ToInt32(_Patient.BloodTypeID);

            btnClearOrReset.Text = "Reset";

        }
        private void _ConvertDataToObject()
        {
            if (_Patient.PersonInfo == null)
                _Patient.PersonInfo = new clsPerson();

            _Patient.PersonInfo.FirstName = txtFirstName.Text.Trim().ToString();
            
            if (string.IsNullOrEmpty(txtSecondName.Text))
            {
                _Patient.PersonInfo.SecondName = null;

            }
            else
            {
                _Patient.PersonInfo.SecondName = txtSecondName.Text.Trim().ToString();

            }

            _Patient.PersonInfo.LastName = txtLastName.Text.Trim().ToString();

            _Patient.PersonInfo.DateOfBirth = dtpDateOfBirth.Value;
            _Patient.PersonInfo.PhoneNumber = txtPhone.Text.Trim().ToString();

            _Patient.PersonInfo.Email = txtEmail.Text.Trim().ToString();

            if (string.IsNullOrEmpty(txtNotes.Text))
            {
                _Patient.Notes = null;
            }
            else
            {
                _Patient.Notes = txtNotes.Text.Trim().ToString();
            }

            if (cbBloodType.SelectedValue != null && int.TryParse(cbBloodType.SelectedValue.ToString(), out int btId))
                _Patient.BloodTypeID = btId;
            else
                _Patient.BloodTypeID = -1;

            _Patient.PersonInfo.Gender = rbMale.Checked ? 1 : 0;
        }

        private void btnClearOrReset_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.UpdatePatient)
            {
                _FillFormWithPatientInfo();
            }
            else
            {
                _ClearForm();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            _ConvertDataToObject();

            if (_Patient == null) return;

            if (_Patient.Save())
            {
                if (_Mode == enMode.UpdatePatient)
                {
                    MessageBox.Show($"Patient with ID {_PatientID} Updated Succefully",
                        "Updated",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    _Mode = enMode.UpdatePatient;
                    _PatientID = _Patient.PatientID;
                    
                    MessageBox.Show($"Patient added successfully with ID {_PatientID}.",
                        "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    _FillFormWithPatientInfo();
                }
            } 
            else
            {
                if (_Mode == enMode.UpdatePatient)
                {
                    MessageBox.Show($"Error while updated Patient with ID : {_PatientID}",
                        "Update Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Error while Added Patient",
                        "Add Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }

        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(txtFirstName.Text) || string.IsNullOrEmpty(txtLastName.Text)) {
                MessageBox.Show("First name and Last name Are required",
                    "Error First or Last name",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtFirstName.Focus();

                return false;
            }

            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("Phone Number Are required",
                    "Error Phone Number",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtPhone.Focus();

                return false;
            }

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Email Are required",
                    "Error Email",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtEmail.Focus();

                return false;
            }

            if (clsPerson.IsEmailTaken(txtEmail.Text.Trim()) && _Mode == enMode.AddNewPatient)
            {
                MessageBox.Show("Email is already taken, please choose another one.",
                    "Error Email",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                
                txtEmail.Focus();
                
                return false;
            }

            if (!txtEmail.Text.Contains('@') || !txtEmail.Text.Contains('.'))
            {
                MessageBox.Show("Enter a valid Email",
                    "Error Email",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtEmail.Focus();

                return false;
            }

            if (cbBloodType.SelectedIndex == -1)
            {
                MessageBox.Show("Choose a valid Blood Type",
                                  "Error Blood Type",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);

                cbBloodType.Focus();

                return false;
            }

            return true;
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '+')
            {
                e.Handled = true;
            }
        }
    }
}
