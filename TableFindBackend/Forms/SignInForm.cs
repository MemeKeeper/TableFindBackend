using BackendlessAPI;
using BackendlessAPI.Async;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TableFindBackend.Global_Variables;
using TableFindBackend.Models;

namespace TableFindBackend.Forms
{
    //This multifunctional form allows the user to register an account and sign into the application
    public partial class SignInForm : Form
    {
        public SignInForm()
        {
            InitializeComponent();
            ShowLoading(true);

            AsyncCallback<Boolean> callback = new AsyncCallback<Boolean>(
            isValidLogin =>
            {
                string loggedInUserObjectId = Backendless.UserService.LoggedInUserObjectId();
                //Checks if the user chose to stay logged in
                if (loggedInUserObjectId != null)
                {
                    AsyncCallback<BackendlessUser> findCallBack = new AsyncCallback<BackendlessUser>(
                        success =>
                        {
                            OwnerStorage.CurrentlyLoggedIn = success;
                            String whereClause = "ownerId = '" + success.ObjectId + "'";
                            BackendlessAPI.Persistence.DataQueryBuilder queryBuilder = BackendlessAPI.Persistence.DataQueryBuilder.Create();
                            queryBuilder.SetWhereClause(whereClause);

                            AsyncCallback<IList<Restaurant>> getRestaurantCallback = new AsyncCallback<IList<Restaurant>>(
                            foundRestaurant =>
                            {
                                //Checks if restaurants exist under the logged in user
                                if (foundRestaurant.Count != 0)
                                {
                                    List<Restaurant> tempList = (List<Restaurant>)foundRestaurant;
                                    //Checks if the user manages multiple restaurants and if true, prompts the user to select a restaurant to sign into
                                    if (tempList.Count > 1)
                                        {
                                            SelectRestaurantForm selectForm = new SelectRestaurantForm(tempList);
                                            selectForm.ShowDialog();
                                            OwnerStorage.ThisRestaurant = selectForm.selected;
                                        }
                                        //The user only manages one restaurant - Logs the user into their registered restaurant
                                        else
                                        {
                                            OwnerStorage.ThisRestaurant = tempList[0];
                                        }
                                        Invoke(new Action(() =>
                                        {
                                            DialogResult = DialogResult.OK;
                                            this.Close();
                                        }));
                                    
                                }
                                //No restaurants were found under the entered login credentials
                                else
                                {
                                    Invoke(new Action(() =>
                                    {
                                        ShowLoading(false);
                                        MessageBox.Show(this, "No Restaurant was located under these login credentials. If " +
                                    "you are a new user who only recently created a Tablefind Profile " +
                                    "then it may be that we are still setting up your profile for a restaurant " +
                                    "on our side. If this issue is not resolved within one week, please contact us."); 
                                    }));
                                }
                            },
                            error =>
                            {
                                //Something went wrong. An error message will now be displayed
                                //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                                Invoke(new Action(() =>
                                {
                                    ShowLoading(false);
                                    MessageBox.Show(this, "Error: " + error.Message);
                                }));
                            });

                            Backendless.Data.Of<Restaurant>().Find(queryBuilder, getRestaurantCallback);
                        },
                        fault =>
                        {
                            //Something went wrong. An error message will now be displayed
                            //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                            AsyncCallback<Object> logoutCallback = new AsyncCallback<Object>(
                            user =>
                            {
                            },
                            logoutfault =>
                            {
                            });
                            Backendless.UserService.Logout(logoutCallback);

                            Invoke(new Action(() =>
                            {
                                ShowLoading(false);
                            }));
                        });
                    //Retrieves the user object
                    Backendless.Data.Of<BackendlessUser>().FindById(loggedInUserObjectId, findCallBack);
                }
                //User chose to not stay signed in
                else
                {
                    ShowLoading(false);
                }
            },
            fault =>
            {
                //Something went wrong. An error message will now be displayed
                //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                Invoke(new Action(() =>
                {
                    ShowLoading(false);
                }));
            });
            //Executes the callback
            Backendless.UserService.IsValidLogin(callback);

            //Displays textbox field hints 
            SendMessage(tbEmail.Handle, 0x1501, 1, "  Please enter E-mail");
            SendMessage(tbPassword.Handle, 0x1501, 1, "  Please enter password");
            SendMessage(tbEmailAddress.Handle, 0x1501, 1, "  Enter E-mail address");
            SendMessage(tbPass.Handle, 0x1501, 1, "  Enter password");
            SendMessage(tbConfirmPass.Handle, 0x1501, 1, "  Confirm password");
            SendMessage(tbFirstName.Handle, 0x1501, 1, "  First name");
            SendMessage(tbLastName.Handle, 0x1501, 1, "  Last name");
            SendMessage(tbContactNumber.Handle, 0x1501, 1, "  Mobile Number");
        }

        //Button used to close the form if the user wishes to not save changes made
        private void lblX_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        //Validates that the email entered by the user is valid and exists
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        //Method that handles password recovery
        private void lblForgot_Click(object sender, EventArgs e)
        {
            string userEmail = tbEmail.Text;
            ShowLoading(true);
            //Validates if the user entered an email address on the login form
            if (!IsValidEmail(userEmail))
            {
                ShowLoading(false);
                //Allows the user to enter their email address
                lblEnterEmailForRecovery.Visible = true;
            }
            //User entered an email address on the login form and wants to recover their password
            else
            {
                AsyncCallback<Object> pwRecoveryCallback = new AsyncCallback<Object>(
                user =>
                {
                    Invoke(new Action(() =>
                    {
                        ShowLoading(false);
                        MessageBox.Show("Password recovery email has been sent");
                    }));
                },
                    fault =>
                    {
                        //Something went wrong. An error message will now be displayed
                        //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                        Invoke(new Action(() =>
                        {
                            ShowLoading(false);
                            MessageBox.Show(this, "Error: " + fault.Message);
                        }));
                    });
                //The user password has been restored
                Backendless.UserService.RestorePassword(userEmail, pwRecoveryCallback);
            }
        }

        //Sets the cursor to a hand icon when the user hovers over the Forgot Password label
        private void lblForgot_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        //Uses the default icon of the cursor
        private void lblForgot_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        //A method that will appear on all forms. It simulates a loading screen by showing and hiding all necessary buttons and interface elements
        private void ShowLoading(bool toggle)
        {
            if (toggle == true)
            {
                pbxLoadingFS.Visible = true;
                pbxLoading.Visible = true;
                btnLogin.Enabled = false;
                lblX.Enabled = false;
                lblX2.Enabled = false;
                btnRegister.Enabled = false;
                lblRegisterTitle.Enabled = false;
                lblForgot.Enabled = false;
            }
            else
            {
                pbxLoadingFS.Visible = false;
                pbxLoading.Visible = false;
                btnLogin.Enabled = true;
                lblX.Enabled = true;
                lblX2.Enabled = true;
                btnRegister.Enabled = true;
                lblRegisterTitle.Enabled = true;
                lblForgot.Enabled = true;
            }
        }

        //Method that logs the user into the application
        private void btnLogin_Click(object sender, EventArgs e)
        {
            ShowLoading(true);
            string userEmail = tbEmail.Text;
            string password = tbPassword.Text;

            //Validates the email address format 
            if (!IsValidEmail(userEmail))
            {
                ShowLoading(false);
                MessageBox.Show("Invalid email has been entered. Please make sure your email format is correct e.g wizerd@oz.com");
            }
            //Correct email format has been entered
            else
            {
                AsyncCallback<BackendlessUser> callback = new AsyncCallback<BackendlessUser>(
                user =>
                {
                    OwnerStorage.CurrentlyLoggedIn = user;
                    String whereClause = "ownerId = '" + user.ObjectId + "'";
                    BackendlessAPI.Persistence.DataQueryBuilder queryBuilder = BackendlessAPI.Persistence.DataQueryBuilder.Create();
                    queryBuilder.SetWhereClause(whereClause);

                    AsyncCallback<IList<Restaurant>> getRestaurantCallback = new AsyncCallback<IList<Restaurant>>(
                    foundRestaurant =>
                    {
                        //Checks if restaurants exist under the logged in user
                        if (foundRestaurant.Count != 0)
                        {
                            List<Restaurant> tempList = (List<Restaurant>)foundRestaurant;
                            //Checks if the user manages multiple restaurants and if true, prompts the user to select a restaurant to sign into
                            if (tempList.Count > 1)
                            {
                                SelectRestaurantForm selectForm = new SelectRestaurantForm(tempList);
                                selectForm.ShowDialog();
                                OwnerStorage.ThisRestaurant = selectForm.selected;
                            }
                            //The user only manages one restaurant - Logs the user into their registered restaurant
                            else
                            {
                                OwnerStorage.ThisRestaurant = tempList[0];
                            }
                            Invoke(new Action(() =>
                            {
                                DialogResult = DialogResult.OK;
                                this.Close();
                            }));

                        }
                        //No restaurants were found under the entered login credentials
                        else
                        {
                            Invoke(new Action(() =>
                            {
                                ShowLoading(false);
                                MessageBox.Show(this, "No Restaurant was located under these login credentials. If " +
                                    "you are a new user who only recently created a Tablefind Profile " +
                                    "then it may be that we are still setting up your profile for a restaurant " +
                                    "on our side. If this issue is not resolved within one week, please contact " +
                                    "a member of our development team to assist with restaurant registeration"); 
                            }));
                        }
                    },
                    error =>
                    {
                        //Something went wrong. An error message will now be displayed
                        //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                        Invoke(new Action(() =>
                        {
                            ShowLoading(false);
                            MessageBox.Show(this, "Error: " + error.Message);
                        }));
                    });

                    //Executes the callback
                    Backendless.Data.Of<Restaurant>().Find(queryBuilder, getRestaurantCallback);
                },
                fault =>
                {
                    //Something went wrong. An error message will now be displayed
                    //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                    Invoke(new Action(() =>
                    {
                        ShowLoading(false);
                        MessageBox.Show(this, "Error: " + fault.Message);
                    }));
                });

                //Signs the user into the application and keeps the user logged in if the 'Stay Signed In' checkbox is checked
                Backendless.UserService.Login(userEmail, password, callback, cbxStaySignedIn.Checked ? true : false);
            }
        }

        //Opens the Register tab
        private void lblRegisterTitle_Click(object sender, EventArgs e)
        {
            tcLoginRegister.SelectedTab = tpRegister;
        }

        //I don't know what this means :)
        private void label1_Click(object sender, EventArgs e)
        {
            lblX_Click(sender, e);
        }

        //Registers a new user if all information is entered correctly
        private void btnRegister_Click(object sender, EventArgs e)
        {
            //Validates if the user left the provided fields blank
            if (tbFirstName.Text == ""
                || tbLastName.Text == ""
                || tbContactNumber.Text == ""
                || tbEmailAddress.Text == ""
                || tbPass.Text == ""
                || tbConfirmPass.Text == "")
            {
                ShowLoading(false);
                MessageBox.Show("Please be sure to fill in all fields");
            }
            //Assigns the corrrect information to the variables
            else
            {
                ShowLoading(true);
                string firstName = tbFirstName.Text;
                string lastName = tbLastName.Text;
                string contactNumber = tbContactNumber.Text;
                string email = tbEmailAddress.Text;
                string password = tbPass.Text;
                string cPassword = tbConfirmPass.Text;
                //Validates if the email is correct and exists
                if (IsValidEmail(email))
                {
                    //Validates the password entered
                    if (PasswordValidater(password))
                    {
                        //Checks if the two passwords entered match
                        if (password == cPassword)
                        {
                            //Checks that the ContactNumber entered is 10 digits long
                            if (tbContactNumber.TextLength == 10)
                            {
                                AsyncCallback<BackendlessUser> callbackRegister = new AsyncCallback<BackendlessUser>(
                                user =>
                                {
                                    //The registration process starts and a verification email is sent to the user
                                    Invoke(new Action(() =>
                                        {
                                            tcLoginRegister.SelectedTab = tpLogin;
                                            MessageBox.Show(this, "A verification email has been sent to the specified email address. Please follow the link which is provided in the email");
                                            tbContactNumber.Text = "";
                                            tbFirstName.Text = "";
                                            tbLastName.Text = "";
                                            tbEmailAddress.Text = "";
                                            tbPass.Text = "";
                                            tbConfirmPass.Text = "";
                                            ShowLoading(false);
                                        }));
                                },
                                fault =>
                                {
                                    //An error occurred which is displayed to the user and the textbox fields are cleared
                                    Invoke(new Action(() =>
                                    {
                                        MessageBox.Show(this, "Error: " + fault.Message);
                                        ShowLoading(false);
                                        //tbContactNumber.Text = "";
                                        //tbFirstName.Text = "";
                                        //tbLastName.Text = "";
                                        //tbEmailAddress.Text = "";
                                        //tbPass.Text = "";
                                        //tbConfirmPass.Text = "";
                                    }));
                                });

                                //Creates the Backendless user and sets the properties 
                                BackendlessUser newUser = new BackendlessUser();
                                newUser.Password = password;
                                newUser.Email = email;
                                newUser.SetProperty("FirstName", firstName);
                                newUser.SetProperty("LastName", lastName);
                                newUser.SetProperty("isOwner", true);
                                newUser.SetProperty("Cellphone", contactNumber);
                                Backendless.UserService.Register(newUser, callbackRegister);
                            }
                            //The contact number is of incorrect format
                            else
                            {
                                ShowLoading(false);
                                MessageBox.Show("Invalid contact number format. Please make sure that your contact number contains ten (10) numbers");
                            }
                        }
                        //The two passwords entered do not match
                        else
                        {
                            ShowLoading(false);
                            MessageBox.Show("The two passwords entered do not match.");
                        }
                    }
                    //The password is of incorrect format
                    else
                    {
                        ShowLoading(false);
                        MessageBox.Show("Invalid password format. Please make sure that your password contains minimum eight characters, at least one uppercase letter, one lowercase letter and one number");
                    }
                }
                //The email address is of incorrect format
                else
                {
                    ShowLoading(false);
                    MessageBox.Show("Invalid email has been entered. Please make sure your email format is correct e.g wizerd@oz.com");
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

        //Makes sure that the user only enters the pre-configured setting on the platform for financial variables (comma or point)
        private void tbContactNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
