using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PokeRogue.Interfaces;
using PokeRogue.Model;
using PokeRogue.Service;
using PokeRogue.Utils;

namespace PokeRogue.ViewModel
{
    //TODO MAGIC STRING AND MAGIC NUMBERS
    public partial class BattleViewModel : ViewModelBase
    {
        private readonly Random random = new Random();
        private readonly ICombateProvider _combateService;
        private readonly ITeamProvider _teamService;
        public BattleViewModel(ICombateProvider combate, ITeamProvider team)
        {
            _combateService = combate;
            _teamService = team;
            Items = new ObservableCollection<StackPanelItemModel>();
            Team = new ObservableCollection<StackPanelTeamModel>();
            BarraVidaUser = "1000";
            CalcularPorcentajeUsuario();
        }
        public ObservableCollection<StackPanelItemModel> Items { get; set; }
        public ObservableCollection<StackPanelTeamModel> Team { get; set; }

        public List<string> PokemonSeleccionado = new List<string>();

        public int PokeSele;

        [ObservableProperty]
        public string _BarraVidaUser;

        [ObservableProperty]
        public string _Ataque_EnemigoContent;

        [ObservableProperty]
        public string _BarraVidaEnemigo;

        [ObservableProperty]
        public string _PorcentajeVidaUsuario;

        [ObservableProperty]
        public string _PorcentajeVidaEnemigo;

        public DateTime DataStart = DateTime.Now;
        public DateTime DataEnd = DateTime.Now;
        public int DamageDoneTrainer = 0;
        public int DamageReceivedTrainer = 0;
        public int DamageDonePokemon = 0;
        public bool Catch = false;
        public bool Shiny = false;
        public int Id = 0;
        public int VidaTotalUsuario = 1000;

        [ObservableProperty]
        public int _MaximaSaludEnemigo;

        [ObservableProperty]
        public string _EsShiny;


        private async Task<List<string>> NombrePokemon()
        {
            PokemonsModel requestData = await HttpJsonClient<PokemonsModel>.Get(Constantes.POKE_NAME_URL)
                ?? new PokemonsModel();

            foreach (var name in requestData.results)
            {
                PokemonSeleccionado.Add(name.name.ToString());
            }

            return await Task.FromResult(PokemonSeleccionado);
        }

        private async Task<int> CountApiPropia()
        {

            List<PokeApiModel> requestDataList = await HttpJsonClient<PokeApiModel>.GetList(Constantes.API_LOCAL_URL)
                ?? new List<PokeApiModel>();

            return requestDataList.Count;
        }

        public void ReiniciarStats()
        {
            DataStart = DateTime.Now;
            DataEnd = DateTime.Now;
            DamageDoneTrainer = 0;
            DamageReceivedTrainer = 0;
            DamageDonePokemon = 0;
            Catch = false;
            Shiny = false;
        }

        private async Task GuardarPokemonTeam()
        {
            DataEnd = DateTime.Now;
            PokemonStatsModel requestData = await HttpJsonClient<PokemonStatsModel>.
                Get($"{Constantes.POKE_URL}{PokemonSeleccionado[PokeSele].ToString()}") ?? new PokemonStatsModel();

            //TODO GUID
            Id = await CountApiPropia() + 1;

            PokeApiModel pokemon = new PokeApiModel
            {
                id = Id,
                dataStart = DataStart,
                dataEnd = DataEnd,
                pokeName = requestData.forms.First().name,
                damageDoneTrainer = DamageDoneTrainer,
                damageReceivedTrainer = DamageReceivedTrainer,
                damageDonePokemon = DamageDonePokemon,
                image = !Shiny ? requestData.sprites.front_default : requestData.sprites.front_shiny,
                @catch = Catch,
                shiny = Shiny
            };
            await HttpJsonClient<PokeApiModel>.Post($"{Constantes.API_LOCAL_URL}", pokemon);
            
            Id++;
            ReiniciarStats();
        }

        private async Task<int> NumeroDePokemon()
        {
            PokemonSeleccionado = await NombrePokemon();
            PokeSele = random.Next(1, PokemonSeleccionado.Count);
            return PokeSele;
        }

        private async Task GenerarPokemon()
        {
            PokeSele = await NumeroDePokemon();
            //TODO service
            PokemonStatsModel requestData = null;
            do
            {
                requestData = await HttpJsonClient<PokemonStatsModel>.
                    Get($"{Constantes.POKE_URL}{PokemonSeleccionado[PokeSele].ToString()}")?? new PokemonStatsModel();
            } while (requestData == null);
            Shiny = _combateService.EsShiny();
            
     
            Items.Add(new StackPanelItemModel
            {
                ImagePath = requestData.sprites.front_shiny,
                PokemonName = requestData.forms.First().name ?? "Pokemon"
            });
            EsShiny = Shiny ? "SHINY" : string.Empty;
           
            if (requestData.stats[1].base_stat == 0) Ataque_EnemigoContent = "100";
            else Ataque_EnemigoContent = requestData.stats[1].base_stat.ToString();

            if (requestData.stats.First().base_stat == 0) BarraVidaEnemigo = "100";
            else BarraVidaEnemigo = requestData.stats.First().base_stat.ToString();

            MaximaSaludEnemigo = requestData.stats.First().base_stat;

            CalcularPorcentajeEnemigo();
        }

