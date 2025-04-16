using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using BounceBall.Manager;
using BounceBall.ViewModels;

namespace BounceBall.Views
{
    /// <summary>
    /// Interaction logic for LoginAndSignUpView.xaml
    /// </summary>
    public partial class LoginAndSignUpView : Window
    {
        public LoginAndSignUpView()
        {
            this.DataContext = new LoginAndSignUpViewModel(new UserManager());
            InitializeComponent();

        }

        private void ShowSignupPanel(object sender, RoutedEventArgs e)
        {
            var fadeOut = (Storyboard)FindResource("FadeOut");
            fadeOut.Completed += (s, a) =>
            {
                LoginPanel.Visibility = Visibility.Collapsed;
                SignupPanel.Visibility = Visibility.Visible;
                var fadeIn = (Storyboard)FindResource("FadeIn");
                fadeIn.Begin(SignupPanel);
            };
            fadeOut.Begin(LoginPanel);
        }

        private void ShowLoginPanel(object sender, RoutedEventArgs e)
        {
            var fadeOut = (Storyboard)FindResource("FadeOut");
            fadeOut.Completed += (s, a) =>
            {
                SignupPanel.Visibility = Visibility.Collapsed;
                LoginPanel.Visibility = Visibility.Visible;
                var fadeIn = (Storyboard)FindResource("FadeIn");
                fadeIn.Begin(LoginPanel);
            };
            fadeOut.Begin(SignupPanel);
        }

        private void RemovePlaceholderText(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Text == "Username")
            {
                textBox.Text = "";
            }
            else if (sender is PasswordBox passwordBox)
            {
                if (passwordBox.Name == "PasswordLogin" && PasswordPlaceholderLogin.Visibility == Visibility.Visible)
                {
                    PasswordPlaceholderLogin.Visibility = Visibility.Collapsed;
                }
                else if (passwordBox.Name == "PasswordSignup" && PasswordPlaceholderSignup.Visibility == Visibility.Visible)
                {
                    PasswordPlaceholderSignup.Visibility = Visibility.Collapsed;
                }
                else if (passwordBox.Name == "ConfirmPasswordSignup" && ConfirmPasswordPlaceholderSignup.Visibility == Visibility.Visible)
                {
                    ConfirmPasswordPlaceholderSignup.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void AddPlaceholderText(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Username";
            }
            else if (sender is PasswordBox passwordBox && string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                if (passwordBox.Name == "PasswordLogin")
                {
                    PasswordPlaceholderLogin.Visibility = Visibility.Visible;
                }
                else if (passwordBox.Name == "PasswordSignup")
                {
                    PasswordPlaceholderSignup.Visibility = Visibility.Visible;
                }
                else if (passwordBox.Name == "ConfirmPasswordSignup")
                {
                    ConfirmPasswordPlaceholderSignup.Visibility = Visibility.Visible;
                }
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                var viewModel = DataContext as LoginAndSignUpViewModel;
                if (passwordBox.Name == "PasswordLogin")
                {
                    viewModel.Password = passwordBox.Password;
                }
                else if (passwordBox.Name == "PasswordSignup")
                {
                    viewModel.Password = passwordBox.Password;
                }
                else if (passwordBox.Name == "ConfirmPasswordSignup")
                {
                    viewModel.ConfirmPassword = passwordBox.Password;
                    PasswordMismatchWarning.Visibility = viewModel.Password == viewModel.ConfirmPassword ? Visibility.Collapsed : Visibility.Visible;
                }
            }
        }
    }
}


