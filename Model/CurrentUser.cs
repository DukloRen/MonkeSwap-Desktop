using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeSwap_Desktop
{
    public class CurrentUser
    {
        public static string userToken { get; set; }
        public string token { get; set; }
        public string username { get; set; }
        public string role { get; set; }
        public int id { get; set; }
        public string email { get; set; }
        public int tradesCompleted { get; set; }
        public string dateOfRegistration { get; set; }
    }
}
