using Context;
using Controllers.Base;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace webapi.Controllers
{
    public class PreguntasPsicologicasController(DapperContext context): BaseController
    {

        /// <summary>
        /// Lista de preguntas psicologicas de una evaluacion psicologica (Test Psicologico)
        /// </summary>
        [HttpGet("{evaluationId:int}")]
        public async Task<ActionResult> ListaDePreguntasPsicologicas(int evaluationId, int limit, int start)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {

                    var questions = await connection.QueryAsync("LISTAR_PREGUNTAS", new { evaluationId, limit, start }, commandType: CommandType.StoredProcedure);
                    return Ok(questions.ToList());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
