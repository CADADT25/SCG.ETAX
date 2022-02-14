using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileEmailTemplateController : ControllerBase
    {
        private readonly IProfileEmailTemplateRepository repo;

        public ProfileEmailTemplateController()
        {
            repo = new ProfileEmailTemplateRepository();
        }


    }
}
