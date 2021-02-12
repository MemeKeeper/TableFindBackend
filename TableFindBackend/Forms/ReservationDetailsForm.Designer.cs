
namespace TableFindBackend.Forms
{
    partial class ReservationDetailsForm
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
            this.pnlUser = new System.Windows.Forms.Panel();
            this.lblMadeByRestaurant = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblContact = new System.Windows.Forms.Label();
            this.lblLName = new System.Windows.Forms.Label();
            this.lblFName = new System.Windows.Forms.Label();
            this.tbxEmail = new System.Windows.Forms.TextBox();
            this.tbxContact = new System.Windows.Forms.TextBox();
            this.tbxLName = new System.Windows.Forms.TextBox();
            this.tbxFName = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlReservation = new System.Windows.Forms.Panel();
            this.tbxTime = new System.Windows.Forms.TextBox();
            this.lblContactNumber = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.tbxContact2 = new System.Windows.Forms.TextBox();
            this.pnlTable = new System.Windows.Forms.Panel();
            this.tbxTable = new System.Windows.Forms.TextBox();
            this.lblCap = new System.Windows.Forms.Label();
            this.lblTable = new System.Windows.Forms.Label();
            this.tbxCapacity = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.pbxLoading = new System.Windows.Forms.PictureBox();
            this.pnlUser.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlReservation.SuspendLayout();
            this.pnlTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlUser
            // 
            this.pnlUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUser.Controls.Add(this.lblMadeByRestaurant);
            this.pnlUser.Controls.Add(this.lblEmail);
            this.pnlUser.Controls.Add(this.lblContact);
            this.pnlUser.Controls.Add(this.lblLName);
            this.pnlUser.Controls.Add(this.lblFName);
            this.pnlUser.Controls.Add(this.tbxEmail);
            this.pnlUser.Controls.Add(this.tbxContact);
            this.pnlUser.Controls.Add(this.tbxLName);
            this.pnlUser.Controls.Add(this.tbxFName);
            this.pnlUser.Location = new System.Drawing.Point(8, 49);
            this.pnlUser.Name = "pnlUser";
            this.pnlUser.Size = new System.Drawing.Size(338, 271);
            this.pnlUser.TabIndex = 8;
            // 
            // lblMadeByRestaurant
            // 
            this.lblMadeByRestaurant.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMadeByRestaurant.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMadeByRestaurant.Location = new System.Drawing.Point(-1, -4);
            this.lblMadeByRestaurant.Name = "lblMadeByRestaurant";
            this.lblMadeByRestaurant.Size = new System.Drawing.Size(338, 274);
            this.lblMadeByRestaurant.TabIndex = 8;
            this.lblMadeByRestaurant.Text = "This Reservation Was Booked By The Restaurant";
            this.lblMadeByRestaurant.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMadeByRestaurant.Visible = false;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(13, 191);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(108, 20);
            this.lblEmail.TabIndex = 7;
            this.lblEmail.Text = "Email Address";
            // 
            // lblContact
            // 
            this.lblContact.AutoSize = true;
            this.lblContact.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContact.Location = new System.Drawing.Point(13, 132);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(132, 20);
            this.lblContact.TabIndex = 6;
            this.lblContact.Text = "Contact Number";
            // 
            // lblLName
            // 
            this.lblLName.AutoSize = true;
            this.lblLName.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLName.Location = new System.Drawing.Point(13, 69);
            this.lblLName.Name = "lblLName";
            this.lblLName.Size = new System.Drawing.Size(85, 20);
            this.lblLName.TabIndex = 5;
            this.lblLName.Text = "Last Name";
            // 
            // lblFName
            // 
            this.lblFName.AutoSize = true;
            this.lblFName.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFName.Location = new System.Drawing.Point(13, 9);
            this.lblFName.Name = "lblFName";
            this.lblFName.Size = new System.Drawing.Size(83, 20);
            this.lblFName.TabIndex = 4;
            this.lblFName.Text = "First Name";
            // 
            // tbxEmail
            // 
            this.tbxEmail.Font = new System.Drawing.Font("Century Gothic", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxEmail.Location = new System.Drawing.Point(17, 214);
            this.tbxEmail.Name = "tbxEmail";
            this.tbxEmail.ReadOnly = true;
            this.tbxEmail.Size = new System.Drawing.Size(301, 28);
            this.tbxEmail.TabIndex = 3;
            // 
            // tbxContact
            // 
            this.tbxContact.Font = new System.Drawing.Font("Century Gothic", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxContact.Location = new System.Drawing.Point(17, 155);
            this.tbxContact.Name = "tbxContact";
            this.tbxContact.ReadOnly = true;
            this.tbxContact.Size = new System.Drawing.Size(301, 28);
            this.tbxContact.TabIndex = 2;
            // 
            // tbxLName
            // 
            this.tbxLName.Font = new System.Drawing.Font("Century Gothic", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxLName.Location = new System.Drawing.Point(17, 92);
            this.tbxLName.Name = "tbxLName";
            this.tbxLName.ReadOnly = true;
            this.tbxLName.Size = new System.Drawing.Size(301, 28);
            this.tbxLName.TabIndex = 1;
            // 
            // tbxFName
            // 
            this.tbxFName.Font = new System.Drawing.Font("Century Gothic", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxFName.Location = new System.Drawing.Point(17, 32);
            this.tbxFName.Name = "tbxFName";
            this.tbxFName.ReadOnly = true;
            this.tbxFName.Size = new System.Drawing.Size(301, 28);
            this.tbxFName.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(471, 335);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 59);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(109)))), ((int)(((byte)(254)))));
            this.pnlHeader.Controls.Add(this.btnExit);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(698, 43);
            this.pnlHeader.TabIndex = 7;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(109)))), ((int)(((byte)(254)))));
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.SystemColors.Control;
            this.btnExit.Location = new System.Drawing.Point(652, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(46, 43);
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
            this.lblTitle.Size = new System.Drawing.Size(0, 23);
            this.lblTitle.TabIndex = 0;
            // 
            // pnlReservation
            // 
            this.pnlReservation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlReservation.Controls.Add(this.tbxTime);
            this.pnlReservation.Controls.Add(this.lblContactNumber);
            this.pnlReservation.Controls.Add(this.lblTime);
            this.pnlReservation.Controls.Add(this.tbxContact2);
            this.pnlReservation.Location = new System.Drawing.Point(352, 49);
            this.pnlReservation.Name = "pnlReservation";
            this.pnlReservation.Size = new System.Drawing.Size(338, 131);
            this.pnlReservation.TabIndex = 9;
            // 
            // tbxTime
            // 
            this.tbxTime.Font = new System.Drawing.Font("Century Gothic", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTime.Location = new System.Drawing.Point(17, 32);
            this.tbxTime.Name = "tbxTime";
            this.tbxTime.ReadOnly = true;
            this.tbxTime.Size = new System.Drawing.Size(301, 28);
            this.tbxTime.TabIndex = 7;
            // 
            // lblContactNumber
            // 
            this.lblContactNumber.AutoSize = true;
            this.lblContactNumber.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactNumber.Location = new System.Drawing.Point(13, 69);
            this.lblContactNumber.Name = "lblContactNumber";
            this.lblContactNumber.Size = new System.Drawing.Size(132, 20);
            this.lblContactNumber.TabIndex = 6;
            this.lblContactNumber.Text = "Contact Number";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(13, 9);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(131, 20);
            this.lblTime.TabIndex = 4;
            this.lblTime.Text = "Reservation Time";
            // 
            // tbxContact2
            // 
            this.tbxContact2.Font = new System.Drawing.Font("Century Gothic", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxContact2.Location = new System.Drawing.Point(17, 92);
            this.tbxContact2.Name = "tbxContact2";
            this.tbxContact2.ReadOnly = true;
            this.tbxContact2.Size = new System.Drawing.Size(301, 28);
            this.tbxContact2.TabIndex = 2;
            // 
            // pnlTable
            // 
            this.pnlTable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTable.Controls.Add(this.tbxTable);
            this.pnlTable.Controls.Add(this.lblCap);
            this.pnlTable.Controls.Add(this.lblTable);
            this.pnlTable.Controls.Add(this.tbxCapacity);
            this.pnlTable.Location = new System.Drawing.Point(352, 186);
            this.pnlTable.Name = "pnlTable";
            this.pnlTable.Size = new System.Drawing.Size(338, 134);
            this.pnlTable.TabIndex = 10;
            // 
            // tbxTable
            // 
            this.tbxTable.Font = new System.Drawing.Font("Century Gothic", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTable.Location = new System.Drawing.Point(17, 32);
            this.tbxTable.Name = "tbxTable";
            this.tbxTable.ReadOnly = true;
            this.tbxTable.Size = new System.Drawing.Size(301, 28);
            this.tbxTable.TabIndex = 7;
            // 
            // lblCap
            // 
            this.lblCap.AutoSize = true;
            this.lblCap.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCap.Location = new System.Drawing.Point(13, 69);
            this.lblCap.Name = "lblCap";
            this.lblCap.Size = new System.Drawing.Size(119, 20);
            this.lblCap.TabIndex = 6;
            this.lblCap.Text = "Table Capacity";
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTable.Location = new System.Drawing.Point(13, 9);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(145, 20);
            this.lblTable.TabIndex = 4;
            this.lblTable.Text = "Reserved for Table";
            // 
            // tbxCapacity
            // 
            this.tbxCapacity.Font = new System.Drawing.Font("Century Gothic", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCapacity.Location = new System.Drawing.Point(17, 92);
            this.tbxCapacity.Name = "tbxCapacity";
            this.tbxCapacity.ReadOnly = true;
            this.tbxCapacity.Size = new System.Drawing.Size(301, 28);
            this.tbxCapacity.TabIndex = 2;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Salmon;
            this.btnDelete.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(572, 335);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(114, 59);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "Remove Reservation";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // pbxLoading
            // 
            this.pbxLoading.Image = global::TableFindBackend.Properties.Resources.Cube_1s_200px;
            this.pbxLoading.Location = new System.Drawing.Point(286, 145);
            this.pbxLoading.Name = "pbxLoading";
            this.pbxLoading.Size = new System.Drawing.Size(120, 111);
            this.pbxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxLoading.TabIndex = 9;
            this.pbxLoading.TabStop = false;
            this.pbxLoading.Visible = false;
            // 
            // ReservationDetailsForm
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 406);
            this.Controls.Add(this.pbxLoading);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.pnlTable);
            this.Controls.Add(this.pnlReservation);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pnlUser);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReservationDetailsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReservationDetailsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReservationDetailsForm_FormClosing);
            this.pnlUser.ResumeLayout(false);
            this.pnlUser.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlReservation.ResumeLayout(false);
            this.pnlReservation.PerformLayout();
            this.pnlTable.ResumeLayout(false);
            this.pnlTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlUser;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblContact;
        private System.Windows.Forms.Label lblLName;
        private System.Windows.Forms.Label lblFName;
        private System.Windows.Forms.TextBox tbxEmail;
        private System.Windows.Forms.TextBox tbxContact;
        private System.Windows.Forms.TextBox tbxLName;
        private System.Windows.Forms.TextBox tbxFName;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlReservation;
        private System.Windows.Forms.Label lblContactNumber;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.TextBox tbxContact2;
        private System.Windows.Forms.TextBox tbxTime;
        private System.Windows.Forms.Panel pnlTable;
        private System.Windows.Forms.TextBox tbxTable;
        private System.Windows.Forms.Label lblCap;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.TextBox tbxCapacity;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblMadeByRestaurant;
        private System.Windows.Forms.PictureBox pbxLoading;
    }
}