using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeRogue.Model
{
    public class PokemonDisplayModel
    {
        public int Id { get; set; }
        public string PokeName { get; set; }
        public string Image { get; set; }
        public bool Catch {  get; set; }
        public int CaptureCount { get; set; }
    }
}
