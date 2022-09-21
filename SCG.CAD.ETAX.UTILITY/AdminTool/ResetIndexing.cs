using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY.Controllers;

namespace SCG.CAD.ETAX.UTILITY.AdminTool
{
    public class ResetIndexing
    {
        UtilityTransactionDescriptionController transactionDescriptionController = new UtilityTransactionDescriptionController();
        AdminToolHelper adminToolHelper = new AdminToolHelper();

        public bool ResetStatusIndexingByRecord(string billno, string updateby)
        {
            bool result = false;
            TransactionDescription transactionDescription = new TransactionDescription();
            string json;
            try
            {
                transactionDescription = adminToolHelper.GetBillingTransaction(billno).FirstOrDefault();
                if (transactionDescription != null)
                {
                    if (transactionDescription.PdfIndexingStatus != "Waiting")
                    {
                        transactionDescription.PdfIndexingStatus = "Waiting";
                        transactionDescription.PdfIndexingDetail = "Reset Status";
                        transactionDescription.PdfIndexingDateTime = DateTime.Now;
                        transactionDescription.UpdateBy = updateby;
                        transactionDescription.UpdateDate = DateTime.Now;

                        json = JsonSerializer.Serialize(transactionDescription);
                        result = adminToolHelper.UpdateTransaction(json);
                    }
                    else
                    {
                        result = true;
                    }
                }
                else
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool ResetStatusIndexingByMutipleRecords(List<string> listbillno, string updateby)
        {
            bool result = false;
            TransactionDescription transactionDescription = new TransactionDescription();
            List<TransactionDescription> listtransactionDescription = new List<TransactionDescription>();
            List<TransactionDescription> updatetransactionDescription = new List<TransactionDescription>();
            string json;
            try
            {
                listtransactionDescription = adminToolHelper.ListTransaction();
                foreach (var billno in listbillno)
                {
                    transactionDescription = listtransactionDescription.FirstOrDefault(x => x.BillingNumber == billno);
                    if (transactionDescription != null)
                    {
                        if (transactionDescription.PdfIndexingStatus != "Waiting")
                        {
                            transactionDescription.PdfIndexingStatus = "Waiting";
                            transactionDescription.PdfIndexingDetail = "Reset Status";
                            transactionDescription.PdfIndexingDateTime = DateTime.Now;
                            transactionDescription.UpdateBy = updateby;
                            transactionDescription.UpdateDate = DateTime.Now;
                            updatetransactionDescription.Add(transactionDescription);

                        }
                    }
                }

                if (updatetransactionDescription.Count > 0)
                {
                    json = JsonSerializer.Serialize(updatetransactionDescription);
                    result = adminToolHelper.UpdateListTransaction(json);
                }
                else
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
