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
        private string resultGlobal;

        public UsersView()
        {
            InitializeComponent();

            loadData();
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            filteredLoadData();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser selectedRowObj = dtGrid.SelectedItem as CurrentUser;
            if (selectedRowObj.role == "USER")
            {
                switchRole("ADMIN", "Are you sure you want to promote this User to Admin?", "Promote User");  
            }
            else
            {
                switchRole("USER", "Are you sure you want to demote this Admin to User?", "Demote Admin");
            }
            filteredLoadData();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser selectedRowObj = dtGrid.SelectedItem as CurrentUser;
            if (selectedRowObj.role=="USER")
            {
                deleteUserOrAdmin("Are you sure you want to delete this user?", "Delete User");
            }
            else
            {
                deleteUserOrAdmin("Are you sure you want to delete this Admin?", "Delete Admin");
            }
            loadData();
        }

        private void loadData()
        {
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

        private void filteredLoadData()
        {
            if (txtFilter.Text != "")
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
                        List<CurrentUser> userFilteredList = new List<CurrentUser>() { userFiltered };
                        dtGrid.ItemsSource = userFilteredList;
                    }
                }
                catch (Exception)
                {
                    dtGrid.ItemsSource = null;
                }
            }
            else
            {
                loadData();
            }
        }

        private void switchRole(string notCurrentRole, string messageBoxText, string caption)
        {
            CurrentUser selectedRowObj = dtGrid.SelectedItem as CurrentUser;
            switch (messageBoxCreator(messageBoxText, caption, MessageBoxButton.YesNo, MessageBoxImage.Warning))
            {
                case MessageBoxResult.Yes:
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                        var endpoint = new Uri(baseURL + "admin/user/" + selectedRowObj.id.ToString());
                        var result = client.PutAsync(endpoint, new StringContent(notCurrentRole)).Result;
                        if (!result.IsSuccessStatusCode)
                        {
                            resultGlobal = result.Content.ReadAsStringAsync().Result;
                            messageBoxCreator(resultGlobal, "Result", MessageBoxButton.OK, MessageBoxImage.None);
                        }
                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void deleteUserOrAdmin(string messageBoxText, string caption)
        {
            CurrentUser selectedRowObj = dtGrid.SelectedItem as CurrentUser;
            switch (messageBoxCreator(messageBoxText, caption, MessageBoxButton.YesNo, MessageBoxImage.Warning))
            {
                case MessageBoxResult.Yes:
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                        var endpoint = new Uri(baseURL + "admin/user/" + selectedRowObj.id.ToString());
                        var result = client.DeleteAsync(endpoint).Result;

                        if (!result.IsSuccessStatusCode)
                        {
                            resultGlobal = result.Content.ReadAsStringAsync().Result;
                            messageBoxCreator(resultGlobal, "Result", MessageBoxButton.OK, MessageBoxImage.None);
                        }
                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private object messageBoxCreator(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            var msgbox_result = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
            return msgbox_result;
        }
    }
}
