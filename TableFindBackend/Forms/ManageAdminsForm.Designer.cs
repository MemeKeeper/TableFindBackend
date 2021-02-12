
namespace TableFindBackend.Forms
{
    partial class ManageAdminsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageAdminsForm));
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
            this.tcAdmins = new System.Windows.Forms.TabControl();
            this.tpActive = new System.Windows.Forms.TabPage();
            this.dgvAdmins = new System.Windows.Forms.DataGridView();
            this.userName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contactNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObjectId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpUnActive = new System.Windows.Forms.TabPage();
            this.dgvUnactive = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlDescription = new System.Windows.Forms.Panel();
            this.lblDescription = new System.Windows.Forms.Label();
            this.btnAddNewAdmin = new System.Windows.Forms.Button();
            this.lblPinTitle = new System.Windows.Forms.Label();
            this.adminPinsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pnlHeader.SuspendLayout();
            this.pnlLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoading)).BeginInit();
            this.pnlPin.SuspendLayout();
            this.tcAdmins.SuspendLayout();
            this.tpActive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdmins)).BeginInit();
            this.tpUnActive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnactive)).BeginInit();
            this.pnlDescription.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adminPinsBindingSource)).BeginInit();
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
            this.pnlHeader.Size = new System.Drawing.Size(636, 43);
            this.pnlHeader.TabIndex = 4;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(109)))), ((int)(((byte)(254)))));
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.SystemColors.Control;
            this.btnExit.Location = new System.Drawing.Point(593, 0);
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
            this.lblTitle.Size = new System.Drawing.Size(237, 23);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Admin PIN Management";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
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
            this.pnlLogin.Size = new System.Drawing.Size(288, 506);
            this.pnlLogin.TabIndex = 5;
            // 
            // pbxLoading
            // 
            this.pbxLoading.Image = global::TableFindBackend.Properties.Resources.Cube_1s_200px;
            this.pbxLoading.Location = new System.Drawing.Point(80, 233);
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
            this.lblPassword.Location = new System.Drawing.Point(28, 110);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(69, 17);
            this.lblPassword.TabIndex = 5;
            this.lblPassword.Text = "Password";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(28, 55);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(43, 17);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "Email";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(166, 439);
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
            this.btnConfirm.Location = new System.Drawing.Point(14, 439);
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
            this.tbxPassword.Location = new System.Drawing.Point(14, 130);
            this.tbxPassword.Name = "tbxPassword";
            this.tbxPassword.PasswordChar = '•';
            this.tbxPassword.Size = new System.Drawing.Size(258, 27);
            this.tbxPassword.TabIndex = 1;
            // 
            // tbxEmail
            // 
            this.tbxEmail.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxEmail.Location = new System.Drawing.Point(14, 75);
            this.tbxEmail.Name = "tbxEmail";
            this.tbxEmail.Size = new System.Drawing.Size(258, 27);
            this.tbxEmail.TabIndex = 0;
            // 
            // pnlPin
            // 
            this.pnlPin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPin.Controls.Add(this.tcAdmins);
            this.pnlPin.Controls.Add(this.pnlDescription);
            this.pnlPin.Controls.Add(this.btnAddNewAdmin);
            this.pnlPin.Controls.Add(this.lblPinTitle);
            this.pnlPin.Enabled = false;
            this.pnlPin.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPin.Location = new System.Drawing.Point(308, 51);
            this.pnlPin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlPin.Name = "pnlPin";
            this.pnlPin.Size = new System.Drawing.Size(316, 506);
            this.pnlPin.TabIndex = 6;
            // 
            // tcAdmins
            // 
            this.tcAdmins.Controls.Add(this.tpActive);
            this.tcAdmins.Controls.Add(this.tpUnActive);
            this.tcAdmins.ItemSize = new System.Drawing.Size(140, 21);
            this.tcAdmins.Location = new System.Drawing.Point(15, 274);
            this.tcAdmins.Name = "tcAdmins";
            this.tcAdmins.SelectedIndex = 0;
            this.tcAdmins.Size = new System.Drawing.Size(284, 160);
            this.tcAdmins.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcAdmins.TabIndex = 13;
            // 
            // tpActive
            // 
            this.tpActive.Controls.Add(this.dgvAdmins);
            this.tpActive.Location = new System.Drawing.Point(4, 25);
            this.tpActive.Name = "tpActive";
            this.tpActive.Padding = new System.Windows.Forms.Padding(3);
            this.tpActive.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tpActive.Size = new System.Drawing.Size(276, 131);
            this.tpActive.TabIndex = 0;
            this.tpActive.Text = "Active Admin Users";
            this.tpActive.UseVisualStyleBackColor = true;
            // 
            // dgvAdmins
            // 
            this.dgvAdmins.AllowUserToAddRows = false;
            this.dgvAdmins.AllowUserToDeleteRows = false;
            this.dgvAdmins.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvAdmins.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvAdmins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdmins.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.userName,
            this.contactNumber,
            this.ObjectId});
            this.dgvAdmins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAdmins.Location = new System.Drawing.Point(3, 3);
            this.dgvAdmins.MultiSelect = false;
            this.dgvAdmins.Name = "dgvAdmins";
            this.dgvAdmins.ReadOnly = true;
            this.dgvAdmins.RowHeadersVisible = false;
            this.dgvAdmins.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAdmins.ShowCellErrors = false;
            this.dgvAdmins.ShowCellToolTips = false;
            this.dgvAdmins.ShowEditingIcon = false;
            this.dgvAdmins.ShowRowErrors = false;
            this.dgvAdmins.Size = new System.Drawing.Size(270, 125);
            this.dgvAdmins.TabIndex = 9;
            this.dgvAdmins.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAdmins_CellDoubleClick);
            // 
            // userName
            // 
            this.userName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.userName.HeaderText = "User Name";
            this.userName.Name = "userName";
            this.userName.ReadOnly = true;
            // 
            // contactNumber
            // 
            this.contactNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.contactNumber.HeaderText = "Contact Number";
            this.contactNumber.Name = "contactNumber";
            this.contactNumber.ReadOnly = true;
            // 
            // ObjectId
            // 
            this.ObjectId.HeaderText = "ObjectId";
            this.ObjectId.Name = "ObjectId";
            this.ObjectId.ReadOnly = true;
            this.ObjectId.Visible = false;
            // 
            // tpUnActive
            // 
            this.tpUnActive.Controls.Add(this.dgvUnactive);
            this.tpUnActive.Location = new System.Drawing.Point(4, 25);
            this.tpUnActive.Name = "tpUnActive";
            this.tpUnActive.Padding = new System.Windows.Forms.Padding(3);
            this.tpUnActive.Size = new System.Drawing.Size(276, 131);
            this.tpUnActive.TabIndex = 1;
            this.tpUnActive.Text = "Unactive Admin Users";
            this.tpUnActive.UseVisualStyleBackColor = true;
            // 
            // dgvUnactive
            // 
            this.dgvUnactive.AllowUserToAddRows = false;
            this.dgvUnactive.AllowUserToDeleteRows = false;
            this.dgvUnactive.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvUnactive.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvUnactive.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUnactive.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dgvUnactive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUnactive.Location = new System.Drawing.Point(3, 3);
            this.dgvUnactive.MultiSelect = false;
            this.dgvUnactive.Name = "dgvUnactive";
            this.dgvUnactive.ReadOnly = true;
            this.dgvUnactive.RowHeadersVisible = false;
            this.dgvUnactive.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUnactive.ShowCellErrors = false;
            this.dgvUnactive.ShowCellToolTips = false;
            this.dgvUnactive.ShowEditingIcon = false;
            this.dgvUnactive.ShowRowErrors = false;
            this.dgvUnactive.Size = new System.Drawing.Size(270, 125);
            this.dgvUnactive.TabIndex = 10;
            this.dgvUnactive.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUnactive_CellDoubleClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "User Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Contact Number";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "ObjectId";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Visible = false;
            // 
            // pnlDescription
            // 
            this.pnlDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDescription.Controls.Add(this.lblDescription);
            this.pnlDescription.Location = new System.Drawing.Point(15, 34);
            this.pnlDescription.Name = "pnlDescription";
            this.pnlDescription.Size = new System.Drawing.Size(280, 234);
            this.pnlDescription.TabIndex = 12;
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(-1, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(280, 232);
            this.lblDescription.TabIndex = 8;
            this.lblDescription.Text = resources.GetString("lblDescription.Text");
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDescription.UseCompatibleTextRendering = true;
            // 
            // btnAddNewAdmin
            // 
            this.btnAddNewAdmin.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNewAdmin.Location = new System.Drawing.Point(85, 440);
            this.btnAddNewAdmin.Name = "btnAddNewAdmin";
            this.btnAddNewAdmin.Size = new System.Drawing.Size(131, 48);
            this.btnAddNewAdmin.TabIndex = 8;
            this.btnAddNewAdmin.Text = "Add New Admin";
            this.btnAddNewAdmin.UseVisualStyleBackColor = true;
            this.btnAddNewAdmin.Click += new System.EventHandler(this.btnAddNewAdmins_Click);
            // 
            // lblPinTitle
            // 
            this.lblPinTitle.AutoSize = true;
            this.lblPinTitle.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPinTitle.Location = new System.Drawing.Point(81, 9);
            this.lblPinTitle.Name = "lblPinTitle";
            this.lblPinTitle.Size = new System.Drawing.Size(162, 22);
            this.lblPinTitle.TabIndex = 8;
            this.lblPinTitle.Text = "Manage Admins";
            // 
            // adminPinsBindingSource
            // 
            this.adminPinsBindingSource.DataSource = typeof(TableFindBackend.Models.AdminPins);
            // 
            // ChangePinForm
            // 
            this.AcceptButton = this.btnConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 570);
            this.Controls.Add(this.pnlPin);
            this.Controls.Add(this.pnlLogin);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ChangePinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ChangePinForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChangePinForm_FormClosing);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoading)).EndInit();
            this.pnlPin.ResumeLayout(false);
            this.pnlPin.PerformLayout();
            this.tcAdmins.ResumeLayout(false);
            this.tpActive.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdmins)).EndInit();
            this.tpUnActive.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnactive)).EndInit();
            this.pnlDescription.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.adminPinsBindingSource)).EndInit();
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
        private System.Windows.Forms.Button btnAddNewAdmin;
        private System.Windows.Forms.Label lblPinTitle;
        private System.Windows.Forms.DataGridView dgvAdmins;
        private System.Windows.Forms.BindingSource adminPinsBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn userName;
        private System.Windows.Forms.DataGridViewTextBoxColumn contactNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObjectId;
        private System.Windows.Forms.TabControl tcAdmins;
        private System.Windows.Forms.TabPage tpActive;
        private System.Windows.Forms.TabPage tpUnActive;
        private System.Windows.Forms.DataGridView dgvUnactive;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}