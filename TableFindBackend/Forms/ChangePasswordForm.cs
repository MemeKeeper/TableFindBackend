using BackendlessAPI;
using BackendlessAPI.Async;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableFindBackend.Global_Variables;

namespace TableFindBackend.Forms
{
    //This form allows the user to reset their password
    public partial class ChangePasswordForm : Form
    {
        public ChangePasswordForm()
        {
            //Standard constructor
            InitializeComponent();
        }

        //Button used to close the form if the user wishes to not save changes made
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Method which simulated a loading screen, disabling and enabling the right elements on the form
        private void ShowLoading(bool toggle)
        {
            if(toggle==true)
            {
                pbxLoading.Visible = true;
                btnCancel.Enabled = false;
                btnClose.Enabled = false;
                btnConfirm.Enabled = false;
                tbxPassNew.Enabled = false;
                tbxNewPassConfirm.Enabled = false;
            }
            else
            {
                pbxLoading.Visible = false;
                btnCancel.Enabled = true;
                btnClose.Enabled = true;
                btnConfirm.Enabled = true;
                tbxPassNew.Enabled = true;
                tbxNewPassConfirm.Enabled = true;
            }
        }

        //Performs all validations necessary to change the user's login password
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            ShowLoading(true);
            //Confirms that all fields are filled in
            if (tbxEmail.Text == "" || tbxNewPassConfirm.Text == "" || tbxPassNew.Text == "" || tbxPassword.Text == "")
            {
                //The user failed to fill in all fields                
                ShowLoading(false);
                MessageBox.Show(this, "Please be sure to fill in all fields");
                
            }
            else
            {
                //Validates that the password meets the password standards described on the form
                if (tbxNewPassConfirm.Text.Equals(tbxPassNew.Text))
                {
                    //Validates that both the password and confirm passwords match
                    if (PasswordValidater(tbxNewPassConfirm.Text) == true)
                    {
                        AsyncCallback<BackendlessUser> callback = new AsyncCallback<BackendlessUser>(
                        user =>
                        {
                            if (user.ObjectId == OwnerStorage.CurrentlyLoggedIn.ObjectId)
                            {
                                AsyncCallback<BackendlessUser> updateCallback = new AsyncCallback<BackendlessUser>(
                                    newPass =>
                                    {
                                        //Success, the program will now restart
                                        Invoke(new Action(() =>
                                        {
                                            ShowLoading(false);
                                            OwnerStorage.FileWriter.WriteLineToFile("User changed the main login password", true);
                                            MessageBox.Show(this, "You have successfully changed your login credentials, the program will now restart");

                                            Application.Restart();
                                            Environment.Exit(0);
                                        }));
                                    },
                                    fault =>
                                    {
                                        //Something went wrong, an error message will now display
                                        Invoke(new Action(() =>
                                        {
                                            ShowLoading(false);
                                            MessageBox.Show(this, "Error: " + fault.Message);
                                        }));
                                    });

                                //Applies the new password
                                OwnerStorage.CurrentlyLoggedIn.Password = tbxNewPassConfirm.Text;
                                //Runs the update callback
                                Backendless.UserService.Update(OwnerStorage.CurrentlyLoggedIn, updateCallback);
                            }
                            //This means that the user logged in with a different user's credentials
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
                            //Something went wrong, an error message will now display
                            Invoke(new Action(() =>
                            {
                                ShowLoading(false);
                                MessageBox.Show(this, "Error: " + fault.Message);
                            }));
                        });

                        String login = tbxEmail.Text;
                        String password = tbxPassword.Text;
                        //Runs the check login callback
                        Backendless.UserService.Login(login, password, callback);
                    }
                    //Password is of invalid format or does not meet password standards
                    else
                    {
                        ShowLoading(false);
                        MessageBox.Show(this, "The password you have specified does not meet the password standards listed on this window");
                    }
                }
                //The two password fields do not match
                else
                {
                    ShowLoading(false);
                    MessageBox.Show(this, "The password field and the confirm password fields do not match");
                }
            }
        }

        //Method that validates that the password is in the correct format
        private bool PasswordValidater(string password)
        {
            bool valid = false;
            var correct = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$");

            if (correct.IsMatch(password))
            {
                valid = true;
            }
            return valid;
        }

        //Button used to close the form if the user wishes to not save changes made
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Blocks the "alt F4" capability so that the user cannot close the program while a process is running
        private void ChangePasswordForm_FormClosing(object sender, FormClosingEventArgs e)
        {            
            if (e.CloseReason == System.Windows.Forms.CloseReason.UserClosing && pbxLoading.Visible == true)
            {
                e.Cancel = true;
            }
        }
    }
}