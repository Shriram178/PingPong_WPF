using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using BounceBall.Command;
using BounceBall.Manager;
using BounceBall.Models;
using Caliburn.Micro;

namespace BounceBall.ViewModels
{
    public class GameViewModel : Screen
    {
        public event EventHandler GameOver;
        public BallModel Ball { get; set; }
        public PaddleModel Paddle { get; set; }
        private int _score;
        private DispatcherTimer _timer;
        private bool _isRunning;
        private double _canvasWidth;
        private double _canvasHeight;

        private readonly GameDataManager _gameDataManager;
        private readonly string _currentUsername;

        private DateTime _currentGameStartTime;

        public int Score { get => _score; set { _score = value; NotifyOfPropertyChange(); } }
        public bool IsRunning { get => _isRunning; set { _isRunning = value; NotifyOfPropertyChange(); } }
        public double CanvasWidth { get => _canvasWidth; set { _canvasWidth = value; NotifyOfPropertyChange(); } }
        public double CanvasHeight { get => _canvasHeight; set { _canvasHeight = value; NotifyOfPropertyChange(); } }

        private readonly IEventAggregator _events;

        private readonly IWindowManager _windowManager;

        public GameViewModel(IWindowManager windowManager, IEventAggregator events, GameDataManager gameDataManager, UserManager userManager)
        {
            _windowManager = windowManager;
            _events = events;
            _gameDataManager = gameDataManager;
            _currentUsername = userManager.CurrentUser.UserName;
            Ball = new BallModel { X = 100, Y = 100, SpeedX = 5, SpeedY = 5, Size = 15, Color = Brushes.Red };
            Paddle = new PaddleModel { X = 350, Width = 100 };
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1) };
            _timer.Tick += Update;
            _currentGameStartTime = DateTime.Now;
            StartGame();
        }

        public void OnGameOver()
        {
            var gameData = new GameData(Score, _currentGameStartTime, DateTime.Now);
            _gameDataManager.AddGameData(_currentUsername, gameData);

            MessageBox.Show($"Game Over! Your score: {Score}\nPress Enter to play again.");
        }

        private void Update(object sender, EventArgs e)
        {
            Ball.X += Ball.SpeedX;
            Ball.Y += Ball.SpeedY;

            if (Ball.X <= 0 || Ball.X >= CanvasWidth - Ball.Size)
            {
                Ball.SpeedX = -Ball.SpeedX;
            }

            if (Ball.Y <= 0)
            {
                Ball.SpeedY = -Ball.SpeedY;
            }

            if (Ball.Y >= CanvasHeight - Ball.Size - 10) // Adjust for paddle height
            {
                if (Ball.X >= Paddle.X && Ball.X <= Paddle.X + Paddle.Width)
                {
                    Ball.Y = CanvasHeight - Ball.Size - 15; // Set the ball's position just above the paddle
                    Ball.SpeedY = -Ball.SpeedY;
                    Score++;
                }
                else
                {
                    _timer.Stop();
                    IsRunning = false;
                    GameOver?.Invoke(this, EventArgs.Empty);
                    OnGameOver();
                    MessageBox.Show("Game Over! Your score: " + Score + "\nPress Enter to play again.");
                }
            }
        }

        public void OpenSettings()
        {
            _windowManager.ShowDialogAsync(new SettingsViewModel(this));
        }

        public async Task GoBack()
        {
            await _events.PublishOnUIThreadAsync(new NavigateToMenuMessage());
        }

        public async Task ReloadPage()
        {
            await _events.PublishOnUIThreadAsync(new NavigateToGameMessage());
        }

        public void Pause()
        {
            _timer.Stop();
            IsRunning = false;
        }

        public void Resume()
        {
            _timer.Start();
            IsRunning = true;
        }

        public void Restart()
        {
            Ball.X = 100;
            Ball.Y = 100;
            //Ball.SpeedX = 5;
            //Ball.SpeedY = 5;
            Paddle.X = (CanvasWidth - Paddle.Width) / 2;
            Score = 0;
            StartGame();
        }


        private void StartGame()
        {
            _timer.Start();
            IsRunning = true;
        }


    }
}
