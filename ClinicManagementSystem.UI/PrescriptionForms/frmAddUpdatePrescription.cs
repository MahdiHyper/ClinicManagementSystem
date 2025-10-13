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

namespace ClinicManagementSystem.UI.PrescriptionForms
{
    public partial class frmAddUpdatePrescription : Form
    {
        private int _MedicalRecordID;
        private clsMedicalRecord _MedicalRecord;
        private clsPrescription _Prescription;
        private int _PrescriptionID;
        private clsPatient _Patient;
        private DataTable _dt;
        private DataTable _BloodTypes;

        public frmAddUpdatePrescription(int MedicalRecordID)
        {
            InitializeComponent();
            _MedicalRecordID = MedicalRecordID;
        }

        private void frmAddUpdatePrescription_Load(object sender, EventArgs e)
        {
            _MedicalRecord = clsMedicalRecord.FindByAppID(_MedicalRecordID);
            if (_MedicalRecord == null)
            {
                MessageBox.Show("Medical Record not found",
                                        "Can't find Medical Record",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);

                return;
            }

            _Prescription = new clsPrescription();

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
                lblPrescriptionId.Text = "ID: " + _PrescriptionID.ToString();

                _LoadOrRefreshPrescriptionInfo();

            }
            else
            {
                DateTime IssueDate = DateTime.Now;
                string Notes = "";
                int PatientID = _MedicalRecord.PatientID;

                _Prescription.MedicalRecordID = _MedicalRecordID;
                _Prescription.IssueDate = IssueDate;
                _Prescription.Notes = Notes;
                _Prescription.PatientID = PatientID;

                if (_Prescription.Save())
                {
                    _PrescriptionID = _Prescription.PrescriptionID;
                    lblPrescriptionId.Text = "ID: " + _PrescriptionID.ToString();
                }
                else
                {
                    MessageBox.Show("Error while adding new Prescription",
                                        "Can't generate new Prescription",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);

                    return;
                }

                btnDeleteApp.Enabled = false;
                btnPrintPrescription.Enabled = false;
            }
            _FillFormInfo();
        }
        private void _LoadOrRefreshPrescriptionInfo()
        {
            _dt = clsPrescription.GetMedicationsByPrescriptionID( _PrescriptionID );
            dgvPrescriptionList.DataSource = _dt;
            _ApplyAppointmentsListGridStyle();

            if (dgvPrescriptionList.Rows.Count < 1)
            {
                btnDeleteApp.Enabled = false;
                btnPrintPrescription.Enabled = false;
            }
            else
            {
                btnDeleteApp.Enabled = true;
                btnPrintPrescription.Enabled = true;
               
                dgvPrescriptionList.Rows[0].Selected = true;
            }

            lblPrescriptionNumberOfItems.Text = _Prescription.GetNumberOfPrescriptionItems().ToString();
        }

        private void _ApplyAppointmentsListGridStyle()
        {
            dgvPrescriptionList.ColumnHeadersHeight = 40;

            dgvPrescriptionList.ColumnHeadersVisible = true;
            dgvPrescriptionList.EnableHeadersVisualStyles = false;

            dgvPrescriptionList.DefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            dgvPrescriptionList.DefaultCellStyle.ForeColor = Color.FromArgb(22, 40, 73);
            dgvPrescriptionList.DefaultCellStyle.SelectionBackColor = Color.FromArgb(234, 50, 88);
            dgvPrescriptionList.DefaultCellStyle.SelectionForeColor = Color.White;


            dgvPrescriptionList.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            dgvPrescriptionList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 40, 73);
            dgvPrescriptionList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgvPrescriptionList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPrescriptionList.ReadOnly = true;
            dgvPrescriptionList.AllowUserToAddRows = false;
            dgvPrescriptionList.RowHeadersVisible = false;
            dgvPrescriptionList.RowTemplate.Height = 70;


