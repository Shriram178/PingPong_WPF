using System.Collections.ObjectModel;
using BounceBall.Command;
using BounceBall.Manager;
using BounceBall.Models;
using Caliburn.Micro;

namespace BounceBall.ViewModels
{
    public class ProfileViewModel : Screen
    {
        public ObservableCollection<GameData> GameHistory { get; set; }


        public User CurrentUser { get; }

        public GameDataManager _gameDataManager { get; }

        public GameData HighScore { get; set; }

        private readonly IEventAggregator _events;

        public ProfileViewModel(IEventAggregator events, UserManager userManager, GameDataManager gameDataManager)
        {
            _events = events;
            CurrentUser = userManager.CurrentUser;
            _gameDataManager = gameDataManager;
            GameHistory = new ObservableCollection<GameData>(_gameDataManager.GetGameData(CurrentUser.UserName));
            HighScore = GetHighScoreGameData();
        }

        public async Task GoBack()
        {
            _events.PublishOnUIThreadAsync(new NavigateToMenuMessage());
        }

        public GameData GetHighScoreGameData()
        {
            return GameHistory.OrderByDescending(g => g.Score).FirstOrDefault();
        }
    }
}
