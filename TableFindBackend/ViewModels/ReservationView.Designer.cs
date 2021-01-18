
namespace TableFindBackend.ViewModels
{
    partial class ReservationView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblName = new System.Windows.Forms.Label();
            this.lblContact = new System.Windows.Forms.Label();
            this.lblTable = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.pnlContact = new System.Windows.Forms.Panel();
            this.pnlReservation = new System.Windows.Forms.Panel();
            this.pnlContact.SuspendLayout();
            this.pnlReservation.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(3, 9);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(107, 16);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "UserName here";
            // 
            // lblContact
            // 
            this.lblContact.AutoSize = true;
            this.lblContact.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContact.Location = new System.Drawing.Point(3, 35);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(135, 17);
            this.lblContact.TabIndex = 1;
            this.lblContact.Text = "contact number here";
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTable.Location = new System.Drawing.Point(3, 9);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(132, 16);
            this.lblTable.TabIndex = 2;
            this.lblTable.Text = "Reserved for Table:";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(3, 44);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(70, 15);
            this.lblTime.TabIndex = 3;
            this.lblTime.Text = "Time from to";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(3, 29);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(60, 15);
            this.lblDate.TabIndex = 4;
            this.lblDate.Text = "Date here";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Font = new System.Drawing.Font("Century Gothic", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblId.Location = new System.Drawing.Point(3, 71);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(57, 12);
            this.lblId.TabIndex = 5;
            this.lblId.Text = "ObjectIDHere";
            // 
            // pnlContact
            // 
            this.pnlContact.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContact.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContact.Controls.Add(this.lblName);
            this.pnlContact.Controls.Add(this.lblContact);
            this.pnlContact.Location = new System.Drawing.Point(3, 3);
            this.pnlContact.Name = "pnlContact";
            this.pnlContact.Size = new System.Drawing.Size(185, 60);
            this.pnlContact.TabIndex = 6;
            // 
            // pnlReservation
            // 
            this.pnlReservation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlReservation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlReservation.Controls.Add(this.lblTable);
            this.pnlReservation.Controls.Add(this.lblId);
            this.pnlReservation.Controls.Add(this.lblDate);
            this.pnlReservation.Controls.Add(this.lblTime);
            this.pnlReservation.Location = new System.Drawing.Point(3, 69);
            this.pnlReservation.Name = "pnlReservation";
            this.pnlReservation.Size = new System.Drawing.Size(185, 89);
            this.pnlReservation.TabIndex = 7;
            // 
            // ReservationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.pnlReservation);
            this.Controls.Add(this.pnlContact);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ReservationView";
            this.Size = new System.Drawing.Size(195, 166);
            this.pnlContact.ResumeLayout(false);
            this.pnlContact.PerformLayout();
            this.pnlReservation.ResumeLayout(false);
            this.pnlReservation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblContact;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblId;
        public System.Windows.Forms.Panel pnlContact;
        public System.Windows.Forms.Panel pnlReservation;
    }
}
