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
        public IActionResult ProcessXMLSign(ConfigXmlSign configXmlSign, FileXML fileXML)
        {
            var result = repo.ProcessXMLSign(configXmlSign, fileXML).Result;

            return Ok(result);
        }
    }
}
