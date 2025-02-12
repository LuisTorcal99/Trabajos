using AutoMapper;
using RestAPI.Controllers.RestAPI.Controllers;
using RestAPI.Models.DTOs.SubastasDTO;
using RestAPI.Models.Entity;
using RestAPI.Repository.IRepository;

namespace RestAPI.Controllers
{
    public class SubastasController : BaseController<SubastasEntity, SubastasDto, CrearSubastaDto>
    {
        public SubastasController(ISubastaRepository SubastasRepository,
            IMapper mapper, ILogger<SubastasController> logger)
            : base(SubastasRepository, mapper, logger)
        {

        }
    }
}

