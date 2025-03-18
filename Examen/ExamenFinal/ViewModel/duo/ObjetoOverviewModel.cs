using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExamenFinal.DTO;
using ExamenFinal.Interfaces;
using ExamenFinal.Models;
using ExamenFinal.Utils;
using ExamenFinal.ViewModel.Main;

namespace ExamenFinal.ViewModel
{
    public partial class ObjetoOverviewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<ItemObjetoTresModel> _Items;

        [ObservableProperty]
        public string _SearchId;

        private readonly VentanaDesplegableViewModel _viewModel;

        [ObservableProperty]
        private ViewModelBase? _selectedViewModel;

        private IObjetoTresApiProvider _ObjetoApiService;

        [ObservableProperty]
        private ItemObjetoTresModel _Objeto;

        [ObservableProperty]
        private ObservableCollection<ItemObjetoTresModel> _filteredItems = new();


        public ObjetoOverviewModel(VentanaDesplegableViewModel ventanaDesplegableViewModel, IObjetoTresApiProvider objetoApiProvider)
        {
            _viewModel = ventanaDesplegableViewModel;
            Items = new ObservableCollection<ItemObjetoTresModel>();
            _ObjetoApiService = objetoApiProvider;
        }

        public override async Task LoadAsync()
        {
            IEnumerable<ObjetoTresDTO> objetos = await _ObjetoApiService.GetObjetoTres();
            Items = new ObservableCollection<ItemObjetoTresModel>();

            foreach (var objeto in objetos)
            {
                Items.Add(ItemObjetoTresModel.CreateModelFromDTO(objeto));
            }
        }

        [RelayCommand]
        private async Task SelectViewModel(object? parameter)
        {
            _viewModel.SetIdObjeto(StringUtils.ConvertToNumber(parameter?.ToString() ?? string.Empty) ?? int.MinValue);
            _viewModel.SetParentViewModel(this);
            SelectedViewModel = _viewModel;
            await _viewModel.LoadAsync();
        }
    }
}