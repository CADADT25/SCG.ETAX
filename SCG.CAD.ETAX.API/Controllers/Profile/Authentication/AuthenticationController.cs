using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository repo;

        public AuthenticationController()
        {
            repo = new AuthenticationRepository();
        }


        [HttpGet]
        [Route("GetDetail")]
        public IActionResult GetUserAccountLogin(string Username, string Password)
        {
            var result = repo.GET_DETAIL(Username, Password).Result;

            return Ok(result);
        }

        [HttpGet]
        [Route("GetMenu")]
        public IActionResult GetMenu(string Username)
        {
            var result = repo.GET_MENU(Username).Result;

            return Ok(result);
        }
    }
}
