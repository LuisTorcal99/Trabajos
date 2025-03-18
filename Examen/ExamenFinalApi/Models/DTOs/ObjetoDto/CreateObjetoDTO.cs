using System.ComponentModel.DataAnnotations;
using ExamenFinalApi.Models.DTOs.ObjetoDosDto;
using ExamenFinalApi.Models.DTOs.ObjetoTresDto;

namespace ExamenFinalApi.Models.DTOs.ObjetoDto
{
    public class CreateObjetoDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(200, ErrorMessage = "Max char is 200")]
        public string Nombre { get; set; }
        [Required]
        public string Email { get; set; }

    }
}
