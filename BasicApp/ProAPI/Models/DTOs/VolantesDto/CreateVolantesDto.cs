using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models.DTOs.VolantesDto
{
    public class CreateVolantesDto
    {
        [Required(ErrorMessage = "Base is required")]
        [MaxLength(25, ErrorMessage = "Max char is 25")]
        public string Base { get; set; }

        [Required(ErrorMessage = "Aro is required")]
        [MaxLength(25, ErrorMessage = "Max char is 25")]
        public string Aro { get; set; }

        [Required(ErrorMessage = "Pedales is required")]
        [MaxLength(25, ErrorMessage = "Max char is 25")]
        public string Pedales { get; set; }

        [Required(ErrorMessage = "WorkInConsole is required")]
        public bool WorkInConsole { get; set; }
    }
}
