namespace SCG.CAD.ETAX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FontController : ControllerBase
    {
        private readonly IFontRepository repo;

        public FontController()
        {
            repo = new FontRepository();
        }
         
        [HttpGet]
        [Route("GetListAll")]
        public IActionResult GetListAll()
        {
            var result = repo.GET_LIST().Result;

            return Ok(result);
        }
    }
}
