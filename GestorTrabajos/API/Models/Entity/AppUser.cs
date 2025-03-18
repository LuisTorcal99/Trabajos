using Microsoft.AspNetCore.Identity;

namespace API.Models.Entity
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Apellidos { get; set; }
    }
}
