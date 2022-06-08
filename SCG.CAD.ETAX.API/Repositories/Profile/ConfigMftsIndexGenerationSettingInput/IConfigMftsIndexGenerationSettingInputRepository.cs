namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IConfigMftsIndexGenerationSettingInputRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ConfigMftsIndexGenerationSettingInput param);
        Task<Response> UPDATE(ConfigMftsIndexGenerationSettingInput param);
        Task<Response> DELETE(ConfigMftsIndexGenerationSettingInput param);
        Task<Response> UPDATE_ONETIME(ConfigMftsIndexGenerationSettingInput param);
        Task<Response> UPDATE_ANYTIME(ConfigMftsIndexGenerationSettingInput param);
        Task<Response> DELETE_ONETIME(DeleteOnetime param);
        Task<Response> DELETE_ANYTIME(DeleteAnytime param);
        Task<Response> UPDATE_NEXTTIME(ConfigNextTime param);
    }
}
