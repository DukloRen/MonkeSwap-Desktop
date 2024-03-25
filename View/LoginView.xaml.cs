using MonkeSwap_Desktop.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Application = System.Windows.Application;

namespace MonkeSwap_Desktop.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public static string baseURL = "http://localhost:8080/";
        private static string result_string;

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
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var newPostJson = JsonConvert.SerializeObject(new { email = txtEmail.Text, password = txtPass.Password });
                    var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PostAsync("auth/login", payload).Result.Content.ReadAsStringAsync().Result;
                    result_string = result;
                    var token = JsonConvert.DeserializeObject<CurrentUser>(result).token;

                    CurrentUser.userToken = token;
                }
                catch (Exception)
                {
                    txtErrorMessage.Text = result_string;
                }

                try
                {
                    string token = CurrentUser.userToken;

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    //var endpoint = new Uri(baseURL + "user");
                    var result = client.GetAsync("user").Result;
                    result_string = result.ToString();
                    var json = result.Content.ReadAsStringAsync().Result;

                    if (result.IsSuccessStatusCode)
                    {
                        CurrentUser user = JsonConvert.DeserializeObject<CurrentUser>(json);

                        if (user.role == "ADMIN")
                        {
                            UserData.id = user.id;
                            UserData.email = user.email;
                            UserData.username = user.username;
                            UserData.role = user.role;
                            UserData.tradesCompleted = user.tradesCompleted;
                            UserData.dateOfRegistration = user.dateOfRegistration;
                            UserData.profilePicture = user.profilePicture;

                            //UsersViewModel usersV = new UsersViewModel();
                            //usersV.currentUsername = UserData.username;

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

        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {

        }
    }
}
