using BackendlessAPI;
using BackendlessAPI.Async;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TableFindBackend.Global_Variables;
using TableFindBackend.Models;

namespace TableFindBackend.Forms
{
    public partial class EditTableForm : Form
    {
        //this form will allow an admin user to modify settings specific to this RestaurantTable object, as well as get access to more features

        RestaurantTable table;//receives the specific table the form is working with
        private bool availability;// a variable to easily toggle the availability of the table
        MainForm _master;// receives an instance of the mainForm so that it can be easily updated after changes are made.
        public EditTableForm(RestaurantTable item, MainForm _master)
        {
            //constructor which receives an instance of the mainform (parent) and the table which the form is going to work with
            table = item;
            this._master = _master;
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            availability = item.Available;

            if (availability == true)//just determines which text should appear  on the button
                btnDisable.Text = "Make Table Unavailable";
            else
                btnDisable.Text = "Make Table Available";

            //populates some textboxes with the appropriate information
            edtName.Text = item.Name;
            spnSeating.Value = item.Capacity;
            rtbInfo.Text = item.TableInfo;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Button used to close the form if the user wishes to not save changes made
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //simple method which creates or saves the changes made to the RestaurantTable
            showLoading(true);

            //assigns all information to the RestaurantTable object.
            table.Name = edtName.Text;
            table.Capacity = Convert.ToInt32(spnSeating.Value);
            table.Available = availability;
            table.TableInfo = rtbInfo.Text;

            AsyncCallback<RestaurantTable> updateObjectCallback = new AsyncCallback<RestaurantTable>(
              savedTable =>
              {
                  //success, the table has been successfully created/updated and the form will then be closed
                  //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
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
                  //something went wrong. An error message will now be displayed
                  //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                  Invoke(new Action(() =>
                  {
                      showLoading(false);
                      MessageBox.Show(this, "Error: " + error.Message);
                  }));
              });

            AsyncCallback<RestaurantTable> saveObjectCallback = new AsyncCallback<RestaurantTable>(
              savedTable =>
              {
                  //backendless demands that an object first has to be saved before it can be updated
                  Backendless.Persistence.Of<RestaurantTable>().Save(savedTable, updateObjectCallback);

              },
              error =>
              {
                  //something went wrong. An error message will now be displayed
                  //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                  Invoke(new Action(() =>
                  {
                      showLoading(false);
                      MessageBox.Show(this, "Error: " + error.Message);
                  }));
              });

            //the RestaurantTable finally gets saved
            Backendless.Persistence.Of<RestaurantTable>().Save(table, saveObjectCallback);

        }
        private void showLoading(bool activate)
        {
            //a method that will appear on all forms. it simulates a loading screen by showing and hiding all neccessary buttons and interface elements.
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //this method will allow the admin user to completly remove the RestaurantTable, but only if there are no reservations for the table and it has been made Unavailable

            List<Reservation> rList = new List<Reservation>();

            //this block of code checks if there are any reservations under this specific table
            foreach (Reservation r in OwnerStorage.ActiveReservations)
            {
                if (r.TableId == table.objectId)
                {
                    rList.Add(r);//temp list for the amount of reservations made for this table
                }
            }
            if (availability == false && rList.Count == 0)// only allows the user to remove the table on these conditions mentioned above
            {

                //confirms with the user if he/she really wishes to remove the table
                DialogResult result;
                result = MessageBox.Show(this, "Are you sure you would like to remove '" + table.Name + "'?", "Remove '" + table.Name + "' ?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)//the user said 'Yes'
                {
                    showLoading(true);

                    AsyncCallback<long> deleteObjectCallback = new AsyncCallback<long>(
                    deletionTime =>
                    {
                        //The table has been successfully removed
                        //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
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
                        //something went wrong, an error message will now be displayed
                        //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
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
                          //something went wrong, an error message will be displayed
                          //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
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
                //message telling the user that he/she has to wait for all reservations to be removed and to make the table unavailable and try again
                MessageBox.Show("The table has to be made unavailable and have no reservations under it in order to remove this table from the restaurant. Please make these changes before trying again.");
            }


        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            //will launch a ReservationsForm where all reservations for this current table can be viewed, reservations can be made and removed
            ReservationsForm bookings = new ReservationsForm(table, _master);
            bookings.ShowDialog();
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            //just toggles between availability of the table. it Changes text and logs informtion about the actions
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
        public RestaurantTable RetreiveEditedTable()
        {
            //a method which is used outside of this form. it only retreives the fully edited table and updates it on the mainform of performance reasons
            return table;
        }

        private void EditTableForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this wonderfull piece of code blocks the "alt F4" capability so that the user can not close the program while a process is running
            if (e.CloseReason == System.Windows.Forms.CloseReason.UserClosing && pbxLoading.Visible == true)
            {
                e.Cancel = true;
            }
        }
        private void btnX_Click(object sender, EventArgs e)
        {
            //Button used to close the form if the user wishes to not save changes made
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}