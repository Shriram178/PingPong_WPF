using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using BounceBall.ViewModels;

namespace BounceBall.Views
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl, IKeyHandler
    {
        private int selectedIndex = 0;
        private TextBlock[] menuItems;
        public MenuView()
        {
            InitializeComponent();
            menuItems = new TextBlock[] { PlayText, ProfileText, SettingsText, ExitText };
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

        public void OnKeyDown(KeyEventArgs e)
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
                NavigateToPage();
            }
        }

        public void OnKeyUp(KeyEventArgs e)
        {
            // No specific KeyUp logic for MenuView
        }

        private void NavigateToPage()
        {



            switch (selectedIndex)
            {
                case 0:
                    if (DataContext is MenuViewModel menuViewModel)
                    {
                        menuViewModel.GoToGame();
                    }
                    break;
                case 1:
                    if (DataContext is MenuViewModel menuViewModel1)
                    {
                        menuViewModel1.GoToProfile();
                    }
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
            NavigateToPage();
        }

        private void ProfileText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            selectedIndex = 1;
            NavigateToPage();
        }

        private void SettingsText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            selectedIndex = 2;
            NavigateToPage();
        }

        private void ExitText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            selectedIndex = 3;
            NavigateToPage();
        }


    }
}
