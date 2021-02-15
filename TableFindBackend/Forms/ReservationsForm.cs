using BackendlessAPI;
using BackendlessAPI.Async;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableFindBackend.Global_Variables;
using TableFindBackend.Models;

namespace TableFindBackend.Forms
{

    public partial class ReservationsForm : Form
    {
        //this form is used to display reservations for either All or only One specific Table
        //note that this form has two constructors

        List<Reservation> rList;
        int index;
        RestaurantTable thisTable;
        MainForm _master;

        //first constructor: this is used when the user requests this form to show only reservations to a specific table
        public ReservationsForm(RestaurantTable item, MainForm _master)
        {

            this._master = _master;
            InitializeComponent();

            lvBookings.Columns.Add("Customer Name", 180);
            lvBookings.Columns.Add("Taken From", 190);
            lvBookings.Columns.Add("Taken To", 190);
            lvBookings.Columns.Add("Booked By", 120);
            lvBookings.Columns.Add("Contact Number", 170);

            thisTable = item;
            lblTitle.Text = thisTable.Name;

            populateList(true);
        }

        //second constructor: this is used when the user requests this form to show all reservations currently active in the restaurant
        public ReservationsForm()
        {
            InitializeComponent();
            btnAdd.Visible = false;
            this.Width += 170;//<-- widens the form slightly to make room for the extra column (table) on the form
            lvBookings.Columns.Add("Customer Name", 180);
            lvBookings.Columns.Add("Taken From", 190);
            lvBookings.Columns.Add("Taken To", 190);
            lvBookings.Columns.Add("Booked By", 120);
            lvBookings.Columns.Add("Contact Number", 170);
            lvBookings.Columns.Add("Table", 170);
            lblTitle.Text = "All Reservations";

            populateList(false);
        }

        //this method will populate tho onscreen listView with the relevent Reservations
        public void populateList(bool single)
        {
            lvBookings.Items.Clear();
            rList = new List<Reservation>();

            if (thisTable != null)  //<--Means only one table's Reservations are to be shown
            {
                foreach (Reservation reservation in OwnerStorage.ActiveReservations)
                {
                    if (reservation.TableId == thisTable.objectId)
                    {
                        string userId = null;
                        rList.Add(reservation);
                        foreach (BackendlessUser user in OwnerStorage.AllUsers)
                        {
                            if (user.ObjectId == reservation.UserId)
                            {
                                userId = user.ObjectId;
                            }
                        }
                        string[] arr = new string[5];
                        ListViewItem itm;
                        arr[0] = reservation.Name.ToString();
                        arr[1] = reservation.TakenFrom.ToString("ddd, dd/MM/yyyy, HH:mm");
                        arr[2] = reservation.TakenTo.ToString("HH:mm");
                        if (userId == OwnerStorage.CurrentlyLoggedIn.ObjectId)//<--determines who made the reservation
                        {
                            arr[3] = "Restaurant";
                        }
                        else
                        {
                            arr[3] = "Customer";
                        }
                        arr[4] = reservation.Number.ToString();

                        itm = new ListViewItem(arr);

                        lvBookings.Items.Add(itm);
                    }
                }
            }
            else//<---means all reservations are to be shown
            {
                foreach (Reservation reservation in OwnerStorage.ActiveReservations)
                {
                    string userId = null;
                    rList.Add(reservation);
                    foreach (BackendlessUser user in OwnerStorage.AllUsers)
                    {
                        if (user.ObjectId == reservation.UserId)
                        {
                            userId = user.ObjectId;
                        }
                    }
                    string[] arr = new string[6];
                    ListViewItem itm;
                    arr[0] = reservation.Name.ToString();
                    arr[1] = reservation.TakenFrom.ToString("ddd, dd/MM/yyyy, HH:mm");
                    arr[2] = reservation.TakenTo.ToString("HH:mm");
                    if (userId == OwnerStorage.CurrentlyLoggedIn.ObjectId)//<--determines who made the reservation
                    {
                        arr[3] = "Restaurant";
                    }
                    else
                    {
                        arr[3] = "Customer";
                    }
                    arr[4] = reservation.Number.ToString();

                    foreach (RestaurantTable table in OwnerStorage.RestaurantTables)
                    {
                        if (table.objectId == reservation.TableId)
                        {
                            arr[5] = table.Name.ToString();
                        }
                    }

                    itm = new ListViewItem(arr);

                    lvBookings.Items.Add(itm);
                }
            }
        }

