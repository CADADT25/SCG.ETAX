using SCG.CAD.ETAX.MODEL;

namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IETDARepository
    {
        Task<Response> GetThaiISOCountrySubdivisionCode();
        Task<Response> GetTISICityName(string ProviceCode);
        Task<Response> GetTISICitySubDivisionName(string DistrictCode);
        Task<Response> GetThaiPurposeCode_Invoice();
        Task<Response> GetThaiMessageFunctionCode();
        Task<Response> GetThaiDocumentNameCode();
        Task<Response> GetThai_MessageFunctionCode();
    }
}
