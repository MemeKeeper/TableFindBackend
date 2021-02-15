
namespace TableFindBackend.Forms
{
    partial class ChangePasswordForm
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlBackPanel = new System.Windows.Forms.Panel();
            this.pbxLoading = new System.Windows.Forms.PictureBox();
            this.lblPassHint = new System.Windows.Forms.Label();
            this.lblNew2 = new System.Windows.Forms.Label();
            this.lblNew1 = new System.Windows.Forms.Label();
            this.tbxNewPassConfirm = new System.Windows.Forms.TextBox();
            this.tbxPassNew = new System.Windows.Forms.TextBox();
            this.lblLoginTitle = new System.Windows.Forms.Label();
            this.lblPasswordOld = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.tbxPassword = new System.Windows.Forms.TextBox();
            this.tbxEmail = new System.Windows.Forms.TextBox();
            this.panel3.SuspendLayout();
            this.pnlBackPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Controls.Add(this.lblTitle);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(314, 43);
            this.panel3.TabIndex = 23;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.SystemColors.Control;
            this.btnClose.Location = new System.Drawing.Point(267, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(47, 43);
            this.btnClose.TabIndex = 6;
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
            this.lblTitle.Size = new System.Drawing.Size(220, 23);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Change User Password";
            // 
            // pnlBackPanel
            // 
            this.pnlBackPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBackPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBackPanel.Controls.Add(this.pbxLoading);
            this.pnlBackPanel.Controls.Add(this.lblPassHint);
            this.pnlBackPanel.Controls.Add(this.lblNew2);
            this.pnlBackPanel.Controls.Add(this.lblNew1);
            this.pnlBackPanel.Controls.Add(this.tbxNewPassConfirm);
            this.pnlBackPanel.Controls.Add(this.tbxPassNew);
            this.pnlBackPanel.Controls.Add(this.lblLoginTitle);
            this.pnlBackPanel.Controls.Add(this.lblPasswordOld);
            this.pnlBackPanel.Controls.Add(this.lblEmail);
            this.pnlBackPanel.Controls.Add(this.btnCancel);
            this.pnlBackPanel.Controls.Add(this.btnConfirm);
            this.pnlBackPanel.Controls.Add(this.tbxPassword);
            this.pnlBackPanel.Controls.Add(this.tbxEmail);
            this.pnlBackPanel.Location = new System.Drawing.Point(7, 49);
            this.pnlBackPanel.Name = "pnlBackPanel";
            this.pnlBackPanel.Size = new System.Drawing.Size(300, 427);
            this.pnlBackPanel.TabIndex = 24;
            // 
            // pbxLoading
            // 
            this.pbxLoading.Image = global::TableFindBackend.Properties.Resources.Cube_1s_200px;
            this.pbxLoading.Location = new System.Drawing.Point(75, 128);
            this.pbxLoading.Name = "pbxLoading";
            this.pbxLoading.Size = new System.Drawing.Size(140, 130);
            this.pbxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxLoading.TabIndex = 18;
            this.pbxLoading.TabStop = false;
            this.pbxLoading.Visible = false;
            // 
            // lblPassHint
            // 
            this.lblPassHint.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassHint.Location = new System.Drawing.Point(14, 276);
            this.lblPassHint.Name = "lblPassHint";
            this.lblPassHint.Size = new System.Drawing.Size(261, 90);
            this.lblPassHint.TabIndex = 17;
            this.lblPassHint.Text = "Please make sure that your password contains a minimum of eight characters: At le" +
    "ast one uppercase letter, one lowercase letter and one number";
            this.lblPassHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNew2
            // 
            this.lblNew2.AutoSize = true;
            this.lblNew2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNew2.Location = new System.Drawing.Point(30, 227);
            this.lblNew2.Name = "lblNew2";
            this.lblNew2.Size = new System.Drawing.Size(159, 17);
            this.lblNew2.TabIndex = 16;
            this.lblNew2.Text = "Confirm New Password";
            // 
            // lblNew1
            // 
            this.lblNew1.AutoSize = true;
            this.lblNew1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNew1.Location = new System.Drawing.Point(30, 167);
            this.lblNew1.Name = "lblNew1";
            this.lblNew1.Size = new System.Drawing.Size(103, 17);
            this.lblNew1.TabIndex = 15;
            this.lblNew1.Text = "New Password";
            // 
            // tbxNewPassConfirm
            // 
            this.tbxNewPassConfirm.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNewPassConfirm.Location = new System.Drawing.Point(16, 247);
            this.tbxNewPassConfirm.Name = "tbxNewPassConfirm";
            this.tbxNewPassConfirm.PasswordChar = '•';
            this.tbxNewPassConfirm.Size = new System.Drawing.Size(258, 27);
            this.tbxNewPassConfirm.TabIndex = 3;
            // 
            // tbxPassNew
            // 
            this.tbxPassNew.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPassNew.Location = new System.Drawing.Point(16, 187);
            this.tbxPassNew.Name = "tbxPassNew";
            this.tbxPassNew.PasswordChar = '•';
            this.tbxPassNew.Size = new System.Drawing.Size(258, 27);
            this.tbxPassNew.TabIndex = 2;
            // 
            // lblLoginTitle
            // 
            this.lblLoginTitle.AutoSize = true;
            this.lblLoginTitle.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginTitle.Location = new System.Drawing.Point(34, 11);
            this.lblLoginTitle.Name = "lblLoginTitle";
            this.lblLoginTitle.Size = new System.Drawing.Size(221, 22);
            this.lblLoginTitle.TabIndex = 12;
            this.lblLoginTitle.Text = "Change user password";
            // 
            // lblPasswordOld
            // 
            this.lblPasswordOld.AutoSize = true;
            this.lblPasswordOld.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPasswordOld.Location = new System.Drawing.Point(30, 99);
            this.lblPasswordOld.Name = "lblPasswordOld";
            this.lblPasswordOld.Size = new System.Drawing.Size(150, 17);
            this.lblPasswordOld.TabIndex = 11;
            this.lblPasswordOld.Text = "Old/Current Password";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(30, 44);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(43, 17);
            this.lblEmail.TabIndex = 10;
            this.lblEmail.Text = "Email";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(168, 368);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 48);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Location = new System.Drawing.Point(16, 368);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(106, 48);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // tbxPassword
            // 
            this.tbxPassword.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPassword.Location = new System.Drawing.Point(16, 119);
            this.tbxPassword.Name = "tbxPassword";
            this.tbxPassword.PasswordChar = '•';
            this.tbxPassword.Size = new System.Drawing.Size(258, 27);
            this.tbxPassword.TabIndex = 1;
            // 
            // tbxEmail
            // 
            this.tbxEmail.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxEmail.Location = new System.Drawing.Point(16, 64);
            this.tbxEmail.Name = "tbxEmail";
            this.tbxEmail.Size = new System.Drawing.Size(258, 27);
            this.tbxEmail.TabIndex = 0;
            // 
            // ChangePasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 482);
            this.Controls.Add(this.pnlBackPanel);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ChangePasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ChangePasswordForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChangePasswordForm_FormClosing);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlBackPanel.ResumeLayout(false);
            this.pnlBackPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlBackPanel;
        private System.Windows.Forms.Label lblPasswordOld;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.TextBox tbxPassword;
        private System.Windows.Forms.TextBox tbxEmail;
        private System.Windows.Forms.Label lblNew2;
        private System.Windows.Forms.Label lblNew1;
        private System.Windows.Forms.TextBox tbxNewPassConfirm;
        private System.Windows.Forms.TextBox tbxPassNew;
        private System.Windows.Forms.Label lblLoginTitle;
        private System.Windows.Forms.Label lblPassHint;
        private System.Windows.Forms.PictureBox pbxLoading;
    }
}