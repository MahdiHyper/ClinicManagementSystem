using ClinicManagementSystem.Business;
using ClinicManagementSystem.Logic;
using iTextSharp.text;
using iTextSharp.text.pdf;
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

namespace ClinicManagementSystem.UI.PrescriptionForms
{
    public partial class frmAddUpdatePrescription : Form
    {
        private int _MedicalRecordID;
        private clsMedicalRecord _MedicalRecord;
        private clsPrescription _Prescription;
        private int _PrescriptionID;
        private clsPatient _Patient;
        private DataTable _dt;
        private DataTable _BloodTypes;

        public frmAddUpdatePrescription(int MedicalRecordID)
        {
            InitializeComponent();
            _MedicalRecordID = MedicalRecordID;
        }

        private void frmAddUpdatePrescription_Load(object sender, EventArgs e)
        {
            _MedicalRecord = clsMedicalRecord.FindByAppID(_MedicalRecordID);
            if (_MedicalRecord == null)
            {
                MessageBox.Show("Medical Record not found",
                                        "Can't find Medical Record",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);

                return;
            }

            _Prescription = new clsPrescription();

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
                lblPrescriptionId.Text = "ID: " + _PrescriptionID.ToString();

                _LoadOrRefreshPrescriptionInfo();

            }
            else
            {
                DateTime IssueDate = DateTime.Now;
                string Notes = "";
                int PatientID = _MedicalRecord.PatientID;

                _Prescription.MedicalRecordID = _MedicalRecordID;
                _Prescription.IssueDate = IssueDate;
                _Prescription.Notes = Notes;
                _Prescription.PatientID = PatientID;

                if (_Prescription.Save())
                {
                    _PrescriptionID = _Prescription.PrescriptionID;
                    lblPrescriptionId.Text = "ID: " + _PrescriptionID.ToString();
                }
                else
                {
                    MessageBox.Show("Error while adding new Prescription",
                                        "Can't generate new Prescription",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);

                    return;
                }

                btnDeleteApp.Enabled = false;
                btnPrintPrescription.Enabled = false;
            }
            _FillFormInfo();
        }
        private void _LoadOrRefreshPrescriptionInfo()
        {
            _dt = clsPrescription.GetMedicationsByPrescriptionID( _PrescriptionID );
            dgvPrescriptionList.DataSource = _dt;
            _ApplyAppointmentsListGridStyle();

            if (dgvPrescriptionList.Rows.Count < 1)
            {
                btnDeleteApp.Enabled = false;
                btnPrintPrescription.Enabled = false;
            }
            else
            {
                btnDeleteApp.Enabled = true;
                btnPrintPrescription.Enabled = true;
               
                dgvPrescriptionList.Rows[0].Selected = true;
            }

            lblPrescriptionNumberOfItems.Text = _Prescription.GetNumberOfPrescriptionItems().ToString();
        }

        private void _ApplyAppointmentsListGridStyle()
        {
            dgvPrescriptionList.ColumnHeadersHeight = 40;

            dgvPrescriptionList.ColumnHeadersVisible = true;
            dgvPrescriptionList.EnableHeadersVisualStyles = false;

            dgvPrescriptionList.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 10F, FontStyle.Bold);
            dgvPrescriptionList.DefaultCellStyle.ForeColor = Color.FromArgb(22, 40, 73);
            dgvPrescriptionList.DefaultCellStyle.SelectionBackColor = Color.FromArgb(234, 50, 88);
            dgvPrescriptionList.DefaultCellStyle.SelectionForeColor = Color.White;


