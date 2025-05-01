using BounceBall.Models;
using Caliburn.Micro;

namespace BounceBall.ViewModels
{

    class SettingsViewModel : Screen
    {
        private readonly GameViewModel _gameViewModel;
        public SettingsViewModel(GameViewModel gameViewModel)
        {
            _gameViewModel = gameViewModel;
            Ball = gameViewModel.Ball;
            Paddle = gameViewModel.Paddle;
        }

        public BallModel Ball { get; set; }

        public PaddleModel Paddle { get; set; }

        public void GoBack()
        {
            TryCloseAsync(true);
            _gameViewModel.GoBack();
        }
        public void Restart()
        {
            TryCloseAsync(true);
            _gameViewModel.ReloadPage();
        }
    }
}
