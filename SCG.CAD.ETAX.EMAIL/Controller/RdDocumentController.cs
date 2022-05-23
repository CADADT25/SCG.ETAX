using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;


namespace SCG.CAD.ETAX.EMAIL.Controller
{
    public class RdDocumentController
    {
        public async Task<List<RdDocument>> List()
        {
            Response resp = new Response();

            List<RdDocument> tran = new List<RdDocument>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/RdDocument/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<RdDocument>>(task.OUTPUT_DATA.ToString());
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }


            return tran;
        }
    }
}
