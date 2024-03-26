using DTOs;
using Microsoft.AspNetCore.Mvc;
using Controllers.Base;
using Context;
using System.Data;
using Dapper;

namespace Controllers
{
    public class EstudianteController(DapperContext context) : BaseController
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
                    return Ok();
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
