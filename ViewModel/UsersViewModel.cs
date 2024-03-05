using MonkeSwap_Desktop.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MonkeSwap_Desktop.ViewModel
{
    public class UsersViewModel : ViewModelBase
    {
        public string baseURL = LoginView.baseURL;
        private ObservableCollection<UserData> users = new ObservableCollection<UserData>();

        private const string path = "https://localhost:8080";
        private ObservableCollection<User> users = new ObservableCollection<User>();

        private ObservableCollection<UserData> Users
        {
            get { return users; }
            set
            {
            }
        }

        private void SetProperty<T>(ref T users, T value)
        {
            throw new NotImplementedException();
        }

        public async Task LoadUsers()
        {
            Users = await getAllUsers();
        }

        {


            using (HttpClient client = new HttpClient())
            {

                return users;
            }

        }

    }
}
