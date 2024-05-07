using Application.Wrappers;
using Context;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace webapi.Controllers
{
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

                    var questions = await connection.QueryAsync("LISTAR_PREGUNTAS", new { evaluationId, limit, start }, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = null, Succeeded = true, Data = questions.ToList() });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
