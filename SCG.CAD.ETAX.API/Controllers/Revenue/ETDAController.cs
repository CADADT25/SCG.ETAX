using Microsoft.AspNetCore.Mvc;
using SCG.CAD.ETAX.API.Repositories;

namespace SCG.CAD.ETAX.API.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class ETDAController : ControllerBase
    {
        private readonly IETDARepository repo;

        public ETDAController()
        {
            repo = new ETDARepository();     
        }


        [HttpGet]
        [Route("GetProviceFromETDA")]
        public IActionResult GetProviceFromETDA()
        {
            var result = repo.GetThaiISOCountrySubdivisionCode().Result;

            return Ok(result);
        }

        [HttpGet]
        [Route("GetDistrictFromETDA")]
        public IActionResult GetDistrictFromETDA(string ProviceCode)
        {
            var result = repo.GetTISICityName(ProviceCode).Result;

            return Ok(result);
        }

        [HttpGet]
        [Route("GetSubDistrictFromETDA")]
        public IActionResult GetSubDistrictFromETDA(string DistrictCode)
        {
            var result = repo.GetTISICitySubDivisionName(DistrictCode).Result;

            return Ok(result);
        }

    }
}
