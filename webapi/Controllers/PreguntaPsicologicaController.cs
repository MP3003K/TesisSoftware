using API.Controllers.Base;
using Application.Features.RespuestasPsicologicas.Queries;
using Application.Wrappers;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class PreguntaPsicologicaController : BaseController
    {

        /// <summary>
        /// Lista de preguntas psicologicas
        /// </summary>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<IList<PreguntaPsicologicaDto>>>> ListAllBank()
        {
            return Ok(await Mediator.Send(new ListaPreguntasPsicologicasQuery()));
        }
    }
}
