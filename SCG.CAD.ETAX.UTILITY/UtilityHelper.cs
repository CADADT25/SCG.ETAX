using SCG.CAD.ETAX.MODEL.etaxModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.UTILITY
{
    public class UtilityHelper
    {
        public static List<string> GetFileDirectories(string path)
        {
            List<string> files = new List<string>();
            try
            {
                foreach (string f in Directory.GetFiles(path))
                {
                    files.Add(f);
                }
                foreach (string d in Directory.GetDirectories(path))
                {
                    files.AddRange(GetFileDirectories(d));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return files;
        }
        public static List<string> GetDirectories(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            if (searchOption == SearchOption.TopDirectoryOnly)
                return Directory.GetDirectories(path, searchPattern).ToList();

            var directories = new List<string>(GetDirectories(path, searchPattern));

            for (var i = 0; i < directories.Count; i++)
            {
                directories.AddRange(GetDirectories(directories[i], searchPattern));
            }


            return directories;
        }

        private static List<string> GetDirectories(string path, string searchPattern)
        {
            try
            {
                return Directory.GetDirectories(path, searchPattern).ToList();
            }
            catch (UnauthorizedAccessException)
            {
                return new List<string>();
            }
        }
        public static string EncryptString(string plainText, string key = "")
        {
            if (string.IsNullOrEmpty(key))
                key = "C5A8E9AD259043C2AE4758AEB5A67E2C";
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptString(string cipherText, string key = "")
        {
            if (string.IsNullOrEmpty(key))
                key = "C5A8E9AD259043C2AE4758AEB5A67E2C";
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
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
                if (statusCode.ToLower() == Variable.RequestStatusCode_WaitManager)
                {
                    return "Wait Manager";
                }
                if (statusCode.ToLower() == Variable.RequestStatusCode_WaitOfficer)
                {
                    return "Wait Officer";
                }
                if (statusCode.ToLower() == Variable.RequestStatusCode_Reject)
                {
                    return "Reject";
                }
                if (statusCode.ToLower() == Variable.RequestStatusCode_Complete)
                {
                    return "Complete";
                }
                if (statusCode.ToLower() == Variable.RequestStatusCode_Cancel)
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
                if (actionCode.ToLower() == Variable.RequestActionCode_Delete)
                {
                    return "Delete";
                }
                if (actionCode.ToLower() == Variable.RequestActionCode_Undelete)
                {
                    return "Undelete";
                }
                //if (actionCode.ToLower() == "unzip")
                //{
                //    return "Unzip";
                //}
                if (actionCode.ToLower() == Variable.RequestActionCode_ReSignNewTrans)
                {
                    return "Re-Sign PDF XML (Workaround)";
                }    
                if (actionCode.ToLower() == Variable.RequestActionCode_ReSignNewCert)
                {
                    return "Re-Sign PDF XML (New cert)";
                }

            }
            return actionCode;
        }
        public static string EncodeSpecialChar(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace("+", "pmpPluSpmp");
                str = str.Replace("&", "pmpAnDpmp");
                str = str.Replace("#", "pmpCharPpmp");
            }
            return str;
        }
        public static string DecodeSpecialChar(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace("pmpPluSpmp", "+");
                str = str.Replace("pmpAnDpmp", "&");
                str = str.Replace("pmpCharPpmp", "#");
            }
            return str;
        }
    }
}
