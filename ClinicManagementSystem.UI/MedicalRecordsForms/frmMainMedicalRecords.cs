using ClinicManagementSystem.Business;
using ClinicManagementSystem.Logic;
using ClinicManagementSystem.UI.AppointmentsForms;
using ClinicManagementSystem.UI.PatientsForms;
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

namespace ClinicManagementSystem.UI.MedicalRecordsForms
{
    public partial class frmMainMedicalRecords : Form
    {
        private int _PatientID;
        private clsPatient _Patient = new clsPatient();
        private DataTable _dtMedicalRecords = new DataTable();
        private DataTable _BloodTypes = new DataTable();

        public frmMainMedicalRecords()
        {
            InitializeComponent();
        }
        private void frmMainMedicalRecords_Load(object sender, EventArgs e)
        {
            btnExportPDF.Enabled = false;
            btnShowSummary.Enabled = false;
            btnShowPrescription.Enabled = false;
            _BloodTypes = clsPatient.GetAllBloodTypes();
        }

        private void btnChoosePatient_Click(object sender, EventArgs e)
        {
            frmPatientsList frm = new frmPatientsList(true);
            frm.OnPatientPicked += GetPatientID;
            frm.ShowDialog();
        }
        private void GetPatientID (int PatientID)
        {
            _PatientID = PatientID;
            _Patient = clsPatient.GetPatientByID(_PatientID);

            if ( _Patient == null )
            {
                MessageBox.Show("Patient not found try again.",
                    "Patinet Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            _LoadPatientInfo();

            _LoadRecordsData();
        }
        private void _LoadPatientInfo()
        {
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
            lblPatientGender.Text = _Patient.PersonInfo.GenderName;
            lblPhoneNumber.Text = _Patient.PersonInfo.PhoneNumber;
            lblPatientNotes.Text = _Patient.Notes;
        }
        private void _LoadRecordsData()
        {
            _dtMedicalRecords = clsMedicalRecord.GetAllMedicalRecordsForPatient(_PatientID);

            if (_dtMedicalRecords.Rows.Count < 1)
            {
                MessageBox.Show("There are no Medical Records for this Patient",
                                    "No medical Records", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                return;
            }
            btnExportPDF.Enabled = true;
            btnShowSummary.Enabled = true;
            btnShowPrescription.Enabled = true;

            dgvMedicalRecords.DataSource = _dtMedicalRecords;
            _ApplyMedicalRecordsGridStyle();
            dgvMedicalRecords.Rows[0].Selected = true;

        }
        private void _ApplyMedicalRecordsGridStyle()
        {
            dgvMedicalRecords.ColumnHeadersHeight = 40;

            dgvMedicalRecords.ColumnHeadersVisible = true;
            dgvMedicalRecords.EnableHeadersVisualStyles = false;

            dgvMedicalRecords.DefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            dgvMedicalRecords.DefaultCellStyle.ForeColor = Color.FromArgb(22, 40, 73);
            dgvMedicalRecords.DefaultCellStyle.SelectionBackColor = Color.FromArgb(234, 50, 88);
            dgvMedicalRecords.DefaultCellStyle.SelectionForeColor = Color.White;


            dgvMedicalRecords.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            dgvMedicalRecords.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 40, 73);
            dgvMedicalRecords.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgvMedicalRecords.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMedicalRecords.ReadOnly = true;
            dgvMedicalRecords.AllowUserToAddRows = false;
            dgvMedicalRecords.RowHeadersVisible = false;
            dgvMedicalRecords.RowTemplate.Height = 70;


            dgvMedicalRecords.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvMedicalRecords.Columns.Contains("MedicalRecordID")) dgvMedicalRecords.Columns["MedicalRecordID"].FillWeight = 100;
            if (dgvMedicalRecords.Columns.Contains("Diagnosis")) dgvMedicalRecords.Columns["Diagnosis"].FillWeight = 120;
            if (dgvMedicalRecords.Columns.Contains("DiagnosisDate")) dgvMedicalRecords.Columns["DiagnosisDate"].FillWeight = 150;
            if (dgvMedicalRecords.Columns.Contains("PrescriptionID")) dgvMedicalRecords.Columns["PrescriptionID"].FillWeight = 150;
            if (dgvMedicalRecords.Columns.Contains("Notes")) dgvMedicalRecords.Columns["Notes"].FillWeight = 200;


            if (dgvMedicalRecords.Columns.Contains("MedicalRecordID")) dgvMedicalRecords.Columns["MedicalRecordID"].HeaderText = "ID";
            if (dgvMedicalRecords.Columns.Contains("Diagnosis")) dgvMedicalRecords.Columns["Diagnosis"].HeaderText = "Diagnosis";
            if (dgvMedicalRecords.Columns.Contains("DiagnosisDate")) dgvMedicalRecords.Columns["DiagnosisDate"].HeaderText = "Diagnosis Date";
            if (dgvMedicalRecords.Columns.Contains("PrescriptionID")) dgvMedicalRecords.Columns["PrescriptionID"].HeaderText = "Prescription ID";
            dgvMedicalRecords.Update();

        }

        private void btnShowSummary_Click(object sender, EventArgs e)
        {
            frmMedicalRecordSummary frm = new frmMedicalRecordSummary(_GetSelectedRowID());
            frm.ShowDialog();
            _LoadRecordsData();
        }
        private void btnShowPrescription_Click(object sender, EventArgs e)
        {
            bool IsTherePrescription = clsPrescription.IsLinkedWithMedicalRecord(_GetSelectedRowID());

            if (IsTherePrescription)
            {
                frmAddUpdatePrescription frm = new frmAddUpdatePrescription(_GetSelectedRowID());
                frm.ShowDialog();
                _LoadRecordsData();
            }
            else
            {
                if (MessageBox.Show("There is no prescription for this Medical Record are you sure you want to add one ?",
                    "Adding new Prescription", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    frmAddUpdatePrescription frm = new frmAddUpdatePrescription(_GetSelectedRowID());
                    frm.ShowDialog();
                    _LoadRecordsData();
                }
                else
                {
                    return;
                }
            }
        }
        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            MessageBox.Show("SoooOOOooon adding this feature",
                    "sO0O0On", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        private int _GetSelectedRowID()
        {
            int rowID = 0;
            rowID = (int)dgvMedicalRecords.CurrentRow.Cells["MedicalRecordID"].Value;
            return rowID;
        }

    }
}
