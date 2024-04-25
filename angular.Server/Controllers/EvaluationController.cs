using Application.Wrappers;
using Context;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace angular.Server.Controllers
{

    public class StudentEvaluation {
        public int Id { get; set; }
        public int EvaluacionAulaId { get; set; }
        public int EstudianteId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Estado { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class EvaluationController(DapperContext context) : ControllerBase
    {
        [HttpGet("{classroomEvaluationId:int}/students/{studentId:int}")]
        public async Task<ActionResult> GetStudentEvaluation(int classroomEvaluationId, int studentId)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {

                    var students = await connection.QueryAsync<StudentEvaluation>("OBTENER_EVALUACION_ESTUDIANTE", new
                    {
                        classroomEvaluationId,
                        studentId
                    }, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = null, Succeeded = true, Data = students.FirstOrDefault() });

                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


    }
}
