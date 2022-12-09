using SCG.CAD.ETAX.MODEL.etaxModel;
using System.Text.Json;
using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.XML.SIGN.Models;
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
        LogHelper log = new LogHelper();
        LogicToolHelper logicToolHelper = new LogicToolHelper();

        List<ConfigXmlSign> configXmlSign = new List<ConfigXmlSign>();
        List<ConfigGlobal> configGlobal = new List<ConfigGlobal>();
        string pathoutput;
        string pathlog = @"C:\log\";
        string namepathlog = "PATHLOGFILE_XMLSIGN";
        string batchname = "SCG.CAD.ETAX.XML.SIGN";

        public void ProcessXMLSign()
        {
            string fullpath = "";
            string fileNameDest = "";
            APIResponseSignModel resultXMLSign = new APIResponseSignModel();
            int round = 0;
            string fileType = ".xml";
            string pathoutbound = "";
            DateTime billingdate;
            APISendFileXMLSignModel dataSend = new APISendFileXMLSignModel();
            DateTime nexttime;
            try
            {
                Console.WriteLine("Start XMLSign");

                GetDataFromDataBase();
                Console.WriteLine("Start Read All XMLFile");
                log.InsertLog(pathlog, "Start Read All XMLFile");

                foreach (var config in configXmlSign)
                {
                    //if (logicToolHelper.CheckRunTime(config.ConfigXmlsignNextTime))
                    //{
                    var allfile = ReadXmlFile(config);

                    if (allfile != null && allfile.listFileXMLs != null)
                    {
                        //pathoutput = configGlobal.FirstOrDefault(x => x.ConfigGlobalName == "PATHBACKUPXMLFILE").ConfigGlobalValue;
                        if (allfile.listFileXMLs.Count > 0)
                        {
                            foreach (var file in allfile.listFileXMLs)
                            {
                                round += 1;
                                Console.WriteLine("Start round : " + round);
                                log.InsertLog(pathlog, "Start round : " + round);
                                Console.WriteLine("billno : " + file.Billno);
                                log.InsertLog(pathlog, "billno : " + file.Billno);

                                Console.WriteLine("Prepare XML");
                                log.InsertLog(pathlog, "Prepare XML");
                                var dataTran = transactionDescription.GetBilling(file.Billno).Result.FirstOrDefault();
                                if (!CheckCancelBillingOrSentRevenueDepartment(dataTran))
                                {
                                    dataSend = PrepareSendXMLSign(allfile.configPdfSign, file.FullPath, dataTran.DocType);
                                    if (dataSend != null)
                                    {
                                        Console.WriteLine("Send To Sign");
                                        log.InsertLog(pathlog, "Send To Sign");
                                        resultXMLSign = SendFileXMLSignAsync(dataSend).Result;

                                        Console.WriteLine("Status Sign : " + resultXMLSign.resultDes.ToString());
                                        log.InsertLog(pathlog, "Status Sign : " + resultXMLSign.resultDes.ToString());
                                        Console.WriteLine("Update Status in DataBase");
                                        log.InsertLog(pathlog, "Update Status in DataBase");

                                        fileNameDest = file.FileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                                        pathoutbound = file.Outbound;
                                        pathoutput = file.Outbound += "\\BeforeSign\\";
                                        billingdate = DateTime.Now;
                                        if (dataTran != null)
                                        {
                                            billingdate = dataTran.BillingDate ?? DateTime.Now;
                                        }
                                        if (resultXMLSign.resultCode == "000")
                                        {
                                            pathoutbound += "\\Success\\";
                                        }
                                        else
                                        {
                                            pathoutbound += "\\Fail\\";
                                        }
                                        pathoutbound += billingdate.ToString("yyyy") + "\\" + billingdate.ToString("MM") + "\\";
                                        pathoutput += "\\" + billingdate.ToString("yyyy") + "\\" + billingdate.ToString("MM") + "\\";
                                        fullpath = pathoutbound + fileNameDest + fileType;

                                        UpdateStatusAfterSignXML(resultXMLSign, file.Billno, fullpath, dataTran, pathoutput + fileNameDest + fileType);

                                        Console.WriteLine("Start Export XML file");
                                        log.InsertLog(pathlog, "Start Export XML file");
                                        if (resultXMLSign.resultCode == "000")
                                        {
                                            ExportXMLAfterSign(resultXMLSign.fileSigned, pathoutbound, fullpath);
                                        }
                                        else
                                        {
                                            ExportXMLAfterSign(dataSend.fileEncode, pathoutbound, fullpath);
                                        }
                                        Console.WriteLine("End Export XML file");
                                        log.InsertLog(pathlog, "End Export XML file");
                                        Console.WriteLine("Start Move file");
                                        log.InsertLog(pathlog, "Start Move file");
                                        MoveFile(file.FullPath, fileNameDest + fileType, billingdate, pathoutput);
                                        Console.WriteLine("End Move file");
                                        log.InsertLog(pathlog, "End Move file");
                                    }
                                }
                            }
                        }
                    }
                    //    nexttime = logicToolHelper.SetNextRunTime(config.ConfigXmlsignAnyTime, config.ConfigXmlsignOneTime, batchname, config.ConfigXmlsignNo);
                    //    Console.WriteLine("Set NextTime : " + nexttime);
                    //    log.InsertLog(pathlog, "Set NextTime : " + nexttime);
                    //}
                }

                Console.WriteLine("End Read All XMLFile");
                log.InsertLog(pathlog, "End Read All XMLFile");
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
            ListFileXML xmlDetail = new ListFileXML();
            DirectoryInfo directoryInfo;
            string billno = "";
            string filename = "";
            try
            {
                xMLSignModel = new XMLSignModel();
                xMLSignModel.configPdfSign = config;
                xMLSignModel.listFileXMLs = new List<ListFileXML>();
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
                        xmlDetail = new ListFileXML();
                        xmlDetail.FullPath = item.FullName;
                        xmlDetail.FileName = filename;
                        xmlDetail.Outbound = config.ConfigXmlsignOutputPath;
                        xmlDetail.Inbound = config.ConfigXmlsignInputPath;
                        xmlDetail.Billno = billno;
                        xMLSignModel.listFileXMLs.Add(xmlDetail);
                    }
                    result = xMLSignModel;
                    Console.WriteLine("Path Found PDF : " + xMLSignModel.listFileXMLs.Count + " files");
                    log.InsertLog(pathlog, "Path Found PDF : " + xMLSignModel.listFileXMLs.Count + " files");
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

        public async Task<APIResponseSignModel> SendFileXMLSignAsync(APISendFileXMLSignModel data)
        {
            APIResponseSignModel tran = new APIResponseSignModel();
            try
            {
                var json = JsonSerializer.Serialize(data);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var task = await Task.Run(() => ApiHelper.PostURI("api/APISign/SendXMLSign", httpContent));
                if (task.OUTPUT_DATA != null)
                {
                    tran = Newtonsoft.Json.JsonConvert.DeserializeObject<APIResponseSignModel>(task.OUTPUT_DATA.ToString());
                }
                else
                {
                    tran.resultDes = "fail";
                }

                //result = signXMLController.SendFileXMLSign(json).Result;
                ////result.fileSigned = null;
                ////result.resultCode = "000";
                ////result.resultDes = "Success";
                //log.InsertLog(pathlog, "Result : " + result.resultDes);
                //log.InsertLog(pathlog, "ResultCode : " + result.resultCode);
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
                tran = new APIResponseSignModel();
                tran.resultDes = ex.ToString();
            }
            return tran;
        }

        public bool UpdateStatusAfterSignXML(APIResponseSignModel xmlsign, string billno, string pathfile, TransactionDescription dataTran, string beforesignfilepath)
        {
            bool result = false;
            string jsondata = "";
            try
            {
                Task<Response> res;
                dataTran.XmlBeforeSignLocation = beforesignfilepath;
                if (xmlsign.resultCode != null && xmlsign.resultCode.Equals("000"))
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
                    dataTran.XmlSignDetail = xmlsign.resultDes;
                    if (xmlsign.resultDes.Length > 255)
                    {
                        dataTran.XmlSignDetail = (xmlsign.resultDes).Substring(0, 254);
                    }
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

        public bool ExportXMLAfterSign(string filexml, string pathoutbound, string fullpath)
        {
            bool result = false;
            try
            {
                if (!Directory.Exists(pathoutbound))
                {
                    Directory.CreateDirectory(pathoutbound);
                }
                byte[] bytes = Convert.FromBase64String(filexml);
                FileStream stream = new FileStream(fullpath, FileMode.CreateNew);
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(bytes, 0, bytes.Length);
                writer.Close();
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

        public bool MoveFile(string pathinput, string filename, DateTime billingdate, string output)
        {
            bool result = false;

            try
            {
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

                // See if the original exists now.  
                if (File.Exists(output + filename))
                {
                    File.Delete(output + filename);
                }
                // Move the file.  
                File.Move(pathinput, output + filename);
                Console.WriteLine("{0} was moved to {1}.", pathinput, output);
                log.InsertLog(pathlog, pathinput + " was moved to " + output);
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("The process failed: {0}", ex.ToString());
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }

        public APISendFileXMLSignModel PrepareSendXMLSign(ConfigXmlSign config, string filepath, string documentType)
        {
            APISendFileXMLSignModel result = new APISendFileXMLSignModel();
            EncodeHelper encodeHelper = new EncodeHelper();
            try
            {
                result.environment = "0";
                result.hsmName = config.ConfigXmlsignHsmModule;
                result.hsmSerial = config.ConfigXmlsignHsmSerial;
                result.slotPassword = encodeHelper.Base64Decode(config.ConfigXmlsignHsmPassword);
                result.keyAlias = config.ConfigXmlsignKeyAlias;
                result.certSerial = config.ConfigXmlsignCertificateSerial;
                result.documentType = documentType;

                //result.hsmName = "pse";
                //result.hsmSerial = "571271:28593";
                //result.slotPassword = "P@ssw0rd1";
                //result.keyAlias = "NEW06391012205001173_200916150834";
                //result.certSerial = "5c6e65dc1b7b9da8";
                //result.documentType = "388";
                result.fileEncode = logicToolHelper.ConvertFileToEncodeBase64(filepath);

            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }
        public bool CheckCancelBillingOrSentRevenueDepartment(TransactionDescription datatran)
        {
            bool result = false;
            try
            {
                if (datatran != null)
                {
                    if (logicToolHelper.ConvertIntToBoolean(datatran.CancelBilling) || logicToolHelper.ConvertIntToBoolean(datatran.SentRevenueDepartment))
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
