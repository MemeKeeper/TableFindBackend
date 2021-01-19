﻿using BackendlessAPI;
using BackendlessAPI.Async;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TableFindBackend.Models;
using TableFindBackend.Global_Variables;
using SharpUpdate;
using System.Reflection;
using System.IO;
using TableFindBackend.Logging;
using TableFindBackend.RT_Database_Listeneres;
using System.Threading;
using TableFindBackend.ViewModels;


namespace TableFindBackend.Forms
{
    public partial class MainForm : Form
    {

        private Control activeControl;
        private Point previousPosition;
        private SharpUpdater updater;
        private ReservationCreatedEventListener createdListener;
        private ReservationDeletedEventListener deletedListener;
        private static System.Timers.Timer removeTimer;

        public MainForm()
        {
            OwnerStorage.Log = new List<String>();
            string APPLICATION_ID = "3341DD88-C207-2A48-FF9F-D4103CEA4900";
            string API_KEY = "3A3E7B64-7786-49D1-9D5B-AFC31D98CE13";
            Backendless.InitApp(APPLICATION_ID, API_KEY);

            OwnerStorage.FileWriter = new TextFileWriter();

            SignInForm signIn = new SignInForm();
            DialogResult result = signIn.ShowDialog();
            if (result != DialogResult.OK)
            {
                System.Environment.Exit(1); //completely closes the application
                OwnerStorage.FileWriter.WriteLineToFile("User Failed to Login", false);
                OwnerStorage.FileWriter.FormShutDown();
                
            }

            OwnerStorage.AdminMode = false;
            OwnerStorage.RestaurantTables = new List<RestaurantTable>();
            OwnerStorage.MenuItems = new List<RestaurantMenuItem>();
            OwnerStorage.AllReservations = new List<Reservation>();
            OwnerStorage.AllUsers = new List<BackendlessUser>();

            

            this.Hide();
            InitializeComponent();
            CheckPin();
            PopulateTables();
            CheckLayoutImage();
            UpdateCapacityLabel();

            PerformBackgroundMenuItemPopulation();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            OwnerStorage.Log.Add("User Logged in with valid Login at "+System.DateTime.Now.ToString("dd/MM,   HH:mm"));


            updater = new SharpUpdater(Assembly.GetExecutingAssembly(), this, new Uri("http://bernardkleinhans.co.za/Johan/update.xml"));  //<------ Domain on which we host the update for the program

            typeof(Panel).InvokeMember("DoubleBuffered",BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, pnlMain, new object[] { true });

        }
        private void UpdateCapacityLabel()
        {
            switch (OwnerStorage.ThisRestaurant.maxCapacity)
            {
                case 0:
                    {
                        lblStatus.ForeColor = Color.FromName("Green");
                        lblStatus.Text = "Not Busy";
                        break;
                    }
                case 1:
                    {
                        lblStatus.ForeColor = Color.FromName("Orange");
                        lblStatus.Text = "Medium Load";
                        break;
                    }
                default:
                    {
                        lblStatus.ForeColor = Color.FromName("Red");
                        lblStatus.Text = "Very Busy";
                        break;
                    }
            }
        }

        private void InitializeTimer()
        {
            removeTimer = new System.Timers.Timer();
            removeTimer.Interval = 30000;
            removeTimer.Elapsed += OnTimedEvent;
            removeTimer.AutoReset = true;
            removeTimer.Enabled = true;
        }
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {

            foreach (Reservation r in OwnerStorage.AllReservations)
            {
                if (r.takenTo < System.DateTime.Now)
                {
                    AsyncCallback<Reservation> saveObjectCallback = new AsyncCallback<Reservation>(
                    savedReservation =>
                    {
                        TextFileWriter rtTextFileWriter = new TextFileWriter();

                        AsyncCallback<long> deleteObjectCallback = new AsyncCallback<long>(
                        deletionTime =>
                        {
                            OwnerStorage.Log.Add("Reservation has Expired    : " + System.DateTime.Now.ToString("HH:mm"));
                            OwnerStorage.Log.Add("Name:  " + savedReservation.name);
                        },
                        error =>
                        {
                        });
                        Backendless.Persistence.Of<Reservation>().Remove(savedReservation, deleteObjectCallback);
                    },
              error =>
              {
              }
            );

                    Backendless.Persistence.Of<Reservation>().Save(r, saveObjectCallback);
                }
            }
        }

