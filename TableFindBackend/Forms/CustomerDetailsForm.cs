using BackendlessAPI;
using System;
using System.Windows.Forms;
using TableFindBackend.Global_Variables;
using TableFindBackend.Models;

namespace TableFindBackend.Forms
{
    public partial class CustomerDetailsForm : Form
    {
        //this form shows the customer details of the reservation on which the user clicks to view more details
        public CustomerDetailsForm(BackendlessUser user, Reservation r)
        {
            InitializeComponent();
            if (user != null)//in the rare occurance that the user has deleted/deactivated his/her account
            {
                if (user.ObjectId == OwnerStorage.ThisRestaurant.ownerId)//determines if the reservation was made by the restaurant
                {
                    lblRestaurantLabel.Visible = true;
                    lblTitle.Text = "Reservation details for " + r.Name;
                }
                else//the reservation was made by a customer. his/her details will be displayed accordingly
                {
                    tbxContact.Text = user.GetProperty("Cellphone").ToString();
                    tbxFName.Text = user.GetProperty("FirstName").ToString();
                    tbxLName.Text = user.GetProperty("LastName").ToString();
                    tbxEmail.Text = user.GetProperty("email").ToString();
                    lblTitle.Text = "Reservation details for " + user.GetProperty("FirstName").ToString();
                }
            }
            else
            {
                //displays that the account could not be located
                lblRestaurantLabel.Visible = true;
                lblRestaurantLabel.Text = "This user has deactivated or deleted his/her account. No valid information about this user could be retrieved";
                lblTitle.Text = "Reservation details for " + r.Name;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            //Button used to close the form if the user wishes to not save changes made
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //Button used to close the form if the user wishes to not save changes made
            this.Close();
        }
    }
}
