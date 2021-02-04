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
                                pbxLoading.Visible=false;
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