        private void CheckLayoutImage()
        {
            string file = @"layouts\" + OwnerStorage.ThisRestaurant.name + "_" + OwnerStorage.ThisRestaurant.locationString + "_layout.tbl";
            if (File.Exists(file) == true)
            {

                pnlMain.BackgroundImage = Image.FromFile(file);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            else
            {
                pnlMain.BackgroundImage = null;
            }
        }
        public void DisableLayoutImage()
        {
            if (pnlMain.BackgroundImage!=null)
            pnlMain.BackgroundImage.Dispose();
        }
        private string CalcPanelRes()
        {
            string result;
            result = "Recommended floor layout image size: " + pnlMain.Bounds.Width.ToString() + " x " + pnlMain.Bounds.Height.ToString();

            return result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void ShowLoading(bool enable)
        {
            if (enable == true)
            {
                pboxLoading.Visible = true;
            }
            else
                pboxLoading.Visible = false;
        }
        public void RemoveOneReservationView(Reservation r)
        {
            ReservationView tempReservation = null;
            foreach (ReservationView view in flpItems.Controls)
            {

                if (view.Tag.ToString() == r.objectId.ToString())
                {
                    tempReservation = view;

                }
            }
            Invoke(new Action(() =>
            {
                
                tempReservation.pnlContact.MouseClick += new MouseEventHandler(reservation_Click);
                tempReservation.pnlReservation.MouseClick += new MouseEventHandler(reservation_Click);
                flpPrevious.Controls.Add(tempReservation);
                tempReservation.Removed();

                flpPrevious.Controls.SetChildIndex(tempReservation, 0);
                flpItems.Controls.Remove(tempReservation);
            }));
        }    
        public void AddOneReservationView(Reservation r)
        {
            ReservationView reservation = new ReservationView();
            RestaurantTable table = new RestaurantTable();
            foreach (RestaurantTable t in OwnerStorage.RestaurantTables)
            {
                if (t.objectId == r.tableId)
                    table = t;
            }
            reservation.Tag = r.objectId;

            reservation.UserName = r.name;
            reservation.UObjectId = r.userId;
            reservation.UserContactNumber = r.number;
            reservation.TableName = "Table: " + table.name;
            reservation.ObjectId = r.objectId;
            reservation.Date = r.takenFrom.ToString("ddd, dd / MM");
            reservation.FromToTime = r.takenFrom.ToString("HH:mm") + " to " + r.takenTo.ToString("HH:mm");

            reservation.pnlContact.MouseClick += new MouseEventHandler(reservation_Click);
            reservation.pnlReservation.MouseClick += new MouseEventHandler(reservation_Click);
            Invoke(new Action(() =>
            {
                flpItems.Controls.Add(reservation);
                flpItems.Controls.SetChildIndex(reservation, 0);
                reservation.New();
            }));

            
        }
        private void PerformReservationViewListPopulation()
        {

            flpItems.Controls.Clear();
            foreach (Reservation r in OwnerStorage.AllReservations)
            {
                ReservationView reservation = new ReservationView();
                RestaurantTable table = new RestaurantTable();
                foreach(RestaurantTable t in OwnerStorage.RestaurantTables)
                {
                    if (t.objectId == r.tableId)
                        table = t;
                }
                reservation.Tag = r.objectId;

                reservation.UserName = r.name;
                reservation.UObjectId = r.userId;
                reservation.UserContactNumber = r.number;
                reservation.TableName ="Table: "+ table.name;
                reservation.ObjectId = r.objectId;
                reservation.Date = r.takenFrom.ToString("ddd, dd / MM");
                reservation.FromToTime = r.takenFrom.ToString("HH:mm") + " to " + r.takenTo.ToString("HH:mm");

                reservation.pnlContact.MouseClick += new MouseEventHandler(reservation_Click);
                reservation.pnlReservation.MouseClick += new MouseEventHandler(reservation_Click);

                flpItems.Controls.Add(reservation);
            }
        }

        private void reservation_Click(object sender, MouseEventArgs e)
        {

            Panel templabel = (Panel)sender;
            ReservationView tempReservationView = (ReservationView)templabel.Parent;
            foreach(ReservationView views in flpItems.Controls)
            {
                    if (views.Tag == tempReservationView.Tag)
                    {

                            views.Selected();
                            BackendlessUser tempUser = null;
                            foreach (BackendlessUser user in OwnerStorage.AllUsers)
                            {
                                if (user.ObjectId == views.UObjectId)
                                {
                                    tempUser = user;
                                }
                            }
                        Reservation tempReservation = null;
                        foreach (Reservation reservation in OwnerStorage.AllReservations)
                        {
                            if (reservation.objectId == views.Tag.ToString())
                            {
                                tempReservation = reservation;
                            }
                        }
                        RestaurantTable tempTable = null;
                        foreach (RestaurantTable table in OwnerStorage.RestaurantTables)
                        {
                            if (table.objectId == tempReservation.tableId)
                            {
                                tempTable = table;
                            }
                        }
                        ReservationDetailsForm details = new ReservationDetailsForm(tempReservation,tempUser,tempTable);
                            details.ShowDialog();
                            views.Deselected();                             
                    }
            }
        }

        private void PerformBackgroundMenuItemPopulation()
        {
            String whereClause = "restaurantID = '" + OwnerStorage.ThisRestaurant.objectId + "'";
            BackendlessAPI.Persistence.DataQueryBuilder queryBuilder = BackendlessAPI.Persistence.DataQueryBuilder.Create();
            queryBuilder.SetWhereClause(whereClause);

            AsyncCallback<IList<RestaurantMenuItem>> getBookingCallback = new AsyncCallback<IList<RestaurantMenuItem>>(

            foundRestaurantMenuItem =>
            {
                OwnerStorage.MenuItems = (List<RestaurantMenuItem>)foundRestaurantMenuItem;
                OwnerStorage.FileWriter.WriteLineToFile("Online interactive menu has been downloaded", true);
            },
            error =>
            {
                Invoke(new Action(() =>
                {
                    MessageBox.Show(this, "Error: " + error.Message);
                    OwnerStorage.FileWriter.WriteLineToFile("Online interactive menu failed to download", true);
                }));
            });

            Backendless.Data.Of<RestaurantMenuItem>().Find(queryBuilder, getBookingCallback);       
        }

        private void PerformBackgroundReservationPopulation()
        {
            Invoke(new Action(() =>
            {
                ShowLoading(true);
            }));
            OwnerStorage.AllReservations.Clear();
            OwnerStorage.AllUsers.Clear();
            String whereClause = "restaurantId = '" + OwnerStorage.ThisRestaurant.objectId + "'";
            BackendlessAPI.Persistence.DataQueryBuilder queryBuilder = BackendlessAPI.Persistence.DataQueryBuilder.Create();
            queryBuilder.SetWhereClause(whereClause);
            queryBuilder.SetPageSize(100);

            AsyncCallback<IList<Reservation>> getContactsCallback = new AsyncCallback<IList<Reservation>>(
            foundReservations =>
            {
                InitializeTimer();
                OwnerStorage.AllReservations = (List<Reservation>)foundReservations;
                if (foundReservations.Count != 0)
                {
                    int i = 1;
                    foreach (Reservation r in foundReservations)
                    {
                        AsyncCallback<BackendlessUser> loadContactCallback = new AsyncCallback<BackendlessUser>(
                        foundContact =>
                        {
                            
                            OwnerStorage.AllUsers.Add(foundContact);
                            if (i == foundReservations.Count)
                                Invoke(new Action(() =>
                                {
                                    ShowLoading(false);
                                    OwnerStorage.FileWriter.WriteLineToFile("All Reservations has been downloaded", true);
                                    btnViewAll.Enabled = true;
                                    PerformReservationViewListPopulation();
                                }));
                            else
                                i++;
                        },
                        error =>
                        {

                        });

                        Backendless.Data.Of<BackendlessUser>().FindById(r.userId, loadContactCallback);
                    }
                }
                else
                {
                    Invoke(new Action(() =>
                    {
                        ShowLoading(false);
                        OwnerStorage.FileWriter.WriteLineToFile("No Reservations to download", true);
                        btnViewAll.Enabled = true;
                    }));
                }                
            },
            error =>
            {
                System.Console.WriteLine("Server returned an error " + error.Message);
            });

            Backendless.Data.Of<Reservation>().Find(queryBuilder,getContactsCallback);
        }
        private void PopulateTables()
        {
            btnViewAll.Enabled = false;
            OwnerStorage.RestaurantTables.Clear();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Clear();
            ShowLoading(true);

            String whereClause = "restaurantId = '" + OwnerStorage.ThisRestaurant.objectId+"'";
            BackendlessAPI.Persistence.DataQueryBuilder queryBuilder = BackendlessAPI.Persistence.DataQueryBuilder.Create();
            queryBuilder.SetWhereClause(whereClause);
            queryBuilder.SetPageSize(100);

            AsyncCallback<IList<RestaurantTable>> getTableItemCallback = new AsyncCallback<IList<RestaurantTable>>(

                      foundTableItem =>
                      {
                          OwnerStorage.RestaurantTables = (List<RestaurantTable>)foundTableItem;
                          createdListener = new ReservationCreatedEventListener(this);
                          deletedListener = new ReservationDeletedEventListener(this);                          
                          PerformBackgroundReservationPopulation();
                          int i = 0;
                          Invoke (new Action(() =>
                              {
                                  lblPnlSize.Text = CalcPanelRes();
                              }));

                          foreach (RestaurantTable tb in OwnerStorage.RestaurantTables)
                          {

                              Invoke(new Action(() =>
                              {                        
                                  RestaurantTableView newItem = new RestaurantTableView();
                                  
                                  newItem.Tag = tb.objectId;
                                  newItem.Label = tb.name;
                                  newItem.Seating = tb.capacity;
                                  newItem.Availability = tb.available;

                                  pnlMain.Controls.Add(newItem);
                                  if (tb.yPos>pnlMain.Height ||tb.xPos>pnlMain.Width)
                                  {
                                      pnlMain.Controls[i].Location = new Point(pnlMain.Width-73, pnlMain.Height - 38);
                                  }
                                  else
                                      pnlMain.Controls[i].Location = new Point(tb.xPos, tb.yPos);

                                  this.AddControl(newItem);
                                  //newItem.lblName.MouseDoubleClick += new MouseEventHandler(MyControlLabel_DoubleClick);
                                  //newItem.MouseDown += new MouseEventHandler(MyControl_MouseDown);
                                  //newItem.MouseMove += new MouseEventHandler(MyControl_MouseMove);
                                  //newItem.MouseUp += new MouseEventHandler(MyControl_MouseUp);

                                  if (OwnerStorage.AdminMode == true)
                                      newItem.Removable = true;
                              }));
                              i++;                              
                          }
                          //Invoke(new Action(() =>
                          //{
                          //    OwnerStorage.FileWriter.WriteLineToFile("Tables has been downloaded", true);
                          //    ShowLoading(false);
                          //}));
                      },
                      error =>
                      {
                          Invoke(new Action(() =>
                          {
                              MessageBox.Show(this, "Error: " + error.Message);
                              OwnerStorage.FileWriter.WriteLineToFile("Tables downloading failed", true);
                              ShowLoading(false);
                          }));
                      });

                  Backendless.Data.Of<RestaurantTable>().Find(queryBuilder,getTableItemCallback);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            RestaurantTable newTable = new RestaurantTable();

            newTable.name = "Tbl #"+Convert.ToString(OwnerStorage.RestaurantTables.Count+1);
            newTable.capacity = 1; //<-- min value
            newTable.restaurantId = OwnerStorage.ThisRestaurant.objectId;

            OwnerStorage.RestaurantTables.Add(newTable);

            RestaurantTableView newView = new RestaurantTableView();

            newView.Location = new Point(0, 0);
            newView.Seating = newTable.capacity;
            newView.Label = newTable.name;
            newView.Availability = newTable.available;

            AddControl(newView);
            MyControl_DoubleClick(newView, e);

            OwnerStorage.FileWriter.WriteLineToFile("User created a new Table.", true);
            OwnerStorage.FileWriter.WriteLineToFile("Name:  " + newTable.name, false);
            OwnerStorage.Log.Add("User added a new Restaurant Table    : " + System.DateTime.Now.ToString("HH:mm"));
            OwnerStorage.Log.Add("Name:  " + newTable.name);


        }
        private void AddControl(RestaurantTableView item)
        {
            pnlMain.Controls.Add(item);
            item.lblName.MouseDoubleClick+= new MouseEventHandler(MyControl_DoubleClick);
            item.lblName.MouseDown += new MouseEventHandler(MyControl_MouseDown);
            item.lblName.MouseMove += new MouseEventHandler(MyControl_MouseMove);
            item.lblName.MouseUp += new MouseEventHandler(MyControl_MouseUp);

            item.MouseDoubleClick += new MouseEventHandler(MyControl_DoubleClick);
            item.MouseDown += new MouseEventHandler(MyControl_MouseDown);
            item.MouseMove += new MouseEventHandler(MyControl_MouseMove);
            item.MouseUp += new MouseEventHandler(MyControl_MouseUp);
        }
      
        private void MyControl_DoubleClick(object sender, EventArgs e)
        {
            RestaurantTableView tempItem;
            if (sender is Label)
            {
                Label tempLabel = (Label)sender;
                tempItem = (RestaurantTableView)tempLabel.Parent;
            }
            else
            {
                tempItem = (RestaurantTableView)sender;
            }

                
                RestaurantTable tempTable = null;                
                OwnerStorage.FileWriter.WriteLineToFile("Editing a Table", true);
                OwnerStorage.FileWriter.WriteLineToFile("Name:  " + tempItem.Label, false);

                foreach (RestaurantTable ti in OwnerStorage.RestaurantTables)
                {
                    if (ti.objectId == tempItem.Tag)
                    {
                        tempTable = ti;
                    }

                }
            if (OwnerStorage.AdminMode == true)
            {
                EditTableForm editor = new EditTableForm(tempTable);
                DialogResult result = editor.ShowDialog();

                if (result == DialogResult.Yes) //Removes the Table
                {
                    OwnerStorage.RestaurantTables.Remove(tempTable);
                    pnlMain.Controls.Remove((RestaurantTableView)sender);
                }
                if (result == DialogResult.OK) //Updates the Table
                {
                    OwnerStorage.RestaurantTables.Remove(tempTable);

                    if (sender is Label)
                    {
                        Label tempLabel = (Label)sender;
                        RestaurantTableView tempSenderView = (RestaurantTableView)tempLabel.Parent;
                        pnlMain.Controls.Remove(tempSenderView);
                    }
                    else
                    {
                        pnlMain.Controls.Remove((RestaurantTableView)sender);
                    }

                    RestaurantTableView tempView = new RestaurantTableView();

                    tempView.Tag = OwnerStorage.TempTable.objectId;
                    tempView.Label = OwnerStorage.TempTable.name;
                    tempView.Seating = OwnerStorage.TempTable.capacity;
                    tempView.Availability = OwnerStorage.TempTable.available;

                    pnlMain.Controls.Add(tempView);
                    pnlMain.Controls[pnlMain.Controls.Count - 1].Location = new Point(OwnerStorage.TempTable.xPos, OwnerStorage.TempTable.yPos);

                    OwnerStorage.RestaurantTables.Add(OwnerStorage.TempTable);

                    AddControl(tempView);

                    OwnerStorage.FileWriter.WriteLineToFile("User Updated Table", true);
                    OwnerStorage.FileWriter.WriteLineToFile("Name:  " + tempView.Label, false);
                }
            }
            else
            {
                ReservationsForm reservationsForm = new ReservationsForm(tempTable);
                reservationsForm.ShowDialog();
            }
        }
        private void MyControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (OwnerStorage.AdminMode == true)
            {
                activeControl = null;
                Cursor = Cursors.Default;
            }
        }

        private void MyControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (OwnerStorage.AdminMode == true)
            {
                if (sender is Label)
                {
                    Label tempLabel = (Label)sender;
                    RestaurantTableView tempView = (RestaurantTableView)tempLabel.Parent;
                    if (activeControl == null || activeControl != tempView)
                    {
                        return;
                    }
                    var lbllocation = activeControl.Location;

                    lbllocation.Offset(e.Location.X - previousPosition.X, e.Location.Y - previousPosition.Y);
                    activeControl.Location = lbllocation;
                }
                else
                {
                    if (activeControl == null || activeControl != sender)
                    {
                        return;
                    }
                    var location = activeControl.Location;

                    location.Offset(e.Location.X - previousPosition.X, e.Location.Y - previousPosition.Y);
                    activeControl.Location = location;
                }
            }
        }

