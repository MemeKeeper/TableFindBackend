using BackendlessAPI;
using BackendlessAPI.Async;
using BackendlessAPI.RT.Data;
using System;
using TableFindBackend.Forms;
using TableFindBackend.Global_Variables;
using TableFindBackend.Models;

namespace TableFindBackend.RT_Database_Listeneres
{
    //This class houses a Backendless listener, used for when a mobile user creates a reservation while the program is still running
    public class ReservationCreatedEventListener
    {
        IEventHandler<Reservation> reservationCreatedEvent;//Created event handler
        private MainForm _masterform;//An instance of the MainForm where it will visually display the reservations

        //This method is only used when the user reloads the program using the refresh button on the mainForm to prevent duplicate Event listeners
        public void RemoveCreatedEventListener()
        {
            reservationCreatedEvent.RemoveCreateListeners();
        }

        //Constructor for the class
        public ReservationCreatedEventListener(MainForm masterform)
        {
            _masterform = masterform;

            reservationCreatedEvent = Backendless.Data.Of<Reservation>().RT();

            reservationCreatedEvent.AddCreateListener("restaurantId = '" + OwnerStorage.ThisRestaurant.objectId + "'", createdOrder =>
            {
                createdOrder.TakenFrom=createdOrder.TakenFrom.AddHours(2);
                createdOrder.TakenTo= createdOrder.TakenTo.AddHours(2);//<--+2:00 for Time Zone

                Boolean flag = false;
                foreach (Reservation r in OwnerStorage.ActiveReservations)
                {
                    if (createdOrder.objectId == r.objectId)
                        flag = true;
                }
                if (flag != true)
                {
                    AsyncCallback<BackendlessUser> loadContactCallback = new AsyncCallback<BackendlessUser>(
                    foundContact =>
                    {
                        //Success. the reservation will now be added to the flp on the main form
                        if (foundContact != null)
                        {
                            OwnerStorage.AllUsers.Add(foundContact);
                            OwnerStorage.ActiveReservations.Add(createdOrder);
                            _masterform.AddOneReservationView(createdOrder);
                            //Determines if the reservation was made by the restaurant or by a customer
                            if (foundContact.ObjectId == OwnerStorage.CurrentlyLoggedIn.ObjectId)
                            {
                                OwnerStorage.LogInfo.Add("Reservation has been created\nName:   " + createdOrder.Name + "\nCreated By:  Restaurant");
                                OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                            }
                            else
                            {
                                OwnerStorage.LogInfo.Add("Reservation has been created\nName:   " + createdOrder.Name + "\nCreated By:  Customer");
                                OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                            }
                        }
                    },
                    error =>
                    {

                    });
                    //Callback to find the user who created the reservation
                    Backendless.Data.Of<BackendlessUser>().FindById(createdOrder.UserId, loadContactCallback);
                }
            });
        }
    }
}
