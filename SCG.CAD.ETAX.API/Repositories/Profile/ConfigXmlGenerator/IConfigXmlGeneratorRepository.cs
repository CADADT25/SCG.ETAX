namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IConfigXmlGeneratorRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_LIST_FOR_SERVICE();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ConfigXmlGenerator param);
        Task<Response> UPDATE(ConfigXmlGenerator param);
        Task<Response> DELETE(ConfigXmlGenerator param);
    }
}
