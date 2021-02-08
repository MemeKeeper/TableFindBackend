using BackendlessAPI;
using BackendlessAPI.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableFindBackend.Global_Variables;

namespace TableFindBackend.Models
{
    public class Restaurant
    {
        private string ObjectId;//Exception for Backendless

        private string name;

        private string OwnerId;//Exception for Backendless

        private string locationString;

        private string contactNumber;

        private int maxCapacity;

        private DateTime open;

        private DateTime close;
        private bool active;

        public string Name { get => name; set => name = value; }
        public string ownerId { get => OwnerId; set => OwnerId = value; }//Exception for Backendless
        public string LocationString { get => locationString; set => locationString = value; }
        public string objectId { get => ObjectId; set => ObjectId = value; }//Exception for Backendless
        public string ContactNumber { get => contactNumber; set => contactNumber = value; }
        public int MaxCapacity { get => maxCapacity; set => maxCapacity = value; }
        public DateTime Open { get => open; set => open = value; }
        public DateTime Close { get => close; set => close = value; }
        public bool Active { get => active; set => active = value; }
    }
}
