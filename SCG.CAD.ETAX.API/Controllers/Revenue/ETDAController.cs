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
        [Route("ThaiISOCountrySubdivisionCode")]
        public IActionResult GetThaiISOCountrySubdivisionCode()
        {
            var result = repo.GetThaiISOCountrySubdivisionCode().Result;

            return Ok(result);
        }

    }
}
