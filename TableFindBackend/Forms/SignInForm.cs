using BackendlessAPI;
using BackendlessAPI.Async;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableFindBackend.Global_Variables;
using TableFindBackend.Models;

namespace TableFindBackend.Forms
{
    public partial class SignInForm : Form
    {

        public SignInForm()
        {
            InitializeComponent();

            pbxLoading.Visible = true;

            AsyncCallback<Boolean> callback = new AsyncCallback<Boolean>(
            isValidLogin =>
            {
                string loggedInUserObjectId = Backendless.UserService.LoggedInUserObjectId();
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
                                if (foundRestaurant.Count != 0)
                                {
                                    if (foundRestaurant.Count > 1)
                                    {
                                        SelectRestaurantForm selectForm = new SelectRestaurantForm((List<Restaurant>)foundRestaurant);
                                        selectForm.ShowDialog();

                                        OwnerStorage.ThisRestaurant = selectForm.selected;
                                    }
                                    else
                                    {
                                        OwnerStorage.ThisRestaurant = foundRestaurant[0];
                                    }
                                    Invoke(new Action(() =>
                                    {
                                        System.Console.WriteLine("User logged in. Assigned ID - " + success.ObjectId);
                                        DialogResult = DialogResult.OK;
                                        this.Close();
                                    }));
                                }
                                else
                                {
                                    Invoke(new Action(() =>
                                    {
                                        pbxLoading.Visible = false;
                                        MessageBox.Show(this, "No Restaurant was located under these login credentials. If " +
                                    "you are a new user who only recently created a Tablefind Profile " +
                                    "then it may be that we are still setting up your profile for a restaurant " +
                                    "on our side. If this issue is not resolved within one week, please contact us."); //Fix later
                                    }));
                                }
                            },
                            error =>
                            {
                                Invoke(new Action(() =>
                                {
                                    pbxLoading.Visible = false;
                                    MessageBox.Show(this, "Error: " + error.Message);
                                }));
                            });

                            Backendless.Data.Of<Restaurant>().Find(queryBuilder, getRestaurantCallback);
                        },
                        fault =>
                        {
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
                                pbxLoading.Visible = false;
                            }));
                        });

                    Backendless.Data.Of<BackendlessUser>().FindById(loggedInUserObjectId, findCallBack);
                }
                else
                {
                    pbxLoading.Visible = false;
                }
            },
            fault =>
            {
                Invoke(new Action(() =>
                {
                    pbxLoading.Visible = false;
                }));
            });

            Backendless.UserService.IsValidLogin(callback);

            SendMessage(tbEmail.Handle, 0x1501, 1, "  Please enter E-mail");
            SendMessage(tbPassword.Handle, 0x1501, 1, "  Please enter password");
            SendMessage(tbEmailAddress.Handle, 0x1501, 1, "  Enter E-mail address");
            SendMessage(tbPass.Handle, 0x1501, 1, "  Enter password");
            SendMessage(tbConfirmPass.Handle, 0x1501, 1, "  Confirm password");
            SendMessage(tbFirstName.Handle, 0x1501, 1, "  First name");
            SendMessage(tbLastName.Handle, 0x1501, 1, "  Last name");
            SendMessage(tbContactNumber.Handle, 0x1501, 1, "  Mobile Number");
        }

        private void lblX_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }


        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

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

        private void lblForgot_Click(object sender, EventArgs e)
        {
            string userEmail = tbEmail.Text;
            pbxLoading.Visible = true;

            if (!IsValidEmail(userEmail))
            {
                pbxLoading.Visible = false;
                lblEnterEmailForRecovery.Visible = true;
                //MessageBox.Show("Invalid email has been entered. Please make sure your email format is correct e.g wizerd@oz.com");

            }
            else


            {
                AsyncCallback<Object> pwRecoveryCallback = new AsyncCallback<Object>(
                user =>
                {
                    Invoke(new Action(() =>
                    {
                        pbxLoading.Visible = false;
                        MessageBox.Show("Password recovery email has been sent");
                    }));
                },
                    fault =>
                    {
                        Invoke(new Action(() =>
                        {
                            pbxLoading.Visible = false;
                            MessageBox.Show(this, "Error: " + fault.Message);
                        }));
                    });
                Backendless.UserService.RestorePassword(userEmail, pwRecoveryCallback);

            }
        }

        private void lblForgot_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void lblForgot_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            pbxLoading.Visible = true;
            string userEmail = tbEmail.Text;
            string password = tbPassword.Text;

            if (!IsValidEmail(userEmail))
            {
                pbxLoading.Visible = false;
                MessageBox.Show("Invalid email has been entered. Please make sure your email format is correct e.g wizerd@oz.com");

            }
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
                        if (foundRestaurant.Count != 0)
                        {
                            if (foundRestaurant.Count > 1)
                            {
                                SelectRestaurantForm selectForm = new SelectRestaurantForm((List<Restaurant>)foundRestaurant);
                                selectForm.ShowDialog();
                                OwnerStorage.ThisRestaurant = selectForm.selected;
                            }
                            else
                            {
                                OwnerStorage.ThisRestaurant = foundRestaurant[0];
                            }
                            Invoke(new Action(() =>
                            {
                                DialogResult = DialogResult.OK;
                                this.Close();
                            }));
                        }
                        else
                        {
                            Invoke(new Action(() =>
                            {
                                pbxLoading.Visible = false;
                                MessageBox.Show(this, "No Restaurant was located under these login credentials. If " +
                                    "you are a new user who only recently created a Tablefind Profile " +
                                    "then it may be that we are still setting up your profile for a restaurant " +
                                    "on our side. If this issue is not resolved within one week, please contact " +
                                    "a member of our development team to assist with restaurant registeration"); //Fix later
                            }));
                        }

                    },
                    error =>
                    {
                        Invoke(new Action(() =>
                        {
                            pbxLoading.Visible = false;
                            MessageBox.Show(this, "Error: " + error.Message);
                        }));
                    });

                    Backendless.Data.Of<Restaurant>().Find(queryBuilder, getRestaurantCallback);
                },
                fault =>
                {
                    Invoke(new Action(() =>
                    {
                        pbxLoading.Visible = false;
                        MessageBox.Show(this, "Error: " + fault.Message);
                    }));
                });
                Backendless.UserService.Login(userEmail, password, callback, cbxStaySignedIn.Checked ? true : false);
            }
        }

        private void lblRegisterTitle_Click(object sender, EventArgs e)
        {
            tcLoginRegister.SelectedTab = tpRegister;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            lblX_Click(sender, e);
        }

        private void tbLocation_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (tbFirstName.Text == ""
                || tbLastName.Text == ""
                || tbContactNumber.Text == ""
                || tbEmailAddress.Text == ""
                || tbPass.Text == ""
                || tbConfirmPass.Text == "")
            {
                ToggleLoading(false);
                MessageBox.Show("Please be sure to fill in all fields");
            }
            else
            {
                ToggleLoading(true);
                string firstName = tbFirstName.Text;
                string lastName = tbLastName.Text;
                string contactNumber = tbContactNumber.Text;
                string email = tbEmailAddress.Text;
                string password = tbPass.Text;
                string cPassword = tbConfirmPass.Text;
                if (IsValidEmail(email))
                {
                    if (PasswordValidater(password))
                    {
                        if (password == cPassword)
                        {
                            AsyncCallback<BackendlessUser> callbackRegister = new AsyncCallback<BackendlessUser>(
                            user =>
                            {
                                Invoke(new Action(() =>
                                    {
                                        tcLoginRegister.SelectedTab = tpLogin;
                                        MessageBox.Show(this, "A verification email has been sent to the specified email address. Please follow the link which is provided in the email");
                                        ToggleLoading(false);
                                    }));
                            },
                            fault =>
                            {
                                Invoke(new Action(() =>
                                {
                                    MessageBox.Show(this, "Error: " + fault.Message);
                                    ToggleLoading(false);
                                }));
                            });

                            BackendlessUser newUser = new BackendlessUser();
                            newUser.Password = password;
                            newUser.Email = email;
                            newUser.SetProperty("FirstName", firstName);
                            newUser.SetProperty("LastName", lastName);
                            newUser.SetProperty("isOwner", true);
                            newUser.SetProperty("Cellphone", contactNumber);
                            Backendless.UserService.Register(newUser, callbackRegister);
                        }
                        else
                        {
                            ToggleLoading(false);
                            MessageBox.Show("Invalid password format. Please make sure that you're password contains minimum eight characters, at least one uppercase letter, one lowercase letter and one number");
                        }
                    }
                    else
                    {
                        ToggleLoading(false);
                        MessageBox.Show("Invalid password format. Please make sure that you're password contains minimum eight characters, at least one uppercase letter, one lowercase letter and one number");
                    }
                }
                else
                {
                    ToggleLoading(false);
                    MessageBox.Show("Invalid email has been entered. Please make sure your email format is correct e.g wizerd@oz.com");
                }


            }
        }
        private void ToggleLoading(bool toggle)
        {
            if (toggle == true)
            {
                pnlPersonal.Enabled = false;
                lblTitle.Enabled = false;
                btnRegister.Enabled = false;
                pbxLoadingFS.Visible = true;
            }
            else
            {
                pnlPersonal.Enabled = true;
                lblTitle.Enabled = true;
                btnRegister.Enabled = true;
                pbxLoadingFS.Visible = false;
            }
        }
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

        private void tbContactNumber_TextChanged(object sender, EventArgs e)
        {

        }

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
