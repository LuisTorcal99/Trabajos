using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.Curso
{
    public class CreateCursoDTO
    {
        [Required(ErrorMessage = "Field required: nombre")]
        public string Nombre { get; set; }
        public string Turno { get; set; }
    }
}
