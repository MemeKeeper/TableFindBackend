
namespace TableFindBackend.Models
{
    partial class CreateReservationForm
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
            this.pnlPanel = new System.Windows.Forms.Panel();
            this.pbxLoading = new System.Windows.Forms.PictureBox();
            this.lblDuration = new System.Windows.Forms.Label();
            this.spnDuration = new System.Windows.Forms.NumericUpDown();
            this.dtpTakenFrom = new System.Windows.Forms.DateTimePicker();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.lblContact = new System.Windows.Forms.Label();
            this.lblTakenFrom = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.tbxContact = new System.Windows.Forms.TextBox();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pnlHeader.SuspendLayout();
            this.pnlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnDuration)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(109)))), ((int)(((byte)(254)))));
            this.pnlHeader.Controls.Add(this.btnExit);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(5);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(369, 46);
            this.pnlHeader.TabIndex = 5;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(109)))), ((int)(((byte)(254)))));
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.SystemColors.Control;
            this.btnExit.Location = new System.Drawing.Point(319, 0);
            this.btnExit.Margin = new System.Windows.Forms.Padding(5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(50, 46);
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
            this.lblTitle.Location = new System.Drawing.Point(5, 15);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(0, 25);
            this.lblTitle.TabIndex = 0;
            // 
            // pnlPanel
            // 
            this.pnlPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPanel.Controls.Add(this.pbxLoading);
            this.pnlPanel.Controls.Add(this.lblDuration);
            this.pnlPanel.Controls.Add(this.spnDuration);
            this.pnlPanel.Controls.Add(this.dtpTakenFrom);
            this.pnlPanel.Controls.Add(this.btnCancel);
            this.pnlPanel.Controls.Add(this.btnCreate);
            this.pnlPanel.Controls.Add(this.lblContact);
            this.pnlPanel.Controls.Add(this.lblTakenFrom);
            this.pnlPanel.Controls.Add(this.lblName);
            this.pnlPanel.Controls.Add(this.tbxContact);
            this.pnlPanel.Controls.Add(this.tbxName);
            this.pnlPanel.Location = new System.Drawing.Point(12, 55);
            this.pnlPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlPanel.Name = "pnlPanel";
            this.pnlPanel.Size = new System.Drawing.Size(346, 339);
            this.pnlPanel.TabIndex = 6;
            // 
            // pbxLoading
            // 
            this.pbxLoading.Image = global::TableFindBackend.Properties.Resources.Cube_1s_200px;
            this.pbxLoading.Location = new System.Drawing.Point(124, 107);
            this.pbxLoading.Name = "pbxLoading";
            this.pbxLoading.Size = new System.Drawing.Size(100, 97);
            this.pbxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxLoading.TabIndex = 14;
            this.pbxLoading.TabStop = false;
            this.pbxLoading.Visible = false;
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(15, 155);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(93, 16);
            this.lblDuration.TabIndex = 13;
            this.lblDuration.Text = "Duration (hours)";
            // 
            // spnDuration
            // 
            this.spnDuration.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spnDuration.Location = new System.Drawing.Point(18, 174);
            this.spnDuration.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.spnDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spnDuration.Name = "spnDuration";
            this.spnDuration.Size = new System.Drawing.Size(63, 27);
            this.spnDuration.TabIndex = 12;
            this.spnDuration.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // dtpTakenFrom
            // 
            this.dtpTakenFrom.CustomFormat = "dddd, dd/MM,      HH:mm";
            this.dtpTakenFrom.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTakenFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTakenFrom.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtpTakenFrom.Location = new System.Drawing.Point(18, 107);
            this.dtpTakenFrom.MinDate = new System.DateTime(2020, 12, 20, 0, 0, 0, 0);
            this.dtpTakenFrom.Name = "dtpTakenFrom";
            this.dtpTakenFrom.Size = new System.Drawing.Size(312, 27);
            this.dtpTakenFrom.TabIndex = 11;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(233, 287);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 47);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.Location = new System.Drawing.Point(18, 287);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(97, 47);
            this.btnCreate.TabIndex = 9;
            this.btnCreate.Text = "Create Reservation";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // lblContact
            // 
            this.lblContact.AutoSize = true;
            this.lblContact.Location = new System.Drawing.Point(15, 221);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(100, 16);
            this.lblContact.TabIndex = 5;
            this.lblContact.Text = "Contact Number";
            // 
            // lblTakenFrom
            // 
            this.lblTakenFrom.AutoSize = true;
            this.lblTakenFrom.Location = new System.Drawing.Point(15, 88);
            this.lblTakenFrom.Name = "lblTakenFrom";
            this.lblTakenFrom.Size = new System.Drawing.Size(66, 16);
            this.lblTakenFrom.TabIndex = 4;
            this.lblTakenFrom.Text = "Taken from";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(15, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(120, 16);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Name for reservation";
            // 
            // tbxContact
            // 
            this.tbxContact.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxContact.Location = new System.Drawing.Point(18, 240);
            this.tbxContact.MaxLength = 10;
            this.tbxContact.Name = "tbxContact";
            this.tbxContact.Size = new System.Drawing.Size(312, 27);
            this.tbxContact.TabIndex = 2;
            this.tbxContact.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxContact_KeyPress);
            // 
            // tbxName
            // 
            this.tbxName.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxName.Location = new System.Drawing.Point(18, 39);
            this.tbxName.MaxLength = 40;
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(312, 27);
            this.tbxName.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // CreateReservationForm
            // 
            this.AcceptButton = this.btnCreate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 408);
            this.Controls.Add(this.pnlPanel);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CreateReservationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CreateReservationForm_FormClosing);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlPanel.ResumeLayout(false);
            this.pnlPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnDuration)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlPanel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label lblContact;
        private System.Windows.Forms.Label lblTakenFrom;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox tbxContact;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.DateTimePicker dtpTakenFrom;
        private System.Windows.Forms.NumericUpDown spnDuration;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.PictureBox pbxLoading;
    }
}