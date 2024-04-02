using System.Data;
using Context;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using DTOs.Exceptions;
using Application.Wrappers;


namespace Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ClassroomController(DapperContext context) : ControllerBase
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
                    return Ok(new Response<dynamic> { Message = "Listado Correctamente", Succeeded = true, Data = classrooms.ToList() });

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
                    return Ok(new Response<dynamic> { Message = "Creado Correctamente", Succeeded = true, Data = null });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Obtener los estudiantes de una Aula en una Unidad
        /// </summary>
        [HttpGet("getEstudiantesByAulaYUnidad/{aulaId:int}/{unidadId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEstudiantesDeEvalucionAula(int aulaId, int unidadId)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {

                    var students = await connection.QueryAsync("LISTAR_ESTUDIANTES_POR_AULA_Y_UNIDAD", new { v_aulaid = aulaId, v_unidadId = unidadId }, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = null, Succeeded = true, Data = students.ToList() });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
