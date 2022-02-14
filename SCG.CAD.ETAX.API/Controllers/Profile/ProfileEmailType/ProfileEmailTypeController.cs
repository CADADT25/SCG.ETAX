using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileEmailTypeController : ControllerBase
    {

        private readonly IProfileEmailTypeRepository repo;
        public ProfileEmailTypeController()
        {
            repo = new ProfileEmailTypeRepository();
        }



    }
}
