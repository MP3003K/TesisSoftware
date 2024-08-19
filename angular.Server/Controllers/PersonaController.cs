using Microsoft.AspNetCore.Mvc;
using Context;
using System.Data;
using Dapper;
using Application.Wrappers;
using System.Security.Claims;
using System.Text.Json;

namespace Controllers
{
    public class CreatePerson
    {
        public string? Nombres { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public int DNI { get; set; } = 0;
        public int AulaId { get; set; } = 0;
    }

    public class QueryPerson
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? DNI { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class PersonaController(DapperContext context) : ControllerBase
    {

        [HttpGet()]
        public ActionResult ObtenerPersonaPorConsulta(string query, int? classroomId, int? unityId)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    List<QueryPerson> people = connection.Query<QueryPerson>("OBTENER_PERSONA_POR_CONSULTA", new { query, classroomId, unityId }, commandType: CommandType.StoredProcedure).ToList();
                    return Ok(new Response<List<QueryPerson>> { Message = null, Succeeded = true, Data = people });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AgregarEvaPisEstudiante")]
        public async Task<ActionResult> AddPersonaConEvaPsiEstudiante([FromBody] CreatePerson person)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {

                    var classrooms = await connection.QueryAsync("CREAR_ESTUDIANTE", new { firstName = person.Nombres, firstLastName = person.ApellidoPaterno, secondLastName = person.ApellidoMaterno, documentNumber = person.DNI, classroomId = person.AulaId }, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = null, Succeeded = true, Data = classrooms.ToList() });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class RespuestaEstudiante
        {
            public bool esEmocional { get; set; } = false;
            public string respuesta { get; set; } = string.Empty;
        }

        [HttpPost("registrarExamenEstudiante")]
        public async Task<ActionResult> RegistrarExamenEstudiante([FromBody] RespuestaEstudiante dto)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    var userIdClaim = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
                    if (string.IsNullOrEmpty(userIdClaim))return BadRequest("User identifier claim is missing.");

                    int userId;
                    if (!int.TryParse(userIdClaim, out userId)) return BadRequest("Invalid user identifier.");

                    var reporteExamen = await connection.QueryAsync("PROC_REGISTRAR_EXAMEN_ESTUDIANTE",
                     new
                     {
                         @v_userId = userId,
                         @v_esEmocional = dto.esEmocional,
                         @v_respuesta = dto.respuesta
                     }, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = null, Succeeded = true, Data = reporteExamen.ToList() });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
