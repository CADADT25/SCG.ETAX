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
        public IActionResult ProcessPDFSign(ConfigPdfSign configPdfSign, FilePDF filePDF)
        {
            var result = repo.ProcessPDFSign(configPdfSign, filePDF).Result;

            return Ok(result);
        }
    }
}
