using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SCG.CAD.ETAX.API.Functions;
using SCG.CAD.ETAX.UTILITY;

namespace SCG.CAD.ETAX.API.Controllers
{
    public class AutoLoginController : BaseController
    {
        private readonly LogHelper log;
        public AutoLoginController()
        {
            log = new LogHelper();
        }

        [HttpPost, Route("VerifyTokenStepTwo")]
        public IActionResult VerifyTokenStepTwo(VerifyUserRequest request)
        {
            var res = new Response();
            try
            {
                VerifyUserResponse vuser = VerifyTokenInternalStepTwo(request);

                res.STATUS = true;
                res.OUTPUT_DATA = vuser;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.OUTPUT_DATA = ex.Message;
                return Ok(res);
            }
        }
        private VerifyUserResponse VerifyTokenInternalStepTwo(VerifyUserRequest request)
        {
            log.InsertLog(@"D:\log\login\", "jwtToken2");
            log.InsertLog(@"D:\log\login\", request.JwtToken);
            var ret = new VerifyUserResponse() { IsError = true };
            string errorMsg = "";
            var data = JwtAuthExternal.Authenticate(request.JwtToken, out errorMsg);
            if (string.IsNullOrEmpty(errorMsg))
            {
                //if (DateTime.Now > data.iat.AddMinutes(5))
                //{
                //    ret.Message = "AIT has expired.";
                //    return ret;
                //}
                
                ret.IsError = false;
                ret.UserId = data.userId;
                var jsonStr = JsonConvert.SerializeObject(ret);
                log.InsertLog(@"D:\log\login\", "return VerifyToken2");
                log.InsertLog(@"D:\log\login\", jsonStr);
                return ret;
            }
            else
            {
                ret.Message = errorMsg;
            }
            return ret;
        }
    }
}
