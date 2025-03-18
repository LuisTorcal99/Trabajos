using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using WPF_CHEAT_os.DTO;
using WPF_CHEAT_os.Interfaces;
using WPF_CHEAT_os.Utils;

namespace WPF_CHEAT_os.ViewModel
{
    public partial class RegistroViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _Nombre;

        [ObservableProperty]
        private string _Apellido;

        [ObservableProperty]
        private string _Email;

        [ObservableProperty]
        private string _Password;

        [ObservableProperty]
        private string _ConfirmPassword;

        private readonly IHttpsJsonClientProvider<UserDTO> _httpJsonProvider;

        public RegistroViewModel(IHttpsJsonClientProvider<UserDTO> httpJsonProvider)
        {
            _httpJsonProvider = httpJsonProvider;
        }

        [RelayCommand]
        private async Task Registro()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
            {
                MessageBox.Show(Constants.ERROR_CAMPOSNULL, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!IsValidEmail(Email))
            {
                MessageBox.Show("El correo electrónico no es válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!Password.Equals(ConfirmPassword))
            {
                MessageBox.Show(Constants.ERROR_PASSWORDEQUALS, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                RegistroDTO usuarioRegistrado = new RegistroDTO(
                    Nombre, Apellido, Email, Password, Constants.ROLE_REGISTRER_PROFESOR
                );

                UserDTO user = await _httpJsonProvider.RegisterPostAsync(Constants.REGISTER_PATH, usuarioRegistrado);

                MessageBox.Show("Usuario registrado con éxito", "Registro", MessageBoxButton.OK, MessageBoxImage.Information);
                App.Current.Services.GetService<MainViewModel>().SelectedViewModel =
                App.Current.Services.GetService<MainViewModel>().LoginViewModel;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private void Login()
        {
            var mainViewModel = App.Current.Services.GetService<MainViewModel>();
            var loginViewModel = App.Current.Services.GetService<LoginViewModel>();

            mainViewModel.SelectViewModelCommand.Execute(loginViewModel);
        }

        public override Task LoadAsync()
        {
            return base.LoadAsync();
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
