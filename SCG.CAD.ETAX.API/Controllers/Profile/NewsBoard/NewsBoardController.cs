using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsBoardController : ControllerBase
    {
        private readonly INewsBoardRepository repo;

        public NewsBoardController()
        {
            repo = new NewsBoardRepository();
        }


    }
}
