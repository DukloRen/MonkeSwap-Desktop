using MonkeSwap_Desktop.Model;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace MonkeSwap_Desktop.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            if (CurrentUser.profilePicture == "")
            {
                bitmapImage.UriSource = new Uri("https://i.imgur.com/MonXtG8.jpeg");
            }
            else
            {
                bitmapImage.UriSource = new Uri(CurrentUser.profilePicture);
            }
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
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void logOutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginView login = new LoginView();
            UserData.userToken = null;
            login.Show();
            Window.GetWindow(this).Close();
        }

        private void profilePictureInTopRightButton_Click(object sender, RoutedEventArgs e)
        {
            usersRadioButton.IsChecked = false;
            itemsRadioButton.IsChecked = false;
            profileRadioButton.IsChecked = true;
        }
    }
}
