using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WPF_CHEAT_os.DTO;

namespace WPF_CHEAT_os.Models
{
    public class UsuariosGridModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }

        public static UsuariosGridModel CreateModelFromDTO(GetUsuarioDTO usuarioDTO)
        {
            return new UsuariosGridModel
            {
                Id = usuarioDTO.Id,
                Name = usuarioDTO.Name,
                Apellidos = usuarioDTO.Apellidos,
                Email = usuarioDTO.Email,
            };
        }

    }
}
