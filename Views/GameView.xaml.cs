using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using BounceBall.Manager;
using BounceBall.Models;
using BounceBall.ViewModels;

namespace BounceBall.Views
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : Page, IKeyHandler
    {
        private GameViewModel _gameViewModel;
        private bool _isPaused;
        private bool _isMovingLeft;
        private bool _isMovingRight;
        public GameView(User CurrentUser, GameDataManager gameDataManager)
        {
            InitializeComponent();
            _gameViewModel = new GameViewModel(gameDataManager, CurrentUser.UserName);
            DataContext = _gameViewModel;
            this.SizeChanged += OnSizeChanged;
            CompositionTarget.Rendering += OnRendering;

            _gameViewModel.GameOver += OnGameOver;

            // Set initial canvas dimensions
            _gameViewModel.CanvasWidth = Playground.ActualWidth;
            _gameViewModel.CanvasHeight = Playground.ActualHeight;

            // Add obstacles after the layout is ready
            this.Loaded += (s, e) => AddObstacles();
        }

        private void AddObstacles()
        {
            var random = new Random();

            for (int i = 0; i < 10; i++) // Add 10 small obstacles
            {
                double x = random.Next(50, Math.Max(51, (int)_gameViewModel.CanvasWidth - 50));
                double y = random.Next(50, Math.Max(51, (int)_gameViewModel.CanvasHeight / 2));
                double width = random.Next(20, 30);
                double height = random.Next(20, 30);

                var obstacle = new Obstacle { X = x, Y = y, Width = width, Height = height };
                _gameViewModel.AddObstacle(x, y, width, height);

                // Render the obstacle on the canvas
                var rectangle = new System.Windows.Shapes.Rectangle
                {
                    Width = width,
                    Height = height,
                    Fill = Brushes.Black,
                };
                Canvas.SetLeft(rectangle, x);
                Canvas.SetTop(rectangle, y);
                Playground.Children.Add(rectangle);
            }
        }




        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            _gameViewModel.CanvasWidth = Playground.ActualWidth;
            _gameViewModel.CanvasHeight = Playground.ActualHeight;

            Console.WriteLine($"Canvas Resized: Width={Playground.ActualWidth}, Height={Playground.ActualHeight}");
        }

        public void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                _isMovingLeft = true;
            }
            else if (e.Key == Key.Right)
            {
                _isMovingRight = true;
            }
            else if (e.Key == Key.Escape)
            {
                if (_isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                    OpenSettings();
                }
            }
            else if (e.Key == Key.Enter && !_gameViewModel.IsRunning)
            {
                RestartGame();
            }
        }

        public void OnKeyUp(KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                _isMovingLeft = false;
            }
            else if (e.Key == Key.Right)
            {
                _isMovingRight = false;
            }
        }

        private void OnRendering(object sender, EventArgs e)
        {
            if (_isMovingLeft && _gameViewModel.Paddle.X > 0)
            {
                _gameViewModel.Paddle.X -= 5; // Adjust the speed as needed
            }
            if (_isMovingRight && _gameViewModel.Paddle.X < Playground.ActualWidth - _gameViewModel.Paddle.Width)
            {
                _gameViewModel.Paddle.X += 5; // Adjust the speed as needed
            }

        }

        private void PauseGame()
        {
            _isMovingLeft = false;
            _isMovingRight = false;
            _gameViewModel.Pause();
            _isPaused = true;
        }

        private void ResumeGame()
        {
            _gameViewModel.Resume();
            _isPaused = false;
        }

        private void OpenSettings()
        {
            var settingsView = new SettingsView { DataContext = new SettingsViewModel(_gameViewModel.Ball, _gameViewModel.Paddle) };
            var settingsWindow = new Window
            {
                WindowStyle = WindowStyle.None,
                Content = settingsView,
                Width = 400,
                Height = 400,
                Title = "Settings"
            };
            //settingsWindow.Owner = this;
            //settingsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            settingsWindow.Loaded += (s, e) =>
            {
                var storyboard = (Storyboard)settingsView.FindResource("PauseAnimation");
                storyboard.Begin();
            };

            settingsWindow.KeyDown += (s, e) =>
            {
                if (e.Key == Key.Escape)
                {
                    var storyboard = (Storyboard)settingsView.FindResource("ResumeAnimation");
                    storyboard.Completed += (s2, e2) =>
                    {
                        settingsWindow.Close();
                        ResumeGame();
                    };
                    storyboard.Begin(settingsView);
                }
            };

            settingsWindow.ShowDialog();
        }


        private void RestartGame()
        {
            AddObstacles();
            _gameViewModel.Restart();
        }

        private void OnGameOver(object sender, EventArgs e)
        {
            _isMovingLeft = false;
            _isMovingRight = false;
        }
    }
}
