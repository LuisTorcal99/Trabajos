using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokeRogue.Interfaces;
using PokeRogue.Model;
using PokeRogue.Utils;

namespace PokeRogue.Service
{
    public class TeamService : ITeamProvider
    {
        public List<StackPanelTeamModel> Team { get; }
        public TeamService()
        {
            Team = new List<StackPanelTeamModel>();
        }
        public void AgregarAlTeam(StackPanelTeamModel item)
        {
            Team.Add(item);
        }
    }
}