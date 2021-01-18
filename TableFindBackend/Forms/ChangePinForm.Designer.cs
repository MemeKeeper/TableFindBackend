
namespace TableFindBackend.Forms
{
    partial class ChangePinForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangePinForm));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.pbxLoading = new System.Windows.Forms.PictureBox();
            this.lblLoginTitle = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.tbxPassword = new System.Windows.Forms.TextBox();
            this.tbxEmail = new System.Windows.Forms.TextBox();
            this.pnlPin = new System.Windows.Forms.Panel();
            this.pnlDescription = new System.Windows.Forms.Panel();
            this.lblDescription = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblPin2 = new System.Windows.Forms.Label();
            this.lblPinTitle = new System.Windows.Forms.Label();
            this.lblPin1 = new System.Windows.Forms.Label();
            this.tbxPinConfirm = new System.Windows.Forms.TextBox();
            this.tbxPin1 = new System.Windows.Forms.TextBox();
            this.pnlHeader.SuspendLayout();
            this.pnlLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoading)).BeginInit();
            this.pnlPin.SuspendLayout();
            this.pnlDescription.SuspendLayout();
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
            this.pnlHeader.Size = new System.Drawing.Size(610, 43);
            this.pnlHeader.TabIndex = 4;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(109)))), ((int)(((byte)(254)))));
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(567, 0);
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
            this.lblTitle.Size = new System.Drawing.Size(179, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Reset Admin PIN";
            // 
            // pnlLogin
            // 
            this.pnlLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLogin.Controls.Add(this.pbxLoading);
            this.pnlLogin.Controls.Add(this.lblLoginTitle);
            this.pnlLogin.Controls.Add(this.lblPassword);
            this.pnlLogin.Controls.Add(this.lblEmail);
            this.pnlLogin.Controls.Add(this.btnCancel);
            this.pnlLogin.Controls.Add(this.btnConfirm);
            this.pnlLogin.Controls.Add(this.tbxPassword);
            this.pnlLogin.Controls.Add(this.tbxEmail);
            this.pnlLogin.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlLogin.Location = new System.Drawing.Point(14, 51);
            this.pnlLogin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(288, 345);
            this.pnlLogin.TabIndex = 5;
            // 
            // pbxLoading
            // 
            this.pbxLoading.Image = global::TableFindBackend.Properties.Resources.Cube_1s_200px;
            this.pbxLoading.Location = new System.Drawing.Point(84, 164);
            this.pbxLoading.Name = "pbxLoading";
            this.pbxLoading.Size = new System.Drawing.Size(114, 105);
            this.pbxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxLoading.TabIndex = 7;
            this.pbxLoading.TabStop = false;
            this.pbxLoading.Visible = false;
            // 
            // lblLoginTitle
            // 
            this.lblLoginTitle.AutoSize = true;
            this.lblLoginTitle.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginTitle.Location = new System.Drawing.Point(59, 9);
            this.lblLoginTitle.Name = "lblLoginTitle";
            this.lblLoginTitle.Size = new System.Drawing.Size(160, 22);
            this.lblLoginTitle.TabIndex = 6;
            this.lblLoginTitle.Text = "Login To Confirm";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(28, 98);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(69, 17);
            this.lblPassword.TabIndex = 5;
            this.lblPassword.Text = "Password";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(28, 43);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(43, 17);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "Email";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(166, 285);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 48);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Location = new System.Drawing.Point(14, 285);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(106, 48);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // tbxPassword
            // 
            this.tbxPassword.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPassword.Location = new System.Drawing.Point(14, 118);
            this.tbxPassword.Name = "tbxPassword";
            this.tbxPassword.PasswordChar = '•';
            this.tbxPassword.Size = new System.Drawing.Size(258, 27);
            this.tbxPassword.TabIndex = 1;
            // 
            // tbxEmail
            // 
            this.tbxEmail.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxEmail.Location = new System.Drawing.Point(14, 63);
            this.tbxEmail.Name = "tbxEmail";
            this.tbxEmail.Size = new System.Drawing.Size(258, 27);
            this.tbxEmail.TabIndex = 0;
            // 
            // pnlPin
            // 
            this.pnlPin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPin.Controls.Add(this.pnlDescription);
            this.pnlPin.Controls.Add(this.btnSave);
            this.pnlPin.Controls.Add(this.lblPin2);
            this.pnlPin.Controls.Add(this.lblPinTitle);
            this.pnlPin.Controls.Add(this.lblPin1);
            this.pnlPin.Controls.Add(this.tbxPinConfirm);
            this.pnlPin.Controls.Add(this.tbxPin1);
            this.pnlPin.Enabled = false;
            this.pnlPin.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPin.Location = new System.Drawing.Point(308, 51);
            this.pnlPin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlPin.Name = "pnlPin";
            this.pnlPin.Size = new System.Drawing.Size(287, 345);
            this.pnlPin.TabIndex = 6;
            // 
            // pnlDescription
            // 
            this.pnlDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDescription.Controls.Add(this.lblDescription);
            this.pnlDescription.Location = new System.Drawing.Point(15, 34);
            this.pnlDescription.Name = "pnlDescription";
            this.pnlDescription.Size = new System.Drawing.Size(256, 136);
            this.pnlDescription.TabIndex = 12;
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(-1, -1);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(256, 135);
            this.lblDescription.TabIndex = 8;
            this.lblDescription.Text = resources.GetString("lblDescription.Text");
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDescription.UseCompatibleTextRendering = true;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(83, 285);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 48);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblPin2
            // 
            this.lblPin2.AutoSize = true;
            this.lblPin2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPin2.Location = new System.Drawing.Point(62, 232);
            this.lblPin2.Name = "lblPin2";
            this.lblPin2.Size = new System.Drawing.Size(141, 17);
            this.lblPin2.TabIndex = 11;
            this.lblPin2.Text = "Confirm PIN Number";
            // 
            // lblPinTitle
            // 
            this.lblPinTitle.AutoSize = true;
            this.lblPinTitle.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPinTitle.Location = new System.Drawing.Point(81, 9);
            this.lblPinTitle.Name = "lblPinTitle";
            this.lblPinTitle.Size = new System.Drawing.Size(122, 22);
            this.lblPinTitle.TabIndex = 8;
            this.lblPinTitle.Text = "Change PIN";
            // 
            // lblPin1
            // 
            this.lblPin1.AutoSize = true;
            this.lblPin1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPin1.Location = new System.Drawing.Point(62, 182);
            this.lblPin1.Name = "lblPin1";
            this.lblPin1.Size = new System.Drawing.Size(155, 17);
            this.lblPin1.TabIndex = 10;
            this.lblPin1.Text = "Enter New PIN Number";
            this.lblPin1.Click += new System.EventHandler(this.lblPin1_Click);
            // 
            // tbxPinConfirm
            // 
            this.tbxPinConfirm.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPinConfirm.Location = new System.Drawing.Point(61, 252);
            this.tbxPinConfirm.MaxLength = 5;
            this.tbxPinConfirm.Name = "tbxPinConfirm";
            this.tbxPinConfirm.PasswordChar = '•';
            this.tbxPinConfirm.Size = new System.Drawing.Size(169, 27);
            this.tbxPinConfirm.TabIndex = 9;
            this.tbxPinConfirm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxPinConfirm_KeyPress);
            // 
            // tbxPin1
            // 
            this.tbxPin1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPin1.Location = new System.Drawing.Point(61, 202);
            this.tbxPin1.MaxLength = 5;
            this.tbxPin1.Name = "tbxPin1";
            this.tbxPin1.PasswordChar = '•';
            this.tbxPin1.Size = new System.Drawing.Size(169, 27);
            this.tbxPin1.TabIndex = 8;
            this.tbxPin1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxPin1_KeyPress);
            // 
            // ChangePinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 402);
            this.Controls.Add(this.pnlPin);
            this.Controls.Add(this.pnlLogin);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ChangePinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ChangePinForm";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoading)).EndInit();
            this.pnlPin.ResumeLayout(false);
            this.pnlPin.PerformLayout();
            this.pnlDescription.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.Label lblLoginTitle;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.TextBox tbxPassword;
        private System.Windows.Forms.TextBox tbxEmail;
        private System.Windows.Forms.Panel pnlPin;
        private System.Windows.Forms.PictureBox pbxLoading;
        private System.Windows.Forms.Panel pnlDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblPin2;
        private System.Windows.Forms.Label lblPinTitle;
        private System.Windows.Forms.Label lblPin1;
        private System.Windows.Forms.TextBox tbxPinConfirm;
        private System.Windows.Forms.TextBox tbxPin1;
    }
}