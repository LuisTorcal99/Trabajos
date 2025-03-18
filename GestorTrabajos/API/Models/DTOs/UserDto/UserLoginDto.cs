using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.UserDTO
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Field required: Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field required: Password")]      
        public string Password { get; set; }
    }
}
