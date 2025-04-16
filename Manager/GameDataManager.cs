using System.Collections.ObjectModel;
using BounceBall.Models;

namespace BounceBall.Manager
{
    public class GameDataManager
    {
        public ObservableCollection<GameData> GameData { get; set; }

        public void AddGameData(GameData gameData)
        {
            GameData.Add(gameData);
        }
    }
}
