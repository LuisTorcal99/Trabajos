using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using WPF_CHEAT_os.DTO;
using WPF_CHEAT_os.Interfaces;
using WPF_CHEAT_os.Models;
using WPF_CHEAT_os.Services;
using WPF_CHEAT_os.Utils;

namespace WPF_CHEAT_os.ViewModel
{
    public partial class VerPropuestaViewModel : ViewModelBase
    {
        private readonly IPropuestaProvider _propuestaProvider;
        private readonly IUsuarioProvider _usuarioService;

        [ObservableProperty]
        private ProfesorModel? profesor1;

        [ObservableProperty]
        private ProfesorModel? profesor2;

        [ObservableProperty]
        private ProfesorModel? profesor3;

        [ObservableProperty]
        private PropuestaDTO? propuesta;

        [ObservableProperty]
        private EstadoPropuesta estadoSeleccionado;

        [ObservableProperty]
        private ObservableCollection<ProfesorModel> profesores = new();

        public VerPropuestaViewModel(IPropuestaProvider propuestaProvider, IUsuarioProvider usuarioService)
        {
            _propuestaProvider = propuestaProvider ?? throw new ArgumentNullException(nameof(propuestaProvider));
            _usuarioService = usuarioService ?? throw new ArgumentNullException(nameof(usuarioService));
        }

        public async Task SetIdObjeto(int id)
        {
            await CargarPropuesta(id.ToString());
            await CargarProfesores();

            if (Propuesta?.Users != null)
            {
                Profesor1 = Propuesta.Users.ElementAtOrDefault(0);
                Profesor2 = Propuesta.Users.ElementAtOrDefault(1);
                Profesor3 = Propuesta.Users.ElementAtOrDefault(2);
            }
            else
            {
                Profesor1 = null;
                Profesor2 = null;
                Profesor3 = null;
            }
        }

        [RelayCommand]
        private async Task Aceptar()
        {
            if (Propuesta == null) return;
            propuesta.Estado = "Aceptada";
            await GuardarYNotificar("La propuesta ha sido marcada como Aceptada.");
        }

        [RelayCommand]
        private async Task Rechazar()
        {
            if (Propuesta == null) return;
            propuesta.Estado = "Rechazada";
            await GuardarYNotificar("La propuesta ha sido marcada como Rechazada.");
        }

        [RelayCommand]
        private async Task RequerirAmpliacion()
        {
            if (Propuesta == null) return;
            propuesta.Estado = "Requiere Ampliacion";
            await GuardarYNotificar("La propuesta ha sido marcada como Requiere Ampliación.");
        }

        [RelayCommand]
        private async Task Save()
        {
            if (Propuesta == null) return;

            if (Propuesta.Users == null || !(Propuesta.Users is List<ProfesorModel>))
            {
                Propuesta.Users = new List<ProfesorModel> { null, null, null };
            }

            var usersList = (List<ProfesorModel>)Propuesta.Users;

            // Actualizar solo si hay cambios
            if (!object.Equals(usersList[0], Profesor1)) usersList[0] = Profesor1;
            if (!object.Equals(usersList[1], Profesor2)) usersList[1] = Profesor2;
            if (!object.Equals(usersList[2], Profesor3)) usersList[2] = Profesor3;

            await GuardarYNotificar("Propuesta actualizada con éxito.");
        }

        private async Task GuardarYNotificar(string mensaje)
        {
            await _propuestaProvider.UpdateAsync(Propuesta);
            MessageBox.Show(mensaje, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public async Task CargarPropuesta(string id)
        {
            try
            {
                Propuesta = await _propuestaProvider.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la propuesta: {ex.Message}");
            }
        }

        private async Task CargarProfesores()
        {
            try
            {
                var profesoresData = await _usuarioService.GetGetUsuarioDTOAsync();

                Profesores.Clear();
                foreach (var profesor in profesoresData)
                {
                    if (profesor.Rol.Equals(Constants.ROLE_REGISTRER_PROFESOR))
                    {
                        Profesores.Add(new ProfesorModel { Nombre = profesor.Name });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los profesores: {ex.Message}");
            }
        }

        public override Task LoadAsync()
        {
            return base.LoadAsync();
        }
    }
}