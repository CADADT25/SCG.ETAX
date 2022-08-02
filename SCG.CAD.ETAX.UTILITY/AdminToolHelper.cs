using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.UTILITY
{
    public class AdminToolHelper
    {
        UtilityTransactionDescriptionController transactionDescriptionController = new UtilityTransactionDescriptionController();
        UtilityConfigGlobalController configGlobalController = new UtilityConfigGlobalController();

        public List<TransactionDescription> GetBillingTransaction(string billno)
        {
            List<TransactionDescription> tran = new List<TransactionDescription>();
            try
            {
                tran = transactionDescriptionController.GetBilling(billno).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tran;
        }

        public bool UpdateTransaction(string jsonString)
        {
            Task<Response> res;
            try
            {
                res = transactionDescriptionController.Update(jsonString);
                if (res.Result.MESSAGE == "Updated Success.")
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        public bool UpdateListTransaction(string jsonString)
        {
            Task<Response> res;
            try
            {
                res = transactionDescriptionController.Update(jsonString);
                if (res.Result.MESSAGE == "Updated Success.")
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        public List<TransactionDescription> ListTransaction()
        {
            List<TransactionDescription> tran = new List<TransactionDescription>();
            try
            {
                tran = transactionDescriptionController.List().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tran;
        }

        public List<ConfigGlobal> ListConfigGlobal()
        {
            List<ConfigGlobal> tran = new List<ConfigGlobal>();
            try
            {
                tran = configGlobalController.List().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tran;
        }

        public List<string> GetFileInFolder(string pathFolder, string fileType)
        {
            List<string> result = new List<string>();
            try
            {
                var directoryInfo = new DirectoryInfo(pathFolder);
                var listpath = directoryInfo.GetFiles(fileType)
                         .OrderBy(f => f.LastWriteTime).ToList();
                result = listpath.Select(x => x.FullName).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
