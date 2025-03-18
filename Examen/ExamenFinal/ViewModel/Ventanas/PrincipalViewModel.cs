using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExamenFinal.DTO;
using ExamenFinal.Interfaces;
using ExamenFinal.Models;
using ExamenFinal.Service;
using ExamenFinal.Utils;
using ExamenFinal.View;
using ExamenFinal.ViewModel.Main;
using ExamenFinal.ViewModel.popUps;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;

namespace ExamenFinal.ViewModel
{
    public partial class PrincipalViewModel : ViewModelBase
    {
        private readonly IObjetoApiProvider _ObjetoService;
        private readonly IObjetoDosApiProvider _ObjetoDosService;
        private readonly IObjetoTresApiProvider _ObjetoTresService;
        private readonly IServiceProvider _serviceProvider;

        private readonly IHttpsJsonClientProvider<ObjetoDTO> _httpService;
        private readonly IFileProvider<ObjetoDTO> _fileService;

        private List<int> _IdsObjetoDos;
        private List<int> _IdsObjetoTres;

        [ObservableProperty]
        private ObservableCollection<OverViewModel> _Objeto1Items;
        [ObservableProperty]
        private ObservableCollection<ItemObjetoDosModel> _Objeto2Items;
        [ObservableProperty]
        private ObservableCollection<ItemObjetoTresModel> _Objeto3Items;

        public PrincipalViewModel(IObjetoApiProvider objetoApiService, IObjetoDosApiProvider objetoDosApiProvider, 
            IObjetoTresApiProvider ObjetoTresService, IServiceProvider serviceProvider, IFileProvider<ObjetoDTO> fileService, IHttpsJsonClientProvider<ObjetoDTO> httpService)
        {
            RecibirGrid = new ObservableCollection<ObjetoDTO>();
            GridDatos = new ObservableCollection<ObjetoDTO>();
            _httpService = httpService;

            _ObjetoTresService = ObjetoTresService;
            _ObjetoDosService = objetoDosApiProvider;
            _ObjetoService = objetoApiService;
            _serviceProvider = serviceProvider;

            //Objeto1Items = new ObservableCollection<OverViewModel>();
            //Objeto2Items = new ObservableCollection<ItemObjetoDosModel>();
            Objeto3Items = new ObservableCollection<ItemObjetoTresModel>();
        }

        public ObservableCollection<ObjetoDTO> RecibirGrid { get; set; }
        public ObservableCollection<ObjetoDTO> GridDatos { get; set; }

        public async Task CargarObjetos()
        {
            //Objeto1Items.Clear();
            //Objeto2Items.Clear();
            Objeto3Items.Clear();
            try
            {
                //var objetos = await _ObjetoService.GetObjeto();

                //if (objetos != null)
                //{
                //    foreach (var objeto in objetos)
                //    {
                //        Objeto1Items.Add(OverViewModel.CreateModelFromDTO(objeto));
                //    }
                //}

                //var objetos2 = await _ObjetoDosService.GetObjetoDos();

                //if (objetos2 != null)
                //{
                //    foreach (var objeto in objetos2)
                //    {
                //        Objeto2Items.Add(ItemObjetoDosModel.CreateModelFromDTO(objeto));
                //    }
                //}

                var objetos3 = await _ObjetoTresService.GetObjetoTres();

                if (objetos3 != null)
                {
                    foreach (var objeto in objetos3)
                    {
                        Objeto3Items.Add(ItemObjetoTresModel.CreateModelFromDTO(objeto));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(Constantes.MSG_ERROR);
            }
        }

        //[RelayCommand]
        //private async Task AddObjeto()
        //{
        //    var objetoDosApiService = new ObjetoDosApiService(new HttpsJsonClientService<ObjetoDosDTO>());
        //    var objetoTresApiService = new ObjetoTresApiService(new HttpsJsonClientService<ObjetoTresDTO>());
        //    var objetoApiService = new ObjetoApiService(new HttpsJsonClientService<ObjetoDTO>());

        //    var viewModel = new PopUpViewModel(
        //        objetoApiService,  
        //        objetoDosApiService,  
        //        objetoTresApiService
        //    );

        //    var view = new PopUpView { DataContext = viewModel };
        //    view.ShowDialog();
        //    LoadAsync();
        //}

        [RelayCommand]
        private async Task AddObjeto()
        {
            var objetoTresApiService = new ObjetoTresApiService(new HttpsJsonClientService<ObjetoTresDTO>());
            var objetoApiService = new ObjetoApiService(new HttpsJsonClientService<ObjetoDTO>());

            var viewModel = new AddObjetoTresViewModel(
                objetoTresApiService,
                objetoApiService
            );

            var view = new AddObjetoTres { DataContext = viewModel };
            view.ShowDialog();
            LoadAsync();
        }

        [RelayCommand]
        public async Task Importar()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = Constantes.JSON_FILTER
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Cargar los objetos desde el archivo
                var loadedObjects = _fileService.Load(openFileDialog.FileName);
                RecibirGrid = new ObservableCollection<ObjetoDTO>(loadedObjects);

                // Obtener los objetos actuales de la API y eliminarlos
                List<ObjetoDTO> existingObjects = (await _httpService.GetAsync(Constantes.OBJETO_PATH))?.ToList() ?? new List<ObjetoDTO>();
                foreach (var objeto in existingObjects)
                {
                    await _ObjetoService.DeleteObjeto(objeto.Id.ToString());
                }

                // Subir los nuevos objetos a la API
                foreach (ObjetoDTO objetoApi in RecibirGrid)
                {
                    ObjetoDTO nuevoObjeto = new ObjetoDTO
                    {
                        Nombre = objetoApi.Nombre,
                        Email = objetoApi.Email,
                    };
                    await _httpService.PostAsync(Constantes.OBJETO_PATH, nuevoObjeto);
                }
                LoadAsync();
            }
        }

        [RelayCommand]
        public async Task Exportar()
        {
            GridDatos.Clear();
            List<ObjetoDTO> objetos = (await _httpService.GetAsync(Constantes.OBJETO_PATH))?.ToList() ?? new List<ObjetoDTO>();

            foreach (ObjetoDTO objeto in objetos)
            {
                GridDatos.Add(objeto);
            }

            var saveFileDialog = new SaveFileDialog
            {
                Filter = Constantes.JSON_FILTER
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                _fileService.Save(saveFileDialog.FileName, GridDatos);
            }
        }

        //[RelayCommand]
        //private async Task AddObjetoTres()
        //{
        //    var objetoTresApiService = new ObjetoTresApiService(new HttpsJsonClientService<ObjetoTresDTO>());
        //    var objetoApiService = new ObjetoApiService(new HttpsJsonClientService<ObjetoDTO>());

        //    var viewModel = new AddObjetoTresViewModel(
        //        objetoTresApiService, 
        //        objetoApiService
        //    );

        //    var view = new AddObjetoTres { DataContext = viewModel };
        //    view.ShowDialog();
        //    LoadAsync();
        //}

        //[RelayCommand]
        //private async Task ItemClick(int Id)
        //{
        //    var viewModel = _serviceProvider.GetRequiredService<DetallesViewModel>();
        //    await viewModel.SetIdObjeto(Id);

        //    var view = new DetallesView { DataContext = viewModel };
        //    view.ShowDialog();

        //    await LoadAsync();
        //}

        public override async Task LoadAsync()
        {
            await CargarObjetos();
        }
    }
}
