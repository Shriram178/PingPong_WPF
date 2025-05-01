using System.Windows.Input;
using BounceBall.Command;
using BounceBall.Manager;
using Caliburn.Micro;

namespace BounceBall.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<NavigateToGameMessage>, IHandle<NavigateToMenuMessage>, IHandle<NavigateToProfileMessage>, IHandle<NavigateToLoginMessage>
    {
        private readonly IEventAggregator _events;

        public ICommand UpdateViewCommand { get; set; }
        public UserManager _userManager { get; set; }

        public ShellViewModel(IEventAggregator events, UserManager userManager)
        {
            _userManager = userManager;
            if (events == null)
                throw new ArgumentNullException(nameof(events));
            // Set initial view
            _events = events;
            _events.SubscribeOnPublishedThread(this);
            // Initialize command
            var login = IoC.Get<LoginAndSignUpViewModel>();
            if (login == null)
                throw new ArgumentNullException(nameof(login));
            //OnActivateAsync(new CancellationToken());
            //ActivateItemAsync(new LoginAndSignUpViewModel(_events, _userManager));
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            // Set initial view
            await ActivateItemAsync(IoC.Get<LoginAndSignUpViewModel>());
        }

        async Task IHandle<NavigateToGameMessage>.HandleAsync(NavigateToGameMessage message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(IoC.Get<GameViewModel>());
        }

        async Task IHandle<NavigateToMenuMessage>.HandleAsync(NavigateToMenuMessage message, CancellationToken cancellationToken)
        {
            if (_userManager.CurrentUser != null)
            {
                // Navigate to menu
                await ActivateItemAsync(IoC.Get<MenuViewModel>());
            }
        }

        async Task IHandle<NavigateToProfileMessage>.HandleAsync(NavigateToProfileMessage message, CancellationToken cancellationToken)
        {
            if (_userManager.CurrentUser != null)
            {
                // Navigate to profile
                await ActivateItemAsync(IoC.Get<ProfileViewModel>());
            }
        }

        async Task IHandle<NavigateToLoginMessage>.HandleAsync(NavigateToLoginMessage message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(IoC.Get<LoginAndSignUpViewModel>());
        }
    }
}
