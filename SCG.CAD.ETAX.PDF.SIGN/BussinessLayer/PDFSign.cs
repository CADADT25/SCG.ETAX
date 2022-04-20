using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCG.CAD.ETAX.PDF.SIGN.Controller;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.MODEL;
using System.Text.Json;

namespace SCG.CAD.ETAX.PDF.SIGN.BussinessLayer
{
    public class PDFSign
    {
        public string[] ReadPdfFile()
        {
            string[] result = new string[0];
            try
            {
                StringBuilder sb = new StringBuilder();
                string pathFolder = @"D:\sign";
                string fileType = "*.pdf";
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
            string fileType = ".pdf";
            bool resultPDFSign = false;
            int billno = 0;
            int billyear = 0;
            try
            {
                var allfile = ReadPdfFile();
                if (allfile != null && allfile.Length > 0)
                {
                    foreach (string src in allfile)
                    {
                        fileNameDest = Path.GetFileName(src).Replace(".pdf", "");
                        PdfReader reader = new PdfReader(src);
                        FileStream os = new FileStream(folderDest + fileNameDest + "_sign" + fileType, FileMode.Create);
                        PdfStamper stamper = PdfStamper.CreateSignature(reader, os, '\0');
                        resultPDFSign = SendFilePDFSign();

                        UpdateStatusAfterSignPDF(resultPDFSign, billno, billyear);

                        ExportPDFAfterSign(resultPDFSign);

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
        
        public bool UpdateStatusAfterSignPDF(bool status, int billno, int billyear)
        {
            bool result = false;
            try
            {

                Task<Response> res;
                TransactionDescriptionController transactionDescription = new TransactionDescriptionController();
                var dataTran = transactionDescription.GetBilling(billno).Result.FirstOrDefault();
                if(dataTran == null)
                {
                    dataTran = new TransactionDescription();

                    dataTran.BillingNumber = billno;
                    dataTran.BillingYear = billyear;
                    dataTran.CreateBy = "Batch";
                    dataTran.CreateDate = DateTime.Now;
                    dataTran.SourceName = "";//
                    dataTran.TypeInput = "Batch";
                    dataTran.UpdateBy = "Batch";
                    dataTran.UpdateDate = DateTime.Now;
                    if (status)
                    {
                        dataTran.XmlSignDateTime = DateTime.Now;
                        dataTran.XmlSignDetail = "PDF was signed completely";
                        dataTran.XmlSignStatus = "Successful";
                        dataTran.UpdateBy = "Batch";
                        dataTran.UpdateDate = DateTime.Now;

                        var json = JsonSerializer.Serialize(dataTran);
                        res = transactionDescription.Insert(json);
                        if (res.Result.MESSAGE == "Insert success.")
                        {
                            result = true;
                        }
                    }
                    else
                    {
                        dataTran.XmlSignDateTime = DateTime.Now;
                        dataTran.XmlSignDetail = "PDF was signed Failed";
                        dataTran.XmlSignStatus = "Failed";
                        dataTran.UpdateBy = "Batch";
                        dataTran.UpdateDate = DateTime.Now;

                        var json = JsonSerializer.Serialize(dataTran);
                        res = transactionDescription.Insert(json);
                        if (res.Result.MESSAGE == "Insert success.")
                        {
                            result = true;
                        }
                    }
                }
                else
                {
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool InsertDataAfterSignPDF(bool status, int billno)
        {
            bool result = false;
            string jsondata = "";
            try
            {

                TransactionDescriptionController transactiondescriptioncontroller = new TransactionDescriptionController();
                if (status)
                {

                }
                else
                {

                }
                var jsonresult = transactiondescriptioncontroller.Insert(jsondata);
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public void ExportPDFAfterSign(bool resultPDFSign)
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
