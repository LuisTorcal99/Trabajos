
using WPF_CHEAT_os.DTO;
using WPF_CHEAT_os.Utils;

namespace WPF_CHEAT_os.Models
{
    public class PropuestaModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Email { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public ProfesorModel Profesor1 { get; set; }
        public ProfesorModel Profesor2 { get; set; }
        public ProfesorModel Profesor3 { get; set; }

        // Nueva propiedad para almacenar los usuarios asociados a la propuesta
        public List<UsuarioDTO> Users { get; set; } = new List<UsuarioDTO>();
    }
}
