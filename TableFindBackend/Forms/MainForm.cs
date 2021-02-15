using BackendlessAPI;
using BackendlessAPI.Async;
using SharpUpdate;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using TableFindBackend.Global_Variables;
using TableFindBackend.Logging;
using TableFindBackend.Models;
using TableFindBackend.RT_Database_Listeneres;
using TableFindBackend.ViewModels;


namespace TableFindBackend.Forms
{
    public partial class MainForm : Form
    {
        //main Form. This is where all the processes and actions a to happen. Most complex proceedures will occur here.
        private Control activeControl;//properties which is neccessary to move the RestaurantTableViews Around
        private Point previousPosition;

        private SharpUpdater updater;//the updater object used to update the program

        private ReservationCreatedEventListener createdListener;//these properties are for the created and deleted listeners. they give the program the ability to detect changes in the database in real time
        private ReservationDeletedEventListener deletedListener;

        private static System.Timers.Timer removeTimer;//the timer that checks if a reservation has expired

        public MainForm()
        {
            //all properties are instantiated
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

            //all Application Variables are now instantiated
            OwnerStorage.AdminMode = false;
            OwnerStorage.RestaurantTables = new List<RestaurantTable>();
            OwnerStorage.MenuItems = new List<RestaurantMenuItem>();
            OwnerStorage.ActiveReservations = new List<Reservation>();
            OwnerStorage.PastReservations = new List<Reservation>();
            OwnerStorage.AllUsers = new List<BackendlessUser>();
            OwnerStorage.AdminLog = new List<String[]>();
            OwnerStorage.ListOfAdmins = new List<AdminPins>();

            //all Async methods are executed, which main goals are to retreive all relevent information form the database            
            this.Hide();
            InitializeComponent();
            PopulateTables();
            CheckLayoutImage();
            UpdateCapacityLabel();
            RetrieveAdminPins();
            PerformBackgroundMenuItemPopulation();

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            //logs that the user has successfully logged in
            OwnerStorage.LogInfo.Add("User Logged in with valid Login");
            OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));

            //sets up the updater object with the appropriate link
            updater = new SharpUpdater(Assembly.GetExecutingAssembly(), this, new Uri("https://backendlessappcontent.com/6D59291D-64B4-B4E5-FFCD-43BA19198A00/3DD4C6AA-9D71-4111-A769-45F6DEE35B62/files/Update/update.xml"));  //<------ Domain on which we host the update for the program

