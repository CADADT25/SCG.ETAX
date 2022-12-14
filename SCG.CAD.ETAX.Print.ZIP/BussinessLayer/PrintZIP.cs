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
        UtilityConfigMftsCompressPrintSettingController configMftsCompressPrintSettingController = new UtilityConfigMftsCompressPrintSettingController();
        UtilityOutputSearchPrintingController outputSearchPrintingController = new UtilityOutputSearchPrintingController();
        UtilityTransactionDescriptionController transactionDescriptionController = new UtilityTransactionDescriptionController();
        UtilityConfigGlobalController configGlobalController = new UtilityConfigGlobalController();
        LogHelper log = new LogHelper();
        LogicToolHelper logicToolHelper = new LogicToolHelper();

        List<ConfigMftsCompressPrintSetting> configPrintSetting = new List<ConfigMftsCompressPrintSetting>();
        List<TransactionDescription> transactionDescription = new List<TransactionDescription>();
        List<ConfigGlobal> configGlobal = new List<ConfigGlobal>();
        string pathoutput;
        string outputsearchprintingno;
        string pathlog = @"C:\log\";
        string namepathlog = "PATHLOGFILE_PRINTZIP";
        string batchname = "SCG.CAD.ETAX.PRINT.ZIP";

        public void ProcessPrintzip()
        {
            string zipName = "";
            DateTime nexttime;
            try
            {
                Console.WriteLine("Start PrintZip");
                log.InsertLog(pathlog, "Start PrintZip");
                GetDataFromDataBase();
                GetListTransactionDescription();

                foreach (var config in configPrintSetting)
                {
                    if (logicToolHelper.CheckRunTime(config.ConfigMftsCompressPrintSettingNextTime))
                    {
                        Console.WriteLine("Start Read All PDF");
                        log.InsertLog(pathlog, "Start Read All PDF");
                        var dataallCompany = ReadFile(config);
                        Console.WriteLine("End Read All PDF");
                        log.InsertLog(pathlog, "End Read All PDF");
                        pathoutput = configGlobal.FirstOrDefault(x => x.ConfigGlobalName == "PATHBACKUPPRINTZIPFILE").ConfigGlobalValue;
                        foreach (var data in dataallCompany)
                        {
                            Console.WriteLine("Start Zip Company : " + data.CompanyCode);
                            log.InsertLog(pathlog, "Start Zip Company : " + data.CompanyCode);
                            zipName = data.CompanyCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".zip";
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
                        nexttime = logicToolHelper.SetNextRunTime(config.ConfigMftsCompressPrintSettingAnyTime, config.ConfigMftsCompressPrintSettingOneTime, batchname, config.ConfigMftsCompressPrintSettingNo);
                        Console.WriteLine("Set NextTime : " + nexttime);
                        log.InsertLog(pathlog, "Set NextTime : " + nexttime);
                    }
                }
                Console.WriteLine("End PrintZip");
                log.InsertLog(pathlog, "End PrintZip");
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
        }
        public List<FileModel> ReadFile(ConfigMftsCompressPrintSetting config)
        {
            List<FileModel> result = new List<FileModel>();
            string fileType = "*.pdf";
            FileModel fileModel = new FileModel();
            Filedetail Filedetail = new Filedetail();
            List<TransactionDescription> datatransaction = new List<TransactionDescription>();
            try
            {
                        fileModel = new FileModel();
                        fileModel.InputPath = config.ConfigMftsCompressPrintSettingInputPdf;
                        fileModel.OutPath = config.ConfigMftsCompressPrintSettingOutputPdf;
                        fileModel.CompanyCode = config.ConfigMftsCompressPrintSettingCompanyCode;
                        fileModel.FileDetails = new List<Filedetail>();
                        datatransaction = transactionDescription.Where(x => x.PdfSignStatus == "Successful" &&
                                                                        x.PrintStatus == "Waiting" &&
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

    }
}
