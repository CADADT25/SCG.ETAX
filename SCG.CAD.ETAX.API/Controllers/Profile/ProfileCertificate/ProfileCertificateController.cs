using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileCertificateController : ControllerBase
    {
        private readonly IProfileCertificateRepository repo;

        public ProfileCertificateController()
        {
            repo = new ProfileCertificateRepository();
        }



    }
}
