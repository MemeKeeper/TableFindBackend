using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TableFindBackend.Models;
using TableFindBackend.ViewModels;

namespace TableFindBackend.Forms
{
    public partial class SelectRestaurantForm : Form
    {
        public Restaurant selected { get; set; }

        private List<Restaurant> list;
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

        private void PopulateList()
        {
            Boolean toggle = false;
            foreach (Restaurant r in list)
            {
                RestaurantView tempView = new RestaurantView();
                tempView.RestaurantName = r.Name;
                tempView.LocationString = r.LocationString;
                tempView.ObjectId = r.objectId;
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

        private void restaurant_Click(object sender, MouseEventArgs e)
        {
            RestaurantView tempView = (RestaurantView)sender;
            foreach (Restaurant r in list)
            {
                if (r.objectId == tempView.ObjectId)
                {
                    selected = r;
                    tempView.Selected(true);
                }
            }
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

        private void lvRestaurant_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SelectRestaurant_Load(object sender, EventArgs e)
        {

            if (Properties.Settings.Default.defaultRestaurant != -1)
            {
                selected = this.list[Properties.Settings.Default.defaultRestaurant];

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

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
