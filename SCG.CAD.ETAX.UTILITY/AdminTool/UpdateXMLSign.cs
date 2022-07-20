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
    public class UpdateXMLSign
    {
        UtilityTransactionDescriptionController transactionDescriptionController = new UtilityTransactionDescriptionController();
        UtilityConfigXMLSignController configXMLSignController = new UtilityConfigXMLSignController();
        AdminToolHelper adminToolHelper = new AdminToolHelper();

        List<ConfigXmlSign> configXmlSign = new List<ConfigXmlSign>();

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
                foreach (var item in configXmlSign)
                {
                    files = adminToolHelper.GetFileInFolder(item.ConfigXmlsignOutputPath + "//Success", filetype);
                    foreach (var file in files)
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
                        if (transactionDescription.Count > 0)
                        {
                            dataTran = transactionDescription.FirstOrDefault(x => x.XmlSignStatus == "Waiting");
                        }

                        if (dataTran != null)
                        {
                            dataTran.XmlSignDateTime = DateTime.Now;
                            dataTran.XmlSignDetail = "XML was signed completely";
                            dataTran.XmlSignStatus = "Successful";
                            dataTran.UpdateBy = "BatchUpdate";
                            dataTran.UpdateDate = DateTime.Now;
                            dataTran.XmlSignLocation = file;

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
                configXmlSign = configXMLSignController.List().Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
