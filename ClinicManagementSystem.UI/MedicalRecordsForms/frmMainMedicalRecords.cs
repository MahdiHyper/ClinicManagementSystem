using ClinicManagementSystem.Business;
using ClinicManagementSystem.Logic;
using ClinicManagementSystem.UI.AppointmentsForms;
using ClinicManagementSystem.UI.PatientsForms;
using ClinicManagementSystem.UI.PrescriptionForms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicManagementSystem.UI.MedicalRecordsForms
{
    public partial class frmMainMedicalRecords : Form
    {
        private int _PatientID;
        private clsPatient _Patient = new clsPatient();
        private DataTable _dtMedicalRecords = new DataTable();
        private DataTable _BloodTypes = new DataTable();

        public frmMainMedicalRecords()
        {
            InitializeComponent();
        }
        private void frmMainMedicalRecords_Load(object sender, EventArgs e)
        {
            btnExportPDF.Enabled = false;
            btnShowSummary.Enabled = false;
            btnShowPrescription.Enabled = false;
            _BloodTypes = clsPatient.GetAllBloodTypes();
        }

        private void btnChoosePatient_Click(object sender, EventArgs e)
        {
            frmPatientsList frm = new frmPatientsList(true);
            frm.OnPatientPicked += GetPatientID;
            frm.ShowDialog();
        }
        private void GetPatientID (int PatientID)
        {
            _PatientID = PatientID;
            _Patient = clsPatient.GetPatientByID(_PatientID);

            if ( _Patient == null )
            {
                MessageBox.Show("Patient not found try again.",
                    "Patinet Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            _LoadPatientInfo();

            _LoadRecordsData();
        }
        private void _LoadPatientInfo()
        {
            lblPatientFullName.Text = _Patient.PersonInfo.FullName;

            lblBloodeType.Text = _Patient.GetBloodType();
            lblPatientGender.Text = _Patient.PersonInfo.GenderName;
            lblPhoneNumber.Text = _Patient.PersonInfo.PhoneNumber;
            lblPatientNotes.Text = _Patient.Notes;
        }
        private void _LoadRecordsData()
        {
            _dtMedicalRecords = clsMedicalRecord.GetAllMedicalRecordsForPatient(_PatientID);

            if (_dtMedicalRecords.Rows.Count < 1)
            {
                MessageBox.Show("There are no Medical Records for this Patient",
                                    "No medical Records", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                return;
            }
            btnExportPDF.Enabled = true;
            btnShowSummary.Enabled = true;
            btnShowPrescription.Enabled = true;

            dgvMedicalRecords.DataSource = _dtMedicalRecords;
            _ApplyMedicalRecordsGridStyle();
            dgvMedicalRecords.Rows[0].Selected = true;

        }
        private void _ApplyMedicalRecordsGridStyle()
        {
            dgvMedicalRecords.ColumnHeadersHeight = 40;

            dgvMedicalRecords.ColumnHeadersVisible = true;
            dgvMedicalRecords.EnableHeadersVisualStyles = false;

            dgvMedicalRecords.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 10F, FontStyle.Bold);
            dgvMedicalRecords.DefaultCellStyle.ForeColor = Color.FromArgb(22, 40, 73);
            dgvMedicalRecords.DefaultCellStyle.SelectionBackColor = Color.FromArgb(234, 50, 88);
            dgvMedicalRecords.DefaultCellStyle.SelectionForeColor = Color.White;


            dgvMedicalRecords.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 10F, FontStyle.Bold);
            dgvMedicalRecords.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 40, 73);
            dgvMedicalRecords.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgvMedicalRecords.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMedicalRecords.ReadOnly = true;
            dgvMedicalRecords.AllowUserToAddRows = false;
            dgvMedicalRecords.RowHeadersVisible = false;
            dgvMedicalRecords.RowTemplate.Height = 70;


            dgvMedicalRecords.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvMedicalRecords.Columns.Contains("MedicalRecordID")) dgvMedicalRecords.Columns["MedicalRecordID"].FillWeight = 100;
            if (dgvMedicalRecords.Columns.Contains("Diagnosis")) dgvMedicalRecords.Columns["Diagnosis"].FillWeight = 120;
            if (dgvMedicalRecords.Columns.Contains("DiagnosisDate")) dgvMedicalRecords.Columns["DiagnosisDate"].FillWeight = 150;
            if (dgvMedicalRecords.Columns.Contains("PrescriptionID")) dgvMedicalRecords.Columns["PrescriptionID"].FillWeight = 150;
            if (dgvMedicalRecords.Columns.Contains("Notes")) dgvMedicalRecords.Columns["Notes"].FillWeight = 200;


            if (dgvMedicalRecords.Columns.Contains("MedicalRecordID")) dgvMedicalRecords.Columns["MedicalRecordID"].HeaderText = "ID";
            if (dgvMedicalRecords.Columns.Contains("Diagnosis")) dgvMedicalRecords.Columns["Diagnosis"].HeaderText = "Diagnosis";
            if (dgvMedicalRecords.Columns.Contains("DiagnosisDate")) dgvMedicalRecords.Columns["DiagnosisDate"].HeaderText = "Diagnosis Date";
            if (dgvMedicalRecords.Columns.Contains("PrescriptionID")) dgvMedicalRecords.Columns["PrescriptionID"].HeaderText = "Prescription ID";
            dgvMedicalRecords.Update();

        }

        private void btnShowSummary_Click(object sender, EventArgs e)
        {
            frmMedicalRecordSummary frm = new frmMedicalRecordSummary(_GetSelectedRowID());
            frm.ShowDialog();
            _LoadRecordsData();
        }
        private void btnShowPrescription_Click(object sender, EventArgs e)
        {
            bool IsTherePrescription = clsPrescription.IsLinkedWithMedicalRecord(_GetSelectedRowID());

            if (IsTherePrescription)
            {
                frmAddUpdatePrescription frm = new frmAddUpdatePrescription(_GetSelectedRowID());
                frm.ShowDialog();
                _LoadRecordsData();
            }
            else
            {
                if (MessageBox.Show("There is no prescription for this Medical Record are you sure you want to add one ?",
                    "Adding new Prescription", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    frmAddUpdatePrescription frm = new frmAddUpdatePrescription(_GetSelectedRowID());
                    frm.ShowDialog();
                    _LoadRecordsData();
                }
                else
                {
                    return;
                }
            }
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMedicalRecords.CurrentRow == null)
                {
                    MessageBox.Show("Please select a medical record first.", "No Record Selected",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int recordID = (int)dgvMedicalRecords.CurrentRow.Cells["MedicalRecordID"].Value;
                clsMedicalRecord record = clsMedicalRecord.FindByID(recordID);
                if (record == null)
                {
                    MessageBox.Show("Medical record not found.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _Patient = clsPatient.GetPatientByID(record.PatientID);
                if (_Patient == null)
                {
                    MessageBox.Show("Patient data not found.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF Files (*.pdf)|*.pdf";
                sfd.FileName = $"MedicalRecord_{record.MedicalRecordID}_{_Patient.PersonInfo.FullName.Replace(' ', '_')}.pdf";

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                string filePath = sfd.FileName;

                // إنشاء المستند
                iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4, 36, 36, 36, 36);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                doc.Open();

                // خطوط
                iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLACK);
                iTextSharp.text.Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK);
                iTextSharp.text.Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);
                iTextSharp.text.Font smallGray = FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.DARK_GRAY);

                // شعار
                System.Drawing.Image netImg = Properties.Resources.LOGO_PNG;
                using (var ms = new MemoryStream())
                {
                    netImg.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    iTextSharp.text.Image pdfImg = iTextSharp.text.Image.GetInstance(ms.ToArray());
                    pdfImg.ScaleToFit(100f, 100f);
                    pdfImg.Alignment = Element.ALIGN_LEFT;
                    doc.Add(pdfImg);
                }

                // العنوان
                Paragraph title = new Paragraph("Medical Record Summary", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);

                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph(" "));

                // بيانات المريض
                PdfPTable patientTable = new PdfPTable(2);
                patientTable.WidthPercentage = 100;
                patientTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                patientTable.AddCell(new Phrase("Patient Name:", boldFont));
                patientTable.AddCell(new Phrase(_Patient.PersonInfo.FullName, normalFont));

                patientTable.AddCell(new Phrase("Gender:", boldFont));
                patientTable.AddCell(new Phrase(_Patient.PersonInfo.GenderName, normalFont));

                patientTable.AddCell(new Phrase("Phone:", boldFont));
                patientTable.AddCell(new Phrase(_Patient.PersonInfo.PhoneNumber ?? "-", normalFont));

                patientTable.AddCell(new Phrase("Blood Type:", boldFont));
                patientTable.AddCell(new Phrase(_Patient.GetBloodType(), normalFont));

                doc.Add(patientTable);

                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph(" "));

                // بيانات السجل
                PdfPTable recordTable = new PdfPTable(2);
                recordTable.WidthPercentage = 100;
                recordTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                recordTable.AddCell(new Phrase("Diagnosis:", boldFont));
                recordTable.AddCell(new Phrase(record.Diagnosis ?? "-", normalFont));

                recordTable.AddCell(new Phrase("Diagnosis Date:", boldFont));
                recordTable.AddCell(new Phrase(record.DiagnosisDate.ToShortDateString(), normalFont));

                recordTable.AddCell(new Phrase("Notes:", boldFont));
                recordTable.AddCell(new Phrase(record.Notes ?? "-", normalFont));

                doc.Add(recordTable);

                // التاريخ أسفل الصفحة
                doc.Add(new Paragraph(" "));
                ColumnText.ShowTextAligned(writer.DirectContent,
                    Element.ALIGN_RIGHT,
                    new Phrase("Generated on " + DateTime.Now.ToString("G"), smallGray),
                    doc.PageSize.Width - 50, 30, 0);

                doc.Close();
                writer.Close();

                MessageBox.Show("PDF exported successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting PDF:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int _GetSelectedRowID()
        {
            int rowID = 0;
            rowID = (int)dgvMedicalRecords.CurrentRow.Cells["MedicalRecordID"].Value;
            return rowID;
        }

    }
}
