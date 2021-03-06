﻿using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TableFindBackend.ViewModels
{
    public partial class RestaurantView : UserControl
    {
        public RestaurantView()
        {
            InitializeComponent();
        }
        #region properties
        private string name;
        private string locationString;
        private string objectId;
        private bool active;
        private Color backgroundColor;



        [Category("Custom Prop")]
        public string RestaurantName
        {
            get => name;
            set
            {
                name = value;
                lblName.Text = value;
            }
        }
        [Category("Custom Prop")]
        public string LocationString
        {
            get => locationString;
            set
            {
                locationString = value;
                lblLocation.Text = value;
            }
        }
        [Category("Custom Prop")]
        public Color BackgroundColor
        {
            set
            {
                backgroundColor = value;
                this.BackColor = value;
            }
        }
        public void Selected(bool toggled)
        {
            if (toggled == true)
                this.BackColor = System.Drawing.Color.FromName("ActiveCaption");
            else
                this.BackColor = this.backgroundColor;
        }
        public Color TextColor
        {
            set
            {
                lblName.ForeColor = value;
                lblLocation.ForeColor = value;
            }
        }

        public string ObjectId { get => objectId; set => objectId = value; }
        public bool Active 
        {
            set 
            {
                lblActive.Text = "Deactivated";
                active = value; 
            } 
        }
        #endregion

        private void RestaurantView_Load(object sender, System.EventArgs e)
        {

        }
    }
}
