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
        public EditTableForm(RestaurantTable item)
        {
            table = item;
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            availability = item.available;

            if (availability == true)
                btnDisable.Text = "Make Table Unavailable";
            else
                btnDisable.Text = "Make Table Available";

            edtName.Text = item.name;
            spnSeating.Value = item.capacity;
            rtbInfo.Text = item.tableInfo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            showLoading(true);
            table.name = edtName.Text;
            table.capacity = Convert.ToInt32(spnSeating.Value);
            table.available = availability;
            table.tableInfo = rtbInfo.Text ;

            AsyncCallback<RestaurantTable> updateObjectCallback = new AsyncCallback<RestaurantTable>(
              savedTable =>
              {
                  Invoke(new Action(() =>
                  {
                      showLoading(false);
                      MessageBox.Show(this, table.name + " has been updated!");
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
            }
            else
            {
                pbxLoading.Visible = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result;
            if (table.objectId!=null)
            {
                result = MessageBox.Show(this, "Are you sure you would like to delete '" + table.name + "'? All reservations made under this table by customers will be lost!", "Delete '" + table.name + "' ?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
            else
            {
                result = MessageBox.Show(this, "Are you sure you would like to delete '" + table.name + "'?", "Delete '" + table.name + "' ?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
            
            if (result == DialogResult.Yes)
            {
                showLoading(true);

                AsyncCallback<long> deleteObjectCallback = new AsyncCallback<long>(
                deletionTime =>
                {
                    Invoke(new Action(() =>
                    {
                        OwnerStorage.FileWriter.WriteLineToFile("User deleted table ", true);
                        OwnerStorage.FileWriter.WriteLineToFile("Name:  " + table.name, false);             
                    }));
                    if (table.objectId != null)
                    {
                        string whereClause = "tableId = '" + table.objectId + "'";

                        AsyncCallback<int> deleteReservationsCallback = new AsyncCallback<int>(
                        objectsDeleted =>
                        {
                            Invoke(new Action(() =>
                            {
                                showLoading(false);
                                MessageBox.Show(this, table.name + " has been removed, along with "+ objectsDeleted.ToString()+" reservations");
                                OwnerStorage.FileWriter.WriteLineToFile("User deleted "+objectsDeleted.ToString() + " reservations",false);
                                DialogResult = DialogResult.Yes;
                                this.Close();
                            }));
                        },
                        error =>
                        {
                            System.Console.WriteLine("Server returned an error " + error.Message);
                        });

                        Backendless.Data.Of<Reservation>().Remove(whereClause, deleteReservationsCallback);
                    }
                    else
                    {
                        Invoke(new Action(() =>
                        {
                            showLoading(false);
                            MessageBox.Show(this, table.name + " has been removed");                        
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

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            ReservationsForm bookings = new ReservationsForm(table);
            bookings.ShowDialog();
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {

            if (availability == false)
            {
                btnDisable.Text = "Make Table Unavailable";
                availability = true;

                OwnerStorage.FileWriter.WriteLineToFile("User made table '"+table.name+"' available", true);
            }
            else
            {
                btnDisable.Text = "Make Table Available";
                availability = false;

                OwnerStorage.FileWriter.WriteLineToFile("User made table '" + table.name + "' unavailable", true);
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