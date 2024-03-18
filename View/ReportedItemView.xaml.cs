using MonkeSwap_Desktop.Model;
using MonkeSwap_Desktop.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace MonkeSwap_Desktop.View
{
    /// <summary>
    /// Interaction logic for ReportedItemView.xaml
    /// </summary>
    public partial class ReportedItemView : Window
    {
        public ReportedItemView(long selectedItemID, string selectedItemTitle, string selectedItemPicture, string selectedItemDescription, int selectedItemViews, string selectedItemState, string selectedItemCategory, string selectedItemPriceTier, long[] selectedItemReports, string selectedItemUserID)
        {
            InitializeComponent();

            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            idTxt.Text = "ID: " + selectedItemID.ToString();
            titleTxt.Text = selectedItemTitle;

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(selectedItemPicture); ;
            bitmapImage.EndInit();
            pic.ImageSource = bitmapImage;

            descriptionTxt.Text = "Description: " + selectedItemDescription;
            viewsTxt.Text = "Views: " + selectedItemViews.ToString();
            stateTxt.Text = "State: " + selectedItemState;
            categoryTxt.Text = "Category: " + selectedItemCategory;
            pricetierTxt.Text = "Price tier: " + selectedItemPriceTier;
            reportsTxt.Text = "Reports: " + selectedItemReports.Count();
            userIDTxt.Text = "User ID: " + selectedItemUserID;
        }
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void pnlControlBar_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
    }
}
