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
    //This is the MainForm where all the processes and actions are to happen. Most complex procedures will occur here
    public partial class MainForm : Form
    {
        //Properties which are necessary to move the RestaurantTableViews around
        private Control activeControl;
        private Point previousPosition;
        //The updater object used to update the program
        private SharpUpdater updater;

        //These properties are for the created and deleted listeners. They give the program the ability to detect changes in the database in real time
        private ReservationCreatedEventListener createdListener;
        private ReservationDeletedEventListener deletedListener;

        //The timer that checks if a reservation has expired
        private static System.Timers.Timer removeTimer;

        public MainForm()
        {
            //All properties are instantiated
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
                //Completely closes the application
                System.Environment.Exit(1); 
                OwnerStorage.FileWriter.WriteLineToFile("User Failed to Login", false);
                OwnerStorage.FileWriter.FormShutDown();

            }

            //All Application variables are now instantiated
            OwnerStorage.AdminMode = false;
            OwnerStorage.RestaurantTables = new List<RestaurantTable>();
            OwnerStorage.MenuItems = new List<RestaurantMenuItem>();
            OwnerStorage.ActiveReservations = new List<Reservation>();
            OwnerStorage.PastReservations = new List<Reservation>();
            OwnerStorage.AllUsers = new List<BackendlessUser>();
            OwnerStorage.AdminLog = new List<String[]>();
            OwnerStorage.ListOfAdmins = new List<AdminPins>();

            //All Async methods are executed, whose main goals are to retrieve all relevant information from the database            
            this.Hide();
            InitializeComponent();
            PopulateTables();
            CheckLayoutImage();
            UpdateCapacityLabel();
            RetrieveAdminPins();
            PerformBackgroundMenuItemPopulation();

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            //Logs to the TextFile that the user has successfully logged in
            OwnerStorage.LogInfo.Add("User Logged in with valid Login");
            OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));

            //Sets up the updater object with the appropriate link
            updater = new SharpUpdater(Assembly.GetExecutingAssembly(), this, new Uri("https://backendlessappcontent.com/6D59291D-64B4-B4E5-FFCD-43BA19198A00/3DD4C6AA-9D71-4111-A769-45F6DEE35B62/files/update/update.xml"));  //<------ Domain on which we host the update for the program

            //Ensures that the form remains smooth
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, pnlMain, new object[] { true });
        }

        //Gets all AdminPIN objects for this specific restaurant
        private void RetrieveAdminPins()
        {
            String whereClause = "restaurantId = '" + OwnerStorage.ThisRestaurant.objectId + "'";
            BackendlessAPI.Persistence.DataQueryBuilder queryBuilder = BackendlessAPI.Persistence.DataQueryBuilder.Create();
            queryBuilder.SetWhereClause(whereClause);

            AsyncCallback<IList<AdminPins>> findCallback;
            findCallback = new AsyncCallback<IList<AdminPins>>(
              foundObjects =>
              {
                  //Success, all Admin PINS have been retrieved
                  OwnerStorage.ListOfAdmins = (List<AdminPins>)foundObjects;
                  OwnerStorage.LogInfo.Add("Admin PINS have been retrieved.");
                  OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                  //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                  Invoke(new Action(() =>
                  {
                      tbxPass.Enabled = true;
                  }));
                  CheckPin();
              },
              error =>
              {
                  //Something went wrong, OR there are no AdminPINS for this restaurant
                  OwnerStorage.LogInfo.Add("Failed to retrieve Admin PINS.");
                  OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                  CheckPin();
              });

            Backendless.Data.Of<AdminPins>().Find(queryBuilder, findCallback);
        }

        //Checks the current setting of the capacity property and sets the label text
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

        //A method used to setup the timer settings
        private void InitializeTimer()
        {
            removeTimer = new System.Timers.Timer();
            removeTimer.Interval = 15000;//<-- every 15 seconds
            removeTimer.Elapsed += OnTimedEvent;
            removeTimer.AutoReset = true;
            removeTimer.Enabled = true;
        }

        //The timer event checks all reservations every 15 seconds and removes reservations which expire
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            foreach (Reservation r in OwnerStorage.ActiveReservations)
            {
                if (r.TakenTo < System.DateTime.Now)
                {
                    AsyncCallback<Reservation> updateObjectCallback = new AsyncCallback<Reservation>(
                    savedReservation =>
                    {
                        //Success. The object has been deactivated and moved to the flpPastReservations tab
                        OwnerStorage.LogInfo.Add("Reservation has Expired\nName:  " + savedReservation.Name);
                        OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                        //This method moves it to the PastReservations tab
                        RemoveOneReservationView(r, savedReservation);
                    },
                    error =>
                    {
                    });

                    AsyncCallback<Reservation> saveObjectCallback = new AsyncCallback<Reservation>(
                    savedReservation =>
                    {
                        //Updates the saved object
                        savedReservation.Active = false;
                        savedReservation.ReasonForExpiration = "Reservation has passed it's expiration date";
                        Backendless.Persistence.Of<Reservation>().Save(savedReservation, updateObjectCallback);
                    },
                    error =>
                    {
                    });

                    //Runs the save callback
                    Backendless.Persistence.Of<Reservation>().Save(r, saveObjectCallback);
                }
            }
        }//Come back

        //A method that actively checks whether the layout image has been changed
        private void CheckLayoutImage()
        {
            string file = @"layouts\" + OwnerStorage.ThisRestaurant.objectId + "_layout.tbl";
            if (File.Exists(file) == true)
            {
                pnlMain.BackgroundImage = Image.FromFile(file);
                //Garbage collector for 'deactivating' the active layout image
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            else
            {
                pnlMain.BackgroundImage = null;
            }
        }

        //Disposes the active layout image
        public void DisableLayoutImage()
        {
            if (pnlMain.BackgroundImage != null)
                pnlMain.BackgroundImage.Dispose();
        }

        //A method which calculates the dimensions of the panel, so that the RestaurantTableViews can be correctly displayed
        private double[] CalcPanelRes()
        {
            double[] result;
            result = new double[] { pnlMain.Bounds.Width, pnlMain.Bounds.Height };

            return result;
        }

        //The method that simulates a loading screen when enabled
        private void ShowLoading(bool enable)
        {
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

        //This method will move the ReservationView from the active flp to the past flp
        public void RemoveOneReservationView(Reservation oldR, Reservation newR)
        {
            ReservationView tempReservation = null;
            bool flag = false;
            //Determines which ReservationView has to be moved
            foreach (ReservationView view in flpItems.Controls)
            {
                if (view.Tag.ToString() == oldR.objectId.ToString())
                {
                    flag = true;
                    tempReservation = view;
                    //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                    Invoke(new Action(() =>
                    {
                        //This will remove the old reservationView and add the new one to the pastReservations
                        OwnerStorage.ActiveReservations.Remove(oldR);
                        OwnerStorage.PastReservations.Add(newR);
                        tempReservation.pnlContact.MouseClick += new MouseEventHandler(pastReservation_Click);
                        tempReservation.pnlReservation.MouseClick += new MouseEventHandler(pastReservation_Click);
                        flpPrevious.Controls.Add(tempReservation);
                        tempReservation.Removed();
                        
                        //Adds the ReservationView to the top of the flp
                        flpPrevious.Controls.SetChildIndex(tempReservation, 0);
                        flpItems.Controls.Remove(tempReservation);
                    }));
                }
                //In the rare event that the click event does not work, it has to be logged
                if (flag == false)
                {
                    OwnerStorage.FileWriter.WriteLineToFile("No Reservation View (Icon of the reservation) matching the given ID could be found on the panel", true);
                    OwnerStorage.LogInfo.Add("No Reservation View (Icon of the reservation) matching the given ID could be found on the panel");
                    OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                }
            }
        }

        // Controller for ReservationView which adds one ReservationView to the ActiveReservation flp
        public void AddOneReservationView(Reservation r) 
        {
            ReservationView reservation = new ReservationView();
            RestaurantTable table = new RestaurantTable();
            foreach (RestaurantTable t in OwnerStorage.RestaurantTables)
            {
                if (t.objectId == r.TableId)
                    table = t;
            }

            //If the RestuarantTable does not exist
            if(table==null)
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
            
            //Sets the reservationView properties
            reservation.Tag = r.objectId;
            reservation.UserName = r.Name;
            reservation.UObjectId = r.UserId;
            reservation.UserContactNumber = r.Number;
            reservation.TableName = "Table: " + table.Name;
            reservation.ObjectId = r.objectId;
            reservation.Date = r.TakenFrom.ToString("ddd, dd / MM");
            reservation.FromToTime = r.TakenFrom.ToString("HH:mm") + " to " + r.TakenTo.ToString("HH:mm");

            //Adds the on-click events
            reservation.pnlContact.MouseClick += new MouseEventHandler(activeReservation_Click);
            reservation.pnlReservation.MouseClick += new MouseEventHandler(activeReservation_Click);
            //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
            Invoke(new Action(() =>
            {
                flpItems.Controls.Add(reservation);
                flpItems.Controls.SetChildIndex(reservation, 0);
                reservation.New();
            }));
        }
        //This method populates both flp with active and unactive reservationViews
        private void PerformReservationViewListPopulation(List<Reservation> list)
        {
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

        //An on-click event for the flpItems panel's controls (Active Reservations)
        private void activeReservation_Click(object sender, MouseEventArgs e)
        {
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
                    //In the rare case that a user could not be found (removed manually from the database), a blank BackendlessUser object is created
                    if (tempUser==null)
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
            //No RervationView matching the given ID could be found
            if (flag == false)
            {
                OwnerStorage.FileWriter.WriteLineToFile("No Reservation View (Icon of the reservation) matching the given ID could be found on the panel", true);
                OwnerStorage.LogInfo.Add("No Reservation View (Icon of the reservation) matching the given ID could be found on the panel");
                OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
            }
        }

        //An on-click event for the flpItems panel's controls (Past Reservations)
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
                    //In the rare case that a user could not be found (removed manually from the database), a blank BackendlessUser object is created
                    if (tempUser == null)
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
            //No RervationView matching the given ID could be found
            if (flag == false)
            {
                OwnerStorage.FileWriter.WriteLineToFile("No Reservation View (Icon of the reservation) matching the given ID could be found on the panel", true);
                OwnerStorage.LogInfo.Add("No Reservation View (Icon of the reservation) matching the given ID could be found on the panel");
                OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
            }
        }

        //This method will download all currently saved MenuItems from Backendless
        private void PerformBackgroundMenuItemPopulation()
        {
            String whereClause = "restaurantID = '" + OwnerStorage.ThisRestaurant.objectId + "'";
            BackendlessAPI.Persistence.DataQueryBuilder queryBuilder = BackendlessAPI.Persistence.DataQueryBuilder.Create();
            queryBuilder.SetWhereClause(whereClause);
            queryBuilder.SetPageSize(100);

            AsyncCallback<IList<RestaurantMenuItem>> getBookingCallback = new AsyncCallback<IList<RestaurantMenuItem>>(

            foundRestaurantMenuItem =>
            {
                //Success, the event will be logged
                OwnerStorage.MenuItems = (List<RestaurantMenuItem>)foundRestaurantMenuItem;
                OwnerStorage.FileWriter.WriteLineToFile("Online interactive menu has been downloaded", true);
            },
            error =>
            {
                //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                //Something went wrong, an error message will be displayed
                Invoke(new Action(() =>
                {
                    MessageBox.Show(this, "Error: " + error.Message);
                    OwnerStorage.FileWriter.WriteLineToFile("Online interactive menu failed to download", true);
                }));
            });
            //Runs the callback
            Backendless.Data.Of<RestaurantMenuItem>().Find(queryBuilder, getBookingCallback);
        }

        //This method will download the latest 100 reservations from the Backendless database
        private void PerformBackgroundReservationPopulation()
        {
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
                //Success, reservations have been downloaded
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

                //Makes sure that there are reservations for this restaurant
                if (foundReservations.Count != 0)
                {
                    int i = 1;
                    //for each reservation the corrosponding user object will be downloaded
                    foreach (Reservation r in foundReservations)
                    {
                        AsyncCallback<BackendlessUser> loadContactCallback = new AsyncCallback<BackendlessUser>(
                        foundContact =>
                        {
                            //Success, the user will now be added to a list
                            OwnerStorage.AllUsers.Add(foundContact);
                            if (i == foundReservations.Count)
                            //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
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
                                //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread                                
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
                //No reservations for this restaurant were found. A message will now display
                else
                {
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
                //Something went wrong, an error message will be displayed
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

        // Controller for RestaurantTableView which will generate the RestaurantTableView(s)
        private void PopulateTables() 
        {
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
                          //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                          Invoke(new Action(() =>
                             {
                                 lblPnlSize.Text = "Recommended floor layout image size: " + CalcPanelRes()[0] + " x " + CalcPanelRes()[1];
                             }));

                          //For each RestaurantTable which was downloaded, a RestaurantTableView must be generated
                          foreach (RestaurantTable tb in OwnerStorage.RestaurantTables)
                          {
                              //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
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
                          //Something went wrong, an error message will now display
                          //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                          Invoke(new Action(() =>
                          {
                              MessageBox.Show(this, "Error: " + error.Message);
                              OwnerStorage.FileWriter.WriteLineToFile("Tables downloading failed", true);
                              OwnerStorage.LogInfo.Add("Tables downloading failed");
                              OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                              ShowLoading(false);
                          }));
                      });

            //Runs the callback
            Backendless.Data.Of<RestaurantTable>().Find(queryBuilder, getTableItemCallback);
        }

        //This method will prep everything needed to generate a new RestaurantTable and a new RestaurantTableView
        private void btnCreate_Click(object sender, EventArgs e)
        {
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
            //A reused method which defines all the events and handles for the RestaurantTableView
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

        //This method is used when the user double-clicks on the restaurantTableView. Depending on whether AdminMode is active, it should open a specific form
        private void MyControl_DoubleClick(object sender, EventArgs e)
        {
            RestaurantTableView tempItem;
            //Determines what the user double-clicked on and converts it to the same object
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

            //Determines which RestaurantTable the user is referring to by checking which View has the same tag as the RestaurantTable ObjectId
            foreach (RestaurantTable ti in OwnerStorage.RestaurantTables)
            {
                if (ti.objectId == tempItem.Tag)
                {
                    tempTable = ti;
                }
            }
            //If the user is in Admin mode, it will display the table editer form
            if (OwnerStorage.AdminMode == true)
            {
                EditTableForm editor = new EditTableForm(tempTable, this);
                DialogResult result = editor.ShowDialog();
                tempTable = editor.RetreiveEditedTable();

                //Removes the Table
                if (result == DialogResult.Yes) 
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
                //When the user creates a new table but cancels the process
                if (result == DialogResult.Cancel) 
                {
                    if (tempTable.objectId == null)
                    {
                        pnlMain.Controls.Remove(tempItem);
                        OwnerStorage.RestaurantTables.Remove(tempTable);
                    }
                }
                //Updates the Table
                if (result == DialogResult.OK) 
                {
                    OwnerStorage.RestaurantTables.Remove(tempTable);
                    Point tempPoint = new Point(0, 0);
                    if (sender is Label)
                    {
                        Label tempLabel = (Label)sender;
                        RestaurantTableView tempSenderView = (RestaurantTableView)tempLabel.Parent;
                        //Foreach to iterate through the code to capture the location of the control being edited
                        foreach (RestaurantTableView t in pnlMain.Controls) 
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
                        //Foreach to iterate through the code to capture the location of the control being edited
                        foreach (RestaurantTableView t in pnlMain.Controls) 
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

                    OwnerStorage.RestaurantTables.Add(tempTable);

                    AddControl(tempView);

                    OwnerStorage.FileWriter.WriteLineToFile("User Updated Table", true);
                    OwnerStorage.FileWriter.WriteLineToFile("Name:  " + tempView.Label, false);
                    OwnerStorage.LogInfo.Add("User Updated Table\nName:  " + tempItem.Label);
                    OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                }
            }
            //If the user is not in Admin mode, he/she will be directly taken to the reservationsForm
            else
            {
                ReservationsForm reservationsForm = new ReservationsForm(tempTable, this);
                reservationsForm.ShowDialog();
            }
        }

        //Method for when the user deselects the restaurantTableView
        private void MyControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (OwnerStorage.AdminMode == true)
            {
                activeControl = null;
                Cursor = Cursors.Default;
            }
        }

        //While the user holds down the Table Controls he/she can drag them around using this method
        private void MyControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (OwnerStorage.AdminMode == true)
            {
                //Makes sure that no matter where the user clicks it will register the correct control
                if (sender is Label)
                {
                    Label tempLabel = (Label)sender;
                    RestaurantTableView tempView = (RestaurantTableView)tempLabel.Parent;

                    if (activeControl == null || activeControl != tempView)
                    {
                        return;
                    }
                    var lbllocation = activeControl.Location;

                    //Applies the new location to the control
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

                    //Applies the new location to the control
                    location.Offset(e.Location.X - previousPosition.X, e.Location.Y - previousPosition.Y);
                    activeControl.Location = location;
                }
                //Ensures that the user cannot place a RestaurantTableView outside of the Main Panel
                if ((activeControl.Location.Y + 40) > pnlMain.Height)
                {
                    activeControl.Location = new Point(activeControl.Location.X, pnlMain.Height - 40);
                }
                if ((activeControl.Location.X + 73) > pnlMain.Width)
                {
                    activeControl.Location = new Point(pnlMain.Width - 73, activeControl.Location.Y);
                }
                if (activeControl.Location.Y < 0)
                {
                    activeControl.Location = new Point(activeControl.Location.X, 0);
                }
                if (activeControl.Location.X < 0)
                {
                    activeControl.Location = new Point(0, activeControl.Location.Y);
                }
            }
        }

        //Method used to hold down the Table Controls in order to drag them around
        private void MyControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks >= 2)
            {
                MyControl_DoubleClick(sender, e);
                return;
            }

            //Only Admin users are allowed to move table objects
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

        //Method that closes the program after documenting the event in the logs
        private void btnExit_Click(object sender, EventArgs e)
        {
            OwnerStorage.FileWriter.FormShutDown();
            this.Close();
        }

        //This method is used to finalize all changes made while the program was in Admin mode and also deactivates Admin mode
        //It is crucial that no network problems occur during this process, because if something goes wrong in the inner async task all the RestaurantTables (including the ones that already existed) will be permenantly lost
        private void btnApply_Click(object sender, EventArgs e)
        {
            ShowLoading(true);
            int i = 0;
            //Ensures that the locations of the RestaurantTableViews can be used on screens of different sizes
            foreach (RestaurantTable tb in OwnerStorage.RestaurantTables)
            {
                tb.YPos = pnlMain.Controls[i].Location.Y / Convert.ToDouble(pnlMain.Height);
                tb.XPos = pnlMain.Controls[i].Location.X / Convert.ToDouble(pnlMain.Width);
                i++;
            }

            AsyncCallback<int> bulkDeleteCallback = new AsyncCallback<int>(
        objectsDeleted =>
        {
            //Success, all tables on Backendless for this restaurant have been removed. Now they can be reuploaded in bulk with the new changes made
            AsyncCallback<IList<string>> bulkCreateCallback = new AsyncCallback<IList<string>>(
            result =>
            {
                //Save as a list. All RestaurantTables are reuploaded with changes applied
                //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                Invoke(new Action(() =>
                {
                    OwnerStorage.FileWriter.WriteLineToFile("User Finilized changes to restaurant", true);
                    OwnerStorage.FileWriter.WriteLineToFile("User deactivated Admin Mode", true);

                    OwnerStorage.LogInfo.Add("User Applied changes to restaurant layout \nUser deactivated Admin Mode");
                    OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));

                    //Relaunches
                    ShowLoading(false);

                    //Removed these lines for performance reasons, but it makes the interface more stable
                    //createdListener.RemoveCreatedEventListener();
                    //deletedListener.RemoveDeletedEventListener();
                    //PopulateTables();
                }));

                //Deactivated Admin mode
                toggleAdminMode(false);
            },
            error => 
            {
                //Something went wrong during deletion of the tables. It is possible that all RestaurantTable information has been lost. An error message will display.
                //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
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

            //Starts the bulk create callback
            Backendless.Data.Of<RestaurantTable>().Create(OwnerStorage.RestaurantTables, bulkCreateCallback);
        },
        error =>
        {
            //Something went wrong during deletion of the tables. It is possible that all tables are still intact and that the user will just have to make the changes again. An error message will display.
            //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
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
            //In order to update in bulk, all tables must first be deleted and then recreated with the existing objectIDs
            Backendless.Data.Of<RestaurantTable>().Remove("restaurantId = '" + OwnerStorage.ThisRestaurant.objectId + "'", bulkDeleteCallback);
        }

        //Refresh button which redownloads all objects in the rare case of network failure during startup
        private void btnReloadAll_Click(object sender, EventArgs e)
        {
            //Both listeners have to be removed to stop the program from creating duplicates
            createdListener.RemoveCreatedEventListener();
            deletedListener.RemoveDeletedEventListener();
            PopulateTables();
            RetrieveAdminPins();

            PerformBackgroundMenuItemPopulation();
        }

        //This method has multiple functions: It checks the PIN which the user entered, it checks if it's the first time the uses the program and it also
        //detects if this is the first time the user logs in, if so the user wil then be taken through the proccess of setting up the restaurant for first time user
        private void CheckPin()
        {
            if (OwnerStorage.ListOfAdmins.Count == 0)
            {
                //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                Invoke(new Action(() =>
                {
                    //Message stating that it is his first time using the program
                    DialogResult result = MessageBox.Show(this, "Seems like this is your first time logging in to the " +
                        "TableFindBackend Desktop App with this restaurant, therefore " +
                        "you have not yet registered an Admin PIN. Without it " +
                        "you will not be able to have access to Admin capabilities." +
                        "\nWould you like to configure an Admin PIN now?", "Admin PIN", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    //User said yes
                    if (result == DialogResult.Yes)
                    {
                        ManageAdminUsersForm NewPin = new ManageAdminUsersForm();
                        NewPin.ShowDialog();
                    }
                    //User said no
                    else if (result == DialogResult.No)
                    {
                        OwnerStorage.FileWriter.WriteLineToFile("User Denied First-time Admin PIN Setup", true);
                    }
                }));

            }
            // This is not the first time the user opens the program
            else
            {
                //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                Invoke(new Action(() =>
                {
                    //validates if something was entered into the textbox
                    if (tbxPass.Text != "")
                    {
                        //flag will be used to find a matching user with that PIN
                        AdminPins flag = null;
                        foreach (AdminPins a in OwnerStorage.ListOfAdmins)
                        {
                            if (tbxPass.Text.Equals(a.PinCode.ToString()) == true)
                            {
                                //A user has been found
                                flag = a;
                            }
                        }
                        //Determines if the user is both valid and active
                        if (flag != null && flag.Active == true)
                        {
                            //Shows the appropriate controls and features
                            toggleAdminMode(true);

                            OwnerStorage.AdminLog.Add(new string[] { flag.objectId, System.DateTime.Now.ToString("HH:mm:ss") });

                            OwnerStorage.FileWriter.WriteLineToFile(flag.UserName + " Toggled Admin Mode", true);
                            OwnerStorage.LogInfo.Add(flag.UserName + " activated elevated privileges");
                            OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                        }
                        //Determines if the user is valid, but unactive
                        else
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
        //Runs the multipurpose method CheckPin()
        private void btnAdmin_Click(object sender, EventArgs e)
        {
            CheckPin();
        }

        //Toggles between Admin mode. This enables only the right features to be available when Admin mode is active
        private void toggleAdminMode(bool toggle)
        {
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

                //Each RestaurantTableView has a feature to change to a specific color when table.Removable is set to true
                foreach (RestaurantTableView v in pnlMain.Controls)
                {
                    v.Removable = true;
                }
            }
            else
            {
                //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
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

                    //Each RestaurantTableView has a feature to change the background colors
                    foreach (RestaurantTableView v in pnlMain.Controls)
                    {
                        v.Removable = false;
                    }
                }));
            }
        }

        //This method starts the seperate SharpUpdate project, which will check for updates, download and install any new instance of the program that may have been released
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            OwnerStorage.FileWriter.WriteLineToFile("User Checked for Updates", true);
            updater.DoUpdate();
        }

        //Method that creates and shows the EditRestaurantForm
        private void btnEditRestaurant_Click(object sender, EventArgs e)
        {
            EditRestaurantForm editForm = new EditRestaurantForm(this);
            DialogResult result = editForm.ShowDialog();
            //The form closes and returns with the OK property because changes were made on the form
            if (result == DialogResult.OK)
            {
                OwnerStorage.FileWriter.WriteLineToFile("User Edited Restaurant Details", true);
                OwnerStorage.LogInfo.Add("User Edited Restaurant Details");
                OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
            }
            //Checks if the user made changes to the layout while on that form
            CheckLayoutImage();
        }

        //Method where the user logs out of the program
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult logout = MessageBox.Show("Are you sure you would like to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            //The user confirmed with a 'yes'
            if (logout == DialogResult.Yes)
            {
                AsyncCallback<Object> logoutCallback = new AsyncCallback<Object>(
                user =>
                {
                    //Success, the user successfully signed out, the program will now restart
                    //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                    Invoke(new Action(() =>
                    {
                        OwnerStorage.FileWriter.WriteLineToFile("User Logged out", false);
                        OwnerStorage.FileWriter.FormShutDown();
                        //Resets the default restaurant (if the user has more than 1)
                        Properties.Settings.Default.defaultRestaurant = -1;
                        Properties.Settings.Default.Save();
                    }));

                    //Close the program and restart
                    Application.Restart();
                    Environment.Exit(0);
                },
                fault =>
                {
                    //Something went wrong, an error message will now be displayed
                    //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                    Invoke(new Action(() =>
                    {
                        MessageBox.Show(this, "Error: " + fault.Message);
                    }));
                });

                //Runs the callback
                Backendless.UserService.Logout(logoutCallback);
            }
        }

        //This method will create and show the MenuItemsForm, where the user will manage the interactive menu of the restaurant
        private void btnEditMenu_Click(object sender, EventArgs e)
        {
            MenuItemsForm editer = new MenuItemsForm();
            DialogResult result = editer.ShowDialog();

            //When the program finishes successfully, the events will be logged
            if (result == DialogResult.OK)
            {
                OwnerStorage.FileWriter.WriteLineToFile("User made changes to Restaurant Menu", true);
                OwnerStorage.LogInfo.Add("User made changes to Restaurant Menu");
                OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
            }
        }

        //Method which creates and displays an InfoForm, which will act as a digital manual
        private void btnInfo_Click(object sender, EventArgs e)
        {
            InfoForm infoForm = new InfoForm();
            infoForm.ShowDialog();
        }

        //Method which creates and shows a ReservationsForm on which all Active reservations for the restaurant will be displayed
        private void btnViewAll_Click(object sender, EventArgs e)
        {
            ReservationsForm allReservations = new ReservationsForm();
            allReservations.ShowDialog();
        }

        //Method to make the button disabled until something has been typed into the textbox
        private void tbxPass_TextChanged(object sender, EventArgs e)
        {
            if (tbxPass.Text != "")
                btnEnableAdmin.Enabled = true;
            else
                btnEnableAdmin.Enabled = false;
        }

        //This will open a form where the user can change the current capacity status of the restaurant
        private void btnChangeLoad_Click(object sender, EventArgs e)
        {
            RestaurantStatusForm changeStatus = new RestaurantStatusForm();
            DialogResult result = changeStatus.ShowDialog();

            //If a change has been made, the mainForm will than acknowledge the change
            if (result == DialogResult.OK)
            { 
                UpdateCapacityLabel();
            }
        }

        //Blocks the "alt F4" capability so that the user can not close the program while a process is running
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == System.Windows.Forms.CloseReason.UserClosing && pboxLoading.Visible==true)
            {
                e.Cancel = true;
            }
        }

        //This form will open the ManageAdminUserForm, after which it will return and log in the events of that form and check whether the user added at least one new PIN
        private void btnManageAdminUsers_Click(object sender, EventArgs e)
        { 
            ManageAdminUsersForm NewPin = new ManageAdminUsersForm();
            DialogResult pinResult = NewPin.ShowDialog();
            if (pinResult == DialogResult.OK)
            {
                //Logs the event
                OwnerStorage.FileWriter.WriteLineToFile("User Modified Admin PINs", true);
                OwnerStorage.LogInfo.Add("User Modified Admin PINs");
                OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
            }
            //This will most likely only happen the first time the user launches the program. It checks if there is at least one Admin user registered for this restaurant
            if (OwnerStorage.ListOfAdmins.Count != 0)
            {
                tbxPass.Enabled = true;
            }
            //Else it will keep the textbox unavailable
            else
                tbxPass.Enabled = false;
        }
    }
}