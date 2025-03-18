using API.Models.DTOs;
using System.Net;
using API.Models.DTOs.Propuesta;
using API.Models.DTOs.UserDTO;
using API.Models.Entity;
using API.Repository;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Humanizer;
using API.Data;
using API.Models.DTOs.AsignarPropuesta;
using System.Security.Claims;
using API.Models.DTOs.UserDto;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropuestaController : BaseController<PropuestaEntity, PropuestaDTO, CreatePropuestaDTO>
    {
        private readonly IPropuestaRepository _propuestaRepository;
        private readonly IUserPracticoRepository _userPracticoRepository;
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;
        protected ResponseApi _reponseApi;
        private readonly ApplicationDbContext _context;
        public PropuestaController(IPropuestaRepository propuestaRepository, IUserPracticoRepository userPracticoRepository, IMapper mapper, ILogger<PropuestaRepository> logger, ApplicationDbContext context) : base(propuestaRepository, mapper, logger)
        {
            _propuestaRepository = propuestaRepository;
            _userPracticoRepository= userPracticoRepository;
            _reponseApi = new ResponseApi();
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        [Authorize(Roles = "alumno,profesor")]
        [HttpPost("enviar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Proponer([FromBody] CreatePropuestaDTO createPropuestaDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _reponseApi.StatusCode = HttpStatusCode.BadRequest;
                    _reponseApi.IsSuccess = false;
                    _reponseApi.ErrorMessages.Add("Formato de propuesta inválido");
                    return BadRequest(_reponseApi);
                }
                //
                if (string.IsNullOrEmpty(createPropuestaDto.Email))
                {
                    _reponseApi.StatusCode = HttpStatusCode.BadRequest;
                    _reponseApi.IsSuccess = false;
                    _reponseApi.ErrorMessages.Add("Correo electrónico del usuario es requerido.");
                    return BadRequest(_reponseApi);
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == createPropuestaDto.Email);
                if (user == null)
                {
                    _reponseApi.StatusCode = HttpStatusCode.BadRequest;
                    _reponseApi.IsSuccess = false;
                    _reponseApi.ErrorMessages.Add("Usuario no encontrado con el correo electrónico proporcionado.");
                    return BadRequest(_reponseApi);
                }
                //

                var propuestaEntity = _mapper.Map<PropuestaEntity>(createPropuestaDto);
                var responsePropuesta = await _propuestaRepository.CreateAsync(propuestaEntity);
                
                if (!user.Propuestas.Contains(propuestaEntity)) 
                {
                    user.Propuestas.Add(propuestaEntity);
                    await _context.SaveChangesAsync();
                }
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
        /// <summary>
        /// Asigna un usuario a una propuesta (Many-to-Many)
        /// </summary>
        [HttpPost("asignarUsuario")]
        public async Task<IActionResult> AsignarUsuarioAPropuesta([FromBody] AsignarPropuestaDTO request)
        {
            var user = await _context.Users.Include(u => u.Propuestas)
                .FirstOrDefaultAsync(u => u.Id == request.UserId);

            var propuesta = await _context.Propuesta.FirstOrDefaultAsync(p => p.Id == request.PropuestaId);

            if (user == null || propuesta == null)
                return NotFound("Usuario o propuesta no encontrados.");

            if (user.Propuestas.Contains(propuesta))
                return BadRequest("El usuario ya está asignado a esta propuesta.");

            user.Propuestas.Add(propuesta);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Usuario asignado a la propuesta correctamente." });
        }

        /// <summary>
        /// Elimina la relación entre un usuario y una propuesta
        /// </summary>
        [HttpDelete("quitarUsuario")]
        public async Task<IActionResult> QuitarUsuarioDePropuesta([FromBody] AsignarPropuestaDTO request)
        {
            var user = await _context.Users.Include(u => u.Propuestas)
                .FirstOrDefaultAsync(u => u.Id == request.UserId);

            var propuesta = await _context.Propuesta.FirstOrDefaultAsync(p => p.Id == request.PropuestaId);

            if (user == null || propuesta == null)
                return NotFound("Usuario o propuesta no encontrados.");

            if (!user.Propuestas.Contains(propuesta))
                return BadRequest("El usuario no está asignado a esta propuesta.");

            user.Propuestas.Remove(propuesta);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Usuario eliminado de la propuesta." });
        }

        /// <summary>
        /// Obtiene los usuarios asignados a una propuesta
        /// </summary>
        [HttpGet("{propuestaId}/usuarios")]
        public async Task<IActionResult> ObtenerUsuariosDePropuesta(int propuestaId)
        {
            var propuesta = await _context.Propuesta
                .Include(p => p.Users)
                .FirstOrDefaultAsync(p => p.Id == propuestaId);

            if (propuesta == null)
                return NotFound("Propuesta no encontrada.");

            return Ok(propuesta.Users.Select(u => new { u.Id, u.Name, u.Email }));
        }
    }
}