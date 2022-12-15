using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.XML.ZIP.Models;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SCG.CAD.ETAX.UTILITY.Controllers;
using SCG.CAD.ETAX.UTILITY;

namespace SCG.CAD.ETAX.XML.ZIP.BussinessLayer
{
    public class XmlZIP
    {
        UtilityConfigMftsCompressXmlSettingController configMftsCompressXmlSettingController = new UtilityConfigMftsCompressXmlSettingController();
        UtilityOutputSearchXmlZipController outputSearchXmlZipController = new UtilityOutputSearchXmlZipController();
        UtilityTransactionDescriptionController transactionDescriptionController = new UtilityTransactionDescriptionController();
        UtilityConfigGlobalController configGlobalController = new UtilityConfigGlobalController();
        LogHelper log = new LogHelper();
        LogicToolHelper logicToolHelper = new LogicToolHelper();

        List<ConfigMftsCompressXmlSetting> configXmlSetting = new List<ConfigMftsCompressXmlSetting>();
        List<TransactionDescription> transactionDescription = new List<TransactionDescription>();
        List<ConfigGlobal> configGlobal = new List<ConfigGlobal>();
        string pathoutput;
        string outputxmltransactionno;
        string pathlog = @"C:\log\";
        string namepathlog = "PATHLOGFILE_XMLZIP";
        string batchname = "SCG.CAD.ETAX.XML.ZIP";
        DateTime nexttime;

        public void Xml_ZIP()
        {
            string zipName = "";
            XmlFileModel xmlFileModel = new XmlFileModel();
            try
            {
                Console.WriteLine("Start XmlZIP");
                log.InsertLog(pathlog, "Start XmlZIP");
                GetDataFromDataBase();
                GetListTransactionDescription();

                foreach (var config in configXmlSetting)
                {
                    if (logicToolHelper.CheckRunTime(config.ConfigMftsCompressXmlSettingNextTime))
                    {
                        Console.WriteLine("Start Read All XML");
                        log.InsertLog(pathlog, "Start Read All XML");
                        var dataallCompany = ReadFile(config);
                        Console.WriteLine("End Read All XML");
                        log.InsertLog(pathlog, "End Read All XML");
                        pathoutput = configGlobal.FirstOrDefault(x => x.ConfigGlobalName == "PATHBACKUPXMLZIPFILE").ConfigGlobalValue;
                        foreach (var data in dataallCompany)
                        {
                            Console.WriteLine("Start CompanyCode : " + data.CompanyCode);
                            log.InsertLog(pathlog, "Start CompanyCode : " + data.CompanyCode);

                            Console.WriteLine("Start Separate DocumentType Company : " + data.CompanyCode);
                            log.InsertLog(pathlog, "Start Separate DocumentType Company : " + data.CompanyCode);
                            xmlFileModel = separateXmlFilebyDocumentType(data);
                            Console.WriteLine("End Separate DocumentType Company : " + data.CompanyCode);
                            log.InsertLog(pathlog, "End Separate DocumentType Company : " + data.CompanyCode);

                            Console.WriteLine("Start Zip Company : " + data.CompanyCode);
                            log.InsertLog(pathlog, "Start Zip Company : " + data.CompanyCode);
                            zipName = data.CompanyCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".zip";
                            //var resultZipFile = Zipfile(data, zipName);
                            var resultZipfileMax3mb = ZipfileMax3mb(xmlFileModel);
                            Console.WriteLine("End CompanyCode : " + data.CompanyCode);
                            log.InsertLog(pathlog, "End CompanyCode : " + data.CompanyCode);
                        }

                        nexttime = logicToolHelper.SetNextRunTime(config.ConfigMftsCompressXmlSettingAnyTime, config.ConfigMftsCompressXmlSettingOneTime, batchname, config.ConfigMftsCompressXmlSettingNo);
                        Console.WriteLine("Set NextTime : " + nexttime);
                        log.InsertLog(pathlog, "Set NextTime : " + nexttime);
                    }
                }
                Console.WriteLine("End XmlZIP");
                log.InsertLog(pathlog, "End XmlZIP");
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
        }
        public List<FileModel> ReadFile(ConfigMftsCompressXmlSetting config)
        {
            List<FileModel> result = new List<FileModel>();
            string fileType = "*.xml";
            FileModel fileModel = new FileModel();
            Filedetail Filedetail = new Filedetail();
            List<TransactionDescription> datatransaction = new List<TransactionDescription>();
            try
            {
                fileModel = new FileModel();
                fileModel.InputPath = config.ConfigMftsCompressXmlSettingInputFolder;
                fileModel.OutPath = config.ConfigMftsCompressXmlSettingOutputFolder;
                fileModel.CompanyCode = config.ConfigMftsCompressXmlSettingCompanyCode;
                fileModel.FileDetails = new List<Filedetail>();
                datatransaction = transactionDescription.Where(x => x.XmlSignStatus == "Successful" &&
                                                                x.XmlCompressStatus == "Waiting" &&
                                                                x.CompanyCode == config.ConfigMftsCompressXmlSettingCompanyCode &&
                                                                x.Isactive == 1).ToList();
                foreach (var file in datatransaction)
                {
                    Filedetail = new Filedetail();
                    Filedetail.FilePath = file.XmlSignLocation;
                    Filedetail.FileName = Path.GetFileName(file.XmlSignLocation);
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

        //public bool CheckRunningTime(ConfigMftsCompressXmlSetting config)
        //{
        //    bool result = false;
        //    try
        //    {
        //        if (config.ConfigMftsCompressXmlSettingOneTime != null &&
        //                !String.IsNullOrEmpty(config.ConfigMftsCompressXmlSettingOneTime) &&
        //                Convert.ToDateTime(config.ConfigMftsCompressXmlSettingOneTime) <= DateTime.Now)
        //        {
        //            result = true;
        //        }
        //        if (config.ConfigMftsCompressXmlSettingAnyTime != null &&
        //            !String.IsNullOrEmpty(config.ConfigMftsCompressXmlSettingAnyTime))
        //        {
        //            if (config.ConfigMftsCompressXmlSettingAnyTime.IndexOf(DateTime.Now.ToString("HH:mm")) >= 0)
        //            {
        //                result = true;
        //            }
        //        }
        //        else
        //        {
        //            result = true;
        //        }
        //        result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.InsertLog(pathlog, "Exception : " + ex.ToString());
        //    }
        //    return result;
        //}

        public List<FileModel> ReadFile_Old()
        {
            List<FileModel> result = new List<FileModel>();
            string[] fullpath = new string[0];
            string pathFolder = "";
            string fileType = "*.xml";
            List<string> listpath;
            FileModel fileModel = new FileModel();
            Filedetail Filedetail = new Filedetail();
            string billno = "";
            string filename = "";

            //ConfigMftsCompressXmlSetting config = new ConfigMftsCompressXmlSetting();
            //config.ConfigMftsCompressXmlSettingOutputFolder = @"C:\Code_Dev\XMLZip";
            //config.ConfigMftsCompressXmlSettingOutputFolder = @"C:\Code_Dev\XMLZip";
            //config.ConfigMftsCompressXmlSettingCompanyCode = "0030";
            //configXmlSetting = new List<ConfigMftsCompressXmlSetting>();
            //configXmlSetting.Add(config);
            try
            {
                //pathFolder = @"C:\Code_Dev\sign";
                foreach (var path in configXmlSetting)
                {
                    pathFolder = path.ConfigMftsCompressXmlSettingInputFolder;
                    fullpath = Directory.GetFiles(pathFolder, fileType);
                    listpath = fullpath.ToList();

                    fileModel = new FileModel();
                    fileModel.InputPath = path.ConfigMftsCompressXmlSettingInputFolder;
                    fileModel.OutPath = path.ConfigMftsCompressXmlSettingOutputFolder;
                    fileModel.CompanyCode = path.ConfigMftsCompressXmlSettingCompanyCode;
                    fileModel.FileDetails = new List<Filedetail>();
                    foreach (var file in listpath)
                    {
                        Filedetail = new Filedetail();
                        filename = Path.GetFileName(file);
                        Filedetail.FilePath = file;
                        Filedetail.FileName = filename;
                        filename = filename.Replace(".xml", "");
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
                configXmlSetting = configMftsCompressXmlSettingController.List().Result;
                configGlobal = configGlobalController.List().Result;
                transactionDescription = transactionDescriptionController.List().Result;
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

        public bool ZipfileMax3mb(XmlFileModel dataFile)
        {
            bool result = false;
            string zipPath = "";
            string zipName = "";
            bool flagnewfile = true;
            FileInfo fi;
            List<XmlFileDetail> listxmlfileupdate = new List<XmlFileDetail>();
            XmlFileDetail datatxmlfileupdate = new XmlFileDetail();
            try
            {
                foreach (var doctype in dataFile.listByDocumentTypes)
                {
                    flagnewfile = true;
                    listxmlfileupdate = new List<XmlFileDetail>();
                    //zipPath = @"D:\Example\result.zip";
                    //zipPath = dataFile.OutPath + "\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "\\" + doctype.DocumentType;
                    zipPath = dataFile.OutPath + "\\" + doctype.DocumentType;
                    if (!Directory.Exists(zipPath))
                    {
                        Directory.CreateDirectory(zipPath);
                    }

                    for (int i = 0; i < doctype.XmlFileDetails.Count; i++)
                    {
                        datatxmlfileupdate = new XmlFileDetail();
                        if (flagnewfile)
                        {
                            zipName = dataFile.CompanyCode + "_" + doctype.DocumentType.ToUpper() + "_" + DateTime.Now.ToString("yyyyMM") + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".zip";
                            flagnewfile = false;
                        }

                        using (FileStream zipFileToOpen = new FileStream(zipPath + "\\" + zipName, FileMode.OpenOrCreate))
                        {
                            using (ZipArchive archive = new ZipArchive(zipFileToOpen, ZipArchiveMode.Update))
                            {
                                fi = new FileInfo(doctype.XmlFileDetails[i].FullPath);
                                if (fi.Exists)
                                {
                                    Console.WriteLine("Zip Company : " + dataFile.CompanyCode + " | File Name : " + doctype.XmlFileDetails[i].FileName);
                                    log.InsertLog(pathlog, "Zip Company : " + dataFile.CompanyCode + " | File Name : " + doctype.XmlFileDetails[i].FileName);
                                    archive.CreateEntryFromFile(doctype.XmlFileDetails[i].FullPath, doctype.XmlFileDetails[i].FileName);
                                    if ((CalculateMBbyByte(zipFileToOpen.Length)) > 3)
                                    {
                                        foreach (var item in archive.Entries)
                                        {
                                            if (item.Name.Equals(doctype.XmlFileDetails[i].FileName))
                                            {
                                                item.Delete();
                                                flagnewfile = true;
                                                i = i - 1;
                                                break; //needed to break out of the loop
                                            }
                                        }

                                        Console.WriteLine("Start Insert Data OutputSearchXmlZip Company : " + dataFile.CompanyCode + " | ZipName : " + zipName);
                                        log.InsertLog(pathlog, "Start Insert Data OutputSearchXmlZip Company : " + dataFile.CompanyCode + " | ZipName : " + zipName);
                                        InsertTransactionXmlZIP(dataFile.CompanyCode, zipPath, zipName, doctype.DocumentType);
                                        Console.WriteLine("End Insert Data OutputSearchXmlZip Company : " + dataFile.CompanyCode + " | ZipName : " + zipName);
                                        log.InsertLog(pathlog, "End Insert Data OutputSearchXmlZip Company : " + dataFile.CompanyCode + " | ZipName : " + zipName);

                                        Console.WriteLine("Start Update Status TransactionDescription Company : " + dataFile.CompanyCode + " | ZipFilename : " + zipName);
                                        log.InsertLog(pathlog, "Start Update Status TransactionDescription Company : " + dataFile.CompanyCode + " | ZipFilename : " + zipName);
                                        GetListTransactionDescription();
                                        UpdateStatusTransactionDescription(listxmlfileupdate, dataFile.CompanyCode);
                                        Console.WriteLine("End Update Status TransactionDescription Company : " + dataFile.CompanyCode + " | ZipFilename : " + zipName);
                                        log.InsertLog(pathlog, "End Update Status TransactionDescription Company : " + dataFile.CompanyCode + " | ZipFilename : " + zipName);

                                        listxmlfileupdate = new List<XmlFileDetail>();
                                    }
                                    if (!flagnewfile)
                                    {
                                        datatxmlfileupdate.FileName = doctype.XmlFileDetails[i].FileName;
                                        datatxmlfileupdate.FullPath = doctype.XmlFileDetails[i].FullPath;
                                        datatxmlfileupdate.BillingNo = doctype.XmlFileDetails[i].BillingNo;
                                        listxmlfileupdate.Add(datatxmlfileupdate);
                                    }
                                }
                            }
                        }
                    }
                    if (listxmlfileupdate.Count > 0)
                    {
                        Console.WriteLine("Start Insert Data OutputSearchXmlZip Company : " + dataFile.CompanyCode + " | ZipName : " + zipName);
                        log.InsertLog(pathlog, "Start Insert Data OutputSearchXmlZip Company : " + dataFile.CompanyCode + " | ZipName : " + zipName);
                        InsertTransactionXmlZIP(dataFile.CompanyCode, zipPath, zipName, doctype.DocumentType);
                        Console.WriteLine("End Insert Data OutputSearchXmlZip Company : " + dataFile.CompanyCode + " | ZipName : " + zipName);
                        log.InsertLog(pathlog, "End Insert Data OutputSearchXmlZip Company : " + dataFile.CompanyCode + " | ZipName : " + zipName);

                        Console.WriteLine("Start Update Status TransactionDescription Company : " + dataFile.CompanyCode + " | ZipFilename : " + zipName);
                        log.InsertLog(pathlog, "Start Update Status TransactionDescription Company : " + dataFile.CompanyCode + " | ZipFilename : " + zipName);
                        GetListTransactionDescription();
                        UpdateStatusTransactionDescription(listxmlfileupdate, dataFile.CompanyCode);
                        Console.WriteLine("End Update Status TransactionDescription Company : " + dataFile.CompanyCode + " | ZipFilename : " + zipName);
                        log.InsertLog(pathlog, "End Update Status TransactionDescription Company : " + dataFile.CompanyCode + " | ZipFilename : " + zipName);
                        listxmlfileupdate = new List<XmlFileDetail>();
                        flagnewfile = true;
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

        public bool InsertTransactionXmlZIP(string companycode, string outpath, string zipName, string doctype)
        {
            bool result = false;
            try
            {
                Task<Response> res;
                OutputSearchXmlZip insertData = new OutputSearchXmlZip();
                insertData.OutputSearchXmlZipCompanyCode = companycode;
                insertData.OutputSearchXmlZipFileName = zipName;
                insertData.OutputSearchXmlZipFullPath = outpath + "\\" + zipName;
                insertData.OutputSearchXmlZipDowloadCount = 0;
                insertData.OutputSearchXmlZipDowloadStatus = 0;
                insertData.OutputSearchXmlZipDocType = doctype.ToUpper();
                insertData.CreateBy = "Batch";
                insertData.CreateDate = DateTime.Now;
                insertData.UpdateBy = "Batch";
                insertData.UpdateDate = DateTime.Now;
                insertData.Isactive = 1;

                var json = JsonSerializer.Serialize(insertData);
                res = outputSearchXmlZipController.Insert(json);
                if (res.Result.MESSAGE == "Insert success.")
                {
                    outputxmltransactionno = res.Result.OUTPUT_DATA.ToString();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }

        public bool UpdateStatusTransactionDescription(List<XmlFileDetail> listxmlfileupdate, string comcode)
        {
            bool result = false;
            try
            {
                Task<Response> res;
                TransactionDescription updatetransaction = new TransactionDescription();
                List<TransactionDescription> listupdatetransaction = new List<TransactionDescription>();
                foreach (var xmldata in listxmlfileupdate)
                {
                    updatetransaction = transactionDescription.FirstOrDefault(x => x.BillingNumber == xmldata.BillingNo);
                    if (updatetransaction != null)
                    {
                        Console.WriteLine("Update Status TransactionDescription BillingNo : " + xmldata.BillingNo);
                        log.InsertLog(pathlog, "Update Status TransactionDescription BillingNo : " + xmldata.BillingNo);
                        updatetransaction.OutputXmlTransactionNo = outputxmltransactionno;
                        updatetransaction.XmlCompressStatus = "Successful";
                        updatetransaction.XmlCompressDetail = "XML was compressed file is completely";
                        updatetransaction.XmlCompressDateTime = DateTime.Now;
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

        public double CalculateMBbyByte(double bytes)
        {
            double result = 0;
            try
            {
                double temp = bytes % (1024 * 1024 * 1024);

                double MBs = temp / (1024 * 1024);
                result = MBs;
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }

        public XmlFileModel separateXmlFilebyDocumentType(FileModel dataFile)
        {
            TransactionDescription datatransaction = new TransactionDescription();
            XmlFileModel result = new XmlFileModel();
            try
            {
                result.CompanyCode = dataFile.CompanyCode;
                result.OutPath = dataFile.OutPath;
                result.InputPath = dataFile.InputPath;
                result.listByDocumentTypes = new List<ListByDocumentType>();

                ListByDocumentType docTypeTaxInvoice = new ListByDocumentType();
                ListByDocumentType docTypeDebitNote = new ListByDocumentType();
                ListByDocumentType docTypeCreditNote = new ListByDocumentType();
                docTypeTaxInvoice.DocumentType = "TaxInvoice";
                docTypeTaxInvoice.XmlFileDetails = new List<XmlFileDetail>();
                docTypeDebitNote.DocumentType = "DebitNote";
                docTypeDebitNote.XmlFileDetails = new List<XmlFileDetail>();
                docTypeCreditNote.DocumentType = "CreditNote";
                docTypeCreditNote.XmlFileDetails = new List<XmlFileDetail>();
                XmlFileDetail datexmlfile = new XmlFileDetail();
                foreach (var filedetail in dataFile.FileDetails)
                {

                    datatransaction = transactionDescription.FirstOrDefault(x => x.BillingNumber == filedetail.BillingNo);
                    if (datatransaction != null)
                    {
                        datexmlfile = new XmlFileDetail();
                        datexmlfile.FileName = filedetail.FileName;
                        datexmlfile.BillingNo = filedetail.BillingNo;
                        datexmlfile.FullPath = filedetail.FilePath;
                        switch (datatransaction.DocType ?? "")
                        {
                            case "388":
                            case "T02":
                            case "T03":
                            case "T04":
                                //doctype = "TaxInvoice";
                                docTypeTaxInvoice.XmlFileDetails.Add(datexmlfile);
                                break;
                            case "80":
                                //doctype = "DebitNote";
                                docTypeDebitNote.XmlFileDetails.Add(datexmlfile);
                                break;
                            case "81":
                                //doctype = "CreditNote";
                                docTypeCreditNote.XmlFileDetails.Add(datexmlfile);
                                break;
                            case "T01":
                                //doctype = "Invoice";
                                break;
                            default:
                                break;
                        }

                    }
                }
                result.listByDocumentTypes.Add(docTypeTaxInvoice);
                result.listByDocumentTypes.Add(docTypeDebitNote);
                result.listByDocumentTypes.Add(docTypeCreditNote);
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }
    }
}
