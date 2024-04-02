using Microsoft.AspNetCore.Mvc;
using Context;
using System.Data;
using Dapper;
using Application.Wrappers;

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
        public string Nombre { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class PersonaController(DapperContext context) : ControllerBase
    {

        [HttpGet()]
        public ActionResult ObtenerPersonaPorConsulta(string query)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    List<QueryPerson> people = connection.Query<QueryPerson>("OBTENER_PERSONA_POR_CONSULTA", new { query }, commandType: CommandType.StoredProcedure).ToList();
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

    }
}
