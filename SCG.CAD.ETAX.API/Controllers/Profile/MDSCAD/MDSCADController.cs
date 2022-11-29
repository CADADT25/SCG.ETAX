using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{
    public class MDSCADController : BaseController
    {
        private readonly IMDSCADRepository repo;
        public MDSCADController()
        {
            repo = new MDSCADRepository();
        }

        [HttpGet]
        [Route("GetManagerByUser")]
        public IActionResult GetManagerByUser(string user)
        {
            var result = repo.GET_MANAGER_BY_USER(user).Result;

            return Ok(result);
        }

    }
}
