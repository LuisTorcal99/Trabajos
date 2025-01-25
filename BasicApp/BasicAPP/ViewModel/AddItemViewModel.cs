using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using BasicAPP.View;
using CommunityToolkit.Mvvm.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BasicAPP.Interfaces;
using BasicAPP.DTO;
using BasicAPP.Service;
using System.IO;
using BasicAPP.Utils;

namespace BasicAPP.ViewModel
{
    public partial class AddItemViewModel : ViewModelBase
    {
        [ObservableProperty]
        public string _Aro;

        [ObservableProperty]
        public string _Base;

        [ObservableProperty]
        public string _Pedales;

        [ObservableProperty]
        public bool _BoolOptions;

        private readonly IVolantesApiProvider _volantesService;
        public AddItemViewModel(IVolantesApiProvider volantesService)
        {
            _volantesService = volantesService;
        }

        public async Task LoadAsync()
        {
            await base.LoadAsync();
        }

        [RelayCommand]
        private async Task AceptarVentana(object? parameter)
        {
            try
            {
                VolantesDTO PostVolante = new VolantesDTO(
                    Base, Aro, Pedales, BoolOptions
                );

                await _volantesService.PostAsync(PostVolante);

                MessageBox.Show("Volante añadido");
                Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w is AddItemView)?.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        [RelayCommand]
        private void CancelarVentana(object? parameter)
        {
            Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w is AddItemView)?.Close();
        }
    }
}
