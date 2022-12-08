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
        string batchname = "SCG.CAD.ETAX.PDF.SIGN";

        public PDFSignModel ReadPdfFile(ConfigPdfSign config)
        {
            PDFSignModel result = new PDFSignModel();
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
                pDFSignModel = new PDFSignModel();
                pDFSignModel.configPdfSign = config;
                pDFSignModel.listFilePDFs = new List<ListFilePDF>();
                pathFolder = config.ConfigPdfsignInputPath;

                if (Directory.Exists(pathFolder))
                {
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
                    result = pDFSignModel;
                    Console.WriteLine("Path Found PDF : " + pDFSignModel.listFilePDFs.Count + " files");
                    log.InsertLog(pathlog, "Path Found PDF : " + pDFSignModel.listFilePDFs.Count + " files");
                }
                else
                {
                    Console.WriteLine("Path Not Found: " + pathFolder);
                    log.InsertLog(pathlog, "Path Not Found: " + pathFolder);
                }
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
            DateTime nexttime;
            try
            {
                Console.WriteLine("Start PDFSign");
                log.InsertLog(pathlog, "Start PDFSign");
                GetDataFromDataBase();

                Console.WriteLine("Start Read All PDFFile");
                log.InsertLog(pathlog, "Start Read All PDFFile");


                foreach (var config in configPDFSign)
                {
                    //if (logicToolHelper.CheckRunTime(config.ConfigPdfsignNextTime))
                    //{

                    var allfile = ReadPdfFile(config);

                    if (allfile != null && allfile.listFilePDFs != null)
                    {
                        pathoutput = configGlobal.FirstOrDefault(x => x.ConfigGlobalName == "PATHBACKUPPDFFILE").ConfigGlobalValue;
                        if (allfile.listFilePDFs.Count > 0)
                        {
                            foreach (var file in allfile.listFilePDFs)
                            {
                                round += 1;
                                billno = file.Billno;
                                Console.WriteLine("Start round : " + round);
                                log.InsertLog(pathlog, "Start round : " + round);
                                Console.WriteLine("billno : " + billno);
                                log.InsertLog(pathlog, "billno : " + billno);

                                var dataTran = datatransactionDescription.FirstOrDefault(x => x.BillingNumber == billno);
                                if (!CheckCancelBillingOrSentRevenueDepartment(dataTran))
                                {
                                    Console.WriteLine("Prepare PDF");
                                    log.InsertLog(pathlog, "Prepare PDF");
                                    dataSend = PrepareSendPDFSign(allfile.configPdfSign, file.FullPath);

                                    if(dataSend != null)
                                    {
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
                                        if (resultPDFSign.resultCode == "000")
                                        {
                                            pathoutbound += "\\Success\\";
                                        }
                                        else
                                        {
                                            pathoutbound += "\\Fail\\";
                                        }
                                        pathoutbound += billingdate.ToString("yyyy") + "\\" + billingdate.ToString("MM") + "\\";
                                        fullpath = pathoutbound + fileNameDest + fileType;

                                        UpdateStatusAfterSignPDF(resultPDFSign, billno, fullpath, dataTran);

                                        Console.WriteLine("Start Export PDF file");
                                        log.InsertLog(pathlog, "Start Export PDF file");
                                        if (resultPDFSign.resultCode == "000")
                                        {
                                            ExportPDFAfterSign(resultPDFSign.fileSigned, pathoutbound, fullpath);
                                        }
                                        else
                                        {
                                            ExportPDFAfterSign(dataSend.fileEncode, pathoutbound, fullpath);
                                        }
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
                    //nexttime = logicToolHelper.SetNextRunTime(config.ConfigPdfsignAnyTime, config.ConfigPdfsignOneTime, batchname, config.ConfigPdfsignNo);
                    //Console.WriteLine("Set NextTime : " + nexttime);
                    //log.InsertLog(pathlog, "Set NextTime : " + nexttime);
                    //}
                }

                Console.WriteLine("End Read All PDFFile");
                log.InsertLog(pathlog, "End Read All PDFFile");
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
                var res = signPDFController.SendFilePDFSign(data).Result;
                result = JsonSerializer.Deserialize<APIResponseSignModel>(res.OUTPUT_DATA.ToString());
                //result.fileSigned = data.fileEncode;
                //result.resultCode = "000";
                //result.resultDes = "Success";
                log.InsertLog(pathlog, "Result : " + result.resultDes);

            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }

        public bool UpdateStatusAfterSignPDF(APIResponseSignModel pdfsign, string billno, string pathfile, TransactionDescription dataTran)
        {
            bool result = false;
            try
            {
                Task<Response> res;
                if (dataTran == null)
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
                        dataTran.PdfSignDetail = pdfsign.resultDes;
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

        public bool ExportPDFAfterSign(string filepdf, string pathoutbound, string fullpath)
        {
            bool result = false;
            try
            {
                if (!Directory.Exists(pathoutbound))
                {
                    Directory.CreateDirectory(pathoutbound);
                }
                byte[] bytes = Convert.FromBase64String(filepdf);
                FileStream stream = new FileStream(fullpath, FileMode.CreateNew);
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(bytes, 0, bytes.Length);
                writer.Close();

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
            string output = "";
            try
            {
                output = pathoutput + "\\" + billingdate.ToString("yyyy") + "\\" + billingdate.ToString("MM") + "\\";
                if (!File.Exists(pathinput))
                { 
                    using (FileStream fs = File.Create(pathinput)) { }
                }
                // Ensure that the target does not exist.  
                if (!Directory.Exists(output))
                {
                    Directory.CreateDirectory(output);
                }

                // See if the original exists now.  
                if (File.Exists(output + filename))
                {
                    File.Delete(output + filename);
                }
                // Move the file.  
                File.Move(pathinput, output + filename);
                Console.WriteLine("{0} was moved to {1}.", pathinput, output);
                log.InsertLog(pathlog, pathinput + " was moved to " + output);

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
            EncodeHelper encodeHelper = new EncodeHelper();
            int pdfsignUlx;
            int pdfsignUly;
            int fontSize;
            try
            {
                //result.hsmName = "pse";
                //result.hsmSerial = "571271:28593";
                //result.slotPassword = "P@ssw0rd1";
                //result.keyAlias = "NEW06391012205001173_200916150834";
                //result.certSerial = "5c6e65dc1b7b9da8";
                //result.coordinateUpperLeftX = 200;
                //result.coordinateUpperLeftY = 700;
                //result.page = "first";
                //result.fontName = "Tahoma";
                //result.fontSize = 8;
                //result.signatureType = "text";
                result.environment = "0";
                result.hsmName = config.ConfigPdfsignHsmModule;
                result.hsmSerial = config.ConfigPdfsignHsmSerial;
                result.slotPassword = encodeHelper.Base64Decode(config.ConfigPdfsignHsmPassword);
                result.keyAlias = config.ConfigPdfsignKeyAlias;
                result.certSerial = config.ConfigPdfsignCertificateSerial;
                result.signatureType = "text";
                result.coordinateUpperLeftX = 200;
                result.coordinateUpperLeftY = 700;
                result.fontSize = 8;
                if(int.TryParse(config.ConfigPdfsignUlx, out pdfsignUlx))
                {
                    result.coordinateUpperLeftX = pdfsignUlx;
                }
                if(int.TryParse(config.ConfigPdfsignUly, out pdfsignUly))
                {
                    result.coordinateUpperLeftY = pdfsignUly;
                }
                if(int.TryParse(config.ConfigPdfsignFontSize, out fontSize))
                {
                    result.fontSize = fontSize;
                }
                result.page = config.ConfigPdfsignPage;
                result.fontName = config.ConfigPdfsignFontName;
                //result.fontType = "";
                //result.fontSpace = 0;
                //result.fontColor = "";
                //result.imageEncode = "";
                //result.reason = "";
                //result.location = "";
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
