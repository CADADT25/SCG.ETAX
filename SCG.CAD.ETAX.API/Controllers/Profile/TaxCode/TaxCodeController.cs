namespace SCG.CAD.ETAX.API.Controllers
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
        [Route("GET_LIST")]
        public IActionResult GetTaxCodeAll()
        {
            var result = repo.GET_LIST().Result;

            return Ok(result);
        }

        [HttpGet]
        [Route("GET_DETAIL")]
        public IActionResult GetTaxCodeDetail(int taxCodeNo)
        {
            var result = repo.GET_DETAIL(taxCodeNo).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("INSERT")]
        public IActionResult InsertTaxCode(TaxCode param)
        {
            var result = repo.INSERT(param).Result;

            return Ok(result);
        }
    }
}
