using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableFindBackend.Models;

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
            foreach (Restaurant r in list)
            {
                ListViewItem item = new ListViewItem(r.name + ", " + r.locationString);
                lvRestaurant.Items.Add(item);
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
            if (lvRestaurant.SelectedItems.Count > 0)
            {
                selected = this.list[lvRestaurant.Items.IndexOf(lvRestaurant.SelectedItems[0])];

                if (cbxDefault.Checked)
                {
                    Properties.Settings.Default.defaultRestaurant = lvRestaurant.Items.IndexOf(lvRestaurant.SelectedItems[0]);
                    Properties.Settings.Default.Save();
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
