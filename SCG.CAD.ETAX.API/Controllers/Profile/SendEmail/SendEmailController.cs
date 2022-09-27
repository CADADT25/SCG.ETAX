using SCG.CAD.ETAX.API.Repositories.Profile.SendEmail;

namespace SCG.CAD.ETAX.API.Controllers.Profile.SendEmail
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEmailController : ControllerBase
    {
        private readonly ISendEmailRepository repo;

        public SendEmailController()
        {
            repo = new SendEmailRepository();
        }

        [HttpGet]
        [Route("SendEmail")]
        public IActionResult SendEmail(string billno, string updateby)
        {
            var result = repo.ResendEmail(billno, updateby).Result;

            return Ok(result);
        }
    }
}
