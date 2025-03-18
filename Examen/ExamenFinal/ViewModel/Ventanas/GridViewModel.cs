using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
    public partial class GridViewModel : ViewModelBase
    {
        private readonly IFileProvider<ObjetoDosDTO> _fileService;
        private readonly IHttpsJsonClientProvider<ObjetoDosDTO> _httpService;
        private readonly IObjetoDosApiProvider _objetoDosApiService;

        private int _currentPage = 1;
        private readonly int _itemsPerPage = 2;

        public GridViewModel(IFileProvider<ObjetoDosDTO> fileService, IHttpsJsonClientProvider<ObjetoDosDTO> httpService, IObjetoDosApiProvider objetoDosApiService)
        {
            RecibirGrid = new ObservableCollection<ObjetoDosDTO>();
            GridDatos = new ObservableCollection<ObjetoDosDTO>();
            Items = new ObservableCollection<ItemObjetoDosModel>();
            PaginatedItems = new ObservableCollection<ItemObjetoDosModel>();
            _fileService = fileService;
            _httpService = httpService;
            _objetoDosApiService = objetoDosApiService;
        }

        [ObservableProperty]
        private ObservableCollection<ItemObjetoDosModel> _Items;

        [ObservableProperty]
        private ObservableCollection<ItemObjetoDosModel> _PaginatedItems;

        public ObservableCollection<ObjetoDosDTO> RecibirGrid { get; set; }
        public ObservableCollection<ObjetoDosDTO> GridDatos { get; set; }

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                SetProperty(ref _currentPage, value);
                RefreshPaginatedItems();
            }
        }

        public int PageCount => (Items.Count + _itemsPerPage - 1) / _itemsPerPage;

        private void RefreshPaginatedItems()
        {
            PaginatedItems.Clear();
            var items = Items.Skip((CurrentPage - 1) * _itemsPerPage).Take(_itemsPerPage).ToList();
            foreach (var item in items)
            {
                PaginatedItems.Add(item);
            }
        }

        [RelayCommand]
        public void MoveToNextPage()
        {
            if (CurrentPage < PageCount)
            {
                CurrentPage++;
            }
        }

        [RelayCommand]
        public void MoveToPreviousPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
            }
        }

        public async Task CargarObjetos()
        {
            Items.Clear();
            try
            {
                var objetos = await _objetoDosApiService.GetObjetoDos();

                if (objetos != null)
                {
                    foreach (var objeto in objetos)
                    {
                        Items.Add(ItemObjetoDosModel.CreateModelFromDTO(objeto));
                    }
                }
                RefreshPaginatedItems();
            }
            catch (Exception ex)
            {
                Console.WriteLine(Constantes.MSG_ERROR);
            }
        }

        [RelayCommand]
        private async Task Add()
        {
            var objetoDosApiService = new ObjetoDosApiService(new HttpsJsonClientService<ObjetoDosDTO>());
            var objetoApiService = new ObjetoApiService(new HttpsJsonClientService<ObjetoDTO>());

            var viewModel = new AddObjetoDosViewModel(
                objetoDosApiService,
                objetoApiService
            );

            var view = new AddObjetoDos { DataContext = viewModel };
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
                RecibirGrid = new ObservableCollection<ObjetoDosDTO>(loadedObjects);

                // Obtener los objetos actuales de la API y eliminarlos
                List<ObjetoDosDTO> existingObjects = (await _httpService.GetAsync(Constantes.OBJETO_DOS_PATH))?.ToList() ?? new List<ObjetoDosDTO>();
                foreach (var objeto in existingObjects)
                {
                    await _objetoDosApiService.DeleteObjetoDos(objeto.Id.ToString());
                }

                // Subir los nuevos objetos a la API
                foreach (ObjetoDosDTO objetoApi in RecibirGrid)
                {
                    ObjetoDosDTO nuevoObjeto = new ObjetoDosDTO
                    {
                        Nombre = objetoApi.Nombre,
                        Precio = objetoApi.Precio
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
            List<ObjetoDosDTO> objetos = (await _httpService.GetAsync(Constantes.OBJETO_DOS_PATH))?.ToList() ?? new List<ObjetoDosDTO>();

            foreach (ObjetoDosDTO objeto in objetos)
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

        public override async Task LoadAsync()
        {
            await CargarObjetos();
        }
    }
}