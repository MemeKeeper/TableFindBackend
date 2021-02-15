using BackendlessAPI;
using BackendlessAPI.Async;
using System;
using System.Windows.Forms;
using TableFindBackend.Global_Variables;
using TableFindBackend.Models;

namespace TableFindBackend.Forms
{
    public partial class ReservationDetailsForm : Form
    {
        MainForm _master;
        Reservation thisReservation;
        public ReservationDetailsForm(Reservation r, BackendlessUser u, RestaurantTable t, bool active, MainForm _master)
        {

            thisReservation = r;

            InitializeComponent();

            tbxCapacity.Text = t.Capacity.ToString();
            tbxContact.Text = u.GetProperty("Cellphone").ToString();
            tbxContact2.Text = r.Number;
            tbxEmail.Text = u.Email;
            tbxFName.Text = u.GetProperty("FirstName").ToString();
            tbxLName.Text = u.GetProperty("LastName").ToString();
            tbxTable.Text = t.Name;
            tbxTime.Text = r.TakenFrom.ToString("dddd, dd/MM,    HH:mm") + " - " + r.TakenTo.ToString("HH:mm");
            lblTitle.Text = "Reservation for " + r.Name;

            if (u.ObjectId == OwnerStorage.CurrentlyLoggedIn.ObjectId)
            {
                lblMadeByRestaurant.Visible = true;
            }
            else
            {
                if (u.Email == "-")
                {
                    lblMadeByRestaurant.Visible = true;
                    lblMadeByRestaurant.Text = "This User could not be located in the database. It might be that this user has been removed completely.";
                }
                else
                {
                    lblContactNumber.Visible = false;
                    tbxContact2.Visible = false;
                    pnlReservation.Height += -50;
                }
            }

            if (OwnerStorage.AdminMode == true && active == true)
                btnDelete.Visible = true;

            this._master = _master;
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

        //A method that will appear on all forms. It simulates a loading screen by showing and hiding all necessary buttons and interface elements
        private void ShowLoading(bool show)
        {
            if (show == true)
            {
                pbxLoading.Visible = true;
                btnClose.Enabled = false;
                btnDelete.Enabled = false;
                btnExit.Enabled = false;
            }
            else
            {
                pbxLoading.Visible = false;
                btnClose.Enabled = true;
                btnDelete.Enabled = true;
                btnExit.Enabled = true;
            }
        }

        //this method will 'deactivate' the reservation.
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (OwnerStorage.AdminMode == true)//makes sure that the user is in admin mode first
            {
                DialogResult remove = MessageBox.Show("Are you sure you want to remove this reservation?", "Remove Reservation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (remove == DialogResult.Yes)
                {
                    ShowLoading(true);
                    
                    AsyncCallback<Reservation> updateObjectCallback = new AsyncCallback<Reservation>(
                   savedReservation =>
                   {
                       //success, the reservation has been created. it will now call a method on the MainForm to update the visual elements
                       Invoke(new Action(() =>
                       {
                           OwnerStorage.LogInfo.Add("Reservation has Expired\nName:  " + savedReservation.Name);
                           OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                           //calls the method to add the reservation on to the form
                           _master.RemoveOneReservationView(thisReservation, savedReservation);

                           MessageBox.Show(this, "reservation for " + thisReservation.Name + " has been removed");
                           ShowLoading(false);
                           this.Close();
                       }));
                   },
                   error =>
                   {
                       //something went wrong. An error message will now display
                       Invoke(new Action(() =>
                       {
                           MessageBox.Show(this, "Error: " + error.Message);
                           ShowLoading(false);
                       }));
                   });

                    AsyncCallback<Reservation> saveObjectCallback = new AsyncCallback<Reservation>(
                    savedReservation =>
                    {                        
                        //success, now update the saved object
                        savedReservation.Active = false;
                        savedReservation.ReasonForExpiration = "Reservation has passed its expiration date";
                        Backendless.Persistence.Of<Reservation>().Save(savedReservation, updateObjectCallback);
                    },
                    error =>
                    {
                        //something went wrong. An error message will now display
                        Invoke(new Action(() =>
                        {
                            MessageBox.Show(this, "Error: " + error.Message);
                            ShowLoading(false);
                        }));
                    });

                    //backendless demands that the object be 'saved' first before we can update the object
                    Backendless.Persistence.Of<Reservation>().Save(thisReservation, saveObjectCallback);
                }
            }
            else
            {
                MessageBox.Show("You do not have permission to remove this reservation. Only managers or selected assistant mangers can remove reservations");
            }
        }

        //Blocks the "alt F4" capability so that the user cannot close the program while a process is running
        private void ReservationDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == System.Windows.Forms.CloseReason.UserClosing && pbxLoading.Visible == true)
            {
                e.Cancel = true;
            }
        }
    }
}
