using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.UTILITY
{
    public class LogicToolHelper
    {
        UtilityConfigGlobalController configGlobalController = new UtilityConfigGlobalController();
        UtilityConfigMftsEmailSettingController configMftsEmailSettingController = new UtilityConfigMftsEmailSettingController();
        UtilityConfigPDFSignController configPDFSignController = new UtilityConfigPDFSignController();
        UtilityConfigMftsCompressPrintSettingController configMftsCompressPrintSettingController = new UtilityConfigMftsCompressPrintSettingController();
        UtilityConfigXMLGeneratorController configXMLGeneratorController = new UtilityConfigXMLGeneratorController();
        UtilityConfigXMLSignController configXMLSignController = new UtilityConfigXMLSignController();
        UtilityConfigMftsCompressXmlSettingController configMftsCompressXmlSettingController = new UtilityConfigMftsCompressXmlSettingController();
        UtilityConfigMftsIndexGenerationSettingInputController configMftsIndexGenerationSettingInputController = new UtilityConfigMftsIndexGenerationSettingInputController();
        UtilityConfigMftsIndexGenerationSettingOutputController configMftsIndexGenerationSettingOutputController = new UtilityConfigMftsIndexGenerationSettingOutputController();

        List<ConfigGlobal> configGlobals = new List<ConfigGlobal>();
        public bool CheckBatchRunningTime(string configrunningname)
        {
            bool result = false;
            try
            {
                GetGlobalConfig();
                var config = configGlobals.FirstOrDefault(x => x.ConfigGlobalName == configrunningname);
                if (config != null)
                {
                    if (config.ConfigGlobalValue != null && !String.IsNullOrEmpty(config.ConfigGlobalValue))
                    {
                        if (config.ConfigGlobalValue.IndexOf(DateTime.Now.ToString("HH:mm")) >= 0)
                        {
                            result = true;
                        }
                    }
                    else
                    {
                        result = true;
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            result = true;
            return result;
        }

        public void GetGlobalConfig()
        {
            try
            {
                configGlobals = configGlobalController.List().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckRunTime(DateTime? nexttime)
        {
            bool result = false;
            try
            {
                if (DateTime.Now >= nexttime)
                {
                    result = true;
                }
                result = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public DateTime SetNextRunTime(string anytime, string onetime, string batchname, int index)
        {
            DateTime result;
            DateTime? dateonetime;
            DateTime dateanytime;
            DateTime nexttime;
            bool clearOnetime = false;
            try
            {
                dateonetime = CheckRunOneTime(onetime);
                dateanytime = CheckRunAnyTime(anytime);
                if (dateonetime == null)
                {
                    //Set anytime
                    nexttime = dateanytime;
                }
                else if (dateonetime <= dateanytime)
                {
                    //Set onetime
                    clearOnetime = true;
                    nexttime = dateonetime ?? dateanytime;
                    SendClearOneTime(batchname, index);
                }
                else if (dateonetime > dateanytime)
                {
                    //Set anytime
                    nexttime = dateanytime;
                }
                else
                {
                    //Set anytime
                    nexttime = dateanytime;
                }
                result = nexttime;
                SendSetNextRunTime(batchname, index, nexttime, clearOnetime);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public DateTime CheckRunAnyTime(string anytime)
        {
            DateTime result = new DateTime();
            DateTime datenow = DateTime.Now;
            DateTime nexttime = DateTime.Now;
            List<DateTime> listdate = new List<DateTime>();
            string[] timearray;
            try
            {
                var anytimearray = anytime.Split('|');
                foreach (var time in anytimearray)
                {
                    timearray = time.Split(':');
                    if (datenow.Hour > Convert.ToInt32(timearray[0]))
                    {
                        //Set Next day
                        nexttime = ChangeTime(nexttime.AddDays(1), Convert.ToInt32(timearray[0]), Convert.ToInt32(timearray[1]));
                    }
                    else if (datenow.Hour == Convert.ToInt32(timearray[0]))
                    {
                        if (datenow.Minute > Convert.ToInt32(timearray[1]))
                        {
                            //Set Next day
                            nexttime = ChangeTime(nexttime.AddDays(1), Convert.ToInt32(timearray[0]), Convert.ToInt32(timearray[1]));
                        }
                        else
                        {
                            //Set Current day
                            nexttime = ChangeTime(nexttime, Convert.ToInt32(timearray[0]), Convert.ToInt32(timearray[1]));
                        }
                    }
                    else
                    {
                        //Set Current day
                        nexttime = ChangeTime(nexttime, Convert.ToInt32(timearray[0]), Convert.ToInt32(timearray[1]));
                    }
                    listdate.Add(nexttime);
                }
                result = listdate.OrderBy(x => x).First();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public DateTime? CheckRunOneTime(string onetime)
        {

            DateTime? result = null;
            try
            {
                if (!String.IsNullOrEmpty(onetime))
                {
                    var onetimearray = onetime.Split('|');
                    result = Convert.ToDateTime(onetimearray[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public void SendSetNextRunTime(string batchname, int index, DateTime nexttime, bool clearOnetime)
        {
            switch (batchname.ToUpper())
            {
                case "SCG.CAD.ETAX.EMAIL":
                    configMftsEmailSettingController.SendUpdateNextTime(index, nexttime, clearOnetime);
                    break;
                case "SCG.CAD.ETAX.INPUT.INDEXING.TO.DMS":
                    configMftsIndexGenerationSettingInputController.SendUpdateNextTime(index, nexttime, clearOnetime);
                    break;
                case "SCG.CAD.ETAX.OUTPUT.INDEXING.TO.DMS":
                    configMftsIndexGenerationSettingOutputController.SendUpdateNextTime(index, nexttime, clearOnetime);
                    break;
                case "SCG.CAD.ETAX.PDF.SIGN":
                    configPDFSignController.SendUpdateNextTime(index, nexttime, clearOnetime);
                    break;
                case "SCG.CAD.ETAX.PRINT.ZIP":
                    configMftsCompressPrintSettingController.SendUpdateNextTime(index, nexttime, clearOnetime);
                    break;
                case "SCG.CAD.ETAX.XML.GENERATOR":
                    configXMLGeneratorController.SendUpdateNextTime(index, nexttime, clearOnetime);
                    break;
                case "SCG.CAD.ETAX.XML.SIGN":
                    configXMLSignController.SendUpdateNextTime(index, nexttime, clearOnetime);
                    break;
                case "SCG.CAD.ETAX.XML.ZIP":
                    configMftsCompressXmlSettingController.SendUpdateNextTime(index, nexttime, clearOnetime);
                    break;
                default:
                    break;
            }
        }

        public void SendClearOneTime(string batchname, int index)
        {
            switch (batchname.ToUpper())
            {
                case "SCG.CAD.ETAX.EMAIL":
                    configMftsEmailSettingController.SendDeleteOneTime(index);
                    break;
                case "SCG.CAD.ETAX.INPUT.INDEXING.TO.DMS":
                    configMftsIndexGenerationSettingInputController.SendDeleteOneTime(index);
                    break;
                case "SCG.CAD.ETAX.OUTPUT.INDEXING.TO.DMS":
                    configMftsIndexGenerationSettingOutputController.SendDeleteOneTime(index);
                    break;
                case "SCG.CAD.ETAX.INDEXING.TO.DMS":
                    break;
                case "SCG.CAD.ETAX.PDF.SIGN":
                    configPDFSignController.SendDeleteOneTime(index);
                    break;
                case "SCG.CAD.ETAX.PRINT.ZIP":
                    configMftsCompressPrintSettingController.SendDeleteOneTime(index);
                    break;
                case "SCG.CAD.ETAX.XML.GENERATOR":
                    configXMLGeneratorController.SendDeleteOneTime(index);
                    break;
                case "SCG.CAD.ETAX.XML.SIGN":
                    configXMLSignController.SendDeleteOneTime(index);
                    break;
                case "SCG.CAD.ETAX.XML.ZIP":
                    configMftsCompressXmlSettingController.SendDeleteOneTime(index);
                    break;
                default:
                    break;
            }
        }

        public DateTime ChangeTime(DateTime dateTime, int hours, int minutes)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                minutes,
                00,
                000,
                dateTime.Kind);
        }

        public string ConvertFileToEncodeBase64(string filepath)
        {
            string result = "";
            Byte[] bytes = File.ReadAllBytes(filepath);
            result = Convert.ToBase64String(bytes);
            return result;
        }

        public bool ConvertIntToBoolean(int? value)
        {
            // null = false
            // 1 = true
            // else = false
            bool result = false;

            if(value != null && value == 1)
            {
                result = true;
            }

            return result;
        }

        public int GetDelayTimeProgram(string batchnamedelaytime)
        {
            int result = 5 * 60 * 1000; // 5 minutes 
            try
            {
                GetGlobalConfig();
                var config = configGlobals.FirstOrDefault(x => x.ConfigGlobalName == batchnamedelaytime);
                if (config != null)
                {
                    if (config.ConfigGlobalValue != null && !String.IsNullOrEmpty(config.ConfigGlobalValue))
                    {
                        result = Convert.ToInt32(config.ConfigGlobalValue) * 60 * 1000;
                    }
                }
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }

    }
}
