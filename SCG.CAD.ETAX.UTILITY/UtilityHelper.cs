using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.UTILITY
{
    public class UtilityHelper
    {
        public static List<T> ConvertDataTableToModel<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            object o = new { };
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        try
                        {
                            pro.SetValue(obj, dr[column.ColumnName], null);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return obj;
        }

        public static string SetError(string msg, string str)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                return msg + " <br> " + str;
            }
            else
            {
                return str;
            }
        }

        public static string GetStatusName(string statusCode)
        {
            if (!string.IsNullOrEmpty(statusCode))
            {
                if (statusCode.ToLower() == "wait_manager")
                {
                    return "Wait Manager";
                }
                if (statusCode.ToLower() == "wait_officer")
                {
                    return "Wait Officer";
                }
                if (statusCode.ToLower() == "reject")
                {
                    return "Reject";
                }
                if (statusCode.ToLower() == "complete")
                {
                    return "Complete";
                }
                if (statusCode.ToLower() == "cancel")
                {
                    return "Cancel";
                }
            }
            return statusCode;
        }

        public static string GetActionName(string actionCode)
        {
            if (!string.IsNullOrEmpty(actionCode))
            {
                if (actionCode.ToLower() == "delete")
                {
                    return "Delete";
                }
                if (actionCode.ToLower() == "undelete")
                {
                    return "Undelete";
                }
                if (actionCode.ToLower() == "unzip")
                {
                    return "Unzip";
                }
                
            }
            return actionCode;
        }
    }
}
