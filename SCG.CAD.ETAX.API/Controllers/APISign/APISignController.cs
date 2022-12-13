using Microsoft.AspNetCore.Mvc;
using SCG.CAD.ETAX.MODEL.CustomModel;

namespace SCG.CAD.ETAX.API.Controllers.APISign
{
    public class APISignController : BaseController
    {
        private readonly IAPISignRepository repo;

        public APISignController()
        {
            repo = new APISignRepository();
        }

        [HttpPost]
        [Route("SendXMLSign")]
        public IActionResult SendXMLSign(APISendFileXMLSignModel data)
        {
            var result = repo.SendXMLSign(data).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("SendPDFSign")]
        public IActionResult SendPDFSign(APISendFilePDFSignModel data)
        {
            var result = repo.SendPDFSign(data).Result;

            return Ok(result);
        }
    }
}
