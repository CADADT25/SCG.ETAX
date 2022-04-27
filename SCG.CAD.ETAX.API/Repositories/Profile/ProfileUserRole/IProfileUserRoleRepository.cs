namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IProfileUserRoleRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ProfileUserRole param);
        Task<Response> UPDATE(ProfileUserRole param);
        Task<Response> DELETE(ProfileUserRole param);
    }
}
