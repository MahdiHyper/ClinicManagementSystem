using ClinicManagementSystem.Logic;
using ClinicManagementSystem.UI.AppointmentsForms;
using ClinicManagementSystem.UI.DoctorsForms;
using ClinicManagementSystem.UI.MedicalRecordsForms;
using ClinicManagementSystem.UI.PatientsForms;
using ClinicManagementSystem.UI.PaymentsForms;
using ClinicManagmentSystem.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicManagementSystem.UI
{
    public partial class frmMainScreen : Form
    {
        frmLogin _frmLogin;
        public frmMainScreen(frmLogin frm)
        {
            InitializeComponent();
            _frmLogin = frm;
        }
        private void frmMainScreen_Load(object sender, EventArgs e)
        {
            lblClock.Text = DateTime.Now.ToString("hh:mm tt - dd MMMM");
            timer1.Start();
            lblName.Text = clsHelper.CurrentUser.PersonInfo.FirstName;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateTimeDisplay();
        }
        private void UpdateTimeDisplay()
        {
            lblClock.Text = DateTime.Now.ToString("dddd، dd MMMM yyyy - hh:mm tt");
        }
        private void btnAppointments_Click(object sender, EventArgs e)
        {
            frmAppointmentsList frm = new frmAppointmentsList();
            frm.ShowDialog();
        }
        private void btnPatients_Click(object sender, EventArgs e)
        {
            frmPatientsList frm = new frmPatientsList();
            frm.ShowDialog();
        }
        private void btnDoctors_Click(object sender, EventArgs e)
        {
            frmDoctorsList frm = new frmDoctorsList();
            frm.ShowDialog();
        }
        private void btnPrescriptions_Click(object sender, EventArgs e)
        {
            frmMainMedicalRecords frm = new frmMainMedicalRecords();
            frm.ShowDialog();
        }
        private void btnInvoices_Click(object sender, EventArgs e)
        {
            frmMainPayments frm = new frmMainPayments();
            frm.ShowDialog();
        }
        private void btnSettings_Click(object sender, EventArgs e)
        {
            MessageBox.Show("HERE UserSettings");
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            clsHelper.CurrentUser = null;
            timer1.Stop();

            _frmLogin.Show();
            this.Close();
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            frmListUsers frm = new frmListUsers();
            frm.ShowDialog();
        }
    }
}
