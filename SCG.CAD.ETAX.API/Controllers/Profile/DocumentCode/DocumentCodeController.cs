using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentCodeController : ControllerBase
    {
        private readonly IDocumentCodeRepository repo;

        public DocumentCodeController()
        {
            repo = new DocumentCodeRepository();
        }


    }
}
