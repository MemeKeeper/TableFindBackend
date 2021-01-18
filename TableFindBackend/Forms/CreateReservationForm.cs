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

namespace TableFindBackend.Models
{
    public partial class CreateReservationForm : Form
    {

        RestaurantTable thisTable;
        public CreateReservationForm(RestaurantTable thisTable)
        {
            InitializeComponent();

            //dtpTakenFrom.Format = DateTimePickerFormat.Custom;
            //dtpTakenFrom.CustomFormat = "dddd, dd/MM, HH:mm";

            this.thisTable = thisTable;
            lblTitle.Text = thisTable.name;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Reservation reservation = new Reservation();

            if (tbxContact.Text == "" || tbxName.Text == "")
            {
                Invoke(new Action(() =>
                {
                    MessageBox.Show(this, "Make sure to fill in all fields");
                }));
            }
            else
            if (tbxContact.Text.Length != 10)
            {
                MessageBox.Show(this, "The Contact number you have entered is invalid");
            }
            else
            {
                pnlPanel.Visible = false;
                DateTime tempTime = dtpTakenFrom.Value.AddHours(2);   //<-- +2:00 time zone
                tempTime.AddHours(Convert.ToInt32(spnDuration.Value));             

                    reservation.number = tbxContact.Text;
                    reservation.name = tbxName.Text;
                    reservation.takenFrom = dtpTakenFrom.Value.AddHours(2);
                    reservation.tableId = thisTable.objectId;
                    reservation.takenTo = dtpTakenFrom.Value.AddHours(Convert.ToInt32(spnDuration.Value)+2);   //the +2:00 timezone
                    reservation.restaurantId = OwnerStorage.ThisRestaurant.objectId;
                    reservation.userId = OwnerStorage.CurrentlyLoggedIn.ObjectId;

                    AsyncCallback<Reservation> callback = new AsyncCallback<Reservation>(
                                        result =>
                                        {
                                            Invoke(new Action(() =>
                                            {
                                                pnlPanel.Visible = true;
                                                OwnerStorage.FileWriter.WriteLineToFile("User Created Manager Reservation", true);
                                                OwnerStorage.FileWriter.WriteLineToFile("Name:  " + reservation.name, false);

                                                MessageBox.Show(this, "Reservation for " + reservation.name + " is successfull");
                                                DialogResult = DialogResult.OK;
                                                this.Close();
                                            }));
                                        },
                                        fault =>
                                        {
                                            Invoke(new Action(() =>
                                            {
                                                OwnerStorage.FileWriter.WriteLineToFile("Reservation failed", true);

                                                OwnerStorage.FileWriter.WriteLineToFile("Error: " + fault.Message.ToString(), false);

                                                MessageBox.Show(this, "Error: " + fault.Message);
                                            }));
                                        });
                    Backendless.Data.Of<Reservation>().Save(reservation, callback);
             
                //else
                //{
                //    pnlPanel.Visible = true;
                //    MessageBox.Show(this, "this Reservation clashes with "+flag.name+" reservation for "+flag.takenFrom.ToString("HH:mm")+" to " + flag.takenTo.ToString("HH:mm"));
                //}
            }
        }

        private void btnCalander_Click(object sender, EventArgs e)
        {
            MonthCalendar MonthCalendar = new MonthCalendar();
            Panel PanelCalendar = new Panel();

            PanelCalendar.Controls.Add(MonthCalendar);
        }

        private void lblContact_Click(object sender, EventArgs e)
        {

        }

        private void tbxContact_TextChanged(object sender, EventArgs e)
        {
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

        private void dtpTakenFrom_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
