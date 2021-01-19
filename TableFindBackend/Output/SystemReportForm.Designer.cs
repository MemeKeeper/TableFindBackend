﻿
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvTables = new System.Windows.Forms.DataGridView();
            this.pnl = new System.Windows.Forms.Panel();
            this.tcTables = new System.Windows.Forms.TabControl();
            this.tpTables = new System.Windows.Forms.TabPage();
            this.tpSystemLog = new System.Windows.Forms.TabPage();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.flpReservationTables = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnWord = new System.Windows.Forms.Button();
            this.btnPDF = new System.Windows.Forms.Button();
            this.availableDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.capacityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.restaurantTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTables)).BeginInit();
            this.pnl.SuspendLayout();
            this.tcTables.SuspendLayout();
            this.tpTables.SuspendLayout();
            this.tpSystemLog.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.restaurantTableBindingSource)).BeginInit();
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
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.lblTitle.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.Control;
            this.lblTitle.Location = new System.Drawing.Point(3, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(154, 25);
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
            this.availableDataGridViewCheckBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.capacityDataGridViewTextBoxColumn});
            this.dgvTables.DataSource = this.restaurantTableBindingSource;
            this.dgvTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTables.Location = new System.Drawing.Point(3, 3);
            this.dgvTables.Name = "dgvTables";
            this.dgvTables.ReadOnly = true;
            this.dgvTables.Size = new System.Drawing.Size(363, 358);
            this.dgvTables.TabIndex = 7;
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
            // flpReservationTables
            // 
            this.flpReservationTables.AutoScroll = true;
            this.flpReservationTables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpReservationTables.Location = new System.Drawing.Point(405, 51);
            this.flpReservationTables.Margin = new System.Windows.Forms.Padding(5);
            this.flpReservationTables.Name = "flpReservationTables";
            this.flpReservationTables.Size = new System.Drawing.Size(503, 403);
            this.flpReservationTables.TabIndex = 9;
            // 
            // pnlControls
            // 
            this.pnlControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlControls.Controls.Add(this.btnPDF);
            this.pnlControls.Controls.Add(this.btnWord);
            this.pnlControls.Controls.Add(this.btnExcel);
            this.pnlControls.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlControls.Location = new System.Drawing.Point(8, 460);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(902, 61);
            this.pnlControls.TabIndex = 10;
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
            // 
            // availableDataGridViewCheckBoxColumn
            // 
            this.availableDataGridViewCheckBoxColumn.DataPropertyName = "available";
            this.availableDataGridViewCheckBoxColumn.HeaderText = "Table Active?";
            this.availableDataGridViewCheckBoxColumn.Name = "availableDataGridViewCheckBoxColumn";
            this.availableDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Table Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // capacityDataGridViewTextBoxColumn
            // 
            this.capacityDataGridViewTextBoxColumn.DataPropertyName = "capacity";
            this.capacityDataGridViewTextBoxColumn.HeaderText = "Maximum Seating";
            this.capacityDataGridViewTextBoxColumn.Name = "capacityDataGridViewTextBoxColumn";
            this.capacityDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // restaurantTableBindingSource
            // 
            this.restaurantTableBindingSource.DataSource = typeof(TableFindBackend.Models.RestaurantTable);
            // 
            // SystemReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.ClientSize = new System.Drawing.Size(922, 533);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.flpReservationTables);
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
            this.pnl.ResumeLayout(false);
            this.tcTables.ResumeLayout(false);
            this.tpTables.ResumeLayout(false);
            this.tpSystemLog.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.restaurantTableBindingSource)).EndInit();
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
        private System.Windows.Forms.DataGridViewCheckBoxColumn availableDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn capacityDataGridViewTextBoxColumn;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.TabControl tcTables;
        private System.Windows.Forms.TabPage tpTables;
        private System.Windows.Forms.TabPage tpSystemLog;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Button btnPDF;
        private System.Windows.Forms.Button btnWord;
    }
}