﻿
namespace TableFindBackend.Forms
{
    partial class AddEditNewAdminForm
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlNewAdmin = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRemoveAdmin = new System.Windows.Forms.Button();
            this.btnCreateNewAdmin = new System.Windows.Forms.Button();
            this.lblConfirmPinTitle = new System.Windows.Forms.Label();
            this.tbxConfirmPin = new System.Windows.Forms.TextBox();
            this.lblPinCode = new System.Windows.Forms.Label();
            this.tbxPinCode = new System.Windows.Forms.TextBox();
            this.lblContactTitle = new System.Windows.Forms.Label();
            this.tbxContact = new System.Windows.Forms.TextBox();
            this.lblNameTitle = new System.Windows.Forms.Label();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.pnlHeader.SuspendLayout();
            this.pnlNewAdmin.SuspendLayout();
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
            this.pnlHeader.Size = new System.Drawing.Size(496, 43);
            this.pnlHeader.TabIndex = 6;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(109)))), ((int)(((byte)(254)))));
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.SystemColors.Control;
            this.btnExit.Location = new System.Drawing.Point(453, 0);
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
            this.lblTitle.Size = new System.Drawing.Size(206, 23);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Add New Admin User";
            // 
            // pnlNewAdmin
            // 
            this.pnlNewAdmin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNewAdmin.Controls.Add(this.btnCancel);
            this.pnlNewAdmin.Controls.Add(this.btnRemoveAdmin);
            this.pnlNewAdmin.Controls.Add(this.btnCreateNewAdmin);
            this.pnlNewAdmin.Controls.Add(this.lblConfirmPinTitle);
            this.pnlNewAdmin.Controls.Add(this.tbxConfirmPin);
            this.pnlNewAdmin.Controls.Add(this.lblPinCode);
            this.pnlNewAdmin.Controls.Add(this.tbxPinCode);
            this.pnlNewAdmin.Controls.Add(this.lblContactTitle);
            this.pnlNewAdmin.Controls.Add(this.tbxContact);
            this.pnlNewAdmin.Controls.Add(this.lblNameTitle);
            this.pnlNewAdmin.Controls.Add(this.tbxName);
            this.pnlNewAdmin.Location = new System.Drawing.Point(7, 50);
            this.pnlNewAdmin.Name = "pnlNewAdmin";
            this.pnlNewAdmin.Size = new System.Drawing.Size(477, 388);
            this.pnlNewAdmin.TabIndex = 17;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.btnCancel.Location = new System.Drawing.Point(331, 305);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(135, 48);
            this.btnCancel.TabIndex = 27;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnRemoveAdmin
            // 
            this.btnRemoveAdmin.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.btnRemoveAdmin.Location = new System.Drawing.Point(168, 305);
            this.btnRemoveAdmin.Name = "btnRemoveAdmin";
            this.btnRemoveAdmin.Size = new System.Drawing.Size(140, 48);
            this.btnRemoveAdmin.TabIndex = 26;
            this.btnRemoveAdmin.Text = "Remove Admin";
            this.btnRemoveAdmin.UseVisualStyleBackColor = true;
            // 
            // btnCreateNewAdmin
            // 
            this.btnCreateNewAdmin.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.btnCreateNewAdmin.Location = new System.Drawing.Point(10, 305);
            this.btnCreateNewAdmin.Name = "btnCreateNewAdmin";
            this.btnCreateNewAdmin.Size = new System.Drawing.Size(135, 48);
            this.btnCreateNewAdmin.TabIndex = 25;
            this.btnCreateNewAdmin.Text = "Create Admin";
            this.btnCreateNewAdmin.UseVisualStyleBackColor = true;
            this.btnCreateNewAdmin.Click += new System.EventHandler(this.btnCreateNewAdmin_Click);
            // 
            // lblConfirmPinTitle
            // 
            this.lblConfirmPinTitle.AutoSize = true;
            this.lblConfirmPinTitle.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.lblConfirmPinTitle.Location = new System.Drawing.Point(114, 215);
            this.lblConfirmPinTitle.Name = "lblConfirmPinTitle";
            this.lblConfirmPinTitle.Size = new System.Drawing.Size(141, 17);
            this.lblConfirmPinTitle.TabIndex = 24;
            this.lblConfirmPinTitle.Text = "Confirm PIN Number";
            // 
            // tbxConfirmPin
            // 
            this.tbxConfirmPin.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.tbxConfirmPin.Location = new System.Drawing.Point(114, 235);
            this.tbxConfirmPin.MaxLength = 5;
            this.tbxConfirmPin.Name = "tbxConfirmPin";
            this.tbxConfirmPin.PasswordChar = '•';
            this.tbxConfirmPin.Size = new System.Drawing.Size(261, 27);
            this.tbxConfirmPin.TabIndex = 23;
            this.tbxConfirmPin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxPinCode_KeyPress);
            // 
            // lblPinCode
            // 
            this.lblPinCode.AutoSize = true;
            this.lblPinCode.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.lblPinCode.Location = new System.Drawing.Point(115, 150);
            this.lblPinCode.Name = "lblPinCode";
            this.lblPinCode.Size = new System.Drawing.Size(121, 17);
            this.lblPinCode.TabIndex = 22;
            this.lblPinCode.Text = "Enter PIN Number";
            // 
            // tbxPinCode
            // 
            this.tbxPinCode.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.tbxPinCode.Location = new System.Drawing.Point(115, 170);
            this.tbxPinCode.MaxLength = 5;
            this.tbxPinCode.Name = "tbxPinCode";
            this.tbxPinCode.PasswordChar = '•';
            this.tbxPinCode.Size = new System.Drawing.Size(261, 27);
            this.tbxPinCode.TabIndex = 21;
            this.tbxPinCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxPinCode_KeyPress);
            // 
            // lblContactTitle
            // 
            this.lblContactTitle.AutoSize = true;
            this.lblContactTitle.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.lblContactTitle.Location = new System.Drawing.Point(116, 88);
            this.lblContactTitle.Name = "lblContactTitle";
            this.lblContactTitle.Size = new System.Drawing.Size(119, 17);
            this.lblContactTitle.TabIndex = 20;
            this.lblContactTitle.Text = "Contact Number";
            // 
            // tbxContact
            // 
            this.tbxContact.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.tbxContact.Location = new System.Drawing.Point(115, 108);
            this.tbxContact.MaxLength = 10;
            this.tbxContact.Name = "tbxContact";
            this.tbxContact.Size = new System.Drawing.Size(261, 27);
            this.tbxContact.TabIndex = 19;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.AutoSize = true;
            this.lblNameTitle.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.lblNameTitle.Location = new System.Drawing.Point(115, 34);
            this.lblNameTitle.Name = "lblNameTitle";
            this.lblNameTitle.Size = new System.Drawing.Size(48, 17);
            this.lblNameTitle.TabIndex = 18;
            this.lblNameTitle.Text = "Name";
            // 
            // tbxName
            // 
            this.tbxName.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.tbxName.Location = new System.Drawing.Point(115, 54);
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(261, 27);
            this.tbxName.TabIndex = 17;
            // 
            // AddEditNewAdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 450);
            this.Controls.Add(this.pnlNewAdmin);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddEditNewAdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddEditNewAdminForm";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlNewAdmin.ResumeLayout(false);
            this.pnlNewAdmin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlNewAdmin;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRemoveAdmin;
        private System.Windows.Forms.Button btnCreateNewAdmin;
        private System.Windows.Forms.Label lblConfirmPinTitle;
        private System.Windows.Forms.TextBox tbxConfirmPin;
        private System.Windows.Forms.Label lblPinCode;
        private System.Windows.Forms.TextBox tbxPinCode;
        private System.Windows.Forms.Label lblContactTitle;
        private System.Windows.Forms.TextBox tbxContact;
        private System.Windows.Forms.Label lblNameTitle;
        private System.Windows.Forms.TextBox tbxName;
    }
}