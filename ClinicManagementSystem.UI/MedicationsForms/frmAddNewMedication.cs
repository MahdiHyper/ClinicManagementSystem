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

namespace ClinicManagementSystem.UI.MedicationsForms
{
    public partial class frmAddNewMedication : Form
    {
        private clsMedication _Med = new clsMedication();
        public frmAddNewMedication()
        {
            InitializeComponent(); 
        }

        private void frmAddNewMedication_Load(object sender, EventArgs e)
        {
            txtMedicationName.Text = "";
            txtMedicationName.Focus();
            txtSerialNumber.Text = "";
            txtDescription.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtMedicationName.Text.Trim() == "")
            {
                MessageBox.Show("Please enter medication name",
                    "Missing Data",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtMedicationName.Focus();
                return;
            }

            _Med.MedicationName = txtMedicationName.Text.Trim();
            
            if (txtSerialNumber.Text.Trim() != "")
                _Med.MedicationSerialNumber = txtSerialNumber.Text.Trim();
            else _Med.MedicationSerialNumber = "N/A";

            if (txtDescription.Text.Trim() != "")
                _Med.Description = txtDescription.Text.Trim();
            else _Med.Description = "";

            if (_Med.AddNewMedication(_Med.MedicationName, _Med.MedicationSerialNumber, _Med.Description))
            {
                MessageBox.Show($"New Medication added successfully",
                    "Medication Added",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);


                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to add new medication",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtMedicationName.Text = "";
                txtMedicationName.Focus();
                txtSerialNumber.Text = "";
                txtDescription.Text = "";
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
