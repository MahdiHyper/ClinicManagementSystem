using ClinicManagementSystem.Business;
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
    public partial class frmPatientsList : Form
    {
        public delegate void SetTextCallback(int PatientID);
        public event SetTextCallback OnPatientPicked;

        private DataTable dtPatients = new DataTable();
        private bool ShowPickButton = false;
        private List<string> FilterValues = new List<string>();

        public frmPatientsList(bool ShowPickButton = false)
        {
            InitializeComponent();
            this.ShowPickButton = ShowPickButton;
        }

        private void frmPatientsList_Load(object sender, EventArgs e)
        {
            _LoadPatientsList();
        }
        private void _LoadPatientsList()
        {
            dtPatients = clsPatient.GetAllPatients();
            PatientList.DataSource = dtPatients;
            
            _ApplyPatientListGridStyle();

            _LoadFilterValues();

            btnPickPatient.Visible = ShowPickButton;
            lblCount.Text = dtPatients.Rows.Count.ToString();
            txtFilter.Enabled = false;
            cbFilter.SelectedIndex = 0;
        }
        private void _LoadFilterValues()
        {
            cbFilter.Items.Clear();
            FilterValues.Clear();

            FilterValues.Add("None");
            FilterValues.Add("First Name");
            FilterValues.Add("Last Name");
            FilterValues.Add("Patient ID");
            FilterValues.Add("Email");
            FilterValues.Add("Phone Number");
            FilterValues.Add("Gender");

            cbFilter.Items.AddRange(FilterValues.ToArray());
        }

        private void _ApplyPatientListGridStyle()
        {
            if (dtPatients.Rows.Count <= 0)
            {
                MessageBox.Show("There is no Patients in list",
                    "No Patients",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            PatientList.ColumnHeadersHeight = 40;

            PatientList.ColumnHeadersVisible = true;
            PatientList.EnableHeadersVisualStyles = false;
     
            PatientList.DefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Regular);
            PatientList.DefaultCellStyle.ForeColor = Color.FromArgb(22, 40, 73);
            PatientList.DefaultCellStyle.SelectionBackColor = Color.FromArgb(234, 50, 88);
            PatientList.DefaultCellStyle.SelectionForeColor = Color.White;

            PatientList.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            PatientList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 40, 73);
            PatientList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
     
            PatientList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            PatientList.ReadOnly = true;
            PatientList.AllowUserToAddRows = false;
            PatientList.RowHeadersVisible = false;
            PatientList.RowTemplate.Height = 60;

            PatientList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (PatientList.Columns.Contains("PatientID")) PatientList.Columns["PatientID"].FillWeight = 40;
            if (PatientList.Columns.Contains("FirstName")) PatientList.Columns["FirstName"].FillWeight = 110;
            if (PatientList.Columns.Contains("LastName")) PatientList.Columns["LastName"].FillWeight = 110;
            if (PatientList.Columns.Contains("BloodTypeName")) PatientList.Columns["BloodTypeName"].FillWeight = 40;
            if (PatientList.Columns.Contains("DateOfBirth")) PatientList.Columns["DateOfBirth"].FillWeight = 150;
            if (PatientList.Columns.Contains("Email")) PatientList.Columns["Email"].FillWeight = 150;
            if (PatientList.Columns.Contains("Gender")) PatientList.Columns["Gender"].FillWeight = 80;
            if (PatientList.Columns.Contains("PhoneNumber")) PatientList.Columns["PhoneNumber"].FillWeight = 120;

            PatientList.Columns["PatientID"].HeaderText = "Patient ID";
            PatientList.Columns["FirstName"].HeaderText = "First Name";
            PatientList.Columns["LastName"].HeaderText = "Last Name";
            PatientList.Columns["BloodTypeName"].HeaderText = "Blood Type";
            PatientList.Columns["DateOfBirth"].HeaderText = "Date of Birth";
            PatientList.Columns["Email"].HeaderText = "Email";
            PatientList.Columns["PhoneNumber"].HeaderText = "Phone Number";



        }
        private int _GetSelectedPatientID()
        {
            if (PatientList.CurrentRow == null) return -1;

            return Convert.ToInt32(PatientList.CurrentRow.Cells[0].Value);
        }

        private void btnAddPatient_Click(object sender, EventArgs e)
        {
            frmAddUpdatePatient frm = new frmAddUpdatePatient();
            frm.ShowDialog();

            _LoadPatientsList();
        }
        private void btnEditPatient_Click(object sender, EventArgs e)
        {
            var PatientID = _GetSelectedPatientID();

            if (int.TryParse(PatientID.ToString(), out int id) && id > 0)
            {
                frmAddUpdatePatient frm = new frmAddUpdatePatient(PatientID);
                frm.ShowDialog();
                _LoadPatientsList();
            }
            else
            {
                MessageBox.Show("Please select a patient to edit.", "No Patient Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void btnDeletePatient_Click(object sender, EventArgs e)
        {
            var PatientID = _GetSelectedPatientID();
            if (int.TryParse(PatientID.ToString(), out int id) && id > 0)
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this patient?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    bool isDeleted = clsPatient.DeletePatient(PatientID);
                    if (isDeleted)
                    {
                        MessageBox.Show("Patient deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _LoadPatientsList();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete patient.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a patient to delete.", "No Patient Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnPickPatient_Click(object sender, EventArgs e)
        {
            var PatientID = _GetSelectedPatientID();

            if (int.TryParse(PatientID.ToString(), out int id) && id > 0)
            {
                OnPatientPicked?.Invoke(PatientID);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a patient to pick.", "No Patient Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedIndex <= 0) return;

            string filterColumn = FilterValues[cbFilter.SelectedIndex].Replace(" ", "");
            string filterText = txtFilter.Text.Replace("'", "''");

            if (string.IsNullOrWhiteSpace(filterText))
            {
                (PatientList.DataSource as DataTable).DefaultView.RowFilter = "";
            }
            else
            {
                if (filterColumn == "PatientID")
                {
                    if (int.TryParse(filterText, out int id))
                    {
                        (PatientList.DataSource as DataTable).DefaultView.RowFilter = $"{filterColumn} = {id}";
                    }
                    else
                    {
                        (PatientList.DataSource as DataTable).DefaultView.RowFilter = "1 = 0";
                    }
                }
                else if (filterColumn == "FirstName")
                {
                    (PatientList.DataSource as DataTable).DefaultView.RowFilter = $"{filterColumn} LIKE '%{filterText}%'";
                }
                else if (filterColumn == "LastName")
                {
                    (PatientList.DataSource as DataTable).DefaultView.RowFilter = $"{filterColumn} LIKE '%{filterText}%'";
                }
                else if (filterColumn == "Email")
                {
                    (PatientList.DataSource as DataTable).DefaultView.RowFilter = $"{filterColumn} LIKE '%{filterText}%'";
                }
                else if (filterColumn == "PhoneNumber")
                {
                    (PatientList.DataSource as DataTable).DefaultView.RowFilter = $"{filterColumn} LIKE '%{filterText}%'";
                }
                else if (filterColumn == "Gender")
                {
                    (PatientList.DataSource as DataTable).DefaultView.RowFilter = $"{filterColumn} LIKE '%{filterText}%'";
                }

            }
        }
        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedIndex == 0)
            {
                txtFilter.Enabled = false;
                txtFilter.Text = "";
                (PatientList.DataSource as DataTable).DefaultView.RowFilter = "";
                lblCount.Text = (PatientList.DataSource as DataTable).Rows.Count.ToString();
            }
            else
            {
                txtFilter.Enabled = true;
                txtFilter.Text = "";
                txtFilter.Focus();
            }
        }

        private void PatientList_DoubleClick(object sender, EventArgs e)
        {
            if (ShowPickButton)
            {
                btnPickPatient.PerformClick();
            }else
            {
                btnEditPatient.PerformClick();
            }
        }
    }
}
