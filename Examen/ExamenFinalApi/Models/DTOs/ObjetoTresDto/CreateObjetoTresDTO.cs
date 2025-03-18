using System.ComponentModel.DataAnnotations;
using ExamenFinalApi.Models.DTOs.ObjetoDto;

namespace ExamenFinalApi.Models.DTOs.ObjetoTresDto
{
    public class CreateObjetoTresDTO
    {
        [Required]
        public int Usuario { get; set; }
        [Required]
        public List<int> Productos { get; set; }
    }
}
