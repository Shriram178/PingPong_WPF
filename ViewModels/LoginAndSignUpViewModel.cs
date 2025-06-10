using System.Windows;
using BounceBall.Command;
using BounceBall.Manager;
using Caliburn.Micro;

namespace BounceBall.ViewModels
{
    public class LoginAndSignUpViewModel : Screen
    {
        private string _username;
        private string _emial;
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


        public async void Signup()
        {
            try
            {
                if (Password == ConfirmPassword)
                {
                    var isRegistered = await _userManager.RegisterAsync(UserName, Password, Email);
                    if (!isRegistered)
                    {
                        MessageBox.Show("Error : Cannot create account");
                        return;
                    }
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
            if (await _userManager.LoginAsync(UserName, Password))
            {
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
            set => Set(ref _username, value);
        }

        public string Email
        {
            get => _emial;
            set => Set(ref _emial, value);
        }

        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => Set(ref _confirmPassword, value);
        }

    }
}
