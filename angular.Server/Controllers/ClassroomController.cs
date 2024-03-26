using System.Data;
using Context;
using Controllers.Base;
using Dapper;
using Microsoft.AspNetCore.Mvc;


namespace Controllers
{
    public class ClassroomController(DapperContext context) : BaseController
    {

        /// <summary>
        /// Lista de unidades
        /// </summary>
        [HttpGet("unidades/all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UnidadesAll()
        {
            try
            {
                using (var connection = context.CreateConnection())
                {

                    var classrooms = await connection.QueryAsync("SELECT * from UNIDADES order by Año, NUnidad;");
                    return Ok(classrooms.ToList());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Procedimiento para crear o detener una evaluacion psicologica a un aula
        /// </summary>
        [HttpGet("crearEvalucionAula/{aulaId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> proc_crear_evalucion_psicologica_aula(int aulaId)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {

                    await connection.QueryAsync("proc_crear_evalucion_psicologica_aula", new { v_aulaId = aulaId }, commandType: CommandType.StoredProcedure);
                    return Ok();
                }
   
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Obtener los estudiantes de una evaluacion psicologica de un aula
        /// </summary>
        [HttpGet("getEstudiantesDeEvalucionAula/{aulaId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEstudiantesDeEvalucionAula(int aulaId)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    
                    var students = await connection.QueryAsync("get_estudiantes_de_evalucion_aula", new { v_aulaid=aulaId }, commandType: CommandType.StoredProcedure);
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
