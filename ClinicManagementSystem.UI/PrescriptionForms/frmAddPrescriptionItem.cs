using ClinicManagementSystem.Logic;
using ClinicManagementSystem.UI.MedicationsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicManagementSystem.UI.PrescriptionForms
{
    
    public partial class frmAddPrescriptionItem : Form
    {
        private clsPrescriptionItem _PrescriptionItem;
        private int _PrescriptionID;
        private clsPrescription _Prescription;
        private List<string> _DosageType;
        private int _MedicineID;
        private clsMedication _Medicine;

        public frmAddPrescriptionItem(int PrescriptionID)
        {
            InitializeComponent();
            _PrescriptionID = PrescriptionID;
        }

        private void frmAddPrescriptionItem_Load(object sender, EventArgs e)
        {
            _ClearForm();

            _Prescription = clsPrescription.FindByID(_PrescriptionID);
            if (_Prescription == null )
            {
                MessageBox.Show("Not found the Prescription info try later",
                    "Prescription not found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            _LoadDosageInfo();
        }
        private void _LoadDosageInfo()
        {
            _DosageType = new List<string>()
            {
                "Tablet",
                "Capsule",
                "Syrup",
                "Injection",
                "Drop",
                "Cream",
                "Ointment"
            };
            cbDosageType.DataSource = _DosageType;
            cbDosageType.SelectedIndex = 0;
        }

        private void btnPrintPrescription_Click(object sender, EventArgs e)
        {
            frmMedicationsList frm = new frmMedicationsList(true);
            frm.MedicationPicked += _ChoosimgMedicine;
            frm.ShowDialog();
        }
        private void _ChoosimgMedicine(int MedID)
        {
            _MedicineID = MedID;
            _Medicine = clsMedication.GetMedicationByID(MedID);
            if (_Medicine == null)
            {
                MessageBox.Show("Not found the Medicine info try later",
                                    "Medicine not found",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                return;
            }
            _FillMedicineInfo();
        }
        private void _FillMedicineInfo()
        {
            lblMedicineName.Text = _Medicine.MedicationName.ToString();
            lblMedicineSerialNumber.Text = _Medicine.MedicationSerialNumber.ToString();
            lblMedicineDescription.Text = _Medicine.Description.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            _ClearForm();
        }
        private void _ClearForm()
        {
            lblMedicineName.Text = string.Empty;
            lblMedicineDescription.Text = string.Empty;
            lblMedicineSerialNumber.Text = string.Empty;

            _LoadDosageInfo();

            txtDosage.Text = string.Empty;
            txtDosageNotes.Text = string.Empty;

        }

        private bool _Validation()
        {
            if (string.IsNullOrEmpty(txtDosage.Text))
            {
                MessageBox.Show("Please enter the dosage",
                    "Dosage requird",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            if (_Medicine == null || _MedicineID < 1)
            {
                MessageBox.Show("Please Choose a medicine for the prescription",
                    "medicine requird",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_Validation()) return;

            _PrescriptionItem = new clsPrescriptionItem();
            _PrescriptionItem.PrescriptionID = _PrescriptionID;
            _PrescriptionItem.MedicationID = _MedicineID;
            _PrescriptionItem.Dosage = Convert.ToDouble(txtDosage.Text);
            _PrescriptionItem.DosageType = cbDosageType.SelectedItem.ToString();
            _PrescriptionItem.DosageNote = txtDosageNotes.Text.ToString();

            if (!_PrescriptionItem.AddNewPrescriptionItem())
            {
                MessageBox.Show("Error while adding the new Item",
                    "Error adding",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            this.Close();
        }
        private void txtDosage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
    }
}
