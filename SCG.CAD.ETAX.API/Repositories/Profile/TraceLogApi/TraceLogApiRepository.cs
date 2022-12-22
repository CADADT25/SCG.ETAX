namespace SCG.CAD.ETAX.API.Repositories
{
    public class TraceLogApiRepository : ITraceLogApiRepository
    {
        TraceLogApiService service = new TraceLogApiService();
        public async Task<Response> INSERT(TraceLogApi param)
        {
            Response resp = new Response();

            try
            {
                var result = service.INSERT(param);

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
