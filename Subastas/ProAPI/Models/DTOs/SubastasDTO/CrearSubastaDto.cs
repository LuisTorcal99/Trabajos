using System.ComponentModel.DataAnnotations;
using RestAPI.Models.DTOs.PujasDTO;
using RestAPI.Models.Entity;

namespace RestAPI.Models.DTOs.SubastasDTO
{
    public class CrearSubastaDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Max char is 50")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Photo is required")]
        public string Photo { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Pujas are required")]
        public ICollection<PujaDto> Pujas { get; set; }
    }
}
