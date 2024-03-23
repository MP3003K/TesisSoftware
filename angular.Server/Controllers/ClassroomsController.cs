﻿using Application.Features.Usuario.Queries;
using Application.Wrappers;
using Context;
using Controllers.Base;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Context;
using Entities;
using Microsoft.IdentityModel.Tokens;

namespace Controllers
{
    public class ClassroomsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public ClassroomsController(ApplicationDbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Lista de unidades
        /// </summary>
        [HttpGet("unidades/all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Unidad>> UnidadesAll()
        {
            return await _context.Unidades.FromSqlRaw("select * from unidades order by Año, NUnidad;").ToListAsync();
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
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC proc_crear_evalucion_psicologica_aula @v_aulaId = {aulaId}");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Obtener los estudiantes de una evaluacion psicologica de un aula
        /// </summary>
        [HttpGet("getEstudiantesDeEvalucionAula/{aulaId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEstudiantesDeEvalucionAula(int aulaId)
        {
            try
            {
                var grados = await _context.Grados.FromSqlInterpolated($"EXEC get_estudiantes_de_evalucion_aula @v_aulaId = {aulaId}").ToListAsync();
                return Ok(grados);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
