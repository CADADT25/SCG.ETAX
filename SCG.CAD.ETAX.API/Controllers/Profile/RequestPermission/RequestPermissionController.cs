using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestPermissionController : ControllerBase
    {
        private readonly IRequestPermissionRepository repo;
        public RequestPermissionController()
        {
            repo = new RequestPermissionRepository();
        }

        [HttpGet]
        [Route("GetRolesCompanys")]
        public IActionResult GetRolesCompanys(string user)
        {
            var result = repo.GET_ROLES_AND_COMPANYS(user).Result;

            return Ok(result);
        }

    }
}
