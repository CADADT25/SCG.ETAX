namespace SCG.CAD.ETAX.API.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        RequestService service = new RequestService();
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
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }

            return await Task.FromResult(resp);
        }
        public async Task<Response> GET_REQUEST(string requestNo)
        {
            Response resp = new Response();
            try
            {
                var result = service.GET_REQUEST(requestNo);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }

            return await Task.FromResult(resp);
        }
        public async Task<Response> GET_REQUEST_ITEM_TRANSACTION(string requestNo)
        {
            Response resp = new Response();
            try
            {
                var result = service.GET_REQUEST_ITEM_TRANSACTION(requestNo);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }

            return await Task.FromResult(resp);
        }
        public async Task<Response> INSERT(Request param)
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
        public async Task<Response> UPDATE(Request param)
        {
            Response resp = new Response();

            try
            {
                var result = service.UPDATE(param);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.Message.ToString();
            }

            return await Task.FromResult(resp);
        }
        public async Task<Response> DELETE(Request param)
        {
            Response resp = new Response();

            try
            {
                var result = service.DELETE(param);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.Message.ToString();
            }

            return await Task.FromResult(resp);
        }
        public async Task<Response> SUBMIT_REQUEST(RequestDataModel param)
        {
            Response resp = new Response();

            try
            {
                var result = service.SUBMIT_REQUEST(param);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.Message.ToString();
            }

            return await Task.FromResult(resp);
        }
        public async Task<Response> SUBMIT_REQUEST_NEW_TRANS(RequestDataModel param)
        {
            Response resp = new Response();

            try
            {
                var result = service.SUBMIT_REQUEST_NEW_TRANS(param);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.Message.ToString();
            }

            return await Task.FromResult(resp);
        }
        public async Task<Response> Action(RequestActionDataModel param)
        {
            Response resp = new Response();

            try
            {
                var result = service.Action(param);

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
