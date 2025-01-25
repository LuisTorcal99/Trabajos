using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokeRogue.Interfaces;
using PokeRogue.Utils;


namespace PokeRogue.Service
{
    public class CombateService : ICombateProvider
    {
        private readonly Random random = new Random();
        public int Ataque()
        {
            int ataque = random.Next(0, 40);
            return ataque;
        }

        public bool Captura(string hp, int totalVida)
        {
            int posibilidad = random.Next(0, totalVida);
            bool captura = false;
            int? vida = StringUtils.ConvertToNumber(hp);

            if(posibilidad > vida) captura = true;
            else captura = false;
            return captura;
        }

        public int? RecuperarHp(string vida)
        {
            int? recuperar = 5 * StringUtils.ConvertToNumber(vida) / 100;
            return recuperar;
        }

        public bool EsShiny()
        {
            int shiny = random.Next(0, 101);
            return shiny <= 5;
        }
    }
}
