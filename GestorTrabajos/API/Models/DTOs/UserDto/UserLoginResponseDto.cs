using API.Models.Entity;

namespace API.Models.DTOs.UserDTO
{
    public class UserLoginResponseDTO
    {
        public AppUser User { get; set; }
        public string Token { get; set; }

    }
}
