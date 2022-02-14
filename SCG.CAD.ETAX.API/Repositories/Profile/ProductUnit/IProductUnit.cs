namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IProductUnit
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ProductUnit param);
        Task<Response> UPDATE(ProductUnit param);
        Task<Response> DELETE(ProductUnit param);
    }
}
