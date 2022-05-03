namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IProfileFiDocRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ProfileFiDoc param);
        Task<Response> UPDATE(ProfileFiDoc param);
        Task<Response> DELETE(ProfileFiDoc param);
    }
}
