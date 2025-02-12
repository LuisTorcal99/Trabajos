using AutoMapper;
using RestAPI.Controllers.RestAPI.Controllers;
using RestAPI.Models.DTOs.HouseDTO;
using RestAPI.Models.Entity;
using RestAPI.Repository.IRepository;

namespace RestAPI.Controllers
{
    public class HouseController : BaseController<HouseEntity, HouseDTO, CreateHouseDto>
    {
        public HouseController(IHouseRepository HouseRepository,
            IMapper mapper, ILogger<HouseController> logger)
            : base(HouseRepository, mapper, logger)
        {

        }
    }
}
