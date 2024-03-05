using MonkeSwap_Desktop.Model;
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

        private const string path = "https://localhost:8080/";
        private ObservableCollection<UserData> users = new ObservableCollection<UserData>();

        /*
        public ObservableCollection<UserData> Users
        {
            get { return users; }
            set
            {
                SetProperty<ObservableCollection<UserData>>(ref users, value);
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

        public async Task<ObservableCollection<UserData>> getAllUsers()
        {


            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(path);

                users = await response.Content.ReadAsAsync<ObservableCollection<UserData>>();
                return users;
            }

        }*/

    }
}