        //Button used to close the form if the user wishes to not save changes made
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //Button used to close the form if the user wishes to not save changes made
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //this method will simply show the CreateReservationsForm after checking if the user has that privilege.
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (thisTable.Available == true || OwnerStorage.AdminMode == true)//checks if the user is logged in as admin
            {
                CreateReservationForm newReservation = new CreateReservationForm(thisTable);
                DialogResult result = newReservation.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Invoke(new Action(() =>
                    {
                        CheckIfNew();//a timed event which gives the CreatedListener time to catch the new object created and display it on the form
                    }));
                }
            }
            else//user is not in elevated privilege mode
            {
                MessageBox.Show("This Table has been made unactive by an Admin User. Please login as Admin User to create a reservation for this table", "unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //a delayed event that gives the CreatedListener time to catch the new object created and display it on the form
        private async void CheckIfNew()
        {
            await Task.Delay(800);//<-- 0.8 seconds
            if (thisTable != null)
                populateList(true);
            else
                populateList(false);
        }

        //a simple method that keeps track of which object the user has selected from the list
        private void lvBookings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvBookings.SelectedItems.Count > 0)
            {
                btnRemove.Enabled = true;
                btnViewCustomer.Enabled = true;
                index = lvBookings.Items.IndexOf(lvBookings.SelectedItems[0]);
            }
            else
            {
                btnRemove.Enabled = false;
                btnViewCustomer.Enabled = false;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (OwnerStorage.AdminMode == true)
            {
                DialogResult remove = MessageBox.Show("Are you sure you want to remove this reservation?", "Remove Reservation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                pbxLoading.Visible = true;
                this.Enabled = false;
                if (remove == DialogResult.Yes)
                {
                    Reservation tempReservation = rList[index];//the selected reservation

                    AsyncCallback<Reservation> updateObjectCallback = new AsyncCallback<Reservation>(
                savedReservation =>
                {
                    //success. The reservation has been successfully deactivated. the events are being logged
                    Invoke(new Action(() =>
                    {
                        lvBookings.Items.RemoveAt(index);
                        pbxLoading.Visible = false;
                        this.Enabled = true;
                        OwnerStorage.LogInfo.Add("Reservation has been removed\nName:  " + tempReservation.Name);
                        OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                        MessageBox.Show(this, "reservation for " + tempReservation.Name + " has been removed");
                        _master.RemoveOneReservationView(tempReservation, savedReservation);
                        CheckIfNew();
                    }));
                },
                error =>
                {
                    //something went wrong. a error message will now display
                    Invoke(new Action(() =>
                    {
                        MessageBox.Show(this, "Error: " + error.Message);
                        pbxLoading.Visible = true;
                        this.Enabled = false;
                    }));
                });

                    AsyncCallback<Reservation> saveObjectCallback = new AsyncCallback<Reservation>(
                    savedReservation =>
                    {
                        // now update the saved reservation
                        Backendless.Persistence.Of<Reservation>().Save(savedReservation, updateObjectCallback);
                    },
                    error =>
                    {
                        //something went wrong. an error message will now display
                        Invoke(new Action(() =>
                        {
                            MessageBox.Show(this, "Error: " + error.Message);
                            pbxLoading.Visible = true;
                            this.Enabled = false;
                        }));
                    });

                    //in order to update the reservation, it must first be saved
                    tempReservation.Active = false;
                    tempReservation.ReasonForExpiration = "Reservation has been removed";
                    Backendless.Persistence.Of<Reservation>().Save(tempReservation, saveObjectCallback);                    
                }
                else//the user selected 'no'
                {
                    this.Enabled = true;
                    pbxLoading.Visible = false;
                }
            }
            else//user is not in admin mode
            {
                MessageBox.Show("You do not have permission to remove this reservation. Only managers or selected assistant mangers can remove reservations");
                pbxLoading.Visible = false;
                this.Enabled = true;

            }
        }

        //this method will determine on which item the user clicked and show the detailsForm containing information about the user
        private void btnViewCustomer_Click(object sender, EventArgs e)
        {
            BackendlessUser tempUser = null;
            foreach (BackendlessUser user in OwnerStorage.AllUsers)
            {
                if (user.ObjectId == rList[index].UserId)
                    tempUser = user;

            }
            CustomerDetailsForm detailsForm = new CustomerDetailsForm(tempUser, rList[index]);
            detailsForm.ShowDialog();
        }

        //Blocks the "alt F4" capability so that the user cannot close the program while a process is running
        private void ReservationsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == System.Windows.Forms.CloseReason.UserClosing && pbxLoading.Visible == true)
            {
                e.Cancel = true;
            }
        }
    }
}
