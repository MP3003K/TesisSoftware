using API.Controllers.Base;
using Application.Features.Persona.Commands;
using Application.Features.PreguntaPsicologica.Queries;
using Application.Features.RespuestaPsicologica.Commands;
using Application.Features.RespuestaPsicologica.Queries;
using Application.Features.Unidad.Queries;
using Application.Wrappers;
using Domain.Entities;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Context;

namespace webapi.Controllers
{
    public class PersonaController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public PersonaController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [HttpPost]
        [Route("AgregarEvaPisEstudiante")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<EvaluacionPsicologicaEstudiante>>> AddPersonaConEvaPsiEstudiante([FromBody] AgregarPersonaEvaPsiEstudianteRequest request)
        {
            return Created("", await Mediator.Send(request));
        }

    }
}
