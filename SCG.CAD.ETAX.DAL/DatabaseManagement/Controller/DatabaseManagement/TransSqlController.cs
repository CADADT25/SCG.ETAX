using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCG.CAD.ETAX.DAL.MODEL;
using Microsoft.Extensions.Configuration;
using SCG.CAD.ETAX.MODEL.etaxModel;
using System.Drawing;

namespace SCG.CAD.ETAX.DAL.CONTROLLER
{
    public class TransSqlController
    {
        private SqlConnection conTrans;
        private SqlCommand userCommand = new SqlCommand();
        private SqlTransaction trans;

        protected string strConnection = "";
        private OutputOnDbModel dataOutPut;


        public TransSqlController(string conDbName = "")
        {
            //strConnection = ConfigurationManager.ConnectionStrings["ConnectionStr"].ConnectionString;
            if (string.IsNullOrEmpty(conDbName))
            {
                strConnection = new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["ConnectionStr"];
            }
            else
            {
                strConnection = new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")[conDbName];
            }

            //strConnection = ConfigurationManager.ConnectionStrings["ConnectionStr"].ConnectionString;

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
                    MessageOnDb = "Connect Database Error ==> " + er.Message.ToString(),
                    ResultOnDb = null
                };
            }

            return dataOutPut;
        }

        protected OutputOnDbModel TransSelectCommand(string sql)
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
                    MessageOnDb = "Select SQL Error ==> " + er.Message.ToString(),
                    ResultOnDb = null
                };
            }

            return dataOutPut;
        }

        protected OutputOnDbModel TransExecuteCommand(string sql)
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
                    MessageOnDb = "Execute SQL Command Error ==> " + er.Message.ToString(),
                    ResultOnDb = null
                };
            }

            return dataOutPut;
        }
        protected OutputOnDbModel TransInsertTraceLogApiExecuteCommand(string sql, TraceLogApi param)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(strConnection))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sql;

                        cmd.Parameters.AddWithValue("@RequestBody", param.RequestBody ?? "");
                        cmd.Parameters.AddWithValue("@RequestContentType", param.RequestContentType ?? "");
                        cmd.Parameters.AddWithValue("@RequestMethod", param.RequestMethod ?? "");
                        cmd.Parameters.AddWithValue("@RequestPath", param.RequestPath ?? "");
                        cmd.Parameters.AddWithValue("@RequestTimestamp", param.RequestTimestamp);
                        cmd.Parameters.AddWithValue("@RequestUri", param.RequestUri ?? "");
                        cmd.Parameters.AddWithValue("@ResponseBody", param.ResponseBody ?? "");
                        cmd.Parameters.AddWithValue("@ResponseContentType", param.ResponseContentType ?? "");
                        cmd.Parameters.AddWithValue("@ResponseStatusCode", param.ResponseStatusCode ?? "");
                        cmd.Parameters.AddWithValue("@ResponseTimestamp", param.ResponseTimestamp);

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (SqlException e)
                        {
                            var xxx = e.Message.ToString();
                            var xxx2 = xxx;
                        }

                    }
                }
            }
            catch (Exception er)
            {
                dataOutPut = new OutputOnDbModel
                {
                    StatusOnDb = false,
                    ClassOnDb = "Class Name: TransOracleModels",
                    MethodOnDb = "Method Name: TransExecuteCommand",
                    MessageOnDb = "Execute SQL Command Error ==> " + er.Message.ToString(),
                    ResultOnDb = null
                };
            }

            return dataOutPut;
        }

        protected void TransCommit()
        {
            trans.Commit();
            conTrans.Close();
            conTrans.Dispose();
        }

        protected void TransRolback()
        {
            trans.Rollback();
            conTrans.Close();
            conTrans.Dispose();
        }

        protected void TransClose()
        {
            if (conTrans != null)
            {

                conTrans.Close();
                conTrans.Dispose();
            }
        }

        protected string GetDBName()
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
