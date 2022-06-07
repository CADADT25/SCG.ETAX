using SCG.CAD.ETAX.XML.GENERATOR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.XML.GENERATOR.BussinessLayer
{
    public class LogicTool
    {
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
        public bool CheckSpecialChar(string data)
        {
            bool result = false;
            try
            {
                //Console.WriteLine("i/p is " + str);
                Regex rgx = new Regex("[^A-Za-z0-9ก-๙]");
                //bool isNUmber = int.TryParse(data, out int n);
                //bool hasSpecialChars = rgx.IsMatch(data.ToString()) || isNUmber;
                bool hasSpecialChars = rgx.IsMatch(data.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

    }
}
