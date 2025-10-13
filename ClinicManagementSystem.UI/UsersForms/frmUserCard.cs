using ClinicManagementSystem.Logic;
using ClinicManagementSystem.UI.UserForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicManagementSystem.UI.UsersForms
{
    public partial class frmUserCard : Form
    {
        private int _UserID;
        private clsUser _User;
        public frmUserCard(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void frmUserCard_Load(object sender, EventArgs e)
        {
            _User = clsUser.FindUserByID(_UserID);

            if (_User == null)
            {
                MessageBox.Show(
                    "User not found !",
                    "Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                this.Close();
            }
            else
            {
                LoadUserInfo();
            }

        }

        private void LoadUserInfo ()
        {
            lblUserID.Text = _User.UserID.ToString();
            lblUsername.Text = _User.UserName.ToString();

            shIsActive.Checked = _User.IsActive;

            ctrlPersonInfo1.LoadPersonInfo(_User.PersonID);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser(_User.UserID);
            frm.ShowDialog();
            LoadUserInfo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
