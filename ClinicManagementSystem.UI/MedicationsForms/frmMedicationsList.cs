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
    public partial class frmMedicationsList : Form
    {
        public delegate void MedicationCallBack(int MedID);
        public event MedicationCallBack MedicationPicked;

        private DataTable _dtMedications;
        private bool _PickMode = false;
        public frmMedicationsList(bool PickMode = false)
        {
            InitializeComponent();
            _PickMode = PickMode;
        }

        private void frmMedicationsList_Load(object sender, EventArgs e)
        {
            _LoadMedications();
        }
        private void _LoadMedications()
        {
            _dtMedications = clsMedication.GetAllMedications();

            MedicationsList.DataSource = _dtMedications;
            ApplyPatientListGridStyle();

            lblCount.Text = MedicationsList.Rows.Count.ToString();
            btnPickMedication.Visible = _PickMode;

            if (MedicationsList.Rows.Count > 0)
            {
                MedicationsList.Rows[0].Selected = true;
            }
        }
        private void ApplyPatientListGridStyle()
        {
            if (MedicationsList.Rows.Count <= 0)
            {
                MessageBox.Show("There is no Medications in list",
                    "No Medications",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            MedicationsList.ColumnHeadersHeight = 40;

            MedicationsList.ColumnHeadersVisible = true;
            MedicationsList.EnableHeadersVisualStyles = false;
            MedicationsList.DefaultCellStyle.Font = new Font("Century Gothic", 11F, FontStyle.Bold);
            MedicationsList.DefaultCellStyle.ForeColor = Color.FromArgb(22, 40, 73);
            MedicationsList.DefaultCellStyle.SelectionBackColor = Color.FromArgb(234, 50, 88);
            MedicationsList.DefaultCellStyle.SelectionForeColor = Color.White;


            MedicationsList.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Regular);
            MedicationsList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 40, 73);
            MedicationsList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            MedicationsList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            MedicationsList.ReadOnly = true;
            MedicationsList.AllowUserToAddRows = false;
            MedicationsList.RowHeadersVisible = false;
            MedicationsList.RowTemplate.Height = 60;

            MedicationsList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (MedicationsList.Columns.Contains("MedicationID")) MedicationsList.Columns["MedicationID"].FillWeight = 20;
            if (MedicationsList.Columns.Contains("MedicationName")) MedicationsList.Columns["MedicationName"].FillWeight = 110;
            if (MedicationsList.Columns.Contains("MedicationSerialNumber")) MedicationsList.Columns["MedicationSerialNumber"].FillWeight = 40;
            if (MedicationsList.Columns.Contains("Description")) MedicationsList.Columns["Description"].FillWeight = 110;

            MedicationsList.Columns["MedicationID"].HeaderText = "ID";
            MedicationsList.Columns["MedicationName"].HeaderText = "Name";
            MedicationsList.Columns["MedicationSerialNumber"].HeaderText = "Serial Number";
            MedicationsList.Columns["Description"].HeaderText = "Description";

        }

        private int _GetSelectedMedicationID()
        {
            return Convert.ToInt32(MedicationsList.CurrentRow.Cells[0].Value);
        }

        private void btnPickMedication_Click(object sender, EventArgs e)
        {
            if (MedicationsList.Rows.Count <= 0)
            {
                MessageBox.Show("There is no Medications in list",
                    "No Medications",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            int MedID = _GetSelectedMedicationID();

            MedicationPicked?.Invoke(MedID);
            this.Close();
        }
        private void btnAddMedications_Click(object sender, EventArgs e)
        {
            frmAddNewMedication frm = new frmAddNewMedication();
            frm.ShowDialog();
            _LoadMedications();
        }
        private void btnDeleteMedication_Click(object sender, EventArgs e)
        {
            if (MedicationsList.Rows.Count <= 0)
            {
                MessageBox.Show("There is no Medications in list",
                    "No Medications",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            int MedID = _GetSelectedMedicationID();

            clsMedication Med = clsMedication.GetMedicationByID(MedID);

            if (Med == null)
            {
                MessageBox.Show("Selected Medication not found",
                "Medication Not Found",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Are you sure you want to delete Medication: {Med.MedicationName} ?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Med.DeleteMedication(MedID);
                _LoadMedications();
            }
        }

        private void MedicationsList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_PickMode)
            {
                btnPickMedication.PerformClick();
            }
        }
    }
}
