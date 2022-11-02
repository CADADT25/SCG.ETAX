using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InboxManagementController : ControllerBase
    {
        private readonly IInboxManagementRepository repo;
        public InboxManagementController()
        {
            repo = new InboxManagementRepository();
        }

        [HttpPost]
        [Route("SearchToDo")]
        public IActionResult SearchToDo(InboxSearchModel search)
        {
            var result = repo.SEARCH_TODO(search).Result;

            return Ok(result);
        }
        [HttpPost]
        [Route("SearchInprogress")]
        public IActionResult SearchInprogress(InboxSearchModel search)
        {
            var result = repo.SEARCH_INPROGRESS(search).Result;

            return Ok(result);
        }
        [HttpPost]
        [Route("SearchAll")]
        public IActionResult SearchAll(InboxSearchModel search)
        {
            var result = repo.SEARCH_ALL(search).Result;

            return Ok(result);
        }

    }
}
