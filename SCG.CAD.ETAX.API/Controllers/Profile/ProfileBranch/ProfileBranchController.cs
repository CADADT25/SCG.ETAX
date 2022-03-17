using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProfileBranchController : ControllerBase
    {
        private readonly IProfileBranchRepository repo;

        public ProfileBranchController()
        {
            repo = new ProfileBranchRepository();
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
        public ActionResult Insert(ProfileBranch param)
        {
            var result = repo.INSERT(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(ProfileBranch param)
        {
            var result = repo.UPDATE(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Delete")]
        public ActionResult Delete(ProfileBranch param)
        {
            var result = repo.DELETE(param).Result;

            return Ok(result);
        }


    }
}
