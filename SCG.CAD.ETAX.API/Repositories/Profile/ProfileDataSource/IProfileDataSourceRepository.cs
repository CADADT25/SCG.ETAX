namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IProfileDataSourceRepository
    {

        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ProfileDataSource param);
        Task<Response> UPDATE(ProfileDataSource param);
        Task<Response> DELETE(ProfileDataSource param);

    }
}
