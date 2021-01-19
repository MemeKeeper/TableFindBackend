using BackendlessAPI;
using BackendlessAPI.Async;
using BackendlessAPI.RT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableFindBackend.Forms;
using TableFindBackend.Global_Variables;
using TableFindBackend.Models;

namespace TableFindBackend.RT_Database_Listeneres

{   public class ReservationCreatedEventListener
    {
        IEventHandler<Reservation> reservationCreatedEvent;
        private MainForm _masterform;
        public void RemoveCreatedEventListener()
        {
            reservationCreatedEvent.RemoveCreateListeners();
        }
        public ReservationCreatedEventListener(MainForm masterform)
        {
            _masterform = masterform;

            reservationCreatedEvent = Backendless.Data.Of<Reservation>().RT();

            reservationCreatedEvent.AddCreateListener("restaurantId = '" + OwnerStorage.ThisRestaurant.objectId + "'", createdOrder =>
            {

                Boolean flag = false;
                foreach (Reservation r in OwnerStorage.ActiveReservations)
                {
                    if (createdOrder.objectId == r.objectId)
                        flag = true;
                }
                if (flag != true)
                {
                    RestaurantTable temp = null;

                    foreach (RestaurantTable table in OwnerStorage.RestaurantTables)
                    {
                        if (table.objectId == createdOrder.tableId)
                        {
                            temp = table;
                        }
                    }
                    AsyncCallback<BackendlessUser> loadContactCallback = new AsyncCallback<BackendlessUser>(
                            foundContact =>
                            {
                                if (foundContact != null)
                                {
                                    OwnerStorage.AllUsers.Add(foundContact);
                                    OwnerStorage.ActiveReservations.Add(createdOrder);
                                    _masterform.AddOneReservationView(createdOrder);
                                    if (foundContact.ObjectId == OwnerStorage.CurrentlyLoggedIn.ObjectId)
                                    {
                                        OwnerStorage.LogInfo.Add("Reservation has been created\nName:   " + createdOrder.name + "\nCreated By:  Restaurant");
                                        OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                                    }
                                    else
                                    {
                                        OwnerStorage.LogInfo.Add("Reservation has been created\nName:   " + createdOrder.name + "\nCreated By:  Customer");
                                        OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                                    }
                                }
                            },
                            error =>
                            {

                            });
                    Backendless.Data.Of<BackendlessUser>().FindById(createdOrder.userId, loadContactCallback);
                }
            });
        }
    }
}
