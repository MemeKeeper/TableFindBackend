﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableFindBackend.Models
{
    public class AdminPins
    {
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
    }
}
