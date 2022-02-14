namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IProfileSellerRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ProfileSeller param);
        Task<Response> UPDATE(ProfileSeller param);
        Task<Response> DELETE(ProfileSeller param);
    }
}