        private void MyControl_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left && e.Clicks >= 2)
            {
                MyControl_DoubleClick(sender, e);
                return;
            }


            if (OwnerStorage.AdminMode == true)
            {
                if (sender is Label)
                {
                    Label tempLabel = (Label)sender;
                    RestaurantTableView tempView = (RestaurantTableView)tempLabel.Parent;
                    activeControl = tempView as Control;
                }
                else
                {
                    activeControl = sender as Control;
                }
                previousPosition = e.Location;
                Cursor = Cursors.Hand;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            OwnerStorage.FileWriter.FormShutDown();
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ShowLoading(true);
            int i = 0;
            foreach (RestaurantTable tb in OwnerStorage.RestaurantTables)
            {
                tb.yPos = pnlMain.Controls[i].Location.Y;
                tb.xPos = pnlMain.Controls[i].Location.X;
                i++;
            }

                AsyncCallback<int> bulkDeleteCallback = new AsyncCallback<int>(
            objectsDeleted =>
            {
                AsyncCallback<IList<string>> bulkCreateCallback = new AsyncCallback<IList<string>>(
                result =>
                {
                    Invoke(new Action(() =>
                    {
                        OwnerStorage.FileWriter.WriteLineToFile("User Finilized changes to restaurant", true);
                        OwnerStorage.FileWriter.WriteLineToFile("User deactivated Admin Mode", true);

                        OwnerStorage.Log.Add("User made changes to restaurant floor plan    : " + System.DateTime.Now.ToString("HH:mm"));
                        OwnerStorage.Log.Add("User deactivated Admin mode");
                        ShowLoading(false);
                        PopulateTables();
                    }));

                    toggleAdminMode(false);
                },
                error =>                                                                          //Save as a list
                {
                    Invoke(new Action(() =>
                    {
                        OwnerStorage.FileWriter.WriteLineToFile("Changes were not saved", true);
                        OwnerStorage.FileWriter.WriteLineToFile("Error: " + error.Message, false);
                        OwnerStorage.Log.Add("Error: " + error.Message);
                        ShowLoading(false);
                        MessageBox.Show(this, "Error: " + error.Message);
                    }));
                });

                Backendless.Data.Of<RestaurantTable>().Create(OwnerStorage.RestaurantTables, bulkCreateCallback);
            },
            error =>
            {
                Invoke(new Action(() =>
                {
                    OwnerStorage.FileWriter.WriteLineToFile("Changes were not saved", true);
                    OwnerStorage.FileWriter.WriteLineToFile("Error: " + error.Message, false);
                    OwnerStorage.Log.Add("Error: " + error.Message);
                    ShowLoading(false);
                    MessageBox.Show(this, "Error: " + error.Message);

                }));
            });

            Backendless.Data.Of<RestaurantTable>().Remove("restaurantId = '" + OwnerStorage.ThisRestaurant.objectId+"'", bulkDeleteCallback);
            
        }

