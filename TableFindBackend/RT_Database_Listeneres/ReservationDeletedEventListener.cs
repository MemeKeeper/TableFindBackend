using BackendlessAPI;
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
{
    public class ReservationDeletedEventListener
    {
        IEventHandler<Reservation> reservationDeletedListener;
        private MainForm _master;
        public void RemoveDeletedEventListener()
        {
            reservationDeletedListener.RemoveDeleteListeners();
        }
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
