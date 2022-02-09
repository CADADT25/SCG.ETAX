using SCG.CAD.ETAX.MODEL;

namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IETDARepository
    {
        Task<Response> GetThaiISOCountrySubdivisionCode();
        Task<Response> GetTISICityName();
        Task<Response> GetTISICitySubDivisionName();
        Task<Response> GetThaiPurposeCode_Invoice();
        Task<Response> GetThaiMessageFunctionCode();
        Task<Response> GetThaiDocumentNameCode();
        Task<Response> GetThai_MessageFunctionCode();
    }
}
