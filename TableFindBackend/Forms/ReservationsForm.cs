using BackendlessAPI;
using BackendlessAPI.Async;
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
using TableFindBackend.Models;

namespace TableFindBackend.Forms
{
    
    public partial class ReservationsForm : Form
    {
     //   private System.Timers.Timer bottleneck;

        List<Reservation> rList;
        int index;
        RestaurantTable thisTable;
        public ReservationsForm(RestaurantTable item)
        {
            InitializeComponent();


            lvBookings.Columns.Add("Customer Name", 180);
            lvBookings.Columns.Add("Taken From", 190);
            lvBookings.Columns.Add("Taken To", 190);
            lvBookings.Columns.Add("Booked By", 120);
            lvBookings.Columns.Add("Contact Number", 170);
            
            thisTable = item;

            lblTitle.Text = thisTable.name;

            populateList(true);
        }
        public ReservationsForm()
        {
            InitializeComponent();
            btnAdd.Visible = false;
            this.Width += 170;
            lvBookings.Columns.Add("Customer Name", 180);
            lvBookings.Columns.Add("Taken From", 190);
            lvBookings.Columns.Add("Taken To", 190);
            lvBookings.Columns.Add("Booked By", 120);
            lvBookings.Columns.Add("Contact Number", 170);
            lvBookings.Columns.Add("Table", 170);

            lblTitle.Text = "All reservations";

            populateList(false);
        }

        public void populateList(bool single)
        {
            lvBookings.Items.Clear();
            rList = new List<Reservation>();

            if (thisTable != null)  //<--Means only one table's Reservations are to be shown
            {
                foreach (Reservation reservation in OwnerStorage.AllReservations)
                {
                    if (reservation.tableId == thisTable.objectId)
                    {
                        string userId = null;
                        rList.Add(reservation);
                        foreach (BackendlessUser user in OwnerStorage.AllUsers)
                        {
                            if (user.ObjectId == reservation.userId)
                            {
                                userId = user.ObjectId;
                            }
                        }
                        string[] arr = new string[5];
                        ListViewItem itm;
                        arr[0] = reservation.name.ToString();
                        arr[1] = reservation.takenFrom.ToString();
                        arr[2] = reservation.takenTo.ToString();
                        if (userId == OwnerStorage.CurrentlyLoggedIn.ObjectId)
                        {
                            arr[3] = "Restaurant";
                        }
                        else
                        {
                            arr[3] = "Customer";
                        }
                        arr[4] = reservation.number.ToString();

                        itm = new ListViewItem(arr);

                        lvBookings.Items.Add(itm);
                    }
                }
            }
            else//<---meanse all reservations are to be shown
            {
                foreach (Reservation reservation in OwnerStorage.AllReservations)
                {
                    string userId = null;
                    rList.Add(reservation);
                    foreach (BackendlessUser user in OwnerStorage.AllUsers)
                    {
                        if (user.ObjectId == reservation.userId)
                        {
                            userId = user.ObjectId;
                        }
                    }
                    string[] arr = new string[6];
                    ListViewItem itm;
                    arr[0] = reservation.name.ToString();
                    arr[1] = reservation.takenFrom.ToString();
                    arr[2] = reservation.takenTo.ToString();
                    if (userId == OwnerStorage.CurrentlyLoggedIn.ObjectId)
                    {
                        arr[3] = "Restaurant";
                    }
                    else
                    {
                        arr[3] = "Customer";
                    }
                    arr[4] = reservation.number.ToString();

                    foreach (RestaurantTable table in OwnerStorage.RestaurantTables)
                    {
                        if (table.objectId==reservation.tableId)
                        {
                            arr[5] = table.name.ToString();
                        }
                    }

                    itm = new ListViewItem(arr);

                    lvBookings.Items.Add(itm);
                    }
                
            }
        }