        public override async Task LoadAsync()
        {
            if(Items.Count == 0)
            {
                await GenerarPokemon();
                DataStart = DateTime.Now;
            }
        }

        [RelayCommand]
        private async Task Btn_Atacar(object? parameter)
        {
            if (RealizarAtaqueUsuario())
            {
                if (RealizarAtaqueEnemigo())
                {
                    MessageBox.Show("Game over");
                    Application.Current.Shutdown();
                }
            }
            else
            {
                RecuperarVidaUsuario();
                MessageBox.Show("Pokemon eliminado");
                await FinalizarCombate(false);
            }
        }

        [RelayCommand]
        private async void Btn_Capturar(object? parameter)
        {
            if (_combateService.Captura(BarraVidaEnemigo, MaximaSaludEnemigo))
            {
                RecuperarVidaUsuario();
                MessageBox.Show("Pokemon capturado");
                if(Shiny) { BarraVidaUser = "1000"; CalcularPorcentajeUsuario(); DamageDonePokemon = 0; }
                Catch = true;
                await FinalizarCombate(true);
            }
            else
            {
                Catch = false;
                RealizarAtaqueEnemigo();
                MessageBox.Show("El pokemon ha huido");
                await FinalizarCombate(false);
            }
        }


        [RelayCommand]
        private async void Btn_Escapar(object? parameter)
        {
            Items.Clear();
            await GuardarPokemonTeam();
            await GenerarPokemon();
        }

        private bool RealizarAtaqueUsuario()
        {
            int? ataque = _combateService.Ataque();
            DamageDoneTrainer += (int) ataque;
            int? hpEnemigo = StringUtils.ConvertToNumber(BarraVidaEnemigo);

            if (hpEnemigo - ataque > 0)
            {
                BarraVidaEnemigo = (hpEnemigo - ataque).ToString();
                CalcularPorcentajeEnemigo();
                return true; // El enemigo no ha sido derrotado
            }
            else
            {
                BarraVidaEnemigo = "0";
                return false; // El enemigo ha sido derrotado
            }
        }

        private bool RealizarAtaqueEnemigo()
        {
            int? ataqueEnemigo = StringUtils.ConvertToNumber(Ataque_EnemigoContent);
            DamageReceivedTrainer += (int) ataqueEnemigo;
            DamageDonePokemon = DamageReceivedTrainer;
            int? hpUsuario = StringUtils.ConvertToNumber(BarraVidaUser);

            if (hpUsuario - ataqueEnemigo > 0)
            {
                BarraVidaUser = (hpUsuario - ataqueEnemigo).ToString();
                CalcularPorcentajeUsuario();
                return false; // El usuario no ha sido derrotado
            }
            else
            {
                BarraVidaUser = "0";
                return true; // El usuario ha sido derrotado
            }
        }

        private void RecuperarVidaUsuario()
        {
            int? recuperarHp = _combateService.RecuperarHp(BarraVidaUser);
            int? hpUsuario = StringUtils.ConvertToNumber(BarraVidaUser);
            BarraVidaUser = (hpUsuario + recuperarHp).ToString();
            DamageDonePokemon -= (int) recuperarHp;
            CalcularPorcentajeUsuario();
        }

        private async Task FinalizarCombate(bool capturado)
        {
            Items.Clear();
            await GuardarPokemonTeam();
            await GenerarPokemon();
        }

        public void CalcularPorcentajeUsuario()
        {
            int? hpUsuario = StringUtils.ConvertToNumber(BarraVidaUser);

            double resultUser = (double)(hpUsuario ?? 0) / VidaTotalUsuario * 100;
            PorcentajeVidaUsuario = resultUser.ToString("0.00") + "%";
            Vida();
        }
        public void CalcularPorcentajeEnemigo()
        {
            int? hpEnemigo = StringUtils.ConvertToNumber(BarraVidaEnemigo);

            double resultEnemigo = (double)(hpEnemigo ?? 0) / MaximaSaludEnemigo * 100;
            PorcentajeVidaEnemigo = resultEnemigo.ToString("0.00") + "%";
            Vida();
        }

        [ObservableProperty]
        public string _ColorHp;
        private void Vida()
        {
            int? vida = StringUtils.ConvertToNumber(BarraVidaUser);
            switch (vida)
            {
                case > 750:
                    ColorHp = "#FF13FF00";
                    break;
                case > 251:
                    ColorHp = "#FFFFDE00";
                    break;
                case < 250:
                    ColorHp = "#FFFF0000";
                    break;
            }
        }
    }
}
