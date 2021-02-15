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
     //this form is to toggle the capacity status of the restaurant
        
        //basic constructor. sets the information on the form to the currently saved setting
        public RestaurantStatusForm()
        {
            InitializeComponent();
            tbrCapacity.Value = OwnerStorage.ThisRestaurant.MaxCapacity;
        }

        //Button used to close the form if the user wishes to not save changes made
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //this method will just apply the changes made in the slider control on the form.
        private void btnEditor_Click(object sender, EventArgs e)
        {
            //simulate a loading screen
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
                                switch (tbrCapacity.Value)//a switch to easily determine which value has been selected
                                {
                                    case 0://<------- not busy
                                        {
                                            OwnerStorage.FileWriter.WriteLineToFile("User changed the restaurant capacity status to 'Not Busy'", true);
                                            OwnerStorage.LogInfo.Add("User changed the restaurant capacity status to 'Not Busy'");
                                            OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                                            break;
                                        }
                                    case 1://<------- Medium Load
                                        {
                                            OwnerStorage.FileWriter.WriteLineToFile("User changed the restaurant capacity status to 'Medium Load'", true);
                                            OwnerStorage.LogInfo.Add("User changed the restaurant capacity status to 'Medium Load'");
                                            OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                                            break;
                                        }
                                    default://<------- Very Busy
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
                            //something went wrong, a error message will now display
                            Invoke(new Action(() =>
                            {
                                pbxLoading.Visible = false;
                                btnSave.Enabled = true;
                                btnExit.Enabled = true;
                                btnCancel.Enabled = true;
                                MessageBox.Show(this,error.Message.ToString());
                            }));
                        });

            AsyncCallback<Restaurant> saveObjectCallback = new AsyncCallback<Restaurant>(
              savedRestaurant =>
              {
                  //success, now the object can be updated
                  Backendless.Persistence.Of<Restaurant>().Save(savedRestaurant, updateObjectCallback);
              },
              error =>
              {
                  //something went wrong, a error message will now display
                  Invoke(new Action(() =>
                  {
                      pbxLoading.Visible = false;
                      btnSave.Enabled = true;
                      btnExit.Enabled = true;
                      btnCancel.Enabled = true;
                      MessageBox.Show(error.Message.ToString());
                  }));
              });

            //Backendless demands that the object be saved first before it can be updated
            Backendless.Persistence.Of<Restaurant>().Save(OwnerStorage.ThisRestaurant, saveObjectCallback);
        }

        //Blocks the "alt F4" capability so that the user cannot close the program while a process is running
        private void RestaurantStatusForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == System.Windows.Forms.CloseReason.UserClosing && pbxLoading.Visible == true)
            {
                e.Cancel = true;
            }
        }

        //Button used to close the form if the user wishes to not save changes made
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
