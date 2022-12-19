using SCG.CAD.ETAX.MODEL.CustomModel;

namespace SCG.CAD.ETAX.API.Controllers
{
    public class XMLSignController : BaseController
    {
        private readonly IXMLSignRepository repo;
        public XMLSignController()
        {
            repo = new XMLSignRepository();
        }

        [HttpPost]
        [Route("ProcessXMLSign")]
        public IActionResult ProcessXMLSign(ConfigXmlSign configXmlSign, FileXML filePDF)
        {
            var result = repo.ProcessXMLSign(configXmlSign, filePDF).Result;

            return Ok(result);
        }
    }
}
