using FontAwesome.Sharp;
using MonkeSwap_Desktop.Model;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace MonkeSwap_Desktop.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //Username change in the top right corner from profileview
        private readonly ProfileViewModel _childViewModel;
        private string username = CurrentUser.username;

        public string Username
        {
            get { return username; }
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged(nameof(Message));
                }
            }
        }

        private void ChildViewModel_ChildEventRaised(object sender, EventArgs e)
        {
            Username = "Child event raised!";
        }


        //Making updates based on which childview is selected
        //Fields
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public IconChar Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        //--> Commands
        public ICommand ShowUsersViewCommand { get; }
        public ICommand ShowItemsViewCommand { get; }
        public ICommand ShowProfileViewCommand { get; }

        public MainViewModel()
        {

            //Initialize commands
            ShowUsersViewCommand = new ViewModelCommand(ExecuteShowUsersViewCommand);
            ShowItemsViewCommand = new ViewModelCommand(ExecuteShowItemsViewCommand);
            ShowProfileViewCommand = new ViewModelCommand(ExecuteShowProfileViewCommand);

            //Default view
            ExecuteShowUsersViewCommand(null);

            //Initialize profile viewmodel(child) for communication(username change)
            _childViewModel = new ProfileViewModel();
            _childViewModel.ChildEventRaised += ChildViewModel_ChildEventRaised;
        }

        private void ExecuteShowUsersViewCommand(object obj)
        {
            CurrentChildView = new UsersViewModel();
            Caption = "Users";
            Icon = IconChar.UserGroup;
        }

        private void ExecuteShowItemsViewCommand(object obj)
        {
            CurrentChildView = new ItemsViewModel();
            Caption = "Items";
            Icon = IconChar.Book;
        }

        public void ExecuteShowProfileViewCommand(object obj)
        {
            CurrentChildView = new ProfileViewModel();
            Caption = "Profile";
            Icon = IconChar.UserAlt;
        }
    }
}
