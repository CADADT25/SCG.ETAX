namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IConfigXmlSignRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ConfigXmlSign param);
        Task<Response> UPDATE(ConfigXmlSign param);
        Task<Response> DELETE(ConfigXmlSign param);
    }
}
