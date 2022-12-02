using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers.APISign
{
    public class APISignController : BaseController
    {
        private readonly IAPISignRepository repo;

        public APISignController()
        {
            repo = new APISignRepository();
        }

        [HttpGet]
        [Route("SendXMLSign")]
        public IActionResult SendXMLSign(string json)
        {
            var result = repo.SendXMLSign(json).Result;

            return Ok(result);
        }

        [HttpGet]
        [Route("PDFSign")]
        public IActionResult SendPDFSign(string json)
        {
            var result = repo.SendPDFSign(json).Result;

            return Ok(result);
        }
    }
}
