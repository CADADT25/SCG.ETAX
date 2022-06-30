namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IOutputSearchXmlZipRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(OutputSearchXmlZip param);
        Task<Response> UPDATE(OutputSearchXmlZip param);
        Task<Response> DELETE(OutputSearchXmlZip param);
        Task<Response> SEARCH(string JsonString);

    }
}
