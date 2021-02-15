using BackendlessAPI;
using BackendlessAPI.Async;
using BackendlessAPI.Messaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TableFindBackend.Global_Variables;
using TableFindBackend.Models;

namespace TableFindBackend.Forms
{
    //This form is used to deativate the restaurant 
    public partial class ConfirmRestaurantDeactivationForm : Form 
    {
        public ConfirmRestaurantDeactivationForm()
        {
            InitializeComponent();
            //Forces the user to type in the name of the restaurant in order to confirm deactivation
            lblConfirm.Text = "Please Type: " + OwnerStorage.ThisRestaurant.Name;
        }

        private void pnlBackground_Paint(object sender, PaintEventArgs e)
        {
            //Sets the background colour of the panel with red borders to add to the theme of the form's purpose
            Color color = Color.Red;
            Panel panel = (Panel)sender;
            float width = (float)4.0;
            Pen pen = new Pen(color, width);
            e.Graphics.DrawLine(pen, 0, 0, 0, panel.Height - 0);
            e.Graphics.DrawLine(pen, 0, 0, panel.Width - 0, 0);
            e.Graphics.DrawLine(pen, panel.Width - 1, panel.Height - 1, 0, panel.Height - 1);
            e.Graphics.DrawLine(pen, panel.Width - 1, panel.Height - 1, panel.Width - 1, 0);
        }

        //Button used to close the form if the user wishes to not save changes made
        private void btnClose_Click(object sender, EventArgs e) 
        {
            this.Close();
        }

        private void tbxConfirm_TextChanged(object sender, EventArgs e)
        {
            //Confirms that the restaurant name confirmation has been entered
            if (tbxConfirm.Text == OwnerStorage.ThisRestaurant.Name)
            {
                //Enables the confirm button
                btnConfirm.Enabled = true; 
            }
            else
                //Keeps the confirm button disabled
                btnConfirm.Enabled = false; 
        }

        //A method that will appear on all forms. It simulates a loading screen by showing and hiding all neccessary buttons and interface elements
        private void ShowLoading(bool toggle) 
        {
            if (toggle==true)
            {
                pbxLoading.Visible = true;
                btnCancel.Enabled = false;
                btnClose.Enabled = false;
                btnConfirm.Enabled = false;
            }
            else
            {
                pbxLoading.Visible = false;
                btnCancel.Enabled = true;
                btnClose.Enabled = true;
                btnConfirm.Enabled = true;
            }
        }

