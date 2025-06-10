namespace BounceBall.Models
{
    public class LeaderboardEntry
    {
        public string Username { get; set; } = string.Empty;
        public int BestScore { get; set; }
        public double AverageScore { get; set; }
        public int TotalGamesPlayed { get; set; }
        public int? Rank { get; set; }
    }
}
