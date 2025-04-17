using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using BounceBall.Manager;
using BounceBall.Models;

namespace BounceBall.Views
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : Window
    {
        private int selectedIndex = 0;
        private TextBlock[] menuItems;
        private User _currentUser;
        private GameDataManager _gameDataManager = new GameDataManager(new FileHandler());
        public MenuView(User CurrentUser)
        {
            _currentUser = CurrentUser;
            InitializeComponent();
            menuItems = new TextBlock[] { PlayText, ProfileText, SettingsText, ExitText };
            this.KeyDown += Window_KeyDown;
            HighlightText();
        }

        private void HighlightText()
        {
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == selectedIndex)
                {
                    menuItems[i].RenderTransform = new TranslateTransform();
                    menuItems[i].Foreground = new SolidColorBrush(Colors.Green);
                    Storyboard sb = (Storyboard)FindResource("FloatAnimation");
                    sb.Begin(menuItems[i]);

                }
                else
                {
                    menuItems[i].RenderTransform = null;
                    menuItems[i].Foreground = new SolidColorBrush(Colors.White);
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                selectedIndex = (selectedIndex + 1) % menuItems.Length;
                HighlightText();
            }
            else if (e.Key == Key.Up)
            {
                selectedIndex = (selectedIndex - 1 + menuItems.Length) % menuItems.Length;
                HighlightText();
            }
            else if (e.Key == Key.Enter)
            {
                NavigateToWindow();
            }
        }

        private void NavigateToWindow()
        {
            switch (selectedIndex)
            {
                case 0:
                    GameView playWindow = new GameView(_currentUser, _gameDataManager);
                    playWindow.Show();
                    break;
                case 1:
                    ProfileView profileWindow = new ProfileView(_currentUser, _gameDataManager);
                    profileWindow.Show();
                    break;
                case 2:
                    MessageBox.Show("Settings");
                    break;
                case 3:
                    Application.Current.Shutdown();
                    break;
            }
        }

        private void PlayText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            selectedIndex = 0;
            NavigateToWindow();
        }

        private void ProfileText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            selectedIndex = 1;
            NavigateToWindow();
        }

        private void SettingsText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            selectedIndex = 2;
            NavigateToWindow();
        }

        private void ExitText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            selectedIndex = 3;
            NavigateToWindow();
        }


    }
}
