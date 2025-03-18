using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using WPF_CHEAT_os.DTO;
using WPF_CHEAT_os.Interfaces;
using WPF_CHEAT_os.Utils;

namespace WPF_CHEAT_os.ViewModel
{
    public partial class AnadirPopUpViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _Name;

        [ObservableProperty]
        private string _Apellidos;

        [ObservableProperty]
        private string _Email;

        [ObservableProperty]
        private string _Password;

        [ObservableProperty]
        private string _Rol;

        private readonly IHttpsJsonClientProvider<UserDTO> _httpJsonProvider;

        public AnadirPopUpViewModel(IHttpsJsonClientProvider<UserDTO> httpsJsonClient)
        {
            _httpJsonProvider = httpsJsonClient;
        }

        [RelayCommand]
        private async Task AddUser()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) 
                || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Apellidos))
            {
                MessageBox.Show(Constants.ERROR_CAMPOSNULL, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!IsValidEmail(Email))
            {
                MessageBox.Show("El correo electrónico no es válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                RegistroDTO usuarioRegistrado = new RegistroDTO(
                    Name, Apellidos, Email, Password, Rol
                );

                UserDTO user = await _httpJsonProvider.RegisterPostAsync(Constants.REGISTER_PATH, usuarioRegistrado);

                MessageBox.Show("Usuario registrado con éxito", "Registro", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        public override Task LoadAsync()
        {
            return base.LoadAsync();
        }
    }
}
