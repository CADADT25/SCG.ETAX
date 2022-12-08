namespace SCG.CAD.ETAX.API.Repositories
{
    public class ProfileUserManagementRepository : IProfileUserManagementRepository
    {


        ProfileUserManagementService service = new ProfileUserManagementService();

        public async Task<Response> GET_DETAIL(int id)
        {
            Response resp = new Response();

            try
            {
                var result = service.GET_DETAIL(id);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }

            return await Task.FromResult(resp);
        }
        public async Task<Response> GET_DETAIL_BY_EXTERNALID(string id)
        {
            Response resp = new Response();

            try
            {
                var result = service.GET_DETAIL_BY_EXTERNALID(id);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }

            return await Task.FromResult(resp);
        }
        public async Task<Response> GET_DETAIL_BY_EMAIL_EXTERNALID2(string email)
        {
            Response resp = new Response();

            try
            {
                var result = service.GET_DETAIL_BY_EMAIL_EXTERNALID2(email);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }

            return await Task.FromResult(resp);
        }

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
        public async Task<Response> GET_LIST_ADMIN()
        {
            Response resp = new Response();
            try
            {
                var result = service.GET_LIST_ADMIN();

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }

            return await Task.FromResult(resp);
        }

        public async Task<Response> INSERT(ProfileUserManagement param)
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

        public async Task<Response> UPDATE(ProfileUserManagement param)
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

        public async Task<Response> UPDATE_EXTERNALID(ProfileUserManagement param)
        {
            Response resp = new Response();

            try
            {
                var result = service.UPDATE_EXTERNALID(param);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.Message.ToString();
            }

            return await Task.FromResult(resp);
        }

        public async Task<Response> DELETE(ProfileUserManagement param)
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


    }
}
