using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.MODEL.etaxModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.DAL
{
    public class sqlTraceLogApi
    {
        public string GET_INSERT()
        {
            string strSql = @"INSERT INTO [dbo].[TraceLogApi]
                       ([RequestContentType]
                       ,[RequestUri]
                       ,[RequestPath]
                       ,[RequestMethod]
                       ,[RequestTimestamp]
                       ,[RequestBody]
                       ,[ResponseBody]
                       ,[ResponseContentType]
                       ,[ResponseStatusCode]
                       ,[ResponseTimestamp])
                 VALUES
                       (@RequestContentType
			            ,@RequestUri
			            ,@RequestPath
			            ,@RequestMethod
			            ,@RequestTimestamp
			            ,@RequestBody
			            ,@ResponseBody
			            ,@ResponseContentType
			            ,@ResponseStatusCode
			            ,@ResponseTimestamp)";

            return strSql;
        }

    }
}
