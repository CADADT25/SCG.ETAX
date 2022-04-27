namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IConfigMftsCompressPrintSettingRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ConfigMftsCompressPrintSetting param);
        Task<Response> UPDATE(ConfigMftsCompressPrintSetting param);
        Task<Response> DELETE(ConfigMftsCompressPrintSetting param);
    }
}
