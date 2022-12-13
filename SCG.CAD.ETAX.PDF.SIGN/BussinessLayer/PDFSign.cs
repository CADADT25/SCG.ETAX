using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.MODEL;
using System.Text.Json;
using SCG.CAD.ETAX.UTILITY.Controllers;
using SCG.CAD.ETAX.UTILITY;
using SCG.CAD.ETAX.MODEL.CustomModel;

namespace SCG.CAD.ETAX.PDF.SIGN.BussinessLayer
{
    public class PDFSign
    {
        UtilityConfigPDFSignController configPDFSignController = new UtilityConfigPDFSignController();
        UtilityConfigGlobalController configGlobalController = new UtilityConfigGlobalController();
        UtilityPDFSignController utilityPDFSignController = new UtilityPDFSignController();
        LogHelper log = new LogHelper();

        List<ConfigPdfSign> configPDFSign = new List<ConfigPdfSign>();
        List<ConfigGlobal> configGlobal = new List<ConfigGlobal>();
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
            FilePDF pdfDetail = new FilePDF();
            DirectoryInfo directoryInfo;
            string billno = "";
            string comcode = "";
            string filename = "";

            try
            {
                pDFSignModel = new PDFSignModel();
                pDFSignModel.configPdfSign = config;
                pDFSignModel.listFilePDFs = new List<FilePDF>();
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
                        comcode = filename.Substring(0, 4);
                        pdfDetail = new FilePDF();
                        pdfDetail.FullPath = item.FullName;
                        pdfDetail.FileName = filename;
                        pdfDetail.Outbound = config.ConfigPdfsignOutputPath;
                        pdfDetail.Inbound = config.ConfigPdfsignInputPath;
                        pdfDetail.Billno = billno;
                        pdfDetail.Comcode = comcode;
                        pDFSignModel.listFilePDFs.Add(pdfDetail);
                    }
                    result = pDFSignModel;
                    Console.WriteLine("Path Found : " + pathFolder + " PDF : " + pDFSignModel.listFilePDFs.Count + " files");
                    log.InsertLog(pathlog, "Path Found : " + pathFolder + " PDF : " + pDFSignModel.listFilePDFs.Count + " files");
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
            Response res = new Response();
            try
            {
                Console.WriteLine("Start PDFSign");
                log.InsertLog(pathlog, "Start PDFSign");
                GetDataFromDataBase();

                foreach (var config in configPDFSign)
                {
                    var allfile = ReadPdfFile(config);

                    if (allfile != null && allfile.listFilePDFs != null)
                    {
                        if (allfile.listFilePDFs.Count > 0)
                        {
                            foreach (var file in allfile.listFilePDFs)
                            {
                                res = utilityPDFSignController.ProcessPDFSign(config, file);
                                if (res.STATUS)
                                {
                                    Console.WriteLine("Billno : " + file.Billno + " | Result : Success");
                                    log.InsertLog(pathlog, "Billno : " + file.Billno + " | Result : Success");
                                }
                                else
                                {
                                    Console.WriteLine("Billno : " + file.Billno + " | Result : Fail | ErrorMessage : " + res.ERROR_MESSAGE);
                                    log.InsertLog(pathlog, "Billno : " + file.Billno + " | Result : Fail | ErrorMessage : " + res.ERROR_MESSAGE);
                                }
                            }
                        }
                    }
                }

                Console.WriteLine("End PDFSign");
                log.InsertLog(pathlog, "End PDFSign");
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
        }

        public void GetDataFromDataBase()
        {
            try
            {
                configGlobal = configGlobalController.List().Result;
                configPDFSign = configPDFSignController.List().Result;
                pathlog = configGlobal.FirstOrDefault(x => x.ConfigGlobalName == namepathlog).ConfigGlobalValue;
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
        }
    }
}
