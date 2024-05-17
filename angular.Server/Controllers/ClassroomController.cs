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

    public class Unity
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int NUnidad { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Año { get; set; }
        public int EscuelaId { get; set; }
        public bool Estado { get; set; }
    }


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

                    var classrooms = await connection.QueryAsync<Unity>("LISTAR_UNIDADES", commandType: CommandType.StoredProcedure);
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
        [HttpGet("getEstudiantesByAulaYUnidad/{unidadId:int}/{aulaId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEstudiantesDeEvalucionAula(int unidadId, int aulaId)
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

        /// <summary>
        /// Obtener los estudiantes de una Aula en una Unidad
        /// </summary>
        [HttpGet("getRespuestasEstudianteAula/{evaPsiAulaId:int}/{dimension:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> RespuestasEstudianteAula(int evaPsiAulaId, int dimension)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {

                    var students = await connection.QueryAsync("LISTAR_RESPUESTAS_ESTUDIANTES_AULA2", new { evaluationId = evaPsiAulaId, tipoDimension = dimension }, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = null, Succeeded = true, Data = students.ToList() });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("crearNUnidadOAnio/{crear_anio:int}")]
        public async Task<ActionResult> PROC_CREAR_UNIDAD(int crear_anio)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {

                    var classrooms = await connection.QueryAsync("PROC_CREAR_UNIDAD", new { v_crear_anio = crear_anio }, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = null, Succeeded = true, Data = classrooms.ToList() });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut("ActualizarEstudiante")]
        public async Task<ActionResult> PROC_EDITAR_ESTUDIANTE([FromBody] ActualizarEstudianteDto person)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {

                    await connection.QueryAsync("PROC_EDITAR_ESTUDIANTE", new
                    {
                        v_idEstudiante = person.Id,
                        v_nombres = person.Nombres,
                        v_apellidoPaterno = person.ApellidoPaterno,
                        v_apellidoMaterno = person.ApellidoMaterno,
                        v_DNI = person.DNI
                    }, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = "Creado Correctamente", Succeeded = true, Data = person });
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPost("updateEstadoEvaPsiAula/{unidadId:int}/{aulaId:int}/{iniciarEvalucion:int}")]
        public async Task<ActionResult> PROC_CAMBIAR_ESTADO_EVALUCION_PSICOLOGICA(int unidadId, int aulaId, int iniciarEvalucion)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {

                    var classrooms = await connection.QueryAsync("PROC_CAMBIAR_ESTADO_EVALUCION_PSICOLOGICA", new
                    {
                        v_unidadId = unidadId,
                        v_aulaId = aulaId,
                        v_iniciarEvalucion = iniciarEvalucion,
                    }, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = null, Succeeded = true, Data = classrooms.ToList() });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("students")]
        public async Task<ActionResult> AsignarEstudianteAula([FromBody] StudentClassroomDto studentClassroomDto)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {

                    await connection.QueryAsync("proc_asignar_estudiante_a_aula", studentClassroomDto, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = "Asignado Correctamente", Succeeded = true, Data = null });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class StudentClassroomDto
        {
            public int V_aulaId { get; set; }
            public int V_estudianteId { get; set; }
            public int V_unidadId { get; set; }
            public int V_añadir { get; set; }
        }

        public class ActualizarEstudianteDto
        {
            public int Id { get; set; }
            public string Nombres { get; set; } = string.Empty;
            public string ApellidoPaterno { get; set; } = string.Empty;
            public string ApellidoMaterno { get; set; } = string.Empty;
            public int DNI { get; set; }
        }
    }


}
