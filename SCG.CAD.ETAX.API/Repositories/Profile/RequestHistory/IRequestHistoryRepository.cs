﻿namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IRequestHistoryRepository
    {
        Task<Response> GET_LIST();
        Task<Response> INSERT(RequestHistory param);
        Task<Response> UPDATE(RequestHistory param);
        Task<Response> DELETE(RequestHistory param);
    }
}