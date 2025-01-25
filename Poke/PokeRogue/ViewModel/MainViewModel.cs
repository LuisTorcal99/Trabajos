using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace PokeRogue.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        private ViewModelBase? _selectedViewModel;

        public MainViewModel(BattleViewModel battle,
            HistoricoViewModel Historico,
            TeamViewModel team,
            ImportViewModel import)
        {
            _selectedViewModel = battle;
            BattleViewModel = battle;
            HistoricoViewModel = Historico;
            TeamViewModel = team;
            ImportViewModel = import;
        }

        public BattleViewModel BattleViewModel { get; set; }
        public HistoricoViewModel HistoricoViewModel { get; set; }
        public TeamViewModel TeamViewModel { get; set; }
        public ImportViewModel ImportViewModel { get; set; }

        public ViewModelBase? SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                SetProperty(ref _selectedViewModel, value);
            }
        }

        public async override Task LoadAsync()
        {
            if (SelectedViewModel is not null)
            {
                await SelectedViewModel.LoadAsync();
            }
        }

        [RelayCommand]
        private async void SelectViewModel(object? parameter)
        {
            SelectedViewModel = parameter as ViewModelBase;
            await LoadAsync();
        }
    }
}
