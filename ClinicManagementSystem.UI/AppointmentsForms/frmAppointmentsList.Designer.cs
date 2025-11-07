namespace ClinicManagementSystem.UI.AppointmentsForms
{
    partial class frmAppointmentsList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAppointmentsList));
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvAppointmentsList = new Guna.UI2.WinForms.Guna2DataGridView();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.lblCount = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnAddApp = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnShowAppCard = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnEditApp = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnDeleteApp = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnCutsom = new Guna.UI2.WinForms.Guna2GradientButton();
            this.cbFilter = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtFilter = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnThisDay = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnThisWeek = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnThisMonth = new Guna.UI2.WinForms.Guna2GradientButton();
            this.lblDateFilterInfo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnClearFilter = new Guna.UI2.WinForms.Guna2GradientButton();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.guna2Separator2 = new Guna.UI2.WinForms.Guna2Separator();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointmentsList)).BeginInit();
            this.guna2Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
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
            this.guna2HtmlLabel1.Text = "Appointments";
            this.guna2HtmlLabel1.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BorderRadius = 20;
            this.guna2Panel1.Controls.Add(this.guna2HtmlLabel1);
            this.guna2Panel1.FillColor = System.Drawing.Color.White;
            this.guna2Panel1.Location = new System.Drawing.Point(474, 25);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(342, 77);
            this.guna2Panel1.TabIndex = 2;
            // 
            // dgvAppointmentsList
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvAppointmentsList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAppointmentsList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAppointmentsList.ColumnHeadersHeight = 4;
            this.dgvAppointmentsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAppointmentsList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAppointmentsList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvAppointmentsList.Location = new System.Drawing.Point(12, 339);
            this.dgvAppointmentsList.Name = "dgvAppointmentsList";
            this.dgvAppointmentsList.RowHeadersVisible = false;
            this.dgvAppointmentsList.Size = new System.Drawing.Size(1135, 438);
            this.dgvAppointmentsList.TabIndex = 10;
            this.dgvAppointmentsList.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvAppointmentsList.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvAppointmentsList.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvAppointmentsList.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvAppointmentsList.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvAppointmentsList.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvAppointmentsList.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvAppointmentsList.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvAppointmentsList.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvAppointmentsList.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvAppointmentsList.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvAppointmentsList.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvAppointmentsList.ThemeStyle.HeaderStyle.Height = 4;
            this.dgvAppointmentsList.ThemeStyle.ReadOnly = false;
            this.dgvAppointmentsList.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvAppointmentsList.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvAppointmentsList.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvAppointmentsList.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvAppointmentsList.ThemeStyle.RowsStyle.Height = 22;
            this.dgvAppointmentsList.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvAppointmentsList.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.Location = new System.Drawing.Point(122, 310);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(1046, 23);
            this.guna2Separator1.TabIndex = 11;
            // 
            // lblCount
            // 
            this.lblCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCount.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(40)))), ((int)(((byte)(73)))));
            this.lblCount.Location = new System.Drawing.Point(723, 112);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(17, 18);
            this.lblCount.TabIndex = 12;
            this.lblCount.Text = "00";
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(40)))), ((int)(((byte)(73)))));
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(548, 112);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(175, 19);
            this.guna2HtmlLabel2.TabIndex = 13;
            this.guna2HtmlLabel2.Text = "Number of Appointments :";
            // 
            // btnAddApp
            // 
            this.btnAddApp.BorderRadius = 10;
            this.btnAddApp.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddApp.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddApp.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddApp.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddApp.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddApp.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(40)))), ((int)(((byte)(73)))));
            this.btnAddApp.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(54)))), ((int)(((byte)(106)))));
            this.btnAddApp.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddApp.ForeColor = System.Drawing.Color.White;
            this.btnAddApp.Location = new System.Drawing.Point(923, 251);
            this.btnAddApp.Name = "btnAddApp";
            this.btnAddApp.Size = new System.Drawing.Size(127, 60);
            this.btnAddApp.TabIndex = 14;
            this.btnAddApp.Text = "+ Add";
            this.btnAddApp.Click += new System.EventHandler(this.btnAddApp_Click);
            // 
            // btnShowAppCard
            // 
            this.btnShowAppCard.BorderRadius = 10;
            this.btnShowAppCard.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnShowAppCard.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnShowAppCard.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnShowAppCard.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnShowAppCard.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnShowAppCard.Enabled = false;
            this.btnShowAppCard.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(191)))), ((int)(((byte)(193)))));
            this.btnShowAppCard.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(220)))), ((int)(((byte)(193)))));
            this.btnShowAppCard.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowAppCard.ForeColor = System.Drawing.Color.White;
            this.btnShowAppCard.Location = new System.Drawing.Point(755, 251);
            this.btnShowAppCard.Name = "btnShowAppCard";
            this.btnShowAppCard.Size = new System.Drawing.Size(161, 60);
            this.btnShowAppCard.TabIndex = 15;
            this.btnShowAppCard.Text = "Show / Start";
            this.btnShowAppCard.Click += new System.EventHandler(this.btnShowAppCard_Click);
            // 
            // btnEditApp
            // 
            this.btnEditApp.BorderRadius = 10;
            this.btnEditApp.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnEditApp.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnEditApp.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnEditApp.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnEditApp.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnEditApp.Enabled = false;
            this.btnEditApp.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(54)))), ((int)(((byte)(106)))));
            this.btnEditApp.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(40)))), ((int)(((byte)(73)))));
            this.btnEditApp.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditApp.ForeColor = System.Drawing.Color.White;
            this.btnEditApp.Location = new System.Drawing.Point(1057, 251);
            this.btnEditApp.Name = "btnEditApp";
            this.btnEditApp.Size = new System.Drawing.Size(127, 60);
            this.btnEditApp.TabIndex = 16;
            this.btnEditApp.Text = "Edit";
            this.btnEditApp.Click += new System.EventHandler(this.btnEditApp_Click);
            // 
            // btnDeleteApp
            // 
            this.btnDeleteApp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDeleteApp.BorderRadius = 10;
            this.btnDeleteApp.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDeleteApp.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDeleteApp.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDeleteApp.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDeleteApp.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDeleteApp.Enabled = false;
            this.btnDeleteApp.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(50)))), ((int)(((byte)(88)))));
            this.btnDeleteApp.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnDeleteApp.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteApp.ForeColor = System.Drawing.Color.White;
            this.btnDeleteApp.Location = new System.Drawing.Point(1191, 251);
            this.btnDeleteApp.Name = "btnDeleteApp";
            this.btnDeleteApp.Size = new System.Drawing.Size(127, 60);
            this.btnDeleteApp.TabIndex = 17;
            this.btnDeleteApp.Text = "Delete";
            this.btnDeleteApp.Click += new System.EventHandler(this.btnDeleteApp_Click);
            // 
            // btnCutsom
            // 
            this.btnCutsom.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCutsom.BorderRadius = 10;
            this.btnCutsom.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCutsom.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCutsom.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCutsom.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCutsom.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCutsom.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(118)))), ((int)(((byte)(251)))));
            this.btnCutsom.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(187)))), ((int)(((byte)(254)))));
            this.btnCutsom.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCutsom.ForeColor = System.Drawing.Color.White;
            this.btnCutsom.Location = new System.Drawing.Point(1152, 686);
            this.btnCutsom.Name = "btnCutsom";
            this.btnCutsom.Size = new System.Drawing.Size(168, 90);
            this.btnCutsom.TabIndex = 17;
            this.btnCutsom.Text = "Custom Date";
            this.btnCutsom.Click += new System.EventHandler(this.btnCutsom_Click);
            // 
            // cbFilter
            // 
            this.cbFilter.BackColor = System.Drawing.Color.Transparent;
            this.cbFilter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(191)))), ((int)(((byte)(193)))));
            this.cbFilter.BorderRadius = 10;
            this.cbFilter.CausesValidation = false;
            this.cbFilter.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(191)))), ((int)(((byte)(193)))));
            this.cbFilter.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilter.DropDownWidth = 250;
            this.cbFilter.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbFilter.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbFilter.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.cbFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbFilter.ItemHeight = 30;
            this.cbFilter.Location = new System.Drawing.Point(55, 275);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(193, 36);
            this.cbFilter.TabIndex = 18;
            this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
            // 
            // txtFilter
            // 
            this.txtFilter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(191)))), ((int)(((byte)(193)))));
            this.txtFilter.BorderRadius = 10;
            this.txtFilter.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFilter.DefaultText = "";
            this.txtFilter.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtFilter.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtFilter.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFilter.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFilter.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFilter.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.txtFilter.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFilter.Location = new System.Drawing.Point(255, 275);
            this.txtFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.PlaceholderText = "";
            this.txtFilter.SelectedText = "";
            this.txtFilter.Size = new System.Drawing.Size(368, 36);
            this.txtFilter.TabIndex = 20;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // btnThisDay
            // 
            this.btnThisDay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnThisDay.BorderRadius = 10;
            this.btnThisDay.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThisDay.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThisDay.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThisDay.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThisDay.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThisDay.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(118)))), ((int)(((byte)(251)))));
            this.btnThisDay.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(187)))), ((int)(((byte)(254)))));
            this.btnThisDay.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThisDay.ForeColor = System.Drawing.Color.White;
            this.btnThisDay.Location = new System.Drawing.Point(1152, 389);
            this.btnThisDay.Name = "btnThisDay";
            this.btnThisDay.Size = new System.Drawing.Size(168, 90);
            this.btnThisDay.TabIndex = 17;
            this.btnThisDay.Text = "Today";
            this.btnThisDay.Click += new System.EventHandler(this.btnThisDay_Click);
            // 
            // btnThisWeek
            // 
            this.btnThisWeek.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnThisWeek.BorderRadius = 10;
            this.btnThisWeek.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThisWeek.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThisWeek.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThisWeek.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThisWeek.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThisWeek.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(118)))), ((int)(((byte)(251)))));
            this.btnThisWeek.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(187)))), ((int)(((byte)(254)))));
            this.btnThisWeek.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThisWeek.ForeColor = System.Drawing.Color.White;
            this.btnThisWeek.Location = new System.Drawing.Point(1152, 488);
            this.btnThisWeek.Name = "btnThisWeek";
            this.btnThisWeek.Size = new System.Drawing.Size(168, 90);
            this.btnThisWeek.TabIndex = 17;
            this.btnThisWeek.Text = "This Week";
            this.btnThisWeek.Click += new System.EventHandler(this.btnThisWeek_Click);
            // 
            // btnThisMonth
            // 
            this.btnThisMonth.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnThisMonth.BorderRadius = 10;
            this.btnThisMonth.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThisMonth.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThisMonth.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThisMonth.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThisMonth.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThisMonth.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(118)))), ((int)(((byte)(251)))));
            this.btnThisMonth.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(187)))), ((int)(((byte)(254)))));
            this.btnThisMonth.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThisMonth.ForeColor = System.Drawing.Color.White;
            this.btnThisMonth.Location = new System.Drawing.Point(1152, 587);
            this.btnThisMonth.Name = "btnThisMonth";
            this.btnThisMonth.Size = new System.Drawing.Size(168, 90);
            this.btnThisMonth.TabIndex = 17;
            this.btnThisMonth.Text = "This Month";
            this.btnThisMonth.Click += new System.EventHandler(this.btnThisMonth_Click);
            // 
            // lblDateFilterInfo
            // 
            this.lblDateFilterInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblDateFilterInfo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateFilterInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(50)))), ((int)(((byte)(88)))));
            this.lblDateFilterInfo.Location = new System.Drawing.Point(255, 251);
            this.lblDateFilterInfo.Name = "lblDateFilterInfo";
            this.lblDateFilterInfo.Size = new System.Drawing.Size(96, 18);
            this.lblDateFilterInfo.TabIndex = 21;
            this.lblDateFilterInfo.Text = "Date Filter Info";
            // 
            // btnClearFilter
            // 
            this.btnClearFilter.BorderRadius = 8;
            this.btnClearFilter.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClearFilter.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClearFilter.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClearFilter.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClearFilter.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClearFilter.FillColor = System.Drawing.Color.Gray;
            this.btnClearFilter.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.btnClearFilter.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearFilter.ForeColor = System.Drawing.Color.White;
            this.btnClearFilter.Location = new System.Drawing.Point(55, 228);
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.Size = new System.Drawing.Size(193, 41);
            this.btnClearFilter.TabIndex = 22;
            this.btnClearFilter.Text = "Clear all filters";
            this.btnClearFilter.Click += new System.EventHandler(this.btnClearFilter_Click);
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BorderRadius = 10;
            this.guna2Panel2.Controls.Add(this.guna2HtmlLabel3);
            this.guna2Panel2.FillColor = System.Drawing.Color.White;
            this.guna2Panel2.Location = new System.Drawing.Point(1152, 339);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(168, 44);
            this.guna2Panel2.TabIndex = 3;
            // 
            // guna2HtmlLabel3
            // 
            this.guna2HtmlLabel3.AutoSize = false;
            this.guna2HtmlLabel3.BackColor = System.Drawing.Color.White;
            this.guna2HtmlLabel3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(54)))), ((int)(((byte)(106)))));
            this.guna2HtmlLabel3.Location = new System.Drawing.Point(20, 10);
            this.guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            this.guna2HtmlLabel3.Size = new System.Drawing.Size(127, 29);
            this.guna2HtmlLabel3.TabIndex = 0;
            this.guna2HtmlLabel3.Text = "Date Filters :";
            this.guna2HtmlLabel3.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.BorderRadius = 10;
            this.guna2PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox1.Image")));
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(12, 275);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(37, 36);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 19;
            this.guna2PictureBox1.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(31, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(93, 77);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // guna2Separator2
            // 
            this.guna2Separator2.Location = new System.Drawing.Point(443, 134);
            this.guna2Separator2.Name = "guna2Separator2";
            this.guna2Separator2.Size = new System.Drawing.Size(409, 10);
            this.guna2Separator2.TabIndex = 35;
            // 
            // frmAppointmentsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1332, 789);
            this.Controls.Add(this.guna2Separator2);
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.btnClearFilter);
            this.Controls.Add(this.lblDateFilterInfo);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.guna2PictureBox1);
            this.Controls.Add(this.cbFilter);
            this.Controls.Add(this.btnAddApp);
            this.Controls.Add(this.btnShowAppCard);
            this.Controls.Add(this.btnEditApp);
            this.Controls.Add(this.btnThisMonth);
            this.Controls.Add(this.btnThisWeek);
            this.Controls.Add(this.btnThisDay);
            this.Controls.Add(this.btnCutsom);
            this.Controls.Add(this.btnDeleteApp);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.guna2HtmlLabel2);
            this.Controls.Add(this.guna2Separator1);
            this.Controls.Add(this.dgvAppointmentsList);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.guna2Panel1);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmAppointmentsList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Appointments";
            this.Load += new System.EventHandler(this.frmMainAppointmentsPage_Load);
            this.guna2Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointmentsList)).EndInit();
            this.guna2Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2DataGridView dgvAppointmentsList;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCount;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2GradientButton btnAddApp;
        private Guna.UI2.WinForms.Guna2GradientButton btnShowAppCard;
        private Guna.UI2.WinForms.Guna2GradientButton btnEditApp;
        private Guna.UI2.WinForms.Guna2GradientButton btnDeleteApp;
        private Guna.UI2.WinForms.Guna2GradientButton btnCutsom;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2ComboBox cbFilter;
        private Guna.UI2.WinForms.Guna2TextBox txtFilter;
        private Guna.UI2.WinForms.Guna2GradientButton btnThisDay;
        private Guna.UI2.WinForms.Guna2GradientButton btnThisWeek;
        private Guna.UI2.WinForms.Guna2GradientButton btnThisMonth;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDateFilterInfo;
        private Guna.UI2.WinForms.Guna2GradientButton btnClearFilter;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator2;
    }
}