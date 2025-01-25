using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PokeRogue.Interfaces;
using PokeRogue.Service;
using PokeRogue.Services;
using PokeRogue.ViewModel;

namespace PokeRogue
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = Current.Services.GetService<MainWindow>();
            mainWindow?.Show();
        }
        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            //view principal
            services.AddTransient<MainWindow>();

            //view viewModels
            services.AddTransient<MainViewModel>();
            services.AddTransient<BattleViewModel>();
            services.AddTransient<HistoricoViewModel>();
            services.AddTransient<TeamViewModel>();
            services.AddTransient<ImportViewModel>();

            //Services
            services.AddSingleton<ICombateProvider, CombateService>();
            services.AddSingleton<ITeamProvider, TeamService>();
            services.AddSingleton(typeof(IApiProvider<>), typeof(ApiService<>));
            services.AddSingleton(typeof(IHistoricoProvider<>), typeof(HistoricoService<>));
            services.AddSingleton(typeof(IHttpJsonClientProvider<>), typeof(HttpJsonClientService<>));

            return services.BuildServiceProvider();
        }
    }

}
