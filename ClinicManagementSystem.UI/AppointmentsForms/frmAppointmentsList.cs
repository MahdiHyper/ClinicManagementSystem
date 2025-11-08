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

namespace ClinicManagementSystem.UI.AppointmentsForms
{
    public partial class frmAppointmentsList : Form
    {
        private DataTable _dt;
        private DateTime _Date1;
        private DateTime _Date2;

        public frmAppointmentsList()
        {
            InitializeComponent();
        }

        private void frmMainAppointmentsPage_Load(object sender, EventArgs e)
        {
            _LoadInfoToFilter();
            _dt = clsAppointment.GetAllAppointments();
            _LoadDataToGridView();
            lblDateFilterInfo.Text = "No Date Filter";
        }
        private void _LoadDataToGridView()
        {
            dgvAppointmentsList.DataSource = _dt;

            if (_dt.Rows.Count > 0)
            {

                btnAddApp.Enabled = true;
                btnDeleteApp.Enabled = true;
                btnEditApp.Enabled = true;
                btnShowAppCard.Enabled = true;

                _ApplyAppointmentsListGridStyle();

                dgvAppointmentsList.Rows[0].Selected = true;

                lblCount.Text = _dt.Rows.Count.ToString();
                dgvAppointmentsList.SelectedRows[0].Selected = true;

                _dt.DefaultView.RowFilter = null;
                cbFilter.SelectedIndex = 0;
                txtFilter.Text = string.Empty;
                txtFilter.Visible = false;
                lblDateFilterInfo.Text = "No Date Filter";

            }
            else
            {
                btnDeleteApp.Enabled = false;
                btnEditApp.Enabled = false;
                btnShowAppCard.Enabled = false;
            }
        }
        private void _ApplyAppointmentsListGridStyle()
        {
            dgvAppointmentsList.ColumnHeadersHeight = 40;

            dgvAppointmentsList.ColumnHeadersVisible = true;
            dgvAppointmentsList.EnableHeadersVisualStyles = false;

            dgvAppointmentsList.DefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            dgvAppointmentsList.DefaultCellStyle.ForeColor = Color.FromArgb(22, 40, 73);
            dgvAppointmentsList.DefaultCellStyle.SelectionBackColor = Color.FromArgb(234, 50, 88);
            dgvAppointmentsList.DefaultCellStyle.SelectionForeColor = Color.White;


            dgvAppointmentsList.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            dgvAppointmentsList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 40, 73);
            dgvAppointmentsList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgvAppointmentsList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAppointmentsList.ReadOnly = true;
            dgvAppointmentsList.AllowUserToAddRows = false;
            dgvAppointmentsList.RowHeadersVisible = false;
            dgvAppointmentsList.RowTemplate.Height = 70;


            dgvAppointmentsList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvAppointmentsList.Columns.Contains("AppointmentID")) dgvAppointmentsList.Columns["AppointmentID"].FillWeight = 100;
            if (dgvAppointmentsList.Columns.Contains("StartAt")) dgvAppointmentsList.Columns["StartAt"].FillWeight = 120;
            if (dgvAppointmentsList.Columns.Contains("DoctorName")) dgvAppointmentsList.Columns["DoctorName"].FillWeight = 150;
            if (dgvAppointmentsList.Columns.Contains("PatientName")) dgvAppointmentsList.Columns["PatientName"].FillWeight = 150;
            if (dgvAppointmentsList.Columns.Contains("StatusText")) dgvAppointmentsList.Columns["StatusText"].FillWeight = 100;
            if (dgvAppointmentsList.Columns.Contains("Notes")) dgvAppointmentsList.Columns["Notes"].FillWeight = 200;

