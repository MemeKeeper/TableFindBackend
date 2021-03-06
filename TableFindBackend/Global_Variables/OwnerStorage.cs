﻿using BackendlessAPI;
using System;
using System.Collections.Generic;
using TableFindBackend.Logging;
using TableFindBackend.Models;

namespace TableFindBackend.Global_Variables
{
    //This is where all downloaded records are housed. the Global Variables
    static class OwnerStorage
    {
        //properties
        private static TextFileWriter fileWriter;
        private static BackendlessUser currentlyLoggedIn = null;
        private static List<RestaurantTable> restaurantTables;
        private static List<RestaurantMenuItem> menuItems;
        private static List<Reservation> activeReservations;
        private static List<Reservation> pastReservations;
        private static List<BackendlessUser> allUsers;
        private static Restaurant thisRestaurant;
        private static bool adminMode;
        private static List<String> logInfo;
        private static List<String> logTimes;
        private static List<AdminPins> listOfAdmins;
        private static List<String[]> adminLog;

        //getters and setters
        public static BackendlessUser CurrentlyLoggedIn { get => currentlyLoggedIn; set => currentlyLoggedIn = value; }
        public static List<RestaurantTable> RestaurantTables { get => restaurantTables; set => restaurantTables = value; }
        public static Restaurant ThisRestaurant { get => thisRestaurant; set => thisRestaurant = value; }
        public static bool AdminMode { get => adminMode; set => adminMode = value; }
        public static List<RestaurantMenuItem> MenuItems { get => menuItems; set => menuItems = value; }
        public static TextFileWriter FileWriter { get => fileWriter; set => fileWriter = value; }
        public static List<BackendlessUser> AllUsers { get => allUsers; set => allUsers = value; }
        public static List<Reservation> ActiveReservations { get => activeReservations; set => activeReservations = value; }
        public static List<string> LogInfo { get => logInfo; set => logInfo = value; }
        public static List<string> LogTimes { get => logTimes; set => logTimes = value; }
        public static List<Reservation> PastReservations { get => pastReservations; set => pastReservations = value; }
        public static List<AdminPins> ListOfAdmins { get => listOfAdmins; set => listOfAdmins = value; }
        public static List<String[]> AdminLog { get => adminLog; set => adminLog = value; }
    }
}
