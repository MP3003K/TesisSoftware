using API.Controllers.Base;
using Application.Features.PreguntaPsicologica.Queries;
using Application.Wrappers;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class PreguntasPsicologicasController: BaseController
    {

        /// <summary>
        /// Lista de preguntas psicologicas de una evaluacion psicologica (Test Psicologico)
        /// </summary>
        [HttpGet()]
        [HttpGet("{evaPsiId:int}/{evaPsiEstId:int}/{pageNumber:int}/{pageSize:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<IList<PreguntaPsicologicaDto>>>> ListaDePreguntasPsicologicas(int evaPsiId, int evaPsiEstId, int pageNumber, int pageSize)
        {
            return Ok(await Mediator.Send(new ListaPreguntasPsicologicasQuery() { 
                EvaPsiId = evaPsiId, 
                EvaPsiEstId = evaPsiEstId, 
                PageNumber = pageNumber,
                PageSize = pageSize}));
        }
    }
}
