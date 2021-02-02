﻿using BackendlessAPI;
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
            if(tbxConfirm.Text==OwnerStorage.ThisRestaurant.Name)
            {
                btnConfirm.Enabled = true;
            }
            else
                btnConfirm.Enabled = false;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if(tbxEmail.Text=="")
            {
                MessageBox.Show(this, "Be sure to fill in a valid Login Email and try again","Empty Email",MessageBoxButtons.OK,MessageBoxIcon.Error);
                tbxConfirm.Text = "";
            }
            else
            {
                if(tbxPassword.Text=="")
                {
                    MessageBox.Show(this, "Be sure to fill in a valid Login Password and try again", "Empty Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxConfirm.Text = "";
                }
                else
                {
                    AsyncCallback<BackendlessUser> callback = new AsyncCallback<BackendlessUser>(
                    user =>
                    {
                        if(user.ObjectId==OwnerStorage.CurrentlyLoggedIn.ObjectId)
                        {

                            AsyncCallback<Restaurant> updateObjectCallback = new AsyncCallback<Restaurant>(
                            savedRestaurant =>
                                {
                                    Invoke(new Action(() =>
                                    {
                                        OwnerStorage.FileWriter.WriteLineToFile("Restaurant object has been deactivated", false);
                                        OwnerStorage.FileWriter.FormShutDown();
                                        Properties.Settings.Default.defaultRestaurant = -1;
                                        Properties.Settings.Default.Save();
                                    }));

                                    Application.Restart();
                                    Environment.Exit(0);
                                },
                                error =>
                                {
                                    Invoke(new Action(() =>
                                    {
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
                                MessageBox.Show(this, "You have logged in with another user's login credentials. Please login using your correct login credentials");
                            }));
                        }
                    },
                    fault =>
                    {
                        Invoke(new Action(() =>
                        {
                            MessageBox.Show(fault.Message.ToString());
                        }));
                    });

                    String login =tbxEmail.Text;
                    String password = tbxPassword.Text;
                    Backendless.UserService.Login(login, password, callback);
                }
            }
        }
    }
}