using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using PokeRogue.Interfaces;
using PokeRogue.Model;
using PokeRogue.Utils;

namespace PokeRogue.ViewModel
{
    public partial class ImportViewModel : ViewModelBase
    {
        public readonly IHistoricoProvider<PokeApiModel> _historicoService;

        public ImportViewModel(IHistoricoProvider<PokeApiModel> historicoService)
        {
            _historicoService = historicoService;
            HistoricoRecibir = new ObservableCollection<PokeApiModel>();
        }

        public ObservableCollection<PokeApiModel> HistoricoRecibir { get; set; }

        [ObservableProperty]
        private string goodOrError;

        [RelayCommand]
        public async Task ImportarFichero()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = Constantes.JSON_FILTER
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var loadedPoke = _historicoService.Load(openFileDialog.FileName);

                if (loadedPoke != null && loadedPoke.Any())
                {
                    HistoricoRecibir = new ObservableCollection<PokeApiModel>(loadedPoke);

                    List<PokeApiModel> requestDataList = await HttpJsonClient<PokeApiModel>.GetList(Constantes.API_LOCAL_URL)
                    ?? new List<PokeApiModel>();

                    for (int i = 1; i <= requestDataList.Count(); i++)
                    {
                        await HttpJsonClient<PokeApiModel>.Delete($"{Constantes.API_LOCAL_URL}{i}");
                    }
                    // no se me eliminaba el ultimo
                    await HttpJsonClient<PokeApiModel>.Delete($"{Constantes.API_LOCAL_URL}{requestDataList.Last().id}");

                    foreach (PokeApiModel pokeApi in HistoricoRecibir)
                    {
                        PokeApiModel pokemon = new PokeApiModel
                        {
                            id = pokeApi.id,
                            dataStart = pokeApi.dataStart,
                            dataEnd = pokeApi.dataEnd,
                            pokeName = pokeApi.pokeName,
                            damageDoneTrainer = pokeApi.damageDoneTrainer,
                            damageReceivedTrainer = pokeApi.damageReceivedTrainer,
                            damageDonePokemon = pokeApi.damageDonePokemon,
                            image = pokeApi.image,
                            @catch = pokeApi.@catch,
                            shiny = pokeApi.shiny
                        };
                        await HttpJsonClient<PokeApiModel>.Post($"{Constantes.API_LOCAL_URL}", pokemon);
                    }
                    GoodOrError = Constantes.IMAGEN_GOOD; 
                }
                else
                {
                    GoodOrError = Constantes.IMAGEN_ERROR;
                }
            }
        }

        public override Task LoadAsync()
        {
            return base.LoadAsync();
        }
    }
}