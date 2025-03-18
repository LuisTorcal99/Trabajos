using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WPF_CHEAT_os.DTO
{
    public class UsuarioDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("apellidos")]
        public string Apellidos { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("rol")]
        public string Rol { get; set; }
        [JsonPropertyName("propuestas")]
        public ICollection<PropuestaDTO> Propuestas { get; } = [];
        [JsonPropertyName("cursos")]
        public ICollection<CursoDTO> Cursos { get; } = [];
    }
}
