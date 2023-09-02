using API.Controllers.Base;
using Application.Features.PreguntaPsicologica.Queries;
using Application.Features.RespuestaPsicologica.Commands;
using Application.Features.RespuestaPsicologica.Queries;
using Application.Features.Unidad.Queries;
using Application.Wrappers;
using Domain.Entities;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class RespuestasPsicologicasController: BaseController
    {
        /// <summary>
        /// Resultados psicologicos obtenido por un Aula en una Unidad
        /// </summary>
        /// 
        [HttpGet("RespuestasAula/{aulaId:int}/{dimensionId:int}/{unidadId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<UnidadDto>>> RespuestasPsicologicasAula(int aulaId, int dimensionId, int unidadId)
        {
            return Ok(await Mediator.Send(new ResultadosPsicologicosAulaQuery()
            {
                AulaId = aulaId,
                DimensionId = dimensionId,
                UnidadId = unidadId
            }));
        }

        /// <summary>
        /// Resultados psicologicos obtenido por un Estudiante en un Unidad
        /// </summary>
        /// 
        [HttpGet("RespuestasEstudiante/{estudianteId:int}/{unidadId:int}/{aulaId:int}/{dimensionId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<UnidadDto>>> RespuestasPsicologicasEstudiante(int estudianteId, int unidadId, int aulaId, int dimensionId)
        {
            return Ok(await Mediator.Send(new ResultadosPsicologicosEstudiantesQuery()
            {
                EstudianteId = estudianteId,
                UnidadId = unidadId,
                AulaId = aulaId,
                DimensionId = dimensionId,
            }));
        }
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<RespuestaPsicologicaDto>>> AddRespuestaPsicologica([FromBody] AgregarRespuestaPsicologicaRequest request)
        {
            return Created("", await Mediator.Send(request));
        }



        /// <summary>
        /// Actualizar el Estado de una Evalucion Psicologia Estudiante, para actualizarlo a "En Proceso (P)" o "Finalizado (F)"
        /// </summary>
        /// 
        [HttpGet("ActualizarEstadoEvaPsiEst/{evaPsiEstId:int}/{estado}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<UnidadDto>>> ActualizarEstadoEvaPsiEst(int evaPsiEstId, string estado)
        {
            return Ok(await Mediator.Send(new ActualizarEstadoEvaPsiEstRequest()
            {
                EvaPsiEstId = evaPsiEstId,
                Estado = estado
            }));
        }
    }
}
