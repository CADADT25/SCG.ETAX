namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IConfigMftsCompressPrintSettingRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ConfigMftsCompressPrintSetting param);
        Task<Response> UPDATE(ConfigMftsCompressPrintSetting param);
        Task<Response> DELETE(ConfigMftsCompressPrintSetting param);
        Task<Response> UPDATE_ONETIME(ConfigMftsCompressPrintSetting param);
        Task<Response> UPDATE_ANYTIME(ConfigMftsCompressPrintSetting param);
        Task<Response> DELETE_ONETIME(DeleteOnetime param);
        Task<Response> DELETE_ANYTIME(DeleteAnytime param);
        Task<Response> UPDATE_NEXTTIME(ConfigNextTime param);
    }
}
