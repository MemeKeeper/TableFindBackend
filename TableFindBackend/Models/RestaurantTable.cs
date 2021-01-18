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
        private string ObjectId;
        private string RestaurantID;
        private bool Available;
        private string Name;
        private int Capacity;
        private int XPos;
        private int YPos;
        private string TableInfo;


        public string objectId
        {
            get { return ObjectId; }
            set { ObjectId = value; }
        }
        public bool available
        {
            get { return Available; }
            set { Available = value; }
        }
        
        public string name
        {
            get { return Name; }
            set { Name = value; }
        }        

        public int capacity
        {
            get { return Capacity; }
            set { Capacity = value; }
        }
       
        public int xPos
        {
            get { return XPos; }
            set { XPos = value; }
        }
        
        public int yPos
        {
            get { return YPos; }
            set { YPos = value; }
        }

        public string restaurantId
        {
            get => RestaurantID;
            set => RestaurantID = value; 
        }
        public string tableInfo 
        {
            get => TableInfo; 
            set => TableInfo = value; 
        }
    }
}
