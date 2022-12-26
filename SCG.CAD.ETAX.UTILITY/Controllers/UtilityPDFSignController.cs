using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.MODEL.CustomModel;
using SCG.CAD.ETAX.MODEL.etaxModel;
using System.Text;
using System.Text.Json;

namespace SCG.CAD.ETAX.UTILITY.Controllers
{
    public class UtilityPDFSignController
    {
        UtilityConfigPDFSignController configPDFSignController = new UtilityConfigPDFSignController();
        UtilityTransactionDescriptionController transactionDescription = new UtilityTransactionDescriptionController();
        UtilityConfigGlobalController configGlobalController = new UtilityConfigGlobalController();
        UtilityAPISignController signPDFController = new UtilityAPISignController();
        LogicToolHelper logicToolHelper = new LogicToolHelper(); 

        public Response ProcessPDFSign(ConfigPdfSign configPdfSign, FilePDF filePDF)
        {
            Response res = new Response();
            List<ConfigPdfSign> configPDFSign = new List<ConfigPdfSign>();
            List<ConfigGlobal> configGlobal = new List<ConfigGlobal>();
            List<TransactionDescription> datatransactionDescription = new List<TransactionDescription>();
            APISendFilePDFSignModel dataSend = new APISendFilePDFSignModel();
            APIResponseSignModel resultPDFSign = new APIResponseSignModel();

            string pathoutput;
            string namepathlog = "PATHLOGFILE_PDFSIGN";
            string batchname = "SCG.CAD.ETAX.PDF.SIGN";
            string fileNameDest = "";
            string fullpath = "";
            string pathoutbound = "";
            string fileType = ".pdf";
            DateTime billingdate;

            try
            {
                res = GetConfigGlobal();
                if (res.STATUS == false)
                {
                    return res;
                }
                configGlobal = (List<ConfigGlobal>)res.OUTPUT_DATA;
                                                
                res = GetTransactionDescription();
                if (res.STATUS == false)
                {
                    return res;
                }
                datatransactionDescription = (List<TransactionDescription>)res.OUTPUT_DATA;

                var dataTran = transactionDescription.GetBilling(filePDF.Billno).Result.FirstOrDefault();
                //var dataTran = datatransactionDescription.FirstOrDefault(x => x.BillingNumber == billno);
                res = logicToolHelper.CheckCancelBillingOrSentRevenueDepartment(dataTran);
                if (!res.STATUS)
                {
                    res = PrepareSendPDFSign(configPdfSign, filePDF.FullPath);
                    if (res.STATUS)
                    {
                        dataSend = (APISendFilePDFSignModel)res.OUTPUT_DATA;

                        //res = SendFilePDFSign(dataSend);
                        res = SendFilePDFSignAsync(dataSend).Result;
                        resultPDFSign = (APIResponseSignModel)res.OUTPUT_DATA;

                        fileNameDest = filePDF.FileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        pathoutbound = filePDF.Outbound;
                        pathoutput = filePDF.Outbound += "\\BeforeSign\\";

                        billingdate = DateTime.Now;
                        if (dataTran != null)
                        {
                            billingdate = dataTran.BillingDate ?? DateTime.Now;
                        }
                        if (resultPDFSign.resultCode == "000")
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

                        res = UpdateStatusAfterSignPDF(resultPDFSign, filePDF.Billno, fullpath, dataTran, pathoutput + fileNameDest + fileType, filePDF.Comcode);
                        if (res.STATUS)
                        {
                            if (!string.IsNullOrEmpty(resultPDFSign.resultCode) && resultPDFSign.resultCode == "000")
                            {
                                res = ExportPDFAfterSign(resultPDFSign.fileSigned, pathoutbound, fullpath);
                            }
                            else
                            {
                                res = ExportPDFAfterSign(dataSend.fileEncode, pathoutbound, fullpath);
                            }

                            if (res.STATUS)
                            {
                                res = logicToolHelper.MoveFile(filePDF.FullPath, fileNameDest + fileType, billingdate, pathoutput);
                            }
                        }
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
                res.STATUS = true;
                res.OUTPUT_DATA = configGlobalController.List().Result;
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.ToString();
            }
            return res;
        }
        public Response GetTransactionDescription()
        {
            Response res = new Response();
            try
            {
                res.STATUS = true;
                res.OUTPUT_DATA = transactionDescription.List().Result;
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.ToString();
            }
            return res;
        }

        public Response PrepareSendPDFSign(ConfigPdfSign config, string filepath)
        {
            Response res = new Response();
            res.STATUS = true;
            APISendFilePDFSignModel result = new APISendFilePDFSignModel();
            EncodeHelper encodeHelper = new EncodeHelper();
            int pdfsignUlx;
            int pdfsignUly;
            int fontSize;
            try
            {
                result.environment = "0";
                result.hsmName = config.ConfigPdfsignHsmModule;
                result.hsmSerial = config.ConfigPdfsignHsmSerial;
                result.slotPassword = encodeHelper.Base64Decode(config.ConfigPdfsignHsmPassword);
                result.keyAlias = config.ConfigPdfsignKeyAlias;
                result.certSerial = config.ConfigPdfsignCertificateSerial;
                result.signatureType = "text";
                result.coordinateUpperLeftX = 200;
                result.coordinateUpperLeftY = 700;
                result.fontSize = 8;
                if (int.TryParse(config.ConfigPdfsignUlx, out pdfsignUlx))
                {
                    result.coordinateUpperLeftX = pdfsignUlx;
                }
                if (int.TryParse(config.ConfigPdfsignUly, out pdfsignUly))
                {
                    result.coordinateUpperLeftY = pdfsignUly;
                }
                if (int.TryParse(config.ConfigPdfsignFontSize, out fontSize))
                {
                    result.fontSize = fontSize;
                }
                result.page = config.ConfigPdfsignPage;
                result.fontName = config.ConfigPdfsignFontName;
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

        public Response SendFilePDFSign(APISendFilePDFSignModel data)
        {
            Response res = new Response();
            try
            {
                res = signPDFController.SendFilePDFSign(data).Result;
                res.STATUS = true;
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.Message.ToString();
            }
            return res;
        }

        public Response UpdateStatusAfterSignPDF(APIResponseSignModel pdfsign, string billno, string pathfile, TransactionDescription dataTran, string beforesignfilepath, string comcode)
        {
            Response res = new Response();
            res.STATUS = false;
            try
            {
                Task<Response> resp;
                if (dataTran == null)
                {
                    dataTran = new TransactionDescription();

                    dataTran.BillingNumber = Convert.ToString(billno);
                    dataTran.CompanyCode = comcode;
                    dataTran.CreateBy = "Batch";
                    dataTran.CreateDate = DateTime.Now;
                    dataTran.TypeInput = "Batch";
                    dataTran.UpdateBy = "Batch";
                    dataTran.UpdateDate = DateTime.Now;
                    dataTran.GenerateStatus = "Waiting";
                    dataTran.PdfIndexingStatus = "Waiting";
                    dataTran.PrintStatus = "Waiting";
                    dataTran.XmlCompressStatus = "Waiting";
                    dataTran.XmlSignStatus = "Waiting";
                    dataTran.EmailSendStatus = "Waiting";
                    dataTran.DmsAttachmentFileName = null;
                    dataTran.DmsAttachmentFilePath = null;
                    dataTran.PdfBeforeSignLocation = beforesignfilepath;
                    if (pdfsign.resultCode.Equals("000"))
                    {
                        dataTran.PdfSignDateTime = DateTime.Now;
                        dataTran.PdfSignDetail = "PDF was signed completely";
                        dataTran.PdfSignStatus = "Successful";
                        dataTran.UpdateBy = "Batch";
                        dataTran.UpdateDate = DateTime.Now;
                        dataTran.PdfSignLocation = pathfile;

                        var json = System.Text.Json.JsonSerializer.Serialize(dataTran);
                        resp = transactionDescription.Insert(json);
                        if (resp.Result.MESSAGE == "Insert success.")
                        {
                            res.STATUS = true;
                        }
                    }
                    else
                    {
                        dataTran.PdfSignDateTime = DateTime.Now;
                        dataTran.PdfSignDetail = pdfsign.resultDes;
                        dataTran.PdfSignStatus = "Failed";
                        dataTran.UpdateBy = "Batch";
                        dataTran.UpdateDate = DateTime.Now;
                        dataTran.PdfSignLocation = pathfile;
                        dataTran.PdfBeforeSignLocation = beforesignfilepath;

                        var json = System.Text.Json.JsonSerializer.Serialize(dataTran);
                        resp = transactionDescription.Insert(json);
                        if (resp.Result.MESSAGE == "Insert success.")
                        {
                            res.STATUS = true;
                        }
                    }
                }
                else
                {
                    dataTran.CompanyCode = comcode;
                    dataTran.PdfSignLocation = pathfile;
                    dataTran.PdfBeforeSignLocation = beforesignfilepath;
                    if (!string.IsNullOrEmpty(pdfsign.resultCode))
                    {
                        if (pdfsign.resultCode.Equals("000"))
                        {
                            dataTran.PdfSignDateTime = DateTime.Now;
                            dataTran.PdfSignDetail = "PDF was signed completely";
                            dataTran.PdfSignStatus = "Successful";
                            dataTran.UpdateBy = "Batch";
                            dataTran.UpdateDate = DateTime.Now;

                            var json = System.Text.Json.JsonSerializer.Serialize(dataTran);
                            resp = transactionDescription.Update(json);
                            if (resp.Result.MESSAGE == "Updated Success.")
                            {
                                res.STATUS = true;
                            }
                        }
                        else
                        {
                            dataTran.PdfSignDateTime = DateTime.Now;
                            dataTran.PdfSignDetail = "PDF was signed Failed";
                            dataTran.PdfSignStatus = "Failed";
                            dataTran.UpdateBy = "Batch";
                            dataTran.UpdateDate = DateTime.Now;

                            var json = System.Text.Json.JsonSerializer.Serialize(dataTran);
                            resp = transactionDescription.Update(json);
                            if (resp.Result.MESSAGE == "Updated Success.")
                            {
                                res.STATUS = true;
                            }
                        }
                    }
                    else
                    {
                        dataTran.PdfSignDateTime = DateTime.Now;
                        dataTran.PdfSignDetail = "PDF was signed Failed";
                        dataTran.PdfSignStatus = "Failed";
                        dataTran.UpdateBy = "Batch";
                        dataTran.UpdateDate = DateTime.Now;

                        var json = System.Text.Json.JsonSerializer.Serialize(dataTran);
                        resp = transactionDescription.Update(json);
                        if (resp.Result.MESSAGE == "Updated Success.")
                        {
                            res.STATUS = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.ERROR_MESSAGE = ex.Message.ToString();
            }
            return res;
        }
        
        public Response ExportPDFAfterSign(string filepdf, string pathoutbound, string fullpath)
        {
            Response res = new Response();
            res.STATUS = false;
            try
            {
                if (!Directory.Exists(pathoutbound))
                {
                    Directory.CreateDirectory(pathoutbound);
                }
                byte[] bytes = Convert.FromBase64String(filepdf);
                FileStream stream = new FileStream(fullpath, FileMode.CreateNew);
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(bytes, 0, bytes.Length);
                writer.Close();

                res.STATUS = true;
            }
            catch (Exception ex)
            {
                res.ERROR_MESSAGE = ex.Message.ToString();
            }
            return res;
        }

        public async Task<Response> SendFilePDFSignAsync(APISendFilePDFSignModel data)
        {
            Response res = new Response();
            res.STATUS = false;
            APIResponseSignModel tran = new APIResponseSignModel();
            try
            {
                var json = JsonSerializer.Serialize(data);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var task = await Task.Run(() => ApiHelper.PostURI("api/APISign/SendPDFSign", httpContent));
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
                res.ERROR_MESSAGE = ex.Message.ToString();
            }
            return res;
        }

        public async Task<Response> SendProcessPDFSign(PDFSignModel pDFSignModel)
        {
            var jsonString = JsonSerializer.Serialize(pDFSignModel);
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/PDFSign/ProcessPDFSign", httpContent));

            //JsonResult Json = new JsonResult(task);
            return task;
        }
    }
}
