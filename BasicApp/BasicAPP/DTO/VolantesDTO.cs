using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BasicAPP.DTO
{
    public class VolantesDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("fechaSalida")]
        public DateTime FechaSalida { get; set; }
        [JsonPropertyName("base")]
        public string Base { get; set; }
        [JsonPropertyName("aro")]
        public string Aro { get; set; }
        [JsonPropertyName("pedales")]
        public string Pedales { get; set; }
        [JsonPropertyName("workInConsole")]
        public bool WorkInConsole { get; set; }
        

        public VolantesDTO(string basee, string aro, string pedales, bool work) 
        {
            Base = basee;
            Aro = aro;
            Pedales = pedales;
            WorkInConsole = work;
        }

        public VolantesDTO() { }
    }
}
