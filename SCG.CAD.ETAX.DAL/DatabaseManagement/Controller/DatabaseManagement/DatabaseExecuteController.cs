using SCG.CAD.ETAX.DAL.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.DAL.CONTROLLER
{
    public class DatabaseExecuteController : TransSqlController, IDisposable
    {
        OutputOnDbModel resultData = new OutputOnDbModel();

        protected OutputOnDbModel SearchBySqlList(List<string> sqlList)
        {
            try
            {
                resultData = base.TransConnection();

                if (resultData.StatusOnDb == true)
                {
                    resultData = base.TransSelectCommand(sqlList[0]);

                    string totalCount = resultData.ResultOnDb.Rows[0]["TOTAL_COUNT"].ToString();

                    resultData = base.TransSelectCommand(sqlList[1]);

                    resultData.TotalCountOnDb = totalCount;
                }
            }
            finally
            {
                base.TransClose();
            }

            return resultData;
        }

        protected OutputOnDbModel SearchBySql(string sql)
        {
            try
            {
                resultData = base.TransConnection();

                if (resultData.StatusOnDb == true)
                {
                    resultData = base.TransSelectCommand(sql);
                }
            }
            finally
            {
                base.TransClose();
            }

            return resultData;
        }

        protected OutputOnDbModel InsertBySql(string sql)
        {
            try
            {
                resultData = base.TransConnection();

                if (resultData.StatusOnDb == true)
                {

                    resultData = base.TransExecuteCommand(sql);

                    if (resultData.StatusOnDb == true)
                    {
                        base.TransCommit();
                        resultData.MessageOnDb = "Save Data Success";
                    }
                    else
                    {
                        base.TransRolback();
                    }
                }
            }
            finally
            {
                base.TransClose();
            }

            return resultData;
        }

        protected OutputOnDbModel UpdateBySql(string sql)
        {
            try
            {
                resultData = base.TransConnection();

                if (resultData.StatusOnDb == true)
                {
                    resultData = base.TransExecuteCommand(sql);

                    if (resultData.StatusOnDb == true)
                    {
                        base.TransCommit();
                        resultData.MessageOnDb = "Update Data Success";
                    }
                    else
                    {
                        base.TransRolback();
                    }
                }
            }
            finally
            {
                base.TransClose();
            }

            return resultData;
        }

        protected OutputOnDbModel DeleteBySql(string sql)
        {
            try
            {
                resultData = base.TransConnection();

                if (resultData.StatusOnDb == true)
                {
                    resultData = base.TransExecuteCommand(sql);

                    if (resultData.StatusOnDb == true)
                    {
                        base.TransCommit();
                        resultData.MessageOnDb = "Delete Data Success";
                    }
                    else
                    {
                        base.TransRolback();
                    }
                }
            }
            finally
            {
                base.TransClose();
            }

            return resultData;
        }




        #region IDispose
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
