using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableFindBackend.Models
{
    public class RestaurantMenuItem
    {
        private string ObjectId;//Exception for Backendless
        private string restaurantId;
        private string name;
        private string type;  //beverege, grill, breakfast, etc
        private string ingredients;
        private double price;
        private Boolean outOfStock;

        public string objectId { get => ObjectId; set => ObjectId = value; }//Exception for Backendless
        public string RestaurantId { get => restaurantId; set => restaurantId = value; }
        public string Name { get => name; set => name = value; }
        public string Type { get => type; set => type = value; }
        public string Ingredients { get => ingredients; set => ingredients = value; }
        public bool OutOfStock { get => outOfStock; set => outOfStock = value; }
        public double Price { get => price; set => price = value; }
    }
}
