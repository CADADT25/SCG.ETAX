using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using SCG.CAD.ETAX.API.Services.SignDocument;
using SCG.CAD.ETAX.MODEL.CustomModel;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY;
using SCG.CAD.ETAX.UTILITY.Controllers;

namespace SCG.CAD.ETAX.API.Repositories
{
    public class SignDocumentRepository : ISignDocumentRepository
    {
        //UtilityAPISignController utilityAPISignController = new UtilityAPISignController();
        UtilityPDFSignController utilityPDFSignController = new UtilityPDFSignController();
        UtilityXMLSignController utilityXMLSignController = new UtilityXMLSignController();

        SignDocumentService service = new SignDocumentService();
        public async Task<Response> Sign(SignDocumentRequest req)
        {
            Response resp = new Response();
            var res = new SignDocumentResponse();
            resp.STATUS = false;
            try
            {
                //check string empty
                if (string.IsNullOrEmpty(req.TextEncodeBase64))
                {
                    resp.CODE = "103";
                    resp.MESSAGE = "TextEncodeBase64 is required.";
                    return await Task.FromResult(resp);
                }
                if (string.IsNullOrEmpty(req.PdfEncodeBase64))
                {
                    resp.CODE = "103";
                    resp.MESSAGE = "PdfEncodeBase64 is required.";
                    return await Task.FromResult(resp);
                }
                // check text & pdf file
                // "ConfigGlobal/GetDetailByName?cate=REQEUST&name=RESIGN_NEWTRANS_TEMP_PATH";
                string tempPath = service.GetConfigGlobal("RESIGN_NEWTRANS_TEMP_PATH");

                string textPathTemp = Path.Combine(tempPath, "api", DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"), req.TextFileName);
                string textPath = service.CreateFileFromBase64(req.TextEncodeBase64, textPathTemp);

                string pdfPathTemp = Path.Combine(tempPath, "api", DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"), req.PdfFileName);
                string pdfPath = service.CreateFileFromBase64(req.PdfEncodeBase64, pdfPathTemp);

                if (string.IsNullOrEmpty(textPath))
                {
                    resp.CODE = "103";
                    resp.MESSAGE = "TextEncodeBase64 unable to decode.";
                    return await Task.FromResult(resp);
                }
                if (string.IsNullOrEmpty(pdfPath))
                {
                    resp.CODE = "103";
                    resp.MESSAGE = "TextEncodeBase64 unable to decode.";
                    return await Task.FromResult(resp);
                }
                //// gen xml
                //var errorMsg = service.GenerateTextToXml(textPath);
                //if (!string.IsNullOrEmpty(errorMsg))
                //{
                //    resp.CODE = "103";
                //    resp.MESSAGE = errorMsg;
                //    return await Task.FromResult(resp);
                //}
                // sign xml
                var configXmlSign = service.GetConfigXmlSign(req.CompanyCode);
                if (configXmlSign == null)
                {
                    resp.CODE = "103";
                    resp.MESSAGE = "ConfigXmlSign is not found.";
                    return await Task.FromResult(resp);
                }
                var xmlFileDetail = new FileXML();
                xmlFileDetail.FullPath = "";
                xmlFileDetail.FileName = "";
                xmlFileDetail.Outbound = configXmlSign.ConfigXmlsignOutputPath;
                xmlFileDetail.Inbound = configXmlSign.ConfigXmlsignInputPath;
                xmlFileDetail.Billno = req.BillingNo;
                var resXmlSign = utilityXMLSignController.ProcessXMLSign(configXmlSign, xmlFileDetail);
                if (!resXmlSign.STATUS)
                {
                    resp.CODE = "103";
                    resp.MESSAGE = "Unable to sign Xml.";
                    resp.ERROR_MESSAGE = resXmlSign.ERROR_MESSAGE;
                    return await Task.FromResult(resp);
                }
                else
                {
                    var tran = service.GetTransactionDescription(req.BillingNo);
                    if (tran != null)
                    {
                        if (tran.XmlSignStatus == "Successful")
                        {
                            LogicToolHelper logicToolHelper = new LogicToolHelper();
                            res.XmlSignedEncodeBase64 = logicToolHelper.ConvertFileToEncodeBase64(tran.XmlSignStatus);
                        }
                        else
                        {
                            resp.CODE = "103";
                            resp.MESSAGE = tran.XmlSignDetail;
                            return await Task.FromResult(resp);
                        }
                    }
                    else
                    {
                        resp.CODE = "103";
                        resp.MESSAGE = "Billing is not found.";
                        return await Task.FromResult(resp);
                    }
                }

                // sign pdf
                var configPdfSign = service.GetConfigPdfSign(req.CompanyCode);
                if (configPdfSign == null)
                {
                    resp.CODE = "103";
                    resp.MESSAGE = "CofigPdfSign is not found.";
                    return await Task.FromResult(resp);
                }
                var pdfFileDetail = new FilePDF();
                pdfFileDetail.FullPath = pdfPath;
                pdfFileDetail.FileName = req.PdfFileName.Replace("." + req.PdfFileName.Split(".").Last(), "");
                pdfFileDetail.Outbound = configPdfSign.ConfigPdfsignOutputPath;
                pdfFileDetail.Inbound = configPdfSign.ConfigPdfsignInputPath;
                pdfFileDetail.Billno = req.BillingNo;
                var resPdfSign = utilityPDFSignController.ProcessPDFSign(configPdfSign, pdfFileDetail);
                if (!resPdfSign.STATUS)
                {
                    resp.CODE = "103";
                    resp.MESSAGE = "Unable to sign Pdf.";
                    resp.ERROR_MESSAGE = resPdfSign.ERROR_MESSAGE;
                    return await Task.FromResult(resp);
                }
                else
                {
                    var tran = service.GetTransactionDescription(req.BillingNo);
                    if(tran != null)
                    {
                        if(tran.PdfSignStatus == "Successful")
                        {
                            LogicToolHelper logicToolHelper = new LogicToolHelper();
                            res.PdfSignedEncodeBase64 = logicToolHelper.ConvertFileToEncodeBase64(tran.PdfSignLocation);
                            resp.CODE = "00";
                            resp.MESSAGE = "Success";
                            resp.STATUS = true;
                            resp.OUTPUT_DATA = res;
                        }
                        else
                        {
                            resp.CODE = "103";
                            resp.MESSAGE = tran.PdfSignDetail;
                            return await Task.FromResult(resp);
                        }
                    }
                    else
                    {
                        resp.CODE = "103";
                        resp.MESSAGE = "Billing is not found.";
                        return await Task.FromResult(resp);
                    }
                }
                //// create billing if exists clear status to new

                
            }
            catch (Exception ex)
            {
                resp.ERROR_MESSAGE = ex.Message.ToString();
            }
            return await Task.FromResult(resp);
        }



    }
}
