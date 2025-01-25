using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeRogue.Model
{
    public class PokemonStatsModel
    {
        public List<Form> forms { get; set; }
        public Sprites sprites { get; set; }
        public List<Stat> stats { get; set; }
    }

    public class Stat
    {
        public int base_stat { get; set; }
        public int effort { get; set; }
        public Stat stat { get; set; }
    }

    public class Form
    {
        public string name { get; set; }
        public string url { get; set; }
    }
    public class Sprites
    {
        public string back_default { get; set; }
        public object back_female { get; set; }
        public string back_shiny { get; set; }
        public object back_shiny_female { get; set; }
        public string front_default { get; set; }
        public object front_female { get; set; }
        public string front_shiny { get; set; }
        public object front_shiny_female { get; set; }
    }
}
