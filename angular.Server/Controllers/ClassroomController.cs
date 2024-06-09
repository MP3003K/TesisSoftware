using DTOs;
using Microsoft.AspNetCore.Mvc;
using Context;
using System.Data;
using Dapper;
using Application.Wrappers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Entities;
using Newtonsoft.Json;
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

    public class StudentsClassroom
    {
        public string ApellidoMaterno { get; set; }
        public string ApellidoPaterno { get; set; }
        public string DNI { get; set; }
        public string EstadoAula { get; set; }
        public string EstadoEstudiante { get; set; }
        public string EstudianteAulaId { get; set; }
        public int EstudianteId { get; set; }
        public int EvaluacionPsicologicaAulaId { get; set; }
        public int EvaluacionPsicologicaEstudianteId { get; set; }
        public string NombreCompleto { get; set; }
        public string Nombres { get; set; }

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

                    var students = await connection.QueryAsync<StudentsClassroom>("LISTAR_ESTUDIANTES_POR_AULA_Y_UNIDAD", new { v_aulaid = aulaId, v_unidadId = unidadId }, commandType: CommandType.StoredProcedure);
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
        public async Task<ActionResult> AsignarEstudianteAula([FromBody] StudentClassroomDto dto)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {

                    await connection.QueryAsync("ASIGNAR_ESTUDIANTE_AULA", dto, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = "Asignado Correctamente", Succeeded = true, Data = null });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("students")]
        public async Task<ActionResult> EliminarEstudianteAula([FromBody] StudentClassroomDto dto)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {

                    await connection.QueryAsync("ELIMINAR_ESTUDIANTE_AULA", dto, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = "Eliminado Correctamente", Succeeded = true, Data = null });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("PROC_CREAR_Y_ASIGNAR_ESTUDIANTES")]
        public async Task<ActionResult> CrearYAsignarEstudiantes([FromBody] crearEstudiantesDto dto)
        {
            try
            {
                using var connection = context.CreateConnection();
                // Define los parámetros, incluyendo los de salida
                var parameters = new DynamicParameters();
                parameters.Add("v_aulaId", dto.aulaId, DbType.Int32);
                parameters.Add("v_unidadId", dto.unidadId, DbType.Int32);
                parameters.Add("v_jsonEstudiantes", dto.jsonEstudiantes, DbType.String);
                parameters.Add("successful", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                parameters.Add("errorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000); // Ajusta el tamaño según sea necesario
                parameters.Add("dataEstudiantesRechazados", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000); // Ajusta el tamaño según sea necesario

                // Ejecuta el procedimiento almacenado
                await connection.ExecuteAsync("PROC_CREAR_Y_ASIGNAR_ESTUDIANTES", parameters, commandType: CommandType.StoredProcedure);

                // Lee los valores de salida
                bool succeeded = parameters.Get<bool>("successful");
                string errorMessage = parameters.Get<string>("errorMessage");
                string dataEstudiantesRechazados = parameters.Get<string>("dataEstudiantesRechazados");

                // Ahora puedes manejar los resultados como necesites
                if (!succeeded)
                {
                    // Maneja el caso de error
                    return BadRequest(new Response<dynamic> { Message = errorMessage, Succeeded = false, Data = dataEstudiantesRechazados });
                }
                else
                {
                    // Proceso exitoso
                    return Ok(new Response<dynamic> { Message = "Operación exitosa.", Succeeded = true, Data = null });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<dynamic> { Message = ex.Message, Succeeded = false, Data = null });
            }
        }


        [HttpPost("VALIDAR_DNI_UNICO")]
        public async Task<ActionResult> ValidarDniUnico([FromBody] ValidarDniDto dto)
        {
            try
            {
                using var connection = context.CreateConnection();
                // Define los parámetros, incluyendo los de salida
                var parameters = new DynamicParameters();
                parameters.Add("jsonDnis", dto.jsonDnis, DbType.String);
                parameters.Add("resultado", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                parameters.Add("jsonDnisRechazados", dbType: DbType.String, direction: ParameterDirection.Output, size: -1); // -1 para NVARCHAR(MAX)

                // Ejecuta el procedimiento almacenado
                await connection.ExecuteAsync("VALIDAR_DNI_UNICO", parameters, commandType: CommandType.StoredProcedure);

                // Lee los valores de salida
                bool resultado = parameters.Get<bool>("resultado");
                string jsonDnisRechazados = parameters.Get<string>("jsonDnisRechazados");

                // Maneja los resultados
                if (!resultado)
                {
                    // Caso de DNI no únicos
                    return BadRequest(new Response<dynamic> { Message = "Existen DNI no únicos.", Succeeded = false, Data = jsonDnisRechazados });
                }
                else
                {
                    // Todos los DNI son únicos
                    return Ok(new Response<dynamic> { Message = "Todos los DNI son únicos.", Succeeded = true, Data = null });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<dynamic> { Message = ex.Message, Succeeded = false, Data = null });
            }
        }

        public class ValidarDniDto
        {
            public string jsonDnis { get; set; } = string.Empty;
        }
        public class DniDto
        {
            public int Dni { get; set; }
        }
        public class StudentClassroomDto
        {
            public int ClassroomId { get; set; }
            public int StudentId { get; set; }
            public int UnityId { get; set; }
        }

        public class ActualizarEstudianteDto
        {
            public int Id { get; set; }
            public string Nombres { get; set; } = string.Empty;
            public string ApellidoPaterno { get; set; } = string.Empty;
            public string ApellidoMaterno { get; set; } = string.Empty;
            public int DNI { get; set; }
        }


        public class crearEstudiantesDto
        {
            public int unidadId { get; set; }
            public int aulaId { get; set; }
            public string jsonEstudiantes { get; set; } = string.Empty;
        }
    }


}
