using Context;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace angular.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluacionPsicologicaController(DapperContext context) : ControllerBase
    {
        /// <summary>
        /// Lista de los estudiantes que participaron en una Evalucion Psicologica
        /// </summary>
        [HttpGet("{evaluationId:int}")]
        public async Task<ActionResult> ListaEstudiantesEvaPsiAula(int evaluationId)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {

                    var students = await connection.QueryAsync("LISTAR_ESTUDIANTES_POR_EVALUACION_AULA", new
                    {
                        evaluationId
                    }, commandType: CommandType.StoredProcedure);
                    return Ok(students.ToList());
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
