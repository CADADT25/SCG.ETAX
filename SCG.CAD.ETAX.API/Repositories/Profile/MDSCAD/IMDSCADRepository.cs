namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IMDSCADRepository
    {
        Task<Response> GET_MANAGER_BY_USER(string user);
    }
}
