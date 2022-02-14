using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileCompanyController : ControllerBase
    {
        private readonly IProfileCompanyRepository repo;

        public ProfileCompanyController()
        {
            repo = new ProfileCompanyRepository();
        }


    }
}