            dgvPrescriptionList.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 10F, FontStyle.Bold);
            dgvPrescriptionList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 40, 73);
            dgvPrescriptionList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgvPrescriptionList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPrescriptionList.ReadOnly = true;
            dgvPrescriptionList.AllowUserToAddRows = false;
            dgvPrescriptionList.RowHeadersVisible = false;
            dgvPrescriptionList.RowTemplate.Height = 70;


            dgvPrescriptionList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvPrescriptionList.Columns.Contains("PrescriptionItemID")) dgvPrescriptionList.Columns["PrescriptionItemID"].FillWeight = 20;
            if (dgvPrescriptionList.Columns.Contains("MedicationID")) dgvPrescriptionList.Columns["MedicationID"].FillWeight = 20;
            if (dgvPrescriptionList.Columns.Contains("MedicationName")) dgvPrescriptionList.Columns["MedicationName"].FillWeight = 120;
            if (dgvPrescriptionList.Columns.Contains("Dosage")) dgvPrescriptionList.Columns["Dosage"].FillWeight = 40;
            if (dgvPrescriptionList.Columns.Contains("DosageType")) dgvPrescriptionList.Columns["DosageType"].FillWeight = 60;
            if (dgvPrescriptionList.Columns.Contains("DosageNote")) dgvPrescriptionList.Columns["DosageNote"].FillWeight = 100;
            if (dgvPrescriptionList.Columns.Contains("MedicationSerialNumber")) dgvPrescriptionList.Columns["MedicationSerialNumber"].FillWeight = 60;
            if (dgvPrescriptionList.Columns.Contains("Description")) dgvPrescriptionList.Columns["Description"].FillWeight = 100;

            if (dgvPrescriptionList.Columns.Contains("PrescriptionItemID")) dgvPrescriptionList.Columns["PrescriptionItemID"].HeaderText = "Item ID";
            if (dgvPrescriptionList.Columns.Contains("MedicationID")) dgvPrescriptionList.Columns["MedicationID"].HeaderText = "Medication ID";
            if (dgvPrescriptionList.Columns.Contains("MedicationName")) dgvPrescriptionList.Columns["MedicationName"].HeaderText = "Name";
            if (dgvPrescriptionList.Columns.Contains("Dosage")) dgvPrescriptionList.Columns["Dosage"].HeaderText = "Dosage";
            if (dgvPrescriptionList.Columns.Contains("DosageNote")) dgvPrescriptionList.Columns["DosageNote"].HeaderText = "Dosage Note";
            if (dgvPrescriptionList.Columns.Contains("MedicationSerialNumber")) dgvPrescriptionList.Columns["MedicationSerialNumber"].HeaderText = "Serial Number";

        }
        private int _GetSelectedPrescriptionItemID()
        {
            int ID;
            ID = Convert.ToInt32(dgvPrescriptionList.CurrentRow.Cells["PrescriptionItemID"].Value);
            return ID;
        }
        private void _FillFormInfo()
        {
            _FillPatientInfo();
            _FillPrescriptionInfo();
        }
        private void _FillPatientInfo()
        {
            _Patient = clsPatient.GetPatientByID(_MedicalRecord.PatientID);

            if (_Patient == null)
            {
                MessageBox.Show("Error while loading Patient info",
                                       "Error patinet info",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Error);

                return;
            }

            _BloodTypes = clsPatient.GetAllBloodTypes();
            lblPatientFullName.Text = _Patient.PersonInfo.FullName;

            string BloodType = "N/A";
            if (_BloodTypes != null)
            {
                DataRow[] rows = _BloodTypes.Select($"BloodTypeID = {_Patient.BloodTypeID}");
                BloodType = rows[0]["BloodTypeName"].ToString();
            }
            lblBloodeType.Text = BloodType;
            lblPhoneNumber.Text = _Patient.PersonInfo.PhoneNumber;
            lblPatientGender.Text = _Patient.PersonInfo.GenderName;
            lblPatientNotes.Text = _Patient.Notes ?? "No notes available";

        }
        private void _FillPrescriptionInfo()
        {
            lblPrescriptionId.Text = "ID: " + _Prescription.PrescriptionID.ToString();
            lblPrescriptionNumberOfItems.Text = _Prescription.GetNumberOfPrescriptionItems().ToString();
            lblPrescriptionDate.Text = _Prescription.IssueDate.ToString();
            txtPrescriptionNotes.Text = _Prescription.Notes;
        }
        private void _SavingPrescription()
        {
            _Prescription.Notes = txtPrescriptionNotes.Text;
            if (!_Prescription.Save())
            {
                MessageBox.Show("Error while Saving the Prescription",
                                                        "Error saving Prescription",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Error);

                return;
            }
        }
        private string GetBloodType()
        {
            _BloodTypes = clsPatient.GetAllBloodTypes();

            string bloodTypeName = "N/A";

            if (_BloodTypes != null)
            {
                DataRow[] rows = _BloodTypes.Select($"BloodTypeID = {_Patient.BloodTypeID}");
                if (rows.Length > 0)
                {
                    bloodTypeName = rows[0]["BloodTypeName"].ToString();
                }
            }

            return bloodTypeName;
        }


        private void btnPrintPrescription_Click(object sender, EventArgs e)
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

                // Logo
                System.Drawing.Image netImg = Properties.Resources.LOGO_PNG;
                using (var ms = new MemoryStream())
                {
                    netImg.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    iTextSharp.text.Image pdfImg = iTextSharp.text.Image.GetInstance(ms.ToArray());
                    pdfImg.ScaleToFit(100f, 100f);
                    pdfImg.Alignment = Element.ALIGN_LEFT;
                    doc.Add(pdfImg);
                }

                // Title
                Paragraph title = new Paragraph("Appointment / Medical Record Summary", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);

                doc.Add(new Paragraph(" "));

                // Separator line
                PdfContentByte cb = writer.DirectContent;
                cb.SetColorStroke(BaseColor.DARK_GRAY);
                cb.SetLineWidth(2f);
                float lineY = doc.PageSize.Height - 150;
                cb.MoveTo(50, lineY);
                cb.LineTo(doc.PageSize.Width - 50, lineY);
                cb.Stroke();

                doc.Add(new Paragraph(" "));

                // Patient info
                string patientName = _Patient.PersonInfo?.FullName ?? "N/A";
                string phone = _Patient.PersonInfo?.PhoneNumber ?? "N/A";
                string ageStr = (_Patient.PersonInfo?.Age ?? 0).ToString();
                string bloodType = GetBloodType();
                if (string.IsNullOrWhiteSpace(bloodType)) bloodType = "N/A";

                PdfPTable patientTable = new PdfPTable(2);
                patientTable.SpacingBefore = 20f;
                patientTable.WidthPercentage = 100;
                patientTable.HorizontalAlignment = Element.ALIGN_LEFT;
                patientTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                patientTable.AddCell(new Phrase("Patient Name:", boldFont));
                patientTable.AddCell(new Phrase(patientName, normalFont));

                patientTable.AddCell(new Phrase("Blood Type:", boldFont));
                patientTable.AddCell(new Phrase(bloodType, normalFont));

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

                // Diagnosis
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

                // Prescription (only if exists AND has items)
                _Prescription = clsPrescription.FindByMedicalRecordID(_MedicalRecordID);

                if (_Prescription != null)
                {
                    int prescriptionID = _Prescription.PrescriptionID;
                    DataTable dtItems = clsPrescription.GetMedicationsByPrescriptionID(prescriptionID);

                    if (dtItems != null && dtItems.Rows.Count > 0)
                    {
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("Prescription Details", titleFont));
                        doc.Add(new Paragraph(" "));

                        PdfPTable presTable = new PdfPTable(2);
                        presTable.WidthPercentage = 100;
                        presTable.SpacingBefore = 5f;
                        presTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                        presTable.AddCell(new Phrase("Prescription ID:", boldFont));
                        presTable.AddCell(new Phrase(prescriptionID.ToString(), normalFont));

                        presTable.AddCell(new Phrase("Issue Date:", boldFont));
                        presTable.AddCell(new Phrase(_Prescription.IssueDate.ToShortDateString(), normalFont));

                        if (!string.IsNullOrWhiteSpace(_Prescription.Notes))
                        {
                            presTable.AddCell(new Phrase("Notes:", boldFont));
                            presTable.AddCell(new Phrase(_Prescription.Notes, normalFont));
                        }

                        doc.Add(presTable);
                        doc.Add(new Paragraph(" "));

                        PdfPTable itemsTable = new PdfPTable(6);
                        itemsTable.WidthPercentage = 100;
                        itemsTable.SpacingBefore = 5f;
                        itemsTable.SetWidths(new float[] { 2.0f, 1.5f, 1.5f, 3.0f, 2.5f, 4.0f });

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
                }

                doc.Add(new Paragraph(" "));
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
        private void btnDeleteApp_Click(object sender, EventArgs e)
        {
            int PrescriptionItemId = _GetSelectedPrescriptionItemID();

            if (MessageBox.Show($"Are you sure you want to delete Item ID {PrescriptionItemId}",
                "Confirm Deleting", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (!clsPrescriptionItem.DeletePrescriptionItem(PrescriptionItemId))
                {
                    MessageBox.Show($"Error while delete Item ID {PrescriptionItemId}",
                 "Error Deleting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (_Prescription.Save())
            {
                _LoadOrRefreshPrescriptionInfo();
            }
            else
            {
                MessageBox.Show($"Error while saving Prescription",
                 "Error Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Item deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            _SavingPrescription();
            frmAddPrescriptionItem frm = new frmAddPrescriptionItem(_PrescriptionID);
            frm.ShowDialog();
            _LoadOrRefreshPrescriptionInfo();
        }

        private void frmAddUpdatePrescription_FormClosing(object sender, FormClosingEventArgs e)
        {
            _SavingPrescription();
        }
    }
}
