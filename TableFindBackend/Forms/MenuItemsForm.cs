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
using TableFindBackend.ViewModels;

namespace TableFindBackend.Forms
{
    public partial class MenuItemsForm : Form
    {
        private RestaurantMenuItem selectedItem;
        public MenuItemsForm()
        {
            InitializeComponent();
            populateMenu();
        }

        private void populateMenu() // Controller for RestaurantMenuItem
        {
            flpItems.Controls.Clear();

            foreach (RestaurantMenuItem item in OwnerStorage.MenuItems)
            {
                RestaurantMenuItemView newItem = new RestaurantMenuItemView();
                newItem.Tag = item.objectId;
                newItem.Active = item.outOfStock?false:true;
                newItem.Price = item.price;

                newItem.Type = item.type;
                if (newItem.Active != true)
                {
                    newItem.BackColor = Color.FromName("ControlDark");
                }
                newItem.Label = item.name;

                flpItems.Controls.Add(newItem);
                newItem.lblName.MouseClick += new MouseEventHandler(MenuLabel_Click);
                newItem.MouseClick += new MouseEventHandler(MenuItem_Click);
            }
        }

        public void SelectItemHighLight(String id)
        {
            foreach (RestaurantMenuItemView viewItem in flpItems.Controls)
                if ((String)viewItem.Tag == id)
                {
                    {
                        viewItem.BackColor = Color.FromName("ActiveCaption");
                        foreach (RestaurantMenuItemView view in flpItems.Controls)
                        {
                            if (view.Tag != viewItem.Tag)
                            {
                                if (view.Active == true)
                                    view.resetColor();
                                else
                                {
                                    view.BackColor = Color.FromName("ControlDark");
                                }

                            }
                        }
                        foreach (RestaurantMenuItem item in OwnerStorage.MenuItems)
                        {
                            if (viewItem.Label == item.name)
                            {
                                selectedItem = item;
                            }
                        }
                        cbxEnabled.Checked = selectedItem.outOfStock ? false : true;
                        pnlEdit.Enabled = true;
                    }
                }
        }
        private void MenuLabel_Click(object sender, MouseEventArgs e)
        {
            
            Label tempLabel = (Label)sender;
            RestaurantMenuItemView temp = (RestaurantMenuItemView)tempLabel.Parent;
            MenuItem_Click(temp, e) ;
        }
        private void MenuItem_Click(object sender, MouseEventArgs e)
        {
            RestaurantMenuItemView tempItem = (RestaurantMenuItemView)sender;
            string id = tempItem.Tag.ToString();
            SelectItemHighLight(id);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            AddOrEdit(selectedItem);
        }
        private void AddOrEdit(RestaurantMenuItem temp) // Controller for RestaurantMenuItem
        {
            AddEditMenuItem newForm = new AddEditMenuItem(temp);
            
            
                if (newForm.ShowDialog() == DialogResult.OK)
                {
                    AsyncCallback<RestaurantMenuItem> callback = new AsyncCallback<RestaurantMenuItem>(
                        result =>
                        {
                            Invoke(new Action(() =>
                            {
                                if (temp != null)//means an existing item was edited
                                {
                                    OwnerStorage.MenuItems.Insert(OwnerStorage.MenuItems.IndexOf(temp), newForm.transferedItem);
                                    OwnerStorage.MenuItems.Remove(temp);

                                    foreach(RestaurantMenuItemView item in flpItems.Controls)
                                    {
                                        if (item.Tag.Equals(temp.objectId))
                                        {
                                            item.Name = newForm.transferedItem.name;
                                            item.Type = newForm.transferedItem.type;
                                            item.Price = newForm.transferedItem.price;
                                        }
                                    }
                                    populateMenu();
                                    SortRefresh();
                                    SelectItemHighLight(selectedItem.objectId);
                                }
                                else//means a new Item is created
                                {
                                    RestaurantMenuItemView newView = new RestaurantMenuItemView();
                                    newView.Label = result.name;
                                    newView.Active = true;
                                    newView.Type = result.type;
                                    newView.Tag = result.objectId;
                                    newView.Price = result.price;

                                    OwnerStorage.MenuItems.Add(result);

                                    newView.lblName.MouseClick += new MouseEventHandler(MenuLabel_Click);
                                    newView.MouseClick += new MouseEventHandler(MenuItem_Click);
                                    flpItems.Controls.Add(newView);
                                }
                        }));
                        },
                        fault =>
                        {
                            Invoke(new Action(() =>
                            {
                                MessageBox.Show(this, "Error: " + fault.Message);
                            }));
                        });
                    Backendless.Data.Of<RestaurantMenuItem>().Save(newForm.transferedItem, callback);
                }
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            RestaurantMenuItem temp= null;
            AddOrEdit(temp);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pnlEdit.Enabled = false;
            populateMenu();            
            clbSortOptions.ClearSelected();
            while (clbSortOptions.CheckedIndices.Count > 0)
            {
                clbSortOptions.SetItemChecked(clbSortOptions.CheckedIndices[0], false);
            }
        }

        private void cbxEnabled_CheckedChanged(object sender, EventArgs e)
        {

            }

