using System.Windows.Forms;

namespace TableFindBackend.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblDoubleClick = new System.Windows.Forms.Label();
            this.lblPnlSize = new System.Windows.Forms.Label();
            this.tbcReservationHolder = new System.Windows.Forms.TabControl();
            this.tpCurrent = new System.Windows.Forms.TabPage();
            this.flpItems = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flpPrevious = new System.Windows.Forms.FlowLayoutPanel();
            this.btnEnableAdmin = new System.Windows.Forms.Button();
            this.pboxLoading = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnChangeLoad = new System.Windows.Forms.Button();
            this.btnViewAll = new System.Windows.Forms.Button();
            this.btnInfo = new System.Windows.Forms.Button();
            this.btnManageAdminUsers = new System.Windows.Forms.Button();
            this.btnEditMenu = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblLogin = new System.Windows.Forms.Label();
            this.btnEditRestaurant = new System.Windows.Forms.Button();
            this.tbxPass = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnReloadAll = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.lblCapacity = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.tbcReservationHolder.SuspendLayout();
            this.tpCurrent.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxLoading)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor;
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlMain.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMain.Location = new System.Drawing.Point(251, 71);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(854, 596);
            this.pnlMain.TabIndex = 1;
            this.pnlMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMain_Paint);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.Control;
            this.lblTitle.Location = new System.Drawing.Point(3, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(187, 23);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TableFind Backend";
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(109)))), ((int)(((byte)(254)))));
            this.pnlHeader.Controls.Add(this.btnExit);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1124, 43);
            this.pnlHeader.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(109)))), ((int)(((byte)(254)))));
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.SystemColors.Control;
            this.btnExit.Location = new System.Drawing.Point(1078, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(46, 43);
            this.btnExit.TabIndex = 1;
            this.btnExit.TabStop = false;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblDoubleClick
            // 
            this.lblDoubleClick.AutoSize = true;
            this.lblDoubleClick.Location = new System.Drawing.Point(248, 49);
            this.lblDoubleClick.Name = "lblDoubleClick";
            this.lblDoubleClick.Size = new System.Drawing.Size(242, 15);
            this.lblDoubleClick.TabIndex = 5;
            this.lblDoubleClick.Text = "Double Click a table to access more options";
            // 
            // lblPnlSize
            // 
            this.lblPnlSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPnlSize.Location = new System.Drawing.Point(691, 51);
            this.lblPnlSize.Name = "lblPnlSize";
            this.lblPnlSize.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblPnlSize.Size = new System.Drawing.Size(414, 13);
            this.lblPnlSize.TabIndex = 6;
            this.lblPnlSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbcReservationHolder
            // 
            this.tbcReservationHolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbcReservationHolder.Controls.Add(this.tpCurrent);
            this.tbcReservationHolder.Controls.Add(this.tabPage2);
            this.tbcReservationHolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tbcReservationHolder.Location = new System.Drawing.Point(12, 49);
            this.tbcReservationHolder.Multiline = true;
            this.tbcReservationHolder.Name = "tbcReservationHolder";
            this.tbcReservationHolder.SelectedIndex = 0;
            this.tbcReservationHolder.Size = new System.Drawing.Size(236, 314);
            this.tbcReservationHolder.TabIndex = 8;
            // 
            // tpCurrent
            // 
            this.tpCurrent.Controls.Add(this.flpItems);
            this.tpCurrent.Location = new System.Drawing.Point(4, 22);
            this.tpCurrent.Name = "tpCurrent";
            this.tpCurrent.Padding = new System.Windows.Forms.Padding(3);
            this.tpCurrent.Size = new System.Drawing.Size(228, 288);
            this.tpCurrent.TabIndex = 0;
            this.tpCurrent.Text = "Current Reservations";
            this.tpCurrent.UseVisualStyleBackColor = true;
            // 
            // flpItems
            // 
            this.flpItems.AutoScroll = true;
            this.flpItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.flpItems.BackgroundImage = global::TableFindBackend.Properties.Resources.Logo;
            this.flpItems.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.flpItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flpItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpItems.Location = new System.Drawing.Point(3, 3);
            this.flpItems.Margin = new System.Windows.Forms.Padding(5);
            this.flpItems.Name = "flpItems";
            this.flpItems.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.flpItems.Size = new System.Drawing.Size(222, 282);
            this.flpItems.TabIndex = 7;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.flpPrevious);
            this.tabPage2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(228, 288);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Previous Reservations";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // flpPrevious
            // 
            this.flpPrevious.AutoScroll = true;
            this.flpPrevious.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.flpPrevious.BackgroundImage = global::TableFindBackend.Properties.Resources.Logo;
            this.flpPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.flpPrevious.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flpPrevious.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpPrevious.Location = new System.Drawing.Point(3, 3);
            this.flpPrevious.Margin = new System.Windows.Forms.Padding(5);
            this.flpPrevious.Name = "flpPrevious";
            this.flpPrevious.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.flpPrevious.Size = new System.Drawing.Size(222, 282);
            this.flpPrevious.TabIndex = 8;
            // 
            // btnEnableAdmin
            // 
            this.btnEnableAdmin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnEnableAdmin.Enabled = false;
            this.btnEnableAdmin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnEnableAdmin.Location = new System.Drawing.Point(59, 212);
            this.btnEnableAdmin.Name = "btnEnableAdmin";
            this.btnEnableAdmin.Size = new System.Drawing.Size(111, 39);
            this.btnEnableAdmin.TabIndex = 7;
            this.btnEnableAdmin.Text = "Enable Admin Mode";
            this.btnEnableAdmin.UseVisualStyleBackColor = false;
            this.btnEnableAdmin.Click += new System.EventHandler(this.btnAdmin_Click);
            // 
            // pboxLoading
            // 
            this.pboxLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pboxLoading.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pboxLoading.Image = ((System.Drawing.Image)(resources.GetObject("pboxLoading.Image")));
            this.pboxLoading.Location = new System.Drawing.Point(251, 71);
            this.pboxLoading.Name = "pboxLoading";
            this.pboxLoading.Size = new System.Drawing.Size(854, 598);
            this.pboxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pboxLoading.TabIndex = 0;
            this.pboxLoading.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.BackgroundImage = global::TableFindBackend.Properties.Resources.Logo;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Controls.Add(this.lblMessage);
            this.panel2.Controls.Add(this.btnChangeLoad);
            this.panel2.Controls.Add(this.btnViewAll);
            this.panel2.Controls.Add(this.btnInfo);
            this.panel2.Controls.Add(this.btnManageAdminUsers);
            this.panel2.Controls.Add(this.btnEnableAdmin);
            this.panel2.Controls.Add(this.btnEditMenu);
            this.panel2.Controls.Add(this.btnLogout);
            this.panel2.Controls.Add(this.lblLogin);
            this.panel2.Controls.Add(this.btnEditRestaurant);
            this.panel2.Controls.Add(this.tbxPass);
            this.panel2.Controls.Add(this.btnUpdate);
            this.panel2.Controls.Add(this.btnReloadAll);
            this.panel2.Controls.Add(this.btnApply);
            this.panel2.Controls.Add(this.btnCreate);
            this.panel2.Location = new System.Drawing.Point(12, 369);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(233, 300);
            this.panel2.TabIndex = 2;
            // 
            // lblMessage
            // 
            this.lblMessage.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblMessage.Location = new System.Drawing.Point(4, 180);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(228, 32);
            this.lblMessage.TabIndex = 16;
            this.lblMessage.Text = "Message Displayed here";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMessage.Visible = false;
            // 
            // btnChangeLoad
            // 
            this.btnChangeLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnChangeLoad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnChangeLoad.Location = new System.Drawing.Point(52, 57);
            this.btnChangeLoad.Name = "btnChangeLoad";
            this.btnChangeLoad.Size = new System.Drawing.Size(124, 43);
            this.btnChangeLoad.TabIndex = 15;
            this.btnChangeLoad.Text = "Change Restaurant Capacity Status";
            this.btnChangeLoad.UseVisualStyleBackColor = false;
            this.btnChangeLoad.Click += new System.EventHandler(this.btnChangeLoad_Click);
            // 
            // btnViewAll
            // 
            this.btnViewAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnViewAll.Enabled = false;
            this.btnViewAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnViewAll.Location = new System.Drawing.Point(59, 8);
            this.btnViewAll.Name = "btnViewAll";
            this.btnViewAll.Size = new System.Drawing.Size(112, 43);
            this.btnViewAll.TabIndex = 14;
            this.btnViewAll.Text = "View All Reservations";
            this.btnViewAll.UseVisualStyleBackColor = false;
            this.btnViewAll.Click += new System.EventHandler(this.btnViewAll_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnInfo.Location = new System.Drawing.Point(4, 69);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(45, 64);
            this.btnInfo.TabIndex = 13;
            this.btnInfo.Text = "How to use?";
            this.btnInfo.UseVisualStyleBackColor = false;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // btnManageAdminUsers
            // 
            this.btnManageAdminUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnManageAdminUsers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnManageAdminUsers.Location = new System.Drawing.Point(58, 253);
            this.btnManageAdminUsers.Name = "btnManageAdminUsers";
            this.btnManageAdminUsers.Size = new System.Drawing.Size(111, 39);
            this.btnManageAdminUsers.TabIndex = 12;
            this.btnManageAdminUsers.Text = "Manage Admin Users";
            this.btnManageAdminUsers.UseVisualStyleBackColor = false;
            this.btnManageAdminUsers.Click += new System.EventHandler(this.btnManageAdminUsers_Click);
            // 
            // btnEditMenu
            // 
            this.btnEditMenu.BackColor = System.Drawing.Color.Coral;
            this.btnEditMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnEditMenu.Location = new System.Drawing.Point(101, 210);
            this.btnEditMenu.Name = "btnEditMenu";
            this.btnEditMenu.Size = new System.Drawing.Size(120, 39);
            this.btnEditMenu.TabIndex = 11;
            this.btnEditMenu.Text = "Menu Items";
            this.btnEditMenu.UseVisualStyleBackColor = false;
            this.btnEditMenu.Visible = false;
            this.btnEditMenu.Click += new System.EventHandler(this.btnEditMenu_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnLogout.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnLogout.Location = new System.Drawing.Point(31, 253);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(1);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(56, 39);
            this.btnLogout.TabIndex = 10;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Visible = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblLogin.Location = new System.Drawing.Point(56, 110);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(116, 30);
            this.lblLogin.TabIndex = 11;
            this.lblLogin.Text = "Enter Admin PIN to\r\naccess Admin Mode";
            this.lblLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnEditRestaurant
            // 
            this.btnEditRestaurant.BackColor = System.Drawing.Color.Coral;
            this.btnEditRestaurant.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnEditRestaurant.Location = new System.Drawing.Point(8, 210);
            this.btnEditRestaurant.Name = "btnEditRestaurant";
            this.btnEditRestaurant.Size = new System.Drawing.Size(87, 39);
            this.btnEditRestaurant.TabIndex = 9;
            this.btnEditRestaurant.Text = "Restaurant Options";
            this.btnEditRestaurant.UseVisualStyleBackColor = false;
            this.btnEditRestaurant.Visible = false;
            this.btnEditRestaurant.Click += new System.EventHandler(this.btnEditRestaurant_Click);
            // 
            // tbxPass
            // 
            this.tbxPass.Enabled = false;
            this.tbxPass.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPass.Location = new System.Drawing.Point(24, 143);
            this.tbxPass.MaxLength = 10;
            this.tbxPass.Name = "tbxPass";
            this.tbxPass.PasswordChar = '*';
            this.tbxPass.Size = new System.Drawing.Size(182, 37);
            this.tbxPass.TabIndex = 4;
            this.tbxPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxPass.TextChanged += new System.EventHandler(this.tbxPass_TextChanged);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnUpdate.Location = new System.Drawing.Point(88, 253);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(102, 39);
            this.btnUpdate.TabIndex = 8;
            this.btnUpdate.Text = "Check for Updates";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Visible = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnReloadAll
            // 
            this.btnReloadAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnReloadAll.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReloadAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnReloadAll.Location = new System.Drawing.Point(180, 88);
            this.btnReloadAll.Margin = new System.Windows.Forms.Padding(1);
            this.btnReloadAll.Name = "btnReloadAll";
            this.btnReloadAll.Size = new System.Drawing.Size(41, 39);
            this.btnReloadAll.TabIndex = 6;
            this.btnReloadAll.Text = "⟳";
            this.btnReloadAll.UseVisualStyleBackColor = false;
            this.btnReloadAll.Click += new System.EventHandler(this.btnReloadAll_Click);
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnApply.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnApply.Location = new System.Drawing.Point(59, 158);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(111, 39);
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "Finish";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Visible = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnCreate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnCreate.Location = new System.Drawing.Point(58, 113);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(111, 39);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "Add new table";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Visible = false;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // lblCapacity
            // 
            this.lblCapacity.AutoSize = true;
            this.lblCapacity.Location = new System.Drawing.Point(505, 49);
            this.lblCapacity.Name = "lblCapacity";
            this.lblCapacity.Size = new System.Drawing.Size(158, 15);
            this.lblCapacity.TabIndex = 9;
            this.lblCapacity.Text = "Restaurant Capacity Status : ";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(660, 49);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 15);
            this.lblStatus.TabIndex = 10;
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnEnableAdmin;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1124, 681);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblCapacity);
            this.Controls.Add(this.tbcReservationHolder);
            this.Controls.Add(this.lblPnlSize);
            this.Controls.Add(this.lblDoubleClick);
            this.Controls.Add(this.pboxLoading);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Screen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tbcReservationHolder.ResumeLayout(false);
            this.tpCurrent.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pboxLoading)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnReloadAll;
        private System.Windows.Forms.Button btnEnableAdmin;
        private System.Windows.Forms.PictureBox pboxLoading;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnEditRestaurant;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.TextBox tbxPass;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Button btnEditMenu;
        private System.Windows.Forms.Button btnManageAdminUsers;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.Button btnViewAll;
        private System.Windows.Forms.Label lblDoubleClick;
        private System.Windows.Forms.Label lblPnlSize;
        private System.Windows.Forms.FlowLayoutPanel flpItems;
        private TabControl tbcReservationHolder;
        private TabPage tpCurrent;
        private TabPage tabPage2;
        private FlowLayoutPanel flpPrevious;
        private Label lblCapacity;
        private Button btnChangeLoad;
        private Label lblStatus;
        private Label lblMessage;
    }
}

