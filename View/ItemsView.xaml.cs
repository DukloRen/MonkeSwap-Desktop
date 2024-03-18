using MonkeSwap_Desktop.Model;
using MonkeSwap_Desktop.ViewModel;
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
        private List<ItemData> itemList;

        public event EventHandler<string> ValuePassed;

        public ItemsView()
        {
            InitializeComponent();

            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var endpoint = new Uri(baseURL + "admin/items");
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;

                itemList = JsonConvert.DeserializeObject<List<ItemData>>(json);
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

                        ItemData itemFiltered = JsonConvert.DeserializeObject<ItemData>(json2);
                        List<ItemData> itemFilteredList = new List<ItemData>() { itemFiltered };
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

        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            ItemData selectedRowObj = dtGrid.SelectedItem as ItemData;
            long selectedItemID = selectedRowObj.id;
            string selectedItemTitle = selectedRowObj.title;
            string selectedItemPicture = selectedRowObj.itemPicture;
            string selectedItemDescription = selectedRowObj.description;
            int selectedItemViews = selectedRowObj.views;
            string selectedItemState = selectedRowObj.state;
            string selectedItemCategory = selectedRowObj.category;
            string selectedItemPriceTier = selectedRowObj.priceTier;
            long[] selectedItemReports = selectedRowObj.reports;
            string selectedItemUserID = selectedRowObj.userID;
            ReportedItemView reportedItem = new ReportedItemView(selectedItemID, selectedItemTitle, selectedItemPicture, selectedItemDescription, selectedItemViews, selectedItemState, selectedItemCategory, selectedItemPriceTier, selectedItemReports, selectedItemUserID);
            reportedItem.Show();
        }
    }
}
