using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace ExamenFinal.ViewModel
{
    public partial class VentanaDesplegableViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<ObjetoTresDTO> _items;
        private int _ObjetotId;
        private ObjetoOverviewModel _objetoOverviewModel;
        [ObservableProperty]
        private ObjetoTresDTO _Objeto;
        [ObservableProperty]

        private IObjetoTresApiProvider _objetoApiService;

        public VentanaDesplegableViewModel(IObjetoTresApiProvider objetoApiProvider)
        {
            _objetoApiService = objetoApiProvider;
            _items = new ObservableCollection<ObjetoTresDTO>();
        }

        public void SetIdObjeto(int id)
        {
            _ObjetotId = id;
        }

        //public string IdsObjetoDosString => Objeto.IdsObjetoDos.Any() ? $"IDs: {string.Join(" | ", Objeto.IdsObjetoDos)}" : "No hay IDs";
        //public string IdsObjetoTresString => Objeto.IdsObjetoTres.Any() ? $"IDs: {string.Join(" - ", Objeto.IdsObjetoTres)}" : "No hay IDs";

        public override async Task LoadAsync()
        {
            IEnumerable<ObjetoTresDTO> objetos = await _objetoApiService.GetObjetoTres();

            Items = new ObservableCollection<ObjetoTresDTO>(objetos);

            Objeto = objetos.FirstOrDefault(x => x.Id == _ObjetotId) ?? new ObjetoTresDTO();

            //OnPropertyChanged(nameof(IdsObjetoDosString));
            //OnPropertyChanged(nameof(IdsObjetoTresString));
        }


        internal void SetParentViewModel(ViewModelBase ObjetoOverviewModel)
        {
            if (ObjetoOverviewModel is ObjetoOverviewModel ObjetoOverview)
            {
                _objetoOverviewModel = ObjetoOverview;
            }
        }

        [RelayCommand]
        private async Task Delete()
        {
            if (Objeto != null && Objeto.Id > 0)
            {
                var resultado = await _objetoApiService.DeleteObjetoTres(Objeto.Id.ToString());

                if (resultado)
                {
                    MessageBox.Show(Constantes.MSG_PERFECT);

                    _objetoOverviewModel?.LoadAsync();
                    await LoadAsync();

                    _objetoOverviewModel.SelectedViewModel = null;
                }
                else
                {
                    MessageBox.Show(Constantes.MSG_ERROR);
                }
            }
            else
            {
                MessageBox.Show(Constantes.MSG_ERROR);
            }
        }

        [RelayCommand]
        private async Task Add()
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


        //[RelayCommand]
        //private async Task Update()
        //{
        //    if (Objeto != null && Objeto.Id > 0)
        //    {
        //        try
        //        {
        //            await _objetoApiService.PatchObjeto(Objeto);
        //            MessageBox.Show(Constantes.MSG_PERFECT);

        //            _objetoOverviewModel?.LoadAsync();
        //            await LoadAsync();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(Constantes.MSG_ERROR);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show(Constantes.MSG_ERROR);
        //    }
        //}

        [RelayCommand]
        private async Task Close(object? parameter)
        {
            if (_objetoOverviewModel != null)
            {
                _objetoOverviewModel.SelectedViewModel = null;
            }
        }
    }
}
