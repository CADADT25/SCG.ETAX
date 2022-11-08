namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IRequestPermissionRepository
    {
        Task<Response> GET_ROLES_AND_COMPANYS(string user);
    }
}
