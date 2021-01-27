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
        AdminPins admin;
        public AddEditNewAdminForm(AdminPins a)
        {
            InitializeComponent();

            admin = a;
            if(a == null)
            {
                //Addinng New Admin User
            }
            else 
            {
                //Editing Existing Admin User
                lblTitle.Text = "Editing Admin User";
                tbxName.Text = a.UserName;
                tbxContact.Text = a.ContactNumber;
                tbxPinCode.Text = a.PinCode.ToString();
                
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void tbxPinCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnCreateNewAdmin_Click(object sender, EventArgs e)
        {
            if(admin == null)
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
                        DialogResult = DialogResult.OK;
                        this.Close();
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
                    && tbxContact.Text == ""
                    && tbxPinCode.Text == ""
                    && tbxConfirmPin.Text == "")
                {
                    MessageBox.Show(this, "Please fill in all fields.");
                }
                else
                {
                    if (tbxContact.TextLength == 10)
                    {
                        if(tbxPinCode.Text.Equals(tbxConfirmPin.Text))
                        {
                            admin = new AdminPins();
                            admin.UserName = tbxName.Text;
                            admin.ContactNumber = tbxContact.Text;
                            admin.PinCode = Convert.ToInt32(tbxPinCode.Text);
                            admin.RestaurantId = OwnerStorage.ThisRestaurant.objectId;
                            Backendless.Data.Of<AdminPins>().Save(admin, callback);
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
            }
        }
    }
}
