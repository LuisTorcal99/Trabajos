using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using WPF_CHEAT_os.DTO;
using WPF_CHEAT_os.Interfaces;
using WPF_CHEAT_os.Utils;

namespace WPF_CHEAT_os.ViewModel
{
    public partial class LoginViewModel : ViewModelBase
    {
        [ObservableProperty]
        public string _Email;

        [ObservableProperty]
        public string _Password;

        private readonly IHttpsJsonClientProvider<UserDTO> _httpJsonProvider;
        public LoginViewModel(IHttpsJsonClientProvider<UserDTO> httpJsonProvider)
        {
            _httpJsonProvider = httpJsonProvider;

            Email = "profe@gmail.com";
            Password = "@Alumn0123";
        }

        [RelayCommand]
        private async Task Login()
        {
            App.Current.Services.GetService<LoginDTO>().Email = Email;
            App.Current.Services.GetService<LoginDTO>().Password = Password;

            try
            {
                UserDTO user = await _httpJsonProvider.LoginPostAsync(Constants.LOGIN_PATH, App.Current.Services.GetService<LoginDTO>());

                if (user != null && user.Result != null && !string.IsNullOrEmpty(user.Result.Token))
                {
                    App.Current.Services.GetService<LoginDTO>().Token = user.Result.Token;

                    // Cambiar de vista
                    var mainViewModel = App.Current.Services.GetService<MainViewModel>();

                    var principalViewModel = App.Current.Services.GetService<PrincipalViewModel>();
                    mainViewModel.SelectViewModelCommand.Execute(principalViewModel);
                }
                else
                {
                    MessageBox.Show("Error: Usuario o contraseña incorrectos.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        [RelayCommand]
        private void Registro()
        {
            var mainViewModel = App.Current.Services.GetService<MainViewModel>();
            var RegistroViewModel = App.Current.Services.GetService<RegistroViewModel>();

            mainViewModel.SelectViewModelCommand.Execute(RegistroViewModel);
        }

        public override Task LoadAsync()
        {
            return base.LoadAsync();
        }
    }
}
