using Application.Features.RespuestaPsicologica.Commands;
using Application.Features.RespuestaPsicologica.Queries;
using Application.Wrappers;
using Controllers.Base;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Context;

namespace Controllers
{
    public class RespuestasPsicologicasController: BaseController
    {
        /// <summary>
        /// Resultados psicologicos obtenido por un Aula en una Unidad
        /// </summary>
        /// 
        private readonly ApplicationDbContext _context;

        public RespuestasPsicologicasController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [HttpGet("RespuestasAula/all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<dynamic> RespuestasPsicologicas()
        {
            return _context.Database.SqlQuery<dynamic>($"with ctx\r\nas (\r\nselect\r\n\tip.Id IdIndicador,\r\n\tip.NombreIndicador,\r\n\tep.Id IdEscala,\r\n\tep.Nombre NombreEscala,\r\n\tdp.Id IdDimension,\r\n\tAVG(cast(rp.Respuesta as numeric(1, 0))) PromedioIndicador\r\nfrom\r\n\tDimensionesPsicologicas dp\r\njoin EscalasPsicologicas ep on\r\n\t(dp.Id = ep.DimensionId)\r\njoin IndicadoresPsicologicos ip on\r\n\t(ep.Id = ip.EscalaPsicologicaId)\r\njoin PreguntasPsicologicas pp on\r\n\t(ip.Id = pp.IndicadorPsicologicoId)\r\njoin\r\n\tRespuestasPsicologicas rp on\r\n\tpp.Id = rp.PreguntaPsicologicaId\r\njoin EvaluacionesPsicologicasEstudiante epe on\r\n\t(rp.EvaPsiEstId = epe.Id)\r\njoin EvaluacionesPsicologicasAula epa on\r\n\t( epe.EvaluacionAulaId = epa.Id)\r\njoin EvaluacionesPsicologicas evp on\r\n\t(epa.EvaluacionPsicologicaId = evp.Id)\r\nwhere\r\n\tepe.Estado = 'F'\r\n\tand evp.Id = 1\r\ngroup by\r\n\tip.Id,\r\n\tip.NombreIndicador,\r\n\tep.Id,\r\n\tep.Nombre,\r\n\tdp.Id\r\n)\r\nselect\r\n\tctx1.IdDimension,\r\n\tctx1.IdEscala,\r\n\tctx1.NombreEscala,\r\n\tctx2.PromedioEscala,\r\n\tctx1.IdIndicador,\r\n\tctx1.NombreIndicador,\r\n\tctx1.PromedioIndicador\r\nfrom\r\n\t(\r\n\tselect\r\n\t\t*\r\n\tfrom\r\n\t\tctx\r\n) ctx1\r\njoin\r\n\t(\r\n\tselect\r\n\t\tIdEscala,\r\n\t\tROUND(AVG(PromedioIndicador), 1) PromedioEscala\r\n\tfrom\r\n\t\tctx\r\n\tgroup by\r\n\t\tIdEscala\r\n) ctx2 on\r\n\t(ctx1.IdEscala = ctx2.IdEscala);\r\n");
        }
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

        [HttpGet("ReporteEstudiante/{evaluacionId:int}/{estudianteId:int}/{codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ReporteEstudiante(int evaluacionId ,int estudianteId, string codigo)
        {
            try
            {

                var response = await _context.ReporteEstudiantes.FromSqlInterpolated($"EXEC GET_REPORTE_ESTUDIANTE @IdEvaluacion = {evaluacionId}, @IdEstudiante = {estudianteId}, @Codigo = {codigo}").ToListAsync();


                if (response.IsNullOrEmpty())
                {
                    return NotFound();
                }
                return Ok(response);
            }  catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
