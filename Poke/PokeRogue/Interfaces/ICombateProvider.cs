using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeRogue.Interfaces
{
    public interface ICombateProvider
    {
        public int Ataque();
        public bool Captura(string hp, int vidaTotal);
        public int? RecuperarHp(string vida);
        public bool EsShiny();
    }
}
