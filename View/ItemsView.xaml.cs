﻿using MonkeSwap_Desktop.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace MonkeSwap_Desktop.View
{
    /// <summary>
    /// Interaction logic for ItemsView.xaml
    /// </summary>
    public partial class ItemsView : UserControl
    {
        private string baseURL = LoginView.baseURL;
        private string token = UserData.userToken;
        private List<ItemData> itemList;
        public ItemsView()
        {
            InitializeComponent();
            loadData();
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

            ReportedItemView reportedItem = new ReportedItemView(selectedItemID);
            reportedItem.Closed += (s, args) => loadData();
            reportedItem.switchStateButton.Click += (s, args) => loadData();
            reportedItem.Show();
        }

        public void loadData()
        {
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
    }
}
