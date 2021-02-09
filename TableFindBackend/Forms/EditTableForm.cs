using BackendlessAPI;
using BackendlessAPI.Async;
using BackendlessAPI.Exception;
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
    public partial class EditTableForm : Form
    {
        RestaurantTable table;
        private bool availability;
        MainForm _master;
        public EditTableForm(RestaurantTable item,MainForm _master)
        {
            table = item;
            this._master = _master;
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            availability = item.Available;

            if (availability == true)
                btnDisable.Text = "Make Table Unavailable";
            else
                btnDisable.Text = "Make Table Available";

            edtName.Text = item.Name;
            spnSeating.Value = item.Capacity;
            rtbInfo.Text = item.TableInfo;
        }

        private void button1_Click(object sender, EventArgs e)
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
            showLoading(true);
            table.Name = edtName.Text;
            table.Capacity = Convert.ToInt32(spnSeating.Value);
            table.Available = availability;
            table.TableInfo = rtbInfo.Text ;

            AsyncCallback<RestaurantTable> updateObjectCallback = new AsyncCallback<RestaurantTable>(
              savedTable =>
              {
                  Invoke(new Action(() =>
                  {
                      showLoading(false);
                      MessageBox.Show(this, table.Name + " has been updated!");
                      DialogResult = DialogResult.OK;
                      this.table.objectId = savedTable.objectId;
                    //OwnerStorage.TempTable= table;

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

            AsyncCallback<RestaurantTable> saveObjectCallback = new AsyncCallback<RestaurantTable>(
              savedTable =>
              {

                  Backendless.Persistence.Of<RestaurantTable>().Save(savedTable, updateObjectCallback);

              },
              error =>
              {
                  showLoading(false);
                  MessageBox.Show(this, "Error: " + error.Message);
              });
            Backendless.Persistence.Of<RestaurantTable>().Save(table, saveObjectCallback);

        }
        private void showLoading(bool activate)
        {
            if (activate == true)
            {
                pbxLoading.Visible = true;
                btnCancel.Enabled = false;
                btnClose.Enabled = false;
                btnDelete.Enabled = false;
                btnSave.Enabled = false;
                btnViewDetails.Enabled = false;
                btnDisable.Enabled = false;
                edtName.Enabled = false;
                spnSeating.Enabled = false;
                rtbInfo.Enabled = false;

            }
            else
            {
                pbxLoading.Visible = false;
                btnCancel.Enabled = true;
                btnClose.Enabled = true;
                btnDelete.Enabled = true;
                btnSave.Enabled = true;
                btnViewDetails.Enabled = true;
                btnDisable.Enabled = true;
                edtName.Enabled = true;
                spnSeating.Enabled = true;
                rtbInfo.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<Reservation> rList = new List<Reservation>();
            foreach(Reservation r in OwnerStorage.ActiveReservations)
            {
                if(r.TableId == table.objectId)
                {
                    rList.Add(r);
                }
            }
            if(availability == false && rList.Count == 0)
            {
                
                DialogResult result;
                result = MessageBox.Show(this, "Are you sure you would like to remove '" + table.Name + "'?", "Remove '" + table.Name + "' ?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                
                if (result == DialogResult.Yes)
                {
                    showLoading(true);

                    AsyncCallback<long> deleteObjectCallback = new AsyncCallback<long>(
                    deletionTime =>
                    {
                        Invoke(new Action(() =>
                        {
                            OwnerStorage.FileWriter.WriteLineToFile("User removed table ", true);
                            OwnerStorage.FileWriter.WriteLineToFile("Name:  " + table.Name, false);
                        }));
                        if (table.objectId != null) //table exists in database
                        {

                            Invoke(new Action(() =>
                            {
                                showLoading(false);
                                MessageBox.Show(this, table.Name + " has been removed");
                                DialogResult = DialogResult.Yes;
                                this.Close();
                            }));
                            
                        }
                        else
                        {
                            Invoke(new Action(() =>
                            {
                                showLoading(false);
                                MessageBox.Show(this, table.Name + " has been removed");
                                DialogResult = DialogResult.Yes;
                                this.Close();
                            }));
                        }

                    },
                    error =>
                    {
                        Invoke(new Action(() =>
                        {
                            showLoading(false);
                            MessageBox.Show(this, "Error: " + error.Message);
                        }));
                    });

                    AsyncCallback<RestaurantTable> saveObjectCallback = new AsyncCallback<RestaurantTable>(
                      savedTable =>
                      {

                          Backendless.Persistence.Of<RestaurantTable>().Remove(savedTable, deleteObjectCallback);
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

                    Backendless.Persistence.Of<RestaurantTable>().Save(table, saveObjectCallback);
                }
            }
            else
            {
                MessageBox.Show("The table has to be made unavailable and have no reservations under it in order to remove this table from the restaurant. Please make these changes before trying again.");
            }
            
            
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            ReservationsForm bookings = new ReservationsForm(table, _master);
            bookings.ShowDialog();
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {

            if (availability == false)
            {
                btnDisable.Text = "Make Table Unavailable";
                availability = true;

                OwnerStorage.FileWriter.WriteLineToFile("User made table '"+table.Name+"' available", true);
            }
            else
            {
                btnDisable.Text = "Make Table Available";
                availability = false;

                OwnerStorage.FileWriter.WriteLineToFile("User made table '" + table.Name + "' unavailable", true);
            }
        }

        private void EditTableForm_Load(object sender, EventArgs e)
        {

        }
        public RestaurantTable RetreiveEditedTable()
        {
            return table;
        }
    }       
}