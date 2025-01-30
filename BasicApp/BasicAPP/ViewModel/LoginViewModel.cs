using BasicAPP.DTO;
using BasicAPP.Interfaces;
using BasicAPP.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.IO;
using BasicAPP.Utils;
using BasicAPP.View;
using LoginRegister.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Xml.Linq;
using Microsoft.VisualBasic;

namespace BasicAPP.ViewModel
{
    public partial class LoginViewModel : ViewModelBase
    {
        [ObservableProperty]
        public string _Correo;

        [ObservableProperty]
        public string _Password;

        [ObservableProperty]
        public string _Error;

        [ObservableProperty]
        public bool _remember;

        private readonly IHttpsJsonClientProvider<UserDTO> _httpJsonProvider;

        public LoginViewModel(IHttpsJsonClientProvider<UserDTO> httpJsonProvider)
        {
            _httpJsonProvider = httpJsonProvider;
            //Usuario y contraseña por velocidad
            Correo = "luis@gmail.com";
            Password = "gasdgSDG99A.";
            
        }

        [RelayCommand]
        private void ForgotPassword()
        {
            var viewModel = new CambiarContraseñaViewModel();
            var view = new CambiarContraseñaView { DataContext = viewModel };
            view.ShowDialog();
        }

        [RelayCommand]
        private async Task LoginNow()
        {
            App.Current.Services.GetService<LoginDTO>().Email = Correo;
            App.Current.Services.GetService<LoginDTO>().Password = Password;

            try
            {
                UserDTO user = await _httpJsonProvider.LoginPostAsync(Constantes.LOGIN_PATH, App.Current.Services.GetService<LoginDTO>());

                if (user != null && user.Result != null && !string.IsNullOrEmpty(user.Result.Token))
                {
                    App.Current.Services.GetService<LoginDTO>().Token = user.Result.Token;

                    // Cambiar de vista
                    App.Current.Services.GetService<MainViewModel>().SelectedViewModel =
                    App.Current.Services.GetService<DatosGridViewModel>();
                }
                else
                {
                    Error = "Error: Usuario o contraseña incorrectos.";
                    MessageBox.Show("Error: Usuario o contraseña incorrectos.");
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                MessageBox.Show(ex.Message);
            }

        }

        [RelayCommand]
        private void NavigateToRegistration()
        {
            var mainWindow = App.Current.Services.GetService<MainViewModel>();
            mainWindow.SelectedViewModel = App.Current.Services.GetService<MainViewModel>().RegistrationViewModel;
        }

        public override async Task LoadAsync()
        {

        }
    }
}
