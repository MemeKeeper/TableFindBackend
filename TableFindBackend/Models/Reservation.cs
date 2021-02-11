using System;

namespace TableFindBackend.Models
{
    public class Reservation
    {
        private string ObjectId;  //Exception for Backendless

        private string tableId;

        private DateTime takenFrom;

        private DateTime takenTo;

        private string userId;

        private string number;

        private string name;

        private string restaurantId;

        private bool active;

        private string reasonForExpiration;


        public string objectId { get => ObjectId; set => ObjectId = value; }//Exception for Backendless
        public string TableId { get => tableId; set => tableId = value; }
        public DateTime TakenFrom { get => takenFrom; set => takenFrom = value; }
        public DateTime TakenTo { get => takenTo; set => takenTo = value; }
        public string UserId { get => userId; set => userId = value; }
        public string Number { get => number; set => number = value; }
        public string Name { get => name; set => name = value; }
        public string RestaurantId { get => restaurantId; set => restaurantId = value; }
        public bool Active { get => active; set => active = value; }
        public string ReasonForExpiration { get => reasonForExpiration; set => reasonForExpiration = value; }
    }
}
