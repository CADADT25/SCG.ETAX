using SCG.CAD.ETAX.MODEL.etaxModel;
using System.Text.Json;
using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.XML.SIGN.Models;
using System.Xml;
using SCG.CAD.ETAX.UTILITY.Controllers;
using SCG.CAD.ETAX.UTILITY;

namespace SCG.CAD.ETAX.XML.SIGN.BussinessLayer
{
    public class XMLSign
    {
        UtilityConfigXMLSignController configXMLSignController = new UtilityConfigXMLSignController();
        UtilityTransactionDescriptionController transactionDescription = new UtilityTransactionDescriptionController();
        UtilityConfigGlobalController configGlobalController = new UtilityConfigGlobalController();
        LogHelper log = new LogHelper();

        List<ConfigXmlSign> configXmlSign = new List<ConfigXmlSign>();
        List<ConfigGlobal> configGlobal = new List<ConfigGlobal>();
        string pathoutput;
        string pathlog = @"C:\log\";
        string namepathlog = "PATHLOGFILE_XMLSIGN";

        public void ProcessXMLSign()
        {
            string fullpath = "";
            string fileNameDest = "";
            bool resultXMLSign = false;
            string billno = "";
            int round = 0;
            string fileType = ".xml";
            string pathoutbound = "";
            DateTime billingdate;
            try
            {
                Console.WriteLine("Start XMLSign");

                GetDataFromDataBase();
                Console.WriteLine("Start Read All XMLFile");
                log.InsertLog(pathlog, "Start Read All XMLFile");
                var allfile = ReadXmlFile();
                Console.WriteLine("End Read All XMLFile");
                log.InsertLog(pathlog, "End Read All XMLFile");

                if (allfile != null && allfile.Count > 0)
                {
                    pathoutput = configGlobal.FirstOrDefault(x => x.ConfigGlobalName == "PATHBACKUPXMLFILE").ConfigGlobalValue;
                    foreach (var src in allfile)
                    {
                        round += 1;
                        Console.WriteLine("Start round : " + round);
                        log.InsertLog(pathlog, "Start round : " + round);
                        billno = src.FileName.Substring(8, (src.FileName.IndexOf('_')) - 8);
                        Console.WriteLine("billno : " + billno);
                        log.InsertLog(pathlog, "billno : " + billno);
                        XmlDocument doc = new XmlDocument();
                        doc.Load(src.FullPath);

                        Console.WriteLine("Send To Sign");
                        log.InsertLog(pathlog, "Send To Sign");
                        resultXMLSign = SendFileXMLSign(doc);

                        Console.WriteLine("Status Sign : " + resultXMLSign.ToString());
                        log.InsertLog(pathlog, "Status Sign : " + resultXMLSign.ToString());
                        Console.WriteLine("Update Status in DataBase");
                        log.InsertLog(pathlog, "Update Status in DataBase");

                        fileNameDest = src.FileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        pathoutbound = src.Outbound;

                        var dataTran = transactionDescription.GetBilling(billno).Result.FirstOrDefault();
                        billingdate = DateTime.Now;
                        if (dataTran!= null)
                        {
                            billingdate = dataTran.BillingDate ?? DateTime.Now;
                        }
                        pathoutbound += "\\" + billingdate.ToString("yyyy") + "\\" + billingdate.ToString("MM");
                        if (resultXMLSign)
                        {
                            pathoutbound += "\\Success\\";
                        }
                        else
                        {
                            pathoutbound += "\\Fail\\";
                        }
                        fullpath = pathoutbound + fileNameDest + fileType;

                        UpdateStatusAfterSignXML(resultXMLSign, billno, fullpath, dataTran);

                        Console.WriteLine("Start Export XML file");
                        log.InsertLog(pathlog, "Start Export XML file");
                        ExportXMLAfterSign( doc, pathoutbound, fullpath);
                        Console.WriteLine("End Export XML file");
                        log.InsertLog(pathlog, "End Export XML file");

                        Console.WriteLine("Start Move file");
                        log.InsertLog(pathlog, "Start Move file");
                        MoveFile(src.FullPath, src.FileName + fileType, billingdate);
                        Console.WriteLine("End Move file");
                        log.InsertLog(pathlog, "End Move file");
                    }
                }
                Console.WriteLine("End XMLSign");
                log.InsertLog(pathlog, "End XMLSign");
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
        }

        public List<XMLSignModel> ReadXmlFile()
        {
            List<XMLSignModel> result = new List<XMLSignModel>();
            string pathFolder = "";
            string fileType = "*.xml";
            List<FileInfo> listpath;
            XMLSignModel xMLSignModel = new XMLSignModel();
            DirectoryInfo directoryInfo;
            //ConfigXmlSign config = new ConfigXmlSign();
            //config.ConfigXmlsignInputPath = @"D:\sign";
            //config.ConfigXmlsignOutputPath = @"D:\sign";
            //configXmlSign = new List<ConfigXmlSign>();
            //configXmlSign.Add(config);
            try
            {
                //pathFolder = @"C:\Code_Dev\sign";
                foreach (var config in configXmlSign)
                {
                    if (CheckRunningTime(config))
                    {
                        pathFolder = config.ConfigXmlsignInputPath;

                        directoryInfo = new DirectoryInfo(pathFolder);
                        listpath = directoryInfo.GetFiles(fileType)
                         .OrderBy(f => f.LastWriteTime).ToList();

                        foreach (var item in listpath)
                        {
                            xMLSignModel = new XMLSignModel();
                            xMLSignModel.FullPath = item.FullName;
                            xMLSignModel.FileName = Path.GetFileName(item.FullName).Replace(".xml", "");
                            xMLSignModel.Outbound = config.ConfigXmlsignOutputPath;
                            xMLSignModel.Inbound = config.ConfigXmlsignInputPath;
                            result.Add(xMLSignModel);
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

        public bool CheckRunningTime(ConfigXmlSign config)
        {
            bool result = false;
            try
            {
                if (config.ConfigXmlsignOneTime != null &&
                        !String.IsNullOrEmpty(config.ConfigXmlsignOneTime) &&
                        Convert.ToDateTime(config.ConfigXmlsignOneTime) <= DateTime.Now)
                {
                    result = true;
                }
                if (config.ConfigXmlsignAnyTime != null &&
                    !String.IsNullOrEmpty(config.ConfigXmlsignAnyTime))
                {
                    if (config.ConfigXmlsignAnyTime.IndexOf(DateTime.Now.ToString("HH:mm")) >= 0)
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

        public bool SendFileXMLSign(XmlDocument doc)
        {
            bool result = false;
            try
            {
                result = true;
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }

        public bool UpdateStatusAfterSignXML(bool status, string billno, string pathfile, TransactionDescription dataTran)
        {
            bool result = false;
            string jsondata = "";
            try
            {
                Task<Response> res;
                if (status)
                {
                    dataTran.XmlSignDateTime = DateTime.Now;
                    dataTran.XmlSignDetail = "XML was signed completely";
                    dataTran.XmlSignStatus = "Successful";
                    dataTran.UpdateBy = "Batch";
                    dataTran.UpdateDate = DateTime.Now;
                    dataTran.XmlSignLocation = pathfile;

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
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }

        public bool ExportXMLAfterSign(XmlDocument doc, string pathoutbound, string fullpath)
        {
            bool result = false;
            try
            {
                if (!Directory.Exists(pathoutbound))
                {
                    Directory.CreateDirectory(pathoutbound);
                }
                TextWriter filestream = new StreamWriter(fullpath);
                doc.Save(filestream);
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
                configXmlSign = configXMLSignController.List().Result;
                configGlobal = configGlobalController.List().Result;
                pathlog = configGlobal.FirstOrDefault(x => x.ConfigGlobalName == namepathlog).ConfigGlobalValue;
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
            //string pathoutput = @"D:\sign\backupfile\";
            string output = "";

            try
            {
                output = pathoutput + "\\" + billingdate.ToString("YYYY") + "\\" + billingdate.ToString("MM") + "\\";
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
