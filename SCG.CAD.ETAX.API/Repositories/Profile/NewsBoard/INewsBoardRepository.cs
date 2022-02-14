namespace SCG.CAD.ETAX.API.Repositories
{
    public interface INewsBoardRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(NewsBoard param);
        Task<Response> UPDATE(NewsBoard param);
        Task<Response> DELETE(NewsBoard param);
    }
}
