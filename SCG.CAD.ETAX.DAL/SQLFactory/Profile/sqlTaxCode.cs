using SCG.CAD.ETAX.MODEL.etaxModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.DAL
{
    public class sqlTaxCode
    {
        public string GET_TAXTCODE_ALL()
        {
            string strSql = @"SELECT * FROM taxCode";

            return strSql;
        }

        public string GET_TAXTCODE_DETAIL(string taxCodeNo)
        {
            string strSql = @"SELECT * FROM TAXCODE WHERE taxCodeNo = '" + taxCodeNo + "' ";

            return strSql;
        }

        public string INSERT_TAXTCODE(TaxCode param)
        {
            string strSql = @"INSERT INTO [dbo].[taxCode]
           ([TaxCodeErp]
           ,[TaxCodeRd]
           ,[TaxCodeDescription]
           ,[createBy]
           ,[createDate]
           ,[updateBy]
           ,[updateDate]
           ,[isactive])
            VALUES
           ('" + param.TaxCodeErp + "', " +
           "'" + param.TaxCodeRd + "' , " +
           "'" + param.TaxCodeDescription + "' , " +
           "'" + param.CreateBy + "' , " +
           "'" + DateTime.Now + "' , " +
           "'" + param.UpdateBy + "' , " +
           "'" + DateTime.Now + "' , " +
           "'" + 1 + "')";

            return strSql;
        }


        public string UPDATE_TAXTCODE(TaxCode param)
        {
            string strSql = @"UPDATE [dbo].[taxCode]
                           SET 
                            [TaxCodeErp] = '" + param.TaxCodeErp + "'," +
                           "[TaxCodeRd] = '" + param.TaxCodeRd + "'," +
                           "[TaxCodeDescription] = '" + param.TaxCodeDescription + "'," +
                           //"[createBy] = '" + param.TaxCodeErp + "'," +
                           //"[createDate] = '" + param.TaxCodeErp + "'," +
                           "[updateBy] = '" + param.UpdateBy + "'," +
                           "[updateDate] = '" + param.UpdateDate + "'," +
                           "[isactive] = '" + 1 + "'," +
                           "WHERE [taxCodeNo] = '" + param.TaxCodeNo + "'";
            return strSql;
        }

        public string DELETE_TAXTCODE(TaxCode param)
        {
            string strSql = @"";
            return strSql;
        }


    }
}
