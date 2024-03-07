using MonkeSwap_Desktop.Model;
using Org.BouncyCastle.Asn1.Cms;
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
        public ProfileView()
        {
            InitializeComponent();

            profileUsernameTxt.Text = UserData.username;
            profileEmailTxt.Text = UserData.email;
            profileTradesCompletedTxt.Text = Convert.ToString(UserData.tradesCompleted);
            profileDateOfRegistrationTxt.Text = UserData.dateOfRegistration;

        }
    }
}
