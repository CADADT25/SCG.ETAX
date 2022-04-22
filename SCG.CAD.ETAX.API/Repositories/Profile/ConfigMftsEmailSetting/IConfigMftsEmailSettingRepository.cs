namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IConfigMftsEmailSettingRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ConfigMftsEmailSetting param);
        Task<Response> UPDATE(ConfigMftsEmailSetting param);
        Task<Response> DELETE(ConfigMftsEmailSetting param);
    }
}
