using System.ComponentModel.DataAnnotations;

namespace ExamenFinalApi.Models.DTOs.ObjetoDosDto
{
    public class CreateObjetoDosDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(200, ErrorMessage = "Max char is 200")]
        public string Nombre { get; set; }
        [Required]
        public double Precio { get; set; }
    }
}
