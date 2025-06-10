using System.Net.Http;
using System.Net.Http.Json;
using BounceBall.Models;
using static BounceBall.Models.User;

namespace BounceBall.Manager
{
    public class GameDataManager
    {
        private readonly HttpClient _httpClient;
        private readonly FileHandler _fileHandler;
        private readonly UserManager _userManager;
        private List<GameData> _gameData;

        public GameDataManager(FileHandler fileHandler, UserManager userManager)
        {

            _userManager = userManager;
            _httpClient = _userManager.AuthenticatedClient;
            _gameData = new List<GameData>();


        }

        public async Task<List<GameData>> GetGameDataAsync(
            int pageIndex = 0,
            int pageSize = 10,
            string sortColumn = "Id",
            string sortOrder = "DESC")
        {
            try
            {
                var query = $"api/GameScore/my-scores?PageIndex={pageIndex}" +
                    $"&PageSize={pageSize}" +
                    $"&SortColumn={sortColumn}&SortOrder={sortOrder}";

                var response = await _httpClient.GetAsync(query);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<RestDto<GameScoreDto[]>>();

                if (result?.Data == null) return new List<GameData>();

                _gameData = result.Data
                    .Select(dto => new GameData
                    {
                        Score = dto.Score,
                        Duration = dto.Duration,
                        PlayedAt = dto.PlayedAt
                    }).ToList();

                return _gameData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load game data: {ex.Message}");
                return new List<GameData>();
            }
        }


        public async Task AddGameData(string username, GameData gameData)
        {
            try
            {
                var gameDataRequest = new { score = gameData.Score, duration = gameData.Duration };
                using var response = await _httpClient.PostAsJsonAsync("api/GameScore/submit", gameDataRequest);

                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Failed to add game data: {ex.Message}");
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine($"Request timed out: {ex.Message}");
            }

        }

        public async Task<List<LeaderboardEntry>> GetLeaderboardsAsync(
            int pageIndex = 0,
            int pageSize = 10,
            string sortColumn = "BestScore",
            string sortOrder = "DESC")
        {
            try
            {
                var query = $"api/leaderboard/top?PageIndex={pageIndex}" +
                    $"&PageSize={pageSize}" +
                    $"&SortColumn={sortColumn}&SortOrder={sortOrder}";
                var response = await _httpClient.GetAsync(query);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<RestDto<LeaderboardEntry[]>>();
                return result?.Data?.ToList() ?? new List<LeaderboardEntry>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load leaderboards: {ex.Message}");
                return new List<LeaderboardEntry>();
            }
        }
    }
}
