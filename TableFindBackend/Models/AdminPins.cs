namespace TableFindBackend.Models
{
    public class AdminPins
    {
        private bool active;
        private string ObjectId;
        private string contactNumber;
        private int pinCode;
        private string userName;
        private string restaurantId;

        public string objectId { get => ObjectId; set => ObjectId = value; }
        public string ContactNumber { get => contactNumber; set => contactNumber = value; }
        public int PinCode { get => pinCode; set => pinCode = value; }
        public string UserName { get => userName; set => userName = value; }
        public string RestaurantId { get => restaurantId; set => restaurantId = value; }
        public bool Active { get => active; set => active = value; }
    }
}
