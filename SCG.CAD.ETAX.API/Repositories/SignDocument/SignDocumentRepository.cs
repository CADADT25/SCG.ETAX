﻿using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using SCG.CAD.ETAX.API.Services.SignDocument;
using SCG.CAD.ETAX.MODEL.CustomModel;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY.Controllers;

namespace SCG.CAD.ETAX.API.Repositories
{
    public class SignDocumentRepository : ISignDocumentRepository
    {
        UtilityAPISignController utilityAPISignController = new UtilityAPISignController();

        SignDocumentService service = new SignDocumentService();
        public async Task<Response> Sign(SignDocumentRequest req)
        {
            Response resp = new Response();
            var res = new SignDocumentResponse();
            resp.STATUS = false;
            try
            {
                ////check string empty
                //if (string.IsNullOrEmpty(req.TextEncodeBase64))
                //{
                //    resp.CODE = "101";
                //    resp.MESSAGE = "TextEncodeBase64 is required.";
                //    return await Task.FromResult(resp);
                //}
                //if (string.IsNullOrEmpty(req.PdfEncodeBase64))
                //{
                //    resp.CODE = "101";
                //    resp.MESSAGE = "PdfEncodeBase64 is required.";
                //    return await Task.FromResult(resp);
                //}
                //// check text & pdf file
                //// "ConfigGlobal/GetDetailByName?cate=REQEUST&name=RESIGN_NEWTRANS_TEMP_PATH";
                //string tempPath = service.GetConfigGlobal("RESIGN_NEWTRANS_TEMP_PATH");

                //string textPathTemp = Path.Combine(tempPath, "api", DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"), req.TextFileName);
                //string textPath = service.CreateFileFromBase64(req.TextEncodeBase64, textPathTemp);

                //string pdfPathTemp = Path.Combine(tempPath, "api", DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"), req.PdfFileName);
                //string pdfPath = service.CreateFileFromBase64(req.PdfEncodeBase64, pdfPathTemp);

                //if (string.IsNullOrEmpty(textPath))
                //{
                //    resp.CODE = "103";
                //    resp.MESSAGE = "TextEncodeBase64 unable to decode.";
                //    return await Task.FromResult(resp);
                //}
                //if (string.IsNullOrEmpty(pdfPath))
                //{
                //    resp.CODE = "103";
                //    resp.MESSAGE = "TextEncodeBase64 unable to decode.";
                //    return await Task.FromResult(resp);
                //}
                //// gen xml
                //var errorMsg = service.GenerateTextToXml(textPath);
                //if (!string.IsNullOrEmpty(errorMsg))
                //{
                //    resp.CODE = "103";
                //    resp.MESSAGE = errorMsg;
                //    return await Task.FromResult(resp);
                //}
                //// sign xml
                //// sign pdf
                //// create billing if exists clear status to new
            }
            catch (Exception ex)
            {
                resp.MESSAGE = ex.Message.ToString();
            }
            return await Task.FromResult(resp);
        }



    }
}