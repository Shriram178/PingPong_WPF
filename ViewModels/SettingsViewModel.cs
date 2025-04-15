using BounceBall.Models;

namespace BounceBall.ViewModels
{


    class SettingsViewModel
    {
        public SettingsViewModel(BallModel ball, PaddleModel paddle)
        {
            Ball = ball;
            Paddle = paddle;
        }

        public BallModel Ball { get; set; }

        public PaddleModel Paddle { get; set; }


    }
}
