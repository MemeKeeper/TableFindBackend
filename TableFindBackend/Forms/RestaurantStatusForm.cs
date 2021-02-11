using BackendlessAPI;
using BackendlessAPI.Async;
using System;
using System.Windows.Forms;
using TableFindBackend.Global_Variables;
using TableFindBackend.Models;

namespace TableFindBackend.Forms
{
    public partial class RestaurantStatusForm : Form
    {
        public RestaurantStatusForm()
        {
            InitializeComponent();
            tbrCapacity.Value = OwnerStorage.ThisRestaurant.MaxCapacity;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditor_Click(object sender, EventArgs e)
        {
            pbxLoading.Visible = true;
            btnSave.Enabled = false;
            btnExit.Enabled = false;
            btnCancel.Enabled = false;
            OwnerStorage.ThisRestaurant.MaxCapacity = tbrCapacity.Value;

            AsyncCallback<Restaurant> updateObjectCallback = new AsyncCallback<Restaurant>(
                        savedRestaurant =>
                        {
                            Invoke(new Action(() =>
                            {
                                switch (tbrCapacity.Value)
                                {
                                    case 0:
                                        {
                                            OwnerStorage.FileWriter.WriteLineToFile("User changed the restaurant capacity status to 'Not Busy'", true);
                                            OwnerStorage.LogInfo.Add("User changed the restaurant capacity status to 'Not Busy'");
                                            OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                                            break;
                                        }
                                    case 1:
                                        {
                                            OwnerStorage.FileWriter.WriteLineToFile("User changed the restaurant capacity status to 'Medium Load'", true);
                                            OwnerStorage.LogInfo.Add("User changed the restaurant capacity status to 'Medium Load'");
                                            OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                                            break;
                                        }
                                    default:
                                        {
                                            OwnerStorage.FileWriter.WriteLineToFile("User changed the restaurant capacity status to 'Very Busy'", true);
                                            OwnerStorage.LogInfo.Add("User changed the restaurant capacity status to 'Very Busy'");
                                            OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                                            break;
                                        }
                                }

                                pbxLoading.Visible = false;
                                DialogResult = DialogResult.OK;
                                this.Close();
                            }));

                        },
                        error =>
                        {
                            Invoke(new Action(() =>
                            {
                                pbxLoading.Visible = false;
                                btnSave.Enabled = true;
                                btnExit.Enabled = true;
                                btnCancel.Enabled = true;
                                MessageBox.Show(error.Message.ToString());
                            }));
                        });

            AsyncCallback<Restaurant> saveObjectCallback = new AsyncCallback<Restaurant>(
              savedRestaurant =>
              {
                  Backendless.Persistence.Of<Restaurant>().Save(savedRestaurant, updateObjectCallback);
              },
              error =>
              {
                  Invoke(new Action(() =>
                  {
                      pbxLoading.Visible = false;
                      btnSave.Enabled = true;
                      btnExit.Enabled = true;
                      btnCancel.Enabled = true;
                      MessageBox.Show(error.Message.ToString());
                  }));
              }
            );

            Backendless.Persistence.Of<Restaurant>().Save(OwnerStorage.ThisRestaurant, saveObjectCallback);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
