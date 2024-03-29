using Context;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class Access
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
        public string Type { get; set; }
        public List<Access>? Children { get; set; }
    }

    public class Navigation(List<Access> access)
    {
        public List<Access> Compact { get; set; } = access;
        public List<Access> Default { get; set; } = access;
        public List<Access> Futuristic { get; set; } = access;
        public List<Access> Horizontal { get; set; } = access;
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

                    var accessToken = GetAccessToken(user.Id);

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


            try
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                using (var connection = context.CreateConnection())
                {

                    var response = await connection.QueryAsync<User>("OBTENER_USUARIO", new { userId }, commandType: CommandType.StoredProcedure);
                    if (!response.Any()) { return Unauthorized(); }

                    var user = response.FirstOrDefault();

                    if (user == null) { return Unauthorized(); }

                    var accessToken = GetAccessToken(user.Id);

                    return Ok(new { accessToken, user });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return BadRequest();
            }

        }

        [HttpGet("Navigation")]
        public async Task<ActionResult> GetNavigation()
        {
            try
            {
                using (var connection = context.CreateConnection())
                {

                    var accesses = await connection.QueryAsync<Access>("OBTENER_ACCESOS", new { UserId = 5 }, commandType: CommandType.StoredProcedure);
                    return Ok(new Navigation(accesses.ToList()));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return BadRequest();
            }

        }
        private string GetAccessToken(int Id)
        {
            var claims = new Claim[]
                        {
                        new(JwtRegisteredClaimNames.Sub, Id.ToString()),
                        };

            var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Audience"], claims, null, DateTime.UtcNow.Add(TimeSpan.FromHours(1)), new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]!)), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }





}

