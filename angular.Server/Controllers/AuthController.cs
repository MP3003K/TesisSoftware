using Application.Wrappers;
using Context;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Controllers
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Avatar { get; set; }
        public string? Status { get; set; }
        public string? Role { get; set; }
        public string? Redirect { get; set; }
    }

    public class Access
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Link { get; set; }
        public string? Icon { get; set; }
        public string? Type { get; set; }
        public List<Access>? Children { get; set; }
    }

    public class Navigation
    {
        public List<Access> Compact { get; set; }
        public List<Access> Default { get; set; }
        public List<Access> Futuristic { get; set; }
        public List<Access> Horizontal { get; set; }

        public Navigation(List<Access> access)
        {
            Compact = access;
            Default = access;
            Futuristic = access;
            Horizontal = access;
        }
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

        public class UserData
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Email { get; set; }
            public string? Role { get; set; }
            public string? Redirect { get; set; }
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDto Credentials)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    var response = await connection.QueryAsync<dynamic>("VERIFICAR_USUARIO_V2", Credentials, commandType: CommandType.StoredProcedure);

                    if (response == null || !response.GetEnumerator().MoveNext())
                    {
                        return Unauthorized(new ApiResponse<dynamic>
                        {
                            Succeeded = false,
                            Error = "No se recibieron respuestas."
                        });
                    }
                    else
                    {
                        Console.WriteLine("Contenido de response:");
                        foreach (var item in response)
                        {
                            Console.WriteLine(item);
                        }

                        var enumerator = response.GetEnumerator();
                        enumerator.MoveNext();
                        var firstResponse = enumerator.Current;

                        Console.WriteLine("First response:");
                        Console.WriteLine(firstResponse);

                        // Deserializar el JSON usando ApiResponse<List<UserData>>
                        var firstResponseObject = JsonConvert.DeserializeObject<ApiResponse<List<UserData>>>(firstResponse.Response);

                        if (firstResponseObject != null && firstResponseObject.Succeeded && firstResponseObject.Data != null && firstResponseObject.Data.Count > 0)
                        {
                            var usuario = firstResponseObject.Data[0];
                            if (usuario == null) throw new Exception("Usuario no encontrado");

                            var accessToken = GetAccessToken(usuario.Id);

                            return Ok(new ApiResponse<dynamic>
                            {
                                Data = new { accessToken, usuario },
                                Succeeded = true,
                                Message = "Usuario autenticado correctamente"
                            });
                        }

                        return Unauthorized(new ApiResponse<dynamic>
                        {
                            Succeeded = false,
                            Error = firstResponseObject?.Error ?? "Error no encontrado",
                            ErrorLine = firstResponseObject?.ErrorLine,
                            ErrorNumber = firstResponseObject?.ErrorNumber
                        });
                    }
                }
            }
            catch (SqlException ex)
            {
                return BadRequest(new ApiResponse<dynamic>
                {
                    Succeeded = false,
                    Message = "Error en la base de datos",
                    Error = ex.Message,
                    ErrorNumber = ex.Number
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<dynamic>
                {
                    Succeeded = false,
                    Message = ex.Message == "Usuario no encontrado" ? ex.Message : "Error en el servidor",
                    Error = ex.Message
                });
            }
        }

        [Authorize]
        [HttpGet("Navigation")]
        public async Task<ActionResult> GetNavigation()
        {
            string nameIdentifier = ClaimTypes.NameIdentifier;
            if (string.IsNullOrEmpty(nameIdentifier))
            {
                return BadRequest(new { message = "El nameIdentifier del usuario no se encontró.", userId = nameIdentifier ?? "null" });
            }

            nameIdentifier = User.FindFirstValue(nameIdentifier);

            if (string.IsNullOrEmpty(nameIdentifier))
            {
                return BadRequest(new { message = "El userId no se encontró.", userId = nameIdentifier ?? "null" });
            }
            try
            {
                int userId = int.Parse(nameIdentifier);

                using (var connection = context.CreateConnection())
                {
                    var accesses = await connection.QueryAsync<Access>("OBTENER_ACCESOS", new { UserId = userId }, commandType: CommandType.StoredProcedure);
                    return Ok(new Navigation(accesses.ToList()));
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message, userId = nameIdentifier });
            }
        }

        [Authorize]
        [HttpPost("SignOut")]
        public ActionResult Logout()
        {
            return Ok(new Response<dynamic> { Data = true, Succeeded = true, Message = "Sesión cerrada correctamente" });
        }

        private string GetAccessToken(int Id)
        {
            var claims = new Claim[]
            {
                new(ClaimTypes.NameIdentifier, Id.ToString()),
            };

            var token = new JwtSecurityToken(
                config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                null,
                DateTime.UtcNow.Add(TimeSpan.FromHours(6)),
                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]!)), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}