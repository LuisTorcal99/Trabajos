using System;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using ExamenFinal.DTO;
using ExamenFinal.Interfaces;
using ExamenFinal.Utils;
using ExamenFinal.View;
using System.Xml.Linq;
using ExamenFinal.ViewModel.Main;
using ExamenFinal.Service;

namespace ExamenFinal.ViewModel.popUps
{
    public partial class AddObjetoDosViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _Nombre;

        [ObservableProperty]
        private int _Precio;

        private readonly IObjetoDosApiProvider _objetoDosApiService;
        private readonly IObjetoApiProvider _objetoApiService;

        public AddObjetoDosViewModel(IObjetoDosApiProvider objetoDosApiService, IObjetoApiProvider objetoApiService)
        {
            _objetoDosApiService = objetoDosApiService;
            _objetoApiService = objetoApiService;
        }

        [RelayCommand]
        private async Task Aceptar()
        {
            // Validar que el nombre y el IdObjeto no sean nulos o cero
            if (string.IsNullOrEmpty(Nombre) || Precio <= 0)
            {
                MessageBox.Show(Constantes.ERROR_CAMPOSNULL);
                return;
            }

            //// Obtener la lista de objetos
            //var objetoUno = await _objetoApiService.GetObjeto();

            //// Validar que el IdObjeto exista en la lista de objetos
            //if (IdObjeto != 0 && objetoUno != null)
            //{
            //    bool idEncontrado = false;

            //    foreach (var objeto in objetoUno)
            //    {
            //        if (objeto.Id.Equals(IdObjeto))
            //        {
            //            idEncontrado = true;
            //            break;
            //        }
            //    }

            //    if (!idEncontrado)
            //    {
            //        MessageBox.Show(Constantes.ID_ERRONEO);
            //        return;
            //    }
            //}

            try
            {
                // Crear un nuevo ObjetoDosDTO
                var nuevoObjeto = new ObjetoDosDTO
                {
                    Nombre = Nombre,
                    Precio = Precio
                };

                // Enviar el nuevo objeto al servicio
                await _objetoDosApiService.PostObjetoDos(nuevoObjeto);

                //// Obtener todos los objetos de ObjetoDos
                //var objetos = await _objetoDosApiService.GetObjetoDos();
                //var objetoCreado = objetos?.LastOrDefault();

                //if (objetoCreado != null)
                //{
                //    var objetoOne = await _objetoApiService.GetOneObjeto(IdObjeto.ToString());
                //    if (objetoOne != null && !objetoOne.IdsObjetoDos.Contains(objetoCreado.Id))
                //    {
                //        objetoOne.IdsObjetoDos.Add(objetoCreado.Id);
                //        await _objetoApiService.PatchObjeto(objetoOne);
                //    }

                //    MessageBox.Show(Constantes.MSG_PERFECT);
                //    CerrarVentana();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        [RelayCommand]
        private void Cancelar()
        {
            CerrarVentana();
        }

        private void CerrarVentana()
        {
            Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w is AddObjetoDos)?.Close();
        }
    }
}
