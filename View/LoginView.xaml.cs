﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using Application = System.Windows.Application;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using MySqlX.XDevAPI.Common;
using MonkeSwap_Desktop.Model;
using Mysqlx.Session;

namespace MonkeSwap_Desktop.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public static string baseURL = "http://localhost:8080/";

        public LoginView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

                    var newPostJson = JsonConvert.SerializeObject(new { email = txtUser.Text, password = txtPass.Password });
                    var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PostAsync("auth/login", payload).Result.Content.ReadAsStringAsync().Result;
                    var json = result;
                    var token = JsonConvert.DeserializeObject<CurrentUser>(json).token;

                    CurrentUser.userToken = token;

                }
                catch (Exception ex)
                {
                    txtErrorMessage.Text = ex.Message;
                }

                try
                {
                    string token = CurrentUser.userToken;

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var endpoint = new Uri(baseURL + "admin/users");
                    var result = client.GetAsync(endpoint).Result;
                    var json = result.Content.ReadAsStringAsync().Result;

                    if (result.IsSuccessStatusCode)
                    {
                        CurrentUser user = JsonConvert.DeserializeObject<CurrentUser>(json);
                        UserData.id = user.id;
                        UserData.email = user.email;
                        UserData.username = user.username;
                        UserData.role = user.role;
                        UserData.tradesCompleted = user.tradesCompleted;
                        UserData.dateOfRegistration = user.dateOfRegistration;
                        if(user.role=="ADMIN")
                        {
                            MainView main = new MainView();
                            main.Show();
                            Window.GetWindow(this).Close();
                        }
                        else
                        {
                            txtErrorMessage.Text = "This user doesn't have admin privileges!";
                        }
                    }
                }
                catch (Exception ex)
                {
                    txtErrorMessage.Text = ex.Message;
                }
            }
        }
    }
  }
