using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableFindBackend.Global_Variables;

namespace TableFindBackend.Forms
{
    public partial class RestaurantStatusForm : Form
    {
        public RestaurantStatusForm()
        {
            InitializeComponent();
            tbrCapacity.Value = OwnerStorage.ThisRestaurant.MaxCapacity;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditor_Click(object sender, EventArgs e)
        {
            OwnerStorage.ThisRestaurant.MaxCapacity = tbrCapacity.Value;
            OwnerStorage.ThisRestaurant.EditRestaurant();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
