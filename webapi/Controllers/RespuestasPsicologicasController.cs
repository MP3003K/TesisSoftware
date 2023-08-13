using API.Controllers.Base;
using Application.Features.RespuestaPsicologica.Queries;
using Application.Wrappers;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class RespuestasPsicologicasController: BaseController
    {
        /// <summary>
        /// Lista de preguntas psicologicas y respuestas realizadas por un estudiante
        /// </summary>

        [HttpGet()]
        [Route("{evaPsiEstId:int}/{pageNumber:int}/{pageSize:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<IList<RespuestaPsicologicaDto>>>> ListaDeRespuestasPsicologicas(int evaPsiEstId, int pageNumber, int pageSize)
        {
            return Ok(await Mediator.Send(new ListaRespuestasPsicologicasQuery() { EvaPsiEstId  = evaPsiEstId }));
        }
    }
}
