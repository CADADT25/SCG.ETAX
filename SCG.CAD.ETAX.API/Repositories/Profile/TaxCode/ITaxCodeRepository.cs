
namespace SCG.CAD.ETAX.API.Repositories
{
    public interface ITaxCodeRepository
    {
        // CRUD Interface
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(TaxCode param);
        Task<Response> UPDATE(TaxCode param);
        Task<Response> DELETE(TaxCode param);


    }
}
