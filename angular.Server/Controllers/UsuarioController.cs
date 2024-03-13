using API.Controllers.Base;
using Application.Features.RespuestaPsicologica.Queries;
using Application.Features.Usuario.Queries;
using Application.Wrappers;
using DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class UsuarioController: BaseController
    {
        /// <summary>
        /// Resultados psicologicos obtenido por un Aula en una Unidad
        /// </summary>
        [HttpGet("{userName}/{password}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<UsuarioDto>>> InformacionUsuario(string userName, string password)
        {
            return Ok(await Mediator.Send(new ValidarUsuarioQuery()
            {
                UserName = userName,
                Password = password
            }));
        }
    }
}
