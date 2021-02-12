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
    public partial class ConfirmRestaurantDeactivationForm : Form
    {
        public ConfirmRestaurantDeactivationForm()
        {
            InitializeComponent();
            lblConfirm.Text = "Please Type: " + OwnerStorage.ThisRestaurant.Name;
        }

        private void pnlBackground_Paint(object sender, PaintEventArgs e)
        {
            Color color = Color.Red;
            Panel panel = (Panel)sender;
            float width = (float)4.0;
            Pen pen = new Pen(color, width);
            e.Graphics.DrawLine(pen, 0, 0, 0, panel.Height - 0);
            e.Graphics.DrawLine(pen, 0, 0, panel.Width - 0, 0);
            e.Graphics.DrawLine(pen, panel.Width - 1, panel.Height - 1, 0, panel.Height - 1);
            e.Graphics.DrawLine(pen, panel.Width - 1, panel.Height - 1, panel.Width - 1, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbxConfirm_TextChanged(object sender, EventArgs e)
        {
            if (tbxConfirm.Text == OwnerStorage.ThisRestaurant.Name)
            {
                btnConfirm.Enabled = true;
            }
            else
                btnConfirm.Enabled = false;
        }
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
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (tbxEmail.Text == "")
            {
                MessageBox.Show(this, "Be sure to fill in a valid Login Email and try again", "Empty Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxConfirm.Text = "";
            }
            else
            {
                if (tbxPassword.Text == "")
                {
                    MessageBox.Show(this, "Be sure to fill in a valid Login Password and try again", "Empty Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxConfirm.Text = "";
                }
                else
                {
                    ShowLoading(true);
                    AsyncCallback<BackendlessUser> callback = new AsyncCallback<BackendlessUser>(
                    user =>
                    {
                        if (user.ObjectId == OwnerStorage.CurrentlyLoggedIn.ObjectId)
                        {

                            AsyncCallback<Restaurant> updateObjectCallback = new AsyncCallback<Restaurant>(
                            savedRestaurant =>
                                {
                                    Invoke(new Action(() =>
                                    {

                                        OwnerStorage.FileWriter.WriteLineToFile("Restaurant has been deactivated", false);
                                        OwnerStorage.FileWriter.FormShutDown();
                                        Properties.Settings.Default.defaultRestaurant = -1;
                                        Properties.Settings.Default.Save();
                                    }));

                                    AsyncCallback<MessageStatus> responder = new AsyncCallback<MessageStatus>(
                                      result =>
                                      {
                                          Invoke(new Action(() =>
                                          {
                                              ShowLoading(false);
                                              MessageBox.Show(this, "An email has been sent to your provided email as confirmation of your restaurant being made deactivated. Please use RST-" + OwnerStorage.ThisRestaurant.objectId + " as your restaurant reference and USR-" + OwnerStorage.CurrentlyLoggedIn.ObjectId + " as your user account reference.", "restaurant successfully deactivated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                          }));
                                          
                                          Application.Restart();
                                          Environment.Exit(0);
                                      },

                                      fault =>
                                      {
                                          Invoke(new Action(() =>
                                          {
                                              ShowLoading(false);
                                              MessageBox.Show(fault.Message.ToString());
                                          }));
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
                                    Invoke(new Action(() =>
                                    {
                                        ShowLoading(false);
                                        MessageBox.Show(error.Message.ToString());
                                    }));
                                });

                            AsyncCallback<Restaurant> saveObjectCallback = new AsyncCallback<Restaurant>(
                              savedRestaurant =>
                              {
                                  Backendless.Persistence.Of<Restaurant>().Save(savedRestaurant, updateObjectCallback);
                              },
                              error =>
                              {
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
                            Invoke(new Action(() =>
                            {
                                ShowLoading(false);
                                MessageBox.Show(this, "You have logged in with another user's login credentials. Please login using your correct login credentials");
                            }));
                        }
                    },
                    fault =>
                    {
                        Invoke(new Action(() =>
                        {
                            ShowLoading(false);
                            MessageBox.Show(fault.Message.ToString());
                        }));
                    });

                    String login = tbxEmail.Text;
                    String password = tbxPassword.Text;
                    Backendless.UserService.Login(login, password, callback);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void ConfirmRestaurantDeactivationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == System.Windows.Forms.CloseReason.UserClosing && pbxLoading.Visible == true)
            {
                e.Cancel = true;
            }
        }
    }
}
