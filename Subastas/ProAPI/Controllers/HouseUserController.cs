using AutoMapper;
using RestAPI.Controllers.RestAPI.Controllers;
using RestAPI.Models.DTOs.HouseUserDTO;
using RestAPI.Models.Entity;
using RestAPI.Repository.IRepository;

namespace RestAPI.Controllers
{
    public class HouseUserController : BaseController<HouseUserEntity, HouseUserDTO, CreateHouseUserDto>
    {
        public HouseUserController(IHouseUserRepository HouseUserRepository,
            IMapper mapper, ILogger<HouseUserController> logger)
            : base(HouseUserRepository, mapper, logger)
        {

        }
    }
}
