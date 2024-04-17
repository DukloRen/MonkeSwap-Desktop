﻿using MonkeSwap_Desktop.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MonkeSwap_Desktop.View
{
    /// <summary>
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : UserControl
    {
        private string baseURL = LoginView.baseURL;
        private string token = UserData.userToken;
        private static string result_string;
        public ProfileView()
        {
            InitializeComponent();

            profileUsernameTxt.Text = CurrentUser.username;
            profileEmailTxt.Text = CurrentUser.email;
            profileTradesCompletedTxt.Text = Convert.ToString(CurrentUser.tradesCompleted);
            profileDateOfRegistrationTxt.Text = CurrentUser.dateOfRegistration;
            profileRoleTxt.Text = CurrentUser.role;

            MainView mainView = new MainView();
            profileProfilePicture.ImageSource = mainView.profilePictureSource;
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
            if (txtNewPasswordAgain.Password == "")
            {
                txtNewPasswordAgainTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (txtNewPassword.Password != txtNewPasswordAgain.Password)
            {
                txtErrorMessage.Text = "The passwords do not match!";
            }
            else
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                        var newPostJson = JsonConvert.SerializeObject(new { password = txtNewPassword.Password });
                        var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                        var result = client.PutAsync(baseURL + "user/password", payload).Result;
                        result_string = result.Content.ReadAsStringAsync().Result;

                        if (result.IsSuccessStatusCode)
                        {
                            LoginView login = new LoginView();
                            UserData.userToken = null;
                            login.Show();
                            Window.GetWindow(this).Close();
                        }
                        else
                        {
                            txtErrorMessage.Text = result_string;
                        }
                    }
                    catch (Exception)
                    {
                        txtErrorMessage.Text = result_string;
                    }
                }
            }
        }

        private void changeUsernameButton_Click(object sender, RoutedEventArgs e)
        {
            changeUsernameNecessitiesVisibilityChanger(Visibility.Hidden, Visibility.Visible, Visibility.Visible, Visibility.Visible);
        }

        private void changeUsernameAcceptButton_Click(object sender, RoutedEventArgs e)
        {

            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var newPostJson = JsonConvert.SerializeObject(new { username = changeUsernameTextBox.Text });
                    var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PutAsync(baseURL + "user/username", payload).Result;
                    result_string = result.Content.ReadAsStringAsync().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        CurrentUser.username = changeUsernameTextBox.Text;
                        profileUsernameTxt.Text = CurrentUser.username;
                        txtUsernameErrorMessage.Text = "";
                        changeUsernameNecessitiesVisibilityChanger(Visibility.Visible, Visibility.Hidden, Visibility.Hidden, Visibility.Hidden);
                    }
                    else
                    {
                        txtUsernameErrorMessage.Text = result_string;
                    }
                }
                catch (Exception)
                {
                    txtUsernameErrorMessage.Text = result_string;
                }
            }
        }

        private void changeUsernameCancelButton_Click(object sender, RoutedEventArgs e)
        {
            changeUsernameNecessitiesVisibilityChanger(Visibility.Visible, Visibility.Hidden, Visibility.Hidden, Visibility.Hidden);
            txtUsernameErrorMessage.Text = "";
        }

        private void changeUsernameNecessitiesVisibilityChanger(Visibility profileUsernameTxtV, Visibility changeUsernameTextBoxV, Visibility changeUsernameAcceptButtonV, Visibility changeUsernameCancelButtonV)
        {
            profileUsernameTxt.Visibility = profileUsernameTxtV;
            changeUsernameTextBox.Visibility = changeUsernameTextBoxV;
            changeUsernameAcceptButton.Visibility = changeUsernameAcceptButtonV;
            changeUsernameCancelButton.Visibility = changeUsernameCancelButtonV;
        }
    }
}
