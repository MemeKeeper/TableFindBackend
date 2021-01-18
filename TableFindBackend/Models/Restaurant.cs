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
        private string ObjectId;

        private string Name;

        private string OwnerId;

        private string LocationString;

        private string MenuLink;

        private string ContactNumber;

        private int MaxCapacity;

        public string name { get => Name; set => Name = value; }
        public string ownerId { get => OwnerId; set => OwnerId = value; }
        public string locationString { get => LocationString; set => LocationString = value; }
        public string objectId { get => ObjectId; set => ObjectId = value; }
        public string menuLink { get => MenuLink; set => MenuLink = value; }
        public string contactNumber { get => ContactNumber; set => ContactNumber = value; }
        public int maxCapacity { get => MaxCapacity; set => MaxCapacity = value; }

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
