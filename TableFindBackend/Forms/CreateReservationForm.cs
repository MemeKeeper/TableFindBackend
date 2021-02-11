using BackendlessAPI;
using BackendlessAPI.Async;
using System;
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
            lblTitle.Text = thisTable.Name;
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

        private void ShowLoading(bool toggle)
        {
            if (toggle == true)
            {
                pbxLoading.Visible = true;
                btnCancel.Enabled = false;
                btnCreate.Enabled = false;
                btnExit.Enabled = false;
            }
            else
            {
                pbxLoading.Visible = false;
                btnCancel.Enabled = true;
                btnCreate.Enabled = true;
                btnExit.Enabled = true;
            }
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

                if ((dtpTakenFrom.Value.TimeOfDay > OwnerStorage.ThisRestaurant.Open.TimeOfDay) && ((dtpTakenFrom.Value.TimeOfDay.Hours + spnDuration.Value) < OwnerStorage.ThisRestaurant.Close.Hour))
                {
                    Reservation flag = null;
                    foreach (Reservation r in OwnerStorage.ActiveReservations)
                    {
                        if (r.TableId == thisTable.objectId && ((dtpTakenFrom.Value > r.TakenFrom && dtpTakenFrom.Value < r.TakenTo) || ((dtpTakenFrom.Value.AddHours(Convert.ToInt32(spnDuration.Value))) > r.TakenFrom && (dtpTakenFrom.Value.AddHours(Convert.ToInt32(spnDuration.Value))) < r.TakenTo)
                            || dtpTakenFrom.Value < r.TakenFrom && dtpTakenFrom.Value.AddHours(Convert.ToInt32(spnDuration.Value)) > r.TakenTo))
                        {
                            flag = r;
                        }
                    }

                    if (flag == null)
                    {
                        ShowLoading(true);
                        DateTime tempTime = dtpTakenFrom.Value.AddHours(2);   //<-- +2:00 time zone
                        tempTime.AddHours(Convert.ToInt32(spnDuration.Value));

                        reservation.Number = tbxContact.Text;
                        reservation.Name = tbxName.Text;
                        reservation.TakenFrom = dtpTakenFrom.Value.AddHours(2);
                        reservation.TableId = thisTable.objectId;
                        reservation.TakenTo = dtpTakenFrom.Value.AddHours(Convert.ToInt32(spnDuration.Value) + 2);   //the +2:00 timezone
                        reservation.RestaurantId = OwnerStorage.ThisRestaurant.objectId;
                        reservation.UserId = OwnerStorage.CurrentlyLoggedIn.ObjectId;
                        reservation.Active = true;
                        reservation.ReasonForExpiration = "";

                        AsyncCallback<Reservation> callback = new AsyncCallback<Reservation>(
                                            result =>
                                            {
                                                Invoke(new Action(() =>
                                                {
                                                    pnlPanel.Visible = true;
                                                    OwnerStorage.FileWriter.WriteLineToFile("User Created Manager Reservation", true);
                                                    OwnerStorage.FileWriter.WriteLineToFile("Name:  " + reservation.Name, false);

                                                    MessageBox.Show(this, "Reservation for " + reservation.Name + " is successfull");
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
                    }
                    else
                    {
                        {
                            ShowLoading(false);
                            MessageBox.Show(this, "this Reservation clashes with " + flag.Name + " reservation for " + flag.TakenFrom.ToString("HH:mm") + " to " + flag.TakenTo.ToString("HH:mm"));
                        }
                    }
                }
                else
                {
                    ShowLoading(false);
                    MessageBox.Show(this, "This reservation is outside the restaurant's set open and close times, please select another time.");
                }
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
