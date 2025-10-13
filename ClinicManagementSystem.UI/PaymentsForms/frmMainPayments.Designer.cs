namespace ClinicManagementSystem.UI.PaymentsForms
{
    partial class frmMainPayments
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainPayments));
            this.guna2Separator2 = new Guna.UI2.WinForms.Guna2Separator();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.dgvPaymentsList = new Guna.UI2.WinForms.Guna2DataGridView();
            this.btnShowPaymentCard = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnEditPayment = new Guna.UI2.WinForms.Guna2GradientButton();
            this.lblTotalInvoices = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2CustomGradientPanel2 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lblAmountRecived = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2CustomGradientPanel3 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.guna2PictureBox2 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2HtmlLabel5 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.guna2ShadowPanel2 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaymentsList)).BeginInit();
            this.guna2CustomGradientPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.guna2CustomGradientPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).BeginInit();
            this.guna2ShadowPanel1.SuspendLayout();
            this.guna2ShadowPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Separator2
            // 
            this.guna2Separator2.Location = new System.Drawing.Point(316, 103);
            this.guna2Separator2.Name = "guna2Separator2";
            this.guna2Separator2.Size = new System.Drawing.Size(404, 10);
            this.guna2Separator2.TabIndex = 49;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BorderRadius = 20;
            this.guna2Panel1.Controls.Add(this.guna2HtmlLabel1);
            this.guna2Panel1.FillColor = System.Drawing.Color.White;
            this.guna2Panel1.Location = new System.Drawing.Point(344, 22);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(342, 77);
            this.guna2Panel1.TabIndex = 48;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.AutoSize = false;
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.White;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Century Gothic", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(191)))), ((int)(((byte)(193)))));
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(4, 16);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(336, 46);
            this.guna2HtmlLabel1.TabIndex = 0;
            this.guna2HtmlLabel1.Text = "Payments";
            this.guna2HtmlLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvPaymentsList
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.dgvPaymentsList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPaymentsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPaymentsList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvPaymentsList.ColumnHeadersHeight = 4;
            this.dgvPaymentsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPaymentsList.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvPaymentsList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvPaymentsList.Location = new System.Drawing.Point(0, 292);
            this.dgvPaymentsList.Name = "dgvPaymentsList";
            this.dgvPaymentsList.RowHeadersVisible = false;
            this.dgvPaymentsList.RowHeadersWidth = 100;
            this.dgvPaymentsList.Size = new System.Drawing.Size(1080, 466);
            this.dgvPaymentsList.TabIndex = 50;
            this.dgvPaymentsList.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvPaymentsList.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvPaymentsList.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvPaymentsList.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvPaymentsList.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvPaymentsList.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvPaymentsList.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvPaymentsList.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvPaymentsList.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvPaymentsList.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPaymentsList.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvPaymentsList.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvPaymentsList.ThemeStyle.HeaderStyle.Height = 4;
            this.dgvPaymentsList.ThemeStyle.ReadOnly = false;
            this.dgvPaymentsList.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvPaymentsList.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPaymentsList.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPaymentsList.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvPaymentsList.ThemeStyle.RowsStyle.Height = 22;
            this.dgvPaymentsList.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvPaymentsList.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // btnShowPaymentCard
            // 
            this.btnShowPaymentCard.BorderRadius = 10;
            this.btnShowPaymentCard.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnShowPaymentCard.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnShowPaymentCard.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnShowPaymentCard.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnShowPaymentCard.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnShowPaymentCard.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(224)))), ((int)(((byte)(170)))));
            this.btnShowPaymentCard.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(180)))), ((int)(((byte)(170)))));
            this.btnShowPaymentCard.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowPaymentCard.ForeColor = System.Drawing.Color.White;
            this.btnShowPaymentCard.Location = new System.Drawing.Point(348, 218);
            this.btnShowPaymentCard.Name = "btnShowPaymentCard";
            this.btnShowPaymentCard.Size = new System.Drawing.Size(180, 64);
            this.btnShowPaymentCard.TabIndex = 51;
            this.btnShowPaymentCard.Text = "Show Info";
            this.btnShowPaymentCard.Click += new System.EventHandler(this.btnShowPaymentCard_Click);
            // 
            // btnEditPayment
            // 
            this.btnEditPayment.BorderRadius = 10;
            this.btnEditPayment.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnEditPayment.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnEditPayment.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnEditPayment.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnEditPayment.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnEditPayment.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(40)))), ((int)(((byte)(73)))));
            this.btnEditPayment.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(54)))), ((int)(((byte)(106)))));
            this.btnEditPayment.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditPayment.ForeColor = System.Drawing.Color.White;
            this.btnEditPayment.Location = new System.Drawing.Point(534, 218);
            this.btnEditPayment.Name = "btnEditPayment";
            this.btnEditPayment.Size = new System.Drawing.Size(142, 64);
            this.btnEditPayment.TabIndex = 52;
            this.btnEditPayment.Text = "Edit Invoice";
            this.btnEditPayment.Click += new System.EventHandler(this.btnEditPayment_Click);
            // 
            // lblTotalInvoices
            // 
            this.lblTotalInvoices.AutoSize = false;
            this.lblTotalInvoices.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalInvoices.Font = new System.Drawing.Font("Century Gothic", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalInvoices.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(40)))), ((int)(((byte)(73)))));
            this.lblTotalInvoices.Location = new System.Drawing.Point(20, 117);
            this.lblTotalInvoices.Name = "lblTotalInvoices";
            this.lblTotalInvoices.Size = new System.Drawing.Size(104, 43);
            this.lblTotalInvoices.TabIndex = 56;
            this.lblTotalInvoices.Text = "00";
            this.lblTotalInvoices.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel2.ForeColor = System.Drawing.Color.Gray;
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(27, 93);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(90, 19);
            this.guna2HtmlLabel2.TabIndex = 55;
            this.guna2HtmlLabel2.Text = "Total Invoices";
            // 
            // guna2CustomGradientPanel2
            // 
            this.guna2CustomGradientPanel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2CustomGradientPanel2.BorderRadius = 18;
            this.guna2CustomGradientPanel2.Controls.Add(this.guna2PictureBox1);
            this.guna2CustomGradientPanel2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(222)))));
            this.guna2CustomGradientPanel2.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(222)))));
            this.guna2CustomGradientPanel2.FillColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(222)))));
            this.guna2CustomGradientPanel2.FillColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(222)))));
            this.guna2CustomGradientPanel2.Location = new System.Drawing.Point(36, 16);
            this.guna2CustomGradientPanel2.Name = "guna2CustomGradientPanel2";
            this.guna2CustomGradientPanel2.Size = new System.Drawing.Size(72, 71);
            this.guna2CustomGradientPanel2.TabIndex = 54;
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox1.ErrorImage")));
            this.guna2PictureBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox1.Image")));
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(14, 13);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(45, 50);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 55;
            this.guna2PictureBox1.TabStop = false;
            // 
            // lblAmountRecived
            // 
            this.lblAmountRecived.AutoSize = false;
            this.lblAmountRecived.BackColor = System.Drawing.Color.Transparent;
            this.lblAmountRecived.Font = new System.Drawing.Font("Century Gothic", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmountRecived.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(40)))), ((int)(((byte)(73)))));
            this.lblAmountRecived.Location = new System.Drawing.Point(8, 118);
            this.lblAmountRecived.Name = "lblAmountRecived";
            this.lblAmountRecived.Size = new System.Drawing.Size(128, 39);
            this.lblAmountRecived.TabIndex = 59;
            this.lblAmountRecived.Text = "00.0K";
            this.lblAmountRecived.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            // 
            // guna2CustomGradientPanel3
            // 
            this.guna2CustomGradientPanel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2CustomGradientPanel3.BorderRadius = 18;
            this.guna2CustomGradientPanel3.Controls.Add(this.guna2PictureBox2);
            this.guna2CustomGradientPanel3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(191)))), ((int)(((byte)(193)))));
            this.guna2CustomGradientPanel3.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(191)))), ((int)(((byte)(193)))));
            this.guna2CustomGradientPanel3.FillColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(191)))), ((int)(((byte)(193)))));
            this.guna2CustomGradientPanel3.FillColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(191)))), ((int)(((byte)(193)))));
            this.guna2CustomGradientPanel3.Location = new System.Drawing.Point(36, 16);
            this.guna2CustomGradientPanel3.Name = "guna2CustomGradientPanel3";
            this.guna2CustomGradientPanel3.Size = new System.Drawing.Size(72, 71);
            this.guna2CustomGradientPanel3.TabIndex = 57;
            // 
            // guna2PictureBox2
            // 
            this.guna2PictureBox2.ErrorImage = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox2.ErrorImage")));
            this.guna2PictureBox2.FillColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox2.Image")));
            this.guna2PictureBox2.ImageRotate = 0F;
            this.guna2PictureBox2.Location = new System.Drawing.Point(12, 13);
            this.guna2PictureBox2.Name = "guna2PictureBox2";
            this.guna2PictureBox2.Size = new System.Drawing.Size(46, 45);
            this.guna2PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox2.TabIndex = 55;
            this.guna2PictureBox2.TabStop = false;
            // 
            // guna2HtmlLabel5
            // 
            this.guna2HtmlLabel5.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel5.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel5.ForeColor = System.Drawing.Color.Gray;
            this.guna2HtmlLabel5.Location = new System.Drawing.Point(17, 93);
            this.guna2HtmlLabel5.Name = "guna2HtmlLabel5";
            this.guna2HtmlLabel5.Size = new System.Drawing.Size(111, 19);
            this.guna2HtmlLabel5.TabIndex = 58;
            this.guna2HtmlLabel5.Text = "Amount Recived";
            // 
            // guna2ShadowPanel1
            // 
            this.guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel1.Controls.Add(this.lblAmountRecived);
            this.guna2ShadowPanel1.Controls.Add(this.guna2CustomGradientPanel3);
            this.guna2ShadowPanel1.Controls.Add(this.guna2HtmlLabel5);
            this.guna2ShadowPanel1.FillColor = System.Drawing.Color.White;
            this.guna2ShadowPanel1.Location = new System.Drawing.Point(899, 108);
            this.guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            this.guna2ShadowPanel1.Radius = 10;
            this.guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel1.ShadowDepth = 40;
            this.guna2ShadowPanel1.ShadowShift = 4;
            this.guna2ShadowPanel1.ShadowStyle = Guna.UI2.WinForms.Guna2ShadowPanel.ShadowMode.ForwardDiagonal;
            this.guna2ShadowPanel1.Size = new System.Drawing.Size(147, 174);
            this.guna2ShadowPanel1.TabIndex = 55;
            // 
            // guna2ShadowPanel2
            // 
            this.guna2ShadowPanel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel2.Controls.Add(this.lblTotalInvoices);
            this.guna2ShadowPanel2.Controls.Add(this.guna2HtmlLabel2);
            this.guna2ShadowPanel2.Controls.Add(this.guna2CustomGradientPanel2);
            this.guna2ShadowPanel2.FillColor = System.Drawing.Color.White;
            this.guna2ShadowPanel2.Location = new System.Drawing.Point(27, 108);
            this.guna2ShadowPanel2.Name = "guna2ShadowPanel2";
            this.guna2ShadowPanel2.Radius = 10;
            this.guna2ShadowPanel2.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel2.ShadowDepth = 40;
            this.guna2ShadowPanel2.ShadowShift = 4;
            this.guna2ShadowPanel2.ShadowStyle = Guna.UI2.WinForms.Guna2ShadowPanel.ShadowMode.ForwardDiagonal;
            this.guna2ShadowPanel2.Size = new System.Drawing.Size(147, 174);
            this.guna2ShadowPanel2.TabIndex = 60;
            // 
            // frmMainPayments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1080, 758);
            this.Controls.Add(this.guna2ShadowPanel2);
            this.Controls.Add(this.guna2ShadowPanel1);
            this.Controls.Add(this.btnEditPayment);
            this.Controls.Add(this.btnShowPaymentCard);
            this.Controls.Add(this.dgvPaymentsList);
            this.Controls.Add(this.guna2Separator2);
            this.Controls.Add(this.guna2Panel1);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMainPayments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Payments";
            this.Load += new System.EventHandler(this.frmMainPayments_Load);
            this.guna2Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaymentsList)).EndInit();
            this.guna2CustomGradientPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.guna2CustomGradientPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).EndInit();
            this.guna2ShadowPanel1.ResumeLayout(false);
            this.guna2ShadowPanel1.PerformLayout();
            this.guna2ShadowPanel2.ResumeLayout(false);
            this.guna2ShadowPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Separator guna2Separator2;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2DataGridView dgvPaymentsList;
        private Guna.UI2.WinForms.Guna2GradientButton btnShowPaymentCard;
        private Guna.UI2.WinForms.Guna2GradientButton btnEditPayment;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTotalInvoices;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblAmountRecived;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel3;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel5;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel2;
    }
}