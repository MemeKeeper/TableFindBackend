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
                btnRemoveAdmin.Visible = false ;
            }
            else 
            {
                //Editing Existing Admin User
                lblTitle.Text = "Editing Admin User";
                tbxName.Text = a.UserName;
                tbxContact.Text = a.ContactNumber;
                tbxPinCode.Text = a.PinCode.ToString();
                btnCreateNewAdmin.Text = "Update";                
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
            if(TempAdmin == null)
            {
                //Create New Admin

                AsyncCallback<AdminPins> callback = new AsyncCallback<AdminPins>(
                result =>
                {

                    OwnerStorage.ListOfAdmins.Add(result);
                    Invoke(new Action(() =>
                    {
                        // object has been saved
                        MessageBox.Show(this, "Admin User has been successfully created.");
                        this.DialogResult = DialogResult.OK;
                    }));
                },

                fault =>
                {
                    Invoke(new Action(() =>
                    {
                        // server reported an error
                        MessageBox.Show(this, "error: " + fault.Message);
                    }));
                    
                });

                if(tbxName.Text == ""
                    || tbxContact.Text == ""
                    || tbxPinCode.Text == ""
                    || tbxConfirmPin.Text == "")
                {
                    MessageBox.Show(this, "Please fill in all fields.");
                }
                else
                {
                    if (tbxContact.TextLength == 10)
                    {
                        if(tbxPinCode.Text.Equals(tbxConfirmPin.Text))
                        {
                            bool flag = false;
                            foreach(AdminPins a in OwnerStorage.ListOfAdmins)
                            {
                                if(a.PinCode.ToString().Equals(tbxPinCode.Text))
                                {
                                    flag= true;
                                }
                            }
                            if (flag == false)
                            {
                                TempAdmin = new AdminPins();
                                TempAdmin.UserName = tbxName.Text;
                                TempAdmin.ContactNumber = tbxContact.Text;
                                TempAdmin.PinCode = Convert.ToInt32(tbxPinCode.Text);
                                TempAdmin.RestaurantId = OwnerStorage.ThisRestaurant.objectId;
                                Backendless.Data.Of<AdminPins>().Save(TempAdmin, callback);
                            }
                            else
                            {
                                MessageBox.Show(this, "There is already an administrator with this PIN, please use a different PIN");
                                tbxPinCode.Text = "";
                                tbxConfirmPin.Text = "";
                            }
                        }
                        else
                        {
                            MessageBox.Show(this, "The two Admin PINS you have entered do not match.");
                            tbxPinCode.Text = "";
                            tbxConfirmPin.Text = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "The Contact number you have entered is invalid");
                    }
                }                                             
            }
            else
            {
                //Edit Existing Admin


                AsyncCallback<AdminPins> updateObjectCallback = new AsyncCallback<AdminPins>(
                savedAdminPin =>
                {
                    //good stuff
                    Invoke(new Action(() =>
                    {
                        MessageBox.Show(this, "Admin PIN has been updated");
                    }));
                },
                error =>
                {
                    Invoke(new Action(() =>
                    {
                        //server reported an error
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
                          MessageBox.Show(this, "error: " + error.Message);
                      }));
                  });


                if (tbxName.Text == ""
                    || tbxContact.Text == ""
                    || tbxPinCode.Text == ""
                    || tbxConfirmPin.Text == "")
                {
                    MessageBox.Show(this, "Please fill in all fields.");
                }
                else
                {
                    if (tbxContact.TextLength == 10)
                    {
                        if (tbxPinCode.Text.Equals(tbxConfirmPin.Text))
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
                                TempAdmin.UserName = tbxName.Text;
                                TempAdmin.ContactNumber = tbxContact.Text;
                                TempAdmin.PinCode = Convert.ToInt32(tbxPinCode.Text);
                                TempAdmin.RestaurantId = OwnerStorage.ThisRestaurant.objectId;
                                Backendless.Persistence.Of<AdminPins>().Save(TempAdmin, saveObjectCallback);
                            }
                            else
                            {
                                MessageBox.Show(this, "There is already an administrator with this PIN, please use a different PIN");
                                tbxPinCode.Text = "";
                                tbxConfirmPin.Text = "";
                            }
                        }
                        else
                        {
                            MessageBox.Show(this, "The two Admin PINS you have entered do not match.");
                            tbxPinCode.Text = "";
                            tbxConfirmPin.Text = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "The Contact number you have entered is invalid");
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
    }
}
