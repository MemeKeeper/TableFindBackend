
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
            this.tbxMenu = new System.Windows.Forms.TextBox();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblRestaurantName = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlPanel = new System.Windows.Forms.Panel();
            this.lblSize = new System.Windows.Forms.Label();
            this.pbxLoading = new System.Windows.Forms.PictureBox();
            this.lblContactNumber = new System.Windows.Forms.Label();
            this.tbxContactNumber = new System.Windows.Forms.TextBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnBrowseLayout = new System.Windows.Forms.Button();
            this.lblLayout = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.ofdMenuBrowse = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDefault = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.ofdLayoutBrowse = new System.Windows.Forms.OpenFileDialog();
            this.pnlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoading)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbxLocation
            // 
            this.tbxLocation.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxLocation.Location = new System.Drawing.Point(17, 109);
            this.tbxLocation.Name = "tbxLocation";
            this.tbxLocation.Size = new System.Drawing.Size(242, 29);
            this.tbxLocation.TabIndex = 1;
            // 
            // tbxMenu
            // 
            this.tbxMenu.Enabled = false;
            this.tbxMenu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxMenu.Location = new System.Drawing.Point(17, 241);
            this.tbxMenu.Name = "tbxMenu";
            this.tbxMenu.Size = new System.Drawing.Size(199, 29);
            this.tbxMenu.TabIndex = 2;
            // 
            // tbxName
            // 
            this.tbxName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxName.Location = new System.Drawing.Point(17, 39);
            this.tbxName.Name = "tbxName";
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
            this.btnCancel.Location = new System.Drawing.Point(166, 15);
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
            this.lblLocation.Location = new System.Drawing.Point(31, 93);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(110, 13);
            this.lblLocation.TabIndex = 7;
            this.lblLocation.Text = "Restaurant Location";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Upload new Menu";
            // 
            // pnlPanel
            // 
            this.pnlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPanel.Controls.Add(this.lblSize);
            this.pnlPanel.Controls.Add(this.pbxLoading);
            this.pnlPanel.Controls.Add(this.lblContactNumber);
            this.pnlPanel.Controls.Add(this.tbxContactNumber);
            this.pnlPanel.Controls.Add(this.btnUpload);
            this.pnlPanel.Controls.Add(this.btnBrowse);
            this.pnlPanel.Controls.Add(this.lblRestaurantName);
            this.pnlPanel.Controls.Add(this.label4);
            this.pnlPanel.Controls.Add(this.lblLocation);
            this.pnlPanel.Controls.Add(this.tbxName);
            this.pnlPanel.Controls.Add(this.tbxLocation);
            this.pnlPanel.Controls.Add(this.tbxMenu);
            this.pnlPanel.Location = new System.Drawing.Point(12, 49);
            this.pnlPanel.Name = "pnlPanel";
            this.pnlPanel.Size = new System.Drawing.Size(281, 318);
            this.pnlPanel.TabIndex = 9;
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(31, 284);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(0, 13);
            this.lblSize.TabIndex = 10;
            // 
            // pbxLoading
            // 
            this.pbxLoading.Image = ((System.Drawing.Image)(resources.GetObject("pbxLoading.Image")));
            this.pbxLoading.Location = new System.Drawing.Point(82, 125);
            this.pbxLoading.Name = "pbxLoading";
            this.pbxLoading.Size = new System.Drawing.Size(113, 113);
            this.pbxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxLoading.TabIndex = 12;
            this.pbxLoading.TabStop = false;
            this.pbxLoading.Visible = false;
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
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(166, 276);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(93, 28);
            this.btnUpload.TabIndex = 11;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(223, 241);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(36, 29);
            this.btnBrowse.TabIndex = 9;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
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
            this.pnlButtons.Location = new System.Drawing.Point(12, 547);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(281, 73);
            this.pnlButtons.TabIndex = 10;
            // 
            // ofdMenuBrowse
            // 
            this.ofdMenuBrowse.FileName = "openFileDialog1";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Location = new System.Drawing.Point(12, 480);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(281, 61);
            this.panel1.TabIndex = 11;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(21, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(240, 33);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "Open System Report Page";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnDefault);
            this.panel2.Controls.Add(this.lblLayout);
            this.panel2.Controls.Add(this.btnBrowseLayout);
            this.panel2.Location = new System.Drawing.Point(12, 381);
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
            this.panel3.Size = new System.Drawing.Size(305, 43);
            this.panel3.TabIndex = 21;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(109)))), ((int)(((byte)(254)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(258, 0);
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
            this.lblTitle.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.Control;
            this.lblTitle.Location = new System.Drawing.Point(3, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(181, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Restaurant Editor";
            // 
            // ofdLayoutBrowse
            // 
            this.ofdLayoutBrowse.FileName = "openFileDialog1";
            // 
            // EditRestaurantForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 626);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditRestaurantForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "-";
            this.pnlPanel.ResumeLayout(false);
            this.pnlPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoading)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox tbxLocation;
        private System.Windows.Forms.TextBox tbxMenu;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblRestaurantName;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlPanel;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.OpenFileDialog ofdMenuBrowse;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Button btnUpload;
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
    }
}