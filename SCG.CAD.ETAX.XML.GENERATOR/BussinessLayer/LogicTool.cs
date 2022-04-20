using SCG.CAD.ETAX.XML.GENERATOR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.XML.GENERATOR.BussinessLayer
{
    public class LogicTool
    {
        public bool CheckXMLTag(string tagname)
        {
            bool result = false;
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool CheckDataType(string data, TypeData datatype)
        {
            bool result = false;
            try
            {
                switch (datatype)
                {
                    case TypeData.String:
                        if (!string.IsNullOrEmpty(data))
                        {
                            result = true;
                        }
                        break;
                    case TypeData.Interger:
                        if (int.TryParse(data, out int intvalue))
                        {
                            result = true;
                        }
                        break;
                    case TypeData.Double:
                        if (double.TryParse(data, out double doublevalue))
                        {
                            result = true;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool CheckDataRule(string data, string datarule)
        {
            bool result = false;
            try
            {
                data = data ?? "";
                string[] splitdate = datarule.Split("|");
                foreach (string item in splitdate)
                {
                    if (data == item)
                    {
                        result = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

    }
}
