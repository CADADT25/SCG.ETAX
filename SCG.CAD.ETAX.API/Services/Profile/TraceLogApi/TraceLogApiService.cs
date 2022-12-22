using SCG.CAD.ETAX.MODEL.etaxModel;

namespace SCG.CAD.ETAX.API.Services
{
    public class TraceLogApiService : DatabaseExecuteController
    {
        //readonly DatabaseContext _dbContext = new();
        sqlTraceLogApi sqlFactory = new sqlTraceLogApi();
        public Response INSERT(TraceLogApi param)
        {
            Response resp = new Response();
            try
            {
                string sqlInsert = sqlFactory.GET_INSERT();
                base.InsertTraceLogApiBySql(sqlInsert, param);
                resp.STATUS = true;
                //using (_dbContext)
                //{
                //    _dbContext.traceLogApi.Add(param);
                //    _dbContext.SaveChanges();

                //    resp.STATUS = true;
                //    resp.MESSAGE = "Insert success.";
                //}
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Insert faild.";
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }

    }
}
