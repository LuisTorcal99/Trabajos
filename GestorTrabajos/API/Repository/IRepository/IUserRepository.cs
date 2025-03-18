using API.Models.DTOs.UserDTO;
using API.Models.Entity;

namespace API.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<ICollection<UserDTO>> GetUsers();
        bool IsUniqueUser(string userName);
        Task<UserLoginResponseDTO> Login(UserLoginDTO userLoginDTO);
        Task<UserLoginResponseDTO> Register(UserRegistrationDTO userRegistrationDTO);
    }
}
