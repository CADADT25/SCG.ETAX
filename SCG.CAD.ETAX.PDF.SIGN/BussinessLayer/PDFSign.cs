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
        ConfigGlobalController configGlobalController = new ConfigGlobalController();

        List<ConfigPdfSign> configPDFSign = new List<ConfigPdfSign>();
        List<ConfigGlobal> configGlobal = new List<ConfigGlobal>();
        string pathoutput;

        public List<PDFSignModel> ReadPdfFile()
        {
            List<PDFSignModel> result = new List<PDFSignModel>();
            string pathFolder = "";
            string fileType = "*.pdf";
            List<FileInfo> listpath;
            PDFSignModel pdfSignModel = new PDFSignModel();
            DirectoryInfo directoryInfo;

            try
            {
                foreach (var config in configPDFSign)
                {
                    if (CheckRunningTime(config))
                    {
                        pathFolder = config.ConfigPdfsignInputPath;

                        directoryInfo = new DirectoryInfo(pathFolder);
                        listpath = directoryInfo.GetFiles(fileType)
                         .OrderBy(f => f.LastWriteTime).ToList();

                        foreach (var item in listpath)
                        {
                            pdfSignModel = new PDFSignModel();
                            pdfSignModel.FullPath = item.FullName;
                            pdfSignModel.FileName = Path.GetFileName(item.FullName).Replace(".pdf", "");
                            pdfSignModel.Outbound = config.ConfigPdfsignOutputPath;
                            pdfSignModel.Inbound = config.ConfigPdfsignInputPath;
                            result.Add(pdfSignModel);
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


        public bool CheckRunningTime(ConfigPdfSign config)
        {
            bool result = false;
            try
            {
                if (config.ConfigPdfsignOneTime != null &&
                        !String.IsNullOrEmpty(config.ConfigPdfsignOneTime) &&
                        Convert.ToDateTime(config.ConfigPdfsignOneTime) <= DateTime.Now)
                {
                    result = true;
                }
                if (config.ConfigPdfsignAnyTime != null &&
                    !String.IsNullOrEmpty(config.ConfigPdfsignAnyTime))
                {
                    if (config.ConfigPdfsignAnyTime.IndexOf(DateTime.Now.ToString("HH:mm")) >= 0)
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

        public void ProcessPdfSign()
        {
            string fileNameDest = "";
            string fullpath = "";
            bool resultPDFSign = false;
            string billno = "";
            int round = 0;
            string pathoutbound = "";
            string fileType = ".pdf";
            DateTime billingdate;
            try
            {
                Console.WriteLine("Start PDFSign");
                GetDataFromDataBase();

                Console.WriteLine("Start Read All PDFFile");
                var allfile = ReadPdfFile();
                Console.WriteLine("End Read All PDFFile");

                if (allfile != null && allfile.Count > 0)
                {
                    pathoutput = configGlobal.FirstOrDefault(x => x.ConfigGlobalName == "PATHBACKUPPDFFILE").ConfigGlobalValue;
                    foreach (var src in allfile)
                    {
                        round += 1;
                        Console.WriteLine("Start round : " + round);
                        if(src.FileName.IndexOf('_') > -1)
                        {
                            billno = src.FileName.Substring(8, (src.FileName.IndexOf('_')) - 8);
                        }
                        else
                        {
                            billno = src.FileName.Substring(8);
                        }
                        Console.WriteLine("billno : " + billno);

                        PdfReader reader = new PdfReader(src.FullPath);

                        Console.WriteLine("Send To Sign");
                        resultPDFSign = SendFilePDFSign();

                        Console.WriteLine("Status Sign : " + resultPDFSign.ToString());
                        Console.WriteLine("Update Status in DataBase");

                        fileNameDest = src.FileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        pathoutbound = src.Outbound;

                        var dataTran = transactionDescription.GetBilling(billno).Result.FirstOrDefault();
                        billingdate = DateTime.Now;
                        if (dataTran != null)
                        {
                            billingdate = dataTran.BillingDate ?? DateTime.Now;
                        }
                        pathoutbound += "\\" + billingdate.ToString("yyyy") + "\\" + billingdate.ToString("MM");
                        if (resultPDFSign)
                        {
                            pathoutbound += "\\Success\\";
                        }
                        else
                        {
                            pathoutbound += "\\Fail\\";
                        }
                        fullpath = pathoutbound + fileNameDest + fileType;

                        UpdateStatusAfterSignPDF(resultPDFSign, billno, fullpath, dataTran);

                        Console.WriteLine("Start Export PDF file");
                        ExportPDFAfterSign(reader, pathoutbound, fullpath);
                        Console.WriteLine("End Export PDF file");
                        reader.Close();
                        Console.WriteLine("Start Move file");
                        MoveFile(src.FullPath, src.FileName + fileType, billingdate);
                        Console.WriteLine("End Move file");

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
        
        public bool UpdateStatusAfterSignPDF(bool status, string billno, string pathfile,TransactionDescription dataTran)
        {
            bool result = false;
            try
            {

                Task<Response> res;
                if(dataTran == null)
                {
                    dataTran = new TransactionDescription();

                    dataTran.BillingNumber = Convert.ToString(billno);
                    dataTran.CreateBy = "Batch";
                    dataTran.CreateDate = DateTime.Now;
                    dataTran.TypeInput = "Batch";
                    dataTran.UpdateBy = "Batch";
                    dataTran.UpdateDate = DateTime.Now;
                    dataTran.GenerateStatus = "Waiting";
                    dataTran.PdfIndexingStatus = "Waiting";
                    dataTran.PrintStatus = "Waiting";
                    dataTran.XmlCompressStatus = "Waiting";
                    dataTran.XmlSignStatus = "Waiting";
                    dataTran.EmailSendStatus = "Waiting";
                    if (status)
                    {
                        dataTran.PdfSignDateTime = DateTime.Now;
                        dataTran.PdfSignDetail = "PDF was signed completely";
                        dataTran.PdfSignStatus = "Successful";
                        dataTran.UpdateBy = "Batch";
                        dataTran.UpdateDate = DateTime.Now;
                        dataTran.PdfSignLocation = pathfile;

                        var json = JsonSerializer.Serialize(dataTran);
                        res = transactionDescription.Insert(json);
                        if (res.Result.MESSAGE == "Insert success.")
                        {
                            result = true;
                        }
                    }
                    else
                    {
                        dataTran.PdfSignDateTime = DateTime.Now;
                        dataTran.PdfSignDetail = "PDF was signed Failed";
                        dataTran.PdfSignStatus = "Failed";
                        dataTran.UpdateBy = "Batch";
                        dataTran.UpdateDate = DateTime.Now;
                        dataTran.PdfSignLocation = pathfile;

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
                        dataTran.PdfSignDateTime = DateTime.Now;
                        dataTran.PdfSignDetail = "PDF was signed completely";
                        dataTran.PdfSignStatus = "Successful";
                        dataTran.UpdateBy = "Batch";
                        dataTran.UpdateDate = DateTime.Now;
                        dataTran.PdfSignLocation = pathfile;

                        var json = JsonSerializer.Serialize(dataTran);
                        res = transactionDescription.Update(json);
                        if (res.Result.MESSAGE == "Updated Success.")
                        {
                            result = true;
                        }
                    }
                    else
                    {
                        dataTran.PdfSignDateTime = DateTime.Now;
                        dataTran.PdfSignDetail = "PDF was signed Failed";
                        dataTran.PdfSignStatus = "Failed";
                        dataTran.UpdateBy = "Batch";
                        dataTran.UpdateDate = DateTime.Now;
                        dataTran.PdfSignLocation = pathfile;

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

        public bool ExportPDFAfterSign(PdfReader reader, string pathoutbound, string fullpath)
        {
            bool result = false;
            try
            {
                if (!Directory.Exists(pathoutbound))
                {
                    Directory.CreateDirectory(pathoutbound);
                }

                FileStream os = new FileStream(fullpath, FileMode.Create);
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
                configGlobal = configGlobalController.List().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool MoveFile(string pathinput, string filename, DateTime billingdate)
        {
            bool result = false;
            //pathinpput = @"c:\temp\MySample.txt";
            //pathoutput = @"D:\sign\backupfile\";
            string output = "";
            try
            {
                output = pathoutput + "\\" + billingdate.ToString("yyyy") + "\\" + billingdate.ToString("MM") + "\\";
                if (!File.Exists(pathinput))
                {
                    // This statement ensures that the file is created,  
                    // but the handle is not kept.  
                    using (FileStream fs = File.Create(pathinput)) { }
                }
                // Ensure that the target does not exist.  
                if (!Directory.Exists(output))
                {
                    Directory.CreateDirectory(output);
                }
                // Move the file.  
                File.Move(pathinput, output + filename);
                Console.WriteLine("{0} was moved to {1}.", pathinput, output);

                // See if the original exists now.  
                if (File.Exists(pathinput))
                {
                    Console.WriteLine("The original file still exists, which is unexpected.");
                }
                else
                {
                    Console.WriteLine("The original file no longer exists, which is expected.");
                }
                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            return result;
        }
    }
}
