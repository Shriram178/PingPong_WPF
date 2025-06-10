using BounceBall.Command;
using BounceBall.Manager;
using BounceBall.Models;
using Caliburn.Micro;

namespace BounceBall.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly IEventAggregator _events;

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

        public MenuViewModel(IEventAggregator events, UserManager userManager, GameDataManager gameDataManager)
        {
            _events = events;
            _currentUser = userManager.CurrentUser;
            _gameDataManager = gameDataManager;
        }

        public async Task GoToGame()
        {
            _events.PublishOnUIThreadAsync(new NavigateToGameMessage());
        }

        public async Task GoToProfile()
        {
            _events.PublishOnUIThreadAsync(new NavigateToProfileMessage());
        }

        public async Task GoToLogin()
        {
            _events.PublishOnUIThreadAsync(new NavigateToLoginMessage());
        }

        public async Task GoToLeaderboard()
        {
            _events.PublishOnUIThreadAsync(new NavigateToLeaderboardMessage());
        }
    }
}
