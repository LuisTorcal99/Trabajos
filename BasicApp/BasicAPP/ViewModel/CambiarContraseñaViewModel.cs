using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BasicAPP.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BasicAPP.ViewModel
{
    public partial class CambiarContraseñaViewModel : ViewModelBase
    {
        [ObservableProperty]
        public string? _Correo;

        [ObservableProperty]
        public string? _Password;

        [ObservableProperty]
        public string? _PasswordEquals;

        [ObservableProperty]
        public string _Barra;

        [ObservableProperty]
        public string _ValorBarra;

        public CambiarContraseñaViewModel()
        {
            Barra = "hidden";
        }

        [RelayCommand]
        public async Task Cambiar()
        {
            if (string.IsNullOrEmpty(Correo) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(PasswordEquals))
            {
                MessageBox.Show(Constantes.ERROR_CAMPOSNULL);
                return;
            }
            if (!Password.Equals(PasswordEquals))
            {
                MessageBox.Show(Constantes.ERROR_PASSWORDEQUALS);
                return;
            }

            Barra = "visible";
            for(int i = 0; i <= 100; i++)
            {
                ValorBarra = i.ToString();
                await Task.Delay(50);
            }
            MessageBox.Show(Constantes.CAMBIOS_CONTRASEÑA);
        }

        public override Task LoadAsync()
        {
            return base.LoadAsync();
        }
    }
}
