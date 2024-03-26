using angular.Server.Configuration;
using Application.Wrappers;
using Context;
using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DapperContext context;
        private readonly JwtOptions options;
        private readonly JwtBearerOptions jwtOptions;
        public AuthController(DapperContext context, IOptions<JwtOptions> options, IOptions<JwtBearerOptions> jwtOptions)
        {
            this.context = context;
            this.options = options.Value;
            this.jwtOptions = jwtOptions.Value;
        }


        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDto Credentials)
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

                    return Ok(new { accessToken, user });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return Unauthorized();
            }
        }

        [HttpGet("Profile")]
        [Authorize]

        public async Task<ActionResult> Profile()
        {
            try
            {

                using (var connection = context.CreateConnection())
                {

                    var response = await connection.QueryAsync<User>("OBTENER_PERFIL", new { }, commandType: CommandType.StoredProcedure);
                    if (!response.Any()) { return Unauthorized(); }

                    var user = response.FirstOrDefault();

                    if (user == null) { return Unauthorized(); }

                    var accessToken = GenerateJwtToken(user.Email);

                    return Ok(new { accessToken, user });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Unauthorized(new Response<dynamic> { Data = null, Message = "Unauthorized", Succeeded = false });
            }
        }

        [Authorize]
        [HttpGet("Test")]
        public async Task<ActionResult> TestLogin()
        {
            Console.WriteLine(jwtOptions.TokenValidationParameters.IssuerSigningKey);
            return Ok("testes");
        }

        private string GenerateJwtToken(string username)
        {

            var claims = new Claim[]
            {
                new(JwtRegisteredClaimNames.Sub, username)
            };

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddHours(1), signingCredentials: signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }



}

