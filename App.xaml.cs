using System.Windows;
using BounceBall.Views;

namespace BounceBall
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = new MainWindow();
            MainWindow.Show();

        }
    }

}
