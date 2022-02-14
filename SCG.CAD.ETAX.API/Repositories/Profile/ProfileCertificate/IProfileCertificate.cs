namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IProfileCertificate
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ProfileCertificate param);
        Task<Response> UPDATE(ProfileCertificate param);
        Task<Response> DELETE(ProfileCertificate param);
    }
}
