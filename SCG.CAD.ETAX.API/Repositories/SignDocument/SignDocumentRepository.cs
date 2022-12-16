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
        UtilityXMLGenerateController utilityXMLGenerateController = new UtilityXMLGenerateController();
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
                if (string.IsNullOrEmpty(req.TextFileName))
                {
                    resp.CODE = "103";
                    resp.MESSAGE = "TextFileName is required.";
                    return await Task.FromResult(resp);
                }
                if (req.TextFileName.Split(".").Last().ToLower() != "txt")
                {
                    resp.CODE = "103";
                    resp.MESSAGE = "TextFileName is is invalid.";
                    return await Task.FromResult(resp);
                }
                if (string.IsNullOrEmpty(req.PdfFileName))
                {
                    resp.CODE = "103";
                    resp.MESSAGE = "PdfFileName is required.";
                    return await Task.FromResult(resp);
                }
                if (req.PdfFileName.Split(".").Last().ToLower() != "pdf")
                {
                    resp.CODE = "103";
                    resp.MESSAGE = "PdfFileName is is invalid.";
                    return await Task.FromResult(resp);
                }
                if (string.IsNullOrEmpty(req.PdfEncodeBase64))
                {
                    resp.CODE = "103";
                    resp.MESSAGE = "PdfEncodeBase64 is required.";
                    return await Task.FromResult(resp);
                }


                // check text & pdf file
                string tempPath = service.GetConfigGlobal("RESIGN_NEWTRANS_TEMP_PATH");

                string textPathTemp = Path.Combine(tempPath, "api", DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"), DateTime.Now.ToString("HHmmss"), req.TextFileName);
                string textPath = service.CreateFileFromBase64(req.TextEncodeBase64, textPathTemp);

                string pdfPathTemp = Path.Combine(tempPath, "api", DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"), DateTime.Now.ToString("HHmmss"), req.PdfFileName);
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
                    resp.MESSAGE = "PdfEncodeBase64 unable to decode.";
                    return await Task.FromResult(resp);
                }

                // xml gen
                string xmlBeforeSignPath = "";
                TransactionDescription tran = null;
                List<string> billings;
                var resXmlGen = utilityXMLGenerateController.ProcessXMLGenerate(textPath);
                if (!resXmlGen.STATUS)
                {
                    resp.CODE = "103";
                    resp.MESSAGE = "Unable to gen Xml.";
                    resp.ERROR_MESSAGE = resXmlGen.ERROR_MESSAGE;
                    return await Task.FromResult(resp);
                }
                else
                {
                    billings = (List<string>)resXmlGen.OUTPUT_DATA;
                    tran = service.GetTransactionDescription(billings[0]);
                    if (tran != null)
                    {
                        if (tran.GenerateStatus == "Successful")
                        {
                            xmlBeforeSignPath = tran.XmlBeforeSignLocation;
                        }
                        else
                        {
                            resp.CODE = "103";
                            resp.MESSAGE = tran.GenerateDetail;
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

                // sign xml
                var configXmlSign = service.GetConfigXmlSign(tran.CompanyCode);
                if (configXmlSign == null)
                {
                    resp.CODE = "103";
                    resp.MESSAGE = "ConfigXmlSign is not found.";
                    return await Task.FromResult(resp);
                }
                var xmlFileDetail = new FileXML();
                //xmlBeforeSignPath = @"D:\SAPCH\0100\Outbound\GENERATOR\Success\010020221180200162_20221215161230387.xml";
                string xmlFileName = Path.GetFileName(xmlBeforeSignPath);
                xmlFileDetail.FullPath = xmlBeforeSignPath;
                xmlFileDetail.FileName = xmlFileName.Replace("." + xmlFileName.Split(".").Last(), "");
                xmlFileDetail.Outbound = configXmlSign.ConfigXmlsignOutputPath;
                xmlFileDetail.Inbound = configXmlSign.ConfigXmlsignInputPath;
                xmlFileDetail.Billno = billings[0];
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
                    tran = null;
                    tran = service.GetTransactionDescription(billings[0]);
                    if (tran != null)
                    {
                        if (tran.XmlSignStatus == "Successful")
                        {
                            LogicToolHelper logicToolHelper = new LogicToolHelper();
                            res.XmlSignedEncodeBase64 = logicToolHelper.ConvertFileToEncodeBase64(tran.XmlSignLocation);
                        }
                        else
                        {
                            resp.CODE = "103";
                            resp.MESSAGE = tran.BillingNumber + " " + tran.XmlSignStatus + " " + tran.XmlSignDetail;
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
                var configPdfSign = service.GetConfigPdfSign(tran.CompanyCode);
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
                pdfFileDetail.Billno = billings[0];
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
                    tran = null;
                    tran = service.GetTransactionDescription(billings[0]);
                    if (tran != null)
                    {
                        if (tran.PdfSignStatus == "Successful")
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
                resp.CODE = "101";
                resp.ERROR_MESSAGE = ex.Message.ToString();
            }
            return await Task.FromResult(resp);
        }



    }
}
