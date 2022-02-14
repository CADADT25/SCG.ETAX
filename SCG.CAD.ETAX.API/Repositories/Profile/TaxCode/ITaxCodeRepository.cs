
namespace SCG.CAD.ETAX.API.Repositories
{
    public interface ITaxCodeRepository
    {
        // CRUD Interface
        Task<Response> GetTaxCodeAll();
        Task<Response> GetTaxCodeDetail(int taxCodeNo);
        Task<Response> InsertTaxCode(TaxCode param);
        Task<Response> UpdateTaxCode();
        Task<Response> DeleteTaxCode();


    }
}
