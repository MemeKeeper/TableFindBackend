using BackendlessAPI;
using BackendlessAPI.RT.Data;
using TableFindBackend.Forms;
using TableFindBackend.Global_Variables;
using TableFindBackend.Models;

namespace TableFindBackend.RT_Database_Listeneres
{
    //This class houses a Backendless listener, used for the very rare case that a reservation is physically deleted form Backendless
    public class ReservationDeletedEventListener
    {
        //Created event handler and instance of the MainForm
        IEventHandler<Reservation> reservationDeletedListener;
        private MainForm _master;

        //This method is only used when the user reloads the program using the refresh button on the mainForm to prevent duplicate Event listeners
        public void RemoveDeletedEventListener()
        {
            reservationDeletedListener.RemoveDeleteListeners();
        }

        //Constructor for the deleteEventHandler
        public ReservationDeletedEventListener(MainForm master)
        {
            _master = master;
            reservationDeletedListener = Backendless.Data.Of<Reservation>().RT();

            reservationDeletedListener.AddDeleteListener("restaurantId = '" + OwnerStorage.ThisRestaurant.objectId + "'", deletedOrder =>
            {

                Reservation tempReservation = null;

                foreach (Reservation r in OwnerStorage.ActiveReservations)
                {
                    if (r.objectId == deletedOrder.objectId)
                    {
                        tempReservation = r;
                    }
                }

                OwnerStorage.ActiveReservations.Remove(tempReservation);
                OwnerStorage.PastReservations.Add(tempReservation);
                //   _master.RemoveOneReservationView(deletedOrder);
            });
        }

    }
}
