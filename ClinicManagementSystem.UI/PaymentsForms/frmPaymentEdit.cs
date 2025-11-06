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

namespace ClinicManagementSystem.UI.MedicalRecordsForms
{
    public partial class frmPaymentEdit : Form
    {
        private int _PaymentID;
        private clsPayment _Payment;
        private int _PatientID;
        private clsPatient _Patient;
        private int _DoctorID;
        private clsDoctor _Doctor;

        public frmPaymentEdit(int PaymentID)
        {
            InitializeComponent();
            _PaymentID = PaymentID;
        }

        private void frmPaymentEdit_Load(object sender, EventArgs e)
        {
            _Payment = clsPayment.GetPaymentByID(_PaymentID);

            if ( _Payment == null )
            {
                MessageBox.Show("Payment not found try later", "Error payment",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _FillPaymentInfo();
            _GetAndFillPatientInfo();
            _GetAndFillDoctorInfo();
        }

        private void _FillPaymentInfo()
        {
            lblPaymentID.Text = _PaymentID.ToString();
            txtPaymentAmount.Text = _Payment.PaymentAmount.ToString();
            txtPaymentRecived.Text = _Payment.PaymentReceived.ToString();
            lblIssueDate.Text = _Payment.PaymentDate.ToString();
            lblRemaining.Text = _GetRemainingAmountInDouble().ToString();
        }
        private void _GetAndFillPatientInfo()
        {
            _PatientID = clsPayment.GetPatientIdByPaymentID(_PaymentID);

            if (_PatientID < 1)
            {
                MessageBox.Show("Patient not found try later", "Error Patient",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _Patient = clsPatient.GetPatientByID(_PatientID);

            if (_Patient == null)
            {
                MessageBox.Show("Patient not found try later", "Error Patient",
                                                   MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblPatientFullName.Text = _Patient.PersonInfo.FullName;
            lblBloodeType.Text = _Patient.GetBloodType();
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

            lblCounsultationFee.Text = _Doctor.ConsultationFee.ToString();
        }
        private double _GetPaymentRecivedInDouble()
        {
            if (string.IsNullOrEmpty(txtPaymentRecived.Text.Trim())) return 0;

            double PaymentRecived = 0;

            if (Double.TryParse(txtPaymentRecived.Text.Trim(), out PaymentRecived))
            {
                return PaymentRecived;
            }

            return -1;
        }
        private double _GetPaymentAmountInDouble()
        {
            if (string.IsNullOrEmpty(txtPaymentAmount.Text.Trim())) return 0;


            double PaymentAmount = 0;

            if (Double.TryParse(txtPaymentAmount.Text.Trim(), out PaymentAmount))
            {
                return PaymentAmount;
            }

            return -1;
        }
        private double _GetRemainingAmountInDouble()
        {
            return _GetPaymentAmountInDouble() - _GetPaymentRecivedInDouble();
        }

        private void btnAddAmountToBox_Click(object sender, EventArgs e)
        {
            txtPaymentAmount.Text = _Doctor.ConsultationFee.ToString();
            lblRemaining.Text = _GetRemainingAmountInDouble().ToString();

        }

        private void txtPaymentAmount_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && txtPaymentAmount.Text.Contains("."))
            {
                e.Handled = true;
            }

        }
        private void txtPaymentRecived_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && txtPaymentRecived.Text.Contains('.'))
            {
                e.Handled = true;
            }

        }
        private bool _Validation()
        {
            if (_GetPaymentRecivedInDouble() < 0)
            {
              
                MessageBox.Show("Payment Recived can't be in negative", "Error Payment Recived",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                
                return false;
            }

            if (_GetPaymentAmountInDouble() < 0)
            {

                MessageBox.Show("Payment Amount can't be in negative", "Error Payment Amount",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);


                return false;
            }

            if (_GetPaymentRecivedInDouble() > _GetPaymentAmountInDouble())
            {
                MessageBox.Show("Recived can't be more than Amount", "Error Payment Recived",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);


                return false;
            }

            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Logic So0o0on .  . .", "So0on" , MessageBoxButtons.OK);
            return;
        }
        private void btnSavePayment_Click(object sender, EventArgs e)
        {
            if (!_Validation()) return;
            
            _Payment.PaymentDate = DateTime.Now;
            _Payment.PaymentReceived = _GetPaymentRecivedInDouble();
            _Payment.PaymentAmount = _GetPaymentAmountInDouble();
            
            if (_Payment.Save())
            {
                MessageBox.Show("Invoice saved successfully the Remaning amount is: " + _GetRemainingAmountInDouble().ToString()
                                , "Saved invoice",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                
                this.Close();

            }else
            {
                MessageBox.Show("An error occurred while saving the invoice.", "Save Error",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }


        }

        private void txtPaymentAmount_TextChanged(object sender, EventArgs e)
        {
            lblRemaining.Text = _GetRemainingAmountInDouble().ToString();
        }
        private void txtPaymentRecived_TextChanged(object sender, EventArgs e)
        {
            lblRemaining.Text = _GetRemainingAmountInDouble().ToString();
        }
    }
}
