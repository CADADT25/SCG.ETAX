using SCG.CAD.ETAX.MODEL.CustomModel;

namespace SCG.CAD.ETAX.API.Controllers.Profile.PDFSign
{
    public class PDFSignController : BaseController
    {
        private readonly IPDFSignRepository repo;
        public PDFSignController()
        {
            repo = new PDFSignRepository();
        }

        [HttpPost]
        [Route("ProcessPDFSign")]
        public IActionResult ProcessPDFSign(PDFSignModel pDFSignModel)
        {
            var result = repo.ProcessPDFSign(pDFSignModel).Result;

            return Ok(result);
        }
    }
}
