namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IProfileCompany
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ProfileCompany param);
        Task<Response> UPDATE(ProfileCompany param);
        Task<Response> DELETE(ProfileCompany param);
    }
}
