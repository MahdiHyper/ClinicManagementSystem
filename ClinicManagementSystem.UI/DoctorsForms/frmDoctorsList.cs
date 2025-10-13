using ClinicManagementSystem.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ClinicManagementSystem.UI.DoctorsForms
{
    public partial class frmDoctorsList : Form
    {
        public delegate void DoctorSelectedHandler(int DoctorID);
        public event DoctorSelectedHandler DoctorSelected;

        private bool _IsPickMode = false;
        DataTable _AllDoctors = null;

        public frmDoctorsList(bool PickMode = false)
        {
            InitializeComponent();
            _IsPickMode = PickMode;
        }

        private void frmDoctorsList_Load(object sender, EventArgs e)
        {
            _LoadDoctorsListData();

            btnPickDocotr.Visible = (_IsPickMode ?  true : false);
        }
        
        private void _ApplyDoctorsListGridStyle()
        {
            dgvDoctorsList.ColumnHeadersHeight = 40;

            dgvDoctorsList.ColumnHeadersVisible = true;
            dgvDoctorsList.EnableHeadersVisualStyles = false;

            dgvDoctorsList.DefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Regular);
            dgvDoctorsList.DefaultCellStyle.ForeColor = Color.FromArgb(22, 40, 73);
            dgvDoctorsList.DefaultCellStyle.SelectionBackColor = Color.FromArgb(234, 50, 88);
            dgvDoctorsList.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvDoctorsList.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            dgvDoctorsList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 40, 73);
            dgvDoctorsList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgvDoctorsList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDoctorsList.ReadOnly = true;
            dgvDoctorsList.AllowUserToAddRows = false;
            dgvDoctorsList.RowHeadersVisible = false;
            dgvDoctorsList.RowTemplate.Height = 60;


            dgvDoctorsList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvDoctorsList.Columns.Contains("DoctorID")) dgvDoctorsList.Columns["DoctorID"].FillWeight = 70;
            if (dgvDoctorsList.Columns.Contains("FirstName")) dgvDoctorsList.Columns["FirstName"].FillWeight = 110;
            if (dgvDoctorsList.Columns.Contains("LastName")) dgvDoctorsList.Columns["LastName"].FillWeight = 110;
            if (dgvDoctorsList.Columns.Contains("Specialization")) dgvDoctorsList.Columns["Specialization"].FillWeight = 120;
            if (dgvDoctorsList.Columns.Contains("ConsultationFee")) dgvDoctorsList.Columns["ConsultationFee"].FillWeight = 100;
            if (dgvDoctorsList.Columns.Contains("DateOfBirth")) dgvDoctorsList.Columns["DateOfBirth"].FillWeight = 110;
            if (dgvDoctorsList.Columns.Contains("Gender")) dgvDoctorsList.Columns["Gender"].FillWeight = 80;
            if (dgvDoctorsList.Columns.Contains("Email")) dgvDoctorsList.Columns["Email"].FillWeight = 160;

        }
        private void _LoadDoctorsListData ()
        {

            _AllDoctors = clsDoctor.GetAllDoctors();
            
            dgvDoctorsList.DataSource = _AllDoctors;

            if (_AllDoctors?.Rows.Count > 0)
            {
                btnEditDoctor.Enabled = true;
                btnDeleteDoctor.Enabled = true;

                lblCount.Text = _AllDoctors?.Rows.Count.ToString();

                dgvDoctorsList.ReadOnly = true;
                dgvDoctorsList.MultiSelect = false;

                dgvDoctorsList.CurrentCell = dgvDoctorsList.Rows[0].Cells[0];

                _ApplyDoctorsListGridStyle();
            }
            else
            {
                btnEditDoctor.Enabled = false;
                btnDeleteDoctor.Enabled = false;
                MessageBox.Show("There is no Doctors in System", "No Doctors", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _AllFiltersReset();

        }
        
        private void _AllFiltersReset()
        {
            txtFilter.Visible = false;
            _LoadAllSpecialization();
            _LoadAllGender();
            _LoadFilterInfo();
        }
        private void _LoadAllSpecialization()
        {
            cbSpecialization.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSpecialization.Items.Clear();

            cbSpecialization.Items.AddRange(new object[]
            {
                 "None",
                 "General Medicine",
                 "Pediatrics",
                 "Gynecology",
                 "Dermatology",
                 "Dentistry"
            });
            cbSpecialization.SelectedIndex = 0;
        }
        private void _LoadAllGender()
        {
            cbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cbGender.Items.Clear();

            cbGender.Items.AddRange(new object[]
            {
                 "None",
                 "Male",
                 "Female"
            });

            cbGender.SelectedIndex = 0;
        }
        private void _LoadFilterInfo()
        {
            cbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFilter.Items.Clear();

            cbFilter.Items.AddRange(new object[]
            {
                 "None", // 0
                 "Full Name", // 1
                 "Specialization", // 2
                 "Gender", // 3
                 "Doctor ID", // 4
            });

            cbFilter.SelectedIndex = 0;
        }

        private void btnAddDoctor_Click(object sender, EventArgs e)
        {
            frmDoctorAddUpdate frm = new frmDoctorAddUpdate();
            frm.ShowDialog();

            frmDoctorsList_Load(this, e);
        }
        private void btnEditDoctor_Click(object sender, EventArgs e)
        {
            int _DoctorID = Convert.ToInt32(dgvDoctorsList.CurrentRow.Cells[0].Value);
            using (var frm = new frmDoctorAddUpdate(_DoctorID))
            {
                frm.ShowDialog();
            }

            _LoadDoctorsListData();
        }
        private void _EditDoctor()
        {
            int _DoctorID = Convert.ToInt32(dgvDoctorsList.CurrentRow.Cells[0].Value);
            frmDoctorAddUpdate frm = new frmDoctorAddUpdate(_DoctorID);
            frm.ShowDialog();

            _LoadDoctorsListData();
        }

        private void btnDeleteDoctor_Click(object sender, EventArgs e)
        {
            int _DocID = Convert.ToInt32(dgvDoctorsList.CurrentRow.Cells[0].Value);

            if (MessageBox.Show($"Are you sure you want to delete Doctor With ID {_DocID}", "Confirm Deleting",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (clsDoctor.DeleteByID(_DocID))
                {
                    MessageBox.Show($"Doctor with ID : {_DocID} Deleted successfully",
                        "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmDoctorsList_Load(this, e);
                    return;
                }
                else
                {
                    MessageBox.Show($"Doctor with ID : {_DocID} Cant Deleted",
                                        "Error Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*   None  0
                 Name  1
                 Specialization  2
                 Gender  3
                 Doctor ID  4
            */

            txtFilter.Visible = false;
            cbGender.Visible = false;
            cbSpecialization.Visible = false;

            
            if (cbFilter.SelectedIndex == 0)
            {
                _AllDoctors.DefaultView.RowFilter = "";
                lblCount.Text = _AllDoctors.DefaultView.Count.ToString();
                return;
            }

            
            if (cbFilter.SelectedIndex == 2) { cbSpecialization.Visible = true; return; }

            
            if (cbFilter.SelectedIndex == 3) { cbGender.Visible = true; return; }

            txtFilter.Visible = true;
        }
        private void cbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbGender.SelectedIndex <= 0) { _AllDoctors.DefaultView.RowFilter = ""; }
            else { _AllDoctors.DefaultView.RowFilter = $"Gender = '{cbGender.SelectedItem}'"; }

            lblCount.Text = _AllDoctors.DefaultView.Count.ToString();
        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilter.Text))
            {
                _AllDoctors.DefaultView.RowFilter = "";
                lblCount.Text = _AllDoctors.DefaultView.Count.ToString();
                return;
            }

            string filterValue = txtFilter.Text.Trim();

            switch (cbFilter.SelectedIndex)
            {
                case 1: // Name
                      
                    _AllDoctors.DefaultView.RowFilter =
                        $"FirstName LIKE '%{filterValue.Replace("'", "''")}%' " +
                        $"OR LastName LIKE '%{filterValue.Replace("'", "''")}%'";
                    break;

                case 4: // Doctor ID 
                    if (int.TryParse(filterValue, out int id))
                        _AllDoctors.DefaultView.RowFilter = $"DoctorID = {id}";
                    else
                        _AllDoctors.DefaultView.RowFilter = "1=0";
                    break;

                default:
                   
                    _AllDoctors.DefaultView.RowFilter = "";
                    break;
            }

          
            lblCount.Text = _AllDoctors.DefaultView.Count.ToString();
        }
        private void cbSpecialization_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSpecialization.SelectedIndex <= 0) { _AllDoctors.DefaultView.RowFilter = ""; }
            else { _AllDoctors.DefaultView.RowFilter = $"Specialization = '{cbSpecialization.SelectedItem.ToString().Replace("'", "''")}'"; }

            lblCount.Text = _AllDoctors.DefaultView.Count.ToString();
        }

        private void dgvDoctorsList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_IsPickMode)
            {
                PickDoctorAndClose();
            }
            else
            {
                _EditDoctor();
            }
        }

        private void btnPickDocotr_Click(object sender, EventArgs e)
        {
            PickDoctorAndClose();
        }
        private void PickDoctorAndClose()
        {
            if (dgvDoctorsList.CurrentRow == null) return;

            int DoctorID = Convert.ToInt32(dgvDoctorsList.CurrentRow.Cells[0].Value);

            DoctorSelected?.Invoke(DoctorID);

            this.Close();
        }
    }
}
