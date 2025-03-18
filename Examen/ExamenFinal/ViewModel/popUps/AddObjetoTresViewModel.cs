using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using ExamenFinal.DTO;
using ExamenFinal.Interfaces;
using ExamenFinal.Utils;
using ExamenFinal.View;
using ExamenFinal.Service;
using ExamenFinal.ViewModel.Main;

namespace ExamenFinal.ViewModel.popUps
{
    public partial class AddObjetoTresViewModel : ViewModelBase
    {
        [ObservableProperty]
        private int _Usuario;

        [ObservableProperty]
        private string _Productos;

        private readonly IObjetoTresApiProvider _objetoTresApiService;
        private readonly IObjetoApiProvider _objetoApiService;

        public AddObjetoTresViewModel(IObjetoTresApiProvider objetoTresApiService, IObjetoApiProvider objetoApiService)
        {
            _objetoTresApiService = objetoTresApiService;
            _objetoApiService = objetoApiService;
        }

        // Convertir el texto de IdObjeto a una lista de enteros
        public List<int> Prod => ConvertirStringALista(Productos);

        private List<int> ConvertirStringALista(string texto)
        {
            return texto?.Split(',')
                        .Select(id => int.TryParse(id.Trim(), out int result) ? result : (int?)null)
                        .Where(id => id.HasValue)
                        .Select(id => id.Value)
                        .ToList() ?? new List<int>();
        }

        [RelayCommand]
        private async Task Aceptar()
        {
            // Verificar que los campos no estén vacíos
            if (Usuario == 0 || Productos.Length == 0)
            {
                MessageBox.Show(Constantes.ERROR_CAMPOSNULL);
                return;
            }

            // Validar si los IdsObjetos existen en la base de datos
            //if (Usuario != 0)
            //{
            //    var objetos = await _objetoApiService.GetObjeto();

            //    if (objetos != null)
            //    {
            //        foreach (var id in Usuario)
            //        {
            //            if (!objetos.Any(objeto => objeto.Id == id))
            //            {
            //                MessageBox.Show(Constantes.ID_ERRONEO);
            //                return;
            //            }
            //        }
            //    }
            //}

            try
            {
                // Crear el nuevo objeto de tipo ObjetoTresDTO
                var nuevoObjetoTres = new ObjetoTresDTO
            {
                Usuario = Usuario,
                Productos = Prod,
            };

                // Llamar al servicio para crear el nuevo objeto
                await _objetoTresApiService.PostObjetoTres(nuevoObjetoTres);

                var objetos = await _objetoTresApiService.GetObjetoTres();
                var objetoCreado = objetos?.LastOrDefault();

                //// Actualizar los objetos relacionados 
                //if (IdsObjetos.Count > 0)
                //{
                //    foreach (var idObjeto in IdsObjetos)
                //    {
                //        var objeto = await _objetoApiService.GetOneObjeto(idObjeto.ToString());

                //        if (objeto != null && !objeto.IdsObjetoTres.Contains(objetoCreado.Id))
                //        {
                //            objeto.IdsObjetoTres.Add(objetoCreado.Id); 
                //            await _objetoApiService.PatchObjeto(objeto); 
                //        }
                //    }
                //}

                MessageBox.Show(Constantes.MSG_PERFECT);
                CerrarVentana();
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
            Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w is AddObjetoTres)?.Close();
        }
    }
}
