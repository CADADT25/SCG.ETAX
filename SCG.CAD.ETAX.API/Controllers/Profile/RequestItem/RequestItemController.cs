using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{
    public class RequestItemController : BaseController
    {
        private readonly IRequestItemRepository repo;
        public RequestItemController()
        {
            repo = new RequestItemRepository();
        }

        [HttpGet]
        [Route("GetListAll")]
        public IActionResult GetListAll()
        {
            var result = repo.GET_LIST().Result;

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
        public IActionResult Insert(RequestItem param)
        {
            var result = repo.INSERT(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(RequestItem param)
        {
            var result = repo.UPDATE(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(RequestItem param)
        {
            var result = repo.DELETE(param).Result;

            return Ok(result);
        }
    }
}
