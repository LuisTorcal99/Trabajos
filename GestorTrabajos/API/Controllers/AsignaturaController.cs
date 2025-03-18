using API.Models.DTOs;
using API.Models.DTOs.Asignatura;
using API.Models.DTOs.Curso;
using API.Models.Entity;
using API.Repository;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignaturaController : BaseController<AsignaturaEntity, AsignaturaDTO, CreateAsignaturaDTO>
    {
        private readonly IAsignaturaRepository _asignaturaRepository;
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;
        protected ResponseApi _reponseApi;
        public AsignaturaController(IAsignaturaRepository asignaturaRepository, IMapper mapper, ILogger<AsignaturaController> logger) : base(asignaturaRepository, mapper, logger)
        {
            _asignaturaRepository = asignaturaRepository;
            _reponseApi = new ResponseApi();
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize(Roles = "admin,profesor")]
        [HttpPost("crear")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Crear(CreateAsignaturaDTO createAsignaturaDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _reponseApi.StatusCode = HttpStatusCode.BadRequest;
                    _reponseApi.IsSuccess = false;
                    _reponseApi.ErrorMessages.Add("Formato de asignatura inválido");
                    return BadRequest(_reponseApi);
                }

                var asignaturaEntity = _mapper.Map<AsignaturaEntity>(createAsignaturaDTO);
                var responseCurso = await _asignaturaRepository.CreateAsync(asignaturaEntity);

                _reponseApi.StatusCode = HttpStatusCode.OK;
                _reponseApi.IsSuccess = true;
                return Ok(_reponseApi);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Error al crear la asignatura: " + ex.Message + ":\n" + ex.InnerException);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error creating asignatura: " + ex.Message);
            }
        }
    }
}
