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
                newItem.Active = item.OutOfStock?false:true;
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

                                    foreach(RestaurantMenuItemView item in flpItems.Controls)
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

        private void cbxEnabled_Click(object sender, EventArgs e)
        {
            ShowLoading(true);
            selectedItem.OutOfStock = cbxEnabled.Checked ? false : true;
            AsyncCallback<RestaurantMenuItem> updateObjectCallback = new AsyncCallback<RestaurantMenuItem>(
            savedRestaurantMenuItem =>
            {
                Invoke(new Action(() =>
                {
                    System.Console.WriteLine("Restaurant details has been updated");
                    populateMenu();
                    SortRefresh();
                    SelectItemHighLight(selectedItem.objectId);
                    ShowLoading(false);
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
                      ShowLoading(false);
                  }));
              }
            );

            Backendless.Persistence.Of<RestaurantMenuItem>().Save(selectedItem, saveObjectCallback);

        }
        private void ShowLoading(bool toggle)
        {
            if(toggle==true)
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
        private void btnDelete_Click(object sender, EventArgs e)
        {
            RestaurantMenuItem tempItem = selectedItem;
            DialogResult dialogResult = MessageBox.Show("Are you sure you would like to delete "+ tempItem.Name+"?", tempItem.Name, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ShowLoading(true);

                
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
                        ShowLoading(false);
                        lblStatus.Text=selectedItem.Name + " has been removed";
                    }));
                },
                error =>
                {
                    Invoke(new Action(() =>
                    {
                        ShowLoading(false);
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
                              ShowLoading(false);
                              lblStatus.Text = "Error: " + error.Message;
                          }));
                    }
                    );

                    Backendless.Persistence.Of<RestaurantMenuItem>().Save(tempItem, saveObjectCallback);
            }         
        }
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
                        populateMenu();
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
                        foreach(RestaurantMenuItemView view in tempList)
                        {
                            flpItems.Controls.Add(view);
                            view.BringToFront();
                        }
                        break;
                    }
                default:
                    {
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
    }
}
