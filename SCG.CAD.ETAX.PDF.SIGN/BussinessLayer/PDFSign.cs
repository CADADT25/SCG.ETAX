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
using SCG.CAD.ETAX.PDF.SIGN.Models;

namespace SCG.CAD.ETAX.PDF.SIGN.BussinessLayer
{
    public class PDFSign
    {
        ConfigPDFSignController configXMLSignController = new ConfigPDFSignController();
        TransactionDescriptionController transactionDescription = new TransactionDescriptionController();
        List<ConfigPdfSign> configPDFSign = new List<ConfigPdfSign>();

        public List<PDFSignModel> ReadPdfFile()
        {
            List<PDFSignModel> result = new List<PDFSignModel>();
            string[] fullpath = new string[0];
            string pathFolder = "";
            List<string> listpath;
            PDFSignModel pdfSignModel = new PDFSignModel();

            ConfigPdfSign config = new ConfigPdfSign();
            config.ConfigPdfsignInputPath = @"D:\sign";
            config.ConfigPdfsignOutputPath = @"D:\sign";
            configPDFSign = new List<ConfigPdfSign>();
            configPDFSign.Add(config);
            try
            {
                //pathFolder = @"C:\Code_Dev\sign";
                foreach (var path in configPDFSign)
                {
                    pathFolder = path.ConfigPdfsignInputPath;
                    string fileType = "*.pdf";
                    fullpath = Directory.GetFiles(pathFolder, fileType);
                    listpath = fullpath.ToList();
                    foreach (var item in listpath)
                    {
                        pdfSignModel = new PDFSignModel();
                        pdfSignModel.FullPath = item;
                        pdfSignModel.FileName = Path.GetFileName(item).Replace(".pdf","");
                        pdfSignModel.Outbound = path.ConfigPdfsignOutputPath;
                        pdfSignModel.Inbound = path.ConfigPdfsignInputPath;
                        result.Add(pdfSignModel);
                    }
                }

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
            bool resultPDFSign = false;
            double billno = 0;
            int round = 0;
            try
            {
                Console.WriteLine("Start PDFSign");
                GetDataFromDataBase();

                Console.WriteLine("Start Read All PDFFile");
                var allfile = ReadPdfFile();
                Console.WriteLine("End Read All PDFFile");

                if (allfile != null && allfile.Count > 0)
                {
                    foreach (var src in allfile)
                    {
                        round += 1;
                        Console.WriteLine("Start round : " + round);
                        if(src.FileName.IndexOf('-') > -1)
                        {
                            billno = Convert.ToDouble(src.FileName.Substring(7, src.FileName.IndexOf('-') + 1));
                        }
                        else
                        {
                            billno = Convert.ToDouble(src.FileName.Substring(7));
                        }
                        Console.WriteLine("billno : " + billno);

                        fileNameDest = Path.GetFileName(src.FileName).Replace(".pdf", "");
                        PdfReader reader = new PdfReader(src.FullPath);

                        Console.WriteLine("Send To Sign");
                        resultPDFSign = SendFilePDFSign();

                        Console.WriteLine("Status Sign : " + resultPDFSign.ToString());
                        Console.WriteLine("Update Status in DataBase");
                        UpdateStatusAfterSignPDF(resultPDFSign, billno);

                        Console.WriteLine("Start Export PDF file");
                        fileNameDest = src.FileName + "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        ExportPDFAfterSign(resultPDFSign, reader, src.Outbound, fileNameDest);
                        Console.WriteLine("End Export PDF file");

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
        
        public bool UpdateStatusAfterSignPDF(bool status, double billno)
        {
            bool result = false;
            try
            {

                Task<Response> res;
                var dataTran = transactionDescription.GetBilling(Convert.ToInt32(billno)).Result.FirstOrDefault();
                if(dataTran == null)
                {
                    dataTran = new TransactionDescription();

                    dataTran.BillingNumber = Convert.ToString(billno);
                    dataTran.CreateBy = "Batch";
                    dataTran.CreateDate = DateTime.Now;
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

        public bool ExportPDFAfterSign(bool resultPDFSign, PdfReader reader, string pathoutbound, string filename)
        {
            bool result = false;
            string fileType = ".pdf";
            try
            {
                if (resultPDFSign)
                {
                    pathoutbound += "/Success/";
                }
                else
                {
                    pathoutbound += "/Fail/";
                }
                FileStream os = new FileStream(pathoutbound + filename + fileType, FileMode.Create);
                PdfStamper stamper = PdfStamper.CreateSignature(reader, os, '\0');

                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public void GetDataFromDataBase()
        {
            try
            {
                configPDFSign = configXMLSignController.List().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
