using Application.Features.Persona.Commands;
using Application.Wrappers;
using Controllers.Base;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Context;

namespace Controllers
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
