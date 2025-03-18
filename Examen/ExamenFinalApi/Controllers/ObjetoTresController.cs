using AutoMapper;
using ExamenFinalApi.Models.DTOs.ObjetoTresDto;
using ExamenFinalApi.Models.Entity;
using ExamenFinalApi.Repository.IRepository;
using RestAPI.Controllers.ExamenFinalApi.Controllers;

namespace ExamenFinalApi.Controllers
{
    public class ObjetoTresController : BaseController<ObjetoTresEntity, ObjetoTresDTO, CreateObjetoTresDTO>
    {
        public ObjetoTresController(IObjetoTresRepository ObjetoTresRepository,
            IMapper mapper, ILogger<ObjetoTresController> logger)
            : base(ObjetoTresRepository, mapper, logger)
        {

        }
    }
}

