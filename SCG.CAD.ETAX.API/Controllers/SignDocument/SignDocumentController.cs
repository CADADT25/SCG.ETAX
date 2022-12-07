using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace SCG.CAD.ETAX.API.Controllers
{
    public class SignDocumentController : BaseController
    {
        private readonly ISignDocumentRepository repo;
        public SignDocumentController()
        {
            repo = new SignDocumentRepository();
        }
        [HttpGet]
        [Route("Sign")]
        public IActionResult Sign(SignDocumentRequest req)
        {
            var result = repo.Sign(req).Result;

            return Ok(result);
        }
    }
}
