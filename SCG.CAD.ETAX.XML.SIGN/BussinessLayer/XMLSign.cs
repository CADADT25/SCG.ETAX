using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCG.CAD.ETAX.XML.SIGN.Controller;
using SCG.CAD.ETAX.MODEL.etaxModel;
using System.Text.Json;
using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.XML.SIGN.Models;
using System.Xml;

namespace SCG.CAD.ETAX.XML.SIGN.BussinessLayer
{
    public class XMLSign
    {
        ConfigXMLSignController configXMLSignController = new ConfigXMLSignController();
        TransactionDescriptionController transactionDescription = new TransactionDescriptionController();
        ConfigGlobalController configGlobalController = new ConfigGlobalController();

        List<ConfigXmlSign> configXmlSign = new List<ConfigXmlSign>();
        List<ConfigGlobal> configGlobal = new List<ConfigGlobal>();
        string pathoutput;

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
                var allfile = ReadXmlFile();
                Console.WriteLine("End Read All XMLFile");

                if (allfile != null && allfile.Count > 0)
                {
                    pathoutput = configGlobal.FirstOrDefault(x => x.ConfigGlobalName == "PATHBACKUPXMLFILE").ConfigGlobalValue;
                    foreach (var src in allfile)
                    {
                        round += 1;
                        Console.WriteLine("Start round : " + round);
                        billno = src.FileName.Substring(8, (src.FileName.IndexOf('_')) - 8);
                        Console.WriteLine("billno : " + billno);
                        XmlDocument doc = new XmlDocument();
                        doc.Load(src.FullPath);

                        Console.WriteLine("Send To Sign");
                        resultXMLSign = SendFileXMLSign(doc);

                        Console.WriteLine("Status Sign : " + resultXMLSign.ToString());
                        Console.WriteLine("Update Status in DataBase");

                        fileNameDest = src.FileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        pathoutbound = src.Outbound;

                        var dataTran = transactionDescription.GetBilling(billno).Result.FirstOrDefault();
                        billingdate = DateTime.Now;
                        if (dataTran!= null)
                        {
                            billingdate = dataTran.BillingDate ?? DateTime.Now;
                        }
                        pathoutbound += "\\" + billingdate.ToString("YYYY") + "\\" + billingdate.ToString("MM");
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
                        ExportXMLAfterSign( doc, pathoutbound, fullpath);
                        Console.WriteLine("End Export XML file");
                        
                        Console.WriteLine("Start Move file");
                        MoveFile(src.FullPath, src.FileName + fileType, billingdate);
                        Console.WriteLine("End Move file");
                    }
                }
                Console.WriteLine("End XMLSign");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<XMLSignModel> ReadXmlFile()
        {
            List<XMLSignModel> result = new List<XMLSignModel>();
            string[] fullpath = new string[0];
            string pathFolder = "";
            string fileType = "*.xml";
            List<string> listpath;
            XMLSignModel xMLSignModel = new XMLSignModel();

            ConfigXmlSign config = new ConfigXmlSign();
            config.ConfigXmlsignInputPath = @"D:\sign";
            config.ConfigXmlsignOutputPath = @"D:\sign";
            configXmlSign = new List<ConfigXmlSign>();
            configXmlSign.Add(config);
            try
            {
                //pathFolder = @"C:\Code_Dev\sign";
                foreach (var path in configXmlSign)
                {
                    pathFolder = path.ConfigXmlsignInputPath;
                    fullpath = Directory.GetFiles(pathFolder, fileType);
                    listpath = fullpath.ToList();
                    foreach (var item in listpath)
                    {
                        xMLSignModel = new XMLSignModel();
                        xMLSignModel.FullPath = item;
                        xMLSignModel.FileName = Path.GetFileName(item).Replace(".xml", "");
                        xMLSignModel.Outbound = path.ConfigXmlsignOutputPath;
                        xMLSignModel.Inbound = path.ConfigXmlsignInputPath;
                        result.Add(xMLSignModel);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
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
                throw ex;
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
                throw ex;
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
                throw ex;
            }
            return result;
        }

        public void GetDataFromDataBase()
        {
            try
            {
                configXmlSign = configXMLSignController.List().Result;
                configGlobal = configGlobalController.List().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool MoveFile(string pathinput, string filename, DateTime billingdate)
        {
            bool result = false;
            //pathinpput = @"c:\temp\MySample.txt";
            //string pathoutput = @"D:\sign\backupfile\";

            try
            {
                pathoutput += "\\" + billingdate.ToString("YYYY") + "\\" + billingdate.ToString("MM") + "\\";
                if (!File.Exists(pathinput))
                {
                    // This statement ensures that the file is created,  
                    // but the handle is not kept.  
                    using (FileStream fs = File.Create(pathinput)) { }
                }
                // Ensure that the target does not exist.  
                if (!Directory.Exists(pathoutput))
                {
                    Directory.CreateDirectory(pathoutput);
                }
                // Move the file.  
                File.Move(pathinput, pathoutput + filename);
                Console.WriteLine("{0} was moved to {1}.", pathinput, pathoutput);

                // See if the original exists now.  
                if (File.Exists(pathinput))
                {
                    Console.WriteLine("The original file still exists, which is unexpected.");
                }
                else
                {
                    Console.WriteLine("The original file no longer exists, which is expected.");
                }
                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            return result;
        }

    }
}
