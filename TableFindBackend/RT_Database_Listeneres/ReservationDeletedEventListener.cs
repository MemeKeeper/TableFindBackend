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
        private MainForm _master;
        public ReservationDeletedEventListener(MainForm master)
        {
            _master = master;
            IEventHandler<Reservation> reservationDeletedListener = Backendless.Data.Of<Reservation>().RT();

            reservationDeletedListener = Backendless.Data.Of<Reservation>().RT();

            reservationDeletedListener.AddDeleteListener("restaurantId = '" + OwnerStorage.ThisRestaurant.objectId + "'", deletedOrder =>
            {

                Reservation tempReservation = null;

                foreach (Reservation r in OwnerStorage.AllReservations)
                {
                    if (r.objectId == deletedOrder.objectId)
                    {
                        tempReservation = r;
                    }
                }

                OwnerStorage.AllReservations.Remove(tempReservation);
                _master.RemoveOneReservationView(deletedOrder);
            });
        }
    
    }
}
