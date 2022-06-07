namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IProfileUserManagementRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ProfileUserManagement param);
        Task<Response> UPDATE(ProfileUserManagement param);
        Task<Response> DELETE(ProfileUserManagement param);
    }
}
