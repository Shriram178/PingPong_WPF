using System.Windows.Input;
using BounceBall.Command;
using BounceBall.Manager;

namespace BounceBall.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel _selectedViewModel;
        private readonly UserManager _userManager;
        private readonly GameDataManager _gameDataManager;
        public BaseViewModel SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public ICommand UpdateViewCommand { get; set; }

        public MainViewModel()
        {
            FileHandler fileHandler = new FileHandler();

            // Initialize dependencies
            _userManager = new UserManager(fileHandler);
            _gameDataManager = new GameDataManager(fileHandler);

            // Set initial view
            SelectedViewModel = new LoginAndSignUpViewModel(_userManager);

            // Initialize command
            UpdateViewCommand = new UpdateViewCommand(this, _userManager, _gameDataManager);
        }
    }
}
