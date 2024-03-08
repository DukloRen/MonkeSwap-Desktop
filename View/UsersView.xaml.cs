﻿using MonkeSwap_Desktop.Model;
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
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class UsersView : UserControl
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
                        var endpoint2 = new Uri(baseURL + "admin/user/" + txtFilter.Text);
                        var result2 = client.GetAsync(endpoint2).Result;
                        var json2 = result2.Content.ReadAsStringAsync().Result;

                        CurrentUser userFiltered = JsonConvert.DeserializeObject<CurrentUser>(json2);
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
    }
}
