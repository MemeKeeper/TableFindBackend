using BackendlessAPI;
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
            OwnerStorage.LogInfo = new List<String>();
            OwnerStorage.LogTimes = new List<String>();
            string APPLICATION_ID = "6D59291D-64B4-B4E5-FFCD-43BA19198A00";
            string API_KEY = "E1543695-02C8-4C57-8963-0F1AA3861D7B";
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
            OwnerStorage.ActiveReservations = new List<Reservation>();
            OwnerStorage.PastReservations = new List<Reservation>();
            OwnerStorage.AllUsers = new List<BackendlessUser>();
            OwnerStorage.AdminLog = new List<String[]>();
            OwnerStorage.ListOfAdmins = new List<AdminPins>();
            

            this.Hide();
            InitializeComponent();
            PopulateTables();
            CheckLayoutImage();
            UpdateCapacityLabel();
            RetrieveAdminPins();

            PerformBackgroundMenuItemPopulation();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            OwnerStorage.LogInfo.Add("User Logged in with valid Login");
            OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));

            updater = new SharpUpdater(Assembly.GetExecutingAssembly(), this, new Uri("https://backendlessappcontent.com/6D59291D-64B4-B4E5-FFCD-43BA19198A00/3DD4C6AA-9D71-4111-A769-45F6DEE35B62/files/Update/update.xml"));  //<------ Domain on which we host the update for the program

            typeof(Panel).InvokeMember("DoubleBuffered",BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, pnlMain, new object[] { true });

        }

        private void RetrieveAdminPins()
        {
            String whereClause = "restaurantId = '" + OwnerStorage.ThisRestaurant.objectId + "'";
            BackendlessAPI.Persistence.DataQueryBuilder queryBuilder = BackendlessAPI.Persistence.DataQueryBuilder.Create();
            queryBuilder.SetWhereClause(whereClause);

            AsyncCallback<IList<AdminPins>> findCallback;
            findCallback = new AsyncCallback<IList<AdminPins>>(
              foundObjects =>
              {
                  OwnerStorage.ListOfAdmins = (List<AdminPins>)foundObjects;
                  OwnerStorage.LogInfo.Add("Admin PINS have been retrieved.");
                  OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                  Invoke(new Action(() =>
                  {
                      tbxPass.Enabled = true;
                  }));
                  CheckPin();
              },
              error =>
              {
                  OwnerStorage.LogInfo.Add("Failed to retrieve Admin PINS.");
                  OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                  CheckPin();
              });

            Backendless.Data.Of<AdminPins>().Find(queryBuilder, findCallback);
        }
        private void UpdateCapacityLabel()
        {
            switch (OwnerStorage.ThisRestaurant.MaxCapacity)
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
            removeTimer.Interval = 15000;
            removeTimer.Elapsed += OnTimedEvent;
            removeTimer.AutoReset = true;
            removeTimer.Enabled = true;
        }
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {

            foreach (Reservation r in OwnerStorage.ActiveReservations)
            {
                if (r.TakenTo < System.DateTime.Now)
                {
                    AsyncCallback<Reservation> updateObjectCallback = new AsyncCallback<Reservation>(
                    savedReservation =>
                    {
                        OwnerStorage.LogInfo.Add("Reservation has Expired\nName:  " + savedReservation.Name);
                        OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                        RemoveOneReservationView(r,savedReservation);
                    },
                    error =>
                    {
                    });

                    AsyncCallback<Reservation> saveObjectCallback = new AsyncCallback<Reservation>(
                    savedReservation =>
                    {
                        // now update the saved object
                        savedReservation.Active = false;
                        savedReservation.ReasonForExpiration = "Reservation has passed its expiration date";
                        Backendless.Persistence.Of<Reservation>().Save(savedReservation, updateObjectCallback);
                    },
                    error =>
                    {
                    });

                    Backendless.Persistence.Of<Reservation>().Save(r, saveObjectCallback);
                }
            }
        }  //come back

        private void CheckLayoutImage()
        {
            string file = @"layouts\" + OwnerStorage.ThisRestaurant.objectId + "_" + OwnerStorage.ThisRestaurant.LocationString + "_layout.tbl";
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
        private double[] CalcPanelRes()
        {
            double[] result;
            result = new double []{pnlMain.Bounds.Width,pnlMain.Bounds.Height};

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
        public void RemoveOneReservationView(Reservation oldR, Reservation newR)
        {
            ReservationView tempReservation = null;
            bool flag = false;
            foreach (ReservationView view in flpItems.Controls)
            {

                if (view.Tag.ToString() == oldR.objectId.ToString())
                {
                    flag = true;
                    tempReservation = view;
                    Invoke(new Action(() =>
                    {
                        OwnerStorage.ActiveReservations.Remove(oldR);
                        OwnerStorage.PastReservations.Add(newR);
                        tempReservation.pnlContact.MouseClick += new MouseEventHandler(pastReservation_Click);
                        tempReservation.pnlReservation.MouseClick += new MouseEventHandler(pastReservation_Click);
                        flpPrevious.Controls.Add(tempReservation);
                        tempReservation.Removed();

                        flpPrevious.Controls.SetChildIndex(tempReservation, 0);
                        flpItems.Controls.Remove(tempReservation);
                    }));
                }
                if(flag ==false)
                {
                    OwnerStorage.FileWriter.WriteLineToFile("No Reservation View (Icon of the reservation) matching the given ID could be found on the panel", true);
                    OwnerStorage.LogInfo.Add("No Reservation View (Icon of the reservation) matching the given ID could be found on the panel");
                    OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                }
            }


        }    
        public void AddOneReservationView(Reservation r) // Controller for ReservationView
        {
            ReservationView reservation = new ReservationView();
            RestaurantTable table = new RestaurantTable();
            foreach (RestaurantTable t in OwnerStorage.RestaurantTables)
            {
                if (t.objectId == r.TableId)
                    table = t;
            }
            reservation.Tag = r.objectId;

            reservation.UserName = r.Name;
            reservation.UObjectId = r.UserId;
            reservation.UserContactNumber = r.Number;
            reservation.TableName = "Table: " + table.Name;
            reservation.ObjectId = r.objectId;
            reservation.Date = r.TakenFrom.ToString("ddd, dd / MM");
            reservation.FromToTime = r.TakenFrom.ToString("HH:mm") + " to " + r.TakenTo.ToString("HH:mm");            

            reservation.pnlContact.MouseClick += new MouseEventHandler(activeReservation_Click);
            reservation.pnlReservation.MouseClick += new MouseEventHandler(activeReservation_Click);
            Invoke(new Action(() =>
            {
                flpItems.Controls.Add(reservation);
                flpItems.Controls.SetChildIndex(reservation, 0);
                reservation.New();
            }));

            
        }
        private void PerformReservationViewListPopulation(List<Reservation> list)
        {

            flpItems.Controls.Clear();
            flpPrevious.Controls.Clear();

            foreach(Reservation r in list)
            {
                ReservationView reservation = new ReservationView();
                RestaurantTable table = new RestaurantTable();
                foreach (RestaurantTable t in OwnerStorage.RestaurantTables)
                {
                    if (t.objectId == r.TableId)
                        table = t;
                }
                reservation.Tag = r.objectId;

                reservation.UserName = r.Name;
                reservation.UObjectId = r.UserId;
                reservation.UserContactNumber = r.Number;
                reservation.TableName = "Table: " + table.Name;
                reservation.ObjectId = r.objectId;
                reservation.Date = r.TakenFrom.ToString("ddd, dd / MM");
                reservation.FromToTime = r.TakenFrom.ToString("HH:mm") + " to " + r.TakenTo.ToString("HH:mm");

                if (r.Active == true)
                {
                    reservation.pnlContact.MouseClick += new MouseEventHandler(activeReservation_Click);
                    reservation.pnlReservation.MouseClick += new MouseEventHandler(activeReservation_Click);

                    flpItems.Controls.Add(reservation);
                }
                else
                {
                    reservation.pnlContact.MouseClick += new MouseEventHandler(pastReservation_Click);
                    reservation.pnlReservation.MouseClick += new MouseEventHandler(pastReservation_Click);

                    flpPrevious.Controls.Add(reservation);
                }
            }
        }

        private void activeReservation_Click(object sender, MouseEventArgs e)
        {

            Panel templabel = (Panel)sender;
            ReservationView tempReservationView = (ReservationView)templabel.Parent;
            bool flag = false;
            foreach(ReservationView views in flpItems.Controls)
            {
                    if (views.Tag == tempReservationView.Tag)
                    {
                    flag = true;
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
                        foreach (Reservation reservation in OwnerStorage.ActiveReservations)
                        {
                            if (reservation.objectId == views.Tag.ToString())
                            {
                                tempReservation = reservation;
                            }
                        }
                        RestaurantTable tempTable = null;
                        foreach (RestaurantTable table in OwnerStorage.RestaurantTables)
                        {
                            if (table.objectId == tempReservation.TableId)
                            {
                                tempTable = table;
                            }
                        }
                        ReservationDetailsForm details = new ReservationDetailsForm(tempReservation,tempUser,tempTable,true,this);
                            details.ShowDialog();
                            views.Deselected();                             
                    }
            }
            if (flag==false)
            {
                OwnerStorage.FileWriter.WriteLineToFile("No Reservation View (Icon of the reservation) matching the given ID could be found on the panel", true);
                OwnerStorage.LogInfo.Add("No Reservation View (Icon of the reservation) matching the given ID could be found on the panel");
                OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
            }
        }
        private void pastReservation_Click(object sender, MouseEventArgs e)
        {

            Panel templabel = (Panel)sender;
            ReservationView tempReservationView = (ReservationView)templabel.Parent;
            bool flag = false;
            foreach (ReservationView views in flpPrevious.Controls)
            {
                if (views.Tag == tempReservationView.Tag)
                {
                    flag = true;
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
                    foreach (Reservation reservation in OwnerStorage.PastReservations)
                    {
                        if (reservation.objectId == views.Tag.ToString())
                        {
                            tempReservation = reservation;
                        }
                    }
                    RestaurantTable tempTable = null;
                    foreach (RestaurantTable table in OwnerStorage.RestaurantTables)
                    {
                        if (table.objectId == tempReservation.TableId)
                        {
                            tempTable = table;
                        }
                    }
                    ReservationDetailsForm details = new ReservationDetailsForm(tempReservation, tempUser, tempTable, false,this);
                    details.ShowDialog();
                    views.Deselected();
                }
            }
            if(flag==false)
            {
                OwnerStorage.FileWriter.WriteLineToFile("No Reservation View (Icon of the reservation) matching the given ID could be found on the panel", true);
                OwnerStorage.LogInfo.Add("No Reservation View (Icon of the reservation) matching the given ID could be found on the panel");
                OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
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
            OwnerStorage.ActiveReservations.Clear();
            OwnerStorage.PastReservations.Clear();
            //OwnerStorage.AllUsers.Clear();
            String whereClause = "restaurantId = '" + OwnerStorage.ThisRestaurant.objectId + "'";
            BackendlessAPI.Persistence.DataQueryBuilder queryBuilder = BackendlessAPI.Persistence.DataQueryBuilder.Create();
            queryBuilder.SetWhereClause(whereClause);
            queryBuilder.SetPageSize(100);

            AsyncCallback<IList<Reservation>> getContactsCallback = new AsyncCallback<IList<Reservation>>(
            foundReservations =>
            {
                InitializeTimer();

                foreach(Reservation r in foundReservations)
                {
                    if(r.Active==true)
                    {
                        OwnerStorage.ActiveReservations.Add(r);
                    }
                    else
                    {
                        OwnerStorage.PastReservations.Add(r);
                    }
                }
                //OwnerStorage.ActiveReservations = (List<Reservation>)foundReservations;
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
                                    btnApply.Enabled = true;
                                    btnChangePin.Enabled = true;
                                    PerformReservationViewListPopulation((List<Reservation>)foundReservations);
                                }));
                            else
                                i++;
                        },
                        error =>
                        {

                        });

                        Backendless.Data.Of<BackendlessUser>().FindById(r.UserId, loadContactCallback);
                    }
                }
                else
                {
                    Invoke(new Action(() =>
                    {
                        ShowLoading(false);
                        OwnerStorage.FileWriter.WriteLineToFile("No Reservations to download", true);
                        OwnerStorage.LogInfo.Add("No Reservations To Download");
                        OwnerStorage.LogTimes.Add("blank");
                        btnApply.Enabled = true;
                        btnChangePin.Enabled = true;
                        btnViewAll.Enabled = true;
                    }));
                }                
            },
            error =>
            {
                OwnerStorage.FileWriter.WriteLineToFile("Server returned an error " + error.Message, true);
                OwnerStorage.LogInfo.Add("Server returned an error " + error.Message);
                OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
            });

            Backendless.Data.Of<Reservation>().Find(queryBuilder,getContactsCallback);
        }
        private void PopulateTables() // Controller for RestaurantTableView
        {
            btnViewAll.Enabled = false;
            btnApply.Enabled = false;
            btnChangePin.Enabled = false;
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
                                  lblPnlSize.Text = "Recommended floor layout image size: "+CalcPanelRes()[0]+" x " + CalcPanelRes()[1];
                              }));

                          foreach (RestaurantTable tb in OwnerStorage.RestaurantTables)
                          {

                              Invoke(new Action(() =>
                              {                        
                                  RestaurantTableView newItem = new RestaurantTableView();
                                  
                                  newItem.Tag = tb.objectId;
                                  newItem.Label = tb.Name;
                                  newItem.Seating = tb.Capacity;
                                  newItem.Availability = tb.Available;

                                  pnlMain.Controls.Add(newItem);
                                  if (tb.YPos>pnlMain.Height ||tb.XPos>pnlMain.Width)
                                  {
                                      pnlMain.Controls[i].Location = new Point(pnlMain.Width-73, pnlMain.Height - 38);
                                  }
                                  else
                                      pnlMain.Controls[i].Location = new Point(Convert.ToInt32(tb.XPos* pnlMain.Width), Convert.ToInt32(tb.YPos * pnlMain.Height));

                                  this.AddControl(newItem);

                                  if (OwnerStorage.AdminMode == true)
                                      newItem.Removable = true;
                              }));
                              i++;                              
                          }
                      },
                      error =>
                      {
                          Invoke(new Action(() =>
                          {
                              MessageBox.Show(this, "Error: " + error.Message);
                              OwnerStorage.FileWriter.WriteLineToFile("Tables downloading failed", true);
                              OwnerStorage.LogInfo.Add("Tables downloading failed");
                              OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                              ShowLoading(false);
                          }));
                      });

                  Backendless.Data.Of<RestaurantTable>().Find(queryBuilder,getTableItemCallback);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            RestaurantTable newTable = new RestaurantTable();

            newTable.Name = "Tbl #"+Convert.ToString(OwnerStorage.RestaurantTables.Count+1);
            newTable.Capacity = 1; //<-- min value
            newTable.RestaurantId = OwnerStorage.ThisRestaurant.objectId;

            OwnerStorage.RestaurantTables.Add(newTable);

            RestaurantTableView newView = new RestaurantTableView();

            newView.Location = new Point(0, 0);
            newView.Seating = newTable.Capacity;
            newView.Label = newTable.Name;
            newView.Availability = newTable.Available;            

            AddControl(newView);
            MyControl_DoubleClick(newView, e);

            OwnerStorage.FileWriter.WriteLineToFile("User created a new Table.", true);
            OwnerStorage.FileWriter.WriteLineToFile("Name:  " + newTable.Name, false);
            OwnerStorage.LogInfo.Add("User added a new Restaurant Table\nName:  "+ newTable.Name);
            OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));

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
            OwnerStorage.LogInfo.Add("Editing a Table\nName:  " + tempItem.Label);
            OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));

            bool flag = false;
            foreach (RestaurantTable ti in OwnerStorage.RestaurantTables)
            {
                if (ti.objectId == tempItem.Tag)
                {
                    flag = true;
                   tempTable = ti;
                }
            }
                if (OwnerStorage.AdminMode == true)
                {
                    EditTableForm editor = new EditTableForm(tempTable, this);
                    DialogResult result = editor.ShowDialog();
                    tempTable = editor.RetreiveEditedTable();

                    if (result == DialogResult.Yes) //Removes the Table
                    {
                        OwnerStorage.RestaurantTables.Remove(tempTable);
                        if (sender is Label)
                        {
                            Label tempLabel = (Label)sender;
                            pnlMain.Controls.Remove((RestaurantTableView)tempLabel.Parent);
                        }
                        else
                            pnlMain.Controls.Remove((RestaurantTableView)sender);
                    }
                    if (result == DialogResult.Cancel) //When the user creates a new table but cancels the process
                    {
                        if (tempTable.objectId == null)
                        {
                            pnlMain.Controls.Remove(tempItem);
                            OwnerStorage.RestaurantTables.Remove(tempTable);
                        }
                    }
                    if (result == DialogResult.OK) //Updates the Table
                    {
                        OwnerStorage.RestaurantTables.Remove(tempTable);
                        Point tempPoint = new Point(0, 0);
                        if (sender is Label)
                        {
                            Label tempLabel = (Label)sender;
                            RestaurantTableView tempSenderView = (RestaurantTableView)tempLabel.Parent;
                            foreach (RestaurantTableView t in pnlMain.Controls) //foreach to iterate through the code to capture the location of the control being edited
                            {
                                if (t.Tag == tempSenderView.Tag)
                                {
                                    tempPoint = t.Location;
                                }
                            }
                            pnlMain.Controls.Remove(tempSenderView);
                        }
                        else
                        {
                            foreach (RestaurantTableView t in pnlMain.Controls) //foreach to iterate through the code to capture the location of the control being edited
                            {
                                if (t.Tag == ((RestaurantTableView)sender).Tag)
                                {
                                    tempPoint = t.Location;
                                }
                            }
                            pnlMain.Controls.Remove((RestaurantTableView)sender);
                        }

                        RestaurantTableView tempView = new RestaurantTableView();

                        tempView.Tag = tempTable.objectId;
                        tempView.Label = tempTable.Name;
                        tempView.Seating = tempTable.Capacity;
                        tempView.Availability = tempTable.Available;

                        pnlMain.Controls.Add(tempView);
                        pnlMain.Controls[pnlMain.Controls.Count - 1].Location = tempPoint;
                        //pnlMain.Controls[pnlMain.Controls.Count-1].Location = new Point(Convert.ToInt32(tempTable.XPos * pnlMain.Width), Convert.ToInt32(tempTable.YPos * pnlMain.Height));

                        OwnerStorage.RestaurantTables.Add(tempTable);

                        AddControl(tempView);

                        OwnerStorage.FileWriter.WriteLineToFile("User Updated Table", true);
                        OwnerStorage.FileWriter.WriteLineToFile("Name:  " + tempView.Label, false);
                    }
                }
                else
                {
                    ReservationsForm reservationsForm = new ReservationsForm(tempTable, this);
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
                tb.YPos = pnlMain.Controls[i].Location.Y/ Convert.ToDouble(pnlMain.Height);
                tb.XPos = pnlMain.Controls[i].Location.X/ Convert.ToDouble(pnlMain.Width);
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

                        OwnerStorage.LogInfo.Add("User made changes to restaurant floor plan");
                        OwnerStorage.LogInfo.Add("User deactivated Admin mode");
                        OwnerStorage.LogTimes.Add("blank");
                        OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));

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
                        OwnerStorage.LogInfo.Add("Error: " + error.Message);
                        OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
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
                    OwnerStorage.LogInfo.Add("Error: " + error.Message);
                    OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                    ShowLoading(false);
                    MessageBox.Show(this, "Error: " + error.Message);

                }));
            });

            Backendless.Data.Of<RestaurantTable>().Remove("restaurantId = '" + OwnerStorage.ThisRestaurant.objectId+"'", bulkDeleteCallback);
            
        }

        private void btnReloadAll_Click(object sender, EventArgs e)
        {
            createdListener.RemoveCreatedEventListener();
            deletedListener.RemoveDeletedEventListener();
            PopulateTables();
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }
        private void CheckPin()
        {
            
            if(OwnerStorage.ListOfAdmins.Count == 0)
            {
                Invoke(new Action(() =>
                {
                    DialogResult result = MessageBox.Show(this, "Seems like this is your first time logging in to the " +
                        "TableFindBackend Desktop App with this restaurant, therefore " +
                        "you have not yet registered a Manager PIN. Without it " +
                        "you will not be able to have access to admin capabilities." +
                        "\nWould you like to configure a Manager PIN now?", "Manager PIN", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        ChangePinForm NewPin = new ChangePinForm();
                        NewPin.ShowDialog();
                    }
                    else if (result == DialogResult.No)
                    {
                        OwnerStorage.FileWriter.WriteLineToFile("User Denied First-time Manager PIN Setup", true);
                    }
                }));
                
            }
            else
            {

                Invoke(new Action(() =>
                {
                    AdminPins flag = null;
                    foreach (AdminPins a in OwnerStorage.ListOfAdmins)
                    {
                        if (tbxPass.Text.Equals(a.PinCode.ToString())==true)
                        {
                            flag = a;
                        }
                    }

                    if (flag != null)
                    {
                        toggleAdminMode(true);

                        OwnerStorage.AdminLog.Add(new string[] { flag.objectId, System.DateTime.Now.ToString("HH:mm:ss") });

                        OwnerStorage.FileWriter.WriteLineToFile(flag.UserName+" Toggled Admin Mode", true);
                        OwnerStorage.LogInfo.Add(flag.UserName + " activated elevated privileges");
                        OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));

                    }
                    tbxPass.Text = "";
                    tbxPass.Focus();
                }));                
            }                       
        }
        private void btnAdmin_Click(object sender, EventArgs e)
        {
             CheckPin();
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
                OwnerStorage.LogInfo.Add("User Edited Restaurant Details");
                OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
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
                OwnerStorage.LogInfo.Add("User made changes to Restaurant Menu");
                OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
            }
            
        }

        private void btnChangePin_Click(object sender, EventArgs e)
        {

            ChangePinForm NewPin = new ChangePinForm();
            DialogResult pinResult = NewPin.ShowDialog();
            if (pinResult == DialogResult.OK)
            {
                OwnerStorage.FileWriter.WriteLineToFile("User Modified Manager PINs", true);
                OwnerStorage.LogInfo.Add("User Modified Manager PINs");
                OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
            }
            if (OwnerStorage.ListOfAdmins.Count != 0)
            {
                tbxPass.Enabled = true;
            }
            else
                tbxPass.Enabled = false;
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
                OwnerStorage.LogInfo.Add("User Toggled Capacity Level to "+lblCapacity.Text);
                OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
            }
        }

        private void pboxLoading_Click(object sender, EventArgs e)
        {

        }
    }
}