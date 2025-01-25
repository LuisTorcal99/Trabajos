using AutoMapper;
using RestAPI.Controllers.RestAPI.Controllers;
using RestAPI.Models.DTOs.VolantesDto;
using RestAPI.Models.Entity;
using RestAPI.Repository.IRepository;

namespace RestAPI.Controllers
{
    public class VolantesController : BaseController<VolantesEntity, VolantesDto, CreateVolantesDto>
    {
        public VolantesController(IVolantesRepository volantesRepository,
            IMapper mapper, ILogger<VolantesController> logger)
            : base(volantesRepository, mapper, logger)
        {

        }
    }
}
