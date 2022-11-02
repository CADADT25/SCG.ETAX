﻿namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IRequestRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_REQUEST(string requestNo);
        Task<Response> INSERT(Request param);
        Task<Response> UPDATE(Request param);
        Task<Response> DELETE(Request param);
        Task<Response> SUBMIT_REQUEST(RequestDataModel param);
    }
}
