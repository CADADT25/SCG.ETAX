using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestRepository repo;
        public RequestController()
        {
            repo = new RequestRepository();
        }

        [HttpGet]
        [Route("GetListAll")]
        public IActionResult GetListAll()
        {
            var result = repo.GET_LIST().Result;

            return Ok(result);
        }

        [HttpGet]
        [Route("GetRequest")]
        public IActionResult GetRequest(string requestNo)
        {
            var result = repo.GET_REQUEST(requestNo).Result;

            return Ok(result);
        }
        [HttpGet]
        [Route("GetRequestItemTransaction")]
        public IActionResult GetRequestItemTransaction(string requestNo)
        {
            var result = repo.GET_REQUEST_ITEM_TRANSACTION(requestNo).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert(Request param)
        {
            var result = repo.INSERT(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(Request param)
        {
            var result = repo.UPDATE(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(Request param)
        {
            var result = repo.DELETE(param).Result;

            return Ok(result);
        }


        [HttpPost]
        [Route("SubmitRequest")]
        public IActionResult SubmitRequest(RequestDataModel param)
        {
            var result = repo.SUBMIT_REQUEST(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Action")]
        public IActionResult Action(RequestActionDataModel param)
        {
            var result = repo.Action(param).Result;

            return Ok(result);
        }
    }
}
