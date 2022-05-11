using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.PRINT.ZIP.Controller;
using SCG.CAD.ETAX.XML.PRINT.ZIP.Models;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.PRINT.ZIP.BussinessLayer
{
    public class PrintZIP
    {
        ConfigMftsCompressPrintSettingController ConfigMftsCompressPrintSettingController = new ConfigMftsCompressPrintSettingController();
        List<ConfigMftsCompressPrintSetting> configPrintSetting = new List<ConfigMftsCompressPrintSetting>();

        public void Printzip()
        {
            try
            {
                var dataallCompany = ReadFile();
                foreach (var data in dataallCompany)
                {
                    var resultZipFile = Zipfile(data);
                    var resultUpdateStatus = UpdateStatus(data, resultZipFile);
                }
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
            string fileType = "*.pdf";
            List<string> listpath;
            FileModel fileModel = new FileModel();

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
                    pathFolder = path.ConfigMftsCompressPrintSettingOutputPdf;
                    fullpath = Directory.GetFiles(pathFolder, fileType);
                    listpath = fullpath.ToList();

                    fileModel = new FileModel();
                    fileModel.InputPath = "";
                    fileModel.OutPath = "";
                    fileModel.CompanyCode = path.ConfigMftsCompressPrintSettingCompanyCode;
                    fileModel.FilePath = listpath;
                    //foreach (var item in listpath)
                    //{
                    //    xMLSignModel.FullPath = item;
                    //    xMLSignModel.FileName = Path.GetFileName(item).Replace(".xml", "");
                    //    xMLSignModel.Outbound = path.ConfigXmlsignOutputPath;
                    //    xMLSignModel.Inbound = path.ConfigXmlsignInputPath;
                    //}
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
                configPrintSetting = ConfigMftsCompressPrintSettingController.List().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Zipfile(FileModel dataFile)
        {
            bool result = false;
            string zipPath = "";
            string zipName = "";
            //zipPath = @"D:\Example\result.zip";
            zipPath = dataFile.OutPath;
            zipName = dataFile.CompanyCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            if (!Directory.Exists(zipPath))
            {
                Directory.CreateDirectory(zipPath);
            }

            using (FileStream zipFileToOpen = new FileStream(zipPath + "\\" + zipName, FileMode.OpenOrCreate))
            {
                using (ZipArchive archive = new ZipArchive(zipFileToOpen, ZipArchiveMode.Create))
                {
                    foreach (var file in dataFile.FilePath)
                    {
                        archive.CreateEntryFromFile(file, Path.GetFileName(file));
                    }
                    //archive.CreateEntryFromFile(@"D:\Example\file1.pdf", "file1.pdf");
                    //archive.CreateEntryFromFile(@"D:\Example\file2.pdf", "file2.pdf");
                }
            }
            return result;
        }

        public bool UpdateStatus(FileModel dataFile, bool resultZipFile)
        {
            bool result = false;
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
