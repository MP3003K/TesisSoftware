using API.Controllers.Base;
using Application.Features.RespuestaPsicologica.Queries;
using Application.Features.Unidad.Queries;
using Application.Wrappers;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class RespuestasPsicologicasController: BaseController
    {

        /// <summary>
        /// Unidad escolar actual
        /// </summary>
        /// 
        [HttpGet("unidadActual")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<UnidadDto>>> UnidadActual()
        {
            return Ok(await Mediator.Send(new ResultadosPsicologicosEstudiantesQuery()));
        }
    }
}
