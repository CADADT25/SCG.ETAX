namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IConfigMftsIndexGenerationSettingOutputRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ConfigMftsIndexGenerationSettingOutput param);
        Task<Response> UPDATE(ConfigMftsIndexGenerationSettingOutput param);
        Task<Response> DELETE(ConfigMftsIndexGenerationSettingOutput param);
    }
}
