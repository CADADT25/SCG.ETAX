namespace SCG.CAD.ETAX.API.Repositories
{
    public interface ITransactionDescriptionRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> GET_BILLING(string billingNo);
        Task<Response> INSERT(TransactionDescription param);
        Task<Response> UPDATE(TransactionDescription param);
        Task<Response> UPDATE_LIST(List<TransactionDescription> param);
        Task<Response> DELETE(TransactionDescription param);
        Task<Response> SEARCH(string JsonString);
        Task<Response> SYNCSTATUSPDFSIGN(string listbillno, string updateby);
        Task<Response> SYNCSTATUSXMLSIGN(string listbillno, string updateby);
        Task<Response> DOWNLOADFILE(string pathfile);

    }
}
