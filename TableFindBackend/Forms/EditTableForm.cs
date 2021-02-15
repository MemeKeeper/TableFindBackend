using BackendlessAPI;
using BackendlessAPI.Async;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TableFindBackend.Global_Variables;
using TableFindBackend.Models;

namespace TableFindBackend.Forms
{
    //This form will allow an Admin user to modify settings specific to this RestaurantTable object, as well as get access to more features
    public partial class EditTableForm : Form
    {
        RestaurantTable table; //Receives the specific RestaurantTable the form is working with
        private bool availability; //A variable to easily toggle the availability of the RestaurantTable
        MainForm _master; //Receives an instance of the MainForm(parent) so that it can be easily updated after changes are made

        //Constructor which receives an instance of the MainForm and the RestaurantTable which the form is going to work with
        public EditTableForm(RestaurantTable item, MainForm _master)
        {
            table = item;
            this._master = _master;
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            availability = item.Available;

            //Blocks the user from creating reservations for a RestaurantTable that does not exist
            if(item.objectId==null)
            {
                btnViewDetails.Enabled = false;
            }
            //Determines which text should appear on the button
            if (availability == true) 
                btnDisable.Text = "Make Table Unavailable";
            else
                btnDisable.Text = "Make Table Available";

            //Populates some textboxes with the appropriate information
            edtName.Text = item.Name;
            spnSeating.Value = item.Capacity;
            rtbInfo.Text = item.TableInfo;
        }

