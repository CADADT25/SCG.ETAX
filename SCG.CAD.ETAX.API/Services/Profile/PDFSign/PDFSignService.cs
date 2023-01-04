using SCG.CAD.ETAX.MODEL.CustomModel;
using SCG.CAD.ETAX.UTILITY;
using SCG.CAD.ETAX.UTILITY.Controllers;
using System.Text;

namespace SCG.CAD.ETAX.API.Services
{
    public class PDFSignService
    {
        ConfigPdfSignService configPDFSignService = new ConfigPdfSignService();
        TransactionDescriptionService transactionDescriptionService = new TransactionDescriptionService();
        ConfigGlobalService configGlobalService = new ConfigGlobalService();
        LogicToolHelper logicToolHelper = new LogicToolHelper();

        public Response ProcessPDFSign(PDFSignModel pDFSignModel)
        {
            Response res = new Response();
            List<ConfigPdfSign> configPDFSign = new List<ConfigPdfSign>();
            List<ConfigGlobal> configGlobal = new List<ConfigGlobal>();
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
                ConfigPdfSign configPdfSign = pDFSignModel.configPdfSign;
                FilePDF filePDF = pDFSignModel.filePDF;
                res = GetConfigGlobal();
                if (res.STATUS == false)
                {
                    return res;
                }
                configGlobal = (List<ConfigGlobal>)res.OUTPUT_DATA;

                res = transactionDescriptionService.GET_BILLING(filePDF.Billno);
                var dataTran = ((List<TransactionDescription>)res.OUTPUT_DATA).FirstOrDefault();

                res = logicToolHelper.CheckCancelBillingOrSentRevenueDepartment(dataTran);
                if (!res.STATUS)
                {
                    res = PrepareSendPDFSign(configPdfSign, filePDF.FullPath);
                    if (res.STATUS)
                    {
                        dataSend = (APISendFilePDFSignModel)res.OUTPUT_DATA;

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
                res = configGlobalService.GET_LIST();
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
                result.environment = new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("JavaApi")["Env"];
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

        public Response UpdateStatusAfterSignPDF(APIResponseSignModel pdfsign, string billno, string pathfile, TransactionDescription dataTran, string beforesignfilepath, string comcode)
        {
            Response res = new Response();
            res.STATUS = false;
            try
            {
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

                        res = transactionDescriptionService.INSERT(dataTran);
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

                        res = transactionDescriptionService.INSERT(dataTran);
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

                            res = transactionDescriptionService.UPDATE(dataTran);
                        }
                        else
                        {
                            dataTran.PdfSignDateTime = DateTime.Now;
                            dataTran.PdfSignDetail = "PDF was signed Failed";
                            dataTran.PdfSignStatus = "Failed";
                            dataTran.UpdateBy = "Batch";
                            dataTran.UpdateDate = DateTime.Now;

                            res = transactionDescriptionService.UPDATE(dataTran);
                        }
                    }
                    else
                    {
                        dataTran.PdfSignDateTime = DateTime.Now;
                        dataTran.PdfSignDetail = "PDF was signed Failed";
                        dataTran.PdfSignStatus = "Failed";
                        dataTran.UpdateBy = "Batch";
                        dataTran.UpdateDate = DateTime.Now;

                        res = transactionDescriptionService.UPDATE(dataTran);
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
                var json = System.Text.Json.JsonSerializer.Serialize(data);
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
    }
}
