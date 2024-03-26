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
    [ApiController]
    [Route("[controller]")]
    public class PersonaController(DapperContext context) : ControllerBase
    {
        [HttpPost("AgregarEvaPisEstudiante")]
        public async Task<ActionResult> AddPersonaConEvaPsiEstudiante([FromBody] CreatePerson person)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {

                    var classrooms = await connection.QueryAsync("CREAR_ESTUDIANTE", new { firstName = person.Nombres, firstLastName = person.ApellidoPaterno, secondLastName = person.ApellidoMaterno, documentNumber = person.DNI, classroomId = person.AulaId },  commandType: CommandType.StoredProcedure);
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
