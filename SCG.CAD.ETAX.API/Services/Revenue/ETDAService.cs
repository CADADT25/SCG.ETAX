using SCG.CAD.ETAX.DAL.CONTROLLER;
using SCG.CAD.ETAX.DAL.MODEL;
using Microsoft.AspNetCore.Hosting;
using System.Xml;
using System.Configuration;
using static SCG.CAD.ETAX.MODEL.Revenue.ETDA.CodeList.Provice.ThaiISOCountrySubdivisionCodeModel;

namespace SCG.CAD.ETAX.API.Services
{
    public class ETDAService
    {
        public string ReadXmlThaiISOCountrySubdivisionCode()
        {
            string strXml = string.Empty;

            try
            {
                XmlDocument xmlDoc = new XmlDocument();

                var ETDAFileName = "ThaiISOCountrySubdivisionCode_1p0.xsd";

                var ETDALocation = new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("ConfigPath")["ETDAFile"];

                var FilePath = Path.Combine(Directory.GetCurrentDirectory(), ETDALocation + ETDAFileName).Replace("~", "");

                if (!string.IsNullOrEmpty(FilePath))
                {
                    strXml = File.ReadAllText(FilePath);
                }
                else
                {
                    strXml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return strXml;
        }

        public List<ETDAProvice> MappingToObject(string xmlText)
        {
            List<ETDAProvice> listEtdaProvice = new List<ETDAProvice>();

            List<string> strList = new List<string>();

            try
            {
                List<string> idList = xmlText.Split("\n").ToList();

                foreach (var item in idList)
                {
                    if (item.Contains("value"))
                    {
                        string Code = item.Trim().Substring(24,2).ToString();

                        strList.Add(Code);
                    }
                    if (item.Contains("ccts:Name"))
                    {
                        strList.Add(item);
                    }

                    if (strList.Count == 2)
                    {
                        listEtdaProvice.Add(new ETDAProvice()
                        {
                            ProviceCode = strList[0].Trim(),
                            ProviceName = strList[1].Trim()
                        });

                        strList = new List<string>();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return listEtdaProvice;
        }

    }
}
