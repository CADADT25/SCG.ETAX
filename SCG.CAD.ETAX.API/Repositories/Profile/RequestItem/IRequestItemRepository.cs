namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IRequestItemRepository
    {
        Task<Response> GET_LIST();
        Task<Response> INSERT(RequestItem param);
        Task<Response> UPDATE(RequestItem param);
        Task<Response> DELETE(RequestItem param);
    }
}
