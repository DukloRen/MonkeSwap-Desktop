using MonkeSwap_Desktop.ViewModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MonkeSwap_Desktop.View
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class UsersView : UserControl
    {
        public UsersView()
        {
            InitializeComponent();
        
        var converter = new BrushConverter();
        ObservableCollection<User> users = new ObservableCollection<User>();

        //Create DataGrid Items
        users.Add(new User {Id = 1, Username = "Kóth Tevin", Email = "kt@gmail.com", TC = 9, DOR = "25/02/2024", Role = "admin" });
        users.Add(new User { Id = 2, Username = "Kóth Tevin2", Email = "kt@gmail.hu", TC = 11, DOR = "24/02/2024", Role = "user" });
        users.Add(new User {Id = 3, Username = "Kóth Tevin3", Email = "kt@citromail.hu", TC = 20, DOR = "29/02/2024", Role = "user" });
        users.Add(new User {Id = 4, Username= "Kóth Tevin4", Email = "kt@email.com", TC = 13, DOR = "28/02/2024", Role = "user" });
        users.Add(new User {Id = 5, Username= "Kóth Tevin5", Email = "kt@email.hu", TC = 20, DOR = "26/02/2024", Role = "user" });
        users.Add(new User {Id = 6, Username = "Kóth Tevin", Email = "kt@gmail.com", TC = 9, DOR = "25/02/2024", Role = "admin" });
        users.Add(new User {Id = 7, Username = "Kóth Tevin2", Email = "kt@gmail.hu", TC = 11, DOR = "24/02/2024", Role = "user" });
        users.Add(new User {Id = 8, Username = "Kóth Tevin3", Email = "kt@citromail.hu", TC = 20, DOR = "29/02/2024", Role = "user" });
        users.Add(new User {Id = 9, Username = "Kóth Tevin4", Email = "kt@email.com", TC = 13, DOR = "28/02/2024", Role = "user" });
        users.Add(new User {Id = 10, Username = "Kóth Tevin5", Email = "kt@email.hu", TC = 20, DOR = "26/02/2024", Role = "user" });
        users.Add(new User {Id = 11, Username = "Kóth Tevin", Email = "kt@gmail.com", TC = 9, DOR = "25/02/2024", Role = "admin" });
        users.Add(new User {Id = 12, Username = "Kóth Tevin2", Email = "kt@gmail.hu", TC = 11, DOR = "24/02/2024", Role = "user" });
        users.Add(new User {Id = 13, Username = "Kóth Tevin3", Email = "kt@citromail.hu", TC = 20, DOR = "29/02/2024", Role = "user" });
        users.Add(new User {Id = 14, Username = "Kóth Tevin4", Email = "kt@email.com", TC = 13, DOR = "28/02/2024", Role = "user" });
        users.Add(new User {Id = 15, Username = "Kóth Tevin5", Email = "kt@email.hu", TC = 20, DOR = "26/02/2024", Role = "user" });

        dtGrid.ItemsSource = users;
       }
    }
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int TC { get; set; }
        public string DOR { get; set; }
        public string Role { get; set; }
    }
}
