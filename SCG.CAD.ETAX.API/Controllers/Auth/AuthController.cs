using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SCG.CAD.ETAX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IConfigApplicationRepository repo;

        public AuthController()
        {
            repo = new ConfigApplicationRepository();
        }
        [HttpGet, Route("GetToken")]
        public IActionResult GetToken()
        {
            try
            {
                if (!Request.Headers.ContainsKey("Authorization")) return Unauthorized();
                var appSecretKey = Request.Headers["Authorization"].ToString();

                if (string.IsNullOrEmpty(appSecretKey))
                    return Unauthorized();
                var res = repo.CHECK_KEY(appSecretKey).Result;
                if (res.STATUS || appSecretKey == new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("Jwt")["AppKey"])
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("Jwt")["Key"]));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var jwtSecurityToken = new JwtSecurityToken(
                        new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("Jwt")["Issuer"],
                        new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("Jwt")["Issuer"],
                        null,
                        expires: DateTime.Now.AddMinutes(2),
                        signingCredentials: signinCredentials
                    );
                    var ret = new AuthModel() { Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken) };
                    return Ok(ret);
                }
            }
            catch
            {
                return BadRequest
                ("An error occurred in generating the token");
            }
            return Unauthorized();
        }
    }
}
