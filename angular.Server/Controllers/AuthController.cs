using Application.Wrappers;
using Context;
using Controllers.Base;
using Dapper;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Controllers
{
    public class LoginDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
    public class AuthController(DapperContext context): BaseController
    {

        [HttpPost("Login")]
        public async Task<ActionResult> InformacionUsuario([FromBody] LoginDto Credentials)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {

                    await connection.QueryAsync("VERIFICAR_USUARIO", Credentials, commandType: CommandType.StoredProcedure);
                    return Ok();
                }
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
    }
}
