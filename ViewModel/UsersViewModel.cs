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


        private ObservableCollection<UserData> Users
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

        private async Task<ObservableCollection<UserData>> getAllUsers()
        {


            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(baseURL+"admin/users");

                users = await response.Content.ReadAsAsync<ObservableCollection<UserData>>();
                return users;
            }

        }

    }
}
