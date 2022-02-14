namespace SCG.CAD.ETAX.API.Repositories
{
    public interface ITransactionDescription
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(TransactionDescription param);
        Task<Response> UPDATE(TransactionDescription param);
        Task<Response> DELETE(TransactionDescription param);
    }
}
