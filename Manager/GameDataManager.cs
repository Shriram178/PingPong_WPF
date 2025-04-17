using BounceBall.Models;

namespace BounceBall.Manager
{
    public class GameDataManager
    {
        private readonly FileHandler _fileHandler;
        private List<GameData> _gameData;

        public GameDataManager(FileHandler fileHandler)
        {
            _fileHandler = fileHandler;
            _gameData = new List<GameData>();
        }

        public List<GameData> GetGameData(string username)
        {
            if (_gameData.Count == 0)
            {
                // Load game data from the file only if the in-memory list is empty
                _gameData = _fileHandler.LoadGameData(username);
            }

            return _gameData;
        }

        public void AddGameData(string username, GameData gameData)
        {
            // Add the new game data to the in-memory list
            _gameData.Add(gameData);

            // Save the updated game data back to the file
            _fileHandler.SaveGameData(username, _gameData);
        }
    }
}
