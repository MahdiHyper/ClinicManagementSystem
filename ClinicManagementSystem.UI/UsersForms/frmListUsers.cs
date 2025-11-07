using ClinicManagementSystem.Logic;
using ClinicManagementSystem.UI.UserForms;
using iTextSharp.text.pdf.security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ClinicManagementSystem.UI
{
    public partial class frmListUsers : Form
    {
        private DataTable _dtAllUsers;
        public frmListUsers()
        {
            InitializeComponent();
        }
        private void frmListUsers_Load(object sender, EventArgs e)
        {
            _RefreshDataList();
        }
        private void _dgvStyle (DataGridView dvg)
        {
            dgvListUsers.ColumnHeadersHeight = 40;

            dgvListUsers.ColumnHeadersVisible = true;
            dgvListUsers.EnableHeadersVisualStyles = false;

            dgvListUsers.DefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            dgvListUsers.DefaultCellStyle.ForeColor = Color.FromArgb(22, 40, 73);
            dgvListUsers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(234, 50, 88);
            dgvListUsers.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvListUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            dgvListUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 40, 73);
            dgvListUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgvListUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvListUsers.ReadOnly = true;
            dgvListUsers.AllowUserToAddRows = false;
            dgvListUsers.RowHeadersVisible = false;
            dgvListUsers.RowTemplate.Height = 80;

            dgvListUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvListUsers.Columns.Contains("UserID"))
            {
                dgvListUsers.Columns["UserID"].FillWeight = 80;
                dgvListUsers.Columns["UserID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvListUsers.Columns["UserID"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }
            if (dgvListUsers.Columns.Contains("UserName")) dgvListUsers.Columns["UserName"].FillWeight = 150;
            if (dgvListUsers.Columns.Contains("FirstName")) dgvListUsers.Columns["FirstName"].FillWeight = 150;
            if (dgvListUsers.Columns.Contains("LastName")) dgvListUsers.Columns["LastName"].FillWeight = 150;
            if (dgvListUsers.Columns.Contains("PhoneNumber")) dgvListUsers.Columns["PhoneNumber"].FillWeight = 100;
            if (dgvListUsers.Columns.Contains("Gender")) dgvListUsers.Columns["Gender"].FillWeight = 80;
            if (dgvListUsers.Columns.Contains("IsActive")) dgvListUsers.Columns["IsActive"].FillWeight = 80;

            if (dgvListUsers.Columns.Contains("UserID")) dgvListUsers.Columns["UserID"].HeaderText = "User ID";
            if (dgvListUsers.Columns.Contains("UserName")) dgvListUsers.Columns["UserName"].HeaderText = "Username";
            if (dgvListUsers.Columns.Contains("FirstName")) dgvListUsers.Columns["FirstName"].HeaderText = "First Name";
            if (dgvListUsers.Columns.Contains("LastName")) dgvListUsers.Columns["LastName"].HeaderText = "Last Name";
            if (dgvListUsers.Columns.Contains("PhoneNumber")) dgvListUsers.Columns["PhoneNumber"].HeaderText = "Phone Number";
            if (dgvListUsers.Columns.Contains("IsActive")) dgvListUsers.Columns["IsActive"].HeaderText = "Is Active";
        }
        private void _RefreshDataList()
        {
            _dtAllUsers = clsUser.GetAllUsers();

            if (_dtAllUsers.Rows.Count > 0)
            {

                dgvListUsers.DataSource = _dtAllUsers;

                dgvListUsers.ReadOnly = true;
                dgvListUsers.MultiSelect = false;

                _dgvStyle(dgvListUsers);

                lblUsersNumber.Text = _dtAllUsers.Rows.Count.ToString();

                dgvListUsers.Rows[0].Cells[0].Selected = true;

                cbFilter.SelectedIndex = 0;
                txtFilter.Text = string.Empty;
                txtFilter.Enabled = false;

                btnDeleteUser.Enabled = true;
                btnEditUser.Enabled = true;
            }
            else
            {
                MessageBox.Show("There is no Users here", "No Users", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                btnDeleteUser.Enabled = false;
                btnEditUser.Enabled = false;
            }

        }
        private void btnEditUser_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvListUsers.CurrentRow.Cells[0].Value;
            
            frmAddUpdateUser frm = new frmAddUpdateUser(UserID);
            frm.ShowDialog();

            _RefreshDataList();
        }
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            int UserId = (int)dgvListUsers.CurrentRow.Cells[0].Value;

            if (MessageBox.Show("Are you sure you want to Delete User with ID : " + UserId,
                "Delete User", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (clsUser.DeleteUser(UserId))
                {
                    MessageBox.Show("User with ID '" + UserId + "' Deleted successfully.", "Deleted successfully",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshDataList();
                }
                else
                {
                    MessageBox.Show("ERROR while deleting User with ID '" + UserId, "ERROR",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void cbFilter_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            txtFilter.Text = string.Empty;
            txtFilter.Enabled = cbFilter.SelectedIndex != 0;
        }
        private void txtFilter_TextChanged_1(object sender, EventArgs e)
        {
            string txt = txtFilter.Text.Trim();

            if (string.IsNullOrEmpty(txt))
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblUsersNumber.Text = dgvListUsers.RowCount.ToString();

                return;
            }

            if (cbFilter.SelectedIndex == 1) 
            {
                _dtAllUsers.DefaultView.RowFilter = $"UserID = {txt}";
            }
            else
            {
                _dtAllUsers.DefaultView.RowFilter = $"Username LIKE '%{txt}%'";
            }

            lblUsersNumber.Text = dgvListUsers.RowCount.ToString();

            if (dgvListUsers.RowCount > 0)
            {
                btnDeleteUser.Enabled = true;
                btnEditUser.Enabled = true;
            }
            else
            {
                btnDeleteUser.Enabled = false;
                btnEditUser.Enabled = false;
            }
        }
        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedIndex == 1)
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            }
        }
        private void btnAddUser_Click_1(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();
            _RefreshDataList();
        }
    }
}
