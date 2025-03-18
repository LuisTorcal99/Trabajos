using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace WPF_CHEAT_os.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        private ViewModelBase? _selectedViewModel;
     
        public MainViewModel(PrincipalViewModel principal, PropuestaViewModel propuesta, 
            VerPropuestaViewModel verPropuesta, LoginViewModel login, RegistroViewModel registro, GestionViewModel gestion)
        {
            _selectedViewModel = login;

            PrincipalViewModel = principal;
            PropuestaViewModel = propuesta;
            VerPropuestaViewModel = verPropuesta;
            LoginViewModel = login;
            RegistroViewModel = registro;
            GestionViewModel = gestion;
        }

        public LoginViewModel LoginViewModel { get; set; }
        public PropuestaViewModel PropuestaViewModel { get; set; }
        public PrincipalViewModel PrincipalViewModel { get; set; }
        public VerPropuestaViewModel VerPropuestaViewModel { get; set; }
        public RegistroViewModel RegistroViewModel { get; set; }
        public GestionViewModel GestionViewModel { get; set; }

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

        private bool _isMenuVisible = false;

        public bool IsMenuVisible
        {
            get => _isMenuVisible;
            set => SetProperty(ref _isMenuVisible, value);
        }

        [RelayCommand]
        private async void SelectViewModel(object? parameter)
        {
            SelectedViewModel = parameter as ViewModelBase;
            await LoadAsync();

            // Mostrar menú en vistas seleccionadas
            IsMenuVisible = SelectedViewModel is PrincipalViewModel
                 || SelectedViewModel is PropuestaViewModel
                 || SelectedViewModel is VerPropuestaViewModel
                 || SelectedViewModel is GestionViewModel;
        }
    }
}
