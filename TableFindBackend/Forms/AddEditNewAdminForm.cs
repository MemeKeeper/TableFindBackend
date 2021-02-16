using BackendlessAPI;
using BackendlessAPI.Async;
using System;
using System.Windows.Forms;
using TableFindBackend.Global_Variables;
using TableFindBackend.Models;

namespace TableFindBackend.Forms
{
    //This form is to either create or modify Admin Users
    public partial class AddEditNewAdminForm : Form
    {
        //Public property that makes retrieving the modified MenuItem effective and easy
        public AdminPins TempAdmin { get; set; } 

        public AddEditNewAdminForm(AdminPins a)
        {
            InitializeComponent();

            TempAdmin = a;
            //Determines if a new Admin user is being created or an existing one is being modified
            if (a == null)
            {
                //Addinng New Admin User
                btnRemoveDeactivate.Visible = false;
            }
            else
            {
                //Editing Existing Admin User
                lblTitle.Text = "Editing Admin User";
                tbxName.Text = a.UserName;
                tbxContact.Text = a.ContactNumber;
                tbxPinCode.Text = a.PinCode.ToString();

                if (a.Active == true)
                {
                    //Editing Active Admin User
                    tbxConfirmPin.Enabled = false;
                    lblDescription.Text = "You can edit your Admin details below.\n \n Remember that the Admin PIN should only include numerical digits wih a minimum of at least 4 digits and a maximum of 10 digits.";
                    btnCreateNewAdmin.Text = "Update";
                }
                else
                {
                    //Editing Deactivated Admin User
                    lblDescription.Text = "You can reactivate your Admin below.";
                    tbxConfirmPin.Enabled = false;
                    tbxContact.Enabled = false;
                    tbxName.Enabled = false;
                    tbxPinCode.Enabled = false;

                    btnCreateNewAdmin.Text = "Reactivate";
                    btnRemoveDeactivate.Text = "Delete";
                }
            }
        }

