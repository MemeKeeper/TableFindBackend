using BackendlessAPI;
using BackendlessAPI.Async;
using System;
using System.Windows.Forms;
using TableFindBackend.Global_Variables;

namespace TableFindBackend.Models
{
    //This form is used to create new Reservations from the restaurant's side
    public partial class CreateReservationForm : Form
    {
        //Variable to make working with the currently selected RestaurantTable a lot easier 
        RestaurantTable thisTable;

        //The constructor receives the RestaurantTable object to ensure that the reservation to be made is aimed at this specific RestaurantTable
        public CreateReservationForm(RestaurantTable thisTable) 
        {
            InitializeComponent();
            this.thisTable = thisTable;
            lblTitle.Text = thisTable.Name;
        }

        //Button used to close the form if the user wishes to not save changes made
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //Button used to close the form if the user wishes to not save changes made
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //A method that will appear on all forms. It simulates a loading screen by showing and hiding all necessary buttons and interface elements
        private void ShowLoading(bool toggle)
        {
            if (toggle == true)
            {
                pbxLoading.Visible = true;
                btnCancel.Enabled = false;
                btnCreate.Enabled = false;
                btnExit.Enabled = false;
            }
            else
            {
                pbxLoading.Visible = false;
                btnCancel.Enabled = true;
                btnCreate.Enabled = true;
                btnExit.Enabled = true;
            }
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            Reservation reservation = new Reservation();

            //Performs validation to ensure that all text fields are entered
            if (tbxContact.Text == "" || tbxName.Text == "") 
            {
                MessageBox.Show(this, "Make sure to fill in all fields");
            }
            else
            //Checks if the contact number is of valid format
            if (tbxContact.Text.Length != 10) 
            {
                MessageBox.Show(this, "The Contact number you have entered is invalid");
            }
            else
            {                
                //This if statement ensures that the times specified by the user does not go outside of the open and close bounds of the restaurant
                if ((dtpTakenFrom.Value.TimeOfDay > OwnerStorage.ThisRestaurant.Open.TimeOfDay) && ((dtpTakenFrom.Value.TimeOfDay.Hours + spnDuration.Value) < OwnerStorage.ThisRestaurant.Close.Hour))
                {
                    //Ensures that the reservation being made does not clash with another reservation for this RestaurantTable
                    Reservation flag = null;
                    foreach (Reservation r in OwnerStorage.ActiveReservations)
                    {
                        if (r.TableId == thisTable.objectId && ((dtpTakenFrom.Value > r.TakenFrom && dtpTakenFrom.Value < r.TakenTo) || ((dtpTakenFrom.Value.AddHours(Convert.ToInt32(spnDuration.Value))) > r.TakenFrom && (dtpTakenFrom.Value.AddHours(Convert.ToInt32(spnDuration.Value))) < r.TakenTo)
                            || dtpTakenFrom.Value < r.TakenFrom && dtpTakenFrom.Value.AddHours(Convert.ToInt32(spnDuration.Value)) > r.TakenTo))
                        {
                            flag = r;
                        }
                    }

                    //If flag is null then it means that no other clashing reservation was found
                    if (flag == null) 
                    {
                        //All validations are completed. The program can now proceed to creating the program
                        ShowLoading(true);

                        reservation.Number = tbxContact.Text;
                        reservation.Name = tbxName.Text;
                        reservation.TakenFrom = dtpTakenFrom.Value;
                        reservation.TableId = thisTable.objectId;
                        reservation.TakenTo = dtpTakenFrom.Value.AddHours(Convert.ToInt32(spnDuration.Value));
                        reservation.RestaurantId = OwnerStorage.ThisRestaurant.objectId;
                        reservation.UserId = OwnerStorage.CurrentlyLoggedIn.ObjectId;

                        //Since the reservation is created active, these fields are set as is
                        reservation.Active = true;
                        reservation.ReasonForExpiration = "";

                        AsyncCallback<Reservation> callback = new AsyncCallback<Reservation>(
                                            result =>
                                            {
                                                //Success. The form can now close
                                                //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                                                Invoke(new Action(() =>
                                                {
                                                    ShowLoading(false);
                                                    OwnerStorage.FileWriter.WriteLineToFile("User Created Manager Reservation", true);
                                                    OwnerStorage.FileWriter.WriteLineToFile("Name:  " + reservation.Name, false);

                                                    MessageBox.Show(this, "Reservation for " + reservation.Name + " is successfull");
                                                    DialogResult = DialogResult.OK;
                                                    this.Close();
                                                }));
                                            },
                                            fault =>
                                            {
                                                //Something went wrong. An error message will be displayed
                                                //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                                                Invoke(new Action(() =>
                                                {
                                                    ShowLoading(false);
                                                    OwnerStorage.FileWriter.WriteLineToFile("Reservation failed", true);
                                                    OwnerStorage.FileWriter.WriteLineToFile("Error: " + fault.Message.ToString(), false);
                                                    MessageBox.Show(this, "Error: " + fault.Message);
                                                }));
                                            });
                        //Runs the callback
                        Backendless.Data.Of<Reservation>().Save(reservation, callback);
                    }
                    else
                    {
                        //A clashing reservation has been found
                        ShowLoading(false);
                        MessageBox.Show(this, "this Reservation clashes with " + flag.Name + " reservation for " + flag.TakenFrom.ToString("HH:mm") + " to " + flag.TakenTo.ToString("HH:mm"));
                        
                    }
                }
                else
                {
                    //This reservation is outside the restaurant open and close time bounds
                    ShowLoading(false);
                    MessageBox.Show(this, "This reservation is outside the restaurant's set open and close times, please select another time.");
                }
            }
        }
        private void tbxContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            //This only allows numbers to be entered into the textbox
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void CreateReservationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Blocks the "alt F4" capability so that the user cannot close the program while a process is running
            if (e.CloseReason == System.Windows.Forms.CloseReason.UserClosing && pbxLoading.Visible == true)
            {
                e.Cancel = true;
            }
        }
    }
}
