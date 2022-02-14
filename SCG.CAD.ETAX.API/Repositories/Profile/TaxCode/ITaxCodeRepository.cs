
namespace SCG.CAD.ETAX.API.Repositories
{
    public interface ITaxCodeRepository
    {
        // CRUD Interface
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int taxCodeNo);
        Task<Response> INSERT(TaxCode param);
        Task<Response> UPDATE();
        Task<Response> DELETE();


    }
}
