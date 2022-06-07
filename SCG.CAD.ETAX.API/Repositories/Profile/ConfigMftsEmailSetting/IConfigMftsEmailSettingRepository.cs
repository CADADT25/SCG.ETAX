namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IConfigMftsEmailSettingRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ConfigMftsEmailSetting param);
        Task<Response> UPDATE(ConfigMftsEmailSetting param);
        Task<Response> DELETE(ConfigMftsEmailSetting param);
        Task<Response> UPDATE_ONETIME(ConfigMftsEmailSetting param);
        Task<Response> UPDATE_ANYTIME(ConfigMftsEmailSetting param);
        Task<Response> DELETE_ONETIME(DeleteOnetime param);
        Task<Response> DELETE_ANYTIME(DeleteAnytime param);
        Task<Response> UPDATE_NEXTTIME(ConfigNextTime param);

    }
}
