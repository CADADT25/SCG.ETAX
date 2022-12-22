namespace SCG.CAD.ETAX.API.Repositories
{
    public interface ITraceLogApiRepository
    {
        Task<Response> INSERT(TraceLogApi param);
    }
}
