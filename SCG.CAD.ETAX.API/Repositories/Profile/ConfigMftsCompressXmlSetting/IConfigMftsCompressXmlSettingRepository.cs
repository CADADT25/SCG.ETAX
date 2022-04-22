namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IConfigMftsCompressXmlSettingRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ConfigMftsCompressXmlSetting param);
        Task<Response> UPDATE(ConfigMftsCompressXmlSetting param);
        Task<Response> DELETE(ConfigMftsCompressXmlSetting param);
    }
}
