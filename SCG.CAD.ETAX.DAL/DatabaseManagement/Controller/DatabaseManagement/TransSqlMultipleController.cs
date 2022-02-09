using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCG.CAD.ETAX.DAL.MODEL;

namespace SCG.CAD.ETAX.DAL.CONTROLLER
{
    public class TransSqlMultipleController
    {
        private SqlConnection? conTrans;
        private SqlCommand userCommand = new SqlCommand();
        private SqlTransaction? trans;

        protected string strConnection = "";
        private OutputOnDbModel? dataOutPut;

        public TransSqlMultipleController()
        {
            strConnection = ConfigurationManager.ConnectionStrings["ConnectionStr"].ConnectionString;
        }


        public OutputOnDbModel TransConnection()
        {
            try
            {
                conTrans = new SqlConnection(strConnection);
                conTrans.Open();
                trans = conTrans.BeginTransaction(IsolationLevel.ReadCommitted);     //*** Start  
                userCommand.Connection = conTrans;
                userCommand.Transaction = trans;  //*** Command & Transaction ***// 

                dataOutPut = new OutputOnDbModel
                {
                    StatusOnDb = true,
                    MessageOnDb = "Connect Database Success",
                    ClassOnDb = "Class Name: TransOracleModels",
                    MethodOnDb = "Method Name: TransConnection",
                    ResultOnDb = null
                };
            }
            catch (Exception er)
            {
                dataOutPut = new OutputOnDbModel
                {
                    StatusOnDb = false,
                    ClassOnDb = "Class Name: TransOracleModels",
                    MethodOnDb = "Method Name: TransConnection",
                    MessageOnDb = "Connect Database Error ==> " + er.ToString(),
                    ResultOnDb = null
                };
            }

            return dataOutPut;
        }

        public OutputOnDbModel TransSelectCommand(string sql)
        {
            SqlConnection connecter = new SqlConnection(strConnection);
            DataTable tableResult = new DataTable();

            try
            {
                connecter.Open();

                SqlDataAdapter userDataAdaptor = new SqlDataAdapter(sql, connecter);

                userDataAdaptor.Fill(tableResult);

                connecter.Close();
                connecter.Dispose();

                dataOutPut = new OutputOnDbModel
                {
                    StatusOnDb = true,
                    ClassOnDb = "Class Name: TransOracleModels",
                    MethodOnDb = "Method Name: TransSelectCommand",
                    MessageOnDb = "",
                    ResultOnDb = tableResult
                };

            }
            catch (Exception er)
            {
                dataOutPut = new OutputOnDbModel
                {
                    StatusOnDb = false,
                    ClassOnDb = "Class Name: TransOracleModels",
                    MethodOnDb = "Method Name: TransSelectCommand",
                    MessageOnDb = "Select SQL Error ==> " + er.ToString() + "<br>" + sql,
                    ResultOnDb = null
                };
            }

            return dataOutPut;
        }

        public OutputOnDbModel TransExecuteCommand(string sql)
        {
            try
            {
                userCommand.CommandText = sql;
                userCommand.CommandType = CommandType.Text;
                userCommand.ExecuteNonQuery();

                dataOutPut = new OutputOnDbModel
                {
                    StatusOnDb = true,
                    ClassOnDb = "Class Name: TransOracleModels",
                    MethodOnDb = "Method Name: TransExecuteCommand",
                    MessageOnDb = "",
                    ResultOnDb = null
                };
            }
            catch (Exception er)
            {
                dataOutPut = new OutputOnDbModel
                {
                    StatusOnDb = false,
                    ClassOnDb = "Class Name: TransOracleModels",
                    MethodOnDb = "Method Name: TransExecuteCommand",
                    MessageOnDb = "Execute SQL Command Error ==> " + er.ToString() + "<br>" + sql,
                    ResultOnDb = null
                };
            }

            return dataOutPut;
        }

        public void TransCommit()
        {
            trans.Commit();
            conTrans.Close();
            conTrans.Dispose();
        }

        public void TransRolback()
        {
            trans.Rollback();
            conTrans.Close();
            conTrans.Dispose();
        }

        public void TransClose()
        {
            if (conTrans != null)
            {

                conTrans.Close();
                conTrans.Dispose();
            }
        }

        public string GetDBName()
        {
            string result = "";
            if (conTrans != null)
            {
                result = conTrans.DataSource;
            }
            return result;
        }


    }
}
