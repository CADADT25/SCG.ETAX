namespace SCG.CAD.ETAX.API.Controllers
{
    public class XMLGenerateController : BaseController
    {
        private readonly IXMLGenerateRepository repo;
        public XMLGenerateController()
        {
            repo = new XMLGenerateRepository();
        }

        [HttpPost]
        [Route("ProcessXMLGenerate")]
        public IActionResult ProcessXMLGenerate(string parttextfile)
        {
            var result = repo.ProcessXMLGenerate(parttextfile).Result;

            return Ok(result);
        }
    }
}
