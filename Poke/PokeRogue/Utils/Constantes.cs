using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace PokeRogue.Utils
{
    public static class Constantes
    {
        public const string POKE_URL = "https://pokeapi.co/api/v2/pokemon/";
        public const string POKE_NAME_URL = "https://pokeapi.co/api/v2/pokemon/?offset=0&limit=100";
        public const string API_LOCAL_URL = "http://localhost:5000/pokemon/";

        public const string JSON_FILTER = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";

        public const string IMAGEN_GOOD = "../Resources/Good.png";
        public const string IMAGEN_ERROR = "../Resources/Error.png";
    }
}
