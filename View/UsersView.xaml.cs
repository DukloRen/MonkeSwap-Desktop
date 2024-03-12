using MonkeSwap_Desktop.Model;
using MonkeSwap_Desktop.ViewModel;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MonkeSwap_Desktop.View
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class UsersView : System.Windows.Controls.UserControl
    {
        private string baseURL = LoginView.baseURL;
        private string token = CurrentUser.userToken;
        private List<CurrentUser> userList;

        public UsersView()
        {
            InitializeComponent();

            using (var client = new HttpClient())
            {

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var endpoint = new Uri(baseURL + "admin/users");
                    var result = client.GetAsync(endpoint).Result;
                    var json = result.Content.ReadAsStringAsync().Result;

                    userList = JsonConvert.DeserializeObject<List<CurrentUser>>(json);
                    dtGrid.ItemsSource = userList;
            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtFilter.Text!="")
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                        var endpoint = new Uri(baseURL + "admin/user/" + txtFilter.Text);
                        var result = client.GetAsync(endpoint).Result;
                        var json = result.Content.ReadAsStringAsync().Result;

                        CurrentUser userFiltered = JsonConvert.DeserializeObject<CurrentUser>(json);
                        List<CurrentUser> userFilteredList = new List<CurrentUser>() {userFiltered};
                        dtGrid.ItemsSource = userFilteredList;
                    }
                }
                catch (Exception)
                {
                    dtGrid.ItemsSource=null;
                }
            }
            else
            {
                    dtGrid.ItemsSource = userList;
            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser selectedRowObj = dtGrid.SelectedItem as CurrentUser;
            if (selectedRowObj.role == "USER")
            {
                string messageBoxText = "Are you sure you want to promote this User to Admin?";
                string caption = "Promote User";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Warning;
                var msgbox_result = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
                switch (msgbox_result)
                {
                    case MessageBoxResult.Yes:
                        using (var client = new HttpClient())
                        {
                            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                            var endpoint = new Uri(baseURL + "admin/user/" + selectedRowObj.id.ToString());
                            var result = client.PutAsync(endpoint,new StringContent("ADMIN")).Result;
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                }
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var endpoint = new Uri(baseURL + "admin/users");
                    var result = client.GetAsync(endpoint).Result;
                    var json = result.Content.ReadAsStringAsync().Result;
                    

                    userList = JsonConvert.DeserializeObject<List<CurrentUser>>(json);
                    dtGrid.ItemsSource = userList;
                }
            }
            else
            {
                string messageBoxText = "Are you sure you want to demote this Admin to User?";
                string caption = "Demote Admin";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Warning;
                var msgbox_result = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
                switch (msgbox_result)
                {
                    case MessageBoxResult.Yes:
                        using (var client = new HttpClient())
                        {
                            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                            var endpoint = new Uri(baseURL + "admin/user/" + selectedRowObj.id.ToString());
                            var result = client.PutAsync(endpoint, new StringContent("USER")).Result;
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                }
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var endpoint = new Uri(baseURL + "admin/users");
                    var result = client.GetAsync(endpoint).Result;
                    var json = result.Content.ReadAsStringAsync().Result;


                    userList = JsonConvert.DeserializeObject<List<CurrentUser>>(json);
                    dtGrid.ItemsSource = userList;
                }
            }
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser selectedRowObj = dtGrid.SelectedItem as CurrentUser;
            if (selectedRowObj.role=="USER")
            {
                string messageBoxText = "Are you sure you want to delete this user?";
                string caption = "Delete User";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Warning;
                var msgbox_result = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
                switch (msgbox_result)
                {
                    case MessageBoxResult.Yes:
                        using (var client = new HttpClient())
                        {
                            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                            var endpoint = new Uri(baseURL + "admin/user/" + selectedRowObj.id.ToString());
                            var result = client.DeleteAsync(endpoint).Result;
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                }
                using (var client2 = new HttpClient())
                {
                    client2.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var endpoint2 = new Uri(baseURL + "admin/users");
                    var result2 = client2.GetAsync(endpoint2).Result;
                    var json2 = result2.Content.ReadAsStringAsync().Result;

                    userList = JsonConvert.DeserializeObject<List<CurrentUser>>(json2);
                    dtGrid.ItemsSource = userList;
                }
            }
            else
            {
                string messageBoxText = "Error removing this user.";
                string caption = "Error";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }
    }
}
