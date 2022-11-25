using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.IdentityModel.Tokens;
using SCG.CAD.ETAX.API.Functions;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY;
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
        private readonly IProfileUserManagementRepository repoUser;
        private readonly IConfigApplicationRepository repoConfig;
        private readonly LogHelper log;

        public AuthController()
        {
            repoConfig = new ConfigApplicationRepository();
            repoUser = new ProfileUserManagementRepository();
            log = new LogHelper();
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
                var res = repoConfig.CHECK_KEY(appSecretKey).Result;
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

        [HttpPost, Route("VerifyToken")]
        public IActionResult VerifyToken([FromBody] string jwtToken)
        {
            //log.InsertLog(@"D:\log\login\", "jwtToken1");
            //log.InsertLog(@"D:\log\login\", jwtToken);
            if (!Request.Headers.ContainsKey("bearer")) return Unauthorized();
            var appSecretKey = Request.Headers["bearer"].ToString();

            if (string.IsNullOrEmpty(appSecretKey))
                return Unauthorized();
            var res = repoConfig.CHECK_KEY(appSecretKey).Result;
            if (res.STATUS || appSecretKey == new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("Jwt")["AppKey"])
            {
                var ret = VerifyTokenInternal(jwtToken);
                var jsonStr = JsonConvert.SerializeObject(ret);
                //log.InsertLog(@"D:\log\login\", "return VerifyToken");
                //log.InsertLog(@"D:\log\login\", jsonStr);
                return Ok(ret);
            }

            //log.InsertLog(@"D:\log\login\", "return Unauthorized");
            return Unauthorized();
        }
        private VerifyUserResponse VerifyTokenInternal(string jwtToken)
        {
            var ret = new VerifyUserResponse() { IsError = true };
            string errorMsg = "";
            var data = JwtAuthExternal.Authenticate(jwtToken, out errorMsg);
            if (string.IsNullOrEmpty(errorMsg))
            {
                //log.InsertLog(@"D:\log\login\", "ExternalId");
                //log.InsertLog(@"D:\log\login\", data.ExternalId+":"+ data.ExternalId2 + ":"+ data.Email + ":");
                //if (DateTime.Now > data.iat.AddMinutes(5))
                //{
                //    ret.Message = "AIT has expired.";
                //    //log.InsertLog(@"D:\log\login\", ret.Message);
                //    return ret;
                //}
                if (string.IsNullOrEmpty(data.ExternalId))
                {
                    //ret.Message = "The ExternalId was not found in the jwt token.";
                    ret.Message = "Invalid User and Password.";
                    return ret;
                }
                ProfileUserManagement externalId = null;
                var resExternal = repoUser.GET_DETAIL_BY_EXTERNALID(data.ExternalId).Result;
                if (resExternal.STATUS)
                {
                    var strJson = JsonConvert.SerializeObject(resExternal.OUTPUT_DATA);
                    externalId = JsonConvert.DeserializeObject<ProfileUserManagement>(strJson);
                }
                if (externalId == null)
                {
                    if (string.IsNullOrEmpty(data.Email))
                    {
                        //ret.Message = "The Email was not found in the jwt token.";
                        ret.Message = "Invalid User and Password.";
                        return ret;
                    }
                    var resEmail = repoUser.GET_DETAIL_BY_EMAIL_EXTERNALID2(data.Email).Result;
                    if (resEmail.STATUS)
                    {
                        var strJson = JsonConvert.SerializeObject(resEmail.OUTPUT_DATA);
                        externalId = JsonConvert.DeserializeObject<ProfileUserManagement>(strJson);
                    }
                    if (externalId == null)
                    {
                        //ret.Message = "The ExternalId and Email were not found in the Etax system.";
                        ret.Message = "Invalid User and Password.";
                        return ret;
                    }
                    else
                    {
                        externalId.ExternalId = data.ExternalId;
                        externalId.ExternalId2 = data.Email;
                        var updateUser = repoUser.UPDATE_EXTERNALID(externalId).Result;
                        ret.UserId = externalId.UserEmail;
                        ret.IsError = false;
                        return ret;
                    }
                }
                else
                {
                    ret.UserId = externalId.UserEmail;
                    ret.IsError = false;
                    return ret;
                }
            }
            else
            {
                ret.Message = errorMsg;
            }
            return ret;
        }

        

    }
}
