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
        private readonly IConfiguration config;

        public AuthController(DapperContext context, IConfiguration configuration)
        {
            this.context = context;
            this.config = configuration;
          
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

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]!);
                    var claims = new List<Claim>
                    {

                    };

                    var tokenDescriptor = new SecurityTokenDescriptor()
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.UtcNow.Add(TimeSpan.FromHours(1)),
                        Issuer = config["Jwt:Issuer"],
                        Audience = config["Jwt:Audience"],
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var accessToken = tokenHandler.WriteToken(token);

                    return Ok(new { accessToken, user });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return Unauthorized();
            }
        }

        [Authorize]
        [HttpGet("Profile")]
        public async Task<ActionResult> GetProfile()
        {
            var profile = "profile";
            return Ok(new
            {
                profile
            });
        }

    }



}

