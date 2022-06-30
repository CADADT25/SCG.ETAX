namespace SCG.CAD.ETAX.API.Repositories
{ 
    public interface IOutputSearchPrintingRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(OutputSearchPrinting param);
        Task<Response> UPDATE(OutputSearchPrinting param);
        Task<Response> DELETE(OutputSearchPrinting param);
        Task<Response> SEARCH(string JsonString);

    }
}