            if (dgvAppointmentsList.Columns.Contains("AppointmentID")) dgvAppointmentsList.Columns["AppointmentID"].HeaderText = "Appointment ID";
            if (dgvAppointmentsList.Columns.Contains("StartAt")) dgvAppointmentsList.Columns["StartAt"].HeaderText = "Start At";
            if (dgvAppointmentsList.Columns.Contains("DoctorName")) dgvAppointmentsList.Columns["DoctorName"].HeaderText = "Doctor Name";
            if (dgvAppointmentsList.Columns.Contains("PatientName")) dgvAppointmentsList.Columns["PatientName"].HeaderText = "Patient Name";
            if (dgvAppointmentsList.Columns.Contains("StatusText")) dgvAppointmentsList.Columns["StatusText"].HeaderText = "Status";

        }
        private void _LoadInfoToFilter()
        {
            cbFilter.Items.Clear();
            cbFilter.Items.Add("None");
            cbFilter.Items.Add("Patient Name");
            cbFilter.Items.Add("Doctor Name");
            cbFilter.Items.Add("Appointment ID");

            if (cbFilter.Items.Count > 0)
            {
                cbFilter.SelectedIndex = 0;
            }
        }
        private void _LoadDataToGridViewCustomDate(DateTime d1, DateTime d2)
        {
            _dt = clsAppointment.GetAppointmentsInRange(d1, d2);
            _LoadDataToGridView();
        }

        private int _GetSelectedRowID()
        {
            if (dgvAppointmentsList.Rows.Count == 0) return 0;

            int rowID = -1;

            var cellID = dgvAppointmentsList.CurrentRow.Cells["AppointmentID"].Value?.ToString();

            if (int.TryParse(cellID, out rowID))
            {
                return rowID;
            }
            return -1;
        }

        private void btnAddApp_Click(object sender, EventArgs e)
        {
            frmAddUpdateAppointment frm = new frmAddUpdateAppointment();
            frm.ShowDialog();
            _LoadDataToGridView();
        }
        private void btnEditApp_Click(object sender, EventArgs e)
        {
            frmAddUpdateAppointment frm = new frmAddUpdateAppointment(_GetSelectedRowID());
            frm.ShowDialog();
            _LoadDataToGridView();
        }
        private void btnDeleteApp_Click(object sender, EventArgs e)
        {
            int AppID = _GetSelectedRowID();
            clsAppointment _App = clsAppointment.GetAppointmentByID(AppID);
            if (MessageBox.Show($"Are you sure deleting appointment with ID {_App.AppointmentID}",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (_App != null)
                {
                    if (_App.Delete())
                    {
                        MessageBox.Show("Appointment Deleted successfully",
                            "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _LoadDataToGridView();
                    }
                    else
                    {
                        MessageBox.Show("ERROR while Deleting Appointment",
                           "ERROR Deleting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Can't Found appointment with this ID",
                          "ERROR Finding", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                return;
            }

        }
        private void btnShowAppCard_Click(object sender, EventArgs e)
        {
                frmAppointmentPage frm = new frmAppointmentPage(_GetSelectedRowID());
                frm.ShowDialog();
                _LoadDataToGridView();
        }


        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            _ResetAllFilters();
        }
        private void _ResetAllFilters()
        {
            _dt = clsAppointment.GetAllAppointments();

            _LoadDataToGridView();

            cbFilter.SelectedIndex = 0;
            txtFilter.Text = string.Empty;
            txtFilter.Visible = false;
            lblDateFilterInfo.Text = "No Date Filter";
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedIndex == 0)
            {
                txtFilter.Text = string.Empty;
                txtFilter.Visible = false;
            }
            else
            {
                txtFilter.Text = string.Empty;
                txtFilter.Visible = true;
            }
        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilter.Text))
            {
                _dt.DefaultView.RowFilter = null;
                lblCount.Text = _dt.Rows.Count.ToString();
                return;
            }

            string filterText = txtFilter.Text.Trim().Replace("'", "''");
            string filterExpression = string.Empty;

            switch (cbFilter.SelectedItem?.ToString())
            {
                case "Patient Name":
                    filterExpression = $"PatientName LIKE '%{filterText}%'";
                    break;
                case "Doctor Name":
                    filterExpression = $"DoctorName LIKE '%{filterText}%'";
                    break;
                case "Appointment ID":
                    if (int.TryParse(filterText, out int appId))
                        filterExpression = $"AppointmentID = {appId}";
                    else
                        filterExpression = "1=0"; // No match if not a valid int
                    break;
                default:
                    filterExpression = string.Empty;
                    break;
            }

            _dt.DefaultView.RowFilter = filterExpression;
            lblCount.Text = dgvAppointmentsList.Rows.Count.ToString();
        }

        private void btnThisDay_Click(object sender, EventArgs e)
        {
            _Date1 = DateTime.Today;
            _Date2 = _Date1.AddHours(23).AddMinutes(59);
            _LoadDataToGridViewCustomDate(_Date1, _Date2);
            lblDateFilterInfo.Text = "Today Appointments";

        }
        private void btnThisWeek_Click(object sender, EventArgs e)
        {
            _Date1 = DateTime.Today;
            _Date2 = DateTime.Today.AddDays(7);
            _LoadDataToGridViewCustomDate(_Date1, _Date2);
            lblDateFilterInfo.Text = $"Appointments from {_Date1.Date.ToShortDateString()} to {_Date2.Date.ToShortDateString()}";

        }
        private void btnThisMonth_Click(object sender, EventArgs e)
        {
            _Date1 = DateTime.Today;
            _Date2 = DateTime.Today.AddDays(30);
            _LoadDataToGridViewCustomDate(_Date1, _Date2);
            lblDateFilterInfo.Text = $"Appointments from {_Date1.Date.ToShortDateString()} to {_Date2.Date.ToShortDateString()}";
        }
        private void btnCutsom_Click(object sender, EventArgs e)
        {
            frmCustomDate frm = new frmCustomDate();
            frm.OnPicktheDate += _ApplyDelegateCustomDateForm;
            frm.ShowDialog();
        }
        private void _ApplyDelegateCustomDateForm(DateTime dtFrom ,  DateTime dtTo)
        {
            _Date1 = dtFrom.Date;
            _Date2 = dtTo.Date;
            _LoadDataToGridViewCustomDate(_Date1, _Date2);
            lblDateFilterInfo.Text = $"Appointments from {_Date1.Date.ToShortDateString()} to {_Date2.Date.ToShortDateString()}";
        }

        private void dgvAppointmentsList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvAppointmentsList.Columns[e.ColumnIndex].Name == "StatusText" && e.Value != null)
            {
                switch (e.Value.ToString())
                {
                    case "Completed":
                        e.CellStyle.ForeColor = Color.Green;
                        break;
                    case "Cancelled":
                        e.CellStyle.ForeColor = Color.Red;
                        break;
                    case "Scheduled":
                        e.CellStyle.ForeColor = Color.Blue;
                        break;
                }
            }
        }
    }
}
