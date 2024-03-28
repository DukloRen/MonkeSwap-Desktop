using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace MonkeSwap_Desktop.View
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class UsersView : System.Windows.Controls.UserControl
    {
        private string baseURL = LoginView.baseURL;
        private string token = UserData.userToken;
        private List<UserData> userList;

        public UsersView()
        {
            InitializeComponent();

            loadData();
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadData();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            UserData selectedRowObj = dtGrid.SelectedItem as UserData;
            if (selectedRowObj.role == "USER")
            {
                switchRole("ADMIN", "Are you sure you want to promote this User to Admin?", "Promote User");
            }
            else
            {
                switchRole("USER", "Are you sure you want to demote this Admin to User?", "Demote Admin");
            }
            loadData();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            UserData selectedRowObj = dtGrid.SelectedItem as UserData;
            if (selectedRowObj.role == "USER")
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

                        UserData userFiltered = JsonConvert.DeserializeObject<UserData>(json);
                        List<UserData> userFilteredList = new List<UserData>() { userFiltered };
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
                using (var client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var endpoint = new Uri(baseURL + "admin/users");
                    var result = client.GetAsync(endpoint).Result;
                    var json = result.Content.ReadAsStringAsync().Result;

                    userList = JsonConvert.DeserializeObject<List<UserData>>(json);
                    dtGrid.ItemsSource = userList;
                }
            }
        }

        private void switchRole(string notCurrentRole, string messageBoxText, string caption)
        {
            UserData selectedRowObj = dtGrid.SelectedItem as UserData;
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
                            messageBoxCreator(result.Content.ReadAsStringAsync().Result, "Result", MessageBoxButton.OK, MessageBoxImage.None);
                        }
                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void deleteUserOrAdmin(string messageBoxText, string caption)
        {
            UserData selectedRowObj = dtGrid.SelectedItem as UserData;
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
                            messageBoxCreator(result.Content.ReadAsStringAsync().Result, "Result", MessageBoxButton.OK, MessageBoxImage.None);
                        }
                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private object messageBoxCreator(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            var msgbox_result = MessageBox.Show(messageBoxText, caption, button, icon);
            return msgbox_result;
        }
    }
}
