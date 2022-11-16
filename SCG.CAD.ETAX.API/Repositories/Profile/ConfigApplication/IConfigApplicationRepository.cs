namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IConfigApplicationRepository
    {
        Task<Response> CHECK_KEY(string key);
    }
}
