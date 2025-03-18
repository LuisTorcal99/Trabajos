using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using API.Models.DTOs.Curso;
using API.Repository;
using API.Models.Entity;
using API.Models.DTOs.Propuesta;
using API.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : BaseController<CursoEntity, CursoDTO, CreateCursoDTO>
    {
        private readonly ICursoRepository _cursoRepository;
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;
        protected ResponseApi _reponseApi;
        public CursoController(ICursoRepository cursoRepository, IMapper mapper, ILogger<CursoController> logger) : base(cursoRepository, mapper, logger)
        {
            _cursoRepository = cursoRepository;
            _reponseApi = new ResponseApi();
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize(Roles = "admin,profesor")]
        [HttpPost("crear")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Proponer(CreateCursoDTO createCursoDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _reponseApi.StatusCode = HttpStatusCode.BadRequest;
                    _reponseApi.IsSuccess = false;
                    _reponseApi.ErrorMessages.Add("Formato de curso inválido");
                    return BadRequest(_reponseApi);
                }

                var cursoEntity = _mapper.Map<CursoEntity>(createCursoDTO);
                var responseCurso = await _cursoRepository.CreateAsync(cursoEntity);

                _reponseApi.StatusCode = HttpStatusCode.OK;
                _reponseApi.IsSuccess = true;
                return Ok(_reponseApi);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Error al crear la propuesta:: " + ex.Message + ":\n" + ex.InnerException);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error creating propuesta: " + ex.Message);
            }
        }
    }
}