using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCG.CAD.ETAX.XML.SIGN.Controller;
using SCG.CAD.ETAX.MODEL.etaxModel;
using System.Text.Json;
using SCG.CAD.ETAX.MODEL;

namespace SCG.CAD.ETAX.XML.SIGN.BussinessLayer
{
    public class XMLSign
    {
        public string[] ReadXmlFile()
        {
            string[] result = new string[0];
            try
            {
                StringBuilder sb = new StringBuilder();
                string pathFolder = @"D:\sign";
                string fileType = "*.xml";
                result = Directory.GetFiles(pathFolder, fileType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public void PdfSign()
        {
            string folderDest = @"D:/";
            string fileNameDest = "";
            string fileType = ".xml";
            bool resultXMLSign = false;
            int billno = 0;
            try
            {
                var allfile = ReadXmlFile();
                if (allfile != null && allfile.Length > 0)
                {
                    foreach (string src in allfile)
                    {
                        fileNameDest = Path.GetFileName(src).Replace(".xml", "");
                        PdfReader reader = new PdfReader(src);
                        FileStream os = new FileStream(folderDest + fileNameDest + "_sign" + fileType, FileMode.Create);
                        PdfStamper stamper = PdfStamper.CreateSignature(reader, os, '\0');
                        resultXMLSign = SendFilePDFSign();

                        UpdateStatusAfterSignXML(resultXMLSign, billno);

                        ExportXMLAfterSign(resultXMLSign);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SendFilePDFSign()
        {
            bool result = false;
            try
            {
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        
        public bool UpdateStatusAfterSignXML(bool status, int billno)
        {
            bool result = false;
            string jsondata = "";
            try
            {
                Task<Response> res;
                TransactionDescriptionController transactionDescription = new TransactionDescriptionController();
                var dataTran = transactionDescription.GetBilling(billno).Result.FirstOrDefault();
                if (status)
                {
                    dataTran.XmlSignDateTime = DateTime.Now;
                    dataTran.XmlSignDetail = "XML was signed completely";
                    dataTran.XmlSignStatus = "Successful";
                    dataTran.UpdateBy = "Batch";
                    dataTran.UpdateDate = DateTime.Now;

                    var json = JsonSerializer.Serialize(dataTran);
                    res = transactionDescription.Update(json);
                    if (res.Result.MESSAGE == "Updated Success.")
                    {
                        result = true;
                    }
                }
                else
                {
                    dataTran.XmlSignDateTime = DateTime.Now;
                    dataTran.XmlSignDetail = "XML was signed Failed";
                    dataTran.XmlSignStatus = "Failed";
                    dataTran.UpdateBy = "Batch";
                    dataTran.UpdateDate = DateTime.Now;

                    var json = JsonSerializer.Serialize(dataTran);
                    res = transactionDescription.Update(json);
                    if (res.Result.MESSAGE == "Updated Success.")
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public void ExportXMLAfterSign(bool resultPDFSign)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
