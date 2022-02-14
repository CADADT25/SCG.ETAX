namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IProfilePartner
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ProfilePartner param);
        Task<Response> UPDATE(ProfilePartner param);
        Task<Response> DELETE(ProfilePartner param);
    }
}
