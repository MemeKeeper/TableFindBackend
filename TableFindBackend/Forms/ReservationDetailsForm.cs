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
    public partial class ReservationDetailsForm : Form
    {
        Reservation thisReservation;
        public ReservationDetailsForm(Reservation r, BackendlessUser u, RestaurantTable t)
        {
            thisReservation = r;

        
            InitializeComponent();

            tbxCapacity.Text = t.capacity.ToString();
            tbxContact.Text = u.GetProperty("Cellphone").ToString();
            tbxContact2.Text = r.number;
            tbxEmail.Text = u.Email;
            tbxFName.Text = u.GetProperty("FirstName").ToString();
            tbxLName.Text = u.GetProperty("LastName").ToString();
            tbxTable.Text = t.name;
            tbxTime.Text = r.takenFrom.ToString("dddd, dd/MM,    HH:mm") + " - " + r.takenTo.ToString("HH:mm");
            lblTitle.Text ="Reservation for "+ r.name;

            if (u.ObjectId == OwnerStorage.CurrentlyLoggedIn.ObjectId)
            {
                lblMadeByRestaurant.Visible = true;
            }
            else
            {
                lblContactNumber.Visible = false;
                tbxContact2.Visible = false;
                pnlReservation.Height += -50;
            }

            if (OwnerStorage.AdminMode == true)
                btnDelete.Visible = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ShowLoading(bool show)
        {
            if (show ==true)
            {
                pbxLoading.Visible = true;
                btnClose.Enabled = false;
                btnDelete.Enabled = false;
                btnExit.Enabled = false;
            }
            else
            {
                pbxLoading.Visible = false;
                btnClose.Enabled = true;
                btnDelete.Enabled = true;
                btnExit.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (OwnerStorage.AdminMode == true)
            {
                DialogResult remove = MessageBox.Show("Are you sure you want to remove this reservation?", "Remove Reservation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (remove == DialogResult.Yes)
                {
                    ShowLoading(true);
                    AsyncCallback<Reservation> saveObjectCallback = new AsyncCallback<Reservation>(
                      savedReservation =>
                      {


                          AsyncCallback<long> deleteObjectCallback = new AsyncCallback<long>(
                            deletionTime =>
                            {
                                Invoke(new Action(() =>
                                {
                                    OwnerStorage.LogInfo.Add("Reservation has been removed\nName:   "+ thisReservation.name);
                                    OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("dd/MM,   HH:mm"));

                                    MessageBox.Show(this, "reservation for " + thisReservation.name+ " has been removed");
                                    ShowLoading(false);
                                    this.Close();
                                }));
                            },
                            error =>
                            {
                                Invoke(new Action(() =>
                                {
                                    MessageBox.Show(this, "Error: " + error.Message);
                                    ShowLoading(false);
                                }));
                            });
                          Backendless.Persistence.Of<Reservation>().Remove(savedReservation, deleteObjectCallback);
                      },
                      error =>
                      {
                          Invoke(new Action(() =>
                          {
                              MessageBox.Show(this, "Error: " + error.Message);
                              ShowLoading(false);
                          }));
                      }
                    );

                    Backendless.Persistence.Of<Reservation>().Save(thisReservation, saveObjectCallback);
                }
            }
            else
            {
                MessageBox.Show("You do not have permission to remove this reservation. Only managers or selected assistant mangers can remove reservations");
            }
        }
    }
}