        //This method will confirm that the user entered all neccessary information into the corresponding textboxes
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //Confirms that the user should not leave the email field empty 
            if (tbxEmail.Text == "") 
            {
                MessageBox.Show(this, "Be sure to fill in a valid Login Email and try again", "Empty Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxConfirm.Text = "";
            }
            else
            {
                //Confirms that the user should not leave the password field empty
                if (tbxPassword.Text == "") 
                {
                    MessageBox.Show(this, "Be sure to fill in a valid Login Password and try again", "Empty Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxConfirm.Text = "";
                }
                else
                {
                    ShowLoading(true);

                    //This callback will confirm if the user exists on the Backendless database and confirm the login
                    AsyncCallback<BackendlessUser> callback = new AsyncCallback<BackendlessUser>(
                    user =>
                    {
                        //Confirms that the user who is currently logged in is the same one who entered his/her credentials
                        if (user.ObjectId == OwnerStorage.CurrentlyLoggedIn.ObjectId) 
                        {
                            //This callback deactivates the restaurant object
                            AsyncCallback<Restaurant> updateObjectCallback = new AsyncCallback<Restaurant>(
                            savedRestaurant =>
                                {
                                    //Indicates success and the program will now restart
                                    //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                                    Invoke(new Action(() =>
                                    {
                                        OwnerStorage.FileWriter.WriteLineToFile("Restaurant has been deactivated", false);
                                        OwnerStorage.FileWriter.FormShutDown();
                                        Properties.Settings.Default.defaultRestaurant = -1;
                                        Properties.Settings.Default.Save();
                                    }));

                                    //This callback sends a confirmation email to the user with a reference of both the user and the restaurant objectId
                                    AsyncCallback<MessageStatus> responder = new AsyncCallback<MessageStatus>(
                                      result =>
                                      {
                                          //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                                          Invoke(new Action(() =>
                                          {
                                              ShowLoading(false);
                                              MessageBox.Show(this, "An email has been sent to your provided email as confirmation of your restaurant being made deactivated. Please use RST-" + OwnerStorage.ThisRestaurant.objectId + " as your restaurant reference and USR-" + OwnerStorage.CurrentlyLoggedIn.ObjectId + " as your user account reference.", "restaurant successfully deactivated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                          }));
                                          //On success the program will restart
                                          Application.Restart();
                                          Environment.Exit(0);
                                      },

                                      fault =>
                                      {
                                          //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                                          Invoke(new Action(() =>
                                          {
                                              ShowLoading(false);
                                              MessageBox.Show(fault.Message.ToString());
                                          }));
                                          //Even if the email failed, the program should continue to shut down
                                          Application.Restart();
                                          Environment.Exit(0);
                                      });

                                    //Async request. Plain text message to one recipient
                                    List<String> recipients = new List<String>();
                                    recipients.Add(OwnerStorage.CurrentlyLoggedIn.Email);
                                    Backendless.Messaging.SendTextEmail(OwnerStorage.ThisRestaurant.Name + " restaurant Deactivation", "This Email has been sent to confirm that you recently deactivated your " + OwnerStorage.ThisRestaurant.Name + " restaurant in " + OwnerStorage.ThisRestaurant.LocationString + "." +
                                               "\nIf you are seeing this and you did not intentionally deactivated this restaurant please contact the TableFind Development Team.\n\nPlease use RST-" + OwnerStorage.ThisRestaurant.objectId + " as your restaurant reference and USR-" + OwnerStorage.CurrentlyLoggedIn.ObjectId + " as" +
                                               " your user account reference.\n\nRegards,\nThe TableFind Development Team.", recipients, responder);
                                },
                                error =>
                                {
                                    //Something went wrong, an error message with the reason will be displayed
                                    //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                                    Invoke(new Action(() =>
                                    {
                                        ShowLoading(false);
                                        MessageBox.Show(error.Message.ToString());
                                    }));
                                });

                            //This callback first saves the restaurant and then updates the restaurant
                            AsyncCallback<Restaurant> saveObjectCallback = new AsyncCallback<Restaurant>(
                              savedRestaurant =>
                              {
                                  //Success, now the object moves to be updated
                                  Backendless.Persistence.Of<Restaurant>().Save(savedRestaurant, updateObjectCallback);
                              },
                              error =>
                              {
                                  //Something went wrong, an error message will now be displayed
                                  //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                                  Invoke(new Action(() =>
                                  {
                                      ShowLoading(false);
                                      MessageBox.Show(error.Message.ToString());
                                  }));
                              });

                            //Deactivating the restaurant object
                            OwnerStorage.ThisRestaurant.Active = false; 
                            Backendless.Persistence.Of<Restaurant>().Save(OwnerStorage.ThisRestaurant, saveObjectCallback);
                        }
                        else
                        {
                            //This means that the user tried to login with another account's credentials
                            //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                            Invoke(new Action(() =>
                            {
                                ShowLoading(false);
                                MessageBox.Show(this, "You have logged in with another user's login credentials. Please login using your correct login credentials");
                            }));
                        }
                    },
                    fault =>
                    {
                        //Something went wrong with the login verification, an error message will now be displayed
                        //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                        Invoke(new Action(() =>
                        {
                            ShowLoading(false);
                            MessageBox.Show(fault.Message.ToString());
                        }));
                    });

                    //Prepares the user to login
                    String login = tbxEmail.Text;
                    String password = tbxPassword.Text;
                    //Runs the login callback
                    Backendless.UserService.Login(login, password, callback);
                }
            }
        }

        //Button used to close the form if the user wishes to not save changes made
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConfirmRestaurantDeactivationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Blocks the "alt F4" capability so that the user cannot close the program while a process is running
            if (e.CloseReason == System.Windows.Forms.CloseReason.UserClosing && pbxLoading.Visible == true)
            {
                e.Cancel = true;
            }
        }
    }
}
