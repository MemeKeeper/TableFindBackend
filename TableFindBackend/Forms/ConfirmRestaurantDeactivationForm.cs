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
    public partial class ConfirmRestaurantDeactivationForm : Form//This form is used to deativate the restaurant for what ever reason the owner whishes to do so
    {
        public ConfirmRestaurantDeactivationForm()
        {
            InitializeComponent();
            //a little last line of secuirity. which forces the user to type in the name of the restaurant in order to confirm deactivation
            lblConfirm.Text = "Please Type: " + OwnerStorage.ThisRestaurant.Name;
        }

        private void pnlBackground_Paint(object sender, PaintEventArgs e)
        {
            //code to colour in the background of the panel with red borders to add to the theme of the form's purpose
            Color color = Color.Red;
            Panel panel = (Panel)sender;
            float width = (float)4.0;
            Pen pen = new Pen(color, width);
            e.Graphics.DrawLine(pen, 0, 0, 0, panel.Height - 0);
            e.Graphics.DrawLine(pen, 0, 0, panel.Width - 0, 0);
            e.Graphics.DrawLine(pen, panel.Width - 1, panel.Height - 1, 0, panel.Height - 1);
            e.Graphics.DrawLine(pen, panel.Width - 1, panel.Height - 1, panel.Width - 1, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)//Button used to close the form if the user wishes to not save changes made
        {
            this.Close();
        }

        private void tbxConfirm_TextChanged(object sender, EventArgs e)
        {
            //code to confirm that the restaurant name confirmation has been entered
            if (tbxConfirm.Text == OwnerStorage.ThisRestaurant.Name)
            {
                btnConfirm.Enabled = true;//enables the confirm button
            }
            else
                btnConfirm.Enabled = false;//keeps the confirm button disabled
        }
        private void ShowLoading(bool toggle)//a method that will appear on all forms. it simulates a loading screen by showing and hiding all neccessary buttons and interface elements.
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
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //This method will confirm that the user entered all neccessary information into the corrisponding textboxes.
            if (tbxEmail.Text == "")//confirms that the user did not leave the email field empty 
            {
                MessageBox.Show(this, "Be sure to fill in a valid Login Email and try again", "Empty Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxConfirm.Text = "";
            }
            else
            {
                if (tbxPassword.Text == "")//confirms that the user did not leave the password field empty
                {
                    MessageBox.Show(this, "Be sure to fill in a valid Login Password and try again", "Empty Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxConfirm.Text = "";
                }
                else
                {
                    //along with the restaurant name being entered as a final confirmation, the password and email has been confirmed to be entered
                    ShowLoading(true);

                    //this callback will confirm if the user exists on the backendless database and confirm the login
                    AsyncCallback<BackendlessUser> callback = new AsyncCallback<BackendlessUser>(
                    user =>
                    {
                        if (user.ObjectId == OwnerStorage.CurrentlyLoggedIn.ObjectId)//confirms that the user who is currently logged in is the same one who entered his/her credentials
                        {
                            //this callback ultimatly deactivates the restaurant object.
                            AsyncCallback<Restaurant> updateObjectCallback = new AsyncCallback<Restaurant>(
                            savedRestaurant =>
                                {
                                    //indicates success and the program will now restart
                                    //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                                    Invoke(new Action(() =>
                                    {
                                        OwnerStorage.FileWriter.WriteLineToFile("Restaurant has been deactivated", false);
                                        OwnerStorage.FileWriter.FormShutDown();
                                        Properties.Settings.Default.defaultRestaurant = -1;
                                        Properties.Settings.Default.Save();
                                    }));

                                    //This callback sends a confirmation email to the user with a reference to both the user and the restaurant objectId
                                    AsyncCallback<MessageStatus> responder = new AsyncCallback<MessageStatus>(
                                      result =>
                                      {
                                          
                                          //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                                          Invoke(new Action(() =>
                                          {
                                              ShowLoading(false);
                                              MessageBox.Show(this, "An email has been sent to your provided email as confirmation of your restaurant being made deactivated. Please use RST-" + OwnerStorage.ThisRestaurant.objectId + " as your restaurant reference and USR-" + OwnerStorage.CurrentlyLoggedIn.ObjectId + " as your user account reference.", "restaurant successfully deactivated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                          }));
                                          //on success the program will restart
                                          Application.Restart();
                                          Environment.Exit(0);
                                      },

                                      fault =>
                                      {
                                          //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                                          Invoke(new Action(() =>
                                          {
                                              ShowLoading(false);
                                              MessageBox.Show(fault.Message.ToString());
                                          }));
                                          //even if the email failed, the program should continue to shut down
                                          Application.Restart();
                                          Environment.Exit(0);
                                      });

                                    // async request. Plain text message to one recipient
                                    List<String> recipients = new List<String>();
                                    recipients.Add(OwnerStorage.CurrentlyLoggedIn.Email);
                                    Backendless.Messaging.SendTextEmail(OwnerStorage.ThisRestaurant.Name + " restaurant Deactivation", "This Email has been sent to confirm that you recently deactivated your " + OwnerStorage.ThisRestaurant.Name + " restaurant in " + OwnerStorage.ThisRestaurant.LocationString + "." +
                                               "\nIf you are seeing this and you did not intentionally deactivated this restaurant please contact the TableFind Development Team.\n\nPlease use RST-" + OwnerStorage.ThisRestaurant.objectId + " as your restaurant reference and USR-" + OwnerStorage.CurrentlyLoggedIn.ObjectId + " as" +
                                               " your user account reference.\n\nRegards,\nThe TableFind Development Team.", recipients, responder);
                                },
                                error =>
                                {
                                    //something went wrong, an error message with the reason will be displayed
                                    //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                                    Invoke(new Action(() =>
                                    {
                                        ShowLoading(false);
                                        MessageBox.Show(error.Message.ToString());
                                    }));
                                });

                            //because backendless demands that a object has to be saved first before it can be updated, this callback first saves the restaurant and then updates the restaurant
                            AsyncCallback<Restaurant> saveObjectCallback = new AsyncCallback<Restaurant>(
                              savedRestaurant =>
                              {
                                  //success, now the object moves to be updated
                                  Backendless.Persistence.Of<Restaurant>().Save(savedRestaurant, updateObjectCallback);
                              },
                              error =>
                              {
                                  //something went wrong, an error message will now be displayed
                                  //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                                  Invoke(new Action(() =>
                                  {
                                      ShowLoading(false);
                                      MessageBox.Show(error.Message.ToString());
                                  }));
                              });

                            OwnerStorage.ThisRestaurant.Active = false;//Deactivating the restaurant object
                            Backendless.Persistence.Of<Restaurant>().Save(OwnerStorage.ThisRestaurant, saveObjectCallback);
                        }
                        else
                        {
                            //This means that the user tried to login with another account's credentials
                            //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                            Invoke(new Action(() =>
                            {
                                ShowLoading(false);
                                MessageBox.Show(this, "You have logged in with another user's login credentials. Please login using your correct login credentials");
                            }));
                        }
                    },
                    fault =>
                    {
                        //something went wrong with the login verification, an error message will now be displayed
                        //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                        Invoke(new Action(() =>
                        {
                            ShowLoading(false);
                            MessageBox.Show(fault.Message.ToString());
                        }));
                    });

                    //preps the user to login
                    String login = tbxEmail.Text;
                    String password = tbxPassword.Text;
                    //runs the login callback
                    Backendless.UserService.Login(login, password, callback);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Button used to close the form if the user wishes to not save changes made
            this.Close();
        }

        private void ConfirmRestaurantDeactivationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this wonderfull piece of code blocks the "alt F4" capability so that the user can not close the program while a process is running
            if (e.CloseReason == System.Windows.Forms.CloseReason.UserClosing && pbxLoading.Visible == true)
            {
                e.Cancel = true;
            }
        }
    }
}
