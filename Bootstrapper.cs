using System.Net.Http;
using System.Windows;
using BounceBall.Manager;
using BounceBall.ViewModels;
using Caliburn.Micro;

namespace BounceBall
{
    public class Bootstrapper : BootstrapperBase
    {
        protected SimpleContainer Container { get; set; }
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            var container = new SimpleContainer();
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5003/")
            };
            container.Singleton<HttpClient>();
            container.Singleton<IWindowManager, WindowManager>();

            container.Singleton<IEventAggregator, EventAggregator>();
            container.PerRequest<FileHandler>();
            container.Singleton<GameDataManager>();
            container.Singleton<UserManager>();
            container.PerRequest<ShellViewModel>();
            container.PerRequest<LoginAndSignUpViewModel>();
            container.PerRequest<GameViewModel>();
            container.PerRequest<MenuViewModel>();
            container.PerRequest<ProfileViewModel>();
            container.PerRequest<SettingsViewModel>();
            container.PerRequest<LeaderboardViewModel>();

            Container = container;
        }

        protected override object GetInstance(Type service, string key)
        {
            return Container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return Container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            Container.BuildUp(instance);
        }

        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            await DisplayRootViewForAsync<ShellViewModel>();
        }

    }
}
