using SCG.CAD.ETAX.API.Services.Profile.CertificateMaster;

namespace SCG.CAD.ETAX.API.Repositories.Profile.CertificateMaster
{
    public class CertificateMasterRepository : ICertificateMasterRepository
    {
        CertificateMasterService service = new CertificateMasterService();
        public async Task<Response> GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var result = service.GET_LIST();

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.Message.ToString();
            }

            return await Task.FromResult(resp);
        }
    }
}
