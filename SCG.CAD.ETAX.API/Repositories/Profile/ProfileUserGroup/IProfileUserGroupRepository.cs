namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IProfileUserGroupRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ProfileUserGroup param);
        Task<Response> UPDATE(ProfileUserGroup param);
        Task<Response> DELETE(ProfileUserGroup param);
    }
}
