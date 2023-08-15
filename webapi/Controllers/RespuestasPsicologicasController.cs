using API.Controllers.Base;
using Application.Features.PreguntaPsicologica.Queries;
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
        /// Resultados psicologicos de un estudiante
        /// </summary>
        /// 
        [HttpGet()]
        [HttpGet("{estudianteId:int}/{dimensionId:int}/{anio:int}/{nUnidad:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<UnidadDto>>> RespuestasPsicologicasEstudiante(int estudianteId, int dimensionId, int anio, int nUnidad)
        {
            return Ok(await Mediator.Send(new ResultadosPsicologicosEstudiantesQuery()
            {
                EstudianteId = estudianteId,
                DimensionId = dimensionId,
                Anio = anio,
                NUnidad = nUnidad,
            }));
        }

    }
}
