using Application.Features.Usuario.Queries;
using Application.Wrappers;
using Controllers.Base;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
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
