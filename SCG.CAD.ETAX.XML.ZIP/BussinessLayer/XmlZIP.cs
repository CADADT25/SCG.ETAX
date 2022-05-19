using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.XML.ZIP.Controller;
using SCG.CAD.ETAX.XML.ZIP.Models;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.XML.ZIP.BussinessLayer
{
    public class XmlZIP
    {
        ConfigMftsCompressXmlSettingController configMftsCompressXmlSettingController = new ConfigMftsCompressXmlSettingController();
        OutputSearchXmlZipController outputSearchXmlZipController = new OutputSearchXmlZipController();
        TransactionDescriptionController transactionDescriptionController = new TransactionDescriptionController();
        List<ConfigMftsCompressXmlSetting> configXmlSetting = new List<ConfigMftsCompressXmlSetting>();
        List<TransactionDescription> transactionDescription = new List<TransactionDescription>();

        public void Xml_ZIP()
        {
            string zipName = "";
            try
            {
                Console.WriteLine("Start XmlZIP");
                GetDataFromDataBase();
                Console.WriteLine("Start Read All XML");
                var dataallCompany = ReadFile();
                Console.WriteLine("End Read All XML");
                foreach (var data in dataallCompany)
                {
                    Console.WriteLine("Start Zip Company : " + data.CompanyCode);
                    zipName = data.CompanyCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".7z";
                    //var resultZipFile = Zipfile(data, zipName);
                    var resultZipfileMax3mb = ZipfileMax3mb(data);

                }
                Console.WriteLine("End XmlZIP");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<FileModel> ReadFile()
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

            ConfigMftsCompressXmlSetting config = new ConfigMftsCompressXmlSetting();
            config.ConfigMftsCompressXmlSettingOutputFolder = @"C:\Code_Dev\XMLZip";
            config.ConfigMftsCompressXmlSettingOutputFolder = @"C:\Code_Dev\XMLZip";
            config.ConfigMftsCompressXmlSettingCompanyCode = "0030";
            configXmlSetting = new List<ConfigMftsCompressXmlSetting>();
            configXmlSetting.Add(config);
            try
            {
                //pathFolder = @"C:\Code_Dev\sign";
                foreach (var path in configXmlSetting)
                {
                    pathFolder = path.ConfigMftsCompressXmlSettingOutputFolder;
                    fullpath = Directory.GetFiles(pathFolder, fileType);
                    listpath = fullpath.ToList();

                    fileModel = new FileModel();
                    //fileModel.InputPath = path.input;
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
                throw ex;
            }
            return result;
        }

        public void GetDataFromDataBase()
        {
            try
            {
                //configXmlSetting = configMftsCompressXmlSettingController.List().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetListTransactionDescription()
        {
            try
            {
                //transactionDescription = transactionDescriptionController.List().Result;
            }
            catch (Exception ex)
            {
                throw ex;
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
                            archive.CreateEntryFromFile(file.FilePath, file.FileName);
                        }
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool ZipfileMax3mb(FileModel dataFile)
        {
            bool result = false;
            string zipPath = "";
            string zipName = "";
            bool flagnewfile = true;
            FileInfo fi;
            List<string> listbillno = new List<string>();
            try
            {
                //zipPath = @"D:\Example\result.zip";
                zipPath = dataFile.OutPath;
                if (!Directory.Exists(zipPath))
                {
                    Directory.CreateDirectory(zipPath);
                }

                for(int i = 0; i < dataFile.FileDetails.Count; i++)
                {

                    if (flagnewfile)
                    {
                        zipName = dataFile.CompanyCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".7z";
                        flagnewfile = false;
                    }

                    using (FileStream zipFileToOpen = new FileStream(zipPath + "\\" + zipName, FileMode.OpenOrCreate))
                    {
                        using (ZipArchive archive = new ZipArchive(zipFileToOpen, ZipArchiveMode.Update))
                        {
                            fi = new FileInfo(dataFile.FileDetails[i].FilePath);
                            if (fi.Exists)
                            {
                                Console.WriteLine("Zip Company : " + dataFile.CompanyCode + " | File Name : " + dataFile.FileDetails[i].FileName);
                                archive.CreateEntryFromFile(dataFile.FileDetails[i].FilePath, dataFile.FileDetails[i].FileName);
                                if ((CalculateMBbyByte(zipFileToOpen.Length)) > 3)
                                {
                                    foreach (var item in archive.Entries)
                                    {
                                        if (item.Name.Equals(dataFile.FileDetails[i].FileName))
                                        {
                                            item.Delete();
                                            flagnewfile = true;
                                            i--;
                                            break; //needed to break out of the loop
                                        }
                                    }
                                    Console.WriteLine("Start Update Status TransactionDescription Company : " + dataFile.CompanyCode + " | ZipFilename : " + zipName);
                                    GetListTransactionDescription();
                                    UpdateStatusTransactionDescription(listbillno);
                                    Console.WriteLine("End Update Status TransactionDescription Company : " + dataFile.CompanyCode + " | ZipFilename : " + zipName);


                                    Console.WriteLine("Start Insert Data OutputSearchXmlZip Company : " + dataFile.CompanyCode + " | ZipName : " + zipName);
                                    InsertTransactionXmlZIP(dataFile, zipName);
                                    Console.WriteLine("End Insert Data OutputSearchXmlZip Company : " + dataFile.CompanyCode + " | ZipName : " + zipName);
                                    listbillno = new List<string>();
                                }
                                listbillno.Add(dataFile.FileDetails[i].BillingNo);
                            }
                        }
                    }
                }

                 if(listbillno.Count > 0)
                {
                    Console.WriteLine("Start Update Status TransactionDescription Company : " + dataFile.CompanyCode + " | ZipFilename : " + zipName);
                    GetListTransactionDescription();
                    UpdateStatusTransactionDescription(listbillno);
                    Console.WriteLine("End Update Status TransactionDescription Company : " + dataFile.CompanyCode + " | ZipFilename : " + zipName);


                    Console.WriteLine("Start Insert Data OutputSearchXmlZip Company : " + dataFile.CompanyCode + " | ZipName : " + zipName);
                    InsertTransactionXmlZIP(dataFile, zipName);
                    Console.WriteLine("End Insert Data OutputSearchXmlZip Company : " + dataFile.CompanyCode + " | ZipName : " + zipName);
                }

                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool InsertTransactionXmlZIP(FileModel dataFile, string zipName)
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
                res = outputSearchXmlZipController.Insert(json);
                if (res.Result.MESSAGE == "Insert success.")
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

        public bool UpdateStatusTransactionDescription(List<string> listbillno)
        {
            bool result = false;
            try
            {
                Task<Response> res;
                TransactionDescription updatetransaction = new TransactionDescription();
                List<TransactionDescription> listupdatetransaction = new List<TransactionDescription>();
                foreach (var billno in listbillno)
                {
                    updatetransaction = transactionDescription.FirstOrDefault(x => x.BillingNumber == billno);
                    if (updatetransaction != null)
                    {
                        Console.WriteLine("Update Status TransactionDescription BillingNo : " + billno);
                        updatetransaction.XmlCompressStatus = "Successful";
                        updatetransaction.XmlCompressDetail = "PDF file's was prepared for printing completely";
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
                throw ex;
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
                throw ex;
            }
            return result;
        }
    }
}
