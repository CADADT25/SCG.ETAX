namespace SCG.CAD.ETAX.API.Repositories
{
    public class ProductUnitRepository : IProductUnitRepository
    {
        ProductUnitService service = new ProductUnitService();

        public Task<Response> DELETE(MODEL.ProductUnit param)
        {
            throw new NotImplementedException();
        }

        public Task<Response> GET_DETAIL(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response> GET_LIST()
        {
            throw new NotImplementedException();
        }

        public Task<Response> INSERT(MODEL.ProductUnit param)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UPDATE(MODEL.ProductUnit param)
        {
            throw new NotImplementedException();
        }
    }
}
