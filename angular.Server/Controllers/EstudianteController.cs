using DTOs;
using Microsoft.AspNetCore.Mvc;
using Context;
using System.Data;
using Dapper;
using Application.Wrappers;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstudianteController(DapperContext context):ControllerBase
    {
        /// <summary>
        /// Informacion Basica de un Estudiante
        /// </summary>
        [HttpGet("{studentId:int}")]
        public async Task<ActionResult> InformacionEstudiante(int studentId)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {

                    await connection.QueryAsync("PROC_CREAR_ESTUDIANTE_Y_EVALUACION", new
                    {
                        studentId
                    }, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = "Creado Correctamente", Succeeded = true, Data = null });

                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CrearEstudiante(CrearEstudianteDto crearEstudianteDto)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {

                    await connection.QueryAsync("PROC_CREAR_ESTUDIANTE_Y_EVALUACION", new
                    {
                        v_aulaId = crearEstudianteDto.AulaId,
                        v_nombreEstudiante = crearEstudianteDto.Nombres,
                        v_ape_pat = crearEstudianteDto.ApellidoPaterno,
                        v_ape_mat = crearEstudianteDto.ApellidoMaterno,
                        v_dni = crearEstudianteDto.DNI
                    }, commandType: CommandType.StoredProcedure);
                    return Ok();
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        
    }
}
