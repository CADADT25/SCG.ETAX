namespace SCG.CAD.ETAX.API.Repositories.Profile.SendEmail
{
    public interface ISendEmailRepository
    {
        Task<Response> ResendEmail(string billno, string updateby);
        Task<Response> RequestActionEmail(string requestNo, string action);
    }
}
