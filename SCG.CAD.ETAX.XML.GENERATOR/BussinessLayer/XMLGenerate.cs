using System.Data;
using System.Text.Json;
using System.Xml.Linq;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY.Controllers;
using SCG.CAD.ETAX.UTILITY;
using SCG.CAD.ETAX.MODEL.CustomModel;
using System.Text;

namespace SCG.CAD.ETAX.XML.GENERATOR.BussinessLayer
{
    public class XMLGenerate
    {
        LogHelper log = new LogHelper();
        LogicToolHelper toolHelper = new LogicToolHelper();
        UtilityXMLGenerateController utilityXMLGenerateController = new UtilityXMLGenerateController();
        UtilityConfigGlobalController configGlobalController = new UtilityConfigGlobalController();
        UtilityConfigXMLGeneratorController configXMLGeneratorController = new UtilityConfigXMLGeneratorController();

        string pathlog = @"D:\log\";
        string namepathlog = "PATHLOGFILE_XMLGENERATOR";
        List<ConfigGlobal> configGlobal = new List<ConfigGlobal>();
        List<ConfigXmlGenerator> configXMLGenerator = new List<ConfigXmlGenerator>();

        public void ProcessGenXMLFile()
        {
            try
            {
                Response res = new Response();
                GetDataFromDataBase();
                var allTextFile = ReadTextFile();
                ConfigXmlGenerator configXML = new ConfigXmlGenerator();
                ProfileCompany companydata = new ProfileCompany();
                string nametextfilefail = "";

                foreach (var textfile in allTextFile)
                {
                    //res = utilityXMLGenerateController.ProcessXMLGenerate(textfile);
                    res = utilityXMLGenerateController.SendProcessXMLGen(textfile).Result;
                    var filename = Path.GetFileName(textfile);
                    if (res.STATUS)
                    {
                        Console.WriteLine("File : " + filename + " | Result : Success");
                        log.InsertLog(pathlog, "File : " + filename + " | Result : Success");
                    }
                    else
                    {
                        Console.WriteLine("File : " + filename + " | Result : Fail | ErrorMessage : " + res.ERROR_MESSAGE);
                        log.InsertLog(pathlog, "File : " + filename + " | Result : Fail | ErrorMessage : " + res.ERROR_MESSAGE);
                    }
                }

            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
        }

        public List<string> ReadTextFile()
        {
            List<string> result = new List<string>();
            string[] fullpath = new string[0];
            string pathFolder = "";
            List<string> listpath;
            string fileType = "*.txt";
            try
            {
                foreach (var path in configXMLGenerator)
                {
                    if (Directory.Exists(path.ConfigXmlGeneratorInputPath))
                    {
                        pathFolder = path.ConfigXmlGeneratorInputPath;
                        fullpath = Directory.GetFiles(pathFolder, fileType);
                        listpath = fullpath.ToList();
                        result.AddRange(listpath);
                    }
                }
                //result.AddRange(new List<string>() { @"C:\Work space\Document\Etax\Test\010020220912012137.txt" });
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
                configXMLGenerator = configXMLGeneratorController.List().Result;
                configGlobal = configGlobalController.List().Result;
                pathlog = configGlobal.FirstOrDefault(x => x.ConfigGlobalName == namepathlog).ConfigGlobalValue;
            }
            catch (Exception ex)
            {
                log.InsertLog(pathlog, "Exception : " + ex.ToString());
            }
        }

    }
}
