namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IProfileUserManagementRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_LIST_ADMIN();
        Task<Response> GET_DETAIL(int id);
        Task<Response> GET_DETAIL_BY_EXTERNALID(string id);
        Task<Response> GET_DETAIL_BY_EMAIL_EXTERNALID2(string email);
        Task<Response> INSERT(ProfileUserManagement param);
        Task<Response> UPDATE(ProfileUserManagement param);
        Task<Response> UPDATE_EXTERNALID(ProfileUserManagement param);
        Task<Response> DELETE(ProfileUserManagement param);
    }
}
