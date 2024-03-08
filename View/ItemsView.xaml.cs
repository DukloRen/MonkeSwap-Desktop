using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MonkeSwap_Desktop.View
{
    /// <summary>
    /// Interaction logic for ItemsView.xaml
    /// </summary>
    public partial class ItemsView : UserControl
    {
        private string baseURL = LoginView.baseURL;
        private string token = CurrentUser.userToken;
        private List<CurrentUser> itemList;

        public ItemsView()
        {
            InitializeComponent();

            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var endpoint = new Uri(baseURL + "admin/items");
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;

                itemList = JsonConvert.DeserializeObject<List<CurrentUser>>(json);
                dtGrid.ItemsSource = itemList;
            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtFilter.Text != "")
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                        var endpoint2 = new Uri(baseURL + "admin/item/" + txtFilter.Text);
                        var result2 = client.GetAsync(endpoint2).Result;
                        var json2 = result2.Content.ReadAsStringAsync().Result;

                        CurrentUser itemFiltered = JsonConvert.DeserializeObject<CurrentUser>(json2);
                        List<CurrentUser> itemFilteredList = new List<CurrentUser>() { itemFiltered };
                        dtGrid.ItemsSource = itemFilteredList;
                    }
                }
                catch (Exception)
                {
                    dtGrid.ItemsSource = null;
                }
            }
            else
            {
                dtGrid.ItemsSource = itemList;
            }
        }
    }
}
