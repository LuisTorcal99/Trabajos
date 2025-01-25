using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using PokeRogue.Interfaces;
using PokeRogue.Model;
using PokeRogue.Utils;

namespace PokeRogue.ViewModel
{
    public partial class HistoricoViewModel : ViewModelBase
    {
        public readonly IHistoricoProvider<PokeApiModel> _historicoService;
        
        public HistoricoViewModel(IHistoricoProvider<PokeApiModel> historicoService)
        {
            _historicoService = historicoService;
            Historico = new ObservableCollection<PokeGridHistoricoModel>();
            HistoricoEnviar = new ObservableCollection<PokeApiModel>();
        }

        [ObservableProperty]
        private ObservableCollection<PokeGridHistoricoModel> historico;

        public ObservableCollection<PokeApiModel> HistoricoEnviar { get; set; }

        public async Task Tabla()
        {
            List<PokeApiModel> requestDataList = await HttpJsonClient<PokeApiModel>.GetList(Constantes.API_LOCAL_URL)
                    ?? new List<PokeApiModel>();

            foreach (var requestData in requestDataList)
            {
                Historico.Add(new PokeGridHistoricoModel
                {
                    dataStart = requestData.dataStart,
                    dataEnd = requestData.dataEnd,
                    pokeName = requestData.pokeName,
                    damageDoneTrainer = requestData.damageDoneTrainer,
                    damageReceivedTrainer = requestData.damageReceivedTrainer,
                    damageDonePokemon = requestData.damageDonePokemon,
                    @catch = requestData.@catch
                });
                HistoricoEnviar.Add(requestData);
            }
        }

        [RelayCommand]
        public void DescargarHistorico()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = Constantes.JSON_FILTER
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                _historicoService.Save(saveFileDialog.FileName, HistoricoEnviar);
            }
        }

        public override async Task LoadAsync()
        {
            Historico ??= new ObservableCollection<PokeGridHistoricoModel>();
            HistoricoEnviar.Clear();
            Historico.Clear();
            await Tabla();
        }
    }
}