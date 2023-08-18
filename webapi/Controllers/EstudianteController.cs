using API.Controllers.Base;
using Application.Features.Estudiante.Queries;
using Application.Features.PreguntaPsicologica.Queries;
using Application.Wrappers;
using Domain.Entities;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class EstudianteController: BaseController
    {

        /// <summary>
        /// Lista de preguntas psicologicas de una evaluacion psicologica (Test Psicologico)
        /// </summary>
        [HttpGet("{personaId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<IList<EstudianteDto>>>> InformacionEstudiante(int personaId)
        {
            return Ok(await Mediator.Send(new InformacionEstudianteQuery() {
                PersonaId = personaId,
            }));
        }
    }
}
