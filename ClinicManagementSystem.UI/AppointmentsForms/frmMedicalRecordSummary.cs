using ClinicManagementSystem.Logic;
using ClinicManagementSystem.UI.PrescriptionForms;
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
    public partial class frmMedicalRecordSummary : Form
    {
        private int _MedicalRecordID;
        private clsMedicalRecord _MedicalRecord;
        private clsPrescription _Prescription;
        private int _PrescriptionID;

        public frmMedicalRecordSummary(int MedicalRecord)
        {
            InitializeComponent();
            _MedicalRecordID = MedicalRecord;
        }

        private void clsMedicalRecordSummary_Load(object sender, EventArgs e)
        {
            _MedicalRecord = clsMedicalRecord.FindByID(_MedicalRecordID);

            if (_MedicalRecord == null)
            {
                MessageBox.Show("Not found the Medical Record info try later",
                    "Medical Record not found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.Close();
                return;
            }

            _FillMedicalRecordInfo();
            _CheckMedicalRecord();
        }

        private void _FillMedicalRecordInfo()
        {
            lblDiagnosis.Text = _MedicalRecord.Diagnosis.ToString();
            lblDiagnosisDate.Text = _MedicalRecord.DiagnosisDate.ToString();
            lblDiagnosisNotes.Text = _MedicalRecord.Notes.ToString();
        }
        private void _CheckMedicalRecord()
        {
            if (clsPrescription.IsLinkedWithMedicalRecord(_MedicalRecordID))
            {
                _Prescription = clsPrescription.FindByMedicalRecordID(_MedicalRecordID);
                if (_Prescription == null)
                {
                    MessageBox.Show("Not found the Prescription info try later",
                    "Prescription not found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    return;
                }
                _PrescriptionID = _Prescription.PrescriptionID;

                btnAddorUpdatePrescription.Text = "Edit Prescription";
                PrescriptionInfoPanel.Visible = true;
                lblIsTherePrescription.Visible = false;

                lblPriscriptionID.Text = _PrescriptionID.ToString();
                lblPrescriptionDate.Text = _Prescription.IssueDate.ToString();
                lblPrescriptionNumberOfItems.Text = _Prescription.GetNumberOfPrescriptionItems().ToString();
                lblPrescriptionNotes.Text = _Prescription.Notes.ToString();
            }
            else
            {
                btnAddorUpdatePrescription.Text = "Add Prescription";
                PrescriptionInfoPanel.Visible = false;
                lblIsTherePrescription.Visible = true;
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddorUpdatePrescription_Click(object sender, EventArgs e)
        {
            frmAddUpdatePrescription frm = new frmAddUpdatePrescription(_MedicalRecordID);
            frm.ShowDialog();
            _CheckMedicalRecord();
        }

        private void btnSendSummaryToPatient_Click(object sender, EventArgs e)
        {
            //Print Logic;
        }
    }
}
