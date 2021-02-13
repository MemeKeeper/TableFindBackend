
namespace TableFindBackend.Forms
{
    partial class InfoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoForm));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblHeading = new System.Windows.Forms.Label();
            this.tclInfo = new System.Windows.Forms.TabControl();
            this.tpStaff = new System.Windows.Forms.TabPage();
            this.rtbStaff = new System.Windows.Forms.RichTextBox();
            this.tpAdmin = new System.Windows.Forms.TabPage();
            this.rtbAdmin = new System.Windows.Forms.RichTextBox();
            this.tpFunctions = new System.Windows.Forms.TabPage();
            this.tcAdminMode = new System.Windows.Forms.TabControl();
            this.tpAllReservations = new System.Windows.Forms.TabPage();
            this.rtcFunctionsPnlAllReservations = new System.Windows.Forms.RichTextBox();
            this.tpCapacityStatus = new System.Windows.Forms.TabPage();
            this.rtbCapacityStatus = new System.Windows.Forms.RichTextBox();
            this.tpOverview = new System.Windows.Forms.TabPage();
            this.rtbOverview = new System.Windows.Forms.RichTextBox();
            this.tpAdminMode = new System.Windows.Forms.TabPage();
            this.rtbEnableAdmin = new System.Windows.Forms.RichTextBox();
            this.tpChangePIN = new System.Windows.Forms.TabPage();
            this.rtbChangePIN = new System.Windows.Forms.RichTextBox();
            this.tpUsingTables = new System.Windows.Forms.TabPage();
            this.rtbUsingTables = new System.Windows.Forms.RichTextBox();
            this.tpElevated = new System.Windows.Forms.TabPage();
            this.tcElevatedUser = new System.Windows.Forms.TabControl();
            this.tpOverviewElevated = new System.Windows.Forms.TabPage();
            this.rtcElevatedOverview = new System.Windows.Forms.RichTextBox();
            this.tpAddNewTable = new System.Windows.Forms.TabPage();
            this.rtbAddNewTable = new System.Windows.Forms.RichTextBox();
            this.tpRestaurantOptions = new System.Windows.Forms.TabPage();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.rtbRestaurantOptions = new System.Windows.Forms.RichTextBox();
            this.tpEditMenu = new System.Windows.Forms.TabPage();
            this.rtbMenuItems = new System.Windows.Forms.RichTextBox();
            this.tpRefresh = new System.Windows.Forms.TabPage();
            this.rtbRefresh = new System.Windows.Forms.RichTextBox();
            this.tpLogout = new System.Windows.Forms.TabPage();
            this.rtbLogout = new System.Windows.Forms.RichTextBox();
            this.tpUpdates = new System.Windows.Forms.TabPage();
            this.rtbUpdates = new System.Windows.Forms.RichTextBox();
            this.tpFinish = new System.Windows.Forms.TabPage();
            this.rtbUpdate = new System.Windows.Forms.RichTextBox();
            this.tpReservations = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rtbGettingStarted = new System.Windows.Forms.RichTextBox();
            this.pnlHeader.SuspendLayout();
            this.tclInfo.SuspendLayout();
            this.tpStaff.SuspendLayout();
            this.tpAdmin.SuspendLayout();
            this.tpFunctions.SuspendLayout();
            this.tcAdminMode.SuspendLayout();
            this.tpAllReservations.SuspendLayout();
            this.tpCapacityStatus.SuspendLayout();
            this.tpOverview.SuspendLayout();
            this.tpAdminMode.SuspendLayout();
            this.tpChangePIN.SuspendLayout();
            this.tpUsingTables.SuspendLayout();
            this.tpElevated.SuspendLayout();
            this.tcElevatedUser.SuspendLayout();
            this.tpOverviewElevated.SuspendLayout();
            this.tpAddNewTable.SuspendLayout();
            this.tpRestaurantOptions.SuspendLayout();
            this.tpEditMenu.SuspendLayout();
            this.tpRefresh.SuspendLayout();
            this.tpLogout.SuspendLayout();
            this.tpUpdates.SuspendLayout();
            this.tpFinish.SuspendLayout();
            this.tpReservations.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(109)))), ((int)(((byte)(254)))));
            this.pnlHeader.Controls.Add(this.btnExit);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(571, 43);
            this.pnlHeader.TabIndex = 5;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(109)))), ((int)(((byte)(254)))));
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.SystemColors.Control;
            this.btnExit.Location = new System.Drawing.Point(525, 0);
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
            this.lblTitle.Size = new System.Drawing.Size(124, 23);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "User Manual";
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.Location = new System.Drawing.Point(12, 45);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(152, 21);
            this.lblHeading.TabIndex = 11;
            this.lblHeading.Text = "Select a Category";
            // 
            // tclInfo
            // 
            this.tclInfo.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tclInfo.Controls.Add(this.tpStaff);
            this.tclInfo.Controls.Add(this.tpAdmin);
            this.tclInfo.Controls.Add(this.tpFunctions);
            this.tclInfo.Controls.Add(this.tpUsingTables);
            this.tclInfo.Controls.Add(this.tpElevated);
            this.tclInfo.Controls.Add(this.tpReservations);
            this.tclInfo.Controls.Add(this.tabPage1);
            this.tclInfo.Font = new System.Drawing.Font("Century Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tclInfo.Location = new System.Drawing.Point(8, 69);
            this.tclInfo.Multiline = true;
            this.tclInfo.Name = "tclInfo";
            this.tclInfo.SelectedIndex = 0;
            this.tclInfo.Size = new System.Drawing.Size(557, 380);
            this.tclInfo.TabIndex = 12;
            // 
            // tpStaff
            // 
            this.tpStaff.Controls.Add(this.rtbStaff);
            this.tpStaff.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpStaff.Location = new System.Drawing.Point(4, 55);
            this.tpStaff.Name = "tpStaff";
            this.tpStaff.Padding = new System.Windows.Forms.Padding(3);
            this.tpStaff.Size = new System.Drawing.Size(549, 321);
            this.tpStaff.TabIndex = 0;
            this.tpStaff.Text = "Waiters / staff";
            this.tpStaff.UseVisualStyleBackColor = true;
            // 
            // rtbStaff
            // 
            this.rtbStaff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbStaff.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbStaff.Location = new System.Drawing.Point(3, 3);
            this.rtbStaff.Name = "rtbStaff";
            this.rtbStaff.ReadOnly = true;
            this.rtbStaff.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbStaff.Size = new System.Drawing.Size(543, 315);
            this.rtbStaff.TabIndex = 7;
            this.rtbStaff.Text = resources.GetString("rtbStaff.Text");
            // 
            // tpAdmin
            // 
            this.tpAdmin.Controls.Add(this.rtbAdmin);
            this.tpAdmin.Location = new System.Drawing.Point(4, 55);
            this.tpAdmin.Name = "tpAdmin";
            this.tpAdmin.Padding = new System.Windows.Forms.Padding(3);
            this.tpAdmin.Size = new System.Drawing.Size(549, 321);
            this.tpAdmin.TabIndex = 1;
            this.tpAdmin.Text = "Admin";
            this.tpAdmin.UseVisualStyleBackColor = true;
            // 
            // rtbAdmin
            // 
            this.rtbAdmin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbAdmin.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbAdmin.Location = new System.Drawing.Point(3, 3);
            this.rtbAdmin.Name = "rtbAdmin";
            this.rtbAdmin.ReadOnly = true;
            this.rtbAdmin.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbAdmin.Size = new System.Drawing.Size(543, 315);
            this.rtbAdmin.TabIndex = 8;
            this.rtbAdmin.Text = resources.GetString("rtbAdmin.Text");
            // 
            // tpFunctions
            // 
            this.tpFunctions.Controls.Add(this.tcAdminMode);
            this.tpFunctions.Location = new System.Drawing.Point(4, 55);
            this.tpFunctions.Name = "tpFunctions";
            this.tpFunctions.Padding = new System.Windows.Forms.Padding(3);
            this.tpFunctions.Size = new System.Drawing.Size(549, 321);
            this.tpFunctions.TabIndex = 2;
            this.tpFunctions.Text = "Functions Panel";
            this.tpFunctions.UseVisualStyleBackColor = true;
            // 
            // tcAdminMode
            // 
            this.tcAdminMode.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tcAdminMode.Controls.Add(this.tpAllReservations);
            this.tcAdminMode.Controls.Add(this.tpCapacityStatus);
            this.tcAdminMode.Controls.Add(this.tpOverview);
            this.tcAdminMode.Controls.Add(this.tpAdminMode);
            this.tcAdminMode.Controls.Add(this.tpChangePIN);
            this.tcAdminMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcAdminMode.Location = new System.Drawing.Point(3, 3);
            this.tcAdminMode.Multiline = true;
            this.tcAdminMode.Name = "tcAdminMode";
            this.tcAdminMode.SelectedIndex = 0;
            this.tcAdminMode.Size = new System.Drawing.Size(543, 315);
            this.tcAdminMode.TabIndex = 0;
            // 
            // tpAllReservations
            // 
            this.tpAllReservations.Controls.Add(this.rtcFunctionsPnlAllReservations);
            this.tpAllReservations.Location = new System.Drawing.Point(4, 4);
            this.tpAllReservations.Name = "tpAllReservations";
            this.tpAllReservations.Padding = new System.Windows.Forms.Padding(3);
            this.tpAllReservations.Size = new System.Drawing.Size(535, 265);
            this.tpAllReservations.TabIndex = 0;
            this.tpAllReservations.Text = "View All Reservations";
            this.tpAllReservations.UseVisualStyleBackColor = true;
            // 
            // rtcFunctionsPnlAllReservations
            // 
            this.rtcFunctionsPnlAllReservations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtcFunctionsPnlAllReservations.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtcFunctionsPnlAllReservations.Location = new System.Drawing.Point(3, 3);
            this.rtcFunctionsPnlAllReservations.Name = "rtcFunctionsPnlAllReservations";
            this.rtcFunctionsPnlAllReservations.ReadOnly = true;
            this.rtcFunctionsPnlAllReservations.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtcFunctionsPnlAllReservations.Size = new System.Drawing.Size(529, 259);
            this.rtcFunctionsPnlAllReservations.TabIndex = 9;
            this.rtcFunctionsPnlAllReservations.Text = resources.GetString("rtcFunctionsPnlAllReservations.Text");
            // 
            // tpCapacityStatus
            // 
            this.tpCapacityStatus.Controls.Add(this.rtbCapacityStatus);
            this.tpCapacityStatus.Location = new System.Drawing.Point(4, 4);
            this.tpCapacityStatus.Name = "tpCapacityStatus";
            this.tpCapacityStatus.Padding = new System.Windows.Forms.Padding(3);
            this.tpCapacityStatus.Size = new System.Drawing.Size(535, 265);
            this.tpCapacityStatus.TabIndex = 1;
            this.tpCapacityStatus.Text = "Change Restaurant Capacity Status";
            this.tpCapacityStatus.UseVisualStyleBackColor = true;
            // 
            // rtbCapacityStatus
            // 
            this.rtbCapacityStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbCapacityStatus.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbCapacityStatus.Location = new System.Drawing.Point(3, 3);
            this.rtbCapacityStatus.Name = "rtbCapacityStatus";
            this.rtbCapacityStatus.ReadOnly = true;
            this.rtbCapacityStatus.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbCapacityStatus.Size = new System.Drawing.Size(529, 259);
            this.rtbCapacityStatus.TabIndex = 10;
            this.rtbCapacityStatus.Text = resources.GetString("rtbCapacityStatus.Text");
            // 
            // tpOverview
            // 
            this.tpOverview.Controls.Add(this.rtbOverview);
            this.tpOverview.Location = new System.Drawing.Point(4, 4);
            this.tpOverview.Name = "tpOverview";
            this.tpOverview.Padding = new System.Windows.Forms.Padding(3);
            this.tpOverview.Size = new System.Drawing.Size(535, 265);
            this.tpOverview.TabIndex = 2;
            this.tpOverview.Text = "Overview";
            this.tpOverview.UseVisualStyleBackColor = true;
            // 
            // rtbOverview
            // 
            this.rtbOverview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbOverview.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbOverview.Location = new System.Drawing.Point(3, 3);
            this.rtbOverview.Name = "rtbOverview";
            this.rtbOverview.ReadOnly = true;
            this.rtbOverview.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbOverview.Size = new System.Drawing.Size(529, 259);
            this.rtbOverview.TabIndex = 9;
            this.rtbOverview.Text = resources.GetString("rtbOverview.Text");
            // 
            // tpAdminMode
            // 
            this.tpAdminMode.Controls.Add(this.rtbEnableAdmin);
            this.tpAdminMode.Location = new System.Drawing.Point(4, 4);
            this.tpAdminMode.Name = "tpAdminMode";
            this.tpAdminMode.Padding = new System.Windows.Forms.Padding(3);
            this.tpAdminMode.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tpAdminMode.Size = new System.Drawing.Size(535, 265);
            this.tpAdminMode.TabIndex = 3;
            this.tpAdminMode.Text = "Enable Admin Mode";
            this.tpAdminMode.UseVisualStyleBackColor = true;
            // 
            // rtbEnableAdmin
            // 
            this.rtbEnableAdmin.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbEnableAdmin.Location = new System.Drawing.Point(3, 3);
            this.rtbEnableAdmin.Name = "rtbEnableAdmin";
            this.rtbEnableAdmin.ReadOnly = true;
            this.rtbEnableAdmin.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbEnableAdmin.Size = new System.Drawing.Size(529, 259);
            this.rtbEnableAdmin.TabIndex = 10;
            this.rtbEnableAdmin.Text = "This allows the user to access more functionality as an Admin user. \n";
            // 
            // tpChangePIN
            // 
            this.tpChangePIN.Controls.Add(this.rtbChangePIN);
            this.tpChangePIN.Location = new System.Drawing.Point(4, 4);
            this.tpChangePIN.Name = "tpChangePIN";
            this.tpChangePIN.Padding = new System.Windows.Forms.Padding(3);
            this.tpChangePIN.Size = new System.Drawing.Size(535, 265);
            this.tpChangePIN.TabIndex = 4;
            this.tpChangePIN.Text = "Manage Admin Users";
            this.tpChangePIN.UseVisualStyleBackColor = true;
            // 
            // rtbChangePIN
            // 
            this.rtbChangePIN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbChangePIN.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbChangePIN.Location = new System.Drawing.Point(3, 3);
            this.rtbChangePIN.Name = "rtbChangePIN";
            this.rtbChangePIN.ReadOnly = true;
            this.rtbChangePIN.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbChangePIN.Size = new System.Drawing.Size(529, 259);
            this.rtbChangePIN.TabIndex = 11;
            this.rtbChangePIN.Text = resources.GetString("rtbChangePIN.Text");
            // 
            // tpUsingTables
            // 
            this.tpUsingTables.Controls.Add(this.rtbUsingTables);
            this.tpUsingTables.Location = new System.Drawing.Point(4, 55);
            this.tpUsingTables.Name = "tpUsingTables";
            this.tpUsingTables.Padding = new System.Windows.Forms.Padding(3);
            this.tpUsingTables.Size = new System.Drawing.Size(549, 321);
            this.tpUsingTables.TabIndex = 3;
            this.tpUsingTables.Text = "Using / Opening Tables";
            this.tpUsingTables.UseVisualStyleBackColor = true;
            // 
            // rtbUsingTables
            // 
            this.rtbUsingTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbUsingTables.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbUsingTables.Location = new System.Drawing.Point(3, 3);
            this.rtbUsingTables.Name = "rtbUsingTables";
            this.rtbUsingTables.ReadOnly = true;
            this.rtbUsingTables.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbUsingTables.Size = new System.Drawing.Size(543, 315);
            this.rtbUsingTables.TabIndex = 9;
            this.rtbUsingTables.Text = resources.GetString("rtbUsingTables.Text");
            // 
            // tpElevated
            // 
            this.tpElevated.Controls.Add(this.tcElevatedUser);
            this.tpElevated.Location = new System.Drawing.Point(4, 55);
            this.tpElevated.Name = "tpElevated";
            this.tpElevated.Padding = new System.Windows.Forms.Padding(3);
            this.tpElevated.Size = new System.Drawing.Size(549, 321);
            this.tpElevated.TabIndex = 4;
            this.tpElevated.Text = "Elevated Functions Panel";
            this.tpElevated.UseVisualStyleBackColor = true;
            // 
            // tcElevatedUser
            // 
            this.tcElevatedUser.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tcElevatedUser.Controls.Add(this.tpOverviewElevated);
            this.tcElevatedUser.Controls.Add(this.tpAddNewTable);
            this.tcElevatedUser.Controls.Add(this.tpRestaurantOptions);
            this.tcElevatedUser.Controls.Add(this.tpEditMenu);
            this.tcElevatedUser.Controls.Add(this.tpRefresh);
            this.tcElevatedUser.Controls.Add(this.tpLogout);
            this.tcElevatedUser.Controls.Add(this.tpUpdates);
            this.tcElevatedUser.Controls.Add(this.tpFinish);
            this.tcElevatedUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcElevatedUser.Location = new System.Drawing.Point(3, 3);
            this.tcElevatedUser.Multiline = true;
            this.tcElevatedUser.Name = "tcElevatedUser";
            this.tcElevatedUser.SelectedIndex = 0;
            this.tcElevatedUser.Size = new System.Drawing.Size(543, 315);
            this.tcElevatedUser.TabIndex = 0;
            // 
            // tpOverviewElevated
            // 
            this.tpOverviewElevated.Controls.Add(this.rtcElevatedOverview);
            this.tpOverviewElevated.Location = new System.Drawing.Point(4, 4);
            this.tpOverviewElevated.Name = "tpOverviewElevated";
            this.tpOverviewElevated.Padding = new System.Windows.Forms.Padding(3);
            this.tpOverviewElevated.Size = new System.Drawing.Size(535, 265);
            this.tpOverviewElevated.TabIndex = 0;
            this.tpOverviewElevated.Text = "Overview";
            this.tpOverviewElevated.UseVisualStyleBackColor = true;
            // 
            // rtcElevatedOverview
            // 
            this.rtcElevatedOverview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtcElevatedOverview.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtcElevatedOverview.Location = new System.Drawing.Point(3, 3);
            this.rtcElevatedOverview.Name = "rtcElevatedOverview";
            this.rtcElevatedOverview.ReadOnly = true;
            this.rtcElevatedOverview.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtcElevatedOverview.Size = new System.Drawing.Size(529, 259);
            this.rtcElevatedOverview.TabIndex = 9;
            this.rtcElevatedOverview.Text = "As an Admin user, the user will now be able to see additional buttons - Add New T" +
    "able, Finish, Restaurant Options, Edit Menu Items, Refresh, Logout, and Check fo" +
    "r Updates.";
            // 
            // tpAddNewTable
            // 
            this.tpAddNewTable.Controls.Add(this.rtbAddNewTable);
            this.tpAddNewTable.Location = new System.Drawing.Point(4, 4);
            this.tpAddNewTable.Name = "tpAddNewTable";
            this.tpAddNewTable.Padding = new System.Windows.Forms.Padding(3);
            this.tpAddNewTable.Size = new System.Drawing.Size(535, 274);
            this.tpAddNewTable.TabIndex = 1;
            this.tpAddNewTable.Text = "Add New Table";
            this.tpAddNewTable.UseVisualStyleBackColor = true;
            // 
            // rtbAddNewTable
            // 
            this.rtbAddNewTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbAddNewTable.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbAddNewTable.Location = new System.Drawing.Point(3, 3);
            this.rtbAddNewTable.Name = "rtbAddNewTable";
            this.rtbAddNewTable.ReadOnly = true;
            this.rtbAddNewTable.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbAddNewTable.Size = new System.Drawing.Size(529, 268);
            this.rtbAddNewTable.TabIndex = 10;
            this.rtbAddNewTable.Text = resources.GetString("rtbAddNewTable.Text");
            // 
            // tpRestaurantOptions
            // 
            this.tpRestaurantOptions.Controls.Add(this.richTextBox2);
            this.tpRestaurantOptions.Controls.Add(this.rtbRestaurantOptions);
            this.tpRestaurantOptions.Location = new System.Drawing.Point(4, 4);
            this.tpRestaurantOptions.Name = "tpRestaurantOptions";
            this.tpRestaurantOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tpRestaurantOptions.Size = new System.Drawing.Size(535, 265);
            this.tpRestaurantOptions.TabIndex = 2;
            this.tpRestaurantOptions.Text = "Restaurant Options";
            this.tpRestaurantOptions.UseVisualStyleBackColor = true;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox2.Location = new System.Drawing.Point(3, 3);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox2.Size = new System.Drawing.Size(529, 259);
            this.richTextBox2.TabIndex = 12;
            this.richTextBox2.Text = resources.GetString("richTextBox2.Text");
            // 
            // rtbRestaurantOptions
            // 
            this.rtbRestaurantOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbRestaurantOptions.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbRestaurantOptions.Location = new System.Drawing.Point(3, 3);
            this.rtbRestaurantOptions.Name = "rtbRestaurantOptions";
            this.rtbRestaurantOptions.ReadOnly = true;
            this.rtbRestaurantOptions.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbRestaurantOptions.Size = new System.Drawing.Size(529, 259);
            this.rtbRestaurantOptions.TabIndex = 11;
            this.rtbRestaurantOptions.Text = resources.GetString("rtbRestaurantOptions.Text");
            // 
            // tpEditMenu
            // 
            this.tpEditMenu.Controls.Add(this.rtbMenuItems);
            this.tpEditMenu.Location = new System.Drawing.Point(4, 4);
            this.tpEditMenu.Name = "tpEditMenu";
            this.tpEditMenu.Padding = new System.Windows.Forms.Padding(3);
            this.tpEditMenu.Size = new System.Drawing.Size(535, 265);
            this.tpEditMenu.TabIndex = 3;
            this.tpEditMenu.Text = "Menu Items";
            this.tpEditMenu.UseVisualStyleBackColor = true;
            // 
            // rtbMenuItems
            // 
            this.rtbMenuItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbMenuItems.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbMenuItems.Location = new System.Drawing.Point(3, 3);
            this.rtbMenuItems.Name = "rtbMenuItems";
            this.rtbMenuItems.ReadOnly = true;
            this.rtbMenuItems.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbMenuItems.Size = new System.Drawing.Size(529, 259);
            this.rtbMenuItems.TabIndex = 13;
            this.rtbMenuItems.Text = resources.GetString("rtbMenuItems.Text");
            // 
            // tpRefresh
            // 
            this.tpRefresh.Controls.Add(this.rtbRefresh);
            this.tpRefresh.Location = new System.Drawing.Point(4, 4);
            this.tpRefresh.Name = "tpRefresh";
            this.tpRefresh.Padding = new System.Windows.Forms.Padding(3);
            this.tpRefresh.Size = new System.Drawing.Size(535, 265);
            this.tpRefresh.TabIndex = 4;
            this.tpRefresh.Text = "Refresh";
            this.tpRefresh.UseVisualStyleBackColor = true;
            // 
            // rtbRefresh
            // 
            this.rtbRefresh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbRefresh.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbRefresh.Location = new System.Drawing.Point(3, 3);
            this.rtbRefresh.Name = "rtbRefresh";
            this.rtbRefresh.ReadOnly = true;
            this.rtbRefresh.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbRefresh.Size = new System.Drawing.Size(529, 259);
            this.rtbRefresh.TabIndex = 14;
            this.rtbRefresh.Text = "This button will refresh the screen to show any changes made in the reservations " +
    "panel, on the table objects and the restauarant layout.";
            // 
            // tpLogout
            // 
            this.tpLogout.Controls.Add(this.rtbLogout);
            this.tpLogout.Location = new System.Drawing.Point(4, 4);
            this.tpLogout.Name = "tpLogout";
            this.tpLogout.Padding = new System.Windows.Forms.Padding(3);
            this.tpLogout.Size = new System.Drawing.Size(535, 265);
            this.tpLogout.TabIndex = 5;
            this.tpLogout.Text = "Logout";
            this.tpLogout.UseVisualStyleBackColor = true;
            // 
            // rtbLogout
            // 
            this.rtbLogout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLogout.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbLogout.Location = new System.Drawing.Point(3, 3);
            this.rtbLogout.Name = "rtbLogout";
            this.rtbLogout.ReadOnly = true;
            this.rtbLogout.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbLogout.Size = new System.Drawing.Size(529, 259);
            this.rtbLogout.TabIndex = 15;
            this.rtbLogout.Text = "This will log the user completely out of the system. This is intended for users w" +
    "ho would like to log into a different restaurant, as a user might have multiple " +
    "restaurants under their management. ";
            // 
            // tpUpdates
            // 
            this.tpUpdates.Controls.Add(this.rtbUpdates);
            this.tpUpdates.Location = new System.Drawing.Point(4, 4);
            this.tpUpdates.Name = "tpUpdates";
            this.tpUpdates.Padding = new System.Windows.Forms.Padding(3);
            this.tpUpdates.Size = new System.Drawing.Size(535, 265);
            this.tpUpdates.TabIndex = 6;
            this.tpUpdates.Text = "Check for Updates";
            this.tpUpdates.UseVisualStyleBackColor = true;
            // 
            // rtbUpdates
            // 
            this.rtbUpdates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbUpdates.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbUpdates.Location = new System.Drawing.Point(3, 3);
            this.rtbUpdates.Name = "rtbUpdates";
            this.rtbUpdates.ReadOnly = true;
            this.rtbUpdates.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbUpdates.Size = new System.Drawing.Size(529, 259);
            this.rtbUpdates.TabIndex = 16;
            this.rtbUpdates.Text = resources.GetString("rtbUpdates.Text");
            // 
            // tpFinish
            // 
            this.tpFinish.Controls.Add(this.rtbUpdate);
            this.tpFinish.Location = new System.Drawing.Point(4, 4);
            this.tpFinish.Name = "tpFinish";
            this.tpFinish.Padding = new System.Windows.Forms.Padding(3);
            this.tpFinish.Size = new System.Drawing.Size(535, 265);
            this.tpFinish.TabIndex = 7;
            this.tpFinish.Text = "Finish";
            this.tpFinish.UseVisualStyleBackColor = true;
            // 
            // rtbUpdate
            // 
            this.rtbUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbUpdate.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbUpdate.Location = new System.Drawing.Point(3, 3);
            this.rtbUpdate.Name = "rtbUpdate";
            this.rtbUpdate.ReadOnly = true;
            this.rtbUpdate.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbUpdate.Size = new System.Drawing.Size(529, 259);
            this.rtbUpdate.TabIndex = 17;
            this.rtbUpdate.Text = "This will end the Admin user session and revert the user back to a General user. " +
    "The user will then have to enable Admin mode again to access Admin functionality" +
    ".";
            // 
            // tpReservations
            // 
            this.tpReservations.Controls.Add(this.richTextBox1);
            this.tpReservations.Location = new System.Drawing.Point(4, 55);
            this.tpReservations.Name = "tpReservations";
            this.tpReservations.Padding = new System.Windows.Forms.Padding(3);
            this.tpReservations.Size = new System.Drawing.Size(549, 321);
            this.tpReservations.TabIndex = 5;
            this.tpReservations.Text = "Reservations";
            this.tpReservations.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(543, 315);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rtbGettingStarted);
            this.tabPage1.Location = new System.Drawing.Point(4, 55);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(549, 321);
            this.tabPage1.TabIndex = 6;
            this.tabPage1.Text = "Getting Started";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // rtbGettingStarted
            // 
            this.rtbGettingStarted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbGettingStarted.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbGettingStarted.Location = new System.Drawing.Point(3, 3);
            this.rtbGettingStarted.Name = "rtbGettingStarted";
            this.rtbGettingStarted.ReadOnly = true;
            this.rtbGettingStarted.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbGettingStarted.Size = new System.Drawing.Size(543, 315);
            this.rtbGettingStarted.TabIndex = 8;
            this.rtbGettingStarted.Text = resources.GetString("rtbGettingStarted.Text");
            // 
            // InfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 456);
            this.Controls.Add(this.tclInfo);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "InfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "InfoForm";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tclInfo.ResumeLayout(false);
            this.tpStaff.ResumeLayout(false);
            this.tpAdmin.ResumeLayout(false);
            this.tpFunctions.ResumeLayout(false);
            this.tcAdminMode.ResumeLayout(false);
            this.tpAllReservations.ResumeLayout(false);
            this.tpCapacityStatus.ResumeLayout(false);
            this.tpOverview.ResumeLayout(false);
            this.tpAdminMode.ResumeLayout(false);
            this.tpChangePIN.ResumeLayout(false);
            this.tpUsingTables.ResumeLayout(false);
            this.tpElevated.ResumeLayout(false);
            this.tcElevatedUser.ResumeLayout(false);
            this.tpOverviewElevated.ResumeLayout(false);
            this.tpAddNewTable.ResumeLayout(false);
            this.tpRestaurantOptions.ResumeLayout(false);
            this.tpEditMenu.ResumeLayout(false);
            this.tpRefresh.ResumeLayout(false);
            this.tpLogout.ResumeLayout(false);
            this.tpUpdates.ResumeLayout(false);
            this.tpFinish.ResumeLayout(false);
            this.tpReservations.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.TabControl tclInfo;
        private System.Windows.Forms.TabPage tpStaff;
        private System.Windows.Forms.RichTextBox rtbStaff;
        private System.Windows.Forms.TabPage tpAdmin;
        private System.Windows.Forms.RichTextBox rtbAdmin;
        private System.Windows.Forms.TabPage tpFunctions;
        private System.Windows.Forms.TabPage tpUsingTables;
        private System.Windows.Forms.RichTextBox rtbUsingTables;
        private System.Windows.Forms.TabControl tcAdminMode;
        private System.Windows.Forms.TabPage tpAllReservations;
        private System.Windows.Forms.TabPage tpCapacityStatus;
        private System.Windows.Forms.TabPage tpOverview;
        private System.Windows.Forms.TabPage tpAdminMode;
        private System.Windows.Forms.TabPage tpElevated;
        private System.Windows.Forms.TabPage tpReservations;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox rtcFunctionsPnlAllReservations;
        private System.Windows.Forms.TabPage tpChangePIN;
        private System.Windows.Forms.RichTextBox rtbCapacityStatus;
        private System.Windows.Forms.RichTextBox rtbOverview;
        private System.Windows.Forms.RichTextBox rtbEnableAdmin;
        private System.Windows.Forms.RichTextBox rtbChangePIN;
        private System.Windows.Forms.TabControl tcElevatedUser;
        private System.Windows.Forms.TabPage tpOverviewElevated;
        private System.Windows.Forms.TabPage tpAddNewTable;
        private System.Windows.Forms.RichTextBox rtcElevatedOverview;
        private System.Windows.Forms.RichTextBox rtbAddNewTable;
        private System.Windows.Forms.TabPage tpRestaurantOptions;
        private System.Windows.Forms.TabPage tpEditMenu;
        private System.Windows.Forms.TabPage tpRefresh;
        private System.Windows.Forms.TabPage tpLogout;
        private System.Windows.Forms.TabPage tpUpdates;
        private System.Windows.Forms.TabPage tpFinish;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox rtbRestaurantOptions;
        private System.Windows.Forms.RichTextBox rtbMenuItems;
        private System.Windows.Forms.RichTextBox rtbRefresh;
        private System.Windows.Forms.RichTextBox rtbLogout;
        private System.Windows.Forms.RichTextBox rtbUpdates;
        private System.Windows.Forms.RichTextBox rtbUpdate;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox rtbGettingStarted;
    }
}