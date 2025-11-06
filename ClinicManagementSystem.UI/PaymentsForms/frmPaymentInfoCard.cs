using ClinicManagementSystem.Business;
using ClinicManagementSystem.Logic;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;



namespace ClinicManagementSystem.UI.PaymentsForms
{
    public partial class frmPaymentInfoCard : Form
    {
        private int _PaymentID;
        private clsPayment _Payment;
        private int _PatientID;
        private clsPatient _Patient;
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

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            CreateAndDrawPdf();
        }

        private void CreateAndDrawPdf()
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF Files (*.pdf)|*.pdf";
                sfd.FileName = "Invoice_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".pdf";

                if (sfd.ShowDialog() != DialogResult.OK)
                    return; 

                string filePath = sfd.FileName;

                Document doc = new Document(PageSize.A4, 36, 36, 36, 36);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                doc.Open();

                iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLACK);
                iTextSharp.text.Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK);
                iTextSharp.text.Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);

                System.Drawing.Image netImg = Properties.Resources.LOGO_PNG;

                using (var ms = new MemoryStream())
                {
                    netImg.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    iTextSharp.text.Image pdfImg = iTextSharp.text.Image.GetInstance(ms.ToArray());
                    pdfImg.ScaleToFit(100f, 100f);
                    pdfImg.Alignment = Element.ALIGN_LEFT;
                    doc.Add(pdfImg);
                }

                Paragraph title = new Paragraph("Invoice Summary", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);

                doc.Add(new Paragraph(" "));

                PdfContentByte cb = writer.DirectContent;
                cb.SetColorStroke(BaseColor.DARK_GRAY);
                cb.SetLineWidth(2f);
                float lineY = doc.PageSize.Height - 150;
                cb.MoveTo(50, lineY);
                cb.LineTo(doc.PageSize.Width - 50, lineY);
                cb.Stroke();

                doc.Add(new Paragraph(" "));


                PdfPTable patientTable = new PdfPTable(2);
                patientTable.SpacingBefore = 20f;
                patientTable.WidthPercentage = 100;
                patientTable.HorizontalAlignment = Element.ALIGN_LEFT;
                patientTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                patientTable.AddCell(new Phrase("Patient Name:", boldFont));
                patientTable.AddCell(new Phrase(_Patient.PersonInfo.FullName, normalFont));

                patientTable.AddCell(new Phrase("Blood Type:", boldFont));
                patientTable.AddCell(new Phrase(_Patient.GetBloodType(), normalFont));

                patientTable.AddCell(new Phrase("Phone:", boldFont));
                patientTable.AddCell(new Phrase(_Patient.PersonInfo.PhoneNumber, normalFont));

                patientTable.AddCell(new Phrase("Age:", boldFont));
                patientTable.AddCell(new Phrase(_Patient.PersonInfo.Age.ToString(), normalFont));

                patientTable.AddCell(new Phrase("Notes:", boldFont));
                patientTable.AddCell(new Phrase(_Patient.Notes.ToString(), normalFont));

                doc.Add(patientTable);

                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph(" "));

                PdfPTable paymentTable = new PdfPTable(2);
                paymentTable.SpacingBefore = 10f;
                paymentTable.WidthPercentage = 100;
                paymentTable.HorizontalAlignment = Element.ALIGN_LEFT;
                paymentTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                paymentTable.AddCell(new Phrase("Invoice ID:", boldFont));
                paymentTable.AddCell(new Phrase(_PaymentID.ToString(), normalFont));

                paymentTable.AddCell(new Phrase("Payment Date:", boldFont));
                paymentTable.AddCell(new Phrase(_Payment.PaymentDate.ToShortDateString(), normalFont));

                paymentTable.AddCell(new Phrase("Total Amount:", boldFont));
                paymentTable.AddCell(new Phrase("$" + _Payment.PaymentAmount.ToString("0.00"), normalFont));

                paymentTable.AddCell(new Phrase("Received Amount:", boldFont));
                paymentTable.AddCell(new Phrase("$" + _Payment.PaymentReceived.ToString("0.00"), normalFont));

                paymentTable.AddCell(new Phrase("Remaining:", boldFont));
                paymentTable.AddCell(new Phrase("$" + _Payment.GetRemainingAmount().ToString("0.00"), normalFont));

                doc.Add(paymentTable);

                var smallGray = FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.DARK_GRAY);

                ColumnText.ShowTextAligned(
                    writer.DirectContent,
                    Element.ALIGN_RIGHT,
                    new Phrase("Generated on " + DateTime.Now.ToString("G"), smallGray),
                    doc.PageSize.Width - 50, 
                    30,                      
                    0
                );


                doc.Close();
                writer.Close();

                MessageBox.Show("PDF created successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(filePath);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while creating PDF:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
    }
}