        private void cbxEnabled_Click(object sender, EventArgs e)
        {
            selectedItem.outOfStock = cbxEnabled.Checked ? false : true;
            AsyncCallback<RestaurantMenuItem> updateObjectCallback = new AsyncCallback<RestaurantMenuItem>(
            savedRestaurantMenuItem =>
            {
                Invoke(new Action(() =>
                {
                    System.Console.WriteLine("Restaurant details has been updated");
                    populateMenu();
                    SortRefresh();
                    SelectItemHighLight(selectedItem.objectId);
                }));
            },
            error =>
            {
                Invoke(new Action(() =>
                {
                    MessageBox.Show(this, "Error: " + error.Message);
                }));
            });

            AsyncCallback<RestaurantMenuItem> saveObjectCallback = new AsyncCallback<RestaurantMenuItem>(
              savedRestaurantMenuItem =>
              {
                  Backendless.Persistence.Of<RestaurantMenuItem>().Save(savedRestaurantMenuItem, updateObjectCallback);
              },
              error =>
              {
                  Invoke(new Action(() =>
                  {
                      MessageBox.Show(this, "Error: " + error.Message);
                  }));
              }
            );

            Backendless.Persistence.Of<RestaurantMenuItem>().Save(selectedItem, saveObjectCallback);

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            RestaurantMenuItem tempItem = selectedItem;
            DialogResult dialogResult = MessageBox.Show("Are you sure you would like to delete "+ tempItem.name+"?", tempItem.name, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                pbxLoading.Visible = true;

                
                AsyncCallback<long> deleteObjectCallback = new AsyncCallback<long>(
                deletionTime =>
                {

                    Invoke(new Action(() =>
                    {
                        foreach (RestaurantMenuItemView view in flpItems.Controls)
                        {
                            if (view.Tag.ToString() == tempItem.objectId)
                            {
                                flpItems.Controls.Remove(view);

                            }
                        }
                        OwnerStorage.MenuItems.Remove(tempItem);
                        populateMenu();
                        pnlEdit.Enabled = false;
                        pbxLoading.Visible=false;
                        lblStatus.Text=selectedItem.name + " has been removed";
                    }));
                },
                error =>
                {
                    Invoke(new Action(() =>
                    {
                        pbxLoading.Visible = false;
                    lblStatus.Text= "Error: " + error.Message;
                    }));
                });

                    AsyncCallback<RestaurantMenuItem> saveObjectCallback = new AsyncCallback<RestaurantMenuItem>(
                    savedMenuItem =>
                    {

                        Backendless.Persistence.Of<RestaurantMenuItem>().Remove(savedMenuItem, deleteObjectCallback);
                    },
                    error =>
                    {
                        Invoke(new Action(() =>
                          {
                              pbxLoading.Visible = false;
                              lblStatus.Text = "Error: " + error.Message;
                          }));
                    }
                    );

                    Backendless.Persistence.Of<RestaurantMenuItem>().Save(tempItem, saveObjectCallback);
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }            
        }
        private void clbSortOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clbSortOptions.SelectedIndex == 3)
            {
                cbxType.Enabled = true;                
            }
            else
            {
                cbxType.Enabled = false;                
            }
            if(clbSortOptions.SelectedIndex!=-1)
            clbSortOptions.SetItemChecked(clbSortOptions.SelectedIndex, true);

        }

        private void clbSortOptions_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int ix = 0; ix < clbSortOptions.Items.Count; ++ix)
                if (ix != e.Index)
                    clbSortOptions.SetItemChecked(ix, false);
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            SortRefresh();
        }
        private void SortRefresh()
        {
            List<RestaurantMenuItemView> tempList = new List<RestaurantMenuItemView>();
            foreach (RestaurantMenuItemView r in flpItems.Controls)
            {
                tempList.Add(r);
            }
            flpItems.Controls.Clear();
            switch (clbSortOptions.SelectedIndex)
            {
                case -1:
                    {
                        break;
                    }
                case 0:
                    {
                        RestaurantMenuItemView tempView;

                        for (int write = 0; write < tempList.Count; write++)
                        {
                            for (int sort = 0; sort < tempList.Count - 1; sort++)
                            {
                                if (tempList[sort].Price > tempList[sort + 1].Price)
                                {
                                    tempView = tempList[sort];
                                    tempList[sort] = tempList[sort + 1];
                                    tempList[sort + 1] = tempView;
                                }
                            }
                        }
                        flpItems.Controls.AddRange(tempList.ToArray());
                        break;
                    }
                case 1:
                    {
                        RestaurantMenuItemView tempView;

                        for (int write = 0; write < tempList.Count; write++)
                        {
                            for (int sort = 0; sort < tempList.Count - 1; sort++)
                            {
                                if (tempList[sort].Price < tempList[sort + 1].Price)
                                {
                                    tempView = tempList[sort];
                                    tempList[sort] = tempList[sort + 1];
                                    tempList[sort + 1] = tempView;
                                }
                            }
                        }
                        flpItems.Controls.AddRange(tempList.ToArray());
                        break;
                    }
                case 2:
                    {

                        break;
                    }
                default:
                    {

                        break;
                    }
            }
        }
    }
}
