using API.Controllers.Base;
using Application.Features.Aula.Queries;
using Application.Features.Estudiante.Queries;
using Application.Features.PreguntaPsicologica.Queries;
using Application.Wrappers;
using Domain.Entities;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Context;

namespace webapi.Controllers
{
    public class EstudianteController: BaseController
    {
        private readonly ApplicationDbContext _context;

        public EstudianteController(ApplicationDbContext context)
        {
            this._context = context;
        }
        /// <summary>
        /// Informacion Basica de un Estudiante
        /// </summary>
        [HttpGet("{personaId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<IList<EstudianteDto>>>> InformacionEstudiante(int personaId)
        {
            return Ok(await Mediator.Send(new InformacionEstudianteQuery() {
                PersonaId = personaId,
            }));
        }


        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CrearEstudiante(CrearEstudianteDto crearEstudianteDto)
        {
            try
            {
                Console.WriteLine(crearEstudianteDto.Nombres);
                var response = await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC PROC_CREAR_ESTUDIANTE_Y_EVALUACION @v_aulaId = {crearEstudianteDto.AulaId}, @v_nombreEstudiante = {crearEstudianteDto.Nombres}, @v_ape_pat = {crearEstudianteDto.ApellidoPaterno}, @v_ape_mat = {crearEstudianteDto.ApellidoMaterno}, @v_dni = {crearEstudianteDto.DNI}");
                Console.Write(response);
                return Ok(response);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lista de los estudiantes que participaron en una Evalucion Psicologica
        /// </summary>
        [HttpGet("{aulaId:int}/{unidadId:int}/{dimensionId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<IList<EstudianteDto>>>> ListaEstudiantesEvaPsiAula(int aulaId, int unidadId, int dimensionId)
        {
            return Ok(await Mediator.Send(new ListaEstudiantesDeUnAulaQuery()
            {
                AulaId = aulaId,
                UnidadId = unidadId,
                DimensionId = dimensionId
            }));
        }
    }
}
