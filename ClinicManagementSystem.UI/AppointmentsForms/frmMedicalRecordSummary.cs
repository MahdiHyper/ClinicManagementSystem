using ClinicManagementSystem.Business;
using ClinicManagementSystem.Logic;
using ClinicManagementSystem.UI.PrescriptionForms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicManagementSystem.UI.AppointmentsForms
{
    public partial class frmMedicalRecordSummary : Form
    {
        private int _MedicalRecordID;
        private clsMedicalRecord _MedicalRecord;
        private clsPrescription _Prescription;
        private int _PrescriptionID;
        private clsPatient _Patient;

        public frmMedicalRecordSummary(int MedicalRecord)
        {
            InitializeComponent();
            _MedicalRecordID = MedicalRecord;
        }

        private void clsMedicalRecordSummary_Load(object sender, EventArgs e)
        {
            _MedicalRecord = clsMedicalRecord.FindByID(_MedicalRecordID);

            if (_MedicalRecord == null)
            {
                MessageBox.Show("Not found the Medical Record info try later",
                    "Medical Record not found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.Close();
                return;
            }

            _FillMedicalRecordInfo();
            _CheckMedicalRecord();
        }

        private void _FillMedicalRecordInfo()
        {
            lblDiagnosis.Text = _MedicalRecord.Diagnosis.ToString();
            lblDiagnosisDate.Text = _MedicalRecord.DiagnosisDate.ToString();
            lblDiagnosisNotes.Text = _MedicalRecord.Notes.ToString();
        }
        private void _CheckMedicalRecord()
        {
            if (clsPrescription.IsLinkedWithMedicalRecord(_MedicalRecordID))
            {
                _Prescription = clsPrescription.FindByMedicalRecordID(_MedicalRecordID);
                if (_Prescription == null)
                {
                    MessageBox.Show("Not found the Prescription info try later",
                    "Prescription not found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    return;
                }
                _PrescriptionID = _Prescription.PrescriptionID;

                btnAddorUpdatePrescription.Text = "Edit Prescription";
                PrescriptionInfoPanel.Visible = true;
                lblIsTherePrescription.Visible = false;

                lblPriscriptionID.Text = _PrescriptionID.ToString();
                lblPrescriptionDate.Text = _Prescription.IssueDate.ToString();
                lblPrescriptionNumberOfItems.Text = _Prescription.GetNumberOfPrescriptionItems().ToString();
                lblPrescriptionNotes.Text = _Prescription.Notes.ToString();
            }
            else
            {
                btnAddorUpdatePrescription.Text = "Add Prescription";
                PrescriptionInfoPanel.Visible = false;
                lblIsTherePrescription.Visible = true;
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddorUpdatePrescription_Click(object sender, EventArgs e)
        {
            frmAddUpdatePrescription frm = new frmAddUpdatePrescription(_MedicalRecordID);
            frm.ShowDialog();
            _CheckMedicalRecord();
        }

        private void btnSendSummaryToPatient_Click(object sender, EventArgs e)
        {
            CreateSummaryPDF();
        }
        private void CreateSummaryPDF()
        {
            try
            {
                if (_MedicalRecord == null)
                {
                    MessageBox.Show("Medical Record not loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _Patient = clsPatient.GetPatientByID(_MedicalRecord.PatientID);
                if (_Patient == null)
                {
                    MessageBox.Show("Patient data not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF Files (*.pdf)|*.pdf";
                sfd.FileName = "MedicalRecord_" + _MedicalRecordID.ToString() + "_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".pdf";
                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                string filePath = sfd.FileName;

                Document doc = new Document(PageSize.A4, 36, 36, 36, 36);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                doc.Open();

                iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLACK);
                iTextSharp.text.Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK);
                iTextSharp.text.Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);
                iTextSharp.text.Font smallGray = FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.DARK_GRAY);

                System.Drawing.Image netImg = Properties.Resources.LOGO_PNG;
                using (var ms = new MemoryStream())
                {
                    netImg.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    iTextSharp.text.Image pdfImg = iTextSharp.text.Image.GetInstance(ms.ToArray());
                    pdfImg.ScaleToFit(100f, 100f);
                    pdfImg.Alignment = Element.ALIGN_LEFT;
                    doc.Add(pdfImg);
                }

                Paragraph title = new Paragraph("Appointment / Medical Record Summary", titleFont);
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

                string patientName = _Patient.PersonInfo?.FullName ?? "N/A";
                string phone = _Patient.PersonInfo?.PhoneNumber ?? "N/A";
                string ageStr = (_Patient.PersonInfo?.Age ?? 0).ToString();
                string bloodType = _Patient.GetBloodType();

                PdfPTable patientTable = new PdfPTable(2);
                patientTable.SpacingBefore = 20f;
                patientTable.WidthPercentage = 100;
                patientTable.HorizontalAlignment = Element.ALIGN_LEFT;
                patientTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                patientTable.AddCell(new Phrase("Patient Name:", boldFont));
                patientTable.AddCell(new Phrase(patientName, normalFont));

                patientTable.AddCell(new Phrase("Blood Type:", boldFont));
                patientTable.AddCell(new Phrase(string.IsNullOrWhiteSpace(bloodType) ? "N/A" : bloodType, normalFont));

                patientTable.AddCell(new Phrase("Phone:", boldFont));
                patientTable.AddCell(new Phrase(phone, normalFont));

                patientTable.AddCell(new Phrase("Age:", boldFont));
                patientTable.AddCell(new Phrase(ageStr, normalFont));

                if (!string.IsNullOrWhiteSpace(_Patient.Notes))
                {
                    patientTable.AddCell(new Phrase("Notes:", boldFont));
                    patientTable.AddCell(new Phrase(_Patient.Notes, normalFont));
                }

                doc.Add(patientTable);

                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph(" "));

                PdfPTable diagnosisTable = new PdfPTable(2);
                diagnosisTable.SpacingBefore = 10f;
                diagnosisTable.WidthPercentage = 100;
                diagnosisTable.HorizontalAlignment = Element.ALIGN_LEFT;
                diagnosisTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                diagnosisTable.AddCell(new Phrase("Diagnosis:", boldFont));
                diagnosisTable.AddCell(new Phrase((_MedicalRecord.Diagnosis ?? "").ToString(), normalFont));

                diagnosisTable.AddCell(new Phrase("Diagnosis Date:", boldFont));
                diagnosisTable.AddCell(new Phrase(_MedicalRecord.DiagnosisDate.ToString(), normalFont));

                if (!string.IsNullOrWhiteSpace(_MedicalRecord.Notes))
                {
                    diagnosisTable.AddCell(new Phrase("Diagnosis Notes:", boldFont));
                    diagnosisTable.AddCell(new Phrase(_MedicalRecord.Notes, normalFont));
                }

                doc.Add(diagnosisTable);

                _Prescription = clsPrescription.FindByMedicalRecordID(_MedicalRecordID);

                if (_Prescription != null)
                {
                    int prescriptionID = _Prescription.PrescriptionID;
                    DataTable dtItems = clsPrescription.GetMedicationsByPrescriptionID(prescriptionID);

                    doc.Add(new Paragraph(" "));
                    doc.Add(new Paragraph("Prescription Details", titleFont));

                    PdfPTable presTable = new PdfPTable(2);
                    presTable.WidthPercentage = 100;
                    presTable.SpacingBefore = 10f;
                    presTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                    presTable.AddCell(new Phrase("Prescription ID:", boldFont));
                    presTable.AddCell(new Phrase(prescriptionID.ToString(), normalFont));

                    presTable.AddCell(new Phrase("Issue Date:", boldFont));
                    presTable.AddCell(new Phrase(_Prescription.IssueDate.ToShortDateString(), normalFont));

                    presTable.AddCell(new Phrase("Notes:", boldFont));
                    presTable.AddCell(new Phrase(_Prescription.Notes ?? "-", normalFont));

                    doc.Add(presTable);
                    doc.Add(new Paragraph(" "));

                    // رسم جدول العناصر
                    if (dtItems != null && dtItems.Rows.Count > 0)
                    {
                        PdfPTable itemsTable = new PdfPTable(6);
                        itemsTable.WidthPercentage = 100;
                        itemsTable.SpacingBefore = 5f;
                        itemsTable.SetWidths(new float[] {2.0f, 1.5f, 1.5f, 3f, 2.5f, 4f });

                        // ترويسة الأعمدة
                        string[] headers = { "Name", "Dosage", "Type", "Note", "S.N.", "Description" };
                        foreach (var h in headers)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(h, boldFont));
                            cell.BackgroundColor = new BaseColor(235, 235, 235);
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.Padding = 5;
                            itemsTable.AddCell(cell);
                        }

                        foreach (DataRow row in dtItems.Rows)
                        {
                            itemsTable.AddCell(new Phrase(row["MedicationName"]?.ToString() ?? "-", normalFont));
                            itemsTable.AddCell(new Phrase(row["Dosage"]?.ToString() ?? "-", normalFont));
                            itemsTable.AddCell(new Phrase(row["DosageType"]?.ToString() ?? "-", normalFont));
                            itemsTable.AddCell(new Phrase(row["DosageNote"]?.ToString() ?? "-", normalFont));
                            itemsTable.AddCell(new Phrase(row["MedicationSerialNumber"]?.ToString() ?? "-", normalFont));
                            itemsTable.AddCell(new Phrase(row["Description"]?.ToString() ?? "-", normalFont));
                        }

                        doc.Add(itemsTable);
                    }
                    else
                    {
                        doc.Add(new Paragraph("No prescription items found for this record.", normalFont));
                    }
                }
                else
                {
                    doc.Add(new Paragraph("No prescription found for this medical record.", normalFont));
                }


                doc.Add(new Paragraph(" "));
                ColumnText.ShowTextAligned(writer.DirectContent,
                Element.ALIGN_RIGHT,
                new Phrase("Generated on " + DateTime.Now.ToString("G"), smallGray),
                doc.PageSize.Width - 50, 30, 0);


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
