using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TableFindBackend.Models;
using TableFindBackend.ViewModels;

namespace TableFindBackend.Forms
{
    public partial class SelectRestaurantForm : Form
    {
        //This form will only appear if the owner has more than one restaurant under his/her control
        public Restaurant selected { get; set; }//public property which allows the login Form to easily get access to the user's choice

        private List<Restaurant> list;

        //constructor of the form. receives a list of restaurant objects which will be listed on the flp        
        public SelectRestaurantForm(List<Restaurant> list)
        {            
            this.list = list;
            InitializeComponent();
            PopulateList();
            foreach (Restaurant r in list)
            {
                ListViewItem item = new ListViewItem(r.Name);
            }
        }

        //method which generates the RestaurantViews
        private void PopulateList()
        {
            Boolean toggle = false;
            foreach (Restaurant r in list)
            {
                RestaurantView tempView = new RestaurantView();
                tempView.RestaurantName = r.Name;
                tempView.LocationString = r.LocationString;
                tempView.ObjectId = r.objectId;
                if (toggle == true)//toggle is used to change the color of the 'selected restaurantView'
                {
                    toggle = false;
                    tempView.BackgroundColor = System.Drawing.Color.FromArgb(209, 196, 233);
                }
                else
                {
                    toggle = true;
                    tempView.BackgroundColor = System.Drawing.Color.FromArgb(179, 229, 252);
                }
                flpRestaurants.Controls.Add(tempView);
                tempView.MouseClick += new MouseEventHandler(restaurant_Click);
            }
        }

        // method that simulates a button press on one of the restaurantViews
        private void restaurant_Click(object sender, MouseEventArgs e)
        {
            RestaurantView tempView = (RestaurantView)sender;
            foreach (Restaurant r in list)
            {
                if (r.objectId == tempView.ObjectId)//the one that is selected
                {
                    selected = r;
                    tempView.Selected(true);
                }
            }
            foreach (RestaurantView r in flpRestaurants.Controls)//the rest has toe be 'deselected'
            {
                if (r == tempView)
                {
                    r.Selected(true);
                }
                else
                    r.Selected(false);
            }
        }

        //this method will check if the user specified a default restaurant in the past and will prioritize that option first
        private void SelectRestaurant_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.defaultRestaurant != -1)
            {
                selected = this.list[Properties.Settings.Default.defaultRestaurant];

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        //this method will effectively close the form after the user made a selection. if the checkbox is selected, it will apply it as the default option
        private void btnChangeLoad_Click(object sender, EventArgs e)
        {

            if (selected != null)
            {
                RestaurantView tempView = null;
                foreach (RestaurantView r in flpRestaurants.Controls)
                {
                    if (r.ObjectId == selected.objectId)
                    {
                        tempView = r;
                    }
                }
                if (cbxDefault.Checked)//changes the default settings
                {
                    Properties.Settings.Default.defaultRestaurant = flpRestaurants.Controls.IndexOf(tempView);
                    Properties.Settings.Default.Save();
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
