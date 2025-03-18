using System.Collections.Generic;
using System.Text.Json.Serialization;
using WPF_CHEAT_os.Models;

namespace WPF_CHEAT_os.DTO
{
    public class PropuestaDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("titulo")]
        public string Titulo { get; set; }

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }

        [JsonPropertyName("tipo")]
        public string Tipo { get; set; }

        [JsonPropertyName("estado")]
        public string Estado { get; set; }

        [JsonPropertyName("users")]
        public ICollection<ProfesorModel> Users { get; set; }
    }
}
