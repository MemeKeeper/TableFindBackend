﻿namespace TableFindBackend.Forms
{
    partial class EditTableForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditTableForm));
            this.pnlHeading = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnViewDetails = new System.Windows.Forms.Button();
            this.edtName = new System.Windows.Forms.TextBox();
            this.spnSeating = new System.Windows.Forms.NumericUpDown();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblSeatingTitle = new System.Windows.Forms.Label();
            this.lblNameTitle = new System.Windows.Forms.Label();
            this.pnlTableInfo = new System.Windows.Forms.Panel();
            this.pbxLoading = new System.Windows.Forms.PictureBox();
            this.btnDisable = new System.Windows.Forms.Button();
            this.rtbInfo = new System.Windows.Forms.RichTextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblInfoTitle = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.pnlHeading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnSeating)).BeginInit();
            this.pnlTableInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoading)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeading
            // 
            this.pnlHeading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(109)))), ((int)(((byte)(254)))));
            this.pnlHeading.Controls.Add(this.btnClose);
            this.pnlHeading.Controls.Add(this.lblTitle);
            this.pnlHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeading.Location = new System.Drawing.Point(0, 0);
            this.pnlHeading.Name = "pnlHeading";
            this.pnlHeading.Size = new System.Drawing.Size(321, 43);
            this.pnlHeading.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(109)))), ((int)(((byte)(254)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.SystemColors.Control;
            this.btnClose.Location = new System.Drawing.Point(274, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(47, 43);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnX_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.Control;
            this.lblTitle.Location = new System.Drawing.Point(3, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(118, 23);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Table Editor\r\n";
            // 
            // btnViewDetails
            // 
            this.btnViewDetails.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewDetails.Location = new System.Drawing.Point(11, 270);
            this.btnViewDetails.Name = "btnViewDetails";
            this.btnViewDetails.Size = new System.Drawing.Size(281, 50);
            this.btnViewDetails.TabIndex = 3;
            this.btnViewDetails.Text = "Reservations";
            this.btnViewDetails.UseVisualStyleBackColor = true;
            this.btnViewDetails.Click += new System.EventHandler(this.btnViewDetails_Click);
            // 
            // edtName
            // 
            this.edtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edtName.Location = new System.Drawing.Point(11, 25);
            this.edtName.Name = "edtName";
            this.edtName.Size = new System.Drawing.Size(288, 35);
            this.edtName.TabIndex = 0;
            // 
            // spnSeating
            // 
            this.spnSeating.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spnSeating.Location = new System.Drawing.Point(11, 83);
            this.spnSeating.Name = "spnSeating";
            this.spnSeating.Size = new System.Drawing.Size(57, 35);
            this.spnSeating.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(202, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 49);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.YellowGreen;
            this.btnSave.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(12, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(89, 49);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save Changes";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblSeatingTitle
            // 
            this.lblSeatingTitle.AutoSize = true;
            this.lblSeatingTitle.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeatingTitle.Location = new System.Drawing.Point(8, 63);
            this.lblSeatingTitle.Name = "lblSeatingTitle";
            this.lblSeatingTitle.Size = new System.Drawing.Size(123, 17);
            this.lblSeatingTitle.TabIndex = 6;
            this.lblSeatingTitle.Text = "Maximum Seating";
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.AutoSize = true;
            this.lblNameTitle.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameTitle.Location = new System.Drawing.Point(8, 5);
            this.lblNameTitle.Name = "lblNameTitle";
            this.lblNameTitle.Size = new System.Drawing.Size(48, 17);
            this.lblNameTitle.TabIndex = 8;
            this.lblNameTitle.Text = "Name";
            // 
            // pnlTableInfo
            // 
            this.pnlTableInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTableInfo.Controls.Add(this.pbxLoading);
            this.pnlTableInfo.Controls.Add(this.btnDisable);
            this.pnlTableInfo.Controls.Add(this.rtbInfo);
            this.pnlTableInfo.Controls.Add(this.btnDelete);
            this.pnlTableInfo.Controls.Add(this.lblNameTitle);
            this.pnlTableInfo.Controls.Add(this.btnViewDetails);
            this.pnlTableInfo.Controls.Add(this.lblSeatingTitle);
            this.pnlTableInfo.Controls.Add(this.spnSeating);
            this.pnlTableInfo.Controls.Add(this.edtName);
            this.pnlTableInfo.Controls.Add(this.lblInfoTitle);
            this.pnlTableInfo.Location = new System.Drawing.Point(7, 52);
            this.pnlTableInfo.Name = "pnlTableInfo";
            this.pnlTableInfo.Size = new System.Drawing.Size(307, 388);
            this.pnlTableInfo.TabIndex = 9;
            // 
            // pbxLoading
            // 
            this.pbxLoading.Image = ((System.Drawing.Image)(resources.GetObject("pbxLoading.Image")));
            this.pbxLoading.Location = new System.Drawing.Point(92, 159);
            this.pbxLoading.Name = "pbxLoading";
            this.pbxLoading.Size = new System.Drawing.Size(116, 108);
            this.pbxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxLoading.TabIndex = 10;
            this.pbxLoading.TabStop = false;
            this.pbxLoading.Visible = false;
            // 
            // btnDisable
            // 
            this.btnDisable.BackColor = System.Drawing.Color.Yellow;
            this.btnDisable.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisable.Location = new System.Drawing.Point(12, 326);
            this.btnDisable.Name = "btnDisable";
            this.btnDisable.Size = new System.Drawing.Size(184, 52);
            this.btnDisable.TabIndex = 4;
            this.btnDisable.Text = "Make Table Unavailable";
            this.btnDisable.UseVisualStyleBackColor = false;
            this.btnDisable.Click += new System.EventHandler(this.btnDisable_Click);
            // 
            // rtbInfo
            // 
            this.rtbInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbInfo.Location = new System.Drawing.Point(12, 144);
            this.rtbInfo.Name = "rtbInfo";
            this.rtbInfo.Size = new System.Drawing.Size(280, 120);
            this.rtbInfo.TabIndex = 2;
            this.rtbInfo.Text = "";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.OrangeRed;
            this.btnDelete.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(202, 326);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 52);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Remove Table";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblInfoTitle
            // 
            this.lblInfoTitle.AutoSize = true;
            this.lblInfoTitle.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoTitle.Location = new System.Drawing.Point(10, 124);
            this.lblInfoTitle.Name = "lblInfoTitle";
            this.lblInfoTitle.Size = new System.Drawing.Size(118, 17);
            this.lblInfoTitle.TabIndex = 12;
            this.lblInfoTitle.Text = "Table Description";
            // 
            // pnlButtons
            // 
            this.pnlButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnSave);
            this.pnlButtons.Location = new System.Drawing.Point(7, 446);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(307, 75);
            this.pnlButtons.TabIndex = 10;
            // 
            // EditTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 530);
            this.Controls.Add(this.pnlHeading);
            this.Controls.Add(this.pnlTableInfo);
            this.Controls.Add(this.pnlButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EditTableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Table Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditTableForm_FormClosing);
            this.pnlHeading.ResumeLayout(false);
            this.pnlHeading.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnSeating)).EndInit();
            this.pnlTableInfo.ResumeLayout(false);
            this.pnlTableInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoading)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeading;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnViewDetails;
        private System.Windows.Forms.TextBox edtName;
        private System.Windows.Forms.NumericUpDown spnSeating;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblSeatingTitle;
        private System.Windows.Forms.Label lblNameTitle;
        private System.Windows.Forms.Panel pnlTableInfo;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.PictureBox pbxLoading;
        private System.Windows.Forms.Button btnDisable;
        private System.Windows.Forms.RichTextBox rtbInfo;
        private System.Windows.Forms.Label lblInfoTitle;
    }
}