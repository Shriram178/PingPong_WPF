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
    public partial class LoginAndSignUpView : Page
    {
        public LoginAndSignUpView()
        {
            LoginAndSignUpViewModel viewModel = new LoginAndSignUpViewModel(new UserManager(new FileHandler()), NavigateToMenu);
            this.DataContext = viewModel;
            InitializeComponent();

        }

        private void NavigateToMenu(Models.User currentUser)
        {
            NavigationService.Navigate(new MenuView(currentUser));
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


