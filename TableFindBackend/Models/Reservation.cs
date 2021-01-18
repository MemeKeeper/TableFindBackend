using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableFindBackend.Models
{
    public class Reservation
    {
        private string ObjectId;

        private string TableID;

        private DateTime TakenFrom;

        private DateTime TakenTo;

        private string UserID;

        private string Number;

        private string Name;

        private string RestaurantId;


        public string objectId { get => ObjectId; set => ObjectId = value; }
        public string tableId { get => TableID; set => TableID = value; }
        public DateTime takenFrom { get => TakenFrom; set => TakenFrom = value; }
        public DateTime takenTo { get => TakenTo; set => TakenTo = value; }
        public string userId { get => UserID; set => UserID = value; }
        public string number { get => Number; set => Number = value; }
        public string name { get => Name; set => Name = value; }
        public string restaurantId { get => RestaurantId; set => RestaurantId = value; }
    }
}
