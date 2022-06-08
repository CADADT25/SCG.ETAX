namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IConfigMftsIndexGenerationSettingOutputRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ConfigMftsIndexGenerationSettingOutput param);
        Task<Response> UPDATE(ConfigMftsIndexGenerationSettingOutput param);
        Task<Response> DELETE(ConfigMftsIndexGenerationSettingOutput param);
        Task<Response> UPDATE_ONETIME(ConfigMftsIndexGenerationSettingOutput param);
        Task<Response> UPDATE_ANYTIME(ConfigMftsIndexGenerationSettingOutput param);
        Task<Response> DELETE_ONETIME(DeleteOnetime param);
        Task<Response> DELETE_ANYTIME(DeleteAnytime param);
        Task<Response> UPDATE_NEXTTIME(ConfigNextTime param);

    }
}
