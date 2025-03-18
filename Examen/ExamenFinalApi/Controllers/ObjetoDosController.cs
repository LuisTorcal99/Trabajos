using AutoMapper;
using ExamenFinalApi.Models.DTOs.ObjetoDosDto;
using ExamenFinalApi.Models.Entity;
using ExamenFinalApi.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Controllers.ExamenFinalApi.Controllers;

namespace ExamenFinalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjetoDosController : BaseController<ObjetoDosEntity, ObjetoDosDTO, CreateObjetoDosDTO>
    {
        public ObjetoDosController(IObjetoDosRepository ObjetoDosRepository,
            IMapper mapper, ILogger<ObjetoDosController> logger)
            : base(ObjetoDosRepository, mapper, logger)
        {

        }
    }
}

