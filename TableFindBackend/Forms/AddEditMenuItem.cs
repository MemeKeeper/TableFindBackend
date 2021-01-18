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
    public partial class AddEditMenuItem : Form
    {
        public RestaurantMenuItem transferedItem { get; set; }
        public AddEditMenuItem(RestaurantMenuItem item)
        {
            InitializeComponent();
            this.transferedItem = item;
            if (transferedItem != null)
            {
                tbxDescription.Text = item.ingredients;
                tbxName.Text = item.name;
                cbxType.Text = item.type;
                tbxPrice.Text = item.price.ToString();
            }           
        }

        private void AddEditMenuItem_Load(object sender, EventArgs e)
        {

        }

        private void btnX_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbxName.Text=="")
            {
                MessageBox.Show(this, "The Item Name can not be left blank", "Naming Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                if (tbxDescription.Text=="")
                {
                MessageBox.Show(this, "The Description can not be empty", tbxName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    if (tbxPrice.Text == "")
                    {
                    MessageBox.Show(this, "The Price can not be empty", tbxName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    if (tbxDescription.TextLength == 10)
                    {
                        MessageBox.Show(this, "The Description must contain at least 10 characters", tbxName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        if (cbxType.Text == "")
                        {
                            MessageBox.Show(this, "Make sure to select a category for Dish/Item Type", tbxName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            bool flag = false;
                            foreach(RestaurantMenuItem item in OwnerStorage.MenuItems)
                            {
                                if (item.name == tbxName.Text)
                                flag = true;
                            }
                            if (flag == true && transferedItem==null)
                                {
                                MessageBox.Show(this, "A Dish/Item with the same name ("+tbxName.Text+") already exists",tbxName.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
                                }   
                            else
                            {
                                RestaurantMenuItem newItem = new RestaurantMenuItem();
                                newItem.name = tbxName.Text;
                                newItem.ingredients = tbxDescription.Text;
                                newItem.type = cbxType.Text;
                                newItem.restaurantId = OwnerStorage.ThisRestaurant.objectId;
                                newItem.price = Convert.ToDouble(tbxPrice.Text);
                                newItem.outOfStock = false;
                                if (transferedItem!=null)
                                    newItem.objectId = transferedItem.objectId;

                                transferedItem = newItem;
                                
                                
                                OwnerStorage.FileWriter.WriteLineToFile("User Added a new Menu Item", true);
                                OwnerStorage.FileWriter.WriteLineToFile("Name:  " + newItem.name, false);

                    DialogResult = DialogResult.OK;
                    this.Close();
                            }            
                        }
        }
         
        private void tbxPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxPrice_KeyPress(object sender, KeyPressEventArgs e)
        {

                // Verify that the pressed key isn't CTRL or any non-numeric digit
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                // If you want, you can allow decimal (float) numbers
                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
