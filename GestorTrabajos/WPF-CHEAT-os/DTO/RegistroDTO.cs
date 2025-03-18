using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WPF_CHEAT_os.DTO
{
    public class RegistroDTO
    {
        [JsonPropertyName("name")]
        public string Nombre { get; set; }
        [JsonPropertyName("apellidos")]
        public string Apellido { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("rol")]
        public string Rol { get; set; }

        public RegistroDTO(string name, string apellido, string email, string password, string role)
        {
            Nombre = name;
            Apellido = apellido;
            Email = email;
            Password = password;
            Rol = role;
        }
    }
}
