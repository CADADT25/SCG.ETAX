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
                //// sign xml
                // sign pdf
                var configPdfSing = service.GetConfigPdfSign(req.CompanyCode);
                if (configPdfSing == null)
                {
                    resp.CODE = "103";
                    resp.MESSAGE = "CofigPdfSign is not found.";
                    return await Task.FromResult(resp);
                }
                var pdfFileModel = new FilePDF();
                pdfFileModel.FullPath = pdfPath;
                pdfFileModel.FileName = req.PdfFileName.Replace("." + req.PdfFileName.Split(".").Last(), "");
                pdfFileModel.Outbound = configPdfSing.ConfigPdfsignOutputPath;
                pdfFileModel.Inbound = configPdfSing.ConfigPdfsignInputPath;
                pdfFileModel.Billno = req.BillingNo;
                var resPdfSign = utilityPDFSignController.ProcessPDFSign(configPdfSing, pdfFileModel);
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
