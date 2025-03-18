using API.Data;
using API.Models.DTOs.UserDTO;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using API.Models.Entity;
using System.Diagnostics.Metrics;

namespace API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly string secretKey;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly int TokenExpirationDays = 7;

        public UserRepository(ApplicationDbContext context, IConfiguration config,
            UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _context = context;
            secretKey = config.GetValue<string>("ApiSettings:SecretKey");
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<ICollection<UserDTO>> GetUsers()
        {
            var users = _context.AppUsers.OrderBy(user => user.Email).ToList();
            var userDtos = new List<UserDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userDtos.Add(new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Apellidos = user.Apellidos, 
                    Email = user.Email,
                    Rol = roles.FirstOrDefault()
                });
            }

            return userDtos;
        }


        public bool IsUniqueUser(string email)
        {
            return !_context.AppUsers.Any(user => user.Email == email);
        }

        public async Task<UserLoginResponseDTO> Login(UserLoginDTO userLoginDTO)
        {
            var user = _context.AppUsers.FirstOrDefault(u => u.Email.ToLower() == userLoginDTO.Email.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, userLoginDTO.Password);

            //user doesn't exist ?
            if (user == null || !isValid)
            {
                return new UserLoginResponseDTO { Token = "", User = null };
            }

            //User does exist
            var roles = await _userManager.GetRolesAsync(user);

            var tokenHandler = new JwtSecurityTokenHandler();
         
            if (secretKey.Length < 32)
            {
                throw new ArgumentException("The secret key must be at least 32 characters long.");
            }
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())

                }),
                Expires = DateTime.UtcNow.AddMinutes(TokenExpirationDays),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtToken = tokenHandler.CreateToken(tokenDescriptor);

            UserLoginResponseDTO userLoginResponseDto = new UserLoginResponseDTO
            {
                Token = tokenHandler.WriteToken(jwtToken),
                User = user
            };
            return userLoginResponseDto;
        }

        public async Task<UserLoginResponseDTO?> Register(UserRegistrationDTO userRegistrationDTO)
        {
            AppUser user = new AppUser()
            {
                UserName = userRegistrationDTO.Email.Split("@")[0],
                Name = userRegistrationDTO.Name,
                Email = userRegistrationDTO.Email,
                NormalizedEmail = userRegistrationDTO.Email.ToUpper(),
                Apellidos = userRegistrationDTO.Apellidos 
            };

            var result = await _userManager.CreateAsync(user, userRegistrationDTO.Password);
            if (!result.Succeeded)
            {
                return null;
            }
            if (!await _roleManager.RoleExistsAsync("admin"))
            {
                //this will run only for first time the roles are created
                await _roleManager.CreateAsync(new IdentityRole("admin"));
                await _roleManager.CreateAsync(new IdentityRole("alumno"));
                await _roleManager.CreateAsync(new IdentityRole("profesor"));
            }
            switch (userRegistrationDTO.Rol)
            {
                case "admin":
                    await _userManager.AddToRoleAsync(user, "admin");
                    break;

                case "profesor":
                    await _userManager.AddToRoleAsync(user, "profesor");
                    break;

                default:
                    await _userManager.AddToRoleAsync(user, "alumno");
                    break;
            }

            //AppUser? newUser = _context.AppUsers.FirstOrDefault(u => u.Email == userRegistrationDTO.Email);

            //return new UserLoginResponseDTO
            //{
            //    User = newUser
            //};
            var newUser = new User
            {
                Name = userRegistrationDTO.Name,
                Apellidos = userRegistrationDTO.Apellidos, // Asumiendo que Apellidos está en UserRegistrationDTO
                Email = userRegistrationDTO.Email,
                Password = userRegistrationDTO.Password, // Nota: Considera no almacenar la contraseña aquí si ya está en AspNetUsers
                Rol = userRegistrationDTO.Rol,
                AspNetUserId = user.Id // Asignar la clave foránea
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Retornar la respuesta
            return new UserLoginResponseDTO
            {
                User = user
            };
        }
    }
}
