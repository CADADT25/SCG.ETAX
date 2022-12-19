namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IXMLGenerateRepository
    {
        Task<Response> ProcessXMLGenerate(string parttextfile);
    }
}
