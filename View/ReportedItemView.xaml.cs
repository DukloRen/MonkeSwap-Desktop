using MonkeSwap_Desktop.Model;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace MonkeSwap_Desktop.View
{
    /// <summary>
    /// Interaction logic for ReportedItemView.xaml
    /// </summary>
    public partial class ReportedItemView : Window
    {
        private string baseURL = LoginView.baseURL;
        private string token = CurrentUser.userToken;
        private string selectedItemIDGlobal;
        private string selectedItemStateGlobal;
        public ReportedItemView(long selectedItemID)
        {
            InitializeComponent();

            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            selectedItemIDGlobal = selectedItemID.ToString();

            loadSpecificItemData();
        }

        [DllImport("user32.dll")]

        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void pnlControlBar_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        public void switchStateButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedItemStateGlobal == "ENABLED")
            {
                switchState("DISABLED");
            }
            else
            {
                switchState("ENABLED");
            }
            loadSpecificItemData();
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var endpoint = new Uri(baseURL + "admin/item/" + selectedItemIDGlobal);
                var result = client.DeleteAsync(endpoint).Result;
            }
            this.Close();
        }

        private void loadSpecificItemData()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var endpoint = new Uri(baseURL + "admin/item/" + selectedItemIDGlobal);
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;

                ItemData reportedItem = JsonConvert.DeserializeObject<ItemData>(json);

                selectedItemStateGlobal = reportedItem.state;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(reportedItem.itemPicture); ;
                bitmapImage.EndInit();
                pic.ImageSource = bitmapImage;

                idTxt.Text = "ID: " + reportedItem.id.ToString();
                titleTxt.Text = reportedItem.title;
                descriptionTxt.Text = "Description: " + reportedItem.description;
                viewsTxt.Text = "Views: " + reportedItem.views.ToString();
                stateTxt.Text = "State: " + reportedItem.state;
                categoryTxt.Text = "Category: " + reportedItem.category;
                pricetierTxt.Text = "Price tier: " + reportedItem.priceTier;
                reportsTxt.Text = "Reports: " + reportedItem.reports.Count();
                userIDTxt.Text = "User ID: " + reportedItem.userID;
            }
        }

        private void switchState(string notCurrentItemState)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var endpoint = new Uri(baseURL + "admin/item/" + selectedItemIDGlobal);
                var result = client.PutAsync(endpoint, new StringContent(notCurrentItemState)).Result;
            }
        }
    }
}
