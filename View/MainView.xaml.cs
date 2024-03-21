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
using System.Runtime.InteropServices;
using System.Windows.Interop;
using MonkeSwap_Desktop.ViewModel;
using FontAwesome.Sharp;
using MonkeSwap_Desktop.Model;
using System.Windows.Threading;

namespace MonkeSwap_Desktop.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(string userName)
        {
            InitializeComponent();
            this.MaxHeight=SystemParameters.MaximizedPrimaryScreenHeight;

            userNameTopRightCorner.Text = userName;

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(UserData.profilePicture); ;
            bitmapImage.EndInit();
            profilePictureInTopRight.ImageSource = bitmapImage;

        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if(this.WindowState == WindowState.Normal)
            {
                this.WindowState=WindowState.Maximized;                
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void logOutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginView login = new LoginView();
            CurrentUser.userToken=null;
            login.Show();
            Window.GetWindow(this).Close();
        }

        private void profilePictureInTopRightButton_Click(object sender, RoutedEventArgs e)
        {
            usersRadioButton.IsChecked = false;
            itemsRadioButton.IsChecked = false;
            settingsRadioButton.IsChecked = false;
            profileRadioButton.IsChecked = true;
        }
    }
}
