
namespace SCG.CAD.ETAX.API.Controllers.Profile.TaxCode
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCodeController : ControllerBase
    {
        private readonly ITaxCodeRepository repo;

        public TaxCodeController()
        {
            repo = new TaxCodeRepository();
        }

        [HttpGet]
        [Route("GetTaxCode")]
        public IActionResult GetTaxCodeAll()
        {
            var result = repo.GetTaxCodeAll().Result;

            return Ok(result);
        }

        [HttpGet]
        [Route("GetTaxCodeDetail")]
        public IActionResult GetTaxCodeDetail(int taxCodeNo)
        {
            var result = repo.GetTaxCodeDetail(taxCodeNo).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("InsertTaxCode")]
        public IActionResult InsertTaxCode(MODEL.etaxModel.TaxCode param)
        {
            var result = repo.InsertTaxCode(param).Result;

            return Ok(result);
        }
    }
}
