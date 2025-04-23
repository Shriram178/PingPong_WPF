using System.Windows;
using System.Windows.Input;
using BounceBall.Command;
using BounceBall.Manager;
using BounceBall.Models;

namespace BounceBall.ViewModels
{
    public class LoginAndSignUpViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private string _confirmPassword;
        private UserManager _userManager;

        public ICommand LoginCommand { get; }
        public ICommand SignUpCommand { get; }

        public LoginAndSignUpViewModel(UserManager userManager)
        {
            LoginCommand = new RelayCommand(Login, CanLogin);
            SignUpCommand = new RelayCommand(Signup, CanSignUp);
            _userManager = userManager;
        }

        private bool CanSignUp(object obj)
        {
            return true;
        }

        private bool CanLogin(object obj)
        {
            return true;
        }


        private void Signup(object obj)
        {
            try
            {
                if (Password == ConfirmPassword)
                {
                    _userManager.AddUser(new User(UserName, Password));
                    MessageBox.Show($"Account for {UserName} created !!");
                }
                else
                {
                    MessageBox.Show("Error : Cannot create account");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void Login(object obj)
        {
            if (_userManager.ValidateUser(UserName, Password))
            {
                //User user = _userManager.GetUser(UserName, Password);
                _userManager.CurrentUser = _userManager.GetUser(UserName, Password);
                var mainViewModel = Application.Current.MainWindow.DataContext as MainViewModel;

                mainViewModel.UpdateViewCommand.Execute("Menu");
            }
            else
            {
                MessageBox.Show("Log In Failed !!");
            }
        }

        public string UserName
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

    }
}
