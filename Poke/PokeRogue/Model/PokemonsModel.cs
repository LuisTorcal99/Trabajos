using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeRogue.Model
{
    public class PokemonsModel
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
        public List<Pokemons> results { get; set; }
        
    }
    public class Pokemons
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}
