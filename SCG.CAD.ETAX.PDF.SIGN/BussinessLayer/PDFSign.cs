using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.MODEL;
using System.Text.Json;
using SCG.CAD.ETAX.PDF.SIGN.Models;
using SCG.CAD.ETAX.UTILITY.Controllers;
using SCG.CAD.ETAX.UTILITY;
using SCG.CAD.ETAX.MODEL.CustomModel;

namespace SCG.CAD.ETAX.PDF.SIGN.BussinessLayer
{
    public class PDFSign
    {
        UtilityConfigPDFSignController configPDFSignController = new UtilityConfigPDFSignController();
        UtilityTransactionDescriptionController transactionDescription = new UtilityTransactionDescriptionController();
        UtilityConfigGlobalController configGlobalController = new UtilityConfigGlobalController();
        UtilityAPISignController signPDFController = new UtilityAPISignController();
        LogHelper log = new LogHelper();
        LogicToolHelper logicToolHelper = new LogicToolHelper();

        List<ConfigPdfSign> configPDFSign = new List<ConfigPdfSign>();
        List<ConfigGlobal> configGlobal = new List<ConfigGlobal>();
        List<TransactionDescription> datatransactionDescription = new List<TransactionDescription>();
        string pathoutput;
        string pathlog = @"D:\log\";
        string namepathlog = "PATHLOGFILE_PDFSIGN";

