using API.Controllers.Base;
using Application.Features.Unidad.Queries;
using Application.Wrappers;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
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
    }
}
