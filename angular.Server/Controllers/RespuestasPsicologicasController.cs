using Application.Wrappers;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Context;
using System.Data;
using Dapper;

namespace Controllers
{
    public class CreateAnswer
    {
        public string? Answer { get; set; }
        public int? EvaluationId { get; set; }
        public int? QuestionId { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class RespuestasPsicologicasController(DapperContext context) : ControllerBase
    {
        [HttpGet("RespuestasAula/all")]
        public async Task<ActionResult> RespuestasPsicologicas()
        {
            try
            {

                using (var connection = context.CreateConnection())
                {
                    await connection.QueryAsync("LISTAR_RESPUESTAS_AULA", new { evaluationId = 1 }, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = "Listado Correctamente", Succeeded = true, Data = null });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("RespuestasAula/{evaluationId:int}")]
        public async Task<ActionResult> RespuestasPsicologicasAula(int evaluationId)
        {
            try
            {

                using (var connection = context.CreateConnection())
                {
                    await connection.QueryAsync("LISTAR_RESPUESTAS_AULA", new { evaluationId  }, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = "Listado Correctamente", Succeeded = true, Data = null });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Resultados psicologicos obtenido por un Estudiante en un Unidad
        /// </summary>
        /// 
        [HttpGet("RespuestasEstudiante/{evaluationId:int}")]
        public async Task<ActionResult> RespuestasPsicologicasEstudiante(int evaluationId)
        {
           
            try
            {

                using (var connection = context.CreateConnection())
                {
                    await connection.QueryAsync("LISTAR_RESPUESTAS_ESTUDIANTE", new { evaluationId }, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = "Listado Correctamente", Succeeded = true, Data = null });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddRespuestaPsicologica([FromBody] CreateAnswer answer)
        {
            try
            {

                using (var connection = context.CreateConnection())
                {
                    await connection.QueryAsync("CREATE_ANSWER", answer, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = "Creado Correctamente", Succeeded = true, Data = null });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        /// Actualizar el Estado de una Evalucion Psicologia Estudiante, para actualizarlo a "En Proceso (P)" o "Finalizado (F)"
        /// </summary>
        /// 
        [HttpPut("ActualizarEstadoEvaPsiEst/{evaluationId:int}")]
        public async Task<ActionResult<Response<UnidadDto>>> ActualizarEstadoEvaPsiEst(int evaluationId)
        {
            try
            {

                using (var connection = context.CreateConnection())
                {
                    var report = await connection.QueryAsync("ACTUALIZAR_ESTADO_EVALUACION", new { evaluationId }, commandType: CommandType.StoredProcedure);
                    return Ok(report.ToList());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
      
        }

        [HttpGet("ReporteEstudiante/{evaluacionId:int}/{estudianteId:int}/{codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ReporteEstudiante(int evaluacionId, int estudianteId, string codigo)
        {
            try
            {

                using (var connection = context.CreateConnection())
                {

                    var report = await connection.QueryAsync("GET_REPORTE_ESTUDIANTE", new { IdEvaluacion = evaluacionId, IdEstudiante = estudianteId, Codigo = codigo }, commandType: CommandType.StoredProcedure);
                    return Ok(report.ToList());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
