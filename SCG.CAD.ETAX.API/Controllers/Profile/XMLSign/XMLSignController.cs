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
        [Route("ProcessXMLFileSign")]
        public IActionResult ProcessXMLFileSign(XMLSignModel xMLSignModel)
        {
            var result = repo.ProcessXMLFileSign(xMLSignModel).Result;

            return Ok(result);
        }
    }
}
