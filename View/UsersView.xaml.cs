using MonkeSwap_Desktop.ViewModel;
using MySql.Data.MySqlClient;
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
        public UsersView()
        {
            InitializeComponent();
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var endpoint = new Uri(baseURL + "admin/users");
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;

                List<CurrentUser> userList = JsonConvert.DeserializeObject<List<CurrentUser>>(json);
        
                dtGrid.ItemsSource = userList;
            }

            /*
        var converter = new BrushConverter();
        ObservableCollection<User> users = new ObservableCollection<User>();

        //Create DataGrid Items
        users.Add(new User {Id = 1, Username = "Kóth Tevin", Email = "kt@gmail.com", TC = 9, DOR=new DateTime(2024, 12, 01), Role = "admin" });
        users.Add(new User { Id = 2, Username = "Kóth Tevin2", Email = "kt@gmail.hu", TC = 11, DOR = new DateTime(2024, 11, 01), Role = "user" });
        users.Add(new User {Id = 3, Username = "Kóth Tevin3", Email = "kt@citromail.hu", TC = 20, DOR = new DateTime(2024, 10, 01), Role = "user" });
        users.Add(new User {Id = 4, Username= "Kóth Tevin4", Email = "kt@email.com", TC = 13, DOR = new DateTime(2024, 12, 08), Role = "user" });
        users.Add(new User {Id = 5, Username= "Kóth Tevin5", Email = "kt@email.hu", TC = 20, DOR = new DateTime(2024, 12, 10), Role = "user" });
        users.Add(new User {Id = 6, Username = "Kóth Tevin", Email = "kt@gmail.com", TC = 9, DOR = new DateTime(2024, 01, 20), Role = "admin" });
        users.Add(new User {Id = 7, Username = "Kóth Tevin2", Email = "kt@gmail.hu", TC = 11, DOR = new DateTime(2024, 01, 28), Role = "user" });
        users.Add(new User {Id = 8, Username = "Kóth Tevin3", Email = "kt@citromail.hu", TC = 20, DOR = new DateTime(2024, 08, 12), Role = "user" });
        users.Add(new User {Id = 9, Username = "Kóth Tevin4", Email = "kt@email.com", TC = 13, DOR = new DateTime(2024, 12, 01), Role = "user" });
        users.Add(new User {Id = 10, Username = "Kóth Tevin5", Email = "kt@email.hu", TC = 20, DOR = new DateTime(2024, 12, 01), Role = "user" });
        users.Add(new User {Id = 11, Username = "Kóth Tevin", Email = "kt@gmail.com", TC = 9, DOR = new DateTime(2024, 12, 01), Role = "admin" });
        users.Add(new User {Id = 12, Username = "Kóth Tevin2", Email = "kt@gmail.hu", TC = 11, DOR = new DateTime(2024, 12, 01), Role = "user" });
        users.Add(new User {Id = 13, Username = "Kóth Tevin3", Email = "kt@citromail.hu", TC = 20, DOR = new DateTime(2024, 12, 01), Role = "user" });
        users.Add(new User {Id = 14, Username = "Kóth Tevin4", Email = "kt@email.com", TC = 13, DOR = new DateTime(2024, 12, 01), Role = "user" });
        users.Add(new User {Id = 15, Username = "Kóth Tevin5", Email = "kt@email.hu", TC = 20, DOR =new DateTime(2024, 12, 01), Role = "user" });

        dtGrid.ItemsSource = users;
       }
    }
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int TC { get; set; }
        public DateTime DOR { get; set; }
        public string Role { get; set; }
    }
            */
        }
    }
}
