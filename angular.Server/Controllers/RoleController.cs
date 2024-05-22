using Application.Wrappers;
using Context;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace angular.Server.Controllers
{
    public class RoleAccess
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
    public class RoleAccessDto
    {
        public string Path { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class RoleController(DapperContext ctx) : ControllerBase
    {

        [HttpGet("access")]
        public async Task<ActionResult> GetRoleAccess()
        {
            try
            {
                using (var connection = ctx.CreateConnection())
                {

                    var roleAccess = await connection.QueryAsync<RoleAccess>("OBTENER_ROLES_ACCESOS", commandType: CommandType.StoredProcedure);

                    var grouped = roleAccess.GroupBy(x => new { x.Id, x.Name }).Select(x => new
                    {
                        x.Key.Id,
                        x.Key.Name,
                        Paths = x.Select(g => g.Path).ToList()

                    });

                    return Ok(new Response<dynamic> { Message = "Listado Correctamente", Succeeded = true, Data = grouped.ToList() });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpPost("access/validate")]
        public async Task<ActionResult> ValidateAccess([FromBody] RoleAccessDto dto)
        {
            try
            {
                using (var connection = ctx.CreateConnection())
                {

                    int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                    var parameters = new DynamicParameters();
                    parameters.Add("@userId", userId);
                    parameters.Add("@path", dto.Path);
                    parameters.Add("@result", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    connection.Execute("VALIDAR_ACCESO",  parameters, commandType: CommandType.StoredProcedure);

                    bool result = parameters.Get<bool>("@result");

                    return Ok(new Response<dynamic> { Message = "Listado Correctamente", Succeeded = true, Data = result });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
