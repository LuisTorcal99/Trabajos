using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicAPP.DTO;
using BasicAPP.Interfaces;
using BasicAPP.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BasicAPP.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        private ViewModelBase? _selectedViewModel;

        public MainViewModel(LoginViewModel login, RegistrationViewModel registro, DatosGridViewModel Datos)
        {
            LoginViewModel = login;
            RegistrationViewModel = registro;
            DatosGridViewModel = Datos;
            SelectedViewModel = login;
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
