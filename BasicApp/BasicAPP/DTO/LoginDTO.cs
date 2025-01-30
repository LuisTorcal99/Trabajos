using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BasicAPP.DTO
{
    public class LoginDTO
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        public string Token { get; set; }

        public LoginDTO(string correo, string password)
        {
            Email = correo;
            Password = password;
            
        }

        public LoginDTO()
        {
            
        }
    }
}
