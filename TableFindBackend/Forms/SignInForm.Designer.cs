namespace TableFindBackend.Forms
{
    partial class SignInForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignInForm));
            this.lblX = new System.Windows.Forms.Label();
            this.tbEmail = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lblForgot = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblRegisterTitle = new System.Windows.Forms.Label();
            this.tcLoginRegister = new System.Windows.Forms.TabControl();
            this.tpLogin = new System.Windows.Forms.TabPage();
            this.pbxLoading = new System.Windows.Forms.PictureBox();
            this.lblEnterEmailForRecovery = new System.Windows.Forms.Label();
            this.pbxLogo = new System.Windows.Forms.PictureBox();
            this.cbxStaySignedIn = new System.Windows.Forms.CheckBox();
            this.tpRegister = new System.Windows.Forms.TabPage();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlPersonal = new System.Windows.Forms.Panel();
            this.pbxLoadingFS = new System.Windows.Forms.PictureBox();
            this.lblPassHint = new System.Windows.Forms.Label();
            this.tbContactNumber = new System.Windows.Forms.TextBox();
            this.tbConfirmPass = new System.Windows.Forms.TextBox();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.tbFirstName = new System.Windows.Forms.TextBox();
            this.tbLastName = new System.Windows.Forms.TextBox();
            this.tbEmailAddress = new System.Windows.Forms.TextBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.lblX2 = new System.Windows.Forms.Label();
            this.tcLoginRegister.SuspendLayout();
            this.tpLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogo)).BeginInit();
            this.tpRegister.SuspendLayout();
            this.pnlPersonal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoadingFS)).BeginInit();
            this.SuspendLayout();
            // 
            // lblX
            // 
            this.lblX.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(345, 0);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(19, 21);
            this.lblX.TabIndex = 0;
            this.lblX.Text = "X";
            this.lblX.Click += new System.EventHandler(this.lblX_Click);
            // 
            // tbEmail
            // 
            this.tbEmail.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbEmail.Location = new System.Drawing.Point(87, 125);
            this.tbEmail.Name = "tbEmail";
            this.tbEmail.Size = new System.Drawing.Size(187, 29);
            this.tbEmail.TabIndex = 0;
            // 
            // tbPassword
            // 
            this.tbPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPassword.Location = new System.Drawing.Point(87, 174);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '•';
            this.tbPassword.Size = new System.Drawing.Size(187, 29);
            this.tbPassword.TabIndex = 1;
            // 
            // lblForgot
            // 
            this.lblForgot.AutoSize = true;
            this.lblForgot.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForgot.Location = new System.Drawing.Point(94, 301);
            this.lblForgot.Name = "lblForgot";
            this.lblForgot.Size = new System.Drawing.Size(170, 21);
            this.lblForgot.TabIndex = 4;
            this.lblForgot.Text = "Forgot your password?";
            this.lblForgot.Click += new System.EventHandler(this.lblForgot_Click);
            this.lblForgot.MouseLeave += new System.EventHandler(this.lblForgot_MouseLeave);
            this.lblForgot.MouseHover += new System.EventHandler(this.lblForgot_MouseHover);
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Location = new System.Drawing.Point(87, 263);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(187, 35);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblRegisterTitle
            // 
            this.lblRegisterTitle.AutoSize = true;
            this.lblRegisterTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegisterTitle.Location = new System.Drawing.Point(93, 324);
            this.lblRegisterTitle.Name = "lblRegisterTitle";
            this.lblRegisterTitle.Size = new System.Drawing.Size(171, 21);
            this.lblRegisterTitle.TabIndex = 5;
            this.lblRegisterTitle.Text = "Don\'t have an account?";
            this.lblRegisterTitle.Click += new System.EventHandler(this.lblRegisterTitle_Click);
            // 
            // tcLoginRegister
            // 
            this.tcLoginRegister.Controls.Add(this.tpLogin);
            this.tcLoginRegister.Controls.Add(this.tpRegister);
            this.tcLoginRegister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcLoginRegister.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcLoginRegister.ItemSize = new System.Drawing.Size(184, 30);
            this.tcLoginRegister.Location = new System.Drawing.Point(0, 0);
            this.tcLoginRegister.Name = "tcLoginRegister";
            this.tcLoginRegister.SelectedIndex = 0;
            this.tcLoginRegister.Size = new System.Drawing.Size(372, 482);
            this.tcLoginRegister.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcLoginRegister.TabIndex = 10;
            // 
            // tpLogin
            // 
            this.tpLogin.Controls.Add(this.pbxLoading);
            this.tpLogin.Controls.Add(this.lblEnterEmailForRecovery);
            this.tpLogin.Controls.Add(this.pbxLogo);
            this.tpLogin.Controls.Add(this.cbxStaySignedIn);
            this.tpLogin.Controls.Add(this.lblRegisterTitle);
            this.tpLogin.Controls.Add(this.lblX);
            this.tpLogin.Controls.Add(this.btnLogin);
            this.tpLogin.Controls.Add(this.tbEmail);
            this.tpLogin.Controls.Add(this.tbPassword);
            this.tpLogin.Controls.Add(this.lblForgot);
            this.tpLogin.Location = new System.Drawing.Point(4, 34);
            this.tpLogin.Name = "tpLogin";
            this.tpLogin.Padding = new System.Windows.Forms.Padding(3);
            this.tpLogin.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tpLogin.Size = new System.Drawing.Size(364, 444);
            this.tpLogin.TabIndex = 1;
            this.tpLogin.Text = "Login";
            this.tpLogin.UseVisualStyleBackColor = true;
            // 
            // pbxLoading
            // 
            this.pbxLoading.Image = ((System.Drawing.Image)(resources.GetObject("pbxLoading.Image")));
            this.pbxLoading.Location = new System.Drawing.Point(135, 357);
            this.pbxLoading.Name = "pbxLoading";
            this.pbxLoading.Size = new System.Drawing.Size(91, 78);
            this.pbxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxLoading.TabIndex = 3;
            this.pbxLoading.TabStop = false;
            this.pbxLoading.Visible = false;
            // 
            // lblEnterEmailForRecovery
            // 
            this.lblEnterEmailForRecovery.Location = new System.Drawing.Point(142, 343);
            this.lblEnterEmailForRecovery.Name = "lblEnterEmailForRecovery";
            this.lblEnterEmailForRecovery.Size = new System.Drawing.Size(77, 108);
            this.lblEnterEmailForRecovery.TabIndex = 7;
            this.lblEnterEmailForRecovery.Text = "Please enter an Email Address";
            this.lblEnterEmailForRecovery.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEnterEmailForRecovery.Visible = false;
            // 
            // pbxLogo
            // 
            this.pbxLogo.Image = global::TableFindBackend.Properties.Resources.Logo;
            this.pbxLogo.Location = new System.Drawing.Point(130, 22);
            this.pbxLogo.Name = "pbxLogo";
            this.pbxLogo.Size = new System.Drawing.Size(104, 97);
            this.pbxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxLogo.TabIndex = 6;
            this.pbxLogo.TabStop = false;
            // 
            // cbxStaySignedIn
            // 
            this.cbxStaySignedIn.AutoSize = true;
            this.cbxStaySignedIn.Location = new System.Drawing.Point(87, 219);
            this.cbxStaySignedIn.Name = "cbxStaySignedIn";
            this.cbxStaySignedIn.Size = new System.Drawing.Size(156, 25);
            this.cbxStaySignedIn.TabIndex = 2;
            this.cbxStaySignedIn.Text = "Keep me signed in";
            this.cbxStaySignedIn.UseVisualStyleBackColor = true;
            // 
            // tpRegister
            // 
            this.tpRegister.Controls.Add(this.lblTitle);
            this.tpRegister.Controls.Add(this.pnlPersonal);
            this.tpRegister.Controls.Add(this.btnRegister);
            this.tpRegister.Controls.Add(this.lblX2);
            this.tpRegister.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpRegister.Location = new System.Drawing.Point(4, 34);
            this.tpRegister.Name = "tpRegister";
            this.tpRegister.Padding = new System.Windows.Forms.Padding(3);
            this.tpRegister.Size = new System.Drawing.Size(364, 444);
            this.tpRegister.TabIndex = 2;
            this.tpRegister.Text = "Register";
            this.tpRegister.UseVisualStyleBackColor = true;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(22, 19);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(156, 30);
            this.lblTitle.TabIndex = 17;
            this.lblTitle.Text = "Lets get started";
            // 
            // pnlPersonal
            // 
            this.pnlPersonal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPersonal.Controls.Add(this.pbxLoadingFS);
            this.pnlPersonal.Controls.Add(this.lblPassHint);
            this.pnlPersonal.Controls.Add(this.tbContactNumber);
            this.pnlPersonal.Controls.Add(this.tbConfirmPass);
            this.pnlPersonal.Controls.Add(this.tbPass);
            this.pnlPersonal.Controls.Add(this.tbFirstName);
            this.pnlPersonal.Controls.Add(this.tbLastName);
            this.pnlPersonal.Controls.Add(this.tbEmailAddress);
            this.pnlPersonal.Location = new System.Drawing.Point(19, 52);
            this.pnlPersonal.Name = "pnlPersonal";
            this.pnlPersonal.Size = new System.Drawing.Size(326, 338);
            this.pnlPersonal.TabIndex = 16;
            // 
            // pbxLoadingFS
            // 
            this.pbxLoadingFS.Image = ((System.Drawing.Image)(resources.GetObject("pbxLoadingFS.Image")));
            this.pbxLoadingFS.Location = new System.Drawing.Point(97, 204);
            this.pbxLoadingFS.Name = "pbxLoadingFS";
            this.pbxLoadingFS.Size = new System.Drawing.Size(125, 115);
            this.pbxLoadingFS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxLoadingFS.TabIndex = 10;
            this.pbxLoadingFS.TabStop = false;
            this.pbxLoadingFS.Visible = false;
            // 
            // lblPassHint
            // 
            this.lblPassHint.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassHint.Location = new System.Drawing.Point(-1, 192);
            this.lblPassHint.Name = "lblPassHint";
            this.lblPassHint.Size = new System.Drawing.Size(326, 144);
            this.lblPassHint.TabIndex = 11;
            this.lblPassHint.Text = "Please make sure that your password contains a minimum of eight characters: At le" +
    "ast one uppercase letter, one lowercase letter and one number";
            this.lblPassHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbContactNumber
            // 
            this.tbContactNumber.Location = new System.Drawing.Point(10, 21);
            this.tbContactNumber.MaxLength = 10;
            this.tbContactNumber.Name = "tbContactNumber";
            this.tbContactNumber.Size = new System.Drawing.Size(306, 29);
            this.tbContactNumber.TabIndex = 0;
            this.tbContactNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbContactNumber_KeyPress);
            // 
            // tbConfirmPass
            // 
            this.tbConfirmPass.Location = new System.Drawing.Point(163, 160);
            this.tbConfirmPass.Name = "tbConfirmPass";
            this.tbConfirmPass.PasswordChar = '•';
            this.tbConfirmPass.Size = new System.Drawing.Size(153, 29);
            this.tbConfirmPass.TabIndex = 5;
            // 
            // tbPass
            // 
            this.tbPass.Location = new System.Drawing.Point(10, 160);
            this.tbPass.Name = "tbPass";
            this.tbPass.PasswordChar = '•';
            this.tbPass.Size = new System.Drawing.Size(148, 29);
            this.tbPass.TabIndex = 4;
            // 
            // tbFirstName
            // 
            this.tbFirstName.Location = new System.Drawing.Point(10, 71);
            this.tbFirstName.Name = "tbFirstName";
            this.tbFirstName.Size = new System.Drawing.Size(147, 29);
            this.tbFirstName.TabIndex = 1;
            // 
            // tbLastName
            // 
            this.tbLastName.Location = new System.Drawing.Point(163, 71);
            this.tbLastName.Name = "tbLastName";
            this.tbLastName.Size = new System.Drawing.Size(153, 29);
            this.tbLastName.TabIndex = 2;
            // 
            // tbEmailAddress
            // 
            this.tbEmailAddress.Location = new System.Drawing.Point(10, 115);
            this.tbEmailAddress.Name = "tbEmailAddress";
            this.tbEmailAddress.Size = new System.Drawing.Size(306, 29);
            this.tbEmailAddress.TabIndex = 3;
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(117, 396);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(125, 39);
            this.btnRegister.TabIndex = 6;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // lblX2
            // 
            this.lblX2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblX2.AutoSize = true;
            this.lblX2.Location = new System.Drawing.Point(345, 0);
            this.lblX2.Name = "lblX2";
            this.lblX2.Size = new System.Drawing.Size(19, 21);
            this.lblX2.TabIndex = 9;
            this.lblX2.Text = "X";
            this.lblX2.Click += new System.EventHandler(this.label1_Click);
            // 
            // SignInForm
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 482);
            this.Controls.Add(this.tcLoginRegister);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SignInForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TableFindBackend";
            this.tcLoginRegister.ResumeLayout(false);
            this.tpLogin.ResumeLayout(false);
            this.tpLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogo)).EndInit();
            this.tpRegister.ResumeLayout(false);
            this.tpRegister.PerformLayout();
            this.pnlPersonal.ResumeLayout(false);
            this.pnlPersonal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoadingFS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.TextBox tbEmail;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.PictureBox pbxLoading;
        private System.Windows.Forms.Label lblForgot;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblRegisterTitle;
        private System.Windows.Forms.TabControl tcLoginRegister;
        private System.Windows.Forms.TabPage tpLogin;
        private System.Windows.Forms.TabPage tpRegister;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label lblX2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlPersonal;
        private System.Windows.Forms.TextBox tbFirstName;
        private System.Windows.Forms.TextBox tbContactNumber;
        private System.Windows.Forms.TextBox tbLastName;
        private System.Windows.Forms.TextBox tbEmailAddress;
        private System.Windows.Forms.PictureBox pbxLoadingFS;
        private System.Windows.Forms.TextBox tbConfirmPass;
        private System.Windows.Forms.TextBox tbPass;
        private System.Windows.Forms.CheckBox cbxStaySignedIn;
        private System.Windows.Forms.Label lblPassHint;
        private System.Windows.Forms.PictureBox pbxLogo;
        private System.Windows.Forms.Label lblEnterEmailForRecovery;
    }
}