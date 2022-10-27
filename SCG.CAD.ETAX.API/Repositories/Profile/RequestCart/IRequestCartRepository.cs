namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IRequestCartRepository
    {
        Task<Response> GET_LIST();
        Task<Response> INSERT(RequestCart param);
        Task<Response> INSERT(List<RequestCart> param);
        Task<Response> UPDATE(RequestCart param);
        Task<Response> DELETE(RequestCart param);
    }
}
