using BackendlessAPI;
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
    public partial class CustomerDetailsForm : Form
    {
        public CustomerDetailsForm(BackendlessUser user)
        {
            InitializeComponent();

            if (user.ObjectId == OwnerStorage.ThisRestaurant.ownerId)
            {
                lblRestaurantLabel.Visible = true;
            }
            else
            {
                tbxContact.Text = user.GetProperty("Cellphone").ToString();
                tbxFName.Text = user.GetProperty("FirstName").ToString();
                tbxLName.Text = user.GetProperty("LastName").ToString();
                tbxEmail.Text = user.GetProperty("email").ToString();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