        public List<PDFSignModel> ReadPdfFile()
        {
            List<PDFSignModel> result = new List<PDFSignModel>();
            PDFSignModel pDFSignModel = new PDFSignModel();
            string pathFolder = "";
            string fileType = "*.pdf";
            List<FileInfo> listpath;
            ListFilePDF pdfDetail = new ListFilePDF();
            DirectoryInfo directoryInfo;
            string billno = "";
            string filename = "";

            try
            {
                foreach (var config in configPDFSign)
                {
                    if (CheckRunningTime(config))
                    {
                        pDFSignModel = new PDFSignModel();
                        pDFSignModel.configPdfSign = config;
                        pDFSignModel.listFilePDFs = new List<ListFilePDF>();
                        pathFolder = config.ConfigPdfsignInputPath;

                        directoryInfo = new DirectoryInfo(pathFolder);
                        listpath = directoryInfo.GetFiles(fileType)
                         .OrderBy(f => f.LastWriteTime).ToList();

                        foreach (var item in listpath)
                        {
                            filename = Path.GetFileName(item.FullName).Replace(".pdf", "");
                            if (filename.IndexOf('_') > -1)
                            {
                                billno = filename.Substring(8, (filename.IndexOf('_')) - 8);
                            }
                            else
                            {
                                billno = filename.Substring(8);
                            }
                            pdfDetail = new ListFilePDF();
                            pdfDetail.FullPath = item.FullName;
                            pdfDetail.FileName = filename;
                            pdfDetail.Outbound = config.ConfigPdfsignOutputPath;
                            pdfDetail.Inbound = config.ConfigPdfsignInputPath;
                            pdfDetail.Billno = billno;
                            pDFSignModel.listFilePDFs.Add(pdfDetail);
                        }
                        result.Add(pDFSignModel);
                    }
                }

            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
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
                result = true;
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }

        public void ProcessPdfSign()
        {
            string fileNameDest = "";
            string fullpath = "";
            APIResponseSignModel resultPDFSign = new APIResponseSignModel();
            string billno = "";
            int round = 0;
            string pathoutbound = "";
            string fileType = ".pdf";
            DateTime billingdate;
            APISendFilePDFSignModel dataSend = new APISendFilePDFSignModel();
            try
            {
                Console.WriteLine("Start PDFSign");
                log.InsertLog(pathlog, "Start PDFSign");
                GetDataFromDataBase();

                Console.WriteLine("Start Read All PDFFile");
                log.InsertLog(pathlog, "Start Read All PDFFile");
                var allfile = ReadPdfFile();
                Console.WriteLine("End Read All PDFFile");
                log.InsertLog(pathlog, "End Read All PDFFile");

                if (allfile != null && allfile.Count > 0)
                {
                    pathoutput = configGlobal.FirstOrDefault(x => x.ConfigGlobalName == "PATHBACKUPPDFFILE").ConfigGlobalValue;
                    foreach (var src in allfile)
                    {
                        if(src.listFilePDFs.Count > 0)
                        {
                            foreach(var file in src.listFilePDFs)
                            {
                                round += 1;
                                billno = file.Billno;
                                Console.WriteLine("Start round : " + round);
                                log.InsertLog(pathlog, "Start round : " + round);
                                Console.WriteLine("billno : " + billno);
                                log.InsertLog(pathlog, "billno : " + billno);

                                var dataTran = datatransactionDescription.FirstOrDefault(x => x.BillingNumber == billno);
                                if(!CheckCancelBillingOrSentRevenueDepartment(dataTran))
                                {
                                    Console.WriteLine("Prepare PDF");
                                    log.InsertLog(pathlog, "Prepare PDF");
                                    dataSend = PrepareSendPDFSign(src.configPdfSign, file.FullPath);

                                    Console.WriteLine("Send To Sign");
                                    log.InsertLog(pathlog, "Send To Sign");
                                    resultPDFSign = SendFilePDFSign(dataSend);

                                    Console.WriteLine("Status Sign : " + resultPDFSign.resultDes.ToString());
                                    log.InsertLog(pathlog, "Status Sign : " + resultPDFSign.resultDes.ToString());
                                    Console.WriteLine("Update Status in DataBase");
                                    log.InsertLog(pathlog, "Update Status in DataBase");

                                    fileNameDest = file.FileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                                    pathoutbound = file.Outbound;

                                    billingdate = DateTime.Now;
                                    if (dataTran != null)
                                    {
                                        billingdate = dataTran.BillingDate ?? DateTime.Now;
                                    }
                                    //pathoutbound += "\\" + billingdate.ToString("yyyy") + "\\" + billingdate.ToString("MM");
                                    if (resultPDFSign.resultCode == "000")
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
                                    log.InsertLog(pathlog, "Start Export PDF file");
                                    ExportPDFAfterSign(resultPDFSign, pathoutbound, fullpath);
                                    Console.WriteLine("End Export PDF file");
                                    log.InsertLog(pathlog, "End Export PDF file");
                                    Console.WriteLine("Start Move file");
                                    log.InsertLog(pathlog, "Start Move file");
                                    MoveFile(file.FullPath, file.FileName + fileType, billingdate);
                                    Console.WriteLine("End Move file");
                                    log.InsertLog(pathlog, "End Move file");
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
        }

        public APIResponseSignModel SendFilePDFSign(APISendFilePDFSignModel data)
        {
            APIResponseSignModel result = new APIResponseSignModel();
            try
            {
                var json = JsonSerializer.Serialize(data);
                result = signPDFController.SendFilePDFSign(json).Result;
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }
        
        public bool UpdateStatusAfterSignPDF(APIResponseSignModel pdfsign, string billno, string pathfile,TransactionDescription dataTran)
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
                    dataTran.DmsAttachmentFileName = null;
                    dataTran.DmsAttachmentFilePath = null;
                    if (pdfsign.resultCode.Equals("000"))
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
                    if (pdfsign.resultCode.Equals("000"))
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
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }

        public bool ExportPDFAfterSign(APIResponseSignModel pdfsign, string pathoutbound, string fullpath)
        {
            bool result = false;
            try
            {
                if (!Directory.Exists(pathoutbound))
                {
                    Directory.CreateDirectory(pathoutbound);
                }

                File.WriteAllText(fullpath, pdfsign.fileSigned);

                result = true;
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }

        public void GetDataFromDataBase()
        {
            try
            {
                configPDFSign = configPDFSignController.List().Result;
                configGlobal = configGlobalController.List().Result;
                pathlog = configGlobal.FirstOrDefault(x => x.ConfigGlobalName == namepathlog).ConfigGlobalValue;
                datatransactionDescription = transactionDescription.List().Result;
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
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
                log.InsertLog(pathlog, pathinput + " was moved to " + output);

                // See if the original exists now.  
                if (File.Exists(pathinput))
                {
                    Console.WriteLine("The original file still exists, which is unexpected.");
                    log.InsertLog(pathlog, "The original file still exists, which is unexpected.");
                }
                else
                {
                    Console.WriteLine("The original file no longer exists, which is expected.");
                    log.InsertLog(pathlog, "The original file no longer exists, which is expected.");
                }
                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
                log.InsertLog(pathlog, "Exception : " + e.ToString());
            }
            return result;
        }

        public APISendFilePDFSignModel PrepareSendPDFSign(ConfigPdfSign config, string filepath)
        {
            APISendFilePDFSignModel result = new APISendFilePDFSignModel();
            try
            {
                result.hsmName = "pse";
                result.hsmSerial = "571271:28593";
                result.slotPassword = "P@ssw0rd1";
                result.keyAlias = "NEW06391012205001173_200916150834";
                result.certSerial = "5c6e65dc1b7b9da8";
                //result.hsmSerial = config.ConfigPdfsignHsmSerial;
                //result.slotPassword = config.ConfigPdfsignHsmPassword;
                //result.keyAlias = config.ConfigPdfsignKeyAlias;
                //result.certSerial = config.ConfigPdfsignHsmSerial;
                result.signatureType = "text";
                result.coordinateUpperLeftX = 200;
                result.coordinateUpperLeftY = 700;
                result.page = "first";
                result.fontName = "Tahoma";
                result.fontSize = 8;
                result.fontType = "";
                result.fontSpace = 0;
                result.fontColor = "";
                result.imageEncode = "";
                result.reason = "";
                result.location = "";
                result.fileEncode = logicToolHelper.ConvertFileToEncodeBase64(filepath);
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }

        public bool CheckCancelBillingOrSentRevenueDepartment(TransactionDescription datatran)
        {
            bool result = false;
            try
            {
                if (datatran != null)
                {
                    if (logicToolHelper.ConvertIntToBoolean(datatran.CancelBilling) || logicToolHelper.ConvertIntToBoolean(datatran.SentRevenueDepartment))
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }
    }
}
