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
        ConfigGlobalController configGlobalController = new ConfigGlobalController();
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

        public bool CheckRunTime(string anytime, DateTime? onetime, DateTime? nexttime, string batchname, int indexno)
        {
            bool result = false;
            try
            {
                if(CheckRunAnyTime(anytime, nexttime) || CheckRunOneTime(onetime))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool CheckRunAnyTime(string anytime, DateTime? nexttime)
        {
            bool result = false;
            try
            {
                if(DateTime.Now <= nexttime)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool CheckRunOneTime(DateTime? onetime)
        {

            bool result = false;
            try
            {
                if(onetime != null)
                {
                    if(onetime <= DateTime.Now)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public void SetRunTime(string batchname)
        {
            switch (batchname.ToUpper())
            {
                case "SCG.CAD.ETAX.EMAIL":
                    break;
                case "SCG.CAD.ETAX.INDEXING.TO.DMS":
                    break;
                case "SCG.CAD.ETAX.PDF.SIGN":
                    break;
                case "SCG.CAD.ETAX.PRINT.ZIP":
                    break;
                case "SCG.CAD.ETAX.XML.GENERATOR":
                    break;
                case "SCG.CAD.ETAX.XML.SIGN":
                    break;
                case "SCG.CAD.ETAX.XML.ZIP":
                    break;
                default:
                    break;
            }
        }
    }
}
