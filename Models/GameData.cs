using CsvHelper.Configuration.Attributes;

namespace BounceBall.Models
{
    public class GameData
    {
        public GameData() { }

        public GameData(int score, DateTime startTime, DateTime endTime)
        {
            Score = score;
            StartTime = startTime;
            EndTime = endTime;
            Duration = endTime - startTime;
        }

        [Name("Score")]
        public int Score { get; set; }

        [Name("Start Time")]
        public DateTime StartTime { get; set; }

        [Name("End Time")]
        public DateTime EndTime { get; set; }

        [Name("Duration")]
        public TimeSpan Duration { get; set; }

    }
}
