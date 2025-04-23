using System.Collections.ObjectModel;
using BounceBall.Manager;
using BounceBall.Models;

namespace BounceBall.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public ObservableCollection<GameData> GameHistory { get; set; }


        public User CurrentUser { get; }

        public GameDataManager _gameDataManager { get; }

        public GameData HighScore { get; set; }

        public ProfileViewModel(User currentUser, GameDataManager gameDataManager)
        {
            CurrentUser = currentUser;
            _gameDataManager = gameDataManager;
            GameHistory = new ObservableCollection<GameData>(_gameDataManager.GetGameData(currentUser.UserName));
            HighScore = GetHighScoreGameData();
        }

        public GameData GetHighScoreGameData()
        {
            return GameHistory.OrderByDescending(g => g.Score).FirstOrDefault();
        }
    }
}
