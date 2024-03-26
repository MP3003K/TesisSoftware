using angular.Server.Configuration;
using Context;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Controllers
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }

        public string Status { get; set; }
    }
    public class LoginDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class AuthController:ControllerBase
    {
        private readonly DapperContext context;
        private readonly JwtOptions options;
       public AuthController(DapperContext context, IOptions<JwtOptions> options)
        {
            this.context = context;
            this.options = options.Value;
        }


        [HttpPost("Login")]
        public async Task<ActionResult> InformacionUsuario([FromBody] LoginDto Credentials)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {

                    var response = await connection.QueryAsync<User>("VERIFICAR_USUARIO", Credentials, commandType: CommandType.StoredProcedure);
                    if (!response.Any()) { return Unauthorized(); }

                    var user = response.FirstOrDefault();

                    if (user == null) { return Unauthorized(); }

                    var accessToken = GenerateJwtToken(user.Email);

                    return Ok(new { accessToken, user});
                }
            }
            catch (Exception e)
            {
                return Unauthorized();
            }
        }

        private string GenerateJwtToken(string username)
        {

            var claims = new Claim[]
            {
                new(JwtRegisteredClaimNames.Sub, username)
            };
            
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(options.Issuer, options.Audience, claims,null, DateTime.UtcNow.AddHours(1), signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }



}

