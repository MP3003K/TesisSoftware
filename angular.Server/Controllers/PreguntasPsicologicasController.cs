using Application.Wrappers;
using Context;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace webapi.Controllers
{
    public class Question {
        public int Id { get; set; }
        public required string Text { get; set; }
        public string? Answer { get; set; }
        public int Order { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class PreguntasPsicologicasController(DapperContext context): ControllerBase
    {

        /// <summary>
        /// Lista de preguntas psicologicas de una evaluacion psicologica (Test Psicologico)
        /// </summary>
        [HttpGet("{evaluationId:int}")]
        public async Task<ActionResult> ListaDePreguntasPsicologicas(int evaluationId, int? limit = null, int? start = null)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {

                    var questions = await connection.QueryAsync<Question>("LISTAR_PREGUNTAS", new { evaluationId, limit, start }, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = null, Succeeded = true, Data = questions.ToList() });

                }
            }
            catch (SqlException ex)
            {
                return BadRequest(new
                {
                    error = ex.Message,
                    errorNumber = ex.Number
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