        private void btnReloadAll_Click(object sender, EventArgs e)
        {
            PopulateTables();
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }
        private void CheckPin()
        {
            String line;
            try
            {
                if (File.Exists("TableFindman") == true)
                {
                    StreamReader sr = new StreamReader("TableFindMan");         //Still Very basic, will add encryption
                    line = sr.ReadLine();   //<-- Dummy line
                    line = sr.ReadLine();   //<-- Pin Number
                    OwnerStorage.ManagerPin = line;
                    sr.Close();
                }
                else
                {
                    DialogResult result = MessageBox.Show(this, "Seems like this is your first time logging in to the " +
                        "TableFindBackend Desktop App on this computer, therefore " +
                        "you have not yet registered a Manager PIN. Without it " +
                        "you will not be able to have access to admin capabilities." +
                        "\nWould you like to configure a Manager PIN now?", "Manager PIN", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        ChangePinForm NewPin = new ChangePinForm();
                        DialogResult pinResult = NewPin.ShowDialog();
                        if(pinResult ==DialogResult.OK)
                        {
                            CheckPin();
                        }
                    }
                    else if (result == DialogResult.No)
                    {
                        OwnerStorage.FileWriter.WriteLineToFile("User Denied First-time Manager PIN Setup", true);
                    }
                }
            }
            catch (Exception e)
            {
                OwnerStorage.FileWriter.WriteLineToFile("File could not be written.", true);
                OwnerStorage.FileWriter.WriteLineToFile("Error: "+e.Message.ToString(), false);

                MessageBox.Show(this, "Error: " + e.Message);
            }
        }
        private void btnAdmin_Click(object sender, EventArgs e)
        {
            if (OwnerStorage.ManagerPin != null)
            {
                if (tbxPass.Text.Equals(OwnerStorage.ManagerPin))
                {
                    toggleAdminMode(true);

                    OwnerStorage.FileWriter.WriteLineToFile("User Toggled Admin Mode", true);
                    OwnerStorage.Log.Add("User activated elevated privileges    : " + System.DateTime.Now.ToString("HH:mm"));

                }
                tbxPass.Text = "";
                tbxPass.Focus();
            }
            else
            {
                CheckPin();
                
            }
        }

