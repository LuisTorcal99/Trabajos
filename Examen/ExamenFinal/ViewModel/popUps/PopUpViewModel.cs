//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using CommunityToolkit.Mvvm.Input;
//using System.Windows;
//using ExamenFinal.Interfaces;
//using ExamenFinal.DTO;
//using ExamenFinal.View;
//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Messaging;
//using Microsoft.Extensions.DependencyInjection;
//using ExamenFinal.ViewModel.Main;
//using ExamenFinal.Utils;
//using ExamenFinal.Models;

//namespace ExamenFinal.ViewModel
//{
//    public partial class PopUpViewModel : ViewModelBase
//    {
//        [ObservableProperty]
//        private string _name;

//        [ObservableProperty]
//        private bool _boolOption;

//        [ObservableProperty]
//        private string _imagen;

//        [ObservableProperty]
//        private string _idsObjetoDosTexto;

//        [ObservableProperty]
//        private string _idsObjetoTresTexto;

//        private readonly IObjetoApiProvider _objetoApiService;
//        private readonly IObjetoDosApiProvider _objetoDosApiService;
//        private readonly IObjetoTresApiProvider _objetoTresApiService;

//        public PopUpViewModel(IObjetoApiProvider objetoApiService, IObjetoDosApiProvider objetoDosApiService, IObjetoTresApiProvider objetoTresApiService)
//        {
//            _objetoApiService = objetoApiService;
//            _objetoDosApiService = objetoDosApiService;
//            _objetoTresApiService = objetoTresApiService;
//        }

//        public override Task LoadAsync()
//        {
//            return base.LoadAsync();
//        }

//        /// <summary>
//        /// Convierte los valores de texto en listas de enteros
//        /// </summary>
//        public List<int> IdsObjetoDos => ConvertirStringALista(IdsObjetoDosTexto);
//        public List<int> IdsObjetoTres => ConvertirStringALista(IdsObjetoTresTexto);

//        private List<int> ConvertirStringALista(string texto)
//        {
//            return texto?.Split(',')
//                        .Select(id => int.TryParse(id.Trim(), out int result) ? result : (int?)null)
//                        .Where(id => id.HasValue)
//                        .Select(id => id.Value)
//                        .ToList() ?? new List<int>();
//        }

//        [RelayCommand]
//        private async Task AceptarVentana(object? parameter)
//        {
//            if (string.IsNullOrEmpty(Name))
//            {
//                MessageBox.Show(Constantes.ERROR_CAMPOSNULL);
//                return;
//            }

//            if (IdsObjetoDos.Count > 0)
//            {
//                var objetos2 = await _objetoDosApiService.GetObjetoDos();
//                bool idEncontrado2 = false;

//                if (objetos2 != null)
//                {
//                    foreach (var objeto in objetos2)
//                    {
//                        if (IdsObjetoDos.Contains(objeto.Id))
//                        {
//                            idEncontrado2 = true;
//                            break;
//                        }
//                    }

//                    if (!idEncontrado2)
//                    {
//                        MessageBox.Show(Constantes.ID_ERRONEO);
//                        return;
//                    }
//                }
//            }

//            if (IdsObjetoTres.Count > 0)
//            {
//                var objetos3 = await _objetoTresApiService.GetObjetoTres();
//                bool idEncontrado3 = false;

//                if (objetos3 != null)
//                {
//                    foreach (var objeto in objetos3)
//                    {
//                        if (IdsObjetoTres.Contains(objeto.Id))
//                        {
//                            idEncontrado3 = true;
//                            break; 
//                        }
//                    }

//                    if (!idEncontrado3)
//                    {
//                        MessageBox.Show(Constantes.ID_ERRONEO);
//                        return;
//                    }
//                }
//            }

//            try
//            {
//                ObjetoDTO PostObjeto = new ObjetoDTO
//                {
//                    Name = Name,
//                    BoolOption = BoolOption,
//                    Imagen = Imagen,
//                    IdsObjetoDos = IdsObjetoDos,
//                    IdsObjetoTres = IdsObjetoTres
//                };

//                await _objetoApiService.PostObjeto(PostObjeto);

//                var objetos = await _objetoApiService.GetObjeto();
//                var objetoCreado = objetos?.LastOrDefault();

//                // Actualizar ObjetoDos si es necesario
//                if (IdsObjetoDos.Count > 0)
//                {
//                    var objetoDos = await _objetoDosApiService.GetOneObjetoDos(IdsObjetoDos.First().ToString());
//                    if (objetoDos != null)
//                    {
//                        objetoDos.IdObjeto = objetoCreado.Id; 
//                        await _objetoDosApiService.PatchObjetoDos(objetoDos);
//                    }
//                }

//                // Actualizar ObjetoTres si es necesario
//                foreach (var idObjetoTres in IdsObjetoTres)
//                {
//                    var objetoTres = await _objetoTresApiService.GetOneObjetoTres(idObjetoTres.ToString());
//                    if (objetoTres != null && !objetoTres.IdObjeto.Contains(objetoCreado.Id))
//                    {
//                        objetoTres.IdObjeto.Add(objetoCreado.Id);
//                        await _objetoTresApiService.PatchObjetoTres(objetoTres);
//                    }
//                }

//                MessageBox.Show(Constantes.MSG_PERFECT);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//            }
//        }

//        [RelayCommand]
//        private void CancelarVentana(object? parameter)
//        {
//            CerrarVentana();
//        }

//        private void CerrarVentana()
//        {
//            Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w is PopUpView)?.Close();
//        }
//    }
//}
