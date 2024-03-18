using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeSwap_Desktop.Model
{
    internal class ItemData
    {
        public  long id { get; set; }
        public  string title { get; set; }
        public  string itemPicture { get; set; }
        public string description { get; set; }
        public  int views { get; set; }
        public  string state { get; set; }
        public  string category { get; set; }
        public  string priceTier { get; set; }
        public  long[] reports { get; set; }
        public  string userID { get; set; }

    }
}
