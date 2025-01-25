using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using PokeRogue.Interfaces;
using PokeRogue.Model;
using PokeRogue.Utils;


namespace PokeRogue.ViewModel
{
    public partial class TeamViewModel : ViewModelBase
    {
        private readonly ITeamProvider _teamService;
        public TeamViewModel(ITeamProvider teamService)
        {
            _teamService = teamService;
            Team = new ObservableCollection<PokeApiModel>();
            TeamBueno = new ObservableCollection<PokemonDisplayModel>();
        }
        public async Task TraerPokemons()
        {
            List<PokeApiModel> requestDataList = await HttpJsonClient<PokeApiModel>.GetList(Constantes.API_LOCAL_URL) ?? new List<PokeApiModel>();

            foreach (var requestData in requestDataList)
            {
                if (requestData.@catch)
                {
                    Team.Add(new PokeApiModel
                    {
                        id = requestData.id,
                        image = requestData.image,
                        pokeName = requestData.pokeName,
                    });
                }
            }

            foreach (var data in Team)
            {
                int count = 1;
                for (var i = 0; i < Team.Count; i++)
                {
                    if (data.pokeName.Equals(Team[i].pokeName) && data.id != Team[i].id && data.image.Equals(Team[i].image))
                    {
                        count++;
                    }
                }
                TeamBueno.Add(new PokemonDisplayModel
                {
                    Id = data.id,
                    PokeName = data.pokeName,
                    Image = data.image,
                    CaptureCount = count
                });
            }

            var toRemove = new List<PokemonDisplayModel>();

            foreach (var data in TeamBueno)
            {
                for (var i = 0; i < TeamBueno.Count; i++)
                {
                    if (data.PokeName.Equals(TeamBueno[i].PokeName) && data.Id != TeamBueno[i].Id 
                        && data.Id < TeamBueno[i].Id && data.Image.Equals(TeamBueno[i].Image))
                    {
                        toRemove.Add(TeamBueno[i]);
                    }
                }
            }

            foreach (var item in toRemove)
            {
                TeamBueno.Remove(item);
            }
        }

        public ObservableCollection<PokemonDisplayModel> TeamBueno { get; set; }
        public ObservableCollection<PokeApiModel> Team { get; set; }
        public override async Task LoadAsync()
        {
            Team.Clear();
            TeamBueno.Clear();
            await TraerPokemons();
        }
    }
}