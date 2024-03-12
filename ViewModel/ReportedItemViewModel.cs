using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeSwap_Desktop.ViewModel
{
    public class ReportedItemViewModel : ViewModelBase
    {
        public string ImagePath { get; set; }

        public ReportedItemViewModel()
        {
            ImagePath = "https://i.imgur.com/61LN9Ye.jpeg";
        }
    }
}
