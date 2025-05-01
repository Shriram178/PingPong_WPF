using System.Windows;
using BounceBall.Command;
using BounceBall.Manager;
using BounceBall.Models;
using Caliburn.Micro;

namespace BounceBall.ViewModels
{
    public class LoginAndSignUpViewModel : Screen
    {
        private string _username;
        private string _password;
        private string _confirmPassword;
        private UserManager _userManager;
        private readonly IEventAggregator _events;

        //public ICommand LoginCommand { get; }
        //public ICommand SignUpCommand { get; }

        public LoginAndSignUpViewModel(IEventAggregator events, UserManager userManager)
        {
            _events = events;
            //LoginCommand = new RelayCommand(Login, CanLogin);
            //SignUpCommand = new RelayCommand(Signup, CanSignUp);
            _userManager = userManager;
        }

        public bool CanSignUp()
        {
            return true;
        }

        public bool CanLogin()
        {
            return true;
        }


        public void Signup()
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

        public async Task Login()
        {
            if (_userManager.ValidateUser(UserName, Password))
            {
                _userManager.CurrentUser = _userManager.GetUser(UserName, Password);
                await _events.PublishOnUIThreadAsync(new NavigateToMenuMessage());

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
                NotifyOfPropertyChange(nameof(UserName));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyOfPropertyChange(nameof(Password));
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                NotifyOfPropertyChange(nameof(ConfirmPassword));
            }
        }

    }
}
