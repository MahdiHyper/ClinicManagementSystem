using ClinicManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystem.Logic
{
    public class clsPayment
    {
        enum enMode { AddNew = 1 , Update = 2};
        enMode _Mode = enMode.AddNew;

        public int PaymentID { get; private set; }
        public double PaymentAmount { get; set; }
        public double PaymentReceived {  get; set; }
        public DateTime PaymentDate { get; set; }

        public clsPayment ()
        {
            PaymentID = -1;
            PaymentAmount = 0;
            PaymentReceived = 0;
            PaymentDate = DateTime.Now;
            _Mode = enMode.AddNew;
        }
        public clsPayment (int  paymentID , double PaymentAmount , double PaymentReceived,
            DateTime PaymentDate)
        {
            this.PaymentID = paymentID;
            this.PaymentAmount = PaymentAmount;
            this.PaymentReceived = PaymentReceived;
            this.PaymentDate = PaymentDate;
            _Mode  = enMode.Update;
        }

        private bool _AddNew ()
        {
            this.PaymentID =  clsPaymentData.AddNewPayment(this.PaymentAmount , this.PaymentReceived,
                this.PaymentDate);

            if (this.PaymentID != -1)
            {
                return true;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("ERROR - Logic Payment (Add new) ");
                return false;
            }
        }

        private bool _Update ()
        {
            if (clsPaymentData.UpdatePayment(this.PaymentID, this.PaymentAmount, this.PaymentReceived, this.PaymentDate))
            {
                return true;
            }else
            {
                System.Diagnostics.Debug.WriteLine("ERROR - Logic Payment (Update) ");
                return false;
            }
        }

        public bool Save()
        {
            if (_Mode == enMode.AddNew)
            {
                if (_AddNew())
                {
                    _Mode = enMode.Update;
                    return true;
                }
                return false;
            } 
            else
            {
                return _Update();
            }

        }

        public static clsPayment GetPaymentByID (int PaymentID)
        {
            double PaymentAmount = -1;
            double PaymentReceived = -1;
            DateTime PaymentDate = DateTime.MinValue;

            if (clsPaymentData.GetPaymentByID(PaymentID , ref PaymentAmount, ref PaymentReceived, ref PaymentDate))
            {
                return new clsPayment (PaymentID, PaymentAmount, PaymentReceived, PaymentDate);
            }else
            {
                return null;
            }

        }

        public double GetRemainingAmount ()
        {
            return (this.PaymentAmount - this.PaymentReceived);
        }

        public static DataTable GetAllPayments()
        {
            return clsPaymentData.GetAllPayments();
        }

        public static int GetPatientIdByPaymentID (int PaymentID)
        {
            return clsPaymentData.GetPatientIdByPaymentID(PaymentID);
        }

        public static int GetDoctorIdByPaymentID (int PayemntID)
        {
            return clsPaymentData.GetDoctorIdByPaymentID(PayemntID);
        }

        public static string GetTotalAmountReceivedFormatted()
        {
            decimal total = clsPaymentData.GetTotalAmountReceived();

            if (total >= 1000)
                return $"{total / 1000:F1}K";

            return total.ToString("0.##");
        }

    }
}