        private void TableBookings_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CreateReservationForm newReservation = new CreateReservationForm(thisTable);
            DialogResult result =newReservation.ShowDialog();
            if (result == DialogResult.OK)
            {
                Invoke(new Action(() =>
                {
                    CheckIfNew();
                   
                }));
                //bottleneck = new System.Timers.Timer();
                //bottleneck.Enabled = true;
                //bottleneck.AutoReset = false;
                //bottleneck.Interval = 4000;
                //bottleneck.Elapsed += OnTimedEvent;               

                

            }

           
        }
        private async void CheckIfNew()
            {
                    await Task.Delay(800);
            if (thisTable != null)
                populateList(true);
            else
                populateList(false);
        }
        public void initializeTimer()
        {
            //bottleneck = new System.Timers.Timer();
            //bottleneck.Enabled = true;
            //bottleneck.AutoReset = false;
            //bottleneck.Interval = 4000;
            //bottleneck.Elapsed += OnTimedEvent;
        }
        public void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (Application.OpenForms.OfType<ReservationsForm>().Any())
            {
                //Invoke(new Action(() =>
                //{
                //    if (thisTable != null)
                //        populateList(true);
                //    else
                //        populateList(false);
                //}));
            }
        }
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
            if (OwnerStorage.AdminMode==true)
            { 
            DialogResult remove = MessageBox.Show("Are you sure you want to remove this reservation?", "Remove Reservation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                pbxLoading.Visible=true;
                this.Enabled = false;
                if (remove == DialogResult.Yes)
                {

                    Reservation tempReservation = rList[index];

                    AsyncCallback<Reservation> saveObjectCallback = new AsyncCallback<Reservation>(
                      savedReservation =>
                      {


                          AsyncCallback<long> deleteObjectCallback = new AsyncCallback<long>(
                            deletionTime =>
                            {
                                Invoke(new Action(() =>
                                {
                                    pbxLoading.Visible = false;
                                    this.Enabled = true;
                                    MessageBox.Show(this, "reservation for " + rList[index].name + " has been removed");
                                    lvBookings.Items.RemoveAt(index);
                                    OwnerStorage.Log.Add("Reservation has been removed    : " + System.DateTime.Now.ToString("HH:mm"));
                                    OwnerStorage.Log.Add("Name:  " + rList[index].name);
                                    //OwnerStorage.AllReservations.Remove(tempReservation);  <--no need, the listeners will remove it from local storage.
                                    //if (thisTable != null)
                                    //    populateList(true);
                                    //else
                                    //    populateList(false);
                                }));
                            },
                            error =>
                            {
                                Invoke(new Action(() =>
                                {
                                    pbxLoading.Visible = false;
                                    this.Enabled = true;
                                    MessageBox.Show(this, "Error: " + error.Message);
                                }));
                            });
                          Backendless.Persistence.Of<Reservation>().Remove(savedReservation, deleteObjectCallback);
                      },
                      error =>
                      {
                          Invoke(new Action(() =>
                          {
                              pbxLoading.Visible = false;
                              this.Enabled = true;
                              MessageBox.Show(this, "Error: " + error.Message);
                          }));
                      }
                    );

                    Backendless.Persistence.Of<Reservation>().Save(tempReservation, saveObjectCallback);
                }
                this.Enabled = true;
                pbxLoading.Visible = false;
            }
            else
            {
                MessageBox.Show("You do not have permission to remove this reservation. Only managers or selected assistant mangers can remove reservations");
                pbxLoading.Visible = false;
                this.Enabled = true;

            }
        }

        private void btnViewCustomer_Click(object sender, EventArgs e)
        {
            BackendlessUser tempUser=null;          
                foreach (BackendlessUser user in OwnerStorage.AllUsers)
                {
                    if (user.ObjectId == rList[index].userId)
                        tempUser = user;

                }
                CustomerDetailsForm detailsForm = new CustomerDetailsForm(tempUser);
                detailsForm.ShowDialog();          
        }
    }
}
