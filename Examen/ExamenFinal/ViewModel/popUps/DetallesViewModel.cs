//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using ExamenFinal.DTO;
//using ExamenFinal.Models;
//using CommunityToolkit.Mvvm.ComponentModel;
//using ExamenFinal.Interfaces;
//using System.Windows;
//using ExamenFinal.ViewModel.Main;

//namespace ExamenFinal.ViewModel.popUps
//{
//    public partial class DetallesViewModel : ViewModelBase
//    {
//        private readonly IObjetoApiProvider _objetoApiService;
//        private readonly IObjetoDosApiProvider _objetoDosApiService;
//        private readonly IObjetoTresApiProvider _objetoTresApiService;

//        [ObservableProperty]
//        private ObjetoDTO? _Objeto;

//        [ObservableProperty]
//        private int _TotalObjetosRelacionados;

//        [ObservableProperty]
//        private List<ObjetoRelacionModel> _ObjetosAgrupados;

//        [ObservableProperty]
//        private List<ItemObjetoDosModel> _ObjetosRelacionadosDos;

//        [ObservableProperty]
//        private List<ItemObjetoTresModel> _ObjetosRelacionadosTres;

//        public DetallesViewModel(IObjetoApiProvider objetoApiService, IObjetoDosApiProvider objetoDosApiService, IObjetoTresApiProvider objetoTresApiService)
//        {
//            _objetoApiService = objetoApiService;
//            _objetoDosApiService = objetoDosApiService;
//            _objetoTresApiService = objetoTresApiService;
//        }

//        public async Task SetIdObjeto(int id)
//        {
//            await CargarDetalles(id.ToString());
//        }

//        public async Task CargarDetalles(string id)
//        {
//            try
//            {
//                Objeto = await _objetoApiService.GetOneObjeto(id);

//                // Cargar los objetos relacionados
//                var objetosDos = (await _objetoDosApiService.GetObjetoDos()).Select(ItemObjetoDosModel.CreateModelFromDTO).ToList();
//                var objetosTres = (await _objetoTresApiService.GetObjetoTres()).Select(ItemObjetoTresModel.CreateModelFromDTO).ToList();

//                var objetosRelacionadosDos = objetosDos
//                    .Where(objetoDos => Objeto.IdsObjetoDos.Contains(objetoDos.Id))
//                    .ToList();
//                var objetosRelacionadosTres = objetosTres
//                    .Where(objetoTres => Objeto.IdsObjetoTres.Contains(objetoTres.Id))
//                    .ToList();

//                // Combinar los objetos de dos y tres
//                var ObjetosCombinados = objetosRelacionadosDos
//                    .Select(o => new { o.Id, o.Name })
//                    .Concat(objetosRelacionadosTres.Select(o => new { o.Id, o.Name }))
//                    .ToList();

//                // Contar el total de objetos relacionados
//                TotalObjetosRelacionados = ObjetosCombinados.Count();

//                // Relacionar los objetos
//                ObjetosRelacionadosDos = objetosRelacionadosDos;
//                ObjetosRelacionadosTres = objetosRelacionadosTres;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error al cargar los detalles: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }

//        public override Task LoadAsync()
//        {
//            return base.LoadAsync();
//        }
//    }

//    public class ObjetoRelacionModel
//    {
//        public int Id { get; set; }
//        public string Nombre { get; set; }
//        public int Cantidad { get; set; }
//    }
//}
