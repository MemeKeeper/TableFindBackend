
namespace TableFindBackend.Output
{
    partial class SystemReportForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemReportForm));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvTables = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.capacityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableInfoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.availableDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.restaurantTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pnl = new System.Windows.Forms.Panel();
            this.tcTables = new System.Windows.Forms.TabControl();
            this.tpTables = new System.Windows.Forms.TabPage();
            this.tpSystemLog = new System.Windows.Forms.TabPage();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.flpAdminLog = new System.Windows.Forms.FlowLayoutPanel();
            this.flpReservationTables = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.pbxLoading = new System.Windows.Forms.PictureBox();
            this.btnPDF = new System.Windows.Forms.Button();
            this.btnWord = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.tcReservations = new System.Windows.Forms.TabControl();
            this.tpCurrentReservations = new System.Windows.Forms.TabPage();
            this.tpPast = new System.Windows.Forms.TabPage();
            this.flpPastReservations = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.restaurantTableBindingSource)).BeginInit();
            this.pnl.SuspendLayout();
            this.tcTables.SuspendLayout();
            this.tpTables.SuspendLayout();
            this.tpSystemLog.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoading)).BeginInit();
            this.tcReservations.SuspendLayout();
            this.tpCurrentReservations.SuspendLayout();
            this.tpPast.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(109)))), ((int)(((byte)(254)))));
            this.pnlHeader.Controls.Add(this.btnExit);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(922, 43);
            this.pnlHeader.TabIndex = 6;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(109)))), ((int)(((byte)(254)))));
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.SystemColors.Control;
            this.btnExit.Location = new System.Drawing.Point(879, 0);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(43, 43);
            this.btnExit.TabIndex = 1;
            this.btnExit.TabStop = false;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.Control;
            this.lblTitle.Location = new System.Drawing.Point(3, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(140, 23);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "System Report";
            // 
            // dgvTables
            // 
            this.dgvTables.AllowUserToAddRows = false;
            this.dgvTables.AllowUserToDeleteRows = false;
            this.dgvTables.AllowUserToOrderColumns = true;
            this.dgvTables.AutoGenerateColumns = false;
            this.dgvTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTables.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.capacityDataGridViewTextBoxColumn,
            this.tableInfoDataGridViewTextBoxColumn,
            this.availableDataGridViewCheckBoxColumn});
            this.dgvTables.DataSource = this.restaurantTableBindingSource;
            this.dgvTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTables.Location = new System.Drawing.Point(3, 3);
            this.dgvTables.Name = "dgvTables";
            this.dgvTables.ReadOnly = true;
            this.dgvTables.Size = new System.Drawing.Size(363, 358);
            this.dgvTables.TabIndex = 7;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 60;
            // 
            // capacityDataGridViewTextBoxColumn
            // 
            this.capacityDataGridViewTextBoxColumn.DataPropertyName = "Capacity";
            this.capacityDataGridViewTextBoxColumn.HeaderText = "Capacity";
            this.capacityDataGridViewTextBoxColumn.Name = "capacityDataGridViewTextBoxColumn";
            this.capacityDataGridViewTextBoxColumn.ReadOnly = true;
            this.capacityDataGridViewTextBoxColumn.Width = 60;
            // 
            // tableInfoDataGridViewTextBoxColumn
            // 
            this.tableInfoDataGridViewTextBoxColumn.DataPropertyName = "TableInfo";
            this.tableInfoDataGridViewTextBoxColumn.HeaderText = "TableInfo";
            this.tableInfoDataGridViewTextBoxColumn.Name = "tableInfoDataGridViewTextBoxColumn";
            this.tableInfoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // availableDataGridViewCheckBoxColumn
            // 
            this.availableDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.availableDataGridViewCheckBoxColumn.DataPropertyName = "Available";
            this.availableDataGridViewCheckBoxColumn.HeaderText = "Available";
            this.availableDataGridViewCheckBoxColumn.Name = "availableDataGridViewCheckBoxColumn";
            this.availableDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // restaurantTableBindingSource
            // 
            this.restaurantTableBindingSource.DataSource = typeof(TableFindBackend.Models.RestaurantTable);
            // 
            // pnl
            // 
            this.pnl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl.Controls.Add(this.tcTables);
            this.pnl.Location = new System.Drawing.Point(8, 51);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(389, 403);
            this.pnl.TabIndex = 8;
            // 
            // tcTables
            // 
            this.tcTables.Controls.Add(this.tpTables);
            this.tcTables.Controls.Add(this.tpSystemLog);
            this.tcTables.Controls.Add(this.tabPage1);
            this.tcTables.Location = new System.Drawing.Point(7, 8);
            this.tcTables.Name = "tcTables";
            this.tcTables.SelectedIndex = 0;
            this.tcTables.Size = new System.Drawing.Size(377, 390);
            this.tcTables.TabIndex = 11;
            // 
            // tpTables
            // 
            this.tpTables.Controls.Add(this.dgvTables);
            this.tpTables.Location = new System.Drawing.Point(4, 22);
            this.tpTables.Name = "tpTables";
            this.tpTables.Padding = new System.Windows.Forms.Padding(3);
            this.tpTables.Size = new System.Drawing.Size(369, 364);
            this.tpTables.TabIndex = 0;
            this.tpTables.Text = "Restaurant Tables";
            this.tpTables.UseVisualStyleBackColor = true;
            // 
            // tpSystemLog
            // 
            this.tpSystemLog.Controls.Add(this.rtbLog);
            this.tpSystemLog.Location = new System.Drawing.Point(4, 22);
            this.tpSystemLog.Name = "tpSystemLog";
            this.tpSystemLog.Padding = new System.Windows.Forms.Padding(3);
            this.tpSystemLog.Size = new System.Drawing.Size(369, 364);
            this.tpSystemLog.TabIndex = 1;
            this.tpSystemLog.Text = "System Log";
            this.tpSystemLog.UseVisualStyleBackColor = true;
            // 
            // rtbLog
            // 
            this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLog.Location = new System.Drawing.Point(3, 3);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtbLog.Size = new System.Drawing.Size(363, 358);
            this.rtbLog.TabIndex = 0;
            this.rtbLog.Text = "";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.flpAdminLog);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(369, 364);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Admin Users";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // flpAdminLog
            // 
            this.flpAdminLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpAdminLog.Location = new System.Drawing.Point(3, 3);
            this.flpAdminLog.Name = "flpAdminLog";
            this.flpAdminLog.Size = new System.Drawing.Size(363, 358);
            this.flpAdminLog.TabIndex = 0;
            // 
            // flpReservationTables
            // 
            this.flpReservationTables.AutoScroll = true;
            this.flpReservationTables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpReservationTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpReservationTables.Location = new System.Drawing.Point(3, 3);
            this.flpReservationTables.Margin = new System.Windows.Forms.Padding(5);
            this.flpReservationTables.Name = "flpReservationTables";
            this.flpReservationTables.Size = new System.Drawing.Size(493, 371);
            this.flpReservationTables.TabIndex = 9;
            // 
            // pnlControls
            // 
            this.pnlControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlControls.Controls.Add(this.btnClose);
            this.pnlControls.Controls.Add(this.pbxLoading);
            this.pnlControls.Controls.Add(this.btnPDF);
            this.pnlControls.Controls.Add(this.btnWord);
            this.pnlControls.Controls.Add(this.btnExcel);
            this.pnlControls.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlControls.Location = new System.Drawing.Point(8, 460);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(902, 61);
            this.pnlControls.TabIndex = 10;
            // 
            // pbxLoading
            // 
            this.pbxLoading.Image = ((System.Drawing.Image)(resources.GetObject("pbxLoading.Image")));
            this.pbxLoading.Location = new System.Drawing.Point(647, -1);
            this.pbxLoading.Name = "pbxLoading";
            this.pbxLoading.Size = new System.Drawing.Size(72, 61);
            this.pbxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxLoading.TabIndex = 3;
            this.pbxLoading.TabStop = false;
            this.pbxLoading.Visible = false;
            // 
            // btnPDF
            // 
            this.btnPDF.BackColor = System.Drawing.Color.OrangeRed;
            this.btnPDF.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPDF.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnPDF.Location = new System.Drawing.Point(427, 5);
            this.btnPDF.Name = "btnPDF";
            this.btnPDF.Size = new System.Drawing.Size(202, 51);
            this.btnPDF.TabIndex = 2;
            this.btnPDF.Text = "Export to Portable Document Format file (PDF)";
            this.btnPDF.UseVisualStyleBackColor = false;
            this.btnPDF.Click += new System.EventHandler(this.btnPDF_Click);
            // 
            // btnWord
            // 
            this.btnWord.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnWord.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWord.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnWord.Location = new System.Drawing.Point(219, 5);
            this.btnWord.Name = "btnWord";
            this.btnWord.Size = new System.Drawing.Size(202, 51);
            this.btnWord.TabIndex = 1;
            this.btnWord.Text = "Export to a Microsoft Word Document";
            this.btnWord.UseVisualStyleBackColor = false;
            this.btnWord.Click += new System.EventHandler(this.btnWord_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnExcel.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnExcel.Location = new System.Drawing.Point(11, 5);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(202, 51);
            this.btnExcel.TabIndex = 0;
            this.btnExcel.Text = "Export to a Microsoft Excel Document";
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // tcReservations
            // 
            this.tcReservations.Controls.Add(this.tpCurrentReservations);
            this.tcReservations.Controls.Add(this.tpPast);
            this.tcReservations.Location = new System.Drawing.Point(403, 51);
            this.tcReservations.Name = "tcReservations";
            this.tcReservations.SelectedIndex = 0;
            this.tcReservations.Size = new System.Drawing.Size(507, 403);
            this.tcReservations.TabIndex = 0;
            // 
            // tpCurrentReservations
            // 
            this.tpCurrentReservations.BackColor = System.Drawing.Color.Silver;
            this.tpCurrentReservations.Controls.Add(this.flpReservationTables);
            this.tpCurrentReservations.Location = new System.Drawing.Point(4, 22);
            this.tpCurrentReservations.Name = "tpCurrentReservations";
            this.tpCurrentReservations.Padding = new System.Windows.Forms.Padding(3);
            this.tpCurrentReservations.Size = new System.Drawing.Size(499, 377);
            this.tpCurrentReservations.TabIndex = 0;
            this.tpCurrentReservations.Text = "Active Reservations";
            // 
            // tpPast
            // 
            this.tpPast.BackColor = System.Drawing.Color.DarkGray;
            this.tpPast.Controls.Add(this.flpPastReservations);
            this.tpPast.Location = new System.Drawing.Point(4, 22);
            this.tpPast.Name = "tpPast";
            this.tpPast.Padding = new System.Windows.Forms.Padding(3);
            this.tpPast.Size = new System.Drawing.Size(499, 377);
            this.tpPast.TabIndex = 1;
            this.tpPast.Text = "Past Reservations";
            // 
            // flpPastReservations
            // 
            this.flpPastReservations.AutoScroll = true;
            this.flpPastReservations.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpPastReservations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpPastReservations.Location = new System.Drawing.Point(3, 3);
            this.flpPastReservations.Margin = new System.Windows.Forms.Padding(5);
            this.flpPastReservations.Name = "flpPastReservations";
            this.flpPastReservations.Size = new System.Drawing.Size(493, 371);
            this.flpPastReservations.TabIndex = 10;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.Control;
            this.btnClose.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnClose.Location = new System.Drawing.Point(739, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(155, 51);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // SystemReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.ClientSize = new System.Drawing.Size(922, 533);
            this.Controls.Add(this.tcReservations);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.pnl);
            this.Controls.Add(this.pnlHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SystemReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SystemReportForm";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.restaurantTableBindingSource)).EndInit();
            this.pnl.ResumeLayout(false);
            this.tcTables.ResumeLayout(false);
            this.tpTables.ResumeLayout(false);
            this.tpSystemLog.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoading)).EndInit();
            this.tcReservations.ResumeLayout(false);
            this.tpCurrentReservations.ResumeLayout(false);
            this.tpPast.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvTables;
        private System.Windows.Forms.BindingSource restaurantTableBindingSource;
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.FlowLayoutPanel flpReservationTables;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.TabControl tcTables;
        private System.Windows.Forms.TabPage tpTables;
        private System.Windows.Forms.TabPage tpSystemLog;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Button btnPDF;
        private System.Windows.Forms.Button btnWord;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.FlowLayoutPanel flpAdminLog;
        private System.Windows.Forms.PictureBox pbxLoading;
        private System.Windows.Forms.TabControl tcReservations;
        private System.Windows.Forms.TabPage tpCurrentReservations;
        private System.Windows.Forms.TabPage tpPast;
        private System.Windows.Forms.FlowLayoutPanel flpPastReservations;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn capacityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tableInfoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn availableDataGridViewCheckBoxColumn;
        private System.Windows.Forms.Button btnClose;
    }
}