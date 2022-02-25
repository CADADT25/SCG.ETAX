namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IProfileCompanyCodeRepository
    {

        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ProfileCompanyCode param);
        Task<Response> UPDATE(ProfileCompanyCode param);
        Task<Response> DELETE(ProfileCompanyCode param);

    }
}
