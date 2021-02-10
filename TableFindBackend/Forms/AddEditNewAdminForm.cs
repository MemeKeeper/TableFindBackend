using BackendlessAPI;
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
    public partial class AddEditNewAdminForm : Form
    {
        public AdminPins TempAdmin { get; set; }

        public AddEditNewAdminForm(AdminPins a)
        {
            InitializeComponent();

            TempAdmin = a;
            if(a == null)
            {
                //Addinng New Admin User
                btnRemoveDeactivate.Visible = false ;
            }
            else 
            {
                lblTitle.Text = "Editing Admin User";
                tbxName.Text = a.UserName;
                tbxContact.Text = a.ContactNumber;
                tbxPinCode.Text = a.PinCode.ToString();
                
                if (a.Active == true)
                {
                    //Editing Existing Admin User
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
                    btnRemoveDeactivate.Text ="Delete";
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void tbxPinCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnCreateNewAdmin_Click(object sender, EventArgs e)
        {
            showLoading(true);
            if (TempAdmin == null)
            {
                //Create New Admin

                AsyncCallback<AdminPins> callback = new AsyncCallback<AdminPins>(
                result =>
                {

                    OwnerStorage.ListOfAdmins.Add(result);
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
                    Invoke(new Action(() =>
                    {
                        // server reported an error
                        showLoading(false);
                        MessageBox.Show(this, "error: " + fault.Message);
                    }));

                });

                if (tbxName.Text == ""
                    || tbxContact.Text == ""
                    || tbxPinCode.Text == ""
                    || tbxConfirmPin.Text == "")
                {
                    showLoading(false);
                    MessageBox.Show(this, "Please fill in all fields.");
                }
                else
                {
                    if (tbxContact.TextLength == 10)
                    {
                        if (tbxPinCode.Text.Equals(tbxConfirmPin.Text))
                        {
                            if (tbxPinCode.TextLength < 4)
                            {
                                showLoading(false);
                                MessageBox.Show(this, "Your PIN number must be at least 4 digits in length.");
                                tbxPinCode.Text = "";
                                tbxConfirmPin.Text = "";
                            }
                            else
                            {
                                bool flag = false;
                                foreach (AdminPins a in OwnerStorage.ListOfAdmins)
                                {
                                    if (a.PinCode.ToString().Equals(tbxPinCode.Text))
                                    {
                                        flag = true;
                                    }
                                }
                                if (flag == false)
                                {
                                    TempAdmin = new AdminPins();
                                    TempAdmin.UserName = tbxName.Text;
                                    TempAdmin.ContactNumber = tbxContact.Text;
                                    TempAdmin.PinCode = Convert.ToInt32(tbxPinCode.Text);
                                    TempAdmin.RestaurantId = OwnerStorage.ThisRestaurant.objectId;
                                    TempAdmin.Active = true;
                                    Backendless.Data.Of<AdminPins>().Save(TempAdmin, callback);
                                }
                                else
                                {
                                    showLoading(false);
                                    MessageBox.Show(this, "There is already an administrator with this PIN, please use a different PIN");
                                    tbxPinCode.Text = "";
                                    tbxConfirmPin.Text = "";
                                }
                            }
                        }
                        else
                        {
                            showLoading(false);
                            MessageBox.Show(this, "The two Admin PINS you have entered do not match.");
                            tbxPinCode.Text = "";
                            tbxConfirmPin.Text = "";
                        }
                    }
                    else
                    {
                        showLoading(false);
                        MessageBox.Show(this, "The Contact number you have entered is invalid");
                    }
                }
            }
            else
            {
                //Edit Existing Admin


                if (TempAdmin.Active == false)
                {
                    //reactivate Admin
                    AsyncCallback<AdminPins> updateObjectCallback = new AsyncCallback<AdminPins>(
                   savedAdminPins =>
                   {
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
                          Invoke(new Action(() =>
                          {
                              showLoading(false);
                              MessageBox.Show(this, "Error: " + error.Message);
                          }));
                      });
                    TempAdmin.Active = true;
                    Backendless.Persistence.Of<AdminPins>().Save(TempAdmin, saveObjectCallback);
                }
                else
                {
                    AsyncCallback<AdminPins> updateObjectCallback = new AsyncCallback<AdminPins>(
                    savedAdminPin =>
                    {
                    //good stuff
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
                          Backendless.Persistence.Of<AdminPins>().Save(savedAdminPin, updateObjectCallback);
                      },
                      error =>
                      {
                          Invoke(new Action(() =>
                          {
                          //server reported an error
                          showLoading(false);
                              MessageBox.Show(this, "error: " + error.Message);
                          }));
                      });


                    if (tbxName.Text == ""
                        || tbxContact.Text == ""
                        || tbxPinCode.Text == ""
                        || tbxConfirmPin.Text == "")
                    {
                        showLoading(false);
                        MessageBox.Show(this, "Please fill in all fields.");

                    }
                    else
                    {
                        if (tbxContact.TextLength == 10)
                        {
                            if (tbxPinCode.Text.Equals(tbxConfirmPin.Text))
                            {

                                if (tbxPinCode.TextLength < 4)
                                {
                                    showLoading(false);
                                    MessageBox.Show(this, "Your PIN number must be at least 4 digits in length.");
                                    tbxPinCode.Text = "";
                                    tbxConfirmPin.Text = "";
                                }
                                else
                                {
                                    bool flag = false;
                                    foreach (AdminPins a in OwnerStorage.ListOfAdmins)
                                    {
                                        if (a.PinCode.ToString().Equals(tbxPinCode.Text)
                                            && a.PinCode != TempAdmin.PinCode)
                                        {
                                            flag = true;
                                        }
                                    }

                                    if (flag == false)
                                    {
                                        TempAdmin.UserName = tbxName.Text;
                                        TempAdmin.ContactNumber = tbxContact.Text;
                                        TempAdmin.PinCode = Convert.ToInt32(tbxPinCode.Text);
                                        TempAdmin.RestaurantId = OwnerStorage.ThisRestaurant.objectId;
                                        Backendless.Persistence.Of<AdminPins>().Save(TempAdmin, saveObjectCallback);
                                    }
                                    else
                                    {
                                        showLoading(false);
                                        MessageBox.Show(this, "There is already an administrator with this PIN, please use a different PIN");
                                        tbxPinCode.Text = "";
                                        tbxConfirmPin.Text = "";

                                    }
                                }
                            }
                            else
                            {
                                showLoading(false);
                                MessageBox.Show(this, "The two Admin PINS you have entered do not match.");
                                tbxPinCode.Text = "";
                                tbxConfirmPin.Text = "";
                            }
                        }
                        else
                        {
                            showLoading(false);
                            MessageBox.Show(this, "The Contact Number you have entered is invalid");
                        }
                    }


                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void tbxContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
               (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
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
        private void btnRemoveAdmin_Click(object sender, EventArgs e)
        {
            
            if (TempAdmin.Active == false)
            {
                DialogResult result = MessageBox.Show(this, "Are you sure you wish permanently remove " + TempAdmin.UserName + " as administrator?", "Removing Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    showLoading(true);
                    AsyncCallback<long> deleteObjectCallback = new AsyncCallback<long>(
                    deletionTime =>
                    {
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
                    Invoke(new Action(() =>
                    {
                        showLoading(false);
                        MessageBox.Show(this, "Error: " + error.Message);
                    }));
                });

                    AsyncCallback<AdminPins> saveObjectCallback = new AsyncCallback<AdminPins>(
                      savedAdmin =>
                      {

                          Backendless.Persistence.Of<AdminPins>().Remove(savedAdmin, deleteObjectCallback);
                      },
                      error =>
                      {
                          Invoke(new Action(() =>
                          {
                              showLoading(false);
                              MessageBox.Show(this, "Error: " + error.Message);
                          }));
                      }
                    );

                    Backendless.Persistence.Of<AdminPins>().Save(TempAdmin, saveObjectCallback);
                }
            }
            else
            {

                DialogResult result = MessageBox.Show(this, "Are you sure you wish to deactivate " + TempAdmin.UserName + "?", "Deactivating Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result==DialogResult.Yes)
                {
                    showLoading(true);
                    AsyncCallback<AdminPins> updateObjectCallback = new AsyncCallback<AdminPins>(
                    savedAdminPins =>
                    {
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
                          Invoke(new Action(() =>
                          {
                              showLoading(false);
                              MessageBox.Show(this, "Error: " + error.Message);
                          }));
                      });
                    TempAdmin.Active = false;
                    Backendless.Persistence.Of<AdminPins>().Save(TempAdmin, saveObjectCallback);
                }
            }
        }
    }
}
