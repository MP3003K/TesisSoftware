using DTOs;
using Microsoft.AspNetCore.Mvc;
using Context;
using System.Data;
using Dapper;
using Application.Wrappers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Controllers
{

    public class EvaluationStudent {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int EstudianteId { get; set; }
        public int EvaluacionAulaId { get; set; }
        public string? Estado { get; set; }

    }

    [ApiController]
    [Route("[controller]")]
    public class EstudianteController(DapperContext context) : ControllerBase
    {

        [Authorize]
        [HttpGet("evaluations")]
        public async Task<ActionResult> GetStudentEvaluations()
        {
            try
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                Console.WriteLine(userId);
                using (var connection = context.CreateConnection())
                {

                    var evaluations = await connection.QueryAsync<EvaluationStudent>("OBTENER_EVALUACIONES", new
                    {
                        userId
                    }, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = "Creado Correctamente", Succeeded = true, Data = evaluations.ToList() });

                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [Authorize]
        [HttpGet]
        public async Task<ActionResult> InformacionEstudiante()
        {
            try
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                using (var connection = context.CreateConnection())
                {

                    var students = await connection.QueryAsync("OBTENER_ESTUDIANTE", new
                    {
                        userId
                    }, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = "Creado Correctamente", Succeeded = true, Data = students.FirstOrDefault() });

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

                    await connection.QueryAsync("PROC_CREAR_ESTUDIANTE", new
                    {
                        v_aulaId = crearEstudianteDto.AulaId,
                        v_nombreEstudiante = crearEstudianteDto.Nombres,
                        v_ape_pat = crearEstudianteDto.ApellidoPaterno,
                        v_ape_mat = crearEstudianteDto.ApellidoMaterno,
                        v_dni = crearEstudianteDto.DNI,
                        v_unidadId = crearEstudianteDto.UnidadId
                    }, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = "Creado Correctamente", Succeeded = true, Data = crearEstudianteDto });
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


    }
}
