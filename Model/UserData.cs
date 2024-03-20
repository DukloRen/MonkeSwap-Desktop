using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeSwap_Desktop.Model
{
    internal class UserData
    {
        public static long id { get; set; }
        public static string email { get; set; }
        public static string username { get; set; }
        public static string role { get; set; }
        public static int tradesCompleted { get; set; }
        public static string dateOfRegistration { get; set; }
        public static string profilePicture { get; set; }
    }
}
