using Renci.SshNet;
using SCG.CAD.ETAX.INPUT.INDEXING.TO.DMS.Models;
using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY;
using SCG.CAD.ETAX.UTILITY.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.INPUT.INDEXING.TO.DMS.BussinessLayer
{
    public class InputIndexing
    {
        LogHelper log = new LogHelper();
        LogicToolHelper logicTool = new LogicToolHelper();
        UtilityConfigGlobalController configGlobalController = new UtilityConfigGlobalController();
        UtilityConfigMftsIndexGenerationSettingInputController configMftsIndexGenerationSettingInputController = new UtilityConfigMftsIndexGenerationSettingInputController();
        UtilityConfigMftsIndexGenerationSettingOutputController configMftsIndexGenerationSettingOutputController = new UtilityConfigMftsIndexGenerationSettingOutputController();
        UtilityTransactionDescriptionController transactionDescriptionController = new UtilityTransactionDescriptionController();
        LogicToolHelper logicToolHelper = new LogicToolHelper();

        List<ConfigGlobal> configGlobal = new List<ConfigGlobal>();
        List<ConfigMftsIndexGenerationSettingInput> configIndexInput = new List<ConfigMftsIndexGenerationSettingInput>();
        List<ConfigMftsIndexGenerationSettingOutput> configIndexOutput = new List<ConfigMftsIndexGenerationSettingOutput>();
        List<TransactionDescription> transactionDescription = new List<TransactionDescription>();

        string pathlog = @"D:\log\";
        string namepathlog = "PATHLOGFILE_INPUTINDEXING";
        string batchname = "SCG.CAD.ETAX.INPUT.INDEXING.TO.DMS";

        public void ProcessIndexing()
        {
            List<IndexingInputModel> indexingInputModel = new List<IndexingInputModel>();
            DateTime nexttime;
            try
            {
                Console.WriteLine("Start Input Indexing");
                log.InsertLog(pathlog, "Start Input Indexing");
                GetDataTransactionDescription();
                GetDataFromDataBase();

                foreach (var input in configIndexInput)
                {
                    if (logicToolHelper.CheckRunTime(input.ConfigMftsIndexGenerationSettingInputNextTime))
                    {
                        indexingInputModel = GetIndexingInput(input);
                        PrepareToSendToSAP(indexingInputModel);

                        nexttime = logicToolHelper.SetNextRunTime(input.ConfigMftsIndexGenerationSettingInputAnyTime, input.ConfigMftsIndexGenerationSettingInputOneTime, batchname, input.ConfigMftsIndexGenerationSettingInputNo);
                        Console.WriteLine("Set NextTime : " + nexttime);
                        log.InsertLog(pathlog, "Set NextTime : " + nexttime);
                    }
                }


                Console.WriteLine("End Input Indexing");
                log.InsertLog(pathlog, "End Input Indexing");
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
        }

        public List<IndexingInputModel> GetIndexingInput(ConfigMftsIndexGenerationSettingInput input)
        {
            List<IndexingInputModel> result = new List<IndexingInputModel>();
            IndexingInputModel indexingInput = new IndexingInputModel();
            try
            {
                indexingInput = new IndexingInputModel();
                indexingInput.CompanyCode = input.ConfigMftsIndexGenerationSettingInputCompanyCode;
                indexingInput.SourceNameInput = input.ConfigMftsIndexGenerationSettingInputSourceName;
                indexingInput.SourceNameOutput = input.ConfigMftsIndexGenerationSettingInputSourceNameOut;
                indexingInput.OcType = input.ConfigMftsIndexGenerationSettingInputOcType;
                indexingInput.ImageDocTypes = new List<ImageDocType>();
                indexingInput.ImageDocTypes = transactionDescription
                                                    .Where(x => x.CompanyCode == input.ConfigMftsIndexGenerationSettingInputCompanyCode &&
                                                    x.PdfIndexingStatus == "Waiting" && String.IsNullOrEmpty(x.DmsAttachmentFileName))
                                                    .Select(x => new ImageDocType
                                                    {
                                                        BillNo = x.BillingNumber,
                                                        ImageDocumentType = x.ImageDocType,
                                                        PathFilePDF = x.PdfSignLocation,
                                                        Year = x.BillingYear.ToString(),
                                                        FiDocNumber = x.FiDoc
                                                    }).ToList();
                result.Add(indexingInput);
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }

        public bool PrepareToSendToSAP(List<IndexingInputModel> indexingInputModel)
        {
            bool result = false;
            List<IndexingInputModel> indexingInputBySounceName = new List<IndexingInputModel>();
            List<ImageDocType> imageDocType = new List<ImageDocType>();
            List<TransactionDescription> dataUpdate = new List<TransactionDescription>();
            byte[] controllFile = null;
            string controllFileName = "";
            bool resultSendFile = false;
            try
            {
                foreach (var output in configIndexOutput)
                {
                    resultSendFile = false;
                    indexingInputBySounceName = indexingInputModel.Where(x => x.SourceNameOutput == output.ConfigMftsIndexGenerationSettingOutputSourceName).ToList();
                    if (indexingInputBySounceName != null && indexingInputBySounceName.Count > 0)
                    {
                        Console.WriteLine("Prepare To Send To SAP by SourceName : " + output.ConfigMftsIndexGenerationSettingOutputSourceName);
                        log.InsertLog(pathlog, "Prepare To Send To SAP by SourceName : " + output.ConfigMftsIndexGenerationSettingOutputSourceName);

                        imageDocType = ListImageFile(indexingInputBySounceName);
                        if (imageDocType.Count > 0)
                        {
                            controllFile = GenControlFile(imageDocType);
                            controllFileName = "index" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                            if (output.ConfigMftsIndexGenerationSettingOutputLogReceiveType.ToUpper() == "FOLDER")
                            {
                                resultSendFile = SendToFolder(output, imageDocType);
                                CreateControlFile(controllFile, output.ConfigMftsIndexGenerationSettingOutputLogReceiveFolder, controllFileName);
                            }
                            //else
                            //{
                            //    resultSendFile = UploadToSFTP(output, imageDocType, controllFile, controllFileName);
                            //}

                            if (resultSendFile)
                            {
                                dataUpdate = PrepareTransactionDescription(imageDocType, output.ConfigMftsIndexGenerationSettingOutputFolder, controllFileName);
                                UpdateTransactionDescription(dataUpdate);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }

        public List<ImageDocType> ListImageFile(List<IndexingInputModel> indexingInputModel)
        {
            List<ImageDocType> result = new List<ImageDocType>();
            ImageDocType imageDocType = new ImageDocType();
            string[] splitImageDocumentType;
            int imageNo = 1;
            try
            {
                Console.WriteLine("List File Send To SAP");
                log.InsertLog(pathlog, "List File Send To SAP");
                foreach (var input in indexingInputModel)
                {
                    Console.WriteLine("Company : " + input.CompanyCode);
                    log.InsertLog(pathlog, "Company : " + input.CompanyCode);
                    foreach (var item in input.ImageDocTypes)
                    {
                        splitImageDocumentType = item.ImageDocumentType.Split(',');
                        imageNo = 1;
                        foreach (var imagedoc in splitImageDocumentType)
                        {
                            imageDocType = new ImageDocType();
                            imageDocType.BillNo = item.BillNo;
                            imageDocType.PathFilePDF = item.PathFilePDF;
                            imageDocType.ImageDocumentType = imagedoc;
                            imageDocType.ReName = "BKPF-" + item.FiDocNumber + "_" + item.Year + "-" + input.CompanyCode + "-" + imageNo + "-" + imagedoc + "-" + "ETAX00000000000.pdf";
                            imageNo++;

                            Console.WriteLine("BillNo : " + item.BillNo + "New File Name : " + imageDocType.ReName);
                            log.InsertLog(pathlog, "Company :" + input.CompanyCode);
                            result.Add(imageDocType);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }

        public byte[] GenControlFile(List<ImageDocType> imageDocType)
        {
            byte[] result = null;
            try
            {
                Console.WriteLine("Gen Control File");
                log.InsertLog(pathlog, "Gen Control File");
                using (var ms = new MemoryStream())
                {
                    using (TextWriter tw = new StreamWriter(ms))
                    {
                        foreach (var filename in imageDocType)
                            tw.WriteLine(filename.ReName);
                        tw.Flush();
                        ms.Position = 0;
                        result = ms.ToArray();
                    }

                }
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }

        public bool CreateControlFile(byte[] controllfile, string path, string filename)
        {
            bool result = false;
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (File.Exists(path + "\\" + filename))
                {
                    File.Delete(path + "\\" + filename);
                }

                FileStream oFileStream = new FileStream(path + "\\" + filename, System.IO.FileMode.Create);
                oFileStream.Write(controllfile, 0, controllfile.Length);
                oFileStream.Close();
                Console.WriteLine("Create Control File Name : " + filename);
                log.InsertLog(pathlog, "Create Control File Name : " + filename);
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }

        public bool SendToFolder(ConfigMftsIndexGenerationSettingOutput configOutput, List<ImageDocType> imageDocType)
        {
            bool result = false;
            string outpath = configOutput.ConfigMftsIndexGenerationSettingOutputLogReceiveFolder;
            try
            {
                Console.WriteLine("Send To Folder : " + outpath);
                log.InsertLog(pathlog, "Send To Folder : " + outpath);
                if (!Directory.Exists(outpath))
                {
                    Directory.CreateDirectory(outpath);
                }

                foreach (var file in imageDocType)
                {
                    if (File.Exists(outpath + "\\" + file.ReName))
                    {
                        File.Delete(outpath + "\\" + file.ReName);
                    }
                    File.Move(file.PathFilePDF, outpath + "\\" + file.ReName);
                    Console.WriteLine("File : " + file.ReName);
                    log.InsertLog(pathlog, "File : " + file.ReName);
                }
                result = true;
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }

        public bool UploadToSFTP(ConfigMftsIndexGenerationSettingOutput configOutput, List<ImageDocType> imageDocType, byte[] controllFile, string controllFileName)
        {
            bool result = false;
            try
            {
                var host = configOutput.ConfigMftsIndexGenerationSettingOutputHost;
                var port = configOutput.ConfigMftsIndexGenerationSettingOutputPort;
                var username = configOutput.ConfigMftsIndexGenerationSettingOutputUsername;
                var password = configOutput.ConfigMftsIndexGenerationSettingOutputPassword;

                using (var client = new SftpClient(host, Convert.ToInt32(port), username, password))
                {
                    client.Connect();
                    if (client.IsConnected)
                    {
                        Console.WriteLine("Upload To SFTP host : " + host + " | Path : " + configOutput.ConfigMftsIndexGenerationSettingOutputLogReceiveFolder);
                        log.InsertLog(pathlog, "Upload To SFTP host : " + host + " | Path : " + configOutput.ConfigMftsIndexGenerationSettingOutputLogReceiveFolder);
                        foreach (var item in imageDocType)
                        {
                            byte[] imageDoc = File.ReadAllBytes(item.PathFilePDF);
                            using (var ms = new MemoryStream(imageDoc))
                            {
                                client.BufferSize = (uint)ms.Length; // bypass Payload error large files
                                client.UploadFile(ms, configOutput.ConfigMftsIndexGenerationSettingOutputLogReceiveFolder + "/" + item.ReName);
                            }
                        }


                        using (var ms = new MemoryStream(controllFile))
                        {
                            client.BufferSize = (uint)ms.Length; // bypass Payload error large files
                            client.UploadFile(ms, configOutput.ConfigMftsIndexGenerationSettingOutputLogReceiveFolder + "/" + controllFileName);
                        }
                        Console.WriteLine("Create Control File Name : " + controllFileName);
                        log.InsertLog(pathlog, "Create Control File Name : " + controllFileName);
                    }
                    else
                    {
                        Console.WriteLine("couldn't connect sftp host : " + host);
                        log.InsertLog(pathlog, "couldn't connect sftp host : " + host);
                    }
                }
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
                configGlobal = configGlobalController.List().Result;
                configIndexInput = configMftsIndexGenerationSettingInputController.List().Result;
                configIndexOutput = configMftsIndexGenerationSettingOutputController.List().Result;
                transactionDescription = transactionDescription.Where(x => x.XmlSignStatus == "Successful" && x.PdfSignStatus == "Successful").ToList();
                pathlog = configGlobal.FirstOrDefault(x => x.ConfigGlobalName == namepathlog).ConfigGlobalValue;
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
        }

        public void GetDataTransactionDescription()
        {
            try
            {
                transactionDescription = transactionDescriptionController.List().Result;
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
        }

        public List<TransactionDescription> PrepareTransactionDescription(List<ImageDocType> listdata, string path, string filename)
        {
            List<TransactionDescription> result = new List<TransactionDescription>();
            List<TransactionDescription> dataUpdate = new List<TransactionDescription>();
            TransactionDescription data = new TransactionDescription();
            List<string> listBillNo = new List<string>();

            try
            {
                GetDataTransactionDescription();
                listBillNo = listdata.Select(x => x.BillNo).ToList();
                dataUpdate = transactionDescription.Where(x => listBillNo.Contains(x.BillingNumber))
                                .Select(x => new TransactionDescription
                                {
                                    TransactionNo = x.TransactionNo,
                                    BillingNumber = x.BillingNumber,
                                    BillingDate = x.BillingDate,
                                    BillingYear = x.BillingYear,
                                    ProcessingDate = x.ProcessingDate,
                                    CompanyCode = x.CompanyCode,
                                    CompanyName = x.CompanyName,
                                    CustomerId = x.CustomerId,
                                    CustomerName = x.CustomerName,
                                    SoldTo = x.SoldTo,
                                    ShipTo = x.ShipTo,
                                    BillTo = x.BillTo,
                                    Payer = x.Payer,
                                    SourceName = x.SourceName,
                                    Foc = x.Foc,
                                    Ic = x.Ic,
                                    PostingYear = x.PostingYear,
                                    FiDoc = x.FiDoc,
                                    ImageDocType = x.ImageDocType,
                                    DocType = x.DocType,
                                    SellOrg = x.SellOrg,
                                    PoNumber = x.PoNumber,
                                    TypeInput = x.TypeInput,
                                    GenerateStatus = x.GenerateStatus,
                                    GenerateDetail = x.GenerateDetail,
                                    GenerateDateTime = x.GenerateDateTime,
                                    XmlSignStatus = x.XmlSignStatus,
                                    XmlSignDetail = x.XmlSignDetail,
                                    XmlSignDateTime = x.XmlSignDateTime,
                                    PdfSignStatus = x.PdfSignStatus,
                                    PdfSignDetail = x.PdfSignDetail,
                                    PdfSignDateTime = x.PdfSignDateTime,
                                    PrintStatus = x.PrintStatus,
                                    PrintDetail = x.PrintDetail,
                                    PrintDateTime = x.PrintDateTime,
                                    EmailSendStatus = x.EmailSendStatus,
                                    EmailSendDetail = x.EmailSendDetail,
                                    EmailSendDateTime = x.EmailSendDateTime,
                                    XmlCompressStatus = x.XmlCompressStatus,
                                    XmlCompressDetail = x.XmlCompressDetail,
                                    XmlCompressDateTime = x.XmlCompressDateTime,
                                    PdfIndexingStatus = x.PdfIndexingStatus,
                                    PdfIndexingDetail = x.PdfIndexingDetail,
                                    PdfIndexingDateTime = x.PdfIndexingDateTime,
                                    PdfSignLocation = x.PdfSignLocation,
                                    XmlSignLocation = x.XmlSignLocation,
                                    OutputXmlTransactionNo = x.OutputXmlTransactionNo,
                                    OutputPdfTransactionNo = x.OutputPdfTransactionNo,
                                    OutputMailTransactionNo = x.OutputMailTransactionNo,
                                    DmsAttachmentFileName = filename,
                                    DmsAttachmentFilePath = path,
                                    CreateBy = x.CreateBy,
                                    CreateDate = x.CreateDate,
                                    UpdateBy = "BATCH",
                                    UpdateDate = DateTime.Now,
                                    Isactive = x.Isactive,
                                    OneTimeEmail = x.OneTimeEmail
                                })
                                .ToList();
                result = dataUpdate;

            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }

        public bool UpdateTransactionDescription(List<TransactionDescription> dataUpdate)
        {
            bool result = false;
            Task<Response> res;
            try
            {
                if (dataUpdate.Count > 0)
                {
                    var json = JsonSerializer.Serialize(dataUpdate);
                    res = transactionDescriptionController.UpdateList(json);
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
    }
}
