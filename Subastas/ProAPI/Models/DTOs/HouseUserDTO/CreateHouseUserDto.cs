using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models.DTOs.HouseUserDTO
{
    public class CreateHouseUserDto
    {
        [Required(ErrorMessage = "FirstName is required")]
        [MaxLength(50, ErrorMessage = "Max char is 50")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        [MaxLength(50, ErrorMessage = "Max char is 50")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(50, ErrorMessage = "Max char is 50")]
        public string Email { get; set; }
    }

}
