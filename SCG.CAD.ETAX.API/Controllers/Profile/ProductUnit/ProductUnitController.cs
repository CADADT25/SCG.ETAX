using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductUnitController : ControllerBase
    {
        private readonly IProductUnitRepository repo;

        public ProductUnitController()
        {
            repo = new ProductUnitRepository();
        }


        [HttpGet]
        [Route("GetListAll")]
        public ActionResult GetListAll()
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
        public ActionResult Insert(ProductUnit param)
        {
            var result = repo.INSERT(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(ProductUnit param)
        {
            var result = repo.UPDATE(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Delete")]
        public ActionResult Delete(ProductUnit param)
        {
            var result = repo.DELETE(param).Result;

            return Ok(result);
        }

    }
}
