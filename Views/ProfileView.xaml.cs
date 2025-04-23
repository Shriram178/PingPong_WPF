using System.Windows;
using System.Windows.Controls;
using BounceBall.ViewModels;

namespace BounceBall.Views
{
    /// <summary>
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : UserControl
    {
        public ProfileView()
        {
            InitializeComponent();
        }

        private void Back(object sender, System.Windows.RoutedEventArgs e)
        {
            var mainViewModel = Application.Current.MainWindow.DataContext as MainViewModel;

            if (mainViewModel == null)
            {
                MessageBox.Show("MainViewModel is not available.");
                return;
            }
            mainViewModel.UpdateViewCommand.Execute("Menu");
        }
    }
}
