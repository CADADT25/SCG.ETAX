using Renci.SshNet;
using Renci.SshNet.Sftp;
using SCG.CAD.ETAX.INDEXING.TO.DMS.Models;
using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY;
using SCG.CAD.ETAX.UTILITY.Controllers;
using System.Text.Json;

namespace SCG.CAD.ETAX.INDEXING.TO.DMS.BussinessLayer
{
    public class OutputIndexing
    {
        LogHelper log = new LogHelper();
        LogicToolHelper logicTool = new LogicToolHelper();
        UtilityConfigGlobalController configGlobalController = new UtilityConfigGlobalController();
        UtilityConfigMftsIndexGenerationSettingOutputController configMftsIndexGenerationSettingOutputController = new UtilityConfigMftsIndexGenerationSettingOutputController();
        UtilityTransactionDescriptionController transactionDescriptionController = new UtilityTransactionDescriptionController();

        List<ConfigGlobal> configGlobal = new List<ConfigGlobal>();
        List<ConfigMftsIndexGenerationSettingOutput> configIndexOutput = new List<ConfigMftsIndexGenerationSettingOutput>();
        List<TransactionDescription> transactionDescription = new List<TransactionDescription>();

        string pathlog = @"D:\log\";
        string namepathlog = "PATHLOGFILE_OUTPUTINDEXING";

