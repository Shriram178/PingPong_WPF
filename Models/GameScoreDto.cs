namespace BounceBall.Models
{
    public partial class User
    {
        public class GameScoreDto
        {
            public int Score { get; set; }
            public TimeSpan Duration { get; set; }
            public DateTime PlayedAt { get; set; }
        }

    }
}
