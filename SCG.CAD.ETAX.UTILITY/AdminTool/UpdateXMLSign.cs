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

        public void AutoUpdateXMLSignStatus()
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


        public bool UpdateXMLSignStatusByRecord(string billno, string updateby)
        {
            bool result = false;
            string json;
            TransactionDescription dataTran = new TransactionDescription();
            try
            {
                GetConfig();
                dataTran = PrepareDataUpdate(billno, updateby);
                if (dataTran != null)
                {
                    json = JsonSerializer.Serialize(dataTran);
                    result = adminToolHelper.UpdateTransaction(json);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool UpdateXMLSignStatusByMutipleRecords(List<string> listbillno, string updateby)
        {
            bool result = false;
            string json;
            TransactionDescription dataTran = new TransactionDescription();
            List<TransactionDescription> listdataTran = new List<TransactionDescription>();
            try
            {
                GetConfig();
                foreach (var billno in listbillno)
                {
                    if (!string.IsNullOrEmpty(billno))
                    {
                        dataTran = new TransactionDescription();
                        dataTran = PrepareDataUpdate(billno, updateby);
                        if (dataTran != null)
                        {
                            listdataTran.Add(dataTran);
                        }
                    }
                }
                if (listdataTran.Count > 0)
                {
                    json = JsonSerializer.Serialize(listdataTran);
                    result = adminToolHelper.UpdateListTransaction(json);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public TransactionDescription PrepareDataUpdate(string billno, string updateby)
        {
            List<string> files = new List<string>();
            List<TransactionDescription> transactionDescription = new List<TransactionDescription>();
            TransactionDescription dataTran = null;
            ConfigXmlSign config = new ConfigXmlSign();

            string filetype = "*.xml";
            string billnofile;
            string filename;
            string json;
            bool resultUpdate;
            try
            {
                transactionDescription = adminToolHelper.GetBillingTransaction(billno);
                if (transactionDescription != null && transactionDescription.Count > 0)
                {
                    config = configXmlSign.FirstOrDefault(x => x.ConfigXmlsignCompanycode == transactionDescription[0].CompanyCode);
                    files = adminToolHelper.GetFileInFolder(config.ConfigXmlsignOutputPath + "//Success", filetype);
                    var found = files.Where(x => x.ToString().Contains(billno)).ToList();
                    if (found.Count > 0)
                    {
                        transactionDescription[0].XmlSignDateTime = DateTime.Now;
                        transactionDescription[0].XmlSignDetail = "XML was signed completely";
                        transactionDescription[0].XmlSignStatus = "Successful";
                        transactionDescription[0].UpdateBy = updateby;
                        transactionDescription[0].UpdateDate = DateTime.Now;
                        transactionDescription[0].XmlSignLocation = found.ToString();
                        dataTran = transactionDescription[0];
                    }

                    //foreach (var file in files)
                    //{
                    //    filename = Path.GetFileName(file).Replace(filetype, "");
                    //    if (file.IndexOf('_') > -1)
                    //    {
                    //        billnofile = filename.Substring(8, (filename.IndexOf('_')) - 8);
                    //    }
                    //    else
                    //    {
                    //        billnofile = filename.Substring(8);
                    //    }

                    //    if (billno == billnofile)
                    //    {
                    //        transactionDescription[0].XmlSignDateTime = DateTime.Now;
                    //        transactionDescription[0].XmlSignDetail = "XML was signed completely";
                    //        transactionDescription[0].XmlSignStatus = "Successful";
                    //        transactionDescription[0].UpdateBy = updateby;
                    //        transactionDescription[0].UpdateDate = DateTime.Now;
                    //        transactionDescription[0].XmlSignLocation = file;
                    //        dataTran = transactionDescription[0];
                    //        break;
                    //    }

                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTran;
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
