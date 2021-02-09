
namespace TableFindBackend.Forms
{
    partial class EditRestaurantForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditRestaurantForm));
            this.tbxLocation = new System.Windows.Forms.TextBox();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblRestaurantName = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.pnlRestaurantDetails = new System.Windows.Forms.Panel();
            this.lblContactNumber = new System.Windows.Forms.Label();
            this.tbxContactNumber = new System.Windows.Forms.TextBox();
            this.pbxLoading = new System.Windows.Forms.PictureBox();
            this.btnBrowseLayout = new System.Windows.Forms.Button();
            this.lblLayout = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDefault = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.ofdLayoutBrowse = new System.Windows.Forms.OpenFileDialog();
            this.pnlRestaurantTimes = new System.Windows.Forms.Panel();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblClosing = new System.Windows.Forms.Label();
            this.lblOpen = new System.Windows.Forms.Label();
            this.lblTimes = new System.Windows.Forms.Label();
            this.dtpClose = new System.Windows.Forms.DateTimePicker();
            this.dtpOpen = new System.Windows.Forms.DateTimePicker();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnDeactivate = new System.Windows.Forms.Button();
            this.lblDangerZone = new System.Windows.Forms.Label();
            this.pnlRestaurantDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoading)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlRestaurantTimes.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbxLocation
            // 
            this.tbxLocation.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxLocation.Location = new System.Drawing.Point(17, 96);
            this.tbxLocation.Multiline = true;
            this.tbxLocation.Name = "tbxLocation";
            this.tbxLocation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxLocation.Size = new System.Drawing.Size(242, 55);
            this.tbxLocation.TabIndex = 1;
            // 
            // tbxName
            // 
            this.tbxName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxName.Location = new System.Drawing.Point(17, 39);
            this.tbxName.Name = "tbxName";
            this.tbxName.ReadOnly = true;
            this.tbxName.Size = new System.Drawing.Size(242, 29);
            this.tbxName.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(18, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 42);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(454, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 42);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblRestaurantName
            // 
            this.lblRestaurantName.AutoSize = true;
            this.lblRestaurantName.Location = new System.Drawing.Point(31, 23);
            this.lblRestaurantName.Name = "lblRestaurantName";
            this.lblRestaurantName.Size = new System.Drawing.Size(95, 13);
            this.lblRestaurantName.TabIndex = 6;
            this.lblRestaurantName.Text = "Restaurant Name";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(31, 80);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(110, 13);
            this.lblLocation.TabIndex = 7;
            this.lblLocation.Text = "Restaurant Location";
            // 
            // pnlRestaurantDetails
            // 
            this.pnlRestaurantDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlRestaurantDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRestaurantDetails.Controls.Add(this.lblContactNumber);
            this.pnlRestaurantDetails.Controls.Add(this.tbxContactNumber);
            this.pnlRestaurantDetails.Controls.Add(this.lblRestaurantName);
            this.pnlRestaurantDetails.Controls.Add(this.lblLocation);
            this.pnlRestaurantDetails.Controls.Add(this.tbxName);
            this.pnlRestaurantDetails.Controls.Add(this.tbxLocation);
            this.pnlRestaurantDetails.Location = new System.Drawing.Point(12, 49);
            this.pnlRestaurantDetails.Name = "pnlRestaurantDetails";
            this.pnlRestaurantDetails.Size = new System.Drawing.Size(281, 222);
            this.pnlRestaurantDetails.TabIndex = 9;
            // 
            // lblContactNumber
            // 
            this.lblContactNumber.AutoSize = true;
            this.lblContactNumber.Location = new System.Drawing.Point(31, 160);
            this.lblContactNumber.Name = "lblContactNumber";
            this.lblContactNumber.Size = new System.Drawing.Size(150, 13);
            this.lblContactNumber.TabIndex = 16;
            this.lblContactNumber.Text = "Restaurant Contact Number";
            // 
            // tbxContactNumber
            // 
            this.tbxContactNumber.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxContactNumber.Location = new System.Drawing.Point(17, 176);
            this.tbxContactNumber.Name = "tbxContactNumber";
            this.tbxContactNumber.Size = new System.Drawing.Size(242, 29);
            this.tbxContactNumber.TabIndex = 15;
            // 
            // pbxLoading
            // 
            this.pbxLoading.Image = ((System.Drawing.Image)(resources.GetObject("pbxLoading.Image")));
            this.pbxLoading.Location = new System.Drawing.Point(239, 199);
            this.pbxLoading.Name = "pbxLoading";
            this.pbxLoading.Size = new System.Drawing.Size(113, 113);
            this.pbxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxLoading.TabIndex = 12;
            this.pbxLoading.TabStop = false;
            this.pbxLoading.Visible = false;
            // 
            // btnBrowseLayout
            // 
            this.btnBrowseLayout.Location = new System.Drawing.Point(218, 14);
            this.btnBrowseLayout.Name = "btnBrowseLayout";
            this.btnBrowseLayout.Size = new System.Drawing.Size(36, 29);
            this.btnBrowseLayout.TabIndex = 19;
            this.btnBrowseLayout.Text = "...";
            this.btnBrowseLayout.UseVisualStyleBackColor = true;
            this.btnBrowseLayout.Click += new System.EventHandler(this.btnBrowseLayout_Click);
            // 
            // lblLayout
            // 
            this.lblLayout.Location = new System.Drawing.Point(29, 14);
            this.lblLayout.Name = "lblLayout";
            this.lblLayout.Size = new System.Drawing.Size(183, 47);
            this.lblLayout.TabIndex = 18;
            this.lblLayout.Text = "Change restaurant layout image";
            // 
            // pnlButtons
            // 
            this.pnlButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlButtons.Controls.Add(this.btnSave);
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Location = new System.Drawing.Point(12, 376);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(568, 73);
            this.pnlButtons.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Location = new System.Drawing.Point(299, 310);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(281, 60);
            this.panel1.TabIndex = 11;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.PaleGreen;
            this.btnPrint.Location = new System.Drawing.Point(19, 10);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(240, 38);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "Open System Report Page";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnDefault);
            this.panel2.Controls.Add(this.lblLayout);
            this.panel2.Controls.Add(this.btnBrowseLayout);
            this.panel2.Location = new System.Drawing.Point(12, 277);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(281, 93);
            this.panel2.TabIndex = 20;
            // 
            // btnDefault
            // 
            this.btnDefault.Enabled = false;
            this.btnDefault.Location = new System.Drawing.Point(88, 48);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(93, 28);
            this.btnDefault.TabIndex = 20;
            this.btnDefault.Text = "Reset to Blank";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(109)))), ((int)(((byte)(254)))));
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Controls.Add(this.lblTitle);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(587, 43);
            this.panel3.TabIndex = 21;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(109)))), ((int)(((byte)(254)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.SystemColors.Control;
            this.btnClose.Location = new System.Drawing.Point(540, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(47, 43);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.Control;
            this.lblTitle.Location = new System.Drawing.Point(3, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(183, 23);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Restaurant Options";
            // 
            // ofdLayoutBrowse
            // 
            this.ofdLayoutBrowse.FileName = "openFileDialog1";
            // 
            // pnlRestaurantTimes
            // 
            this.pnlRestaurantTimes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRestaurantTimes.Controls.Add(this.lblTo);
            this.pnlRestaurantTimes.Controls.Add(this.lblClosing);
            this.pnlRestaurantTimes.Controls.Add(this.lblOpen);
            this.pnlRestaurantTimes.Controls.Add(this.lblTimes);
            this.pnlRestaurantTimes.Controls.Add(this.dtpClose);
            this.pnlRestaurantTimes.Controls.Add(this.dtpOpen);
            this.pnlRestaurantTimes.Location = new System.Drawing.Point(299, 49);
            this.pnlRestaurantTimes.Name = "pnlRestaurantTimes";
            this.pnlRestaurantTimes.Size = new System.Drawing.Size(281, 122);
            this.pnlRestaurantTimes.TabIndex = 21;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(128, 81);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(19, 13);
            this.lblTo.TabIndex = 24;
            this.lblTo.Text = "To";
            // 
            // lblClosing
            // 
            this.lblClosing.AutoSize = true;
            this.lblClosing.Location = new System.Drawing.Point(165, 54);
            this.lblClosing.Name = "lblClosing";
            this.lblClosing.Size = new System.Drawing.Size(73, 13);
            this.lblClosing.TabIndex = 23;
            this.lblClosing.Text = "Closing Time";
            // 
            // lblOpen
            // 
            this.lblOpen.AutoSize = true;
            this.lblOpen.Location = new System.Drawing.Point(29, 54);
            this.lblOpen.Name = "lblOpen";
            this.lblOpen.Size = new System.Drawing.Size(80, 13);
            this.lblOpen.TabIndex = 22;
            this.lblOpen.Text = "Opening Time";
            // 
            // lblTimes
            // 
            this.lblTimes.Location = new System.Drawing.Point(29, 17);
            this.lblTimes.Name = "lblTimes";
            this.lblTimes.Size = new System.Drawing.Size(225, 21);
            this.lblTimes.TabIndex = 21;
            this.lblTimes.Text = "Change Restaurant Open and Close Times";
            // 
            // dtpClose
            // 
            this.dtpClose.CustomFormat = "HH:mm";
            this.dtpClose.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpClose.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpClose.Location = new System.Drawing.Point(174, 74);
            this.dtpClose.Name = "dtpClose";
            this.dtpClose.ShowUpDown = true;
            this.dtpClose.Size = new System.Drawing.Size(65, 27);
            this.dtpClose.TabIndex = 1;
            this.dtpClose.ValueChanged += new System.EventHandler(this.dtpClose_ValueChanged);
            // 
            // dtpOpen
            // 
            this.dtpOpen.CustomFormat = "HH:mm";
            this.dtpOpen.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpOpen.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOpen.Location = new System.Drawing.Point(39, 74);
            this.dtpOpen.Name = "dtpOpen";
            this.dtpOpen.ShowUpDown = true;
            this.dtpOpen.Size = new System.Drawing.Size(65, 27);
            this.dtpOpen.TabIndex = 0;
            this.dtpOpen.Value = new System.DateTime(2021, 1, 29, 15, 37, 27, 0);
            this.dtpOpen.ValueChanged += new System.EventHandler(this.dtpOpen_ValueChanged);
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.btnDeactivate);
            this.panel5.Controls.Add(this.lblDangerZone);
            this.panel5.Location = new System.Drawing.Point(299, 177);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(281, 126);
            this.panel5.TabIndex = 25;
            this.panel5.Paint += new System.Windows.Forms.PaintEventHandler(this.panel5_Paint);
            // 
            // btnDeactivate
            // 
            this.btnDeactivate.BackColor = System.Drawing.Color.Coral;
            this.btnDeactivate.Location = new System.Drawing.Point(51, 53);
            this.btnDeactivate.Name = "btnDeactivate";
            this.btnDeactivate.Size = new System.Drawing.Size(173, 48);
            this.btnDeactivate.TabIndex = 5;
            this.btnDeactivate.Text = "Deactivate Restaurant";
            this.btnDeactivate.UseVisualStyleBackColor = false;
            this.btnDeactivate.Click += new System.EventHandler(this.btnDeactivate_Click);
            // 
            // lblDangerZone
            // 
            this.lblDangerZone.Location = new System.Drawing.Point(29, 17);
            this.lblDangerZone.Name = "lblDangerZone";
            this.lblDangerZone.Size = new System.Drawing.Size(225, 21);
            this.lblDangerZone.TabIndex = 21;
            this.lblDangerZone.Text = "Danger Zone";
            this.lblDangerZone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EditRestaurantForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 456);
            this.Controls.Add(this.pbxLoading);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.pnlRestaurantTimes);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlRestaurantDetails);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditRestaurantForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "-";
            this.pnlRestaurantDetails.ResumeLayout(false);
            this.pnlRestaurantDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoading)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlRestaurantTimes.ResumeLayout(false);
            this.pnlRestaurantTimes.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox tbxLocation;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblRestaurantName;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Panel pnlRestaurantDetails;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.PictureBox pbxLoading;
        private System.Windows.Forms.Label lblContactNumber;
        private System.Windows.Forms.TextBox tbxContactNumber;
        private System.Windows.Forms.Button btnBrowseLayout;
        private System.Windows.Forms.Label lblLayout;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.OpenFileDialog ofdLayoutBrowse;
        private System.Windows.Forms.Panel pnlRestaurantTimes;
        private System.Windows.Forms.Label lblTimes;
        private System.Windows.Forms.DateTimePicker dtpClose;
        private System.Windows.Forms.DateTimePicker dtpOpen;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblClosing;
        private System.Windows.Forms.Label lblOpen;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblDangerZone;
        private System.Windows.Forms.Button btnDeactivate;
    }
}