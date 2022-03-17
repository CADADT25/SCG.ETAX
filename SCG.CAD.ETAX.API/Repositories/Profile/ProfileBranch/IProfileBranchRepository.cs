namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IProfileBranchRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ProfileBranch param);
        Task<Response> INSERTS(List<ProfileBranch> param);
        Task<Response> UPDATE(ProfileBranch param);
        Task<Response> DELETE(ProfileBranch param);
    }
}