            dgvPrescriptionList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvPrescriptionList.Columns.Contains("PrescriptionItemID")) dgvPrescriptionList.Columns["PrescriptionItemID"].FillWeight = 20;
            if (dgvPrescriptionList.Columns.Contains("MedicationID")) dgvPrescriptionList.Columns["MedicationID"].FillWeight = 20;
            if (dgvPrescriptionList.Columns.Contains("MedicationName")) dgvPrescriptionList.Columns["MedicationName"].FillWeight = 120;
            if (dgvPrescriptionList.Columns.Contains("Dosage")) dgvPrescriptionList.Columns["Dosage"].FillWeight = 40;
            if (dgvPrescriptionList.Columns.Contains("DosageType")) dgvPrescriptionList.Columns["DosageType"].FillWeight = 60;
            if (dgvPrescriptionList.Columns.Contains("DosageNote")) dgvPrescriptionList.Columns["DosageNote"].FillWeight = 100;
            if (dgvPrescriptionList.Columns.Contains("MedicationSerialNumber")) dgvPrescriptionList.Columns["MedicationSerialNumber"].FillWeight = 60;
            if (dgvPrescriptionList.Columns.Contains("Description")) dgvPrescriptionList.Columns["Description"].FillWeight = 100;

            if (dgvPrescriptionList.Columns.Contains("PrescriptionItemID")) dgvPrescriptionList.Columns["PrescriptionItemID"].HeaderText = "Item ID";
            if (dgvPrescriptionList.Columns.Contains("MedicationID")) dgvPrescriptionList.Columns["MedicationID"].HeaderText = "Medication ID";
            if (dgvPrescriptionList.Columns.Contains("MedicationName")) dgvPrescriptionList.Columns["MedicationName"].HeaderText = "Name";
            if (dgvPrescriptionList.Columns.Contains("Dosage")) dgvPrescriptionList.Columns["Dosage"].HeaderText = "Dosage";
            if (dgvPrescriptionList.Columns.Contains("DosageNote")) dgvPrescriptionList.Columns["DosageNote"].HeaderText = "Dosage Note";
            if (dgvPrescriptionList.Columns.Contains("MedicationSerialNumber")) dgvPrescriptionList.Columns["MedicationSerialNumber"].HeaderText = "Serial Number";

        }
        private int _GetSelectedPrescriptionItemID()
        {
            int ID;
            ID = Convert.ToInt32(dgvPrescriptionList.CurrentRow.Cells["PrescriptionItemID"].Value);
            return ID;
        }
        private void _FillFormInfo()
        {
            _FillPatientInfo();
            _FillPrescriptionInfo();
        }
        private void _FillPatientInfo()
        {
            _Patient = clsPatient.GetPatientByID(_MedicalRecord.PatientID);

            if (_Patient == null)
            {
                MessageBox.Show("Error while loading Patient info",
                                       "Error patinet info",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Error);

                return;
            }

            _BloodTypes = clsPatient.GetAllBloodTypes();
            lblPatientFullName.Text = _Patient.PersonInfo.FullName;

            string BloodType = "N/A";
            if (_BloodTypes != null)
            {
                DataRow[] rows = _BloodTypes.Select($"BloodTypeID = {_Patient.BloodTypeID}");
                BloodType = rows[0]["BloodTypeName"].ToString();
            }
            lblBloodeType.Text = BloodType;
            lblPhoneNumber.Text = _Patient.PersonInfo.PhoneNumber;
            lblPatientGender.Text = _Patient.PersonInfo.GenderName;
            lblPatientNotes.Text = _Patient.Notes;

        }
        private void _FillPrescriptionInfo()
        {
            lblPrescriptionId.Text = "ID: " + _Prescription.PrescriptionID.ToString();
            lblPrescriptionNumberOfItems.Text = _Prescription.GetNumberOfPrescriptionItems().ToString();
            lblPrescriptionDate.Text = _Prescription.IssueDate.ToString();
            txtPrescriptionNotes.Text = _Prescription.Notes;
        }
        private void _SavingPrescription()
        {
            _Prescription.Notes = txtPrescriptionNotes.Text;
            if (!_Prescription.Save())
            {
                MessageBox.Show("Error while Saving the Prescription",
                                                        "Error saving Prescription",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Error);

                return;
            }
        }

        private void btnPrintPrescription_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Print Logic will be here sOon !" , "SoOoOon" , MessageBoxButtons.OK);
        }
        private void btnDeleteApp_Click(object sender, EventArgs e)
        {
            int PrescriptionItemId = _GetSelectedPrescriptionItemID();

            if (MessageBox.Show($"Are you sure you want to delete Item ID {PrescriptionItemId}",
                "Confirm Deleting", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (!clsPrescriptionItem.DeletePrescriptionItem(PrescriptionItemId))
                {
                    MessageBox.Show($"Error while delete Item ID {PrescriptionItemId}",
                 "Error Deleting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (_Prescription.Save())
            {
                _LoadOrRefreshPrescriptionInfo();
            }
            else
            {
                MessageBox.Show($"Error while saving Prescription",
                 "Error Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            _SavingPrescription();
            frmAddPrescriptionItem frm = new frmAddPrescriptionItem(_PrescriptionID);
            frm.ShowDialog();
            _LoadOrRefreshPrescriptionInfo();
        }

        private void frmAddUpdatePrescription_FormClosing(object sender, FormClosingEventArgs e)
        {
            _SavingPrescription();
        }
    }
}