        private void toggleAdminMode(bool toggle)
        {
            if (toggle == true)
            {
                btnApply.Visible = true;
                btnCreate.Visible = true;
                btnUpdate.Visible = true;
                btnReloadAll.Visible = true;
                btnLogout.Visible = true;
                btnEditRestaurant.Visible = true;
                tbxPass.Visible = false;
                lblLogin.Visible = false;
                btnEditor.Visible = false;
                btnChangePin.Visible = false;
                btnEditMenu.Visible = true;
                OwnerStorage.AdminMode = true;

                foreach(RestaurantTableView v in pnlMain.Controls)
                {
                    v.Removable = true;
                }
            }
            else
            {
                Invoke(new Action(() =>
                {
                    btnApply.Visible = false;
                    btnCreate.Visible = false;                    
                    btnUpdate.Visible = false;
                    btnLogout.Visible = false;
                    btnEditRestaurant.Visible = false;
                    btnReloadAll.Visible = false;
                    tbxPass.Visible = true;
                    lblLogin.Visible = true;
                    btnEditor.Visible = true;
                    btnChangePin.Visible = true;
                    btnEditMenu.Visible = false;
                    OwnerStorage.AdminMode = false;

                    foreach (RestaurantTableView v in pnlMain.Controls)
                    {
                        v.Removable = false;
                    }
                }));
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            OwnerStorage.FileWriter.WriteLineToFile("User Checked for Updates", true);
            updater.DoUpdate();
        }

        private void btnEditRestaurant_Click(object sender, EventArgs e)
        {
            EditRestaurantForm editForm = new EditRestaurantForm(this);
            DialogResult result =editForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                OwnerStorage.FileWriter.WriteLineToFile("User Edited Restaurant Details", true);
                OwnerStorage.Log.Add("User Edited Restaurant Details    : " + System.DateTime.Now.ToString("HH:mm"));
            }           

            CheckLayoutImage();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult logout = MessageBox.Show("Are you sure you would like to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (logout == DialogResult.Yes)
            {
                AsyncCallback<Object> logoutCallback = new AsyncCallback<Object>(
                user =>
                {
                Invoke(new Action(() =>
                {
                    OwnerStorage.FileWriter.WriteLineToFile("User Logged out", false);
                    OwnerStorage.FileWriter.FormShutDown();
                    Properties.Settings.Default.defaultRestaurant = -1;
                    Properties.Settings.Default.Save();
                }));

                    Application.Restart();
                    Environment.Exit(0);
                },
                fault =>
                {
                    System.Console.WriteLine(fault.ToString());
                });

                Backendless.UserService.Logout(logoutCallback);
            }
        }

        private void btnEditMenu_Click(object sender, EventArgs e)
        {

            
            MenuItemsForm editer = new MenuItemsForm();
            DialogResult result=editer.ShowDialog();
            if (result==DialogResult.OK)
            {
                OwnerStorage.FileWriter.WriteLineToFile("User made changes to Restaurant Menu", true);
                OwnerStorage.Log.Add("User made changes to Restaurant Menu    : " + System.DateTime.Now.ToString("HH:mm"));
            }
            
        }

        private void btnChangePin_Click(object sender, EventArgs e)
        {

            ChangePinForm NewPin = new ChangePinForm();
            DialogResult pinResult = NewPin.ShowDialog();
            if (pinResult == DialogResult.OK)
            {
                OwnerStorage.FileWriter.WriteLineToFile("User Changed Manager PIN", true);
                OwnerStorage.Log.Add("User Changed Manager PIN    : " + System.DateTime.Now.ToString("HH:mm"));
                CheckPin();
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            InfoForm infoForm = new InfoForm();
            infoForm.ShowDialog();
        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            ReservationsForm allReservations = new ReservationsForm();
            allReservations.ShowDialog();
        }

        private void tbxPass_TextChanged(object sender, EventArgs e)
        {
            if (tbxPass.Text != "")
                btnEditor.Enabled = true;
            else
                btnEditor.Enabled = false;
        }

        private void btnChangeLoad_Click(object sender, EventArgs e)
        {
            RestaurantStatusForm changeStatus = new RestaurantStatusForm();
            DialogResult result = changeStatus.ShowDialog();

            if(result==DialogResult.OK)
            {
                UpdateCapacityLabel();
                OwnerStorage.Log.Add("User Toggled Capacity Level to "+lblCapacity.Text+"    : " + System.DateTime.Now.ToString("HH:mm"));
            }
        }
    }
}