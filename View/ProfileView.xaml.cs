﻿using MonkeSwap_Desktop.Model;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Cms;
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
using System.Windows.Threading;

namespace MonkeSwap_Desktop.View
{
    /// <summary>
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : UserControl
    {
        private string baseURL = LoginView.baseURL;
        private string token = CurrentUser.userToken;
        private static string result_string;
        public ProfileView()
        {
            InitializeComponent();

            profileUsernameTxt.Text = UserData.username;
            profileEmailTxt.Text = UserData.email;
            profileTradesCompletedTxt.Text = Convert.ToString(UserData.tradesCompleted);
            profileDateOfRegistrationTxt.Text = UserData.dateOfRegistration;
            profileRoleTxt.Text = UserData.role;

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(UserData.profilePicture); ;
            bitmapImage.EndInit();
            profileProfilePicture.ImageSource = bitmapImage;
        }

        private void txtNewPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            txtNewPasswordTextBlock.Visibility = Visibility.Hidden;
        }

        private void txtNewPasswordAgain_PasswordChanged(object sender, RoutedEventArgs e)
        {
            txtNewPasswordAgainTextBlock.Visibility = Visibility.Hidden;
        }
        private void txtNewPassword_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (txtNewPassword.Password == "")
            {
                txtNewPasswordTextBlock.Visibility = Visibility.Visible;
            }
        }
        private void txtNewPasswordAgain_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (txtNewPasswordAgain.Password=="")
            {
                txtNewPasswordAgainTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (txtNewPassword.Password!=txtNewPasswordAgain.Password)
            {
                txtErrorMessage.Text = "The passwords do not match!";
            }
            else if (txtNewPassword.Password== "" || txtNewPasswordAgain.Password == "")
            {
                txtErrorMessage.Text = "The passwords can not be empty!";
            }
            else
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                        var newPostJson = JsonConvert.SerializeObject(new {password = txtNewPassword.Password });
                        var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                        var result = client.PutAsync(baseURL + "user/password", payload).Result;
                        result_string = result.Content.ReadAsStringAsync().Result;

                        if (result.IsSuccessStatusCode)
                        {
                            LoginView login = new LoginView();
                            login.Show();
                            Window.GetWindow(this).Close();
                        }
                    }
                    catch (Exception)
                    {
                        txtErrorMessage.Text = result_string;
                    }
                }
            }
        }
    }
}
