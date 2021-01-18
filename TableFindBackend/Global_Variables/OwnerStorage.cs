using BackendlessAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TableFindBackend.Logging;
using TableFindBackend.Models;

namespace TableFindBackend.Global_Variables
{
    static class OwnerStorage
    {
        private static TextFileWriter fileWriter;
        private static BackendlessUser currentlyLoggedIn = null;
        private static List<RestaurantTable> restaurantTables;
        private static List<RestaurantMenuItem> menuItems;
        private static List<Reservation> allReservations;
        private static List<BackendlessUser> allUsers;
        private static Restaurant thisRestaurant;
        private static RestaurantTable tempTable;
        private static bool adminMode;
        private static string managerPin;
        private static string eventsLog;
        private static List<String> log;
        public static BackendlessUser CurrentlyLoggedIn 
        {
            get => currentlyLoggedIn; 
            set => currentlyLoggedIn = value; 
        }
        public static List<RestaurantTable> RestaurantTables { get => restaurantTables; set => restaurantTables = value; }
        public static Restaurant ThisRestaurant { get => thisRestaurant; set => thisRestaurant = value; }
        public static RestaurantTable TempTable { get => tempTable; set => tempTable = value; }
        public static bool AdminMode { get => adminMode; set => adminMode = value; }
        public static List<RestaurantMenuItem> MenuItems { get => menuItems; set => menuItems = value; }
        public static string ManagerPin { get => managerPin; set => managerPin = value; }
        public static string EventsLog { get => eventsLog; set => eventsLog = value; }
        public static TextFileWriter FileWriter { get => fileWriter; set => fileWriter = value; }
        public static List<BackendlessUser> AllUsers { get => allUsers; set => allUsers = value; }
        public static List<Reservation> AllReservations { get => allReservations; set => allReservations = value; }
        public static List<string> Log { get => log; set => log = value; }
    }
}
