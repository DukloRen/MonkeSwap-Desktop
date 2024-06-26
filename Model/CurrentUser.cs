﻿namespace MonkeSwap_Desktop.Model
{
    internal class CurrentUser
    {
        public static long id { get; set; }
        public static string email { get; set; }
        public static string username { get; set; }
        public static string role { get; set; }
        public static int tradesCompleted { get; set; }
        public static string dateOfRegistration { get; set; }
        public static byte[] profilePicture { get; set; }
    }
}