        //Button used to close the form if the user wishes to not save changes made
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //Method which creates or saves the changes made to the RestaurantTable
        private void btnSave_Click(object sender, EventArgs e)
        {
            showLoading(true);

            //Assigns all information to the RestaurantTable object
            table.Name = edtName.Text;
            table.Capacity = Convert.ToInt32(spnSeating.Value);
            table.Available = availability;
            table.TableInfo = rtbInfo.Text;

            AsyncCallback<RestaurantTable> updateObjectCallback = new AsyncCallback<RestaurantTable>(
              savedTable =>
              {
                  //Success, the RestaurantTable has been successfully created/updated and the form will then be closed
                  //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                  Invoke(new Action(() =>
                  {
                      showLoading(false);
                      MessageBox.Show(this, table.Name + " has been updated!");
                      DialogResult = DialogResult.OK;
                      this.table.objectId = savedTable.objectId;
                      this.Close();
                  }));
              },
              error =>
              {
                  //Something went wrong. An error message will now be displayed
                  //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                  Invoke(new Action(() =>
                  {
                      showLoading(false);
                      MessageBox.Show(this, "Error: " + error.Message);
                  }));
              });

            AsyncCallback<RestaurantTable> saveObjectCallback = new AsyncCallback<RestaurantTable>(
              savedTable =>
              {
                  //Backendless requires that an object first has to be saved before it can be updated
                  Backendless.Persistence.Of<RestaurantTable>().Save(savedTable, updateObjectCallback);

              },
              error =>
              {
                  //Something went wrong. An error message will now be displayed
                  //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                  Invoke(new Action(() =>
                  {
                      showLoading(false);
                      MessageBox.Show(this, "Error: " + error.Message);
                  }));
              });

            //The RestaurantTable gets saved
            Backendless.Persistence.Of<RestaurantTable>().Save(table, saveObjectCallback);

        }

        //A method that will appear on all forms. It simulates a loading screen by showing and hiding all necessary buttons and interface elements
        private void showLoading(bool activate)
        {
            if (activate == true)
            {
                pbxLoading.Visible = true;
                btnCancel.Enabled = false;
                btnClose.Enabled = false;
                btnDelete.Enabled = false;
                btnSave.Enabled = false;
                btnViewDetails.Enabled = false;
                btnDisable.Enabled = false;
                edtName.Enabled = false;
                spnSeating.Enabled = false;
                rtbInfo.Enabled = false;

            }
            else
            {
                pbxLoading.Visible = false;
                btnCancel.Enabled = true;
                btnClose.Enabled = true;
                btnDelete.Enabled = true;
                btnSave.Enabled = true;
                btnViewDetails.Enabled = true;
                btnDisable.Enabled = true;
                edtName.Enabled = true;
                spnSeating.Enabled = true;
                rtbInfo.Enabled = true;
            }
        }

        //This method will allow the Admin user to completely remove the RestaurantTable, but only if there are no reservations for the RestaurantTable and it has been made unavailable
        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<Reservation> rList = new List<Reservation>();

            //Checks if there are any reservations under this specific RestaurantTable
            foreach (Reservation r in OwnerStorage.ActiveReservations)
            {
                if (r.TableId == table.objectId)
                {
                    //Temp list for the amount of reservations made for this RestaurantTable
                    rList.Add(r); 
                }
            }
            //Only allows the user to remove the RestaurnatTable on these conditions mentioned above
            if (availability == false && rList.Count == 0) 
            {

                //Confirms if the user wishes to remove the RestaurantTable
                DialogResult result;
                result = MessageBox.Show(this, "Are you sure you would like to remove '" + table.Name + "'?", "Remove '" + table.Name + "' ?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                //The user said 'Yes'
                if (result == DialogResult.Yes) 
                {
                    showLoading(true);

                    AsyncCallback<long> deleteObjectCallback = new AsyncCallback<long>(
                    deletionTime =>
                    {
                        //The RestaurantTable has been successfully removed
                        //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                        Invoke(new Action(() =>
                        {
                            showLoading(false);
                            OwnerStorage.FileWriter.WriteLineToFile("User removed table ", true);
                            OwnerStorage.FileWriter.WriteLineToFile("Name:  " + table.Name, false);                            
                            MessageBox.Show(this, table.Name + " has been removed");
                            DialogResult = DialogResult.Yes;
                            this.Close();
                        }));                       
                    },
                    error =>
                    {
                        //Something went wrong, an error message will now be displayed
                        //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                        Invoke(new Action(() =>
                        {
                            showLoading(false);
                            MessageBox.Show(this, "Error: " + error.Message);
                        }));
                    });

                    AsyncCallback<RestaurantTable> saveObjectCallback = new AsyncCallback<RestaurantTable>(
                      savedTable =>
                      {
                          //Backendless requires you to save an object before it can be deleted
                          Backendless.Persistence.Of<RestaurantTable>().Remove(savedTable, deleteObjectCallback);
                      },
                      error =>
                      {
                          //Something went wrong, an error message will be displayed
                          //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                          Invoke(new Action(() =>
                          {
                              showLoading(false);
                              MessageBox.Show(this, "Error: " + error.Message);
                          }));
                      }
                    );
                    //Backendless requires you to save an object before it can be deleted
                    Backendless.Persistence.Of<RestaurantTable>().Save(table, saveObjectCallback);
                }
            }
            else
            {
                //Message telling the user that he/she has to wait for all reservations to be removed and to make the RestaurantTable unavailable and try again
                MessageBox.Show("The table has to be made unavailable and have no reservations under it in order to remove this table from the restaurant. Please make these changes before trying again.");
            }


        }

        //Will launch the ReservationsForm where all reservations for this current RestaurantTable can be viewed, reservations can be made and removed
        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            ReservationsForm bookings = new ReservationsForm(table, _master);
            bookings.ShowDialog();
        }

        //Toggles availability of the RestaurantTable. It changes text and logs informtion about these actions
        private void btnDisable_Click(object sender, EventArgs e)
        {
            
            if (availability == false)
            {
                btnDisable.Text = "Make Table Unavailable";
                availability = true;

                OwnerStorage.FileWriter.WriteLineToFile("User made table '" + table.Name + "' available", true);
            }
            else
            {
                btnDisable.Text = "Make Table Available";
                availability = false;

                OwnerStorage.FileWriter.WriteLineToFile("User made table '" + table.Name + "' unavailable", true);
            }
        }

        //A method which is used outside of this form. It retrieves the fully edited table and updates it on the MainForm for performance 
        public RestaurantTable RetreiveEditedTable()
        {
            return table;
        }

        private void EditTableForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Blocks the "alt F4" capability so that the user cannot close the program while a process is running
            if (e.CloseReason == System.Windows.Forms.CloseReason.UserClosing && pbxLoading.Visible == true)
            {
                e.Cancel = true;
            }
        }

        //Button used to close the form if the user wishes to not save changes made
        private void btnX_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}