using ClinicManagementSystem.Business;
using ClinicManagementSystem.Logic;
using ClinicManagementSystem.UI.DoctorsForms;
using ClinicManagementSystem.UI.PatientsForms;
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

namespace ClinicManagementSystem.UI.AppointmentsForms
{
    public partial class frmAddUpdateAppointment : Form
    {
        private clsPatient _Patient;
        private clsDoctor _Doctor;
        private clsAppointment _Appointment;
        private int _AppID;
        private Dictionary<int, string> _specs;
        private enum enMode {AddNew = 0 , Update = 1};
        enMode _Mode = enMode.AddNew;

        public frmAddUpdateAppointment()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddUpdateAppointment(int AppID)
        {
            InitializeComponent();
            _AppID = AppID;
            _Mode = enMode.Update;
        }

        private void frmAddUpdateAppointment_Load(object sender, EventArgs e)
        {

            _specs = clsDoctor.GetAllSpecializations();
            if (_Mode == enMode.AddNew)
            {
                _EmptyTheForm();
                _Patient = new clsPatient();
                _Doctor = new clsDoctor();
                _Appointment = new clsAppointment();
            }
            else
            {
                _Appointment = clsAppointment.GetAppointmentByID(_AppID);

                if (_Appointment == null)
                {
                    MessageBox.Show($"There is no appointment with ID {_AppID} ",
                        "Appointment Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    _Mode = enMode.AddNew;
                    _EmptyTheForm();
                    return;
                }
                
                _Patient = clsPatient.GetPatientByID(_Appointment.PatientID);
                _Doctor = clsDoctor.GetDoctorByID(_Appointment.DoctorID);

                if (_Patient == null)
                {
                        MessageBox.Show($"There is no Patient with this appointment ",
                       "Patient Not Found",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Warning);
                        _Mode = enMode.AddNew;
                        _EmptyTheForm();
                        return;
                }
                if (_Doctor == null)
                {
                        MessageBox.Show($"There is no Doctor with this appointment",
                       "Doctor Not Found",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Warning);
                        _Mode = enMode.AddNew;
                        _EmptyTheForm();
                        return;
                }

                _FillTheForm();
            }
        }
        private void _EmptyTheForm()
        {
            _EmptyDoctorInfo();
            _EmptyPatientInfo();
            dtpAppDate.Value = DateTime.Now;
            txtNotes.Text = string.Empty;
            btnClearOrReset.Text = "Clear";
            lblTitle.Text = "New Appointment";
            lblAppID.Text = "N/A";
            this.Text = "Add New Appointment";
            rbScheduled.Checked = true;
        }
        private void _EmptyDoctorInfo()
        {
            lblDoctorFullName.Text = "N/A";
            lblDoctorGender.Text = "N/A";
            lblSpecialization.Text = "N/A";
            lblConsultaionFee.Text = "N/A";
            lblEmail.Text = "N/A";
        }
        private void _EmptyPatientInfo()
        {
            lblPatientFullName.Text = "N/A";
            lblPatientGender.Text = "N/A";
            lblBloodeType.Text = "N/A";
            lblPhoneNumber.Text = "N/A";
            lblPatientNotes.Text = "N/A";
        }

        private void _FillTheForm()
        {
            _FillDoctorInfo();
            _FillPatientInfo();

            lblAppID.Text = $"Appointment ID : {_AppID}";

            dtpAppDate.Value = _Appointment.AppointmentDateTime;
            NumHour.Value = _Appointment.AppointmentDateTime.Hour;
            NumMinute.Value = _Appointment.AppointmentDateTime.Minute;

            if (_Appointment.Status == (int)clsAppointment.enAppointmentStatus.Scheduled)
            {
                rbScheduled.Checked = true;
            }
            else if (_Appointment.Status == (int)clsAppointment.enAppointmentStatus.Completed)
            {
                rbCompleted.Checked = true;
            }
            else
            {
                rbCancelled.Checked = true;
            }

            txtNotes.Text = _Appointment.Notes;
            btnClearOrReset.Text = "Reset";
            lblTitle.Text = "Update Appointment";
            this.Text = "Update Appointment";

        }
        private void _FillDoctorInfo()
        {
            lblDoctorFullName.Text = _Doctor.PersonInfo.FullName;
            lblDoctorGender.Text = _Doctor.PersonInfo.GenderName;

            if (_specs.TryGetValue(_Doctor.SpecializationID, out string specialization))
            {
                lblSpecialization.Text = specialization;
            }
            else
            {
                lblSpecialization.Text = "N/A";
            }

            lblConsultaionFee.Text = _Doctor.ConsultationFee.ToString();
            lblEmail.Text = _Doctor.PersonInfo.Email;
        }
        private void _FillPatientInfo()
        {
            lblPatientFullName.Text = _Patient.PersonInfo.FullName;
            lblBloodeType.Text = _Patient.GetBloodType();
            lblPhoneNumber.Text = _Patient.PersonInfo.PhoneNumber;
            lblPatientNotes.Text = _Patient.Notes;
            lblPatientGender.Text = _Patient.PersonInfo.GenderName;

        }
        private DateTime GetFinalDate()
        {
            int hour = (int)NumHour.Value;
            int minute = (int)NumMinute.Value;
            DateTime date = dtpAppDate.Value.Date;
            DateTime finalDate = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
            return finalDate;
        }

        private bool _Validation()
        {
            if (_Patient == null || _Patient.PatientID < 0)
            {
                MessageBox.Show("Please choose a Patient",
                    "Choose patient",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            if (_Doctor == null || _Doctor.DoctorID < 0)
            {
                MessageBox.Show("Please choose a Doctor",
                    "Choose Doctor",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            //if ((NumHour.Value < 7) || (NumHour.Value > 18))
            //{
            //    MessageBox.Show("Please enter an active hours between (7 - 18) !",
            //        "ERROR Hour",
            //        MessageBoxButtons.OK,
            //        MessageBoxIcon.Error);
            //    return false;
            //}

            if ((NumMinute.Value != 0) && (NumMinute.Value != 30))
            {
                MessageBox.Show("The Minutes are only available is ( 0 or 30 )",
                   "ERROR Minute",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return false;
            }

            if (!clsAppointment.IsPatientFree(_Patient.PatientID, GetFinalDate()))
            {
                if (_Mode == enMode.AddNew)
                {
                    MessageBox.Show("There is another Appointment in this Date and time to this Patient",
                        "Another Date for patient",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
            }

            if (!clsAppointment.IsDoctorFree(_Doctor.DoctorID, GetFinalDate()))
            {
                if (_Mode == enMode.AddNew)
                {
                    MessageBox.Show("There is another Appointment in this Date and time to this Doctor",
                        "Another Date for Doctor",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
            }

            if ((GetFinalDate() > DateTime.Now) && rbCompleted.Checked)
            {
                if (MessageBox.Show("Are you sure you want to check complete in future date ?",
                                    "complete warning",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Hand)== DialogResult.Yes)
                {
                    return true;
                }

                return false;
            }

            if (GetFinalDate() < DateTime.Now)
            {
                MessageBox.Show("You can't put the date to Past date",
                                    "Date ERROR",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Hand);
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_Validation()) return;

            _ConvertAPPInfoToObject();

            try
            {
                if (_Appointment.Save())
                {
                    if (_Mode == enMode.AddNew)
                    {
                        MessageBox.Show("Appointment Added Successfully",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        _Mode = enMode.Update;
                        _AppID = _Appointment.AppointmentID;
                        _FillTheForm();
                    }
                    else
                    {
                        MessageBox.Show("Appointment Updated Successfully",
                          "Success",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Information);
                        _FillTheForm();
                    }
                }
                else
                {
                    if (_Mode == enMode.AddNew)
                    {
                        MessageBox.Show("Error While Adding new Appointment. Please check all required fields and try again.",
                            "ERROR Add",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Error While Updating Appointment",
                          "Error Update",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}",
                    "Unexpected Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void _ConvertAPPInfoToObject()
        {
            _Appointment.DoctorID = _Doctor.DoctorID;
            _Appointment.PatientID = _Patient.PatientID;
            _Appointment.AppointmentDateTime = GetFinalDate();

            if (rbScheduled.Checked)
            {
                _Appointment.Status = 1;
            }
            else if (rbCompleted.Checked)
            {
                _Appointment.Status = 2;
            }
            else
            {
                _Appointment.Status = 3;
            }

            string notes = txtNotes.Text.Trim();
            _Appointment.Notes = string.IsNullOrEmpty(notes) ? null : notes;
            
            if (_Mode == enMode.AddNew)
            {
                clsPayment NewPayment = new clsPayment();
                if (!NewPayment.Save())
                {
                    MessageBox.Show("ERROR while add new payment", "ERROR new Payment",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _Appointment.PaymentID = NewPayment.PaymentID;
            }
        }

        private void btnChoosePatient_Click(object sender, EventArgs e)
        {
            frmPatientsList frm  = new frmPatientsList(true);
            frm.OnPatientPicked += _FillPatientInfoFromList;
            frm.ShowDialog();
        }
        private void btnChooseDoctor_Click(object sender, EventArgs e)
        {
            frmDoctorsList frm = new frmDoctorsList(true);
            frm.DoctorSelected += _FillDoctorInfoFromList;
            frm.ShowDialog();
        }
        private void _FillDoctorInfoFromList(int DoctorID)
        {
            _Doctor = clsDoctor.GetDoctorByID(DoctorID);
            if (_Doctor == null)
            {
                MessageBox.Show("Error while getting Doctor info try again",
                    "Error upload doctor info",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            _FillDoctorInfo();
        }
        private void _FillPatientInfoFromList(int PatientID)
        {
            _Patient = clsPatient.GetPatientByID(PatientID);
            if (_Patient == null)
            {
                MessageBox.Show("Error while getting Patient info try again",
                    "Error upload Patient info",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            _FillPatientInfo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnClearOrReset_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNew)
            {
                _EmptyTheForm();
            }
            else
            {
                _FillTheForm();
            }
        }

    }
}
