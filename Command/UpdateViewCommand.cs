using System.Windows.Input;
using BounceBall.Manager;
using BounceBall.ViewModels;

namespace BounceBall.Command
{
    public class UpdateViewCommand : ICommand
    {
        private readonly MainViewModel _mainViewModel;
        private readonly UserManager _userManager;
        private readonly GameDataManager _gameDataManager;

        public UpdateViewCommand(MainViewModel mainViewModel, UserManager userManager, GameDataManager gameDataManager)
        {
            _mainViewModel = mainViewModel;
            _userManager = userManager;
            _gameDataManager = gameDataManager;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Login":
                    _mainViewModel.SelectedViewModel = new LoginAndSignUpViewModel(_userManager);
                    break;
                case "Profile":
                    if (_userManager.CurrentUser != null)
                    {
                        _mainViewModel.SelectedViewModel = new ProfileViewModel(_userManager.CurrentUser, _gameDataManager);
                    }
                    break;
                case "Game":
                    if (_userManager.CurrentUser != null)
                    {
                        _mainViewModel.SelectedViewModel = new GameViewModel(_gameDataManager, _userManager.CurrentUser.UserName);
                    }
                    break;
                case "Settings":
                    if (_mainViewModel.SelectedViewModel is GameViewModel gameViewModel)
                    {
                        _mainViewModel.SelectedViewModel = new SettingsViewModel(gameViewModel.Ball, gameViewModel.Paddle);
                    }
                    break;
                case "Menu":
                    _mainViewModel.SelectedViewModel = new MenuViewModel(_userManager.CurrentUser, _gameDataManager);
                    break;
            }
        }
    }
}
