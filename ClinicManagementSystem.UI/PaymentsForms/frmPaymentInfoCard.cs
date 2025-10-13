using ClinicManagementSystem.Business;
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

namespace ClinicManagementSystem.UI.PaymentsForms
{
    public partial class frmPaymentInfoCard : Form
    {
        private int _PaymentID;
        private clsPayment _Payment;
        private int _PatientID;
        private clsPatient _Patient;
        private DataTable _BloodTypes;
        private int _DoctorID;
        private clsDoctor _Doctor;
        private Dictionary<int, string> _AllSpecializations;

        public frmPaymentInfoCard(int PaymentID)
        {
            InitializeComponent();
            _PaymentID = PaymentID;
        }

        private void frmPaymentInfoCard_Load(object sender, EventArgs e)
        {
            _Payment = clsPayment.GetPaymentByID(_PaymentID);
            if (_Payment == null )
            {
                MessageBox.Show("ERROR - Payment (invoice) not found try later !",
                    "Payment not found" , MessageBoxButtons.OK , MessageBoxIcon.Error);

                return;
            }

            _GetAndFillPatientInfo();
            _GetAndFillDoctorInfo();
            _FillPaymentInfo();

        }

        private void _GetAndFillPatientInfo()
        {
            _PatientID = clsPayment.GetPatientIdByPaymentID(_PaymentID);
            _Patient = clsPatient.GetPatientByID(_PatientID);

            if (_Patient == null)
            {
                MessageBox.Show("Patient not found try later", "Error Patient",
                                                   MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _BloodTypes = clsPatient.GetAllBloodTypes();

            lblPatientFullName.Text = _Patient.PersonInfo.FullName.ToUpper();
            string bloodTypeName = "N/A";

            if (_BloodTypes != null)
            {
                DataRow[] rows = _BloodTypes.Select($"BloodTypeID = {_Patient.BloodTypeID}");
                if (rows.Length > 0)
                {
                    bloodTypeName = rows[0]["BloodTypeName"].ToString();
                }
            }

            lblBloodeType.Text = bloodTypeName;
            lblPhoneNumber.Text = _Patient.PersonInfo.PhoneNumber;
            lblPatientGender.Text = _Patient.PersonInfo.GenderName;
            lblPatientNotes.Text = _Patient.Notes.ToString();

        }
        private void _GetAndFillDoctorInfo()
        {
            _DoctorID = clsPayment.GetDoctorIdByPaymentID(_PaymentID);
            _Doctor = clsDoctor.GetDoctorByID(_DoctorID);

            if (_Doctor == null)
            {
                MessageBox.Show("Doctor not found try later", "Error Doctor",
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblConsultationFee.Text = _Doctor.ConsultationFee.ToString();
            lblDoctorName.Text = _Doctor.PersonInfo.FullName.ToUpper();
            lblEmail.Text = _Doctor.PersonInfo.Email;

            _AllSpecializations = clsDoctor.GetAllSpecializations();

            if (_AllSpecializations != null)
            {
                if (_AllSpecializations.ContainsKey(_Doctor.SpecializationID))
                {
                    lblSpecialization.Text = _AllSpecializations[_Doctor.SpecializationID].ToString();
                }
                else
                {
                    lblSpecialization.Text = "N/A";
                }
            }


        }
        private void _FillPaymentInfo()
        {
            lblPaymentID.Text = _PaymentID.ToString();
            lblPaymentAmount.Text = _Payment.PaymentAmount.ToString();
            lblPaymentRecived.Text = _Payment.PaymentReceived.ToString();
            lblIssueDate.Text = _Payment.PaymentDate.ToString();
            lblPaymentRemaining.Text = (_Payment.PaymentAmount - _Payment.PaymentReceived).ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
