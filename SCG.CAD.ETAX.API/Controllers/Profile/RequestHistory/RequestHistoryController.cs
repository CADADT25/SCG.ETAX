using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestHistoryController : ControllerBase
    {
        private readonly IRequestHistoryRepository repo;
        public RequestHistoryController()
        {
            repo = new RequestHistoryRepository();
        }

        [HttpGet]
        [Route("GetListAll")]
        public IActionResult GetListAll()
        {
            var result = repo.GET_LIST().Result;

            return Ok(result);
        }

        [HttpGet]
        [Route("GetList")]
        public IActionResult GetList(Guid requestId)
        {
            var result = repo.GET_LIST(requestId).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert(RequestHistory param)
        {
            var result = repo.INSERT(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(RequestHistory param)
        {
            var result = repo.UPDATE(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(RequestHistory param)
        {
            var result = repo.DELETE(param).Result;

            return Ok(result);
        }
    }
}
