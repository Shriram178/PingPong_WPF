using System.Windows;
using System.Windows.Controls;
using BounceBall.ViewModels;

namespace BounceBall.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
        }



        private void Restart(object sender, System.Windows.RoutedEventArgs e)
        {
            var mainViewModel = Application.Current.MainWindow.DataContext as MainViewModel;

            if (mainViewModel == null)
            {
                MessageBox.Show("MainViewModel is not available.");
                return;
            }
            mainViewModel.UpdateViewCommand.Execute("Game");
            CloseParentWindow();
        }

        private void GoBack(object sender, System.Windows.RoutedEventArgs e)
        {
            var mainViewModel = Application.Current.MainWindow.DataContext as MainViewModel;

            if (mainViewModel == null)
            {
                MessageBox.Show("MainViewModel is not available.");
                return;
            }
            mainViewModel.UpdateViewCommand.Execute("Menu");
            CloseParentWindow();

        }

        private void CloseParentWindow()
        {
            // Find the parent window and close it
            var parentWindow = Window.GetWindow(this);
            parentWindow?.Close();
        }

    }
}
