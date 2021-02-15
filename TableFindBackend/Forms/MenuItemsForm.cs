using BackendlessAPI;
using BackendlessAPI.Async;
using System;
using System.Collections.Generic;
using System.Drawing;
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
                newItem.Active = item.OutOfStock ? false : true;
                newItem.Price = item.Price;

                newItem.Type = item.Type;
                if (newItem.Active != true)
                {
                    newItem.BackColor = Color.FromName("ControlDark");
                }
                newItem.Label = item.Name;

                flpItems.Controls.Add(newItem);
                newItem.lblName.MouseClick += new MouseEventHandler(MenuLabel_Click);
                newItem.MouseClick += new MouseEventHandler(MenuItem_Click);
            }
        }

        //this method will simulate the selection of an object by changing the color of the control the user selects
        public void SelectItemHighLight(String id)
        {
            foreach (RestaurantMenuItemView viewItem in flpItems.Controls)
            {
                if ((String)viewItem.Tag == id)
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
                        if (viewItem.Label == item.Name)
                        {
                            selectedItem = item;
                        }
                    }
                    cbxEnabled.Checked = selectedItem.OutOfStock ? false : true;
                    pnlEdit.Enabled = true;

                }
            }
        }
        //Method for when the user attempts to click on the control but selects the label ON the control
        private void MenuLabel_Click(object sender, MouseEventArgs e)
        {            
            Label tempLabel = (Label)sender;
            RestaurantMenuItemView temp = (RestaurantMenuItemView)tempLabel.Parent;
            MenuItem_Click(temp, e);
        }

        //Method for when the user selects the control (MenuItemView)
        private void MenuItem_Click(object sender, MouseEventArgs e)
        {
            RestaurantMenuItemView tempItem = (RestaurantMenuItemView)sender;
            string id = tempItem.Tag.ToString();
            SelectItemHighLight(id);
        }

        //method for when the user selected a control and wishes to edit its properties. sending a valid MenuItem to the method indicates that it should be edited
        private void btnEdit_Click(object sender, EventArgs e)
        {
            AddOrEdit(selectedItem);
        }

        //this method determines if the user wishes to add or edit a MenuItem to the restaurant
        private void AddOrEdit(RestaurantMenuItem temp) // Controller for RestaurantMenuItem
        {
            AddEditMenuItem newForm = new AddEditMenuItem(temp);
            if (newForm.ShowDialog() == DialogResult.OK)
            {
                ShowLoading(true);
                AsyncCallback<RestaurantMenuItem> callback = new AsyncCallback<RestaurantMenuItem>(
                        result =>
                        {
                            Invoke(new Action(() =>
                            {
                                if (temp != null)//means an existing item was edited
                                {
                                    OwnerStorage.MenuItems.Insert(OwnerStorage.MenuItems.IndexOf(temp), newForm.transferedItem);
                                    OwnerStorage.MenuItems.Remove(temp);

                                    foreach (RestaurantMenuItemView item in flpItems.Controls)
                                    {
                                        if (item.Tag.Equals(temp.objectId))
                                        {
                                            item.Name = newForm.transferedItem.Name;
                                            item.Type = newForm.transferedItem.Type;
                                            item.Price = newForm.transferedItem.Price;
                                        }
                                    }
                                    populateMenu();
                                    SortRefresh();
                                    SelectItemHighLight(selectedItem.objectId);
                                }
                                else//means a new Item is created
                                {
                                    RestaurantMenuItemView newView = new RestaurantMenuItemView();
                                    newView.Label = result.Name;
                                    newView.Active = true;
                                    newView.Type = result.Type;
                                    newView.Tag = result.objectId;
                                    newView.Price = result.Price;

                                    OwnerStorage.MenuItems.Add(result);

                                    newView.lblName.MouseClick += new MouseEventHandler(MenuLabel_Click);
                                    newView.MouseClick += new MouseEventHandler(MenuItem_Click);
                                    flpItems.Controls.Add(newView);
                                }
                                ShowLoading(false);
                            }));

                        },
                        fault =>
                        {
                            Invoke(new Action(() =>
                            {
                                MessageBox.Show(this, "Error: " + fault.Message);
                                ShowLoading(false);
                            }));
                        });

                //runs the save callback. it automatically updates objects, so no need to create multiple callbacks
                Backendless.Data.Of<RestaurantMenuItem>().Save(newForm.transferedItem, callback);
            }

        }

        //method is for when the user wishes to add a new empty MenuItem to the restaurant. sending a null value will indicate that a new item is to be created
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddOrEdit(null);
        }

        //Button used to close the form if the user wishes to not save changes made
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //Button used to close the form if the user wishes to not save changes made
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //this event handler will automatically update the selected menuItem by simply changing the OutOfStock property
        private void cbxEnabled_Click(object sender, EventArgs e)
        {
            ShowLoading(true);
            
            AsyncCallback<RestaurantMenuItem> updateObjectCallback = new AsyncCallback<RestaurantMenuItem>(
            savedRestaurantMenuItem =>
            {
                //success. Now the page gets refreshed so that the color of the item may change.
                Invoke(new Action(() =>
                {
                    OwnerStorage.LogInfo.Add("User toggled MenuItem: "+savedRestaurantMenuItem.Name);
                    OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                    populateMenu();//<--repopulates the items
                    SortRefresh();//<--sorts it to what it was
                    SelectItemHighLight(selectedItem.objectId);//<--highlights the Item which the user was currently busy with
                    ShowLoading(false);
                }));
            },
            error =>
            {
                //something went wrong. An error message will now display
                Invoke(new Action(() =>
                {
                    MessageBox.Show(this, "Error: " + error.Message);
                    ShowLoading(false);
                }));
            });

            AsyncCallback<RestaurantMenuItem> saveObjectCallback = new AsyncCallback<RestaurantMenuItem>(
              savedRestaurantMenuItem =>
              {
                  //the Item has been saved, now it can be updated
                  Backendless.Persistence.Of<RestaurantMenuItem>().Save(savedRestaurantMenuItem, updateObjectCallback);
              },
              error =>
              {
                  //something went wrong, an error message will now display
                  Invoke(new Action(() =>
                  {
                      MessageBox.Show(this, "Error: " + error.Message);
                      ShowLoading(false);
                  }));
              }
            );

            //changes the single property which effectively deactivates the menuItem
            selectedItem.OutOfStock = cbxEnabled.Checked ? false : true;
            Backendless.Persistence.Of<RestaurantMenuItem>().Save(selectedItem, saveObjectCallback);

        }
        //A method that will appear on all forms. It simulates a loading screen by showing and hiding all necessary buttons and interface elements
        private void ShowLoading(bool toggle)
        {
            if (toggle == true)
            {
                pbxLoading.Visible = true;
                btnClose.Enabled = false;
                btnExit.Enabled = false;
            }
            else
            {
                pbxLoading.Visible = false;
                btnClose.Enabled = true;
                btnExit.Enabled = true;
            }
        }

        //Method for deleting the MenuItem.
        private void btnDelete_Click(object sender, EventArgs e)
        {
            RestaurantMenuItem tempItem = selectedItem;
            DialogResult dialogResult = MessageBox.Show("Are you sure you would like to delete " + tempItem.Name + "?", tempItem.Name, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ShowLoading(true);
                AsyncCallback<long> deleteObjectCallback = new AsyncCallback<long>(
                deletionTime =>
                {
                    //success. The object has been removed successfully
                    Invoke(new Action(() =>
                    {
                        foreach (RestaurantMenuItemView view in flpItems.Controls)
                        {
                            if (view.Tag.ToString() == tempItem.objectId)
                            {
                                flpItems.Controls.Remove(view);//the MenuItemView must be removed from the FlowLayoutPanel

                            }
                        }
                        OwnerStorage.MenuItems.Remove(tempItem);
                        populateMenu();
                        pnlEdit.Enabled = false;
                        ShowLoading(false);
                        lblStatus.Text = selectedItem.Name + " has been removed";
                    }));
                },
                error =>
                {
                    //something went wrong, an error message will be displayed
                    Invoke(new Action(() =>
                    {
                        ShowLoading(false);
                        lblStatus.Text = "Error: " + error.Message;
                    }));
                });

                AsyncCallback<RestaurantMenuItem> saveObjectCallback = new AsyncCallback<RestaurantMenuItem>(
                savedMenuItem =>
                {
                    //the object has been saved, now it can be removed
                    Backendless.Persistence.Of<RestaurantMenuItem>().Remove(savedMenuItem, deleteObjectCallback);
                },
                error =>
                {
                    //something went wrong, an error message will be displayed
                    Invoke(new Action(() =>
                      {
                          ShowLoading(false);
                          lblStatus.Text = "Error: " + error.Message;
                      }));
                }
                );

                //Backendless demands that an object has to be saved first before it gets deleted
                Backendless.Persistence.Of<RestaurantMenuItem>().Save(tempItem, saveObjectCallback);
            }
        }

        //Event handler which checks which option changes has occured
        private void clbSortOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clbSortOptions.SelectedIndex == 3)
            {
                cbxType.Visible = true;
            }
            else
            {
                cbxType.Visible = false;
            }
            if (clbSortOptions.SelectedIndex != -1)
                clbSortOptions.SetItemChecked(clbSortOptions.SelectedIndex, true);

        }

        //Event handler which checks if the user selected an option in the CheckListBox
        private void clbSortOptions_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int ix = 0; ix < clbSortOptions.Items.Count; ++ix)
                if (ix != e.Index)
                    clbSortOptions.SetItemChecked(ix, false);
        }

        //The sortation button. It runs the SortRefresh method, otherwise the code would have been copied
        private void btnSort_Click(object sender, EventArgs e)
        {
            SortRefresh();
        }

        //this method will sort the information on the form according to the setting the user selected
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
                case -1: //default sortation
                    {
                        populateMenu();
                        break;
                    }
                case 0://price: low to High
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
                case 1://price: High to Low
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
                case 2://recently added
                    {
                        foreach (RestaurantMenuItemView view in tempList)
                        {
                            flpItems.Controls.Add(view);
                            view.BringToFront();
                        }
                        break;
                    }
                default://sort by meal type
                    {
                        //note, this will only arange the selected meal types to the first position. The reset of the Items will remain unsorted as is.
                        RestaurantMenuItemView tempView;
                        for (int write = 0; write < tempList.Count; write++)
                        {
                            for (int sort = 0; sort < tempList.Count - 1; sort++)
                            {
                                if (cbxType.Text == tempList[sort + 1].Type)
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
            }
        }

        //Blocks the "alt F4" capability so that the user cannot close the program while a process is running
        private void MenuItemsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == System.Windows.Forms.CloseReason.UserClosing && pbxLoading.Visible == true)
            {
                e.Cancel = true;
            }
        }

        //This method simply refreshes the form's elements and resets the sorting settings
        private void Reload_Click(object sender, EventArgs e)
        {
            pnlEdit.Enabled = false;
            populateMenu();
            clbSortOptions.ClearSelected();
            while (clbSortOptions.CheckedIndices.Count > 0)
            {
                clbSortOptions.SetItemChecked(clbSortOptions.CheckedIndices[0], false);
            }
        }
    }
}
