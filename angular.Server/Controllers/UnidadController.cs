using Application.Wrappers;
using Context;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UnidadController(DapperContext context): ControllerBase
    {
        /// <summary>
        /// Lista de Unidades escolares
        /// </summary>

        [HttpGet("all")]
        public async Task<ActionResult> ListaDeUnidadesEscolares()
        {
            try
            {
                using (var connection = context.CreateConnection())
                {

                    var questions = await connection.QueryAsync("LISTAR_UNIDADES", commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = null, Succeeded = true, Data = questions.ToList() });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Unidad escolar actual
        /// </summary>
        /// 
        [HttpGet("unidadActual")]
        public async Task<ActionResult> UnidadActual()
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    var unities = await connection.QueryAsync("OBTENER_UNIDAD_ACTUAL", commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = null, Succeeded = true, Data = unities.FirstOrDefault() });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Unidad escolar actual
        /// </summary>
        /// 
        [HttpGet("unidadesEstudiantes/{studentId:int}")]
        public async Task<ActionResult> UnidadEstudiante(int studentId)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    var unities = await connection.QueryAsync("LISTAR_UNIDADES_POR_ESTUDIANTE", new { studentId }, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = null, Succeeded = true, Data = unities.FirstOrDefault() });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          
        }
    }
}
