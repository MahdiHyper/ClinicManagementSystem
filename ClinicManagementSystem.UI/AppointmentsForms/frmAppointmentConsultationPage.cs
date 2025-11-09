using ClinicManagementSystem.Business;
using ClinicManagementSystem.Logic;
using ClinicManagementSystem.UI.PrescriptionForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ClinicManagementSystem.Logic.clsAppointment;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ClinicManagementSystem.UI.AppointmentsForms
{
    public partial class frmAppointmentPage : Form
    {
        private int _AppID;
        private clsAppointment _App;
        private int _MedicalRecordID;
        private clsMedicalRecord _MedicalRecord = new clsMedicalRecord();
        private clsPatient _Patient;
        private DataTable _BloodTypes;
        private enum btnMode { Start =  0, Stop = 1 };
        private btnMode _btnMode = btnMode.Start;
        private TimeSpan _sessionElapsed = TimeSpan.Zero;

        public frmAppointmentPage(int AppID)
        {
            InitializeComponent();
            _AppID = AppID;
        }

        private void frmAppointmentPage_Load(object sender, EventArgs e)
        {
            lblAppID.Text = "ID: " + _AppID.ToString();

            _App = clsAppointment.GetAppointmentByID(_AppID);

            if (_App == null)
            {
                MessageBox.Show("Not found the appointment info try later",
                    "Appointment not found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.Close();
                return;
            }

            _AppID = _App.AppointmentID;

            if (clsMedicalRecord.IsApplinkedWithMedicalRecored(_AppID))
            {
                _MedicalRecord = clsMedicalRecord.FindByAppID(_AppID);
                _MedicalRecordID = _MedicalRecord.MedicalRecordID;

                frmMedicalRecordSummary frm = new frmMedicalRecordSummary(_MedicalRecordID);
                frm.ShowDialog();

                this.Close();
            }

            _LoadPatientInfo();
            _LoadAppointmentInfo();

            MedicalRecoredPanel.Enabled = false;
            PrescriptionPanel.Enabled = false;
            PrescriptionInfoPanel.Visible = false;
            lblTimer.Visible = false;
            lblTimerLabel.Visible = false;
            lblIsTherePrescription.Visible = true;
        }
        private void _LoadPatientInfo()
        {
            _BloodTypes = clsPatient.GetAllBloodTypes();
            _Patient = clsPatient.GetPatientByID(_App.PatientID);

            if (_Patient == null)
            {
                MessageBox.Show("Not found the Patient info try later",
                                    "Patient not found",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                return;
            }

            lblPatientFullName.Text = _Patient.PersonInfo.FullName;

            string bloodTypeName = "N/A";
            if (_BloodTypes != null)
            {
                DataRow[] rows = _BloodTypes.Select($"BloodTypeID = {_Patient.BloodTypeID}");
                if (rows.Length > 0)
                {
                    bloodTypeName = rows[0]["BloodTypeName"].ToString();
                }
            }
            lblBloodeType.Text = bloodTypeName;
            lblPhoneNumber.Text = _Patient.PersonInfo?.PhoneNumber;
            lblPatientGender.Text = _Patient.PersonInfo?.GenderName;
            lblPatientNotes.Text = _Patient?.Notes;
        }
        private void _LoadAppointmentInfo()
        {
            lblAppDate.Text = _App.AppointmentDateTime.ToString();
            string statusName = Enum.GetName(typeof(enAppointmentStatus), _App.Status);
            lblAppStatus.Text = statusName;
            lblAppNotes.Text = _App.Notes;
        }
        private void _CheckPrescription()
        {
            if (clsPrescription.IsLinkedWithMedicalRecord(_MedicalRecordID))
            {
                clsPrescription prescription = clsPrescription.FindByMedicalRecordID(_MedicalRecordID);

                if (prescription == null)
                {
                    MessageBox.Show("Not found the Prescription info try later",
                                    "Prescription not found",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                    lblIsTherePrescription.Visible = true;
                    PrescriptionInfoPanel.Visible = false;
                    btnAddorUpdatePrescription.Text = "Add Prescription";

                    return;
                }

                lblIsTherePrescription.Visible = false;
                PrescriptionInfoPanel.Visible = true;
                btnAddorUpdatePrescription.Text = "Edit Prescription";


                lblPrescriptionNumberOfItems.Text = Convert.ToString(prescription.GetNumberOfPrescriptionItems());
                lblPrescriptionID.Text = prescription.PrescriptionID.ToString();
                lblPrescriptionDate.Text = prescription.IssueDate.ToString();
                lblPrescriptionNotes.Text = prescription.Notes;
            }
        }

        private bool ValidateSessionStart()
        {
            if (_App.AppointmentDateTime.Date != DateTime.Today)
            {
                MessageBox.Show("Sorry you can't start Session except on the same date as the appointment",
                    "Not valid date",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            //Another layer of security
            if (clsMedicalRecord.IsApplinkedWithMedicalRecored(_AppID))
            {
                MessageBox.Show("You can't start this Consolution again",
                    "Can't start session again",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }

            if (_App.Status == 3)
            {
                MessageBox.Show("You can't start the Session with canceld Appointment",
                    "Can't start session",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }

            return true;
        }
        private bool ValidateSessionStop()
        {
            if (string.IsNullOrEmpty(txtDiagnosis.Text))
            {
                MessageBox.Show("Please Type any diagnosis info for this session",
                    "Diagnosis is required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                
                return false;
            }

            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (_btnMode == btnMode.Stop) 
            {
                MessageBox.Show("Please end this session first before close this form",
                    "end session",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            this.Close();
        }
        private void btnStartOrStop_Click(object sender, EventArgs e)
        {

            if (_btnMode == btnMode.Start)
            {
                if (!ValidateSessionStart()) { return; }

                if (MessageBox.Show("Start consultation now?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                string Diagnosis = "N/A";
                DateTime DiagnosisDate = DateTime.Now;
                string MedicalRecordNotes = "";

                _MedicalRecord.Notes = MedicalRecordNotes;
                _MedicalRecord.Diagnosis = Diagnosis;
                _MedicalRecord.DiagnosisDate = DiagnosisDate;
                _MedicalRecord.AppointmentID = _AppID;
                _MedicalRecord.PatientID = _App.PatientID;

                if (_MedicalRecord.Save())
                {
                    _MedicalRecordID = _MedicalRecord.MedicalRecordID;
                    _MedicalRecord = clsMedicalRecord.FindByID(_MedicalRecordID);
                }
                else
                {
                    MessageBox.Show("There was a problem while adding a new Medical Record",
                    "Error Adding new M.R",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    return;
                }

                MedicalRecoredPanel.Enabled = true;
                PrescriptionPanel.Enabled = true;
                lblTimer.Visible = true;
                lblTimerLabel.Visible = true;

                timer1.Start();

                _btnMode = _btnMode == btnMode.Start ? btnMode.Stop : btnMode.Start;
                btnStartOrStop.Text = _btnMode == btnMode.Start ? "Start Session" : "End Session";
                btnStartOrStop.FillColor2 = _btnMode == btnMode.Start ? Color.FromArgb(88, 180, 170) : Color.Red;

            }
            else 
            {
                if (!ValidateSessionStop()) { return; }

                _MedicalRecord.Diagnosis = txtDiagnosis.Text.ToString();
                _MedicalRecord.Notes = txtDiagnosisNotes.Text.ToString();
                _MedicalRecord.DiagnosisDate = DateTime.Now;

                if (_MedicalRecord.Save())
                {
                    _MedicalRecordID = _MedicalRecord.MedicalRecordID;

                    MessageBox.Show("Session saved successfully ✅", 
                        "Done", MessageBoxButtons.OK, 
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("There was error while saving this session",
                    "Error updating this M.R",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    return;
                }

                _App.Status = 2;
                if (!_App.Save())
                {
                    MessageBox.Show("There was error while saving this session",
                    "Error updating this Appointment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }

                timer1.Stop();

                lblTimer.Visible = false;
                lblTimerLabel.Visible = false;
                btnStartOrStop.Text = _btnMode == btnMode.Start ? "End Session" : "Start Session";
                btnStartOrStop.FillColor2 = Color.FromArgb(88, 180, 170);

                frmMedicalRecordSummary frm = new frmMedicalRecordSummary(_MedicalRecordID);
                frm.ShowDialog();

                this.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _sessionElapsed = _sessionElapsed.Add(TimeSpan.FromSeconds(1));
            lblTimer.Text = _sessionElapsed.ToString(@"mm\:ss");
        }

        private void btnAddorUpdatePrescription_Click(object sender, EventArgs e)
        {
            frmAddUpdatePrescription frm = new frmAddUpdatePrescription(_MedicalRecordID);
            frm.ShowDialog();

            _CheckPrescription();
        }
    }
}
