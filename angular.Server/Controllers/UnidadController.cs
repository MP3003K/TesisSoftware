using Application.Features.Unidad.Queries;
using Application.Wrappers;
using Controllers.Base;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    public class UnidadController: BaseController
    {
        /// <summary>
        /// Lista de Unidades escolares
        /// </summary>

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<IList<UnidadDto>>>> ListaDeUnidadesEscolares()
        {
            return Ok(await Mediator.Send(new ListaUnidadesQuery()));
        }

        /// <summary>
        /// Unidad escolar actual
        /// </summary>
        /// 
        [HttpGet("unidadActual")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<UnidadDto>>> UnidadActual()
        {
            return Ok(await Mediator.Send(new UnidadActualQuery()));
        }

        /// <summary>
        /// Unidad escolar actual
        /// </summary>
        /// 
        [HttpGet("unidadesEstudiantes/{estudianteId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<UnidadDto>>> UnidadActual(int estudianteId)
        {
            return Ok(await Mediator.Send(new ListaUnidadesEstudianteQuery()
            {
                EstudianteId = estudianteId
            }));
        }
    }
}
