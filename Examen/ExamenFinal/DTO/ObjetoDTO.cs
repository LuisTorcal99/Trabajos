using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExamenFinal.DTO
{
    public class ObjetoDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }


        public ObjetoDTO(string Nombre, string bol) 
        {
            Nombre = Nombre;
            Email = bol;
        }

        public ObjetoDTO() { }
    }
}
