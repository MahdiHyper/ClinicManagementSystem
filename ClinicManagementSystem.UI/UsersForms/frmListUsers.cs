using ClinicManagementSystem.Logic;
using ClinicManagementSystem.UI.UserForms;
using ClinicManagementSystem.UI.UsersForms;
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
          
            dgvListUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvListUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 11F, FontStyle.Bold);
            dgvListUsers.DefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Regular);
            dgvListUsers.EnableHeadersVisualStyles = false;
            dgvListUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(14, 54, 106);
            dgvListUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvListUsers.DefaultCellStyle.BackColor = Color.White;
            dgvListUsers.DefaultCellStyle.ForeColor = Color.FromArgb(14, 54, 106);
            dgvListUsers.AllowUserToAddRows = false;
            dgvListUsers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;


            dgvListUsers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

         
            dgvListUsers.ColumnHeadersHeight = 35;

        
            dgvListUsers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 150, 200);

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

                dgvListUsers.CurrentCell = dgvListUsers.Rows[0].Cells[0];

                cbFilter.SelectedIndex = 0;
                txtFilter.Text = string.Empty;
                txtFilter.Enabled = false;
            }
            else
            {
                MessageBox.Show("There is no Users here", "No Users", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

        }
        private void btnEditUser_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvListUsers.CurrentRow.Cells[0].Value;
            
            frmAddUpdateUser frm = new frmAddUpdateUser(UserID);
            frm.ShowDialog();

            _RefreshDataList();
        }

        private void btnUserCard_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvListUsers.CurrentRow.Cells[0].Value;
            frmUserCard frm = new frmUserCard(UserID);
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
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedIndex == 1)
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                {
                    e.Handled = true; // منع إدخال الحرف
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
