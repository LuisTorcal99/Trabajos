using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WPF_CHEAT_os.DTO;
using WPF_CHEAT_os.Interfaces;
using WPF_CHEAT_os.Models;
using WPF_CHEAT_os.Services;
using WPF_CHEAT_os.Utils;
using WPF_CHEAT_os.View;

namespace WPF_CHEAT_os.ViewModel
{
    public partial class GestionViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<UsuariosGridModel> _ListaAlumnos;

        [ObservableProperty]
        private ObservableCollection<UsuariosGridModel> _ListaProfesores;

        [ObservableProperty]
        private GetUsuarioDTO _SelectedUsuario;

        private IUsuarioProvider _UsuarioService;

        public GestionViewModel(IUsuarioProvider usuarioProvider)
        {
            ListaAlumnos = new ObservableCollection<UsuariosGridModel>();
            ListaProfesores = new ObservableCollection<UsuariosGridModel>();

            _UsuarioService = usuarioProvider;
        }

        [RelayCommand]
        public async void AnadirUsuario()
        {
            var viewModel = new AnadirPopUpViewModel(new HttpsJsonClientService<UserDTO>());
            var view = new AnadirPopUpView();
            view.DataContext = viewModel;
            view.ShowDialog();
            await LoadAsync();
        }
        public override async Task LoadAsync()
        {
            var usuarios = await _UsuarioService.GetGetUsuarioDTOAsync();

            ListaAlumnos.Clear();
            ListaProfesores.Clear();

            if (usuarios != null)
            {
                foreach (var usuario in usuarios)
                {
                    if (usuario.Rol.Equals(Constants.ROLE_REGISTRER_ALUMNO))
                    {
                        ListaAlumnos.Add(UsuariosGridModel.CreateModelFromDTO(usuario));
                    }
                    else if (usuario.Rol.Equals(Constants.ROLE_REGISTRER_PROFESOR))
                    {
                        ListaProfesores.Add(UsuariosGridModel.CreateModelFromDTO(usuario));
                    }
                }
            }
        }
    }
}
