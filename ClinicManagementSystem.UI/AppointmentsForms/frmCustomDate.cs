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
    public partial class frmCustomDate : Form
    {
        public delegate void PicktheDate(DateTime dtFrom, DateTime dtTo);
        public event PicktheDate OnPicktheDate;

        public frmCustomDate()
        {
            InitializeComponent();
        }

        private void frmCustomDate_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Today;
            dtpTo.Value = DateTime.Today.AddDays(7);
            guna2ShadowForm1.SetShadowForm(this);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPicktheDate_Click(object sender, EventArgs e)
        {
            if (dtpFrom.Value >= dtpTo.Value) 
            {
                MessageBox.Show("You cant choose the (TO) Date before (FROM) Date",
                    "ERROR choosing Date",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime dtFrom = dtpFrom.Value;
            DateTime dtTo = dtpTo.Value;
            
            OnPicktheDate?.Invoke(dtFrom, dtTo);

            this.Close();
        }
    }
}
