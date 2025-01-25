using System.Configuration;
using System.Data;
using System.Windows;
using BasicAPP.Interfaces;
using BasicAPP.Service;
using BasicAPP.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace BasicAPP
{
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
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegistrationViewModel>();
            services.AddTransient<DatosGridViewModel>();
            services.AddTransient<AddItemViewModel>();
            services.AddTransient<CambiarContraseñaViewModel>();
            //Services 
            services.AddSingleton<IVolantesApiProvider, VolantesApiService>();
            services.AddSingleton(typeof(IHttpsJsonClientProvider<>), typeof(HttpsJsonClientService<>));

            return services.BuildServiceProvider();
        }
    }

}
