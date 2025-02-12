using AutoMapper;
using RestAPI.Controllers.RestAPI.Controllers;
using RestAPI.Models.DTOs.PujasDTO;
using RestAPI.Models.Entity;
using RestAPI.Repository.IRepository;

namespace RestAPI.Controllers
{
    public class PujaController : BaseController<PujaEntity, PujaDto, CrearPujaDto>
    {
        public PujaController(IPujaRepository PujaRepository,
            IMapper mapper, ILogger<PujaController> logger)
            : base(PujaRepository, mapper, logger)
        {

        }
    }
}
