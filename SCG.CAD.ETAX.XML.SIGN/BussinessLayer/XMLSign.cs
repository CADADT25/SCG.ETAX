using SCG.CAD.ETAX.MODEL.etaxModel;
using System.Text.Json;
using SCG.CAD.ETAX.MODEL;
using System.Xml;
using SCG.CAD.ETAX.UTILITY.Controllers;
using SCG.CAD.ETAX.UTILITY;
using SCG.CAD.ETAX.MODEL.CustomModel;
using System.Text;

namespace SCG.CAD.ETAX.XML.SIGN.BussinessLayer
{
    public class XMLSign
    {
        UtilityConfigXMLSignController configXMLSignController = new UtilityConfigXMLSignController();
        UtilityTransactionDescriptionController transactionDescription = new UtilityTransactionDescriptionController();
        UtilityConfigGlobalController configGlobalController = new UtilityConfigGlobalController();
        UtilityAPISignController signXMLController = new UtilityAPISignController();
        UtilityXMLSignController utilityXMLSignController = new UtilityXMLSignController();
        LogHelper log = new LogHelper();
        LogicToolHelper logicToolHelper = new LogicToolHelper();

        List<ConfigXmlSign> configXmlSign = new List<ConfigXmlSign>();
        List<ConfigGlobal> configGlobal = new List<ConfigGlobal>();
        string pathlog = @"C:\log\";
        string namepathlog = "PATHLOGFILE_XMLSIGN";
        string batchname = "SCG.CAD.ETAX.XML.SIGN";

        public void ProcessXMLSign()
        {
            Response res = new Response();
            try
            {
                Console.WriteLine("Start XMLSign");
                log.InsertLog(pathlog, "Start XMLSign");

                GetDataFromDataBase();

                foreach (var config in configXmlSign)
                {
                    var allfile = ReadXmlFile(config);

                    if (allfile != null && allfile.listFileXMLs != null)
                    {
                        if (allfile.listFileXMLs.Count > 0)
                        {
                            foreach (var file in allfile.listFileXMLs)
                            {
                                res = utilityXMLSignController.ProcessXMLSign(config, file);
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

                Console.WriteLine("End XMLSign");
                log.InsertLog(pathlog, "End XMLSign");
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
        }

        public XMLSignModel ReadXmlFile(ConfigXmlSign config)
        {
            XMLSignModel result = new XMLSignModel();
            XMLSignModel xMLSignModel = new XMLSignModel();
            string pathFolder = "";
            string fileType = "*.xml";
            List<FileInfo> listpath;
            FileXML xmlDetail = new FileXML();
            DirectoryInfo directoryInfo;
            string billno = "";
            string filename = "";
            try
            {
                xMLSignModel = new XMLSignModel();
                xMLSignModel.configXmlSign = config;
                xMLSignModel.listFileXMLs = new List<FileXML>();
                pathFolder = config.ConfigXmlsignInputPath;
                if (Directory.Exists(pathFolder))
                {
                    directoryInfo = new DirectoryInfo(pathFolder);
                    listpath = directoryInfo.GetFiles(fileType)
                     .OrderBy(f => f.LastWriteTime).ToList();

                    foreach (var item in listpath)
                    {
                        filename = Path.GetFileName(item.FullName).Replace(".xml", "");
                        billno = filename.Substring(8, (filename.IndexOf('_')) - 8);
                        xmlDetail = new FileXML();
                        xmlDetail.FullPath = item.FullName;
                        xmlDetail.FileName = filename;
                        xmlDetail.Outbound = config.ConfigXmlsignOutputPath;
                        xmlDetail.Inbound = config.ConfigXmlsignInputPath;
                        xmlDetail.Billno = billno;
                        xMLSignModel.listFileXMLs.Add(xmlDetail);
                    }
                    result = xMLSignModel;
                    Console.WriteLine("Path : " + pathFolder + " Found PDF : " + xMLSignModel.listFileXMLs.Count + " files");
                    log.InsertLog(pathlog, "Path : " + pathFolder + " Found PDF : " + xMLSignModel.listFileXMLs.Count + " files");
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
    }
}
