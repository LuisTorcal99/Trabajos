using BasicAPP.DTO;
using BasicAPP.Interfaces;
using BasicAPP.Service;
using BasicAPP.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace BasicAPP.ViewModel
{
    public partial class RegistrationViewModel : ViewModelBase
    {
        private readonly MainViewModel _mainViewModel;

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

        public RegistrationViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        [RelayCommand]
        private async Task RegisterNowAsync()
        {
            if (string.IsNullOrEmpty(Correo) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(PasswordEquals) || string.IsNullOrEmpty(Nombre))
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
                IHttpsJsonClientProvider<RegistroDTO> httpsJsonClient = new HttpsJsonClientService<RegistroDTO>(Constantes.BASE_URL);

                RegistroDTO UsuarioRegistrado = new RegistroDTO(
                    Nombre, Nombre, Correo, Password, Constantes.ROLE_REGISTRER_ADMIN
                );

                await httpsJsonClient.Post(Path.Combine(Constantes.BASE_URL, Constantes.REGISTER_PATH), UsuarioRegistrado);

                MessageBox.Show("Usuario registrado con éxito", "Registro", MessageBoxButton.OK, MessageBoxImage.Information);
                _mainViewModel.SelectedViewModel = _mainViewModel.LoginViewModel;
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
            _mainViewModel.SelectedViewModel = _mainViewModel.LoginViewModel;
        }

        public override Task LoadAsync()
        {
            // Cargar lógica específica del registro si es necesario
            return Task.CompletedTask;
        }
    }
}
