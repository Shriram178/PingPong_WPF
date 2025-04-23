using BounceBall.Manager;
using BounceBall.Models;

namespace BounceBall.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private User _currentUser;
        private GameDataManager _gameDataManager;

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        public MenuViewModel(User currentUser, GameDataManager gameDataManager)
        {
            _currentUser = currentUser;
            _gameDataManager = gameDataManager;
        }
    }
}
