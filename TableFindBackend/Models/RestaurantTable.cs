using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableFindBackend.Models
{
    public class RestaurantTable
    {
        private string ObjectId;//Exception for Backendless
        private string restaurantId;
        private bool available;
        private string name;
        private int capacity;
        private double xPos;
        private double yPos;
        private string tableInfo;


        public string objectId//Exception for Backendless
        {
            get { return ObjectId; }
            set { ObjectId = value; }
        }
        public bool Available
        {
            get { return available; }
            set { available = value; }
        }
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }        

        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }
       
        public double XPos
        {
            get { return xPos; }
            set { xPos = value; }
        }
        
        public double YPos
        {
            get { return yPos; }
            set { yPos = value; }
        }

        public string RestaurantId
        {
            get => restaurantId;
            set => restaurantId = value; 
        }
        public string TableInfo 
        {
            get => tableInfo; 
            set => tableInfo = value; 
        }
    }
}
