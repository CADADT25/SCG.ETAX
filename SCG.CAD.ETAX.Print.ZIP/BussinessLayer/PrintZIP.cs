using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.XML.PRINT.ZIP.Models;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SCG.CAD.ETAX.UTILITY.Controllers;
using SCG.CAD.ETAX.UTILITY;

namespace SCG.CAD.ETAX.PRINT.ZIP.BussinessLayer
{
    public class PrintZIP
    {
        ConfigMftsCompressPrintSettingController configMftsCompressPrintSettingController = new ConfigMftsCompressPrintSettingController();
        OutputSearchPrintingController outputSearchPrintingController = new OutputSearchPrintingController();
        TransactionDescriptionController transactionDescriptionController = new TransactionDescriptionController();
        ConfigGlobalController configGlobalController = new ConfigGlobalController();
        LogHelper log = new LogHelper();

        List<ConfigMftsCompressPrintSetting> configPrintSetting = new List<ConfigMftsCompressPrintSetting>();
        List<TransactionDescription> transactionDescription = new List<TransactionDescription>();
        List<ConfigGlobal> configGlobal = new List<ConfigGlobal>();
        string pathoutput;
        string outputsearchprintingno;
        string pathlog = @"C:\log\";
        string namepathlog = "PATHLOGFILE_PRINTZIP";

