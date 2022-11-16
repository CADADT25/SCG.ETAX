using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class OutputSearchPrintingController : BaseController
    {

        private readonly IOutputSearchPrintingRepository repo;

        public OutputSearchPrintingController()
        {
            repo = new OutputSearchPrintingRepository();
        }


        [HttpGet]
        [Route("GetListAll")]
        public IActionResult GetListAll()
        {
            var result = repo.GET_LIST().Result;

            return Ok(result);
        }

        [HttpGet]
        [Route("GetDetail")]
        public IActionResult GetDetail(int id)
        {
            var result = repo.GET_DETAIL(id).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert(OutputSearchPrinting param)
        {
            var result = repo.INSERT(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(OutputSearchPrinting param)
        {
            var result = repo.UPDATE(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(OutputSearchPrinting param)
        {
            var result = repo.DELETE(param).Result;

            return Ok(result);
        }


        [HttpGet]
        [Route("Search")]
        public IActionResult Search(string JsonString)
        {
            var result = repo.SEARCH(JsonString).Result;

            return Ok(result);
        }
        [HttpPost]
        [Route("DownloadZipFile")]
        public IActionResult DownloadZipFile(OutputSearchPrinting param)
        {
            var result = repo.DOWNLOADZIPFILE(param).Result;

            return Ok(result);
        }
    }
}
