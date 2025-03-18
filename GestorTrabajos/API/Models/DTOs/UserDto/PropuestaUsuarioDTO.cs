using API.Models.DTOs.Propuesta;

namespace API.Models.DTOs.UserDto
{
    public class PropuestaUsuarioDTO
    {
        public CreatePropuestaDTO Propuesta { get; set; }
        public UserIntDTO Usuario { get; set; }
    }
}
