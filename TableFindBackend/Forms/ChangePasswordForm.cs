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
    public partial class ChangePasswordForm : Form
    {
        public ChangePasswordForm()
        {
            //standard constructor
            InitializeComponent();
        }

        //Button used to close the form if the user wishes to not save changes made
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowLoading(bool toggle)
        {
            //Method which simulated a loading screen, disabeling and enabling the right elements on the form
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

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //this method will perform all validations neccessary to change the user's login password
            ShowLoading(true);
            if (tbxEmail.Text == "" || tbxNewPassConfirm.Text == "" || tbxPassNew.Text == "" || tbxPassword.Text == "")//confirms that all fields are filled in
            {
                //the user failed to fill in all fields                
                ShowLoading(false);
                MessageBox.Show(this, "Please be sure to fill in all fields");
                
            }
            else
            { 
                if (tbxNewPassConfirm.Text.Equals(tbxPassNew.Text))//validates that the password meets the password standards described on the form
                {
                    if (PasswordValidater(tbxNewPassConfirm.Text) == true)//validates that both the password and confirm passwords mathc
                    {
                        AsyncCallback<BackendlessUser> callback = new AsyncCallback<BackendlessUser>(
                        user =>
                        {
                            if (user.ObjectId == OwnerStorage.CurrentlyLoggedIn.ObjectId)
                            {
                                AsyncCallback<BackendlessUser> updateCallback = new AsyncCallback<BackendlessUser>(
                                    newPass =>
                                    {
                                        //success, the program will now restart
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
                                        //something went wrong, an error message will now display
                                        Invoke(new Action(() =>
                                        {
                                            ShowLoading(false);
                                            MessageBox.Show(this, "Error: " + fault.Message);
                                        }));
                                    });

                                //applies the new password
                                OwnerStorage.CurrentlyLoggedIn.Password = tbxNewPassConfirm.Text;
                                //runs the update callback
                                Backendless.UserService.Update(OwnerStorage.CurrentlyLoggedIn, updateCallback);
                            }
                            else//this means that the user logged in with a different user's credentials
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
                            //something went wrong, an error message will now displa
                            Invoke(new Action(() =>
                            {
                                ShowLoading(false);
                                MessageBox.Show(this, "Error: " + fault.Message);
                            }));
                        });

                        String login = tbxEmail.Text;
                        String password = tbxPassword.Text;
                        //runs the check login callback
                        Backendless.UserService.Login(login, password, callback);
                    }
                    else//the two password fields does not match
                    {
                        ShowLoading(false);
                        MessageBox.Show(this, "The password you have specified does not meet the password standards listed on this window");
                    }
                }
                else//password is of invalid format op does not meet password standards
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