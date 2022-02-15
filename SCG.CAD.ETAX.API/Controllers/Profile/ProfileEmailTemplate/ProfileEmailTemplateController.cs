using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileEmailTemplateController : ControllerBase
    {
        private readonly IProfileEmailTemplateRepository repo;

        public ProfileEmailTemplateController()
        {
            repo = new ProfileEmailTemplateRepository();
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
        public IActionResult Insert(ProfileEmailTemplate param)
        {
            var result = repo.INSERT(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(ProfileEmailTemplate param)
        {
            var result = repo.UPDATE(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(ProfileEmailTemplate param)
        {
            var result = repo.DELETE(param).Result;

            return Ok(result);
        }
    }
}
