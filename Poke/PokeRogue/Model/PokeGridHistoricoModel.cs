using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeRogue.Model
{
    public class PokeGridHistoricoModel
    {
        public DateTime dataStart { get; set; }
        public DateTime dataEnd { get; set; }
        public string pokeName { get; set; }
        public int damageDoneTrainer { get; set; }
        public int damageReceivedTrainer { get; set; }
        public int damageDonePokemon { get; set; }
        public bool @catch { get; set; }
    }
}
