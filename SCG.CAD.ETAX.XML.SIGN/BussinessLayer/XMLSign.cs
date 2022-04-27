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
        List<ConfigXmlSign> configXmlSign = new List<ConfigXmlSign>();

        public void XML_Sign()
        {
            string folderDest = @"D:/";
            string fileNameDest = "";
            string fileType = ".xml";
            bool resultXMLSign = false;
            int billno = 0;
            int round = 0;
            try
            {
                Console.WriteLine("Start XMLSign");

                GetDataFromDataBase();
                Console.WriteLine("Start Read All XMLFile");
                var allfile = ReadXmlFile();
                Console.WriteLine("End Read All XMLFile");

                if (allfile != null && allfile.Count > 0)
                {
                    foreach (var src in allfile)
                    {
                        round += 1;
                        Console.WriteLine("Start round : " + round);

                        billno = Convert.ToInt32(src.FileName.Substring(7, src.FileName.IndexOf('-') + 1));
                        Console.WriteLine("billno : " + billno);
                        XmlDocument doc = new XmlDocument();
                        doc.Load(src.FullPath);

                        Console.WriteLine("Send To Sign");
                        resultXMLSign = SendFileXMLSign(doc);

                        Console.WriteLine("Status Sign : " + resultXMLSign.ToString());
                        Console.WriteLine("Update Status in DataBase");
                        UpdateStatusAfterSignXML(resultXMLSign, billno);

                        Console.WriteLine("Start Export XML file");
                        fileNameDest = src.FileName + "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        ExportXMLAfterSign(resultXMLSign, doc, src.Outbound, fileNameDest);
                        Console.WriteLine("End Export XML file");

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
            List<string> listpath;
            XMLSignModel xMLSignModel = new XMLSignModel();
            try
            {
                //pathFolder = @"C:\Code_Dev\sign";
                foreach (var path in configXmlSign)
                {
                    pathFolder = path.ConfigXmlsignInputPath;
                    string fileType = "*.xml";
                    fullpath = Directory.GetFiles(pathFolder, fileType);
                    listpath = fullpath.ToList();
                    foreach (var item in listpath)
                    {
                        xMLSignModel = new XMLSignModel();
                        xMLSignModel.FullPath = item;
                        xMLSignModel.FileName = Path.GetFileName(item).Replace(".pdf", "");
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

        public bool UpdateStatusAfterSignXML(bool status, int billno)
        {
            bool result = false;
            string jsondata = "";
            try
            {
                Task<Response> res;
                TransactionDescriptionController transactionDescription = new TransactionDescriptionController();
                var dataTran = transactionDescription.GetBilling(billno).Result.FirstOrDefault();
                if (status)
                {
                    dataTran.XmlSignDateTime = DateTime.Now;
                    dataTran.XmlSignDetail = "XML was signed completely";
                    dataTran.XmlSignStatus = "Successful";
                    dataTran.UpdateBy = "Batch";
                    dataTran.UpdateDate = DateTime.Now;

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

        public bool ExportXMLAfterSign(bool resultXMLSign, XmlDocument doc, string pathoutbound, string filename)
        {
            bool result = false;
            try
            {
                if (resultXMLSign)
                {
                    pathoutbound += "/Success/";
                }
                else
                {
                    pathoutbound += "/Fail/";
                }
                TextWriter filestream = new StreamWriter(pathoutbound + filename);
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
