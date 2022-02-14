namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IProfileStatus
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ProfileStatus param);
        Task<Response> UPDATE(ProfileStatus param);
        Task<Response> DELETE(ProfileStatus param);
    }
}
