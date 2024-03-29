﻿using FontAwesome.Sharp;
using System.Windows.Forms;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace MonkeSwap_Desktop.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
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
