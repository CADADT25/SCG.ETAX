using SCG.CAD.ETAX.MODEL.etaxModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCG.CAD.ETAX.UTILITY.Controllers;
using SCG.CAD.ETAX.MODEL;

namespace SCG.CAD.ETAX.UTILITY
{
    public class ControllerHelper
    {
        UtilityTransactionDescriptionController transactionDescriptionController = new UtilityTransactionDescriptionController();

        public List<TransactionDescription> List()
        {
            List<TransactionDescription> tran = new List<TransactionDescription>();
            try
            {
                tran = transactionDescriptionController.List().Result;
            }
            catch (Exception ex)
            {
                throw;
            }
            return tran;
        }

        public List<TransactionDescription> GetBilling(string billno)
        {
            List<TransactionDescription> tran = new List<TransactionDescription>();
            try
            {
                tran = transactionDescriptionController.GetBilling(billno).Result;
            }
            catch (Exception ex)
            {
                throw;
            }
            return tran;
        }

        public List<TransactionDescription> Detail()
        {
            List<TransactionDescription> tran = new List<TransactionDescription>();
            try
            {
                tran = transactionDescriptionController.Detail().Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            return tran;
        }
        public Response Insert(string jsonString)
        {
            Response task = new Response();
            try
            {
                task = transactionDescriptionController.Insert(jsonString).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            return task;
        }
        public async Task<Response> Update(string jsonString)
        {
            Response task = new Response();
            try
            {
                task = transactionDescriptionController.Update(jsonString).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            return task;
        }
        public async Task<Response> Delete(string jsonString)
        {
            Response task = new Response();
            try
            {
                task = transactionDescriptionController.Delete(jsonString).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            return task;
        }
    }
}
