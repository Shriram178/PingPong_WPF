namespace BounceBall.Models
{
    public class GameData
    {
        public GameData(int score, DateTime startTime, DateTime endTime)
        {
            Score = score;
            StartTime = startTime;
            EndTime = endTime;
        }

        public int Score { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
