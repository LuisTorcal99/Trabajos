using BasicAPP.DTO;
using BasicAPP.Interfaces;
using BasicAPP.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.IO;
using BasicAPP.Utils;
using BasicAPP.View;

namespace BasicAPP.ViewModel
{
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly MainViewModel _mainViewModel;

        [ObservableProperty]
        public string _Correo;

        [ObservableProperty]
        public string _Password;

        [ObservableProperty]
        public string _Error;

        [ObservableProperty]
        public bool _remember;

        [ObservableProperty]
        public string _ValorarApp;

        [ObservableProperty]
        public string _ValorApp;

        public LoginViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ValorarApp = "hidden";

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
            if (string.IsNullOrEmpty(Correo) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show(Constantes.ERROR_CAMPOSNULL);
                Error = Constantes.ERROR_CAMPOSNULL;    
                return;
            }
            try
            {
                IHttpsJsonClientProvider<LoginDTO> httpsJsonClient = new HttpsJsonClientService<LoginDTO>(Constantes.BASE_URL);

                LoginDTO UsuarioLogueado = new LoginDTO(
                    Correo, Password
                );

                await httpsJsonClient.Post(Path.Combine(Constantes.BASE_URL, Constantes.LOGIN_PATH), UsuarioLogueado);

                _mainViewModel.SelectedViewModel = _mainViewModel.DatosGridViewModel;

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
            _mainViewModel.SelectedViewModel = _mainViewModel.RegistrationViewModel;
        }

        public override async Task LoadAsync()
        {
            await Task.Delay(5000);
            ValorarApp = "visible";
            for (int i = 0; i <= 5; i++)
            {
                ValorApp = i.ToString();
                await Task.Delay(1000);
            }
        }
    }
}
