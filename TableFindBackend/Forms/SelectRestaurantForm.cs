using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TableFindBackend.Models;
using TableFindBackend.ViewModels;

namespace TableFindBackend.Forms
{
    //This form will only appear if the owner has more than one restaurant under his/her control
    public partial class SelectRestaurantForm : Form
    {
        //Public property which allows the login form to easily get access to the user's choice
        public Restaurant selected { get; set; }

        private List<Restaurant> list;

        //Constructor of the form which receives a list of restaurant objects which will be listed on the flp        
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

        //Method which generates the RestaurantViews
        private void PopulateList()
        {
            Boolean toggle = false;
            foreach (Restaurant r in list)
            {
                RestaurantView tempView = new RestaurantView();
                tempView.RestaurantName = r.Name;
                tempView.LocationString = r.LocationString;
                tempView.ObjectId = r.objectId;
                //Toggle is used to change the color of the 'selected restaurantView'
                if (toggle == true)
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

        //Method that simulates a button press on one of the restaurantViews
        private void restaurant_Click(object sender, MouseEventArgs e)
        {
            RestaurantView tempView = (RestaurantView)sender;
            foreach (Restaurant r in list)
            {
                //the restaurantObject that is selected
                if (r.objectId == tempView.ObjectId)
                {
                    selected = r;
                    tempView.Selected(true);
                }
            }
            //the rest has to be 'deselected'
            foreach (RestaurantView r in flpRestaurants.Controls)
            {
                if (r == tempView)
                {
                    r.Selected(true);
                }
                else
                    r.Selected(false);
            }
        }

        //This method will check if the user specified a default restaurant in the past and will prioritize that option first
        private void SelectRestaurant_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.defaultRestaurant != -1)
            {
                selected = this.list[Properties.Settings.Default.defaultRestaurant];

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        //This method will effectively close the form after the user made a selection. If the checkbox is selected, it will apply it as the default option
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
                //Changes the default settings
                if (cbxDefault.Checked)
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
