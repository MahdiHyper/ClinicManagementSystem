using ClinicManagementSystem.Logic;
using ClinicManagementSystem.UI.MedicalRecordsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicManagementSystem.UI.PaymentsForms
{
    public partial class frmMainPayments : Form
    {
        private DataTable _dt = new DataTable();
        
        public frmMainPayments()
        {
            InitializeComponent();
        }

        private void frmMainPayments_Load(object sender, EventArgs e)
        {
            _LoadDate();
        }
        private void _LoadDate()
        {
            _dt = clsPayment.GetAllPayments();
            dgvPaymentsList.DataSource = _dt;

            btnEditPayment.Enabled = false;
            btnShowPaymentCard.Enabled = false;

            if (dgvPaymentsList.Rows.Count < 1)
            {
                MessageBox.Show("There are no payment invoices",
                    "No Invoices", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            _ApplyPaymentListGridStyle();

            dgvPaymentsList.Rows[0].Selected = true;

            btnEditPayment.Enabled = true;
            btnShowPaymentCard.Enabled = true;

            _LoadBoxInfo();
        }
        private void _ApplyPaymentListGridStyle()
        {
            dgvPaymentsList.ColumnHeadersHeight = 40;

            dgvPaymentsList.ColumnHeadersVisible = true;
            dgvPaymentsList.EnableHeadersVisualStyles = false;

            dgvPaymentsList.DefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            dgvPaymentsList.DefaultCellStyle.ForeColor = Color.FromArgb(22, 40, 73);
            dgvPaymentsList.DefaultCellStyle.SelectionBackColor = Color.FromArgb(234, 50, 88);
            dgvPaymentsList.DefaultCellStyle.SelectionForeColor = Color.White;


            dgvPaymentsList.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            dgvPaymentsList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 40, 73);
            dgvPaymentsList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgvPaymentsList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPaymentsList.ReadOnly = true;
            dgvPaymentsList.AllowUserToAddRows = false;
            dgvPaymentsList.RowHeadersVisible = false;
            dgvPaymentsList.RowTemplate.Height = 70;


            dgvPaymentsList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvPaymentsList.Columns.Contains("PaymentID")) dgvPaymentsList.Columns["PaymentID"].FillWeight = 200;
            if (dgvPaymentsList.Columns.Contains("PaymentAmount")) dgvPaymentsList.Columns["PaymentAmount"].FillWeight = 400;
            if (dgvPaymentsList.Columns.Contains("PaymentReceived")) dgvPaymentsList.Columns["PaymentReceived"].FillWeight = 400;
            if (dgvPaymentsList.Columns.Contains("PaymentDate")) dgvPaymentsList.Columns["PaymentDate"].FillWeight = 600;
            if (dgvPaymentsList.Columns.Contains("PatientID")) dgvPaymentsList.Columns["PatientID"].FillWeight = 250;
            if (dgvPaymentsList.Columns.Contains("FullName")) dgvPaymentsList.Columns["FullName"].FillWeight = 800;
            if (dgvPaymentsList.Columns.Contains("Remaining amount")) dgvPaymentsList.Columns["Remaining amount"].FillWeight = 400;


            if (dgvPaymentsList.Columns.Contains("PaymentID")) dgvPaymentsList.Columns["PaymentID"].HeaderText = "ID";
            if (dgvPaymentsList.Columns.Contains("PaymentAmount")) dgvPaymentsList.Columns["PaymentAmount"].HeaderText = "Amount";
            if (dgvPaymentsList.Columns.Contains("PaymentReceived")) dgvPaymentsList.Columns["PaymentReceived"].HeaderText = "Received";
            if (dgvPaymentsList.Columns.Contains("PaymentDate")) dgvPaymentsList.Columns["PaymentDate"].HeaderText = "Date";
            if (dgvPaymentsList.Columns.Contains("PatientID")) dgvPaymentsList.Columns["PatientID"].HeaderText = "Patient ID";
            if (dgvPaymentsList.Columns.Contains("FullName")) dgvPaymentsList.Columns["FullName"].HeaderText = "Full Name";


            dgvPaymentsList.Update();

        }
        private void _LoadBoxInfo()
        {
            lblAmountRecived.Text = clsPayment.GetTotalAmountReceivedFormatted();
            lblTotalInvoices.Text = _dt.Rows.Count.ToString();
        }

        private int _GetSelectedRowID()
        {
            int ID = 0;
            ID = (int) dgvPaymentsList.CurrentRow.Cells["PaymentID"].Value;
            return ID;
        }

        private void btnEditPayment_Click(object sender, EventArgs e)
        {
            frmPaymentEdit frm = new frmPaymentEdit(_GetSelectedRowID());
            frm.ShowDialog();
            _LoadDate();
        }

        private void btnShowPaymentCard_Click(object sender, EventArgs e)
        {
            frmPaymentInfoCard frm = new frmPaymentInfoCard(_GetSelectedRowID());
            frm.ShowDialog();
        }
    }
}
