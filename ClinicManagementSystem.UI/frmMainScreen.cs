using ClinicManagementSystem.Business;
using ClinicManagementSystem.Logic;
using ClinicManagementSystem.UI.AppointmentsForms;
using ClinicManagementSystem.UI.DoctorsForms;
using ClinicManagementSystem.UI.MedicalRecordsForms;
using ClinicManagementSystem.UI.PatientsForms;
using ClinicManagementSystem.UI.PaymentsForms;
using ClinicManagementSystem.UI.Settings;
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
using System.Windows.Forms.VisualStyles;

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
            _LoadAllFormData();
        }

        private void _LoadAllFormData()
        {
            lblClock.Text = DateTime.Now.ToString("hh:mm tt - dd MMMM");
            timer1.Start();
            lblName.Text = clsHelper.CurrentUser.PersonInfo.FirstName;

            _LoadMainScreenAllInfoOfBoxes();
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
            _LoadAllFormData();
        }
        private void btnPatients_Click(object sender, EventArgs e)
        {
            frmPatientsList frm = new frmPatientsList();
            frm.ShowDialog();
            _LoadAllFormData();
        }
        private void btnDoctors_Click(object sender, EventArgs e)
        {
            frmDoctorsList frm = new frmDoctorsList();
            frm.ShowDialog();
            _LoadAllFormData();
        }
        private void btnPrescriptions_Click(object sender, EventArgs e)
        {
            frmMainMedicalRecords frm = new frmMainMedicalRecords();
            frm.ShowDialog();
            _LoadAllFormData();
        }
        private void btnInvoices_Click(object sender, EventArgs e)
        {
            frmMainPayments frm = new frmMainPayments();
            frm.ShowDialog();
            _LoadAllFormData();
        }
        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmSettings frm = new frmSettings();
            frm.PasswordUpdated += _PasswordUpdated;
            frm.ShowDialog();

        }
        private void _PasswordUpdated(bool IsUpdated)
        {
            clsHelper.CurrentUser = null;
            timer1.Stop();

            _frmLogin.Show();
            this.Close();
        }
        public void btnLogout_Click(object sender, EventArgs e)
        {
            clsHelper.CurrentUser = null;
            timer1.Stop();

            _frmLogin.Show();
            this.Close();
        }
        private void btnListUser_Click(object sender, EventArgs e)
        {
            frmListUsers frm = new frmListUsers();
            frm.ShowDialog();
            _LoadAllFormData();
        }

        private void _LoadMainScreenAllInfoOfBoxes()
        {
            _LoadTotalUsersBox();
            _LoadTotalPatient();
            _LoadTotalDoctors();
            _LoadTotalTodayApps();
            _LoadTotalIncome();
            _LoadDoctorListData();
            _LoadNextAppointment();
        }
        private void _LoadTotalUsersBox()
        {
            lblTotalUsers.Text = clsMainScreenData.GetTotalUsers();
        }
        private void _LoadTotalPatient()
        {
            lblTotalPatient.Text = clsMainScreenData.GetTotalPatient();
        }
        private void _LoadTotalDoctors()
        {
            lblTotalDoctors.Text = clsMainScreenData.GetTotalDoctors();
        }
        private void _LoadTotalTodayApps()
        {
            lbltodayAppointment.Text = clsMainScreenData.GetTotalTodayAppointments();
        }
        private void btnNewAppointment_Click(object sender, EventArgs e)
        {
            frmAddUpdateAppointment frm = new frmAddUpdateAppointment();
            frm.ShowDialog();
            _LoadAllFormData();
        }
        private void _LoadTotalIncome()
        {
            lblTotalIncome.Text = clsMainScreenData.GetLastMonthIncome() + " $";
        }
        private void _LoadDoctorListData()
        {
            clsDoctor D1 = clsDoctor.GetDoctorByID(9);
            clsDoctor D2 = clsDoctor.GetDoctorByID(10);
            clsDoctor D3 = clsDoctor.GetDoctorByID(11);

            lblDoctorName1.Text = D1.PersonInfo.FullName;
            lblSpec1.Text = D1.GetSpecializationName();

            lblDoctorName2.Text = D2.PersonInfo.FullName;
            lblSpec2.Text = D2.GetSpecializationName();

            lblDoctorName3.Text = D3.PersonInfo.FullName;
            lblSpec3.Text = D3.GetSpecializationName();

            pb1.Image = D1.PersonInfo.Gender == 1 ? Properties.Resources.doctorMale : Properties.Resources.doctorFemale;
            pb2.Image = D2.PersonInfo.Gender == 1 ? Properties.Resources.doctorMale : Properties.Resources.doctorFemale;
            pb3.Image = D3.PersonInfo.Gender == 1 ? Properties.Resources.doctorMale : Properties.Resources.doctorFemale;
        }

        private void lblShowDoctorList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDoctorsList frm = new frmDoctorsList();
            frm.ShowDialog();
            _LoadAllFormData();
        }

        private void _LoadNextAppointment()
        {
            clsAppointment app = clsMainScreenData.GetNextScheduleAppointment();

            if (app != null)
            {
                PanelNextApp.Visible = true;
                lblShowAllInfo.Visible = true;
                lblNoApp.Visible = false;

                clsPatient Pt = clsPatient.GetPatientByID(app.PatientID);
                clsDoctor Dr = clsDoctor.GetDoctorByID(app.DoctorID);

                if (Dr != null && Pt != null)
                {


                    lblPatientName.Text = Pt.PersonInfo.FullName;
                    lblDoctorName.Text = Dr.PersonInfo.FullName;
                    lblPatientBloodType.Text = Pt.GetBloodType();
                    lblAppointmentDate.Text = app.AppointmentDateTime.ToString();

                    return;
                }
                
            }
            PanelNextApp.Visible = false;
            lblShowAllInfo.Visible = false;
            lblNoApp.Visible = true;
        }

        private void lblShowAllInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsAppointment app = clsMainScreenData.GetNextScheduleAppointment();

            frmAddUpdateAppointment frm = new frmAddUpdateAppointment(app.AppointmentID);
            frm.ShowDialog();
            _LoadAllFormData();
        }

        private void frmMainScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            clsHelper.CurrentUser = null;
            timer1.Stop();

            _frmLogin.Show();
           
        }
    }
}
