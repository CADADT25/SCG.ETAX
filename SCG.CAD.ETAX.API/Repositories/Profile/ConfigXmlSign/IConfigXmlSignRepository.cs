namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IConfigXmlSignRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ConfigXmlSign param);
        Task<Response> UPDATE(ConfigXmlSign param);
        Task<Response> DELETE(ConfigXmlSign param);
        Task<Response> UPDATE_ONETIME(ConfigXmlSign param);
        Task<Response> UPDATE_ANYTIME(ConfigXmlSign param);
        Task<Response> DELETE_ONETIME(DeleteOnetime param);
        Task<Response> DELETE_ANYTIME(DeleteAnytime param);
        Task<Response> UPDATE_NEXTTIME(ConfigNextTime param);
    }
}
