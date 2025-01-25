using Microsoft.AspNetCore.Mvc;
using PokeRogueApi.DTO;
using static System.Net.WebRequestMethods;

namespace PokeRogueApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : Controller
    {
        private readonly ILogger<PokemonDTO> _logger;

        private static List<PokemonDTO> Pokemon = new List<PokemonDTO>()
        {
            new PokemonDTO
            {
                Id = 1,
                DataStart = new DateTime(2024, 12, 25, 14, 30, 0), // Año, Mes, Día, Hora, Minuto, Segundo
                DataEnd = new DateTime(2024, 12, 25, 18, 45, 0),  // Otra fecha y hora específica
                PokeName = "pikachu",
                DamageDoneTrainer = 0,
                DamageReceivedTrainer = 0,
                DamageDonePokemon = 0,
                Image = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/25.png",
                Catch = true,
                Shiny = false
            },
            new PokemonDTO
            {
                Id = 2,
                DataStart = new DateTime(2024, 12, 26, 14, 30, 0), 
                DataEnd = new DateTime(2024, 12, 26, 18, 45, 0),  
                PokeName = "pikachu",
                DamageDoneTrainer = 0,
                DamageReceivedTrainer = 0,
                DamageDonePokemon = 0,
                Image = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/25.png",
                Catch = true,
                Shiny = false
            },
            new PokemonDTO
            {
                Id = 3,
                DataStart = new DateTime(2024, 12, 27, 22, 30, 0),
                DataEnd = new DateTime(2024, 12, 27, 23, 45, 0), 
                PokeName = "charmander",
                DamageDoneTrainer = 52,
                DamageReceivedTrainer = 13,
                DamageDonePokemon = 41,
                Image = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/4.png",
                Catch = true,
                Shiny = false
            },
            new PokemonDTO
            {
                Id = 4,
                DataStart = new DateTime(2024, 12, 30, 23, 30, 0),
                DataEnd = new DateTime(2024, 12, 31, 2, 45, 0),
                PokeName = "charmander",
                DamageDoneTrainer = 78,
                DamageReceivedTrainer = 102,
                DamageDonePokemon = 99,
                Image = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/shiny/4.png",
                Catch = true,
                Shiny = true
            },
        };

        public PokemonController(ILogger<PokemonDTO> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAllElement")]
        public IEnumerable<PokemonDTO> Get()
        {
            return Pokemon;
        }

        [HttpGet("{id}")]
        public PokemonDTO GetOne(int id)
        {
            return Pokemon.FirstOrDefault(x=>x.Id==id);
        }

        [HttpPost]
        public PokemonDTO Post([FromBody] PokemonDTO pokemon)
        {
            if (Pokemon.Any(x=> x.Id== pokemon.Id))
            {
                return null;
            }
            Pokemon.Add(pokemon);
            return pokemon;
        }

        [HttpPut("{id}")]
        public PokemonDTO Put([FromBody] PokemonDTO pokemon,int id)
        {
            if (id!= pokemon?.Id)
            {
                return null;
            }
            PokemonDTO? pokemonBBDD = Pokemon.FirstOrDefault(x => x.Id == pokemon.Id);
            if (pokemonBBDD == null)
            {
                return null;
            }
            pokemonBBDD.DataStart = pokemon.DataStart;
            pokemonBBDD.DataEnd = pokemon.DataEnd;
            pokemonBBDD.PokeName = pokemon.PokeName;
            pokemonBBDD.DamageDoneTrainer = pokemon.DamageDoneTrainer;
            pokemonBBDD.DamageDonePokemon = pokemon.DamageDonePokemon;
            pokemonBBDD.DamageReceivedTrainer = pokemon.DamageReceivedTrainer;
            pokemonBBDD.Image = pokemon.Image;
            pokemonBBDD.Catch = pokemon.Catch;
            pokemonBBDD.Shiny = pokemon.Shiny;

            return pokemonBBDD;
        }

        [HttpDelete("{id}")]
        public bool Remove(int id)
        {
            PokemonDTO? pokemonBBDD = Pokemon.FirstOrDefault(x => x.Id == id);
            if (pokemonBBDD == null)
            {
                return false;
            }            
            return Pokemon.Remove(pokemonBBDD);
        }
    }
}
