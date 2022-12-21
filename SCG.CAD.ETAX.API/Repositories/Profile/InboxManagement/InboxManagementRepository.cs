namespace SCG.CAD.ETAX.API.Repositories
{
    public class InboxManagementRepository : IInboxManagementRepository
    {
        InboxManagementService service = new InboxManagementService();
        public async Task<Response> SEARCH_TODO(InboxSearchModel search)
        {
            Response resp = new Response();
            try
            {
                var result = service.SEARCH_TODO(search);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.Message.ToString();
            }

            return await Task.FromResult(resp);
        }
        public async Task<Response> SEARCH_INPROGRESS(InboxSearchModel search)
        {
            Response resp = new Response();
            try
            {
                var result = service.SEARCH_INPROGRESS(search);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.Message.ToString();
            }

            return await Task.FromResult(resp);
        }
        public async Task<Response> SEARCH_ALL(InboxSearchModel search)
        {
            Response resp = new Response();
            try
            {
                var result = service.SEARCH_ALL(search);

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