        public void ProcessPrintzip()
        {
            string zipName = "";
            try
            {
                Console.WriteLine("Start PrintZip");
                log.InsertLog(pathlog, "Start PrintZip");
                GetDataFromDataBase();
                GetListTransactionDescription();
                Console.WriteLine("Start Read All PDF");
                log.InsertLog(pathlog, "Start Read All PDF");
                var dataallCompany = ReadFile();
                Console.WriteLine("End Read All PDF");
                log.InsertLog(pathlog, "End Read All PDF");
                pathoutput = configGlobal.FirstOrDefault(x => x.ConfigGlobalName == "PATHBACKUPPRINTZIPFILE").ConfigGlobalValue;
                foreach (var data in dataallCompany)
                {
                    Console.WriteLine("Start Zip Company : " + data.CompanyCode);
                    log.InsertLog(pathlog, "Start Zip Company : " + data.CompanyCode);
                    zipName = data.CompanyCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".7z";
                    var resultZipFile = Zipfile(data, zipName);

                    Console.WriteLine("Insert Data OutputSearchPrinting Company : " + data.CompanyCode + " | ZipName : " + zipName);
                    log.InsertLog(pathlog, "Insert Data OutputSearchPrinting Company : " + data.CompanyCode + " | ZipName : " + zipName);
                    var resultTransactionPrintZip = InsertTransactionPrintZip(data, zipName);
                    GetListTransactionDescription();
                    if (resultZipFile)
                    {
                        Console.WriteLine("Start Update Status TransactionDescription Company : " + data.CompanyCode);
                        log.InsertLog(pathlog, "Start Update Status TransactionDescription Company : " + data.CompanyCode);
                        var resultUpdateStatus = UpdateStatusTransactionDescription(data);
                        Console.WriteLine("End Update Status TransactionDescription Company : " + data.CompanyCode);
                        log.InsertLog(pathlog, "End Update Status TransactionDescription Company : " + data.CompanyCode);
                    }
                    Console.WriteLine("End Zip Company : " + data.CompanyCode);
                    log.InsertLog(pathlog, "End Zip Company : " + data.CompanyCode);

                }
                Console.WriteLine("End PrintZip");
                log.InsertLog(pathlog, "End PrintZip");
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
        }
        public List<FileModel> ReadFile()
        {
            List<FileModel> result = new List<FileModel>();
            string fileType = "*.pdf";
            FileModel fileModel = new FileModel();
            Filedetail Filedetail = new Filedetail();
            List<TransactionDescription> datatransaction = new List<TransactionDescription>();
            try
            {
                //pathFolder = @"C:\Code_Dev\sign";
                foreach (var config in configPrintSetting)
                {
                    if (CheckRunningTime(config))
                    {
                        fileModel = new FileModel();
                        fileModel.InputPath = config.ConfigMftsCompressPrintSettingInputPdf;
                        fileModel.OutPath = config.ConfigMftsCompressPrintSettingOutputPdf;
                        fileModel.CompanyCode = config.ConfigMftsCompressPrintSettingCompanyCode;
                        fileModel.FileDetails = new List<Filedetail>();
                        datatransaction = transactionDescription.Where(x => x.PdfSignStatus == "Successful" &&
                                                                        x.CompanyCode == config.ConfigMftsCompressPrintSettingCompanyCode &&
                                                                        x.Isactive == 1).ToList();
                        foreach (var file in datatransaction)
                        {
                            Filedetail = new Filedetail();
                            Filedetail.FilePath = file.PdfSignLocation;
                            Filedetail.FileName = Path.GetFileName(file.PdfSignLocation);
                            Filedetail.BillingNo = file.BillingNumber;
                            fileModel.FileDetails.Add(Filedetail);
                        }
                        result.Add(fileModel);
                    }
                }

            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }

        public bool CheckRunningTime(ConfigMftsCompressPrintSetting config)
        {
            bool result = false;
            try
            {
                if (config.ConfigMftsCompressPrintSettingOneTime != null &&
                        !String.IsNullOrEmpty(config.ConfigMftsCompressPrintSettingOneTime) &&
                        Convert.ToDateTime(config.ConfigMftsCompressPrintSettingOneTime) <= DateTime.Now)
                {
                    result = true;
                }
                if (config.ConfigMftsCompressPrintSettingAnyTime != null &&
                    !String.IsNullOrEmpty(config.ConfigMftsCompressPrintSettingAnyTime))
                {
                    if (config.ConfigMftsCompressPrintSettingAnyTime.IndexOf(DateTime.Now.ToString("HH:mm")) >= 0)
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
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }

        public List<FileModel> ReadFile_Old()
        {
            List<FileModel> result = new List<FileModel>();
            string[] fullpath = new string[0];
            string pathFolder = "";
            string fileType = "*.pdf";
            List<string> listpath;
            FileModel fileModel = new FileModel();
            Filedetail Filedetail = new Filedetail();
            string billno = "";
            string filename = "";

            ConfigMftsCompressPrintSetting config = new ConfigMftsCompressPrintSetting();
            //config.ConfigXmlsignInputPath = @"D:\sign";
            //config.ConfigXmlsignOutputPath = @"D:\sign";
            //configXmlSign = new List<ConfigXmlSign>();
            //configXmlSign.Add(config);
            try
            {
                //pathFolder = @"C:\Code_Dev\sign";
                foreach (var path in configPrintSetting)
                {
                    pathFolder = path.ConfigMftsCompressPrintSettingInputPdf;
                    fullpath = Directory.GetFiles(pathFolder, fileType);
                    listpath = fullpath.ToList();

                    fileModel = new FileModel();
                    fileModel.InputPath = path.ConfigMftsCompressPrintSettingInputPdf;
                    fileModel.OutPath = path.ConfigMftsCompressPrintSettingOutputPdf;
                    fileModel.CompanyCode = path.ConfigMftsCompressPrintSettingCompanyCode;
                    fileModel.FileDetails = new List<Filedetail>();
                    foreach (var file in listpath)
                    {
                        Filedetail = new Filedetail();
                        filename = Path.GetFileName(file);
                        Filedetail.FilePath = file;
                        Filedetail.FileName = filename;
                        filename = filename.Replace(".pdf", "");
                        if (filename.IndexOf('_') > -1)
                        {
                            billno = filename.Substring(8, (filename.IndexOf('_')) - 8);
                        }
                        else
                        {
                            billno = filename.Substring(8);
                        }
                        Filedetail.BillingNo = billno;
                        fileModel.FileDetails.Add(Filedetail);
                    }
                    result.Add(fileModel);
                }

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
                configPrintSetting = configMftsCompressPrintSettingController.List().Result;
                configGlobal = configGlobalController.List().Result;
                pathlog = configGlobal.FirstOrDefault(x => x.ConfigGlobalName == namepathlog).ConfigGlobalValue;
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
        }

        public void GetListTransactionDescription()
        {
            try
            {
                transactionDescription = transactionDescriptionController.List().Result;
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
        }

        public bool Zipfile(FileModel dataFile, string zipName)
        {
            bool result = false;
            string zipPath = "";
            try
            {
                //zipPath = @"D:\Example\result.zip";
                zipPath = dataFile.OutPath;
                if (!Directory.Exists(zipPath))
                {
                    Directory.CreateDirectory(zipPath);
                }

                using (FileStream zipFileToOpen = new FileStream(zipPath + "\\" + zipName, FileMode.OpenOrCreate))
                {
                    using (ZipArchive archive = new ZipArchive(zipFileToOpen, ZipArchiveMode.Create))
                    {
                        foreach (var file in dataFile.FileDetails)
                        {
                            Console.WriteLine("Zip Company : " + dataFile.CompanyCode + " | File Name : " + file.FileName);
                            log.InsertLog(pathlog, "Zip Company : " + dataFile.CompanyCode + " | File Name : " + file.FileName);
                            archive.CreateEntryFromFile(file.FilePath, file.FileName);
                        }
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }

        public bool InsertTransactionPrintZip(FileModel dataFile, string zipName)
        {
            bool result = false;
            try
            {
                Task<Response> res;
                OutputSearchPrinting insertData = new OutputSearchPrinting();
                insertData.OutputSearchPrintingCompanyCode = dataFile.CompanyCode;
                insertData.OutputSearchPrintingFileName = zipName;
                insertData.OutputSearchPrintingFullPath = dataFile.OutPath + "\\" + zipName;
                insertData.CreateBy = "Batch";
                insertData.CreateDate = DateTime.Now;
                insertData.UpdateBy = "Batch";
                insertData.UpdateDate = DateTime.Now;
                insertData.Isactive = 1;

                var json = JsonSerializer.Serialize(insertData);
                res = outputSearchPrintingController.Insert(json);
                if (res.Result.MESSAGE == "Insert success.")
                {
                    outputsearchprintingno = res.Result.OUTPUT_DATA.ToString();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }

        public bool UpdateStatusTransactionDescription(FileModel dataFile)
        {
            bool result = false;
            try
            {
                Task<Response> res;
                TransactionDescription updatetransaction = new TransactionDescription();
                List<TransactionDescription> listupdatetransaction = new List<TransactionDescription>();
                foreach (var filedata in dataFile.FileDetails)
                {
                    updatetransaction = transactionDescription.FirstOrDefault(x => x.BillingNumber == filedata.BillingNo);
                    if (updatetransaction != null)
                    {
                        Console.WriteLine("Update Status TransactionDescription BillingNo : " + filedata.BillingNo);
                        log.InsertLog(pathlog, "Update Status TransactionDescription BillingNo : " + filedata.BillingNo);
                        updatetransaction.OutputPdfTransactionNo = outputsearchprintingno;
                        updatetransaction.PrintStatus = "Successful";
                        updatetransaction.PrintDetail = "PDF file's was prepared for printing completely";
                        updatetransaction.PrintDateTime = DateTime.Now;
                        listupdatetransaction.Add(updatetransaction);

                        Console.WriteLine("Start MoveFile Company : " + dataFile.CompanyCode);
                        log.InsertLog(pathlog, "Start MoveFile Company : " + dataFile.CompanyCode);
                        MoveFile(filedata.FilePath, filedata.FileName, updatetransaction.BillingDate ?? DateTime.Now);
                        Console.WriteLine("End MoveFile Company : " + dataFile.CompanyCode);
                        log.InsertLog(pathlog, "End MoveFile Company : " + dataFile.CompanyCode);
                    }
                }
                if (listupdatetransaction.Count > 0)
                {
                    var json = JsonSerializer.Serialize(listupdatetransaction);
                    res = transactionDescriptionController.UpdateList(json);
                    if (res.Result.MESSAGE == "Updated Success.")
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

        public bool MoveFile(string pathinput, string filename, DateTime billingdate)
        {
            bool result = false;
            //pathinpput = @"c:\temp\MySample.txt";
            //string pathoutput = @"D:\sign\backupfile\";
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
            catch (Exception ex)
            {
                Console.WriteLine("The process failed: {0}", ex.ToString());
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }
    }
}
