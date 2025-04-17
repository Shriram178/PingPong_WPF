using System.Globalization;
using System.IO;
using BounceBall.Models;
using CsvHelper;

namespace BounceBall.Manager
{
    public class FileHandler
    {
        private const string UsersFileName = "users.csv";
        private const string UserDataFolder = "UserData";

        public FileHandler()
        {
            // Ensure the UserData folder exists
            if (!Directory.Exists(UserDataFolder))
            {
                Directory.CreateDirectory(UserDataFolder);
            }
        }

        // Save the list of users to a single CSV file
        public void SaveUsers(List<User> users)
        {
            using (var writer = new StreamWriter(UsersFileName))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(users);
            }
        }

        // Load the list of users from the CSV file
        public List<User> LoadUsers()
        {
            if (!File.Exists(UsersFileName))
                return new List<User>();

            using (var reader = new StreamReader(UsersFileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return new List<User>(csv.GetRecords<User>());
            }
        }

        // Save game data for a specific user
        public void SaveGameData(string username, List<GameData> gameDataList)
        {
            var fileName = Path.Combine(UserDataFolder, $"{username}.csv");
            using (var writer = new StreamWriter(fileName))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(gameDataList);
            }
        }

        // Load game data for a specific user
        public List<GameData> LoadGameData(string username)
        {
            var fileName = Path.Combine(UserDataFolder, $"{username}.csv");
            if (!File.Exists(fileName))
                return new List<GameData>();

            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return new List<GameData>(csv.GetRecords<GameData>());
            }
        }

        // Add a new game data entry for a specific user
        public void AddGameData(string username, GameData newGameData)
        {
            var gameDataList = LoadGameData(username);
            gameDataList.Add(newGameData);
            SaveGameData(username, gameDataList);
        }
    }
}
