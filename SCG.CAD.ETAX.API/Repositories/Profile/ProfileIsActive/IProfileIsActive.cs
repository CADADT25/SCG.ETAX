namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IProfileIsActive
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ProfileIsActive param);
        Task<Response> UPDATE(ProfileIsActive param);
        Task<Response> DELETE(ProfileIsActive param);
    }
}
