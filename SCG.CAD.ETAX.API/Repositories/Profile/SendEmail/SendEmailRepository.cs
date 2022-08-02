using SCG.CAD.ETAX.UTILITY.AdminTool;

namespace SCG.CAD.ETAX.API.Repositories.Profile.SendEmail
{
    public class SendEmailRepository : ISendEmailRepository
    {
        ResendEmail resendEmail = new ResendEmail();
        public async Task<Response> ResendEmail(string billno, string updateby)
        {
            Response resp = new Response();

            try
            {
                var result = resendEmail.ResendEmailAgain(billno, updateby);
                
                resp.STATUS = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }
            return await Task.FromResult(resp);
        }
    }
}
