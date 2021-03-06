﻿using System;
using System.Windows.Forms;
using TableFindBackend.Global_Variables;
using TableFindBackend.Models;

namespace TableFindBackend.Forms
{
    //This form is used to either create or modify Menu Items
    public partial class AddEditMenuItem : Form
    {
        //Public property that makes retrieving the modified MenuItem effective and easy
        public RestaurantMenuItem transferedItem { get; set; } 
        public AddEditMenuItem(RestaurantMenuItem item)
        {
            InitializeComponent();
            this.transferedItem = item;
            //Determines if this form should be used to create a new Menu Item or modify an existing one
            if (transferedItem != null) 
            {
                lblTitle.Text = "Editing Menu Item";
                tbxDescription.Text = item.Ingredients;
                tbxName.Text = item.Name;
                cbxType.Text = item.Type;
                nudPrice.Text = item.Price.ToString("N2");
            }
        }

        //Button used to close the form if the user wishes to not save changes made
        private void btnCancel_Click(object sender, EventArgs e) 
        {            
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbxName.Text == "") //Validation that makes sure the Name field is not empty
            {
                MessageBox.Show(this, "The Item Name can not be left blank", "Naming Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                if (tbxDescription.Text == "") //Validation that makes sure the description field is not empty
            {
                MessageBox.Show(this, "The Description can not be empty", tbxName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                    if (nudPrice.Text == "") //Validation that makes sure the price field is not empty
            {
                MessageBox.Show(this, "The Price can not be empty", tbxName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                    if (tbxDescription.TextLength < 10) //Validation that makes sure the description is no less than 10 characters long
            {
                MessageBox.Show(this, "The Description must contain at least 10 characters", tbxName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                        if (cbxType.Text == "") //Validation that makes sure that a type has been selected
            {
                MessageBox.Show(this, "Make sure to select a category for Dish/Item Type", tbxName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Determines if a Menu Item with the same name exists
                bool flag = false;
                foreach (RestaurantMenuItem item in OwnerStorage.MenuItems)
                {
                    if (item.Name == tbxName.Text)
                        flag = true;
                }
                if (flag == true && transferedItem == null)
                {
                    MessageBox.Show(this, "A Dish/Item with the same name (" + tbxName.Text + ") already exists", tbxName.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //MenuItem object gets created
                    RestaurantMenuItem newItem = new RestaurantMenuItem();
                    newItem.Name = tbxName.Text;
                    newItem.Ingredients = tbxDescription.Text;
                    newItem.Type = cbxType.Text;
                    newItem.RestaurantId = OwnerStorage.ThisRestaurant.objectId;
                    newItem.Price = Convert.ToDouble(nudPrice.Text);
                    newItem.OutOfStock = false;
                    if (transferedItem != null)
                        newItem.objectId = transferedItem.objectId;

                    //Since the Backendless creation happens on the parent form, we only have to assign it to the public property and then close this form
                    transferedItem = newItem; 


                    OwnerStorage.FileWriter.WriteLineToFile("User Added a new Menu Item", true);
                    OwnerStorage.FileWriter.WriteLineToFile("Name:  " + newItem.Name, false);

                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        //Button used to close the form if the user wishes to not save changes made
        private void btnExit_Click(object sender, EventArgs e) 
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void nudPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Makes sure that the user only enters the pre-configured setting on the platform for financial variables (comma or point)
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }

        }
    }
}
