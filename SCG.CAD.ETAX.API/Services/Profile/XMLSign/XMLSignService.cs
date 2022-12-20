﻿using SCG.CAD.ETAX.MODEL.CustomModel;
using SCG.CAD.ETAX.UTILITY;
using System.Text;
using System.Text.Json;

namespace SCG.CAD.ETAX.API.Services
{
    public class XMLSignService : DatabaseExecuteController
    {
        ConfigGlobalService configGlobalService = new ConfigGlobalService();
        TransactionDescriptionService transactionDescriptionService = new TransactionDescriptionService();
        LogicToolHelper logicToolHelper = new LogicToolHelper();

        List<ConfigGlobal> configGlobal = new List<ConfigGlobal>();

        string pathoutput;
        string namepathlog = "PATHLOGFILE_XMLSIGN";
        string batchname = "SCG.CAD.ETAX.XML.SIGN";

        public Response ProcessXMLFileSign(XMLSignModel xMLSignModel)
        //public Response ProcessXMLFileSign(ConfigXmlSign configXmlSign, FileXML fileXML)
        {
            Response res = new Response();
            APISendFileXMLSignModel dataSend = new APISendFileXMLSignModel();
            APIResponseSignModel resultXMLSign = new APIResponseSignModel();

            string fullpath = "";
            string fileNameDest = "";
            string fileType = ".xml";
            string pathoutbound = "";
            DateTime billingdate;
            try
            {
                ConfigXmlSign configXmlSign = xMLSignModel.configXmlSign;
                FileXML fileXML = xMLSignModel.fileXML;
                res = GetConfigGlobal();
                if (res.STATUS == false)
                {
                    return res;
                }
                configGlobal = (List<ConfigGlobal>)res.OUTPUT_DATA;

                res = transactionDescriptionService.GET_BILLING(fileXML.Billno);
                if (res.STATUS)
                {
                    var dataTran = ((List<TransactionDescription>)res.OUTPUT_DATA).FirstOrDefault();
                    if (dataTran != null)
                    {

                        if (!logicToolHelper.CheckCancelBillingOrSentRevenueDepartment(dataTran).STATUS)
                        {
                            res = PrepareSendXMLSign(configXmlSign, fileXML.FullPath, dataTran.DocType);
                            dataSend = (APISendFileXMLSignModel)res.OUTPUT_DATA;
                            if (dataSend != null)
                            {
                                res = SendFileXMLSignAsync(dataSend).Result;
                                resultXMLSign = (APIResponseSignModel)res.OUTPUT_DATA;

                                fileNameDest = fileXML.FileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                                pathoutbound = fileXML.Outbound;
                                pathoutput = fileXML.Outbound += "\\BeforeSign\\";
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

                                res = UpdateStatusAfterSignXML(resultXMLSign, fileXML.Billno, fullpath, dataTran, pathoutput + fileNameDest + fileType);
                                if (res.STATUS)
                                {
                                    if (!string.IsNullOrEmpty(resultXMLSign.resultCode) && resultXMLSign.resultCode == "000")
                                    {
                                        res = ExportXMLAfterSign(resultXMLSign.fileSigned, pathoutbound, fullpath);
                                    }
                                    else
                                    {
                                        res = ExportXMLAfterSign(dataSend.fileEncode, pathoutbound, fullpath);
                                    }

                                    if (res.STATUS)
                                    {
                                        res = logicToolHelper.MoveFile(fileXML.FullPath, fileNameDest + fileType, billingdate, pathoutput);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        res.STATUS = false;
                        res.ERROR_MESSAGE = "Not Found Data Transaction";
                    }
                }
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.Message;
            }
            return res;
        }

        public Response GetConfigGlobal()
        {
            Response res = new Response();
            try
            {
                res = configGlobalService.GET_LIST();
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.ToString();
            }
            return res;
        }

        public Response PrepareSendXMLSign(ConfigXmlSign config, string filepath, string documentType)
        {
            Response res = new Response();
            res.STATUS = true;
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
                res.OUTPUT_DATA = result;
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.Message.ToString();
            }
            return res;
        }

        public async Task<Response> SendFileXMLSignAsync(APISendFileXMLSignModel data)
        {
            Response res = new Response();
            res.STATUS = false;
            APIResponseSignModel tran = new APIResponseSignModel();
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(data);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var task = await Task.Run(() => ApiHelper.PostURI("api/APISign/SendXMLSign", httpContent));
                if (task.OUTPUT_DATA != null)
                {
                    tran = Newtonsoft.Json.JsonConvert.DeserializeObject<APIResponseSignModel>(task.OUTPUT_DATA.ToString());
                    res.STATUS = true;
                }
                else
                {
                    tran.resultDes = "API Signed Fail";
                    res.ERROR_MESSAGE = "API Signed Fail";
                }
                res.OUTPUT_DATA = tran;

            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.Message.ToString();
            }
            return res;
        }

        public Response UpdateStatusAfterSignXML(APIResponseSignModel xmlsign, string billno, string pathfile, TransactionDescription dataTran, string beforesignfilepath)
        {
            Response res = new Response();
            res.STATUS = false;
            string jsondata = "";
            try
            {
                dataTran.XmlBeforeSignLocation = beforesignfilepath;
                dataTran.XmlSignLocation = pathfile;
                if (!string.IsNullOrEmpty(xmlsign.resultCode) && xmlsign.resultCode.Equals("000"))
                {
                    dataTran.XmlSignDateTime = DateTime.Now;
                    dataTran.XmlSignDetail = "XML was signed completely";
                    dataTran.XmlSignStatus = "Successful";
                    dataTran.UpdateBy = "Batch";
                    dataTran.UpdateDate = DateTime.Now;

                    res = transactionDescriptionService.UPDATE(dataTran);
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

                    res = transactionDescriptionService.UPDATE(dataTran);
                }
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.Message;
            }
            return res;
        }

        public Response ExportXMLAfterSign(string filexml, string pathoutbound, string fullpath)
        {
            Response res = new Response();
            res.STATUS = false;
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
                res.STATUS = true;
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.Message;
            }
            return res;
        }
    }
}
