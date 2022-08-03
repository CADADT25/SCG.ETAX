using SCG.CAD.ETAX.API.Repositories.Profile.ConnectHSM;

namespace SCG.CAD.ETAX.API.Controllers.Profile.ConnectHSM
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectHSMController : ControllerBase
    {
        private readonly IConnectHSMRepository repo;

        public ConnectHSMController()
        {
            repo = new ConnectHSMRepository();
        }

        [HttpPut]
        [Route("GetKeyAlias")]
        public IActionResult GetKeyAlias(string jsonstring)
        {
            var result = repo.GetKeyAlias(jsonstring).Result;

            return Ok(result);
        }
        [HttpPut]
        [Route("GetHSMSerial")]
        public IActionResult GetHSMSerial(string jsonstring)
        {
            var result = repo.GetHSMSerial(jsonstring).Result;

            return Ok(result);
        }
    }
}