        public void ProcessIndexing()
        {
            List<IndexingInputModel> indexingInputModel = new List<IndexingInputModel>();
            List<ConfigMftsIndexGenerationSettingOutput> configOutput = new List<ConfigMftsIndexGenerationSettingOutput>();
            List<IndexingOutputModel> listFileLoginIndex = new List<IndexingOutputModel>();
            List<LoginIndexFIle> loginIndexFIle = new List<LoginIndexFIle>();
            List<TransactionDescription> ListUpdate = new List<TransactionDescription>();
            try
            {
                Console.WriteLine("Start Output Indexing");
                log.InsertLog(pathlog, "Start Output Indexing");

                GetDataFromDataBase();
                configOutput = GetIndexingOutput();
                ReadFile(configOutput);


                loginIndexFIle = ReadResultInLoginIndexFile(listFileLoginIndex);
                GetDataTransactionDescriptionFromDataBase();

                Console.WriteLine("Start Update Status Transaction");
                log.InsertLog(pathlog, "Start Update Status Transaction");

                ListUpdate = PrepareListUpdateDataTransaction(loginIndexFIle);
                if(ListUpdate.Count > 0)
                {
                    UpdateDataTransaction(ListUpdate);
                }

                Console.WriteLine("End Update Status Transaction : " + ListUpdate.Count + " Records");
                log.InsertLog(pathlog, "End Update Status Transaction");

                Console.WriteLine("End Output Indexing");
                log.InsertLog(pathlog, "End Output Indexing");
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
        }

        public List<ConfigMftsIndexGenerationSettingOutput> GetIndexingOutput()
        {
            List<ConfigMftsIndexGenerationSettingOutput> result = new List<ConfigMftsIndexGenerationSettingOutput>();
            IndexingInputModel indexingInput = new IndexingInputModel();
            try
            {
                foreach (var output in configIndexOutput)
                {
                    if (CheckRunningTime(output))
                    {
                        result.Add(output);
                    }
                }
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }

        public List<IndexingOutputModel> ReadFile(List<ConfigMftsIndexGenerationSettingOutput> configoutput)
        {
            List<IndexingOutputModel> listFileLoginIndex = new List<IndexingOutputModel>();
            try
            {
                foreach (var output in configoutput)
                {
                    Console.WriteLine("Read File From SourceName : " + output.ConfigMftsIndexGenerationSettingOutputSourceName.ToUpper());
                    log.InsertLog(pathlog, "Read File From SourceName : " + output.ConfigMftsIndexGenerationSettingOutputSourceName.ToUpper());

                    Console.WriteLine("Read File From Type : " + output.ConfigMftsIndexGenerationSettingOutputType.ToUpper());
                    log.InsertLog(pathlog, "Read File Type : " + output.ConfigMftsIndexGenerationSettingOutputType.ToUpper());
                    if (output.ConfigMftsIndexGenerationSettingOutputType.ToUpper() == "FOLDER")
                    {
                        listFileLoginIndex = ReadFileFromFolder(output.ConfigMftsIndexGenerationSettingOutputFolder);
                    }
                    else
                    {
                        listFileLoginIndex = ReadFileFromSFTP(output);
                    }
                }
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return listFileLoginIndex;
        }

        public List<IndexingOutputModel> ReadFileFromSFTP(ConfigMftsIndexGenerationSettingOutput configoutput)
        {
            IEnumerable<SftpFile> allFileInPath;
            List<IndexingOutputModel> result = new List<IndexingOutputModel>();
            IndexingOutputModel filedetail = new IndexingOutputModel();
            string filename = "logindex";
            try
            {
                var host = configoutput.ConfigMftsIndexGenerationSettingOutputHost;
                var port = configoutput.ConfigMftsIndexGenerationSettingOutputPort;
                var username = configoutput.ConfigMftsIndexGenerationSettingOutputUsername;
                var password = configoutput.ConfigMftsIndexGenerationSettingOutputPassword;

                using (var client = new SftpClient(host, Convert.ToInt32(port), username, password))
                {
                    client.Connect();
                    if (client.IsConnected)
                    {
                        Console.WriteLine("Read File From SFTP host : " + host + " | Path : " + configoutput.ConfigMftsIndexGenerationSettingOutputFolder);
                        log.InsertLog(pathlog, "Read File From SFTP host : " + host + " | Path : " + configoutput.ConfigMftsIndexGenerationSettingOutputFolder);

                        allFileInPath = client.ListDirectory(configoutput.ConfigMftsIndexGenerationSettingOutputFolder);
                        foreach (var item in allFileInPath)
                        {
                            if (item.Name.StartsWith(filename))
                            {
                                filedetail = new IndexingOutputModel();
                                Console.WriteLine("Read File  : " + item.Name);
                                log.InsertLog(pathlog, "Read File  : " + item.Name);
                                filedetail.FIleName = item.Name;
                                filedetail.FIleValues = new List<string>();
                                filedetail.FIleValues = client.ReadAllLines(item.FullName).ToList();
                                result.Add(filedetail);
                            }
                        }
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

        public List<IndexingOutputModel> ReadFileFromFolder(string path)
        {
            List<IndexingOutputModel> result = new List<IndexingOutputModel>();
            IndexingOutputModel filedetail = new IndexingOutputModel();
            string[] file = new string[0];
            string fileType = "*.txt";
            string filename = "logindex";
            try
            {
                if (Directory.Exists(path))
                {
                    file = Directory.GetFiles(path, fileType);
                    foreach (var item in file)
                    {
                        if (item.StartsWith(filename))
                        {
                            filedetail = new IndexingOutputModel();
                            filedetail.FIleName = Path.GetFileName(item);
                            filedetail.FIleValues = new List<string>();
                            filedetail.FIleValues = File.ReadAllLines(path + "\\" + item).ToList();
                            result.Add(filedetail);
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

        public void GetDataFromDataBase()
        {
            try
            {
                configGlobal = configGlobalController.List().Result;
                configIndexOutput = configMftsIndexGenerationSettingOutputController.List().Result;
                pathlog = configGlobal.FirstOrDefault(x => x.ConfigGlobalName == namepathlog).ConfigGlobalValue;
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
        }

        public void GetDataTransactionDescriptionFromDataBase()
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

        public bool CheckRunningTime(ConfigMftsIndexGenerationSettingOutput output)
        {
            bool result = false;
            try
            {
                result = logicTool.CheckRunTime(output.ConfigMftsIndexGenerationSettingOutputNextTime);
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }

        public List<LoginIndexFIle> ReadResultInLoginIndexFile(List<IndexingOutputModel> indexingOutputModels)
        {
            List<LoginIndexFIle> result = new List<LoginIndexFIle>();
            LoginIndexFIle loginIndexFIle = new LoginIndexFIle();
            string[] text;
            try
            {
                foreach (var file in indexingOutputModels)
                {
                    Console.WriteLine("Read Text In File Name : " + file.FIleName);
                    log.InsertLog(pathlog, "Read Text In File Name : " + file.FIleName);
                    foreach(var filedetail in file.FIleValues)
                    {
                        Console.WriteLine("Text : " + filedetail);
                        log.InsertLog(pathlog, "Text : " + filedetail);
                        text = filedetail.Split('|');
                        loginIndexFIle = new LoginIndexFIle();
                        loginIndexFIle.ImageName = text[0];
                        loginIndexFIle.ImageDocDd = text[1];
                        loginIndexFIle.Result = text[2];
                        loginIndexFIle.Massage = text[3];
                        result.Add(loginIndexFIle);
                    }
                }
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return result;
        }

        public List<TransactionDescription> PrepareListUpdateDataTransaction(List<LoginIndexFIle> loginIndexFIle)
        {
            TransactionDescription update = new TransactionDescription();
            List<TransactionDescription> listUpdate = new List<TransactionDescription>();
            string[] imageName;
            try
            {
                foreach(var data in loginIndexFIle)
                {
                    imageName = data.ImageName.Split('-');
                    update = transactionDescription.Where(x => x.FiDoc == imageName[1]).FirstOrDefault();
                    if(update != null)
                    {
                        update.PdfIndexingDetail = imageName[3];
                        update.PdfIndexingStatus = "Successful";
                        if(imageName[2].ToUpper() == "E")
                        {
                            update.PdfIndexingStatus = "Failed";
                        }
                        listUpdate.Add(update);
                    }
                }
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
            return listUpdate;
        }

        public bool UpdateDataTransaction(List<TransactionDescription> listUpdate)
        {
            bool result = false;
            Task<Response> res;
            try
            {
                if (listUpdate.Count > 0)
                {
                    var json = JsonSerializer.Serialize(listUpdate);
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