        //Button used to close the form if the user wishes to not save changes made
        private void btnExit_Click(object sender, EventArgs e) 
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //Forces the user to only be able to enter numbers instead of any other character
        private void tbxPinCode_KeyPress(object sender, KeyPressEventArgs e) 
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        //This button is used for both creating a new user or reactivating a deactivated user
        private void btnCreateNewAdmin_Click(object sender, EventArgs e) 
        {
            showLoading(true);

            #region callbacks are kept here to rest
            AsyncCallback<AdminPins> reactivateObjectCallback = new AsyncCallback<AdminPins>(
                savedAdminPins =>
                {
                    //Has been reactivated successfully, will then log all information and close the form
                    //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                    Invoke(new Action(() =>
                    {
                        showLoading(false);
                        OwnerStorage.FileWriter.WriteLineToFile("User reactivated Admin ", true);
                        OwnerStorage.FileWriter.WriteLineToFile("Name:  " + TempAdmin.UserName, false);
                        OwnerStorage.LogInfo.Add(TempAdmin.UserName + " admin User was reactivated");
                        OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                        MessageBox.Show(this, TempAdmin.UserName + " has been reactivated");
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }));
                },
                error =>
                {
                    //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                    Invoke(new Action(() =>
                    {
                        showLoading(false);
                        MessageBox.Show(this, "Error: " + error.Message);
                    }));
                });

        AsyncCallback<AdminPins> saveReactivatedObjectCallback = new AsyncCallback<AdminPins>(
            savedAdminPins =>
            {
                //The object has to be saved first in order to be updated
                Backendless.Persistence.Of<AdminPins>().Save(savedAdminPins, reactivateObjectCallback);
            },
            error =>
            {
                //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                Invoke(new Action(() =>
                {
                    showLoading(false);
                    MessageBox.Show(this, "Error: " + error.Message);
                }));
            });
        AsyncCallback<AdminPins> callback = new AsyncCallback<AdminPins>(
            result =>
            {
                //Perform other operations that indicate the success of the creation (logging, showing a message, closing the form)
                OwnerStorage.ListOfAdmins.Add(result);
                //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                Invoke(new Action(() =>
                {
                    // object has been saved
                    showLoading(false);
                    MessageBox.Show(this, "Admin User has been successfully created.");
                    OwnerStorage.LogInfo.Add(TempAdmin.UserName + " admin User was Created");
                    OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                    this.DialogResult = DialogResult.OK;
                }));
            },

            fault =>
            {
                //Perform other operations that indicate the failure of the creation (showing a message)
                //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                Invoke(new Action(() =>
                {
                    // server reported an error
                    showLoading(false);
                    MessageBox.Show(this, "error: " + fault.Message);
                }));
            });


            AsyncCallback<AdminPins> updateObjectCallback = new AsyncCallback<AdminPins>(
                savedAdminPin =>
                {
                    //Admin has been successfully created
                    //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                    Invoke(new Action(() =>
                    {
                        showLoading(false);
                        MessageBox.Show(this, "Admin PIN has been updated");
                        OwnerStorage.LogInfo.Add(TempAdmin.UserName + " admin User was Updated");
                        OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                        this.DialogResult = DialogResult.OK;
                    }));
                },
                error =>
                {
                    //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                    Invoke(new Action(() =>
                    {
                        //server reported an error
                        showLoading(false);
                        MessageBox.Show(this, "error: " + error.Message);
                    }));
                });

            AsyncCallback<AdminPins> saveObjectCallback = new AsyncCallback<AdminPins>(
                savedAdminPin =>
                {
                        //The Object has to be saved first in order to update the object
                        Backendless.Persistence.Of<AdminPins>().Save(savedAdminPin, updateObjectCallback);
                },
                error =>
                {
                        //server reported an error
                        //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                        Invoke(new Action(() =>
                    {
                        showLoading(false);
                        MessageBox.Show(this, "error: " + error.Message);
                    }));
                });

            #endregion

            //Performs validation to determine if all TextBoxes are filled in
            if (tbxName.Text == ""
                || tbxContact.Text == ""
                || tbxPinCode.Text == "") 
            {
                showLoading(false);
                MessageBox.Show(this, "Please fill in all fields.");
            }
            else
            {
                //Performs Contact validation
                if (tbxContact.TextLength == 10) 
                {
                    //Performs validation to determine if the PIN is at least 4 digits long
                    if (tbxPinCode.TextLength < 4) 
                    {
                        showLoading(false);
                        MessageBox.Show(this, "Your PIN number must be at least 4 digits in length.");
                        tbxPinCode.Text = "";
                        tbxConfirmPin.Text = "";
                    }
                    else
                    {
                        //Determines if a new Admin user is being made because the parent form sends in a Null object
                        if (TempAdmin == null) 
                        {
                            //Performs validation to determine if the PIN codes both match
                            if (tbxPinCode.Text.Equals(tbxConfirmPin.Text)) 
                            {
                                //Validates that the confirm PIN is entered
                                if (tbxConfirmPin.Text != "") 
                                {
                                    //Create New Admin user
                                    bool flag = false;
                                    foreach (AdminPins a in OwnerStorage.ListOfAdmins)
                                    {
                                        if (a.PinCode.ToString().Equals(tbxPinCode.Text))
                                        {
                                            flag = true;
                                        }
                                    }
                                    //flag is used to determine if the PIN specified by the user already exists. The way the program tracks which user logs in is his/her PINCode, therefore it has to be unique
                                    if (flag == false) 
                                    {
                                        TempAdmin = new AdminPins();
                                        TempAdmin.UserName = tbxName.Text;
                                        TempAdmin.ContactNumber = tbxContact.Text;
                                        TempAdmin.PinCode = tbxPinCode.Text;
                                        TempAdmin.RestaurantId = OwnerStorage.ThisRestaurant.objectId;
                                        TempAdmin.Active = true;
                                        Backendless.Data.Of<AdminPins>().Save(TempAdmin, callback);
                                    }
                                    //A duplicate PIN code has been detected, so messages are to be displayed
                                    else
                                    {
                                        showLoading(false);
                                        MessageBox.Show(this, "There is already an administrator with this PIN, please use a different PIN");
                                        tbxPinCode.Text = "";
                                        tbxConfirmPin.Text = "";
                                    }

                                }
                                //the user failed to enter the confirm pin
                                else
                                {
                                    showLoading(false);
                                    MessageBox.Show(this, "Please be sure to Confirm the pin");
                                }
                            }
                            else
                            {
                                showLoading(false);
                                MessageBox.Show(this, "Please be sure to fill in both spaces for the pin Code");
                            }

                        }
                        //Edit Existing Admin
                        else
                        {
                            //Determines whether the Admin should be reactivated or just edited
                            if (TempAdmin.Active == false) 
                            {
                                TempAdmin.Active = true;
                                Backendless.Persistence.Of<AdminPins>().Save(TempAdmin, saveReactivatedObjectCallback);
                            }
                            //Admin is to be updated
                            else
                            {

                                bool flag = false;//flag is used to determine if the PIN specified by the user already exists. The way the program tracks which user logs in is his/her PINCode, therefore it has to be unique
                                foreach (AdminPins a in OwnerStorage.ListOfAdmins)
                                {
                                    if (a.PinCode.ToString().Equals(tbxPinCode.Text)
                                        && a.PinCode != TempAdmin.PinCode)
                                    {
                                        flag = true;
                                    }
                                }
                                if (tbxPinCode.Text.Equals(TempAdmin.PinCode) == true)
                                {                                   
                                    if (flag == false)
                                    {
                                        TempAdmin.UserName = tbxName.Text;
                                        TempAdmin.ContactNumber = tbxContact.Text;
                                        TempAdmin.PinCode = tbxPinCode.Text;
                                        TempAdmin.RestaurantId = OwnerStorage.ThisRestaurant.objectId;
                                        Backendless.Persistence.Of<AdminPins>().Save(TempAdmin, saveObjectCallback);
                                    }
                                    //An Admin with this PIN already exists
                                    else
                                    {
                                        showLoading(false);
                                        MessageBox.Show(this, "There is already an administrator with this PIN, please use a different PIN");
                                        tbxPinCode.Text = "";
                                        tbxConfirmPin.Text = "";

                                    }
                                }
                                else//means that the pin was changed
                                {
                                    if (tbxConfirmPin.Text.Equals(tbxPinCode.Text) == true)
                                    {
                                        TempAdmin.UserName = tbxName.Text;
                                        TempAdmin.ContactNumber = tbxContact.Text;
                                        TempAdmin.PinCode = tbxPinCode.Text;
                                        TempAdmin.RestaurantId = OwnerStorage.ThisRestaurant.objectId;
                                        Backendless.Persistence.Of<AdminPins>().Save(TempAdmin, saveObjectCallback);
                                    }
                                    else//the user did not confirm the pin
                                    {
                                        showLoading(false);
                                        MessageBox.Show(this, "It seems that you have changed the pin, please be sure to confirm the pin code");
                                        tbxPinCode.Text = "";
                                        tbxConfirmPin.Text = "";
                                    }
                                }
                            }
                        }
                    }
                }
                //The contact number is of incorrect format, so messages are being displayed
                else
                {
                    showLoading(false);
                    MessageBox.Show(this, "The Contact number you have entered is invalid");
                }
            }
        }

        //Button used to close the form if the user wishes to not save changes made
        private void btnCancel_Click(object sender, EventArgs e) 
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void tbxContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Ensures that only digits can be entered into the contact TextBox
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
               (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        //A method that will appear on all forms. It simulates a loading screen by showing and hiding all necessary buttons and interface elements
        private void showLoading(bool toggle)
        {
            if (toggle == true)
            {
                pbxLoading.Visible = true;
                btnCancel.Enabled = false;
                btnExit.Enabled = false;
                btnCreateNewAdmin.Enabled = false;
                btnRemoveDeactivate.Enabled = false;
            }
            else
            {
                pbxLoading.Visible = false;
                btnCancel.Enabled = true;
                btnExit.Enabled = true;
                btnCreateNewAdmin.Enabled = true;
                btnRemoveDeactivate.Enabled = true;
            }

        }
        //Multifunctional button that can both deactivate an Admin object and remove it
        private void btnRemoveAdmin_Click(object sender, EventArgs e)
        {
            //This will permanently remove the deactivated record off of the database
            if (TempAdmin.Active == false)
            {
                DialogResult result = MessageBox.Show(this, "Are you sure you wish permanently remove " + TempAdmin.UserName + " as administrator?", "Removing Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    showLoading(true);
                    AsyncCallback<long> deleteObjectCallback = new AsyncCallback<long>(
                    deletionTime =>
                    {
                        //Success, the program will now log the event, show a success message and close the form
                        //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                        Invoke(new Action(() =>
                        {
                            showLoading(false);
                            OwnerStorage.FileWriter.WriteLineToFile("User deleted Admin ", true);
                            OwnerStorage.FileWriter.WriteLineToFile("Name:  " + TempAdmin.UserName, false);
                            OwnerStorage.LogInfo.Add(TempAdmin.UserName + " admin User was deleted");
                            OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                            MessageBox.Show(this, TempAdmin.UserName + " has been removed");
                            OwnerStorage.ListOfAdmins.Remove(TempAdmin);
                            DialogResult = DialogResult.OK;
                            this.Close();
                        }));


                    },
                error =>
                {
                    //Something went wrong, na error message will now be displayed
                    //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                    Invoke(new Action(() =>
                    {
                        showLoading(false);
                        MessageBox.Show(this, "Error: " + error.Message);
                    }));
                });

                    AsyncCallback<AdminPins> saveObjectCallback = new AsyncCallback<AdminPins>(
                      savedAdmin =>
                      {
                          //The object has to be saved first in order to be deleted
                          Backendless.Persistence.Of<AdminPins>().Remove(savedAdmin, deleteObjectCallback);
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
                      }
                    );
                    //Runs the callback which removes the object
                    Backendless.Persistence.Of<AdminPins>().Save(TempAdmin, saveObjectCallback);
                }
            }
            else
            {

                DialogResult result = MessageBox.Show(this, "Are you sure you wish to deactivate " + TempAdmin.UserName + "?", "Deactivating Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    showLoading(true);
                    AsyncCallback<AdminPins> updateObjectCallback = new AsyncCallback<AdminPins>(
                    savedAdminPins =>
                    {
                        //Success, the program will now log the event, show a success message and close the form
                        //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                        Invoke(new Action(() =>
                        {
                            showLoading(false);
                            OwnerStorage.FileWriter.WriteLineToFile("User deactivated Admin ", true);
                            OwnerStorage.FileWriter.WriteLineToFile("Name:  " + TempAdmin.UserName, false);
                            OwnerStorage.LogInfo.Add(TempAdmin.UserName + " admin User was deactivated");
                            OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                            MessageBox.Show(this, TempAdmin.UserName + " has been deactivated");
                            DialogResult = DialogResult.OK;
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

                    AsyncCallback<AdminPins> saveObjectCallback = new AsyncCallback<AdminPins>(
                      savedAdminPins =>
                      {
                          Backendless.Persistence.Of<AdminPins>().Save(savedAdminPins, updateObjectCallback);
                      },
                      error =>
                      {
                          //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                          Invoke(new Action(() =>
                          {
                              showLoading(false);
                              MessageBox.Show(this, "Error: " + error.Message);
                          }));
                      });
                    //Makes the small ajustment that "Deactivates" an Admin
                    TempAdmin.Active = false;
                    Backendless.Persistence.Of<AdminPins>().Save(TempAdmin, saveObjectCallback);
                }
            }
        }

        private void AddEditNewAdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Blocks the "alt F4" capability so that the user cannot close the program while a process is running
            if (e.CloseReason == System.Windows.Forms.CloseReason.UserClosing && pbxLoading.Visible == true)
            {
                e.Cancel = true;
            }
        }

        //method that adds slightly more security to the confirm PINs when updating the AdminPin
        private void tbxPinCode_TextChanged(object sender, EventArgs e)
        {
            if (TempAdmin != null)
            {
                if (tbxPinCode.Text.Equals(TempAdmin.PinCode) != true)
                {
                    tbxConfirmPin.Enabled = true;
                }
                else
                {
                    tbxConfirmPin.Enabled = false;
                    tbxConfirmPin.Text = "";
                }
            }
            else
            {
                tbxConfirmPin.Enabled = true;
            }
        }
    }
}
