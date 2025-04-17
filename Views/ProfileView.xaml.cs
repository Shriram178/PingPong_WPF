using System.Windows;
using BounceBall.Manager;
using BounceBall.Models;
using BounceBall.ViewModels;

namespace BounceBall.Views
{
    /// <summary>
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : Window
    {
        public ProfileView(User currentUser, GameDataManager gameDataManager)
        {
            this.DataContext = new ProfileViewModel(currentUser, gameDataManager);
            InitializeComponent();
        }

    }
}
