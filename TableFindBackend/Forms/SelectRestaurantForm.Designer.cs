﻿
namespace TableFindBackend.Forms
{
    partial class SelectRestaurantForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.cbxDefault = new System.Windows.Forms.CheckBox();
            this.btnChangeLoad = new System.Windows.Forms.Button();
            this.flpRestaurants = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(348, 86);
            this.panel1.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(5);
            this.lblTitle.Size = new System.Drawing.Size(346, 84);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Please select the restaurant you wish to log in to";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbxDefault
            // 
            this.cbxDefault.AutoSize = true;
            this.cbxDefault.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxDefault.Location = new System.Drawing.Point(49, 398);
            this.cbxDefault.Name = "cbxDefault";
            this.cbxDefault.Size = new System.Drawing.Size(257, 24);
            this.cbxDefault.TabIndex = 2;
            this.cbxDefault.Text = "Make this my default restaurant";
            this.cbxDefault.UseVisualStyleBackColor = true;
            // 
            // btnChangeLoad
            // 
            this.btnChangeLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnChangeLoad.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeLoad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnChangeLoad.Location = new System.Drawing.Point(104, 427);
            this.btnChangeLoad.Name = "btnChangeLoad";
            this.btnChangeLoad.Size = new System.Drawing.Size(152, 43);
            this.btnChangeLoad.TabIndex = 16;
            this.btnChangeLoad.Text = "Select Restaurant";
            this.btnChangeLoad.UseVisualStyleBackColor = false;
            this.btnChangeLoad.Click += new System.EventHandler(this.btnChangeLoad_Click);
            // 
            // flpRestaurants
            // 
            this.flpRestaurants.AutoScroll = true;
            this.flpRestaurants.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.flpRestaurants.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpRestaurants.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpRestaurants.Location = new System.Drawing.Point(13, 104);
            this.flpRestaurants.Name = "flpRestaurants";
            this.flpRestaurants.Padding = new System.Windows.Forms.Padding(15);
            this.flpRestaurants.Size = new System.Drawing.Size(346, 288);
            this.flpRestaurants.TabIndex = 17;
            // 
            // SelectRestaurantForm
            // 
            this.AcceptButton = this.btnChangeLoad;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 482);
            this.Controls.Add(this.flpRestaurants);
            this.Controls.Add(this.btnChangeLoad);
            this.Controls.Add(this.cbxDefault);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SelectRestaurantForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SelectRestaurant";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SelectRestaurant_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.CheckBox cbxDefault;
        private System.Windows.Forms.Button btnChangeLoad;
        private System.Windows.Forms.FlowLayoutPanel flpRestaurants;
    }
}