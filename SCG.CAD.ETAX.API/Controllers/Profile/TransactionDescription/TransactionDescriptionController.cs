using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionDescriptionController : ControllerBase
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
        [Route("Delete")]
        public IActionResult Delete(TransactionDescription param)
        {
            var result = repo.DELETE(param).Result;

            return Ok(result);
        }
    }
}
