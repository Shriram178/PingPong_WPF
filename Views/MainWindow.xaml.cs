using System.Windows;
using System.Windows.Input;

namespace BounceBall.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new LoginAndSignUpView());
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            // Forward KeyDown to the current Page
            if (MainFrame.Content is IKeyHandler keyHandlerPage)
            {
                keyHandlerPage.OnKeyDown(e);
            }
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            // Forward KeyUp to the current Page
            if (MainFrame.Content is IKeyHandler keyHandlerPage)
            {
                keyHandlerPage.OnKeyUp(e);
            }
        }
    }
}
