using System.Collections.ObjectModel;
using System.ComponentModel;
using BounceBall.Manager;
using BounceBall.Models;

namespace BounceBall.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<GameData> GameHistory { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

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
