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
    public class ClassroomStudentAnswer
    {
        public string? Id { get; set; }
        public string? Nombre { get; set; }
        public double? Promedio { get; set; }
    }
    public class StudentReport
    {
        public string? Dimension { get; set; }
        public double? DimensionMean { get; set; }
        public string? Scale { get; set; }
        public double? ScaleMean { get; set; }
        public string? Indicator { get; set; }
        public double? IndicatorMean { get; set; }
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
                    var response = await connection.QueryAsync("LISTAR_RESPUESTAS_AULA", new { evaluationId }, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = "Listado Correctamente", Succeeded = true, Data = response.ToList() });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("RespuestasEstudianteAula/{evaluationId:int}")]
        public async Task<ActionResult> RespuestasPsicologicasEstudianteAula(int evaluationId)
        {
            try
            {

                using (var connection = context.CreateConnection())
                {
                    var response = await connection.QueryAsync<ClassroomStudentAnswer>("LISTAR_RESPUESTAS_ESTUDIANTES_AULA", new { evaluationId }, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = "Listado Correctamente", Succeeded = true, Data = response.ToList() });
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
        [HttpGet("RespuestasEstudiante/{code}/{evaluationId:int}")]
        public async Task<ActionResult> RespuestasPsicologicasEstudiante(string code, int evaluationId)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    var responses = await connection.QueryAsync<StudentReport>("LISTAR_RESPUESTAS_ESTUDIANTE", new { code, evaluationId }, commandType: CommandType.StoredProcedure);
                    foreach (var response in responses)
                    {
                        Console.WriteLine(response.Dimension);
                        Console.WriteLine(response.Scale);
                        Console.WriteLine(response.Indicator);

                    }
                    return Ok(new Response<dynamic> { Message = "Listado Correctamente", Succeeded = true, Data = responses.ToList() });

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
                    await connection.QueryAsync("COMPLETAR_RESPUESTA", answer, commandType: CommandType.StoredProcedure);
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
        [HttpGet("resultadosPsiAulaExcel/{aulaId:int}/{unidadId:int}")]
        public async Task<ActionResult> ResultadosPsicologicosAulaExcel(int aulaId, int unidadId)
        {
            try
            {

                using (var connection = context.CreateConnection())
                {
                    var response = await connection.QueryAsync("OBTENER_RESULTADOS_AULA_EXCEL", new { v_aulaId = aulaId, v_unidadId = unidadId }, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = "Listado Correctamente", Succeeded = true, Data = response.ToList() });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
