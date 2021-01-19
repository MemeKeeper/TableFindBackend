﻿using BackendlessAPI;
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
    public partial class CustomerDetailsForm : Form
    {
        public CustomerDetailsForm(BackendlessUser user, Reservation r)
        {
            InitializeComponent();

            if (user.ObjectId == OwnerStorage.ThisRestaurant.ownerId)
            {
                lblRestaurantLabel.Visible = true;
                lblTitle.Text = "Reservation details for " + r.name;
            }
            else
            {
                tbxContact.Text = user.GetProperty("Cellphone").ToString();
                tbxFName.Text = user.GetProperty("FirstName").ToString();
                tbxLName.Text = user.GetProperty("LastName").ToString();
                tbxEmail.Text = user.GetProperty("email").ToString();
                lblTitle.Text = "Reservation details for " + user.GetProperty("FirstName").ToString();
            }

            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
