using BackendlessAPI;
using System;
using System.Windows.Forms;
using TableFindBackend.Global_Variables;
using TableFindBackend.Models;

namespace TableFindBackend.Forms
{
    //This form shows the customer details of the reservation on which the user clicks to view more details
    public partial class CustomerDetailsForm : Form
    {
        public CustomerDetailsForm(BackendlessUser user, Reservation r)
        {
            InitializeComponent();
            //In the rare occurance that the user has deleted or deactivated his/her account
            if (user != null) 
            {
                //Determines if the reservation was made by the restaurant
                if (user.ObjectId == OwnerStorage.ThisRestaurant.ownerId) 
                {
                    lblRestaurantLabel.Visible = true;
                    lblTitle.Text = "Reservation details for " + r.Name;
                }
                //The reservation was made by a customer. His/her details will be displayed accordingly
                else
                {
                    tbxContact.Text = user.GetProperty("Cellphone").ToString();
                    tbxFName.Text = user.GetProperty("FirstName").ToString();
                    tbxLName.Text = user.GetProperty("LastName").ToString();
                    tbxEmail.Text = user.GetProperty("email").ToString();
                    lblTitle.Text = "Reservation details for " + user.GetProperty("FirstName").ToString();
                }
            }
            //Displays that the account could not be located
            else
            {
                lblRestaurantLabel.Visible = true;
                lblRestaurantLabel.Text = "This user has deactivated or deleted his/her account. No valid information about this user could be retrieved";
                lblTitle.Text = "Reservation details for " + r.Name;
            }
        }

        //Button used to close the form if the user wishes to not save changes made
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Button used to close the form if the user wishes to not save changes made
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
