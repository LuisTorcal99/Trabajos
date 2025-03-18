using System.Configuration;
using System.Data;
using System.Windows;
using ExamenFinal.DTO;
using ExamenFinal.Interfaces;
using ExamenFinal.Service;
using ExamenFinal.ViewModel;
using ExamenFinal.ViewModel.popUps;
using Microsoft.Extensions.DependencyInjection;

namespace ExamenFinal
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
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegistroViewModel>();
            services.AddTransient<PrincipalViewModel>();
            services.AddTransient<GridViewModel>();
            services.AddTransient<VentanaDesplegableViewModel>();
            services.AddTransient<ObjetoOverviewModel>();
            //services.AddTransient<PopUpViewModel>();
            services.AddTransient<AddObjetoDosViewModel>();
            services.AddTransient<AddObjetoTresViewModel>();
            //services.AddTransient<DetallesViewModel>();

            //Services 
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<LoginDTO>();
            services.AddSingleton<IObjetoApiProvider, ObjetoApiService>();
            services.AddSingleton<IObjetoDosApiProvider, ObjetoDosApiService>();
            services.AddSingleton<IObjetoTresApiProvider, ObjetoTresApiService>();
            services.AddSingleton(typeof(IFileProvider<>), typeof(FileService<>));
            services.AddSingleton(typeof(IHttpsJsonClientProvider<>), typeof(HttpsJsonClientService<>));

            return services.BuildServiceProvider();
        }
    }

}