            //ensures that the form remains smooth
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, pnlMain, new object[] { true });
        }

        private void RetrieveAdminPins()
        {
            //Gets all AdminPIN objects for this specific restaurant
            String whereClause = "restaurantId = '" + OwnerStorage.ThisRestaurant.objectId + "'";
            BackendlessAPI.Persistence.DataQueryBuilder queryBuilder = BackendlessAPI.Persistence.DataQueryBuilder.Create();
            queryBuilder.SetWhereClause(whereClause);

            AsyncCallback<IList<AdminPins>> findCallback;
            findCallback = new AsyncCallback<IList<AdminPins>>(
              foundObjects =>
              {
                  //success, all Admin Pins have been retreived
                  OwnerStorage.ListOfAdmins = (List<AdminPins>)foundObjects;
                  OwnerStorage.LogInfo.Add("Admin PINS have been retrieved.");
                  OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                  //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                  Invoke(new Action(() =>
                  {
                      tbxPass.Enabled = true;
                  }));
                  CheckPin();
              },
              error =>
              {
                  //something went wrong, OR there is no AdminPINS for this restaurant
                  OwnerStorage.LogInfo.Add("Failed to retrieve Admin PINS.");
                  OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                  CheckPin();
              });

            Backendless.Data.Of<AdminPins>().Find(queryBuilder, findCallback);
        }
        private void UpdateCapacityLabel()
        {
            //a simple method that checks the current setting of the capacity property and sets the label text
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
            //a method used to setup the timer settings
            removeTimer = new System.Timers.Timer();
            removeTimer.Interval = 15000;//<-- every 15 seconds
            removeTimer.Elapsed += OnTimedEvent;
            removeTimer.AutoReset = true;
            removeTimer.Enabled = true;
        }
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            //the timer event that checks all reservations every 15 seconds and removes reservations which expires
            foreach (Reservation r in OwnerStorage.ActiveReservations)
            {
                if (r.TakenTo < System.DateTime.Now)
                {
                    AsyncCallback<Reservation> updateObjectCallback = new AsyncCallback<Reservation>(
                    savedReservation =>
                    {
                        //success. The object has been deactivated and moved to the flpPastReservations tab
                        OwnerStorage.LogInfo.Add("Reservation has Expired\nName:  " + savedReservation.Name);
                        OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                        //this method moves it to the other view
                        RemoveOneReservationView(r, savedReservation);
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

                    //runs the save callback
                    Backendless.Persistence.Of<Reservation>().Save(r, saveObjectCallback);
                }
            }
        }  //come back
        private void CheckLayoutImage()
        {
            //a method that checks actively wheather the layout image has been changed
            string file = @"layouts\" + OwnerStorage.ThisRestaurant.objectId + "_" + OwnerStorage.ThisRestaurant.LocationString + "_layout.tbl";
            if (File.Exists(file) == true)
            {
                pnlMain.BackgroundImage = Image.FromFile(file);
                //garbage collector for 'deactivating' the active layout image
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
            //disposes the active layout image
            if (pnlMain.BackgroundImage != null)
                pnlMain.BackgroundImage.Dispose();
        }
        private double[] CalcPanelRes()
        {
            //a method which calculates the dimensions of the panel, so that the RestaurantTableViews can be correctly displayed
            double[] result;
            result = new double[] { pnlMain.Bounds.Width, pnlMain.Bounds.Height };

            return result;
        }

        private void ShowLoading(bool enable)
        {
            //The method that simulates a loading screen when enabled
            if (enable == true)
            {
                pboxLoading.Visible = true;
                btnApply.Enabled = false;
            }
            else
            {
                pboxLoading.Visible = false;
                btnApply.Enabled = true;
            }
        }
        public void RemoveOneReservationView(Reservation oldR, Reservation newR)
        {
            //this method will move the ReservationView from the active flp to the past flp
            ReservationView tempReservation = null;
            bool flag = false;
            //it has to determine which ReservationView has to be moved.
            foreach (ReservationView view in flpItems.Controls)
            {
                if (view.Tag.ToString() == oldR.objectId.ToString())
                {
                    flag = true;
                    tempReservation = view;
                    //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                    Invoke(new Action(() =>
                    {
                        //this will remove the old reservationView and add the new one to the pastReservations
                        OwnerStorage.ActiveReservations.Remove(oldR);
                        OwnerStorage.PastReservations.Add(newR);
                        tempReservation.pnlContact.MouseClick += new MouseEventHandler(pastReservation_Click);
                        tempReservation.pnlReservation.MouseClick += new MouseEventHandler(pastReservation_Click);
                        flpPrevious.Controls.Add(tempReservation);
                        tempReservation.Removed();
                        
                        //adds it to the top of the flp
                        flpPrevious.Controls.SetChildIndex(tempReservation, 0);
                        flpItems.Controls.Remove(tempReservation);
                    }));
                }
                if (flag == false)//in the rare event that the click event does not work, it has to be logged
                {
                    OwnerStorage.FileWriter.WriteLineToFile("No Reservation View (Icon of the reservation) matching the given ID could be found on the panel", true);
                    OwnerStorage.LogInfo.Add("No Reservation View (Icon of the reservation) matching the given ID could be found on the panel");
                    OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                }
            }
        }
        public void AddOneReservationView(Reservation r) // Controller for ReservationView
        {
            //Adds one ReservationView to the ActiveReservation flp
            ReservationView reservation = new ReservationView();
            RestaurantTable table = new RestaurantTable();
            foreach (RestaurantTable t in OwnerStorage.RestaurantTables)
            {
                if (t.objectId == r.TableId)
                    table = t;
            }

            if(table==null)//rare event, but precautions must be added
            {
                table.Name = "-";
                table.Capacity =0;
                table.RestaurantId = OwnerStorage.ThisRestaurant.objectId;
                table.TableInfo = "-";
                table.objectId = "-";
                table.Available = false;
                table.XPos = 0;
                table.YPos = 0;
            }
            
            //sets the reservationView Properties
            reservation.Tag = r.objectId;
            reservation.UserName = r.Name;
            reservation.UObjectId = r.UserId;
            reservation.UserContactNumber = r.Number;
            reservation.TableName = "Table: " + table.Name;
            reservation.ObjectId = r.objectId;
            reservation.Date = r.TakenFrom.ToString("ddd, dd / MM");
            reservation.FromToTime = r.TakenFrom.ToString("HH:mm") + " to " + r.TakenTo.ToString("HH:mm");

            //adds the on-click events
            reservation.pnlContact.MouseClick += new MouseEventHandler(activeReservation_Click);
            reservation.pnlReservation.MouseClick += new MouseEventHandler(activeReservation_Click);
            //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
            Invoke(new Action(() =>
            {
                flpItems.Controls.Add(reservation);
                flpItems.Controls.SetChildIndex(reservation, 0);
                reservation.New();
            }));
        }
        private void PerformReservationViewListPopulation(List<Reservation> list)
        {
            //this method populates both flp with active and unactive reservationViews
            flpItems.Controls.Clear();
            flpPrevious.Controls.Clear();

            foreach (Reservation r in list)
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
            //an On-click event for the flpItems panel's controls
            Panel templabel = (Panel)sender;
            ReservationView tempReservationView = (ReservationView)templabel.Parent;
            bool flag = false;
            foreach (ReservationView views in flpItems.Controls)
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
                    if(tempUser==null)//in the rare case that a user could not be found (removed manually from the database), a blank BackendlessUser object is created
                    {
                        BackendlessUser blankUser = new BackendlessUser();
                        blankUser.Email = "-";
                        blankUser.SetProperty("Cellphone", "-");
                        blankUser.Password = "-";
                        blankUser.SetProperty("FirstName", "-");
                        blankUser.SetProperty("LastName", "-");
                        blankUser.SetProperty("isOwner", false);
                        tempUser = blankUser;
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

                    ReservationDetailsForm details = new ReservationDetailsForm(tempReservation, tempUser, tempTable, true, this);
                    details.ShowDialog();
                    views.Deselected();
                }
            }
            if (flag == false)//rare event, but added precautions anyway
            {
                OwnerStorage.FileWriter.WriteLineToFile("No Reservation View (Icon of the reservation) matching the given ID could be found on the panel", true);
                OwnerStorage.LogInfo.Add("No Reservation View (Icon of the reservation) matching the given ID could be found on the panel");
                OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
            }
        }
        private void pastReservation_Click(object sender, MouseEventArgs e)
        {
            //an On-click event for the flpItems panel's controls
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
                    if (tempUser == null)//in the rare case that a user could not be found (removed manually from the database), a blank BackendlessUser object is created
                    {
                        BackendlessUser blankUser = new BackendlessUser();
                        blankUser.Email = "-";
                        blankUser.SetProperty("Cellphone", "-");
                        blankUser.Password = "-";
                        blankUser.SetProperty("FirstName", "-");
                        blankUser.SetProperty("LastName", "-");
                        blankUser.SetProperty("isOwner", false);
                        tempUser = blankUser;
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
                    ReservationDetailsForm details = new ReservationDetailsForm(tempReservation, tempUser, tempTable, false, this);
                    details.ShowDialog();
                    views.Deselected();
                }
            }
            if (flag == false)//rare event, but added precautions anyway
            {
                OwnerStorage.FileWriter.WriteLineToFile("No Reservation View (Icon of the reservation) matching the given ID could be found on the panel", true);
                OwnerStorage.LogInfo.Add("No Reservation View (Icon of the reservation) matching the given ID could be found on the panel");
                OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
            }
        }

        private void PerformBackgroundMenuItemPopulation()
        {
            //This Method will download all currently saved MenuItems from Backendless
            String whereClause = "restaurantID = '" + OwnerStorage.ThisRestaurant.objectId + "'";
            BackendlessAPI.Persistence.DataQueryBuilder queryBuilder = BackendlessAPI.Persistence.DataQueryBuilder.Create();
            queryBuilder.SetWhereClause(whereClause);
            queryBuilder.SetPageSize(100);

            AsyncCallback<IList<RestaurantMenuItem>> getBookingCallback = new AsyncCallback<IList<RestaurantMenuItem>>(

            foundRestaurantMenuItem =>
            {
                //success, the event will be logged
                OwnerStorage.MenuItems = (List<RestaurantMenuItem>)foundRestaurantMenuItem;
                OwnerStorage.FileWriter.WriteLineToFile("Online interactive menu has been downloaded", true);
            },
            error =>
            {
                //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                //something went wrong, an error message will be displayed
                Invoke(new Action(() =>
                {
                    MessageBox.Show(this, "Error: " + error.Message);
                    OwnerStorage.FileWriter.WriteLineToFile("Online interactive menu failed to download", true);
                }));
            });
            //runs the callback
            Backendless.Data.Of<RestaurantMenuItem>().Find(queryBuilder, getBookingCallback);
        }

        private void PerformBackgroundReservationPopulation()
        {
            //this method will download all reservation (max 100) from the backendless database
            //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
            Invoke(new Action(() =>
            {
                ShowLoading(true);
            }));
            OwnerStorage.ActiveReservations.Clear();
            OwnerStorage.PastReservations.Clear();
            String whereClause = "restaurantId = '" + OwnerStorage.ThisRestaurant.objectId + "'";
            BackendlessAPI.Persistence.DataQueryBuilder queryBuilder = BackendlessAPI.Persistence.DataQueryBuilder.Create();
            queryBuilder.SetWhereClause(whereClause);
            queryBuilder.SetPageSize(100);
            queryBuilder.AddSortBy("takenFrom asc");

            AsyncCallback<IList<Reservation>> getContactsCallback = new AsyncCallback<IList<Reservation>>(
            foundReservations =>
            {
                //success, reservations has been downloaded
                InitializeTimer();

                foreach (Reservation r in foundReservations)
                {
                    if (r.Active == true)
                    {
                        OwnerStorage.ActiveReservations.Add(r);
                    }
                    else
                    {
                        OwnerStorage.PastReservations.Add(r);
                    }
                }
                
                if (foundReservations.Count != 0)//makes sure that there are reservations for this restaurant
                {
                    int i = 1;
                    foreach (Reservation r in foundReservations)
                    {
                        //for each reservation the corrosponding user object will be downloaded
                        AsyncCallback<BackendlessUser> loadContactCallback = new AsyncCallback<BackendlessUser>(
                        foundContact =>
                        {
                            //success, the user will now be added to a list
                            OwnerStorage.AllUsers.Add(foundContact);
                            if (i == foundReservations.Count)
                            //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                            Invoke(new Action(() =>
                                {
                                    ShowLoading(false);
                                    OwnerStorage.FileWriter.WriteLineToFile("All Reservations has been downloaded", true);
                                    btnViewAll.Enabled = true;
                                    btnApply.Enabled = true;
                                    btnManageAdminUsers.Enabled = true;
                                    PerformReservationViewListPopulation((List<Reservation>)foundReservations);
                                }));
                            else
                                i++;                                                       
                        },
                        error =>
                        {
                            //In the rare case that a user has been removed completely from the database, a blank record has to be generated so that the program can resume
                            BackendlessUser blankUser = new BackendlessUser();
                            blankUser.Email = "-";
                            blankUser.SetProperty("Cellphone", "-");
                            blankUser.Password = "-";
                            blankUser.SetProperty("FirstName", "-");
                            blankUser.SetProperty("LastName", "-");
                            blankUser.SetProperty("isOwner", false);
                            OwnerStorage.AllUsers.Add(blankUser);
                            if (i == foundReservations.Count)
                                //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread                                
                                Invoke(new Action(() =>
                                {
                                    ShowLoading(false);
                                    OwnerStorage.FileWriter.WriteLineToFile("All Reservations has been downloaded", true);
                                    btnViewAll.Enabled = true;
                                    btnApply.Enabled = true;
                                    btnManageAdminUsers.Enabled = true;
                                    PerformReservationViewListPopulation((List<Reservation>)foundReservations);
                                }));
                            else
                                i++;

                            OwnerStorage.FileWriter.WriteLineToFile("Server returned an error " + error.Message, true);
                            OwnerStorage.LogInfo.Add("Server returned an error " + error.Message);
                            OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                        });

                        Backendless.Data.Of<BackendlessUser>().FindById(r.UserId, loadContactCallback);
                    }
                }
                else
                {
                    //no reservations for this restaurant was found. a message will now display
                    //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                    Invoke(new Action(() =>
                    {
                        ShowLoading(false);
                        OwnerStorage.FileWriter.WriteLineToFile("No Reservations to download", true);
                        OwnerStorage.LogInfo.Add("No Reservations To Download");
                        OwnerStorage.LogTimes.Add("blank");
                        btnApply.Enabled = true;
                        btnManageAdminUsers.Enabled = true;
                        btnViewAll.Enabled = true;
                    }));
                }
            },
            error =>
            {
                //something went wrong, an error message will be displayed
                OwnerStorage.FileWriter.WriteLineToFile("Server returned an error " + error.Message, true);
                OwnerStorage.LogInfo.Add("Server returned an error " + error.Message);
                OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                Invoke(new Action(() =>
                {
                    MessageBox.Show(this, "Error: " + error.Message);
                }));
            });

            Backendless.Data.Of<Reservation>().Find(queryBuilder, getContactsCallback);
        }
        private void PopulateTables() // Controller for RestaurantTableView
        {
            //this method will generate the RestaurantTableView(s)
            btnViewAll.Enabled = false;
            btnApply.Enabled = false;
            btnManageAdminUsers.Enabled = false;
            OwnerStorage.RestaurantTables.Clear();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Clear();
            ShowLoading(true);

            String whereClause = "restaurantId = '" + OwnerStorage.ThisRestaurant.objectId + "'";
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
                          //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                          Invoke(new Action(() =>
                             {
                                 lblPnlSize.Text = "Recommended floor layout image size: " + CalcPanelRes()[0] + " x " + CalcPanelRes()[1];
                             }));

                          foreach (RestaurantTable tb in OwnerStorage.RestaurantTables)
                          {
                              // for each RestaurantTable which was downloaded, a RestaurantTableView must be generated
                              //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                              Invoke(new Action(() =>
                              {
                                  RestaurantTableView newItem = new RestaurantTableView();

                                  newItem.Tag = tb.objectId;
                                  newItem.Label = tb.Name;
                                  newItem.Seating = tb.Capacity;
                                  newItem.Availability = tb.Available;

                                  pnlMain.Controls.Add(newItem);
                                  if (tb.YPos > pnlMain.Height || tb.XPos > pnlMain.Width)
                                  {
                                      pnlMain.Controls[i].Location = new Point(pnlMain.Width - 73, pnlMain.Height - 38);
                                  }
                                  else
                                      pnlMain.Controls[i].Location = new Point(Convert.ToInt32(tb.XPos * pnlMain.Width), Convert.ToInt32(tb.YPos * pnlMain.Height));

                                  this.AddControl(newItem);

                                  if (OwnerStorage.AdminMode == true)
                                      newItem.Removable = true;
                              }));
                              i++;
                          }
                      },
                      error =>
                      {
                          //something went wrong, an error message will now display
                          //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                          Invoke(new Action(() =>
                          {
                              MessageBox.Show(this, "Error: " + error.Message);
                              OwnerStorage.FileWriter.WriteLineToFile("Tables downloading failed", true);
                              OwnerStorage.LogInfo.Add("Tables downloading failed");
                              OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                              ShowLoading(false);
                          }));
                      });

            //runs the callback
            Backendless.Data.Of<RestaurantTable>().Find(queryBuilder, getTableItemCallback);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            //this method will prep everything needed to generate a new RestaurantTable AND a new RestaurantTableView
            RestaurantTable newTable = new RestaurantTable();

            newTable.Name = "Tbl #" + Convert.ToString(OwnerStorage.RestaurantTables.Count + 1);
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

            //Logs everything that happends
            OwnerStorage.FileWriter.WriteLineToFile("User created a new Table.", true);
            OwnerStorage.FileWriter.WriteLineToFile("Name:  " + newTable.Name, false);
            OwnerStorage.LogInfo.Add("User added a new Restaurant Table\nName:  " + newTable.Name);
            OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));

        }
        private void AddControl(RestaurantTableView item)
        {
            //reused method which defines all the events and handles for the RestaurantTableView
            pnlMain.Controls.Add(item);
            item.lblName.MouseDoubleClick += new MouseEventHandler(MyControl_DoubleClick);
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
            //this method is used when the user double clicks on the restaurantTableView. Depending on whether adminMode is active, its should open a specific form

            RestaurantTableView tempItem;

            //this block of code determines on what the user double clicked and converts it to the same object
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

            //this foreach determines which RestaurantTable the user is refering to by checking which View has the same tag as the the RestaurantTable ObjectId
            foreach (RestaurantTable ti in OwnerStorage.RestaurantTables)
            {
                if (ti.objectId == tempItem.Tag)
                {
                    tempTable = ti;
                }
            }
            // If the used is in admin mode, It will display the table editer form
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

                    RestaurantTableView tempView = new RestaurantTableView
                    {
                        Tag = tempTable.objectId,
                        Label = tempTable.Name,
                        Seating = tempTable.Capacity,
                        Availability = tempTable.Available
                    };

                    pnlMain.Controls.Add(tempView);
                    pnlMain.Controls[pnlMain.Controls.Count - 1].Location = tempPoint;
                    //pnlMain.Controls[pnlMain.Controls.Count-1].Location = new Point(Convert.ToInt32(tempTable.XPos * pnlMain.Width), Convert.ToInt32(tempTable.YPos * pnlMain.Height));

                    OwnerStorage.RestaurantTables.Add(tempTable);

                    AddControl(tempView);

                    OwnerStorage.FileWriter.WriteLineToFile("User Updated Table", true);
                    OwnerStorage.FileWriter.WriteLineToFile("Name:  " + tempView.Label, false);
                    OwnerStorage.LogInfo.Add("User Updated Table\nName:  " + tempItem.Label);
                    OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                }
            }
            else//if the user is not in admin mode, he/she will be directly taken to the reservationsForm
            {
                ReservationsForm reservationsForm = new ReservationsForm(tempTable, this);
                reservationsForm.ShowDialog();
            }
        }
        private void MyControl_MouseUp(object sender, MouseEventArgs e)
        {
            //simple method for when the user deselects the restaurantTableView
            if (OwnerStorage.AdminMode == true)
            {
                activeControl = null;
                Cursor = Cursors.Default;
            }
        }

        private void MyControl_MouseMove(object sender, MouseEventArgs e)
        {
            //while the user holds down the Table Controls he/she can drag them around using this method
            if (OwnerStorage.AdminMode == true)
            {
                //makes sure that no matter where the user clicks it will register the correct control
                if (sender is Label)
                {
                    Label tempLabel = (Label)sender;
                    RestaurantTableView tempView = (RestaurantTableView)tempLabel.Parent;

                    //only admin users are allowed to move table objects
                    if (activeControl == null || activeControl != tempView)
                    {
                        return;
                    }
                    var lbllocation = activeControl.Location;

                    //applies the new location to the control
                    lbllocation.Offset(e.Location.X - previousPosition.X, e.Location.Y - previousPosition.Y);
                    activeControl.Location = lbllocation;
                }
                else
                {
                    //only admin users are allowed to move table objects
                    if (activeControl == null || activeControl != sender)
                    {
                        return;
                    }
                    var location = activeControl.Location;

                    //applies the new location to the control
                    location.Offset(e.Location.X - previousPosition.X, e.Location.Y - previousPosition.Y);
                    activeControl.Location = location;
                }
            }
        }

        private void MyControl_MouseDown(object sender, MouseEventArgs e)
        {
            //method used to hold down the Table Controls in order to drag them around

            if (e.Button == MouseButtons.Left && e.Clicks >= 2)
            {
                MyControl_DoubleClick(sender, e);
                return;
            }

            //only admin users are allowed to move table objects
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
            //simple method that closes the program after documenting the event in the logs
            OwnerStorage.FileWriter.FormShutDown();
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            //This Method is used to finilize all changes made while the program was in admin mode and also deactivates admin mode
            //it is crucial that no network problems occure during this proccess, because if something goes wrong in the inner async task all the RestaurantTables (including the ones that already existed) will be permenantly lost.
            ShowLoading(true);
            int i = 0;
            foreach (RestaurantTable tb in OwnerStorage.RestaurantTables)//this block of code is neccessary to ensure that the locations of the RestaurantTableViews can be used on screens of different sizes
            {
                tb.YPos = pnlMain.Controls[i].Location.Y / Convert.ToDouble(pnlMain.Height);
                tb.XPos = pnlMain.Controls[i].Location.X / Convert.ToDouble(pnlMain.Width);
                i++;
            }

            AsyncCallback<int> bulkDeleteCallback = new AsyncCallback<int>(
        objectsDeleted =>
        {
            //success, all tables on backendless for this restaurant has been removed. Now it can be reuploaded in bulk with the new changes made
            AsyncCallback<IList<string>> bulkCreateCallback = new AsyncCallback<IList<string>>(
            result =>
            {
                //Save as a list. All RestaurantTables are reuploaded with changes applied.
                //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                Invoke(new Action(() =>
                {
                    OwnerStorage.FileWriter.WriteLineToFile("User Finilized changes to restaurant", true);
                    OwnerStorage.FileWriter.WriteLineToFile("User deactivated Admin Mode", true);

                    OwnerStorage.LogInfo.Add("User Applied changes to restaurant layout \nUser deactivated Admin Mode");
                    OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));

                    //relaunches
                    ShowLoading(false);

                    //removed these lines for performance reasons, but it makes the interface more stable
                    //createdListener.RemoveCreatedEventListener();
                    //deletedListener.RemoveDeletedEventListener();
                    //PopulateTables();
                }));

                //deactivated admin mode
                toggleAdminMode(false);
            },
            error => 
            {
                //something went wrong during deletion of the tables. It is possible that all RestaurantTable information has been lost. An error message will display.
                //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
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

            //starts the bulk create callback
            Backendless.Data.Of<RestaurantTable>().Create(OwnerStorage.RestaurantTables, bulkCreateCallback);
        },
        error =>
        {
            //something went wrong during deletion of the tables. It is possible that all tables are still intact and that the user will just have to make the changes again. an error message will display.
            //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
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
            // in order to update in bulk, all tables must first be deleted and then recreated with the existing objectIDs.
            Backendless.Data.Of<RestaurantTable>().Remove("restaurantId = '" + OwnerStorage.ThisRestaurant.objectId + "'", bulkDeleteCallback);
        }

        private void btnReloadAll_Click(object sender, EventArgs e)
        {
            //a simple refresh button which redownloads all objects in the rare case of network failure during startup]

            createdListener.RemoveCreatedEventListener();//both listeners has to be removed to stop the program from creating duplicates
            deletedListener.RemoveDeletedEventListener();
            PopulateTables();
            RetrieveAdminPins();

            PerformBackgroundMenuItemPopulation();
        }

        private void CheckPin()
        {
            //this method will do multiple functions: It checks the pin which the user entered, it checks if its the first time the uses the program and it also
            if (OwnerStorage.ListOfAdmins.Count == 0)//basically detects if this is the first time the user logs in, if so the user wil then be taken through the proccess of setting up the restaurant for first time user
            {
                //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                Invoke(new Action(() =>
                {
                    DialogResult result = MessageBox.Show(this, "Seems like this is your first time logging in to the " +
                        "TableFindBackend Desktop App with this restaurant, therefore " +
                        "you have not yet registered a Manager PIN. Without it " +
                        "you will not be able to have access to admin capabilities." +
                        "\nWould you like to configure a Manager PIN now?", "Manager PIN", MessageBoxButtons.YesNo, MessageBoxIcon.Information);//message stating that it is his first time using the program
                    if (result == DialogResult.Yes)//user said yes
                    {
                        ManageAdminUsersForm NewPin = new ManageAdminUsersForm();
                        NewPin.ShowDialog();
                    }
                    else if (result == DialogResult.No)//user said no
                    {
                        OwnerStorage.FileWriter.WriteLineToFile("User Denied First-time Manager PIN Setup", true);
                    }
                }));

            }
            else// This is not the first time the user opens the program
            {
                //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                Invoke(new Action(() =>
                {
                    if (tbxPass.Text != "")//validates that something was entered into the textbox
                    {
                        AdminPins flag = null;//flag will be used to find a matching user with that pin
                        foreach (AdminPins a in OwnerStorage.ListOfAdmins)
                        {
                            if (tbxPass.Text.Equals(a.PinCode.ToString()) == true)
                            {
                                flag = a;//a user has been found
                            }
                        }

                        if (flag != null && flag.Active == true)//determines that the user is both valid and active
                        {
                            toggleAdminMode(true);//shows the appropriate controls and features

                            OwnerStorage.AdminLog.Add(new string[] { flag.objectId, System.DateTime.Now.ToString("HH:mm:ss") });

                            OwnerStorage.FileWriter.WriteLineToFile(flag.UserName + " Toggled Admin Mode", true);
                            OwnerStorage.LogInfo.Add(flag.UserName + " activated elevated privileges");
                            OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                        }
                        else//determines that the user is valid, but unactive
                        {
                            lblMessage.Visible = true;
                            if (flag != null)
                                lblMessage.Text = "This user PIN has been deactivated.\nPlease contact the restaurant manager.";
                            else
                                lblMessage.Text = "No user with that PIN could be found.\nPlease try again.";
                        }
                        tbxPass.Text = "";
                        tbxPass.Focus();
                    }
                }));
            }
        }
        private void btnAdmin_Click(object sender, EventArgs e)
        {
            // runs this method, which is multipurpose.
            CheckPin();
        }

        private void toggleAdminMode(bool toggle)
        {
            //this unique method will toggle between admin mode. this enables only the right features to be available when admin mode is activbe
            if (toggle == true)
            {
                btnApply.Visible = true;
                btnCreate.Visible = true;
                btnUpdate.Visible = true;
                btnLogout.Visible = true;
                btnEditRestaurant.Visible = true;
                tbxPass.Visible = false;
                lblLogin.Visible = false;
                btnEnableAdmin.Visible = false;
                btnManageAdminUsers.Visible = false;
                btnEditMenu.Visible = true;
                lblMessage.Visible = false;
                OwnerStorage.AdminMode = true;

                //each RestaurantTableView has a feature to change to a specific color when table.Removable is set to true
                foreach (RestaurantTableView v in pnlMain.Controls)
                {
                    v.Removable = true;
                }
            }
            else
            {
                //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                Invoke(new Action(() =>
                {
                    btnApply.Visible = false;
                    btnCreate.Visible = false;
                    btnUpdate.Visible = false;
                    btnLogout.Visible = false;
                    btnEditRestaurant.Visible = false;
                    tbxPass.Visible = true;
                    lblLogin.Visible = true;
                    btnEnableAdmin.Visible = true;
                    btnManageAdminUsers.Visible = true;
                    btnEditMenu.Visible = false;
                    OwnerStorage.AdminMode = false;

                    //each RestaurantTableView has a feature to change the background colors
                    foreach (RestaurantTableView v in pnlMain.Controls)
                    {
                        v.Removable = false;
                    }
                }));
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //this amazing method starts the seperate SharpUpdate project, which will check for updates, download and install any new instance of the program that may have came out
            OwnerStorage.FileWriter.WriteLineToFile("User Checked for Updates", true);
            updater.DoUpdate();
        }

        private void btnEditRestaurant_Click(object sender, EventArgs e)
        {
            //simple method that creates and shows the EditRestaurantForm
            EditRestaurantForm editForm = new EditRestaurantForm(this);
            DialogResult result = editForm.ShowDialog();
            if (result == DialogResult.OK)//the form closed and return with the OK property because changes were made on the form
            {
                OwnerStorage.FileWriter.WriteLineToFile("User Edited Restaurant Details", true);
                OwnerStorage.LogInfo.Add("User Edited Restaurant Details");
                OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
            }
            //checks if the user made changes to the layout while on that form
            CheckLayoutImage();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            //simple method where the user logs out of the program for what ever reason he/she decides to do so

            DialogResult logout = MessageBox.Show("Are you sure you would like to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (logout == DialogResult.Yes)//the user confirmed with a 'yes'
            {
                AsyncCallback<Object> logoutCallback = new AsyncCallback<Object>(
                user =>
                {
                    //success, the user successfully signed out, the program will now restart
                    //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                    Invoke(new Action(() =>
                    {
                        OwnerStorage.FileWriter.WriteLineToFile("User Logged out", false);
                        OwnerStorage.FileWriter.FormShutDown();
                        Properties.Settings.Default.defaultRestaurant = -1;//resets the default restaurant (if the user has more than 1)
                        Properties.Settings.Default.Save();
                    }));

                    //close the program and restart
                    Application.Restart();
                    Environment.Exit(0);
                },
                fault =>
                {
                    //something went wrong, an error message will now be displayed
                    //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                    Invoke(new Action(() =>
                    {
                        MessageBox.Show(this, "Error: " + fault.Message);
                    }));
                });

                //runs the callback
                Backendless.UserService.Logout(logoutCallback);
            }
        }

        private void btnEditMenu_Click(object sender, EventArgs e)
        {
            //This method will create and show the MenuItemsForm, where the user will manage the interactive menu of the restaurant
            MenuItemsForm editer = new MenuItemsForm();
            DialogResult result = editer.ShowDialog();

            //when the program finishes successfully, the events will be logged
            if (result == DialogResult.OK)
            {
                OwnerStorage.FileWriter.WriteLineToFile("User made changes to Restaurant Menu", true);
                OwnerStorage.LogInfo.Add("User made changes to Restaurant Menu");
                OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            //simply a method which creates and displays a InfoForm, which will act as a digital manual
            InfoForm infoForm = new InfoForm();
            infoForm.ShowDialog();
        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            //simply a method which creates and shows a ReservationsForm on which All Active reservations for the restaurant will be displayed
            ReservationsForm allReservations = new ReservationsForm();
            allReservations.ShowDialog();
        }

        private void tbxPass_TextChanged(object sender, EventArgs e)
        {
            //just a simple method to make the button disabled until something has been typed into the textbox
            if (tbxPass.Text != "")
                btnEnableAdmin.Enabled = true;
            else
                btnEnableAdmin.Enabled = false;
        }

        private void btnChangeLoad_Click(object sender, EventArgs e)
        {
            //this will open a form where the user can change the current load on the restaurant
            RestaurantStatusForm changeStatus = new RestaurantStatusForm();
            DialogResult result = changeStatus.ShowDialog();

            if (result == DialogResult.OK)
            {
                //if a change has been made, the mainForm will than acknowledge the change. 
                UpdateCapacityLabel();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this wonderfull piece of code blocks the "alt F4" capability so that the user can not close the program while a process is running
            if (e.CloseReason == System.Windows.Forms.CloseReason.UserClosing && pboxLoading.Visible==true)
            {
                e.Cancel = true;
            }
        }

        private void btnManageAdminUsers_Click(object sender, EventArgs e)
        {
            //this form will open the ManageAdminUserForm. after which it will return and log in the events of that form and check whether the user added atleast one new pin
            ManageAdminUsersForm NewPin = new ManageAdminUsersForm();
            DialogResult pinResult = NewPin.ShowDialog();
            if (pinResult == DialogResult.OK)
            {
                //logs the event
                OwnerStorage.FileWriter.WriteLineToFile("User Modified Manager PINs", true);
                OwnerStorage.LogInfo.Add("User Modified Manager PINs");
                OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
            }
            if (OwnerStorage.ListOfAdmins.Count != 0)
            {
                //this will most likely only happen the first time the user launches the program. It checks if there is at least one Admin user regestered for this restaurant
                tbxPass.Enabled = true;
            }
            else//else it will keep the textbox unavailable
                tbxPass.Enabled = false;
        }
    }
}