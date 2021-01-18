using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableFindBackend.Models
{
    public class RestaurantMenuItem
    {
        private string ObjectId;
        private string RestaurantID;
        private string Name;
        private string Type;  //beverege, grill, breakfast, etc
        private string Ingredients;
        private double Price;
        private Boolean OutOfStock;

        public string objectId { get => ObjectId; set => ObjectId = value; }
        public string restaurantId { get => RestaurantID; set => RestaurantID = value; }
        public string name { get => Name; set => Name = value; }
        public string type { get => Type; set => Type = value; }
        public string ingredients { get => Ingredients; set => Ingredients = value; }
        public bool outOfStock { get => OutOfStock; set => OutOfStock = value; }
        public double price { get => Price; set => Price = value; }
    }
}
