namespace SCG.CAD.ETAX.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class CancelZipHeaderControllers : BaseController
    {
        private readonly ICancelZipHeaderRepository repo;

        public CancelZipHeaderControllers()
        {
            repo = new CancelZipHeaderRepository();
        }

        [HttpGet]
        [Route("GetListAll")]
        public IActionResult GetTaxCodeAll()
        {
            var result = repo.GET_LIST().Result;

            return Ok(result);
        }

        [HttpGet]
        [Route("GetDetail")]
        public IActionResult GetTaxCodeDetail(int id)
        {
            var result = repo.GET_DETAIL(id).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert(CancelZipHeader param)
        {
            var result = repo.INSERT(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(CancelZipHeader param)
        {
            var result = repo.UPDATE(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(CancelZipHeader param)
        {
            var result = repo.DELETE(param).Result;

            return Ok(result);
        }



    }
}
