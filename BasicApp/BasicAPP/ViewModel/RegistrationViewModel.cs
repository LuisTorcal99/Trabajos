using BasicAPP.DTO;
using BasicAPP.Interfaces;
using BasicAPP.Service;
using BasicAPP.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginRegister.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace BasicAPP.ViewModel
{
    public partial class RegistrationViewModel : ViewModelBase
    {
        private readonly IHttpsJsonClientProvider<UserDTO> _httpJsonProvider;

        [ObservableProperty]
        public string _Nombre;

        [ObservableProperty]
        public string _Correo;

        [ObservableProperty]
        public string _Password;

        [ObservableProperty]
        public string _PasswordEquals;

        [ObservableProperty]
        public string _Error;

        [ObservableProperty]
        public bool _Terminos;

        public RegistrationViewModel(IHttpsJsonClientProvider<UserDTO> httpJsonProvider)
        {
            _httpJsonProvider = httpJsonProvider;
        }

        [RelayCommand]
        private async Task RegisterNowAsync()
        {
            if (string.IsNullOrEmpty(Correo) 
                || string.IsNullOrEmpty(Password) 
                || string.IsNullOrEmpty(PasswordEquals) 
                || string.IsNullOrEmpty(Nombre))
            {
                MessageBox.Show(Constantes.ERROR_CAMPOSNULL);
                Error = Constantes.ERROR_CAMPOSNULL;
                return;
            }
            if (!Terminos)
            {
                MessageBox.Show(Constantes.ERROR_TYC);
                Error = Constantes.ERROR_TYC;
                return;
            }

            if (!Password.Equals(PasswordEquals))
            {
                MessageBox.Show(Constantes.ERROR_PASSWORDEQUALS);
                Error = Constantes.ERROR_PASSWORDEQUALS;
                return;
            }

            try
            {
                RegistroDTO UsuarioRegistrado = new RegistroDTO(
                    Nombre, Nombre, Correo, Password, Constantes.ROLE_REGISTRER_ADMIN
                );

                UserDTO user = await _httpJsonProvider.RegisterPostAsync(Constantes.REGISTER_PATH, UsuarioRegistrado);

                MessageBox.Show("Usuario registrado con éxito", "Registro", MessageBoxButton.OK, MessageBoxImage.Information);
                App.Current.Services.GetService<MainViewModel>().SelectedViewModel = App.Current.Services.GetService<MainViewModel>().LoginViewModel;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                MessageBox.Show(ex.Message);
            }
        }


        // Comando para navegar al login
        [RelayCommand]
        private void NavigateToLogin()
        {
            App.Current.Services.GetService<MainViewModel>().SelectedViewModel = App.Current.Services.GetService<MainViewModel>().LoginViewModel;
        }

        public override Task LoadAsync()
        {
            return Task.CompletedTask;
        }
    }
}
