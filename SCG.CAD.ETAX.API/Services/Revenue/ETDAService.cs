using SCG.CAD.ETAX.DAL.CONTROLLER;
using SCG.CAD.ETAX.DAL.MODEL;
using Microsoft.AspNetCore.Hosting;
using System.Xml;
using System.Configuration;
using static SCG.CAD.ETAX.MODEL.Revenue.ETDA.CodeList.Provice.ThaiISOCountrySubdivisionCodeModel;
using static SCG.CAD.ETAX.MODEL.Revenue.ETDA.CodeList.City.TISICityNameModel;
using static SCG.CAD.ETAX.MODEL.Revenue.ETDA.CodeList.SubDivision.TISICitySubDivisionNameModel;

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

        public string ReadXmlTISICityName()
        {
            string strXml = string.Empty;

            try
            {
                XmlDocument xmlDoc = new XmlDocument();

                var ETDAFileName = "TISICityName_1p0.xsd";

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

        public string ReadXmlTISICitySubDivisionName()
        {
            string strXml = string.Empty;

            try
            {
                XmlDocument xmlDoc = new XmlDocument();

                var ETDAFileName = "TISICitySubDivisionName_1p0.xsd";

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


        public List<ETDAProvice> ProviceMappingToObject(string xmlText)
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
                        string Code = item.Trim().Substring(24, 2).ToString();

                        strList.Add(Code);
                    }
                    if (item.Contains("ccts:Name"))
                    {
                        string FirstString = "<ccts:Name>";
                        string LastString = "</ccts:Name>";

                        int Pos1 = item.IndexOf(FirstString) + FirstString.Length;
                        int Pos2 = item.IndexOf(LastString);

                        if (Pos2 > 0)
                        {
                            string FinalString = item.Substring(Pos1, Pos2 - Pos1);
                            strList.Add(FinalString);
                        }
                        else
                        {
                            strList.Add("");
                        }
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

            listEtdaProvice = listEtdaProvice.Where(x => x.ProviceName != string.Empty).ToList();

            return listEtdaProvice;
        }
        public List<ETDADistrict> DistrictMappingToObject(string xmlText, string ProviceCode)
        {
            List<ETDADistrict> listDistricts = new List<ETDADistrict>();

            List<string> strList = new List<string>();

            try
            {
                List<string> idList = xmlText.Split("\n").ToList();

                foreach (var item in idList)
                {
                    if (item.Contains("value"))
                    {
                        string Code = item.Trim().Substring(24, 4).ToString();

                        strList.Add(Code);
                    }
                    if (item.Contains("ccts:Name"))
                    {
                        string FirstString = "<ccts:Name>";
                        string LastString = "</ccts:Name>";

                        int Pos1 = item.IndexOf(FirstString) + FirstString.Length;
                        int Pos2 = item.IndexOf(LastString);

                        if (Pos2 > 0)
                        {
                            string FinalString = item.Substring(Pos1, Pos2 - Pos1);
                            strList.Add(FinalString);
                        }
                        else
                        {
                            strList.Add("");
                        }
                    }

                    if (strList.Count == 2)
                    {
                        listDistricts.Add(new ETDADistrict()
                        {
                            districtCode = strList[0].Trim(),
                            districtName = strList[1].Trim(),
                            ProviceCode = strList[0].Trim().Substring(0, 2)
                        });

                        strList = new List<string>();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            listDistricts = listDistricts.Where(x => x.districtName != string.Empty && x.ProviceCode == ProviceCode).ToList();

            return listDistricts;
        }
        public List<ETDASubDistrict> SubDistrictMappingToObject(string xmlText, string DistrictCode)
        {
            List<ETDASubDistrict> listSubDistricts = new List<ETDASubDistrict>();

            List<string> strList = new List<string>();

            try
            {
                List<string> idList = xmlText.Split("\n").ToList();

                foreach (var item in idList)
                {
                    if (item.Contains("value"))
                    {
                        string Code = item.Trim().Substring(24, 6).ToString();

                        strList.Add(Code);
                    }
                    if (item.Contains("ccts:Name"))
                    {
                        string FirstString = "<ccts:Name>";
                        string LastString = "</ccts:Name>";

                        int Pos1 = item.IndexOf(FirstString) + FirstString.Length;
                        int Pos2 = item.IndexOf(LastString);

                        if (Pos2 > 0)
                        {
                            string FinalString = item.Substring(Pos1, Pos2 - Pos1);
                            strList.Add(FinalString);
                        }
                        else
                        {
                            strList.Add("");
                        }
                    }

                    if (strList.Count == 2)
                    {
                        listSubDistricts.Add(new ETDASubDistrict()
                        {
                            subDistrictCode = strList[0].Trim(),
                            subDistrictName = strList[1].Trim(),
                            districtCode = strList[0].Trim().Substring(0, 4),
                            ProviceCode = strList[0].Trim().Substring(0, 2)
                        });

                        strList = new List<string>();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            listSubDistricts = listSubDistricts.Where(x => x.subDistrictName != string.Empty && x.districtCode == DistrictCode).ToList();

            return listSubDistricts;
        }

    }
}
