using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class TransactionDescriptionController : BaseController
    {
        private readonly ITransactionDescriptionRepository repo;
        public TransactionDescriptionController()
        {
            repo = new TransactionDescriptionRepository();
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

        [HttpGet]
        [Route("GetBilling")]
        public IActionResult GetBilling(string billingNo)
        {
            var result = repo.GET_BILLING(billingNo).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert(TransactionDescription param)
        {
            var result = repo.INSERT(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(TransactionDescription param)
        {
            var result = repo.UPDATE(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("UpdateList")]
        public IActionResult UpdateList(List<TransactionDescription> param)
        {
            var result = repo.UPDATE_LIST(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(TransactionDescription param)
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

        [HttpGet]
        [Route("SyncStatusPDFSign")]
        public IActionResult SyncStatusPDFSign(string listbillno, string updateby)
        {
            var result = repo.SYNCSTATUSPDFSIGN(listbillno, updateby).Result;

            return Ok(result);
        }
        [HttpGet]
        [Route("SyncStatusXMLSign")]
        public IActionResult SyncStatusXMLSign(string listbillno, string updateby)
        {
            var result = repo.SYNCSTATUSXMLSIGN(listbillno, updateby).Result;

            return Ok(result);
        }
        [HttpGet]
        [Route("DownloadFile")]
        public IActionResult DownloadFile(string pathfile)
        {
            var result = repo.DOWNLOADFILE(pathfile).Result;

            return Ok(result);
        }
    }
}
