using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WPF_CHEAT_os.DTO
{
    public class LoginDTO
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        public string Token { get; set; }

        public LoginDTO(string mail, string password)
        {
            Email = mail;
            Password = password;
        }

        public LoginDTO()
        {

        }
    }
}
