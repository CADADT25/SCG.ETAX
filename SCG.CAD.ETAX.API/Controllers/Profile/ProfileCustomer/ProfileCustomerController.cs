using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileCustomerController : ControllerBase
    {
        private readonly IProfileCustomerRepository repo;

        public ProfileCustomerController()
        {
            repo = new ProfileCustomerRepository();
        }



    }
}
