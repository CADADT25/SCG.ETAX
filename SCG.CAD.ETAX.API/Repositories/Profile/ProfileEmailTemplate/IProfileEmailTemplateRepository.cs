namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IProfileEmailTemplateRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ProfileEmailTemplate param);
        Task<Response> UPDATE(ProfileEmailTemplate param);
        Task<Response> DELETE(ProfileEmailTemplate param);
    }
}
