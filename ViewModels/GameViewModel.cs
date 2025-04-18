using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using BounceBall.Manager;
using BounceBall.Models;

namespace BounceBall.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Obstacle> Obstacles { get; set; } = new ObservableCollection<Obstacle>();

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

        public int Score { get => _score; set { _score = value; OnPropertyChanged(); } }
        public bool IsRunning { get => _isRunning; set { _isRunning = value; OnPropertyChanged(); } }
        public double CanvasWidth { get => _canvasWidth; set { _canvasWidth = value; OnPropertyChanged(); } }
        public double CanvasHeight { get => _canvasHeight; set { _canvasHeight = value; OnPropertyChanged(); } }

        public GameViewModel(GameDataManager gameDataManager, string currentUsername)
        {
            _gameDataManager = gameDataManager;
            _currentUsername = currentUsername;
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
            // Check for collision with obstacles BEFORE updating the ball's position
            for (int i = Obstacles.Count - 1; i >= 0; i--)
            {
                var obstacle = Obstacles[i];
                if (IsCollidingWithObstacle(Ball, obstacle))
                {
                    // Remove the obstacle
                    Obstacles.RemoveAt(i);

                    // Change ball direction based on collision
                    if (Ball.X + Ball.Size >= obstacle.X && Ball.X <= obstacle.X + obstacle.Width)
                    {
                        Ball.SpeedY = -Ball.SpeedY; // Vertical collision
                    }
                    else
                    {
                        Ball.SpeedX = -Ball.SpeedX; // Horizontal collision
                    }

                    break; // Only handle one collision per frame
                }
            }

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

        // Helper method to check collision between ball and obstacle
        private bool IsCollidingWithObstacle(BallModel ball, Obstacle obstacle)
        {
            // Check if the ball's rectangle intersects with the obstacle's rectangle
            return ball.X < obstacle.X + obstacle.Width &&
                   ball.X + ball.Size > obstacle.X &&
                   ball.Y < obstacle.Y + obstacle.Height &&
                   ball.Y + ball.Size > obstacle.Y;
        }

        public void AddObstacle(double x, double y, double width, double height)
        {
            Obstacles.Add(new Obstacle { X = x, Y = y, Width = width, Height = height });
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
