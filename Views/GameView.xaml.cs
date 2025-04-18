using System.Windows;
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
    public partial class GameView : Window
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
            this.KeyDown += OnKeyDown;
            this.KeyUp += OnKeyUp;
            this.SizeChanged += OnSizeChanged;
            CompositionTarget.Rendering += OnRendering;

            _gameViewModel.GameOver += OnGameOver;

            // Set initial canvas dimensions
            _gameViewModel.CanvasWidth = Playground.ActualWidth;
            _gameViewModel.CanvasHeight = Playground.ActualHeight;

        }


        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            _gameViewModel.CanvasWidth = Playground.ActualWidth;
            _gameViewModel.CanvasHeight = Playground.ActualHeight;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
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

        private void OnKeyUp(object sender, KeyEventArgs e)
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
            settingsWindow.Owner = this;
            settingsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

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
            _gameViewModel.Restart();
        }

        private void OnGameOver(object sender, EventArgs e)
        {
            _isMovingLeft = false;
            _isMovingRight = false;
        }
    }
}
