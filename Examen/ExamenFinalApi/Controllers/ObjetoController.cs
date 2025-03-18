using AutoMapper;
using ExamenFinalApi.Models.DTOs.ObjetoDto;
using ExamenFinalApi.Models.Entity;
using ExamenFinalApi.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Controllers.ExamenFinalApi.Controllers;

namespace ExamenFinalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjetoController : BaseController<ObjetoEntity, ObjetoDTO, CreateObjetoDTO>
    {
        public ObjetoController(IObjetoRepository ObjetoRepository,
            IMapper mapper, ILogger<ObjetoController> logger)
            : base(ObjetoRepository, mapper, logger)
        {

        }
    }
}
