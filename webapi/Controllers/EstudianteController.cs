using API.Controllers.Base;
using Application.Features.Aula.Queries;
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
