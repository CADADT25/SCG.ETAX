namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IProfileCustomerRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ProfileCustomer param);
        Task<Response> UPDATE(ProfileCustomer param);
        Task<Response> DELETE(ProfileCustomer param);
    }
}
