using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCG.CAD.ETAX.API.Repositories.Profile.CertificateMaster;

namespace SCG.CAD.ETAX.API.Controllers.Profile.CertificateMaster
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateMasterController : ControllerBase
    {
        private readonly ICertificateMasterRepository repo;

        public CertificateMasterController()
        {
            repo = new CertificateMasterRepository();
        }

        [HttpGet]
        [Route("GetListAll")]
        public ActionResult GetListAll()
        {
            var result = repo.GET_LIST().Result;

            return Ok(result);
        }
    }
}
