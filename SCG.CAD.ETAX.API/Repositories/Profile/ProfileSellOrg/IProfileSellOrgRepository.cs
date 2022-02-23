namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IProfileSellOrgRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ProfileSellOrg param);
        Task<Response> UPDATE(ProfileSellOrg param);
        Task<Response> DELETE(ProfileSellOrg param);
    }
}
