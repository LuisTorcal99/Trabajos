using API.Data;
using API.Models.DTOs;
using API.Models.DTOs.UserDto;
using API.Models.Entity;
using API.Repository;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPracticoController : BaseController<User,UserIntDTO,IUserPracticoRepository>
    {
        private readonly IUserPracticoRepository _userPracticoRepository;
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;
        protected ResponseApi _reponseApi;
        private readonly ApplicationDbContext _context;
        public UserPracticoController(IUserPracticoRepository userPracticoRepository, IMapper mapper, 
            ILogger<UserPracticoRepository> logger, ApplicationDbContext context) : base(userPracticoRepository, mapper, logger)
        {
            _userPracticoRepository = userPracticoRepository;
            _reponseApi = new ResponseApi();
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }
    }
}
