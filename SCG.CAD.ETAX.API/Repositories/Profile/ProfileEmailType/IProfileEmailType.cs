namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IProfileEmailType
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ProfileEmailType param);
        Task<Response> UPDATE(ProfileEmailType param);
        Task<Response> DELETE(ProfileEmailType param);
    }
}
