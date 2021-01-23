﻿using BackendlessAPI;
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

        private string menuLink;

        private string contactNumber;

        private int maxCapacity;

        public string Name { get => name; set => name = value; }
        public string ownerId { get => OwnerId; set => OwnerId = value; }//Exception for Backendless
        public string LocationString { get => locationString; set => locationString = value; }
        public string objectId { get => ObjectId; set => ObjectId = value; }//Exception for Backendless
        public string MenuLink { get => menuLink; set => menuLink = value; }
        public string ContactNumber { get => contactNumber; set => contactNumber = value; }
        public int MaxCapacity { get => maxCapacity; set => maxCapacity = value; }

        public void EditRestaurant()
        {
            AsyncCallback<Restaurant> updateObjectCallback = new AsyncCallback<Restaurant>(
            savedRestaurant =>
            {
                System.Console.WriteLine("Restaurant details has been updated");
            },
            error =>
            {
                //
            });

            AsyncCallback<Restaurant> saveObjectCallback = new AsyncCallback<Restaurant>(
              savedRestaurant =>
              {
                  Backendless.Persistence.Of<Restaurant>().Save(savedRestaurant, updateObjectCallback);
              },
              error =>
              {
                  //
              }
            );

            Backendless.Persistence.Of<Restaurant>().Save(OwnerStorage.ThisRestaurant, saveObjectCallback);
        }
    }
}
