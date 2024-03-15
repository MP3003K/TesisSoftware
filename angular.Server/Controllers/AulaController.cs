using Application.Features.Aula.Queries;
using Application.Features.Unidad.Queries;
using Application.Wrappers;
using Controllers.Base;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    public class AulaController: BaseController
    {
        /// <summary>
        /// Lista de Unidades escolares
        /// </summary>

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<IList<AulaDto>>>> ListaDeAulas()
        {
            return Ok(await Mediator.Send(new ListaAulasQuery()));
        }

        /// <summary>
        /// Lista de Aulas registradas en EvalucionPsicologicaEstudiante
        /// </summary>

        [HttpGet("aulasEstudiante/{estudianteId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<IList<AulaDto>>>> ListaAulasEstudiantes(int estudianteId)
        {
            return Ok(await Mediator.Send(new ListaAulasEstudianteQuery()
            {
                EstudianteId = estudianteId
            }));
        }

    }
}
