using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{
    public class RequestPathController : BaseController
    {
        private readonly IRequestPathRepository repo;
        public RequestPathController()
        {
            repo = new RequestPathRepository();
        }

        [HttpGet]
        [Route("GetList")]
        public IActionResult GetList(Guid id)
        {
            var result = repo.GET_LIST(id).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("GetListByStatus")]
        public IActionResult GetListByStatus(List<string> param)
        {
            var result = repo.GET_LIST_BY_STATUS(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert(RequestPath param)
        {
            var result = repo.INSERT(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(RequestPath param)
        {
            var result = repo.UPDATE(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(RequestPath param)
        {
            var result = repo.DELETE(param).Result;

            return Ok(result);
        }
    }
}
