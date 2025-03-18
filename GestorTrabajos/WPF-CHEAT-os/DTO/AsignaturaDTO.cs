using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WPF_CHEAT_os.DTO
{
    public class AsignaturaDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("cursoId")]
        public int CursoId { get; set; }
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }
        [JsonPropertyName("userIds")]
        public List<int> UserIds { get; set; } = new List<int>();
    }
}
