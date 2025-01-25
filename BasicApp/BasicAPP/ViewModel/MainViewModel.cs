using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicAPP.Interfaces;
using BasicAPP.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BasicAPP.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        private ViewModelBase? _selectedViewModel;

        public MainViewModel()
        {
            var volantesService = new VolantesApiService();

            LoginViewModel = new LoginViewModel(this);
            RegistrationViewModel = new RegistrationViewModel(this);
            DatosGridViewModel = new DatosGridViewModel(volantesService, this);
            SelectedViewModel = LoginViewModel;
        }

        public LoginViewModel LoginViewModel { get; set; }
        public RegistrationViewModel RegistrationViewModel { get; set; }
        public DatosGridViewModel DatosGridViewModel { get; set; }

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
