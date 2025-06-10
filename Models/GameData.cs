using CsvHelper.Configuration.Attributes;

namespace BounceBall.Models
{
    public class GameData
    {
        public GameData() { }

        public GameData(int score, DateTime startTime, DateTime endTime)
        {
            Score = score;
            Duration = endTime - startTime;
            PlayedAt = startTime;
        }

        [Name("Score")]
        public int Score { get; set; }

        [Name("Start Time")]
        public DateTime PlayedAt { get; set; }

        [Name("Duration")]
        public TimeSpan Duration { get; set; }

    }
}
