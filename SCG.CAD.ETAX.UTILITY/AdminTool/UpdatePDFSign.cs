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
    public class UpdatePDFSign
    {
        UtilityTransactionDescriptionController transactionDescriptionController = new UtilityTransactionDescriptionController();
        UtilityConfigPDFSignController configPDFSignController = new UtilityConfigPDFSignController();
        AdminToolHelper adminToolHelper = new AdminToolHelper();

        List<ConfigPdfSign> configPdfSign = new List<ConfigPdfSign>();

        public void AutoUpdatePDFSignStatus()
        {
            List<string> files = new List<string>();
            List<TransactionDescription> transactionDescription = new List<TransactionDescription>();
            TransactionDescription dataTran = new TransactionDescription();

            string filetype = "*.pdf";
            string billno;
            string filename;
            string json;
            bool resultUpdate;
            try
            {
                GetConfig();
                foreach(var item in configPdfSign)
                {
                    files = adminToolHelper.GetFileInFolder(item.ConfigPdfsignOutputPath + "//Success", filetype);
                    foreach(var file in files)
                    {
                        filename = Path.GetFileName(file).Replace(filetype, "");
                        if (file.IndexOf('_') > -1)
                        {
                            billno = filename.Substring(8, (filename.IndexOf('_')) - 8);
                        }
                        else
                        {
                            billno = filename.Substring(8);
                        }

                        transactionDescription = adminToolHelper.GetBillingTransaction(billno);
                        if(transactionDescription.Count > 0)
                        {
                            dataTran = transactionDescription.FirstOrDefault(x => x.PdfSignStatus == "Waiting");
                        }
                        else
                        {
                            dataTran = new TransactionDescription();

                            dataTran.BillingNumber = Convert.ToString(billno);
                            dataTran.CreateBy = "BatchUpdate";
                            dataTran.CreateDate = DateTime.Now;
                            dataTran.TypeInput = "BatchUpdate";
                            dataTran.UpdateBy = "BatchUpdate";
                            dataTran.UpdateDate = DateTime.Now;
                            dataTran.GenerateStatus = "Waiting";
                            dataTran.PdfIndexingStatus = "Waiting";
                            dataTran.PrintStatus = "Waiting";
                            dataTran.XmlCompressStatus = "Waiting";
                            dataTran.XmlSignStatus = "Waiting";
                            dataTran.EmailSendStatus = "Waiting";
                            dataTran.DmsAttachmentFileName = null;
                            dataTran.DmsAttachmentFilePath = null;

                        }

                        if(dataTran != null)
                        {
                            dataTran.PdfSignDateTime = DateTime.Now;
                            dataTran.PdfSignDetail = "PDF was signed completely";
                            dataTran.PdfSignStatus = "Successful";
                            dataTran.UpdateBy = "BatchUpdate";
                            dataTran.UpdateDate = DateTime.Now;
                            dataTran.PdfSignLocation = file;

                            json = JsonSerializer.Serialize(dataTran);
                            resultUpdate = adminToolHelper.UpdateTransaction(json);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetConfig()
        {
            List<ConfigPdfSign> result = new List<ConfigPdfSign>();
            try
            {
                configPdfSign = configPDFSignController.List().Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
