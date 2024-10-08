﻿using Application.Wrappers;
using Context;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace Controllers
{
    public class Classroom
    {
        public int Id { get; set; }
        public string? Grado { get; set; }
        public string? Seccion { get; set; }
    }
    public class ClassroomEvaluation
    {
        public int AulaId { get; set; }
        public string? Estado { get; set; }
        public int EvaluacionPsicologicaId { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaInicio { get; set; }
        public int Id { get; set; }
        public int UnidadId { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class AulaController(DapperContext ctx) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<ActionResult> ListaDeAulas()
        {
            try
            {
                using (var connection = ctx.CreateConnection())
                {

                    var classrooms = await connection.QueryAsync<Classroom>("LISTAR_AULAS", commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = "Listado Correctamente", Succeeded = true, Data = classrooms.ToList() });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lista de Aulas registradas en EvalucionPsicologicaEstudiante
        /// </summary>

        [HttpGet("aulasEstudiante/{estudianteId:int}")]
        public async Task<ActionResult> ListaAulasEstudiantes(int studentId)
        {
            try
            {
                using (var connection = ctx.CreateConnection())
                {

                    var classrooms = await connection.QueryAsync("LISTAR_AULAS_POR_ESTUDIANTE", new { studentId }, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = "Listado Correctamente", Succeeded = true, Data = classrooms.ToList() });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Evaluacion")]
        public async Task<ActionResult> ObtenerEvaluacionAula(int unityId, int classroomId)
        {
            try
            {
                using (var connection = ctx.CreateConnection())
                {
                    var classrooms = await connection.QueryAsync<ClassroomEvaluation>("OBTENER_EVALUACION_AULA", new { unityId, classroomId }, commandType: CommandType.StoredProcedure);
                    return Ok(new Response<dynamic> { Message = "Listado Correctamente", Succeeded = true, Data = classrooms.FirstOrDefault() });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("examenEstudiante")]
        public async Task<ActionResult> PROC_OBTENER_EXAMENES_ESTUDIANTE()
        {
            try
            {
                using (var connection = ctx.CreateConnection())
                {
                    var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (string.IsNullOrEmpty(userIdClaim))
                    {
                        return BadRequest("User identifier claim is missing.");
                    }
        
                    int userId;
                    if (!int.TryParse(userIdClaim, out userId))
                    {
                        return BadRequest("Invalid user identifier.");
                    }
        
                    var examenesEstudiante = await connection.QueryAsync(
                        "PROC_OBTENER_EXAMENES_ESTUDIANTE", 
                        new { v_userId = userId }, 
                        commandType: CommandType.StoredProcedure
                    );

                    return Ok(new Response<dynamic> 
                    { 
                        Message = "Listado Correctamente", 
                        Succeeded = true, 
                        Data = examenesEstudiante
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
