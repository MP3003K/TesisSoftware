using Context;
using Controllers.Base;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Controllers
{
    public class AulaController(DapperContext ctx) : BaseController
    {
        [HttpGet("all")]
        public async Task<ActionResult> ListaDeAulas()
        {
            try
            {
                using (var connection = ctx.CreateConnection())
                {

                    var classrooms = await connection.QueryAsync("LISTAR_AULAS", commandType: CommandType.StoredProcedure);
                    return Ok(classrooms.ToList());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lista de Aulas registradas en EvalucionPsicologicaEstudiante
        /// </summary>

        [HttpGet("aulasEstudiante/{estudianteId:int}")]
        public async Task<ActionResult> ListaAulasEstudiantes(int studentId)
        {
            try
            {
                using (var connection = ctx.CreateConnection())
                {

                    var classrooms = await connection.QueryAsync("LISTAR_AULAS_POR_ESTUDIANTE", new { studentId }, commandType: CommandType.StoredProcedure);
                    return Ok(classrooms.ToList());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
