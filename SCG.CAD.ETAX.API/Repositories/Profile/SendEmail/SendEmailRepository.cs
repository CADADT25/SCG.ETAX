using SCG.CAD.ETAX.UTILITY.AdminTool;

namespace SCG.CAD.ETAX.API.Repositories.Profile.SendEmail
{
    public class SendEmailRepository : ISendEmailRepository
    {
        ResendEmail resendEmail = new ResendEmail();
        RequestActionEmail requestActionEmail = new RequestActionEmail();
        public async Task<Response> ResendEmail(string billno, string updateby)
        {
            Response resp = new Response();

            try
            {
                resp = resendEmail.ResendEmailAgain(billno, updateby);
                
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }
            return await Task.FromResult(resp);
        }
        public async Task<Response> RequestActionEmail(string requestNo, string action)
        {
            Response resp = new Response();

            try
            {
                resp = requestActionEmail.SendEmail(requestNo, action);
                
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
