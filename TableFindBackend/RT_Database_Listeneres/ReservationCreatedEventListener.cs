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
        private MainForm _masterform;
        public ReservationCreatedEventListener(MainForm masterform)
        {
            _masterform = masterform;

            IEventHandler<Reservation> reservationCreatedEvent = Backendless.Data.Of<Reservation>().RT();

            reservationCreatedEvent = Backendless.Data.Of<Reservation>().RT();

            reservationCreatedEvent.AddCreateListener("restaurantId = '" + OwnerStorage.ThisRestaurant.objectId + "'", createdOrder =>
            {

                Boolean flag = false;
                foreach (Reservation r in OwnerStorage.AllReservations)
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
                                    OwnerStorage.AllReservations.Add(createdOrder);
                                    _masterform.AddOneReservationView(createdOrder);
                                    OwnerStorage.Log.Add("Reservation has been created    : " + System.DateTime.Now.ToString("HH:mm"));
                                    OwnerStorage.Log.Add("Name:  " + createdOrder.name);
                                    if (foundContact.ObjectId == OwnerStorage.CurrentlyLoggedIn.ObjectId)
                                        OwnerStorage.Log.Add("Created By:   Restaurant");
                                    else
                                        OwnerStorage.Log.Add("Created By:   Customer");
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
