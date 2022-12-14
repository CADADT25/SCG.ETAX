using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using SCG.CAD.ETAX.MODEL.CustomModel;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SCG.CAD.ETAX.API.Services.SignDocument
{
    public class SignDocumentService
    {
        readonly DatabaseContext _dbContext = new();
        LogicToolHelper toolHelper = new LogicToolHelper();

        //#region Get data
        //public List<ProfileCompany> GetAllProfileCompany()
        //{
        //    return _dbContext.profileCompany.Where(t => t.Isactive == 1).ToList();
        //}
        //public List<ConfigXmlGenerator> GetAllConfigXmlGenerator()
        //{
        //    return _dbContext.configXmlGenerator.Where(t => t.Isactive == 1).ToList();
        //}
        //public ProfileCompany GetProfileCompany(string taxnumber)
        //{
        //    return _dbContext.profileCompany.Where(t => t.TaxNumber == taxnumber).FirstOrDefault();
        //}
        //public ConfigXmlGenerator GetConfigXmlGenerator(string comcode)
        //{
        //    return _dbContext.configXmlGenerator.Where(t => t.ConfigXmlGeneratorCompanyCode == comcode).FirstOrDefault();
        //}
        public string GetConfigGlobal(string configGlobalName)
        {
            var data = _dbContext.configGlobal.Where(t => t.ConfigGlobalName == configGlobalName).FirstOrDefault();
            if (data != null)
            {
                return data.ConfigGlobalValue;
            }
            else
            {
                return "";
            }
        }
        //public string GetConfigGlobal(string cate, string name)
        //{

        //    try
        //    {
        //        var data = _dbContext.configGlobal.Where(x => x.ConfigGlobalName == name && x.ConfigGlobalCategoryName == cate).FirstOrDefault();

        //        if (data != null)
        //        {
        //            return data.ConfigGlobalValue;
        //        }
        //        else
        //        {
        //            return "";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return "";
        //}
        public ConfigPdfSign GetConfigPdfSign(string companyCode)
        {
            var data = _dbContext.configPdfSign.Where(t => t.ConfigPdfsignCompanyCode == companyCode && t.ConfigPdfsignOnlineRecordNumber != null && t.ConfigPdfsignOnlineRecordNumber != "").FirstOrDefault();
            return data;
        }
        public ConfigXmlSign GetConfigXmlSign(string companyCode)
        {
            var data = _dbContext.configXmlSign.Where(t => t.ConfigXmlsignCompanycode == companyCode && t.ConfigXmlsignOnlineRecordNumber != null && t.ConfigXmlsignOnlineRecordNumber != "").FirstOrDefault();
            return data;
        }
        //#endregion

        public TransactionDescription GetTransactionDescription(string billingNo)
        {
            var data = _dbContext.transactionDescription.Where(t => t.BillingNumber == billingNo).FirstOrDefault();
            return data;
        }

        //#region Xml generator
        //public string GenerateTextToXml(string textfile)
        //{
        //    int row = 56;
        //    var filename = Path.GetFileName(textfile);
        //    //log.InsertLog(pathlog, "Start Read TextFile : " + textfile);
        //    var dt = ConvertToDataTable(textfile, row);
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        //    log.InsertLog(pathlog, "ConvertToDataTable success");
        //        var cs = ConvertDTtoClass(dt);
        //        //    log.InsertLog(pathlog, "ConvertToClass success");
        //        string pathoutput = GetConfigGlobal("PATHBACKUPTEXTFILE");
        //        ConfigXmlGenerator configXML;
        //        foreach (var classtextfile in cs)
        //        {
        //            if (!CheckCancelBillingOrSentRevenueDepartment(classtextfile.BILLING_NO))
        //            {
        //                var companydata = GetProfileCompany(classtextfile.SELLER_TIN);
        //                if (companydata != null)
        //                {
        //                    configXML = GetConfigXmlGenerator(companydata.CompanyCode);
        //                }
        //                else
        //                {
        //                    return "Company not found.";
        //                }
        //                //            log.InsertLog(pathlog, "Start round : " + round);
        //                //            log.InsertLog(pathlog, "Start Insert Data TransactionDescription");
        //                InsertDataTransactionDescription(classtextfile, configXML.ConfigXmlGeneratorInputSource);
        //                //            log.InsertLog(pathlog, "End Insert Data TransactionDescription");

        //                //            log.InsertLog(pathlog, "Start ValidateFileText");
        //                var errormessage = ValidateTextFile(classtextfile);
        //                if (errormessage.Count > 0)
        //                {
        //                    //                log.InsertLog(pathlog, "ValidateFileText Fail");
        //                    UpdateDataTransaction(errormessage, classtextfile.BILLING_NO);
        //                    var nametextfilefail = filename.Replace(".txt", "") + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt";
        //                    GenTextFileFail(nametextfilefail, classtextfile, configXML.ConfigXmlGeneratorOutputPath + "\\Fail");
        //                }
        //                else
        //                {
        //                    //                log.InsertLog(pathlog, "ValidateFileText Success");
        //                    errormessage = new List<string>();

        //                    var dataxml = ConvertClasstoXMLFormat(classtextfile);
        //                    //                log.InsertLog(pathlog, "ConvertClasstoXMLFormat success");

        //                    if (errormessage.Count > 0)
        //                    {
        //                        // log.InsertLog(pathlog, "ValidateSchematron Fail");
        //                        UpdateDataTransaction(errormessage, classtextfile.BILLING_NO);
        //                        var nametextfilefail = filename.Replace(".txt", "") + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt";
        //                        GenTextFileFail(nametextfilefail, classtextfile, configXML.ConfigXmlGeneratorOutputPath + "\\Fail\\");
        //                    }
        //                    else
        //                    {
        //                        // log.InsertLog(pathlog, "Start Gen XML File");
        //                        string xmlfilename = companydata.CompanyCode + classtextfile.FISCAL_YEAR + classtextfile.BILLING_NO + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xml";
        //                        var xml = GenXMLFromTemplate(dataxml, configXML.ConfigXmlGeneratorOutputPath + "\\Success\\", xmlfilename, classtextfile.BILLING_NO, classtextfile.BILLING_DATE);

        //                        // log.InsertLog(pathlog, "End Gen XML File");
        //                    }
        //                }
        //            }
        //        }

        //        //log.InsertLog(pathlog, "End Read TextFile : " + textfile);

        //        //log.InsertLog(pathlog, "Start Move File");
        //        MoveFile(textfile, filename, pathoutput);
        //        //log.InsertLog(pathlog, "End Move File");
        //    }
        //    else
        //    {
        //        return "Can't find data in text file.";
        //    }
        //    return "";
        //}
        //public DataTable ConvertToDataTable(string filePath, int numberOfColumns)
        //{
        //    DataTable tbl = new DataTable();

        //    for (int col = 0; col < numberOfColumns + 1; col++)
        //        tbl.Columns.Add(new DataColumn("Column" + (col + 1).ToString()));

        //    string[] lines = System.IO.File.ReadAllLines(filePath);

        //    foreach (string line in lines)
        //    {
        //        if (!string.IsNullOrEmpty(line))
        //        {
        //            var separator = @"\|";
        //            var replateseparator = line.Replace(separator, "|");
        //            var cols = replateseparator.Split('|');
        //            DataRow dr = tbl.NewRow();
        //            for (int cIndex = 0; cIndex < cols.Count(); cIndex++)
        //            {
        //                dr[cIndex] = cols[cIndex];
        //            }
        //            dr[numberOfColumns] = filePath;

        //            tbl.Rows.Add(dr);
        //        }
        //    }
        //    return tbl;
        //}
        //public List<TextFileSchema> ConvertDTtoClass(DataTable dt)
        //{
        //    List<TextFileSchema> result = new List<TextFileSchema>();
        //    try
        //    {
        //        TextFileSchema row = new TextFileSchema();
        //        TextFileItemSchema item;
        //        string headerflag;
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            headerflag = dt.Rows[i][0].ToString();
        //            if (headerflag.Equals("H", StringComparison.InvariantCultureIgnoreCase))
        //            {
        //                if (i != 0)
        //                {
        //                    result.Add(row);
        //                }
        //                row = new TextFileSchema();
        //                row.Item = new List<TextFileItemSchema>();
        //                row.HEADER_FLAG = dt.Rows[i][0].ToString();
        //                row.FI_DOC_TYPE = dt.Rows[i][1].ToString();
        //                row.FISCAL_YEAR = dt.Rows[i][2].ToString();
        //                row.IC_FLAG = dt.Rows[i][3].ToString();
        //                row.SELLER_TIN = dt.Rows[i][4].ToString();
        //                row.SELLER_BRANCH = dt.Rows[i][5].ToString();
        //                row.BUYER_TYPE_CODE = dt.Rows[i][6].ToString();
        //                row.BUYER_TAXID = dt.Rows[i][7].ToString();
        //                row.BUYER_BRANCH = dt.Rows[i][8].ToString();
        //                row.BUYER_NAME = dt.Rows[i][9].ToString();
        //                row.BUYER_CODE = dt.Rows[i][10].ToString();
        //                row.BUYER_PSTLZ = dt.Rows[i][11].ToString();
        //                row.BUYER_LAND1 = dt.Rows[i][12].ToString();
        //                row.SELLER_NAME = dt.Rows[i][13].ToString();
        //                row.SELLER_BUKRS = dt.Rows[i][14].ToString();
        //                row.SELLER_MAIL = dt.Rows[i][15].ToString();
        //                row.SELLER_PSTLZ = dt.Rows[i][16].ToString();
        //                row.SELLER_BUILD_NAME = dt.Rows[i][17].ToString();
        //                row.SELLER_STREET = dt.Rows[i][18].ToString();
        //                row.SELLER_CITY = dt.Rows[i][19].ToString();
        //                row.SELLER_SUBDIV = dt.Rows[i][20].ToString();
        //                row.SELLER_LAND1 = dt.Rows[i][21].ToString();
        //                row.SELLER_PROVINCE = dt.Rows[i][22].ToString();
        //                row.SELLER_BUILD_NO = dt.Rows[i][23].ToString();
        //                row.BUYER_ADDRESS1 = dt.Rows[i][24].ToString();
        //                row.BUYER_ADDRESS2 = dt.Rows[i][25].ToString();
        //                row.BILLING_NO = dt.Rows[i][26].ToString();
        //                row.FI_DOC = dt.Rows[i][27].ToString();
        //                row.DOC_CURRENCY = dt.Rows[i][28].ToString();
        //                row.SALES_AMOUNT = dt.Rows[i][29].ToString();
        //                row.TAX_CODE = dt.Rows[i][30].ToString();
        //                row.TAX_RATE = dt.Rows[i][31].ToString();
        //                row.TAX_AMOUNT = dt.Rows[i][32].ToString();
        //                row.CHARGE_AMOUNT = dt.Rows[i][33].ToString();
        //                row.ALLOWANCE_AMOUNT = dt.Rows[i][34].ToString();
        //                row.ORIGINAL_AMOUNT = dt.Rows[i][35].ToString();
        //                row.DIFFERENCE_AMOUNT = dt.Rows[i][36].ToString();
        //                row.TAX_BASIS_AMOUNT = dt.Rows[i][37].ToString();
        //                row.TAX_TOTAL_AMOUNT = dt.Rows[i][38].ToString();
        //                row.GRAND_TOTAL_AMOUNT = dt.Rows[i][39].ToString();
        //                row.CREATE_DATE_TIME = dt.Rows[i][40].ToString();
        //                row.REF_DOC = dt.Rows[i][41].ToString();
        //                row.REF_DOC_DATE = dt.Rows[i][42].ToString();
        //                row.REF_FI_DOCTYPE = dt.Rows[i][43].ToString();
        //                row.ORDER_REASON = dt.Rows[i][44].ToString();
        //                row.ORDER_REASON_CODE = dt.Rows[i][45].ToString();
        //                row.BILLING_DATE = dt.Rows[i][46].ToString();
        //                row.INSTANCE = dt.Rows[i][47].ToString();
        //                row.BILLING_TYPE = dt.Rows[i][48].ToString();
        //                row.SALES_ORG = dt.Rows[i][49].ToString();
        //                row.PRINT_DATE_TIME = dt.Rows[i][50].ToString();
        //                row.Number_Sold_to = dt.Rows[i][51].ToString();
        //                row.Number_Ship_to = dt.Rows[i][52].ToString();
        //                row.Number_Bill_to = dt.Rows[i][53].ToString();
        //                row.Number_Payer = dt.Rows[i][54].ToString();
        //                row.CORRECT_AMOUNT = dt.Rows[i][55].ToString();
        //                row.pathfile = dt.Rows[i][56].ToString();
        //            }
        //            else
        //            {
        //                var data = dt.Rows[i];
        //                item = ConvertItemtoClass(data);
        //                row.Item.Add(item);
        //            }

        //        }
        //        // lastloop
        //        result.Add(row);
        //    }
        //    catch (Exception ex)
        //    {
        //        //log.InsertLog(pathlog, "Exception : " + ex.ToString());
        //    }
        //    return result;
        //}
        //public TextFileItemSchema ConvertItemtoClass(DataRow dr)
        //{
        //    TextFileItemSchema result = new TextFileItemSchema();
        //    try
        //    {
        //        result.ITEM_FLAG = dr[0].ToString();
        //        result.FI_DOC_TYPE = dr[1].ToString();
        //        result.SELLER_TIN = dr[2].ToString();
        //        result.SELLER_BRANCH = dr[3].ToString();
        //        result.BILLING_NO = dr[4].ToString();
        //        result.ITEM_NO = dr[5].ToString();
        //        result.PRODUCT_NAME = dr[6].ToString();
        //        result.QUANTITY = dr[7].ToString();
        //        result.SALES_UNT = dr[8].ToString();
        //        result.UNIT_NAME = dr[9].ToString();
        //        result.CURRENCY = dr[10].ToString();
        //        result.UNIT_PRICT = dr[11].ToString();
        //        result.NETUNIT_PRICE = dr[12].ToString();
        //        result.TAX_AMOUNT = dr[13].ToString();
        //        result.CHARGE_AMOUNT = dr[14].ToString();
        //        result.ALLOWANCE_AMOUNT = dr[15].ToString();
        //        result.TAX_BASIS_AMOUNT = dr[16].ToString();
        //        result.TAX_TOTAL_AMOUNT = dr[17].ToString();
        //        result.GRAND_TOTAL_AMOUNT = dr[18].ToString();
        //        result.PO_NUMBER = dr[19].ToString();
        //        result.BILLING_DATE = dr[20].ToString();

        //    }
        //    catch (Exception ex)
        //    {
        //        //log.InsertLog(pathlog, "Exception : " + ex.ToString());
        //    }
        //    return result;
        //}
        //public bool CheckCancelBillingOrSentRevenueDepartment(string billno)
        //{
        //    bool result = false;
        //    try
        //    {
        //        var datatran = _dbContext.transactionDescription.FirstOrDefault(x => x.BillingNumber == billno);
        //        if (datatran != null)
        //        {
        //            if (toolHelper.ConvertIntToBoolean(datatran.CancelBilling) || toolHelper.ConvertIntToBoolean(datatran.SentRevenueDepartment))
        //            {
        //                result = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //log.InsertLog(pathlog, "Exception : " + ex.ToString());
        //    }
        //    return result;
        //}

        //public bool InsertDataTransactionDescription(TextFileSchema dataxml, string source)
        //{
        //    TransactionDescription data = new TransactionDescription();
        //    Task<Response> res;
        //    bool result = false;
        //    bool insert = false;
        //    double ic = 0;
        //    string imageDocType = "";
        //    string doctype = "";
        //    DateTime billingdate = DateTime.Now;
        //    string poNumber = "";
        //    try
        //    {
        //        string errorText = "";

        //        data = _dbContext.transactionDescription.Where(t => t.BillingNumber == dataxml.BILLING_NO).FirstOrDefault();

        //        ProfileCompany profiledetail = new ProfileCompany();
        //        DocumentCode doccode = new DocumentCode();

        //        doccode = _dbContext.documentCode.FirstOrDefault(x => x.DocumentCodeErp == dataxml.FI_DOC_TYPE);
        //        profiledetail = _dbContext.profileCompany.FirstOrDefault(x => x.TaxNumber == dataxml.SELLER_TIN);
        //        if (data == null)
        //        {
        //            insert = true;
        //            data = new TransactionDescription();
        //        }

        //        if (!String.IsNullOrEmpty(dataxml.BILLING_DATE))
        //        {
        //            billingdate = Convert.ToDateTime(dataxml.BILLING_DATE);
        //            data.BillingDate = billingdate;
        //            data.BillingYear = billingdate.Year;
        //        }
        //        if (!String.IsNullOrEmpty(dataxml.BILLING_NO))
        //        {
        //            data.BillingNumber = dataxml.BILLING_NO.ToString();
        //        }
        //        if (!String.IsNullOrEmpty(dataxml.Number_Bill_to))
        //        {
        //            data.BillTo = dataxml.Number_Bill_to.ToString();
        //        }

        //        if (profiledetail != null)
        //        {
        //            data.CompanyCode = profiledetail.CompanyCode;
        //            data.CompanyName = profiledetail.CompanyNameTh;
        //        }
        //        data.CreateBy = "Batch";
        //        data.CreateDate = DateTime.Now;
        //        data.CustomerId = dataxml.BUYER_CODE;
        //        data.CustomerName = dataxml.BUYER_NAME;
        //        if (doccode != null)
        //        {
        //            doctype = doccode.DocumentCodeRd;
        //            data.DocType = doccode.DocumentCodeRd;
        //        }

        //        if (!String.IsNullOrEmpty(dataxml.FI_DOC))
        //        {
        //            data.FiDoc = dataxml.FI_DOC.ToString();
        //        }
        //        data.Foc = (dataxml.FI_DOC_TYPE == "FOC") ? 1 : 0;
        //        if (!string.IsNullOrEmpty(doctype))
        //        {
        //            ic = 1;
        //            switch (doctype.ToUpper())
        //            {
        //                case "380":
        //                case "T02":
        //                case "T03":
        //                case "T04":
        //                case "T05":
        //                case "T06":
        //                    imageDocType = "ZFI1060,ZFI1061";
        //                    break;
        //                case "388":
        //                    imageDocType = "ZFI1060,ZFI1061";
        //                    break;
        //                case "80":
        //                    imageDocType = "ZFI1040,ZFI1041";
        //                    break;
        //                case "81":
        //                    imageDocType = "ZFI1050,ZFI1051";
        //                    break;
        //                case "T01":
        //                case "T07":
        //                default:
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            switch (doctype.ToUpper())
        //            {
        //                case "380":
        //                case "T02":
        //                case "T03":
        //                case "T04":
        //                case "T05":
        //                case "T06":
        //                    imageDocType = "ZFI1061";
        //                    break;
        //                case "388":
        //                    imageDocType = "ZFI1061";
        //                    break;
        //                case "80":
        //                    imageDocType = "ZFI1041";
        //                    break;
        //                case "81":
        //                    imageDocType = "ZFI1051";
        //                    break;
        //                case "T01":
        //                case "T07":
        //                default:
        //                    break;
        //            }
        //        }
        //        data.Ic = ic;
        //        data.ImageDocType = imageDocType;// mapping
        //        data.Isactive = 1;
        //        if (!String.IsNullOrEmpty(dataxml.Number_Payer))
        //        {
        //            data.Payer = dataxml.Number_Payer.ToString();
        //        }
        //        data.PostingYear = billingdate.Year.ToString();
        //        data.ProcessingDate = DateTime.Now;
        //        foreach (var item in dataxml.Item)
        //        {
        //            poNumber = poNumber + item.PO_NUMBER + ",";
        //        }
        //        poNumber = poNumber.Substring(0, poNumber.Length - 1);
        //        data.PoNumber = poNumber;
        //        data.SellOrg = dataxml.SALES_ORG;
        //        if (!String.IsNullOrEmpty(dataxml.Number_Ship_to))
        //        {
        //            data.ShipTo = dataxml.Number_Ship_to.ToString();
        //        }
        //        if (!String.IsNullOrEmpty(dataxml.Number_Sold_to))
        //        {
        //            data.SoldTo = dataxml.Number_Sold_to.ToString();
        //        }
        //        data.SourceName = source;
        //        data.TypeInput = "Batch";
        //        data.UpdateBy = "Batch";
        //        data.UpdateDate = DateTime.Now;
        //        data.EmailSendStatus = "Waiting";
        //        data.PdfIndexingStatus = "Waiting";
        //        data.PrintStatus = "Waiting";
        //        data.XmlCompressStatus = "Waiting";
        //        data.XmlSignStatus = "Waiting";
        //        data.EmailSendStatus = "Waiting";
        //        //data.PdfSignStatus = "Waiting";
        //        data.DmsAttachmentFileName = null;
        //        data.DmsAttachmentFilePath = null;

        //        if (insert == true)
        //        {
        //            _dbContext.transactionDescription.Add(data);
        //        }

        //        _dbContext.SaveChanges();
        //        result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //log.InsertLog(pathlog, "Exception : " + ex.ToString());
        //    }
        //    return result;
        //}

        //public List<string> ValidateTextFile(TextFileSchema textfile
        //    //, List<ProfileBranch> profileBranches
        //    //, List<ProductUnit> productUnit
        //    )
        //{
        //    List<string> result = new List<string>();
        //    bool checkproductname = false;
        //    int countlengthpo = 0;
        //    bool checksalesunit = false;
        //    bool checkprice = false;
        //    bool checkbuyeraddress = false;
        //    try
        //    {
        //        if (CheckDataRule(textfile.BUYER_PSTLZ, "") || CheckDataRule(textfile.SELLER_PSTLZ, ""))
        //        {
        //            result.Add("Failed ไม่มีรหัสไปรษณีย์");
        //        }
        //        if (CheckDataRule(textfile.FI_DOC_TYPE, "") || CheckDataRule(textfile.BILLING_NO, ""))
        //        {
        //            result.Add("Failed เลขทีอ้างอิง หรือประเภทเอกสารอ้างอิงไม่ครบถ้วน");
        //        }
        //        if (textfile.Item == null || textfile.Item.Count == 0)
        //        {
        //            result.Add("Failed ไม่มีข้อมูล Item");
        //        }
        //        else
        //        {
        //            foreach (var item in textfile.Item)
        //            {
        //                if (checkproductname == false && CheckDataRule(item.PRODUCT_NAME, ""))
        //                {
        //                    checkproductname = true;
        //                }
        //                if (checksalesunit == false && _dbContext.productUnit.FirstOrDefault(x => x.ProductUnitErp == item.SALES_UNT) == null)// map database
        //                {
        //                    checksalesunit = true;
        //                }
        //                try
        //                {
        //                    if (checkprice == false &&
        //                        (Convert.ToDecimal(item.UNIT_PRICT) < 0 ||
        //                        Convert.ToDecimal(item.NETUNIT_PRICE) < 0 ||
        //                        Convert.ToDecimal(item.TAX_AMOUNT) < 0 ||
        //                        Convert.ToDecimal(item.TAX_BASIS_AMOUNT) < 0 ||
        //                        Convert.ToDecimal(item.TAX_TOTAL_AMOUNT) < 0 ||
        //                        Convert.ToDecimal(item.GRAND_TOTAL_AMOUNT) < 0))
        //                    {
        //                        checkprice = true;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    checkprice = true;
        //                }
        //                countlengthpo += item.PO_NUMBER.Length;
        //            }
        //        }
        //        if (checkproductname)
        //        {
        //            result.Add("Failed ชื่อสินค้ามีค่าเป็น Null");
        //        }
        //        if (countlengthpo > 255)
        //        {
        //            result.Add("Failed เลขที่ PO ที่อ้างอิงในแต่ละ item เมื่อมารวมกันยาวเกินกว่าที่กำหนด");
        //        }
        //        //if (_dbContext.profileBranches.FirstOrDefault(x => x.ProfileBranchCode == textfile.SELLER_BRANCH) == null)
        //        //{
        //        //    //result.Add("Failed Branch " + textfile.SELLER_BRANCH + " ไม่ได้ mapping กับ e - Tax");
        //        //}
        //        if (checksalesunit)
        //        {
        //            result.Add("Failled มีอักษรพิเศษใน Unit of Measure");
        //        }
        //        try
        //        {
        //            if (checkprice || (Convert.ToDecimal(textfile.SALES_AMOUNT) < 0 ||
        //                Convert.ToDecimal(textfile.TAX_AMOUNT) < 0 ||
        //                //Convert.ToDecimal(textfile.CORRECT_AMOUNT) < 0 || 
        //                //Convert.ToDecimal(textfile.DIFFERENCE_AMOUNT) < 0 ||
        //                Convert.ToDecimal(textfile.GRAND_TOTAL_AMOUNT) < 0 ||
        //                //Convert.ToDecimal(textfile.ORIGINAL_AMOUNT) < 0 || 
        //                Convert.ToDecimal(textfile.TAX_BASIS_AMOUNT) < 0 ||
        //                Convert.ToDecimal(textfile.TAX_TOTAL_AMOUNT) < 0))
        //            {
        //                result.Add("Failed จำนวนเงินติดลบ");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            result.Add("Failed จำนวนเงินติดลบ");
        //        }
        //        string s = JsonConvert.SerializeObject(textfile);
        //        if (CheckSpecialChar(s))
        //        {
        //            result.Add("Failled มีอักษรพิเศษใน Text file");
        //        }
        //        if (CheckDataRule(textfile.BUYER_ADDRESS1, ""))
        //        {
        //            result.Add("Failed ไม่มีที่อยู่ผู้ซื้อ");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return result;
        //}
        //public bool CheckDataRule(string data, string datarule)
        //{
        //    bool result = false;
        //    try
        //    {
        //        data = data ?? "";
        //        string[] splitdate = datarule.Split("|");
        //        foreach (string item in splitdate)
        //        {
        //            if (data == item)
        //            {
        //                result = true;
        //                break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return result;
        //}
        //public bool CheckSpecialChar(string data)
        //{
        //    bool result = false;
        //    try
        //    {
        //        //Console.WriteLine("i/p is " + str);
        //        Regex rgx = new Regex("[^A-Za-z0-9ก-๙]");
        //        //bool isNUmber = int.TryParse(data, out int n);
        //        //bool hasSpecialChars = rgx.IsMatch(data.ToString()) || isNUmber;
        //        bool hasSpecialChars = rgx.IsMatch(data.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return result;
        //}

        //public bool GenTextFileFail(string filename, TextFileSchema dataxml, string path)
        //{
        //    bool result = false;
        //    string filePath = path + filename;
        //    string lineheader = "";
        //    List<string> lineitem = new List<string>();
        //    string line = "";
        //    try
        //    {
        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }

        //        lineheader += dataxml.HEADER_FLAG + "\\|";
        //        lineheader += dataxml.FI_DOC_TYPE + "\\|";
        //        lineheader += dataxml.FISCAL_YEAR + "\\|";
        //        lineheader += dataxml.IC_FLAG + "\\|";
        //        lineheader += dataxml.SELLER_TIN + "\\|";
        //        lineheader += dataxml.SELLER_BRANCH + "\\|";
        //        lineheader += dataxml.BUYER_TYPE_CODE + "\\|";
        //        lineheader += dataxml.BUYER_TAXID + "\\|";
        //        lineheader += dataxml.BUYER_BRANCH + "\\|";
        //        lineheader += dataxml.BUYER_NAME + "\\|";
        //        lineheader += dataxml.BUYER_CODE + "\\|";
        //        lineheader += dataxml.BUYER_PSTLZ + "\\|";
        //        lineheader += dataxml.BUYER_LAND1 + "\\|";
        //        lineheader += dataxml.SELLER_NAME + "\\|";
        //        lineheader += dataxml.SELLER_BUKRS + "\\|";
        //        lineheader += dataxml.SELLER_MAIL + "\\|";
        //        lineheader += dataxml.SELLER_PSTLZ + "\\|";
        //        lineheader += dataxml.SELLER_BUILD_NAME + "\\|";
        //        lineheader += dataxml.SELLER_STREET + "\\|";
        //        lineheader += dataxml.SELLER_CITY + "\\|";
        //        lineheader += dataxml.SELLER_SUBDIV + "\\|";
        //        lineheader += dataxml.SELLER_LAND1 + "\\|";
        //        lineheader += dataxml.SELLER_PROVINCE + "\\|";
        //        lineheader += dataxml.SELLER_BUILD_NO + "\\|";
        //        lineheader += dataxml.BUYER_ADDRESS1 + "\\|";
        //        lineheader += dataxml.BUYER_ADDRESS2 + "\\|";
        //        lineheader += dataxml.BILLING_NO + "\\|";
        //        lineheader += dataxml.FI_DOC + "\\|";
        //        lineheader += dataxml.DOC_CURRENCY + "\\|";
        //        lineheader += dataxml.SALES_AMOUNT + "\\|";
        //        lineheader += dataxml.TAX_CODE + "\\|";
        //        lineheader += dataxml.TAX_RATE + "\\|";
        //        lineheader += dataxml.TAX_AMOUNT + "\\|";
        //        lineheader += dataxml.CHARGE_AMOUNT + "\\|";
        //        lineheader += dataxml.ALLOWANCE_AMOUNT + "\\|";
        //        lineheader += dataxml.ORIGINAL_AMOUNT + "\\|";
        //        lineheader += dataxml.DIFFERENCE_AMOUNT + "\\|";
        //        lineheader += dataxml.TAX_BASIS_AMOUNT + "\\|";
        //        lineheader += dataxml.TAX_TOTAL_AMOUNT + "\\|";
        //        lineheader += dataxml.GRAND_TOTAL_AMOUNT + "\\|";
        //        lineheader += dataxml.CREATE_DATE_TIME + "\\|";
        //        lineheader += dataxml.REF_DOC + "\\|";
        //        lineheader += dataxml.REF_DOC_DATE + "\\|";
        //        lineheader += dataxml.REF_FI_DOCTYPE + "\\|";
        //        lineheader += dataxml.ORDER_REASON + "\\|";
        //        lineheader += dataxml.ORDER_REASON_CODE + "\\|";
        //        lineheader += dataxml.BILLING_DATE + "\\|";
        //        lineheader += dataxml.INSTANCE + "\\|";
        //        lineheader += dataxml.BILLING_TYPE + "\\|";
        //        lineheader += dataxml.SALES_ORG + "\\|";
        //        lineheader += dataxml.PRINT_DATE_TIME + "\\|";
        //        lineheader += dataxml.Number_Sold_to + "\\|";
        //        lineheader += dataxml.Number_Ship_to + "\\|";
        //        lineheader += dataxml.Number_Bill_to + "\\|";
        //        lineheader += dataxml.Number_Payer + "\\|";
        //        lineheader += dataxml.CORRECT_AMOUNT;

        //        foreach (var item in dataxml.Item)
        //        {
        //            line += item.ITEM_FLAG + "\\|";
        //            line += item.FI_DOC_TYPE + "\\|";
        //            line += item.SELLER_TIN + "\\|";
        //            line += item.SELLER_BRANCH + "\\|";
        //            line += item.BILLING_NO + "\\|";
        //            line += item.ITEM_NO + "\\|";
        //            line += item.PRODUCT_NAME + "\\|";
        //            line += item.QUANTITY + "\\|";
        //            line += item.SALES_UNT + "\\|";
        //            line += item.UNIT_NAME + "\\|";
        //            line += item.CURRENCY + "\\|";
        //            line += item.UNIT_PRICT + "\\|";
        //            line += item.NETUNIT_PRICE + "\\|";
        //            line += item.TAX_AMOUNT + "\\|";
        //            line += item.CHARGE_AMOUNT + "\\|";
        //            line += item.ALLOWANCE_AMOUNT + "\\|";
        //            line += item.TAX_BASIS_AMOUNT + "\\|";
        //            line += item.TAX_TOTAL_AMOUNT + "\\|";
        //            line += item.GRAND_TOTAL_AMOUNT + "\\|";
        //            line += item.PO_NUMBER + "\\|";
        //            line += item.BILLING_DATE;
        //            lineitem.Add(line);
        //        }

        //        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
        //        {
        //            StreamWriter write = new StreamWriter(fs);
        //            write.BaseStream.Seek(0, SeekOrigin.End);
        //            write.WriteLine(lineheader);
        //            foreach (var item in lineitem)
        //            {
        //                write.WriteLine(item);
        //            }
        //            write.Flush();
        //            write.Close();
        //            fs.Close();
        //            result = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //log.InsertLog(pathlog, "Exception : " + ex.ToString());
        //    }
        //    return result;
        //}
        //public CrossIndustryInvoice ConvertClasstoXMLFormat(TextFileSchema data)
        //{
        //    CrossIndustryInvoice result = new CrossIndustryInvoice();
        //    try
        //    {
        //        CrossIndustryInvoice xmldata;
        //        ExchangedDocument exchangedDocument;
        //        SupplyChainTradeTransaction supplyChainTradeTransaction;
        //        IncludedSupplyChainTradeLineItem includedSupplyChainTradeLineItem;
        //        ProfileSeller profile = new ProfileSeller();
        //        ProfileCompany profiledetail = new ProfileCompany();
        //        DocumentCode doccode = new DocumentCode();
        //        string schemeID = "OTHR";
        //        xmldata = new CrossIndustryInvoice();
        //        supplyChainTradeTransaction = new SupplyChainTradeTransaction();
        //        doccode = _dbContext.documentCode.FirstOrDefault(x => x.DocumentCodeErp == data.FI_DOC_TYPE);
        //        profiledetail = _dbContext.profileCompany.FirstOrDefault(x => x.TaxNumber == data.SELLER_TIN);

        //        xmldata.exchangedDocumentContext = new ExchangedDocumentContext();
        //        xmldata.exchangedDocumentContext.guidelineSpecifiedDocumentContextParameter = new GuidelineSpecifiedDocumentContextParameter();
        //        xmldata.exchangedDocumentContext.guidelineSpecifiedDocumentContextParameter.schemeAgencyID = "ETDA";
        //        xmldata.exchangedDocumentContext.guidelineSpecifiedDocumentContextParameter.id = "ER3-2560";
        //        xmldata.exchangedDocumentContext.guidelineSpecifiedDocumentContextParameter.schemeVersionID = "v2.0";

        //        string doccodetext = doccode.DocumentDescription ?? "";
        //        if (doccodetext.IndexOf('(') > 0)
        //        {
        //            doccodetext = doccodetext.Substring(0, doccodetext.IndexOf('(') - 1);
        //        }

        //        xmldata.exchangedDocument = new ExchangedDocument();
        //        xmldata.exchangedDocument.id = data.BILLING_NO ?? "";
        //        xmldata.exchangedDocument.name = doccodetext;
        //        xmldata.exchangedDocument.typeCode = doccode.DocumentCodeRd ?? "";
        //        xmldata.exchangedDocument.issueDateTime = data.BILLING_DATE ?? "";
        //        xmldata.exchangedDocument.createionDateTime = data.CREATE_DATE_TIME ?? "";

        //        profile = _dbContext.profileSeller.Where(x => x.CompanyCode == profiledetail.CompanyCode && x.BranchCode == data.SELLER_BRANCH).FirstOrDefault();
        //        supplyChainTradeTransaction.applicableHeaderTradeAgreement = new ApplicableHeaderTradeAgreement();
        //        supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty = new SellerTradeParty();
        //        supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.name = data.SELLER_NAME ?? "";
        //        supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration = new SpecifiedTaxRegistration();
        //        supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id = data.SELLER_TIN ?? "";
        //        if ((!CheckDataRule(data.BUYER_TAXID, "") && !CheckDataRule(data.BUYER_BRANCH, "")) && (data.BUYER_TAXID.Length == 13 && data.BUYER_BRANCH.Length == 5))
        //        {
        //            schemeID = "TXID";
        //        }
        //        else if ((!CheckDataRule(data.BUYER_TAXID, "")) && (data.BUYER_TAXID.Length == 13 && CheckDataRule(data.BUYER_BRANCH, "")))
        //        {
        //            schemeID = "NIDN";
        //        }
        //        supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeID = schemeID ?? "";
        //        supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeagencyid = "RD";
        //        supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress = new PostalTradeAddress();
        //        supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.postcodeCode = data.SELLER_PSTLZ ?? "";
        //        supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.streetName = profile.Road ?? "";
        //        supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.cityName = profile.District ?? "";
        //        supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.citySubDivisionName = profile.SubDistrict ?? "";
        //        supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.countryID = data.SELLER_LAND1 ?? "";
        //        supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.countrySubDivisionID = profile.Province ?? "";
        //        supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.buildingNumber = profile.Addressnumber ?? "";

        //        supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty = new BuyerTradeParty();
        //        supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.name = data.SELLER_NAME ?? "";
        //        supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration = new SpecifiedTaxRegistration();
        //        supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id = data.BUYER_TAXID ?? "" + data.BUYER_BRANCH ?? "";
        //        supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeagencyid = "RD";
        //        supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeID = "TXID";
        //        supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress = new PostalTradeAddress();
        //        if (data.BUYER_LAND1.ToUpper() != "TH" && String.IsNullOrEmpty(data.BUYER_PSTLZ))
        //        {
        //            supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.postcodeCode = "0";
        //        }
        //        else
        //        {
        //            supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.postcodeCode = data.BUYER_PSTLZ ?? "";
        //        }
        //        supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.line1 = data.BUYER_ADDRESS1 ?? "";
        //        supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.countryID = data.BUYER_LAND1 ?? "";

        //        supplyChainTradeTransaction.applicableHeaderTradeSettlement = new ApplicableHeaderTradeSettlement();
        //        supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoiceCurrencyCode = new InvoiceCurrencyCode();
        //        supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoiceCurrencyCode.invoiceCurrencyCode = data.DOC_CURRENCY ?? "";
        //        supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoiceCurrencyCode.listID = "ISO 4217 3A";
        //        supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax = new ApplicableTradeTax();
        //        supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.typeCode = _dbContext.taxCode.FirstOrDefault(x => x.TaxCodeErp == data.TAX_CODE).TaxCodeRd ?? "";
        //        supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.calculatedRate = data.TAX_RATE ?? "";
        //        supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.basisAmount = data.SALES_AMOUNT ?? "";
        //        supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.calculatedAmount = data.TAX_AMOUNT ?? "";
        //        supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation = new SpecifiedTradeSettlementHeaderMonetarySummation();
        //        supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.lineTotalAmount = data.SALES_AMOUNT ?? "";
        //        supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.allowanceTotalAmount = data.ALLOWANCE_AMOUNT ?? "";
        //        supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.taxBasisTotalAmount = data.TAX_BASIS_AMOUNT ?? "";
        //        supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.taxTotalAmount = data.TAX_TOTAL_AMOUNT ?? "";
        //        supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.grandTotalAmount = data.GRAND_TOTAL_AMOUNT ?? "";
        //        supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.chargeTotalAmount = data.CHARGE_AMOUNT ?? "";

        //        supplyChainTradeTransaction.includedSupplyChainTradeLineItem = new List<IncludedSupplyChainTradeLineItem>();
        //        for (int i = 0; i < data.Item.Count; i++)
        //        {
        //            includedSupplyChainTradeLineItem = new IncludedSupplyChainTradeLineItem();
        //            includedSupplyChainTradeLineItem.associatedDocumentLineDocument = new AssociatedDocumentLineDocument();
        //            includedSupplyChainTradeLineItem.associatedDocumentLineDocument.lineID = (i + 1).ToString();
        //            includedSupplyChainTradeLineItem.specifiedTradeProduct = new SpecifiedTradeProduct();
        //            includedSupplyChainTradeLineItem.specifiedTradeProduct.name = data.Item[i].PRODUCT_NAME ?? "";
        //            includedSupplyChainTradeLineItem.specifiedTradeProduct.informationNote = new InformationNote();
        //            includedSupplyChainTradeLineItem.specifiedTradeProduct.informationNote.subject = data.Item[i].PO_NUMBER ?? "test";
        //            if (string.IsNullOrEmpty(data.Item[i].PO_NUMBER))
        //            {
        //                includedSupplyChainTradeLineItem.specifiedTradeProduct.informationNote.subject = "test";
        //            }
        //            includedSupplyChainTradeLineItem.specifiedLineTradeAgreement = new SpecifiedLineTradeAgreement();
        //            includedSupplyChainTradeLineItem.specifiedLineTradeAgreement.grossPriceProductTradePrice = new GrossPriceProductTradePrice();
        //            includedSupplyChainTradeLineItem.specifiedLineTradeAgreement.grossPriceProductTradePrice.chargeAmount = data.Item[i].CHARGE_AMOUNT ?? "";
        //            includedSupplyChainTradeLineItem.specifiedLineTradeDelivery = new SpecifiedLineTradeDelivery();
        //            includedSupplyChainTradeLineItem.specifiedLineTradeDelivery.billedQuantity = new BilledQuantity();
        //            includedSupplyChainTradeLineItem.specifiedLineTradeDelivery.billedQuantity.unitCode = data.Item[i].SALES_UNT ?? "";
        //            includedSupplyChainTradeLineItem.specifiedLineTradeDelivery.billedQuantity.billedQuantity = data.Item[i].QUANTITY ?? "";
        //            includedSupplyChainTradeLineItem.specifiedLineTradeSettlement = new SpecifiedLineTradeSettlement();
        //            includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge = new SpecifiedTradeAllowanceCharge();
        //            if (Convert.ToDecimal(data.ALLOWANCE_AMOUNT) > 0)
        //            {
        //                includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge.chargeIndicator = "true";
        //            }
        //            else
        //            {
        //                includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge.chargeIndicator = "false";
        //            }
        //            includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge.actualAmount = data.ALLOWANCE_AMOUNT ?? "";
        //            includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation = new SpecifiedTradeSettlementLineMonetarySummation();
        //            includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.taxTotalAmount = data.TAX_AMOUNT ?? "";
        //            includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.NetLineTotalAmount = data.TAX_BASIS_AMOUNT ?? "";
        //            includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.netIncludingTaxesLineTotalAmount = new NetIncludingTaxesLineTotalAmount();
        //            includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.netIncludingTaxesLineTotalAmount.currencyID = data.Item[i].CURRENCY ?? "";//format
        //            includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.netIncludingTaxesLineTotalAmount.netIncludingTaxesLineTotalAmount = data.Item[i].GRAND_TOTAL_AMOUNT ?? "";//format
        //            supplyChainTradeTransaction.includedSupplyChainTradeLineItem.Add(includedSupplyChainTradeLineItem);
        //        }

        //        xmldata.supplyChainTradeTransaction = supplyChainTradeTransaction;
        //        result = xmldata;
        //    }
        //    catch (Exception ex)
        //    {
        //        //log.InsertLog(pathlog, "Exception : " + ex.ToString());
        //    }
        //    return result;
        //}
        //public bool GenXMLFromTemplate(CrossIndustryInvoice data, string pathoutbound, string filename, string billingno, string billingdate)
        //{
        //    bool result = false;
        //    //pathoutbound = @"D:\gen\gen.xml";
        //    XDocument xml = new XDocument();
        //    List<string> errormessage = new List<string>();
        //    try
        //    {
        //        DateTime billdate = Convert.ToDateTime(billingdate);
        //        //pathoutbound += "\\" + billdate.ToString("yyyy") + "\\" + billdate.ToString("MM") + "\\";
        //        if (!Directory.Exists(pathoutbound))
        //        {
        //            Directory.CreateDirectory(pathoutbound);
        //        }
        //        switch (data.exchangedDocument.typeCode)
        //        {
        //            case "388":
        //            case "T02":
        //            case "T03":
        //            case "T04":
        //                xml = XMLTaxInvoiceTemplate(data);
        //                result = GenXMLFile(xml, pathoutbound + filename);
        //                break;
        //            case "80":
        //            case "81":
        //                xml = XMLDebitCreditNoteTemplate(data);
        //                result = GenXMLFile(xml, pathoutbound + filename);
        //                break;
        //            case "T01":
        //            default:
        //                break;
        //        }
        //        UpdateDataTransaction(errormessage, billingno);

        //    }
        //    catch (Exception ex)
        //    {
        //        //log.InsertLog(pathlog, "Exception : " + ex.ToString());
        //    }
        //    return result;
        //}

        //public bool GenXMLFile(XDocument xml, string fullpath)
        //{
        //    bool result = false;
        //    try
        //    {

        //        TextWriter filestream = new StreamWriter(fullpath);
        //        //TextWriter filestream = new StreamWriter(@"C:\Code_Dev\output.xml");
        //        xml.Save(filestream);
        //        result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //log.InsertLog(pathlog, "Exception : " + ex.ToString());
        //    }
        //    return result;
        //}

        //public XDocument XMLTaxInvoiceTemplate(CrossIndustryInvoice data)
        //{
        //    XDocument xmlDocument = new XDocument();
        //    try
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        sb.Append("<rsm:TaxInvoice_CrossIndustryInvoice xmlns:rsm='urn:etda:uncefact:data:standard:TaxInvoice_CrossIndustryInvoice:2' ");
        //        sb.Append("xmlns:ram='urn:etda:uncefact:data:standard:TaxInvoice_ReusableAggregateBusinessInformationEntity:2' ");
        //        sb.Append("xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' ");
        //        sb.Append("xsi:schemaLocation='urn:etda:uncefact:data:standard:TaxInvoice_CrossIndustryInvoice:2 file:../data /standard/TaxInvoice_CrossIndustryInvoice_2p0.xsd'> ");
        //        sb.Append("<rsm:ExchangedDocumentContext>");
        //        sb.Append("<ram:GuidelineSpecifiedDocumentContextParameter>");
        //        sb.Append("<ram:ID schemeAgencyID='" + data.exchangedDocumentContext.guidelineSpecifiedDocumentContextParameter.schemeAgencyID + "' schemeVersionID='" + data.exchangedDocumentContext.guidelineSpecifiedDocumentContextParameter.schemeVersionID + "'>" + data.exchangedDocumentContext.guidelineSpecifiedDocumentContextParameter.id + "</ram:ID>");
        //        sb.Append("</ram:GuidelineSpecifiedDocumentContextParameter>");
        //        sb.Append("</rsm:ExchangedDocumentContext>");
        //        sb.Append("<rsm:ExchangedDocument>");
        //        sb.Append("<ram:ID>" + data.exchangedDocument.id + "</ram:ID>");
        //        sb.Append("<ram:Name>" + data.exchangedDocument.name + "</ram:Name>");
        //        sb.Append("<ram:TypeCode>" + data.exchangedDocument.typeCode + "</ram:TypeCode>");
        //        sb.Append("<ram:IssueDateTime>" + data.exchangedDocument.issueDateTime + "</ram:IssueDateTime>");
        //        sb.Append("<ram:CreationDateTime>" + data.exchangedDocument.createionDateTime + "</ram:CreationDateTime>");
        //        sb.Append("</rsm:ExchangedDocument>");
        //        sb.Append("<rsm:SupplyChainTradeTransaction>");
        //        sb.Append("<ram:ApplicableHeaderTradeAgreement>");
        //        sb.Append("<ram:SellerTradeParty>");
        //        sb.Append("<ram:Name>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.name + "</ram:Name>");
        //        sb.Append("<ram:SpecifiedTaxRegistration>");
        //        sb.Append("<ram:ID schemeAgencyID='" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeagencyid + "' schemeID='" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeID + "'>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id + "</ram:ID>");
        //        sb.Append("</ram:SpecifiedTaxRegistration>");
        //        sb.Append("<ram:PostalTradeAddress>");
        //        sb.Append("<ram:PostcodeCode>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.postcodeCode + "</ram:PostcodeCode>");
        //        sb.Append("<ram:StreetName>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.streetName + "</ram:StreetName>");
        //        sb.Append("<ram:CityName>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.cityName + "</ram:CityName>");
        //        sb.Append("<ram:CitySubDivisionName>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.citySubDivisionName + "</ram:CitySubDivisionName>");
        //        sb.Append("<ram:CountryID schemeID='3166-1 alpha-2'>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.countryID + "</ram:CountryID>");
        //        sb.Append("<ram:CountrySubDivisionID>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.countrySubDivisionID + "</ram:CountrySubDivisionID>");
        //        sb.Append("<ram:BuildingNumber>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.buildingNumber + "</ram:BuildingNumber>");
        //        sb.Append("</ram:PostalTradeAddress>");
        //        sb.Append("</ram:SellerTradeParty>");
        //        sb.Append("<ram:BuyerTradeParty>");
        //        sb.Append("<ram:Name>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.name + "</ram:Name>");
        //        sb.Append("<ram:SpecifiedTaxRegistration>");
        //        sb.Append("<ram:ID schemeAgencyID='" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeagencyid + "' schemeID='" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeID + "'>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id + "</ram:ID>");
        //        sb.Append("</ram:SpecifiedTaxRegistration>");
        //        sb.Append("<ram:PostalTradeAddress>");
        //        sb.Append("<ram:PostcodeCode>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.postcodeCode + "</ram:PostcodeCode>");
        //        sb.Append("<ram:LineOne>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.line1 + "</ram:LineOne>");
        //        sb.Append("<ram:CountryID schemeID='3166-1 alpha-2'>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.countryID + "</ram:CountryID>");
        //        sb.Append("</ram:PostalTradeAddress>");
        //        sb.Append("</ram:BuyerTradeParty>");
        //        sb.Append("</ram:ApplicableHeaderTradeAgreement>");
        //        sb.Append("<ram:ApplicableHeaderTradeDelivery/>");
        //        sb.Append("<ram:ApplicableHeaderTradeSettlement>");
        //        sb.Append("<ram:InvoiceCurrencyCode listID='" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoiceCurrencyCode.listID + "'>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoiceCurrencyCode.invoiceCurrencyCode + "</ram:InvoiceCurrencyCode>");
        //        sb.Append("<ram:ApplicableTradeTax>");
        //        sb.Append("<ram:TypeCode>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.typeCode + "</ram:TypeCode>");
        //        sb.Append("<ram:CalculatedRate>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.calculatedRate + "</ram:CalculatedRate>");
        //        sb.Append("<ram:BasisAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.basisAmount + "</ram:BasisAmount>");
        //        sb.Append("<ram:CalculatedAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.calculatedAmount + "</ram:CalculatedAmount>");
        //        sb.Append("</ram:ApplicableTradeTax>");
        //        sb.Append("<ram:SpecifiedTradeSettlementHeaderMonetarySummation>");
        //        sb.Append("<ram:LineTotalAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.lineTotalAmount + "</ram:LineTotalAmount>");
        //        sb.Append("<ram:AllowanceTotalAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.allowanceTotalAmount + "</ram:AllowanceTotalAmount>");
        //        sb.Append("<ram:ChargeTotalAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.chargeTotalAmount + "</ram:ChargeTotalAmount>");
        //        sb.Append("<ram:TaxBasisTotalAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.taxBasisTotalAmount + "</ram:TaxBasisTotalAmount>");
        //        sb.Append("<ram:TaxTotalAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.taxTotalAmount + "</ram:TaxTotalAmount>");
        //        sb.Append("<ram:GrandTotalAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.grandTotalAmount + "</ram:GrandTotalAmount>");
        //        sb.Append("</ram:SpecifiedTradeSettlementHeaderMonetarySummation>");
        //        sb.Append("</ram:ApplicableHeaderTradeSettlement>");
        //        foreach (var item in data.supplyChainTradeTransaction.includedSupplyChainTradeLineItem)
        //        {
        //            sb.Append("<ram:IncludedSupplyChainTradeLineItem>");
        //            sb.Append("<ram:AssociatedDocumentLineDocument>");
        //            sb.Append("<ram:LineID>" + item.associatedDocumentLineDocument.lineID + "</ram:LineID>");
        //            sb.Append("</ram:AssociatedDocumentLineDocument>");
        //            sb.Append("<ram:SpecifiedTradeProduct>");
        //            sb.Append("<ram:Name>" + item.specifiedTradeProduct.name + "</ram:Name>");
        //            sb.Append("<ram:InformationNote>");
        //            sb.Append("<ram:Subject>" + item.specifiedTradeProduct.informationNote.subject + "</ram:Subject>");
        //            sb.Append("</ram:InformationNote>");
        //            sb.Append("</ram:SpecifiedTradeProduct>");
        //            sb.Append("<ram:SpecifiedLineTradeAgreement>");
        //            sb.Append("<ram:GrossPriceProductTradePrice>");
        //            sb.Append("<ram:ChargeAmount>" + item.specifiedLineTradeAgreement.grossPriceProductTradePrice.chargeAmount + "</ram:ChargeAmount>");
        //            sb.Append("</ram:GrossPriceProductTradePrice>");
        //            sb.Append("</ram:SpecifiedLineTradeAgreement>");
        //            sb.Append("<ram:SpecifiedLineTradeDelivery>");
        //            sb.Append("<ram:BilledQuantity unitCode='" + item.specifiedLineTradeDelivery.billedQuantity.unitCode + "'>" + item.specifiedLineTradeDelivery.billedQuantity.billedQuantity + "</ram:BilledQuantity>");
        //            sb.Append("</ram:SpecifiedLineTradeDelivery>");
        //            sb.Append("<ram:SpecifiedLineTradeSettlement>");
        //            sb.Append("<ram:SpecifiedTradeAllowanceCharge>");
        //            sb.Append("<ram:ChargeIndicator>" + item.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge.chargeIndicator + "</ram:ChargeIndicator>");
        //            sb.Append("<ram:ActualAmount>" + item.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge.actualAmount + "0</ram:ActualAmount>");
        //            sb.Append("</ram:SpecifiedTradeAllowanceCharge>");
        //            //sb.Append("<ram:SpecifiedTradeAllowanceCharge>");
        //            //sb.Append("<ram:ChargeIndicator>" + item.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge.chargeIndicator + "</ram:ChargeIndicator>");
        //            //sb.Append("<ram:ActualAmount>" + item.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge.actualAmount + "</ram:ActualAmount>");
        //            //sb.Append("</ram:SpecifiedTradeAllowanceCharge>");
        //            sb.Append("<ram:SpecifiedTradeSettlementLineMonetarySummation>");
        //            sb.Append("<ram:TaxTotalAmount>" + item.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.taxTotalAmount + "</ram:TaxTotalAmount>");
        //            sb.Append("<ram:NetLineTotalAmount currencyID='" + item.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.netIncludingTaxesLineTotalAmount.currencyID + "'>" + item.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.NetLineTotalAmount + "</ram:NetLineTotalAmount>");
        //            sb.Append("<ram:NetIncludingTaxesLineTotalAmount currencyID='" + item.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.netIncludingTaxesLineTotalAmount.currencyID + "'>" + item.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.netIncludingTaxesLineTotalAmount.netIncludingTaxesLineTotalAmount + "</ram:NetIncludingTaxesLineTotalAmount>");
        //            sb.Append("</ram:SpecifiedTradeSettlementLineMonetarySummation>");
        //            sb.Append("</ram:SpecifiedLineTradeSettlement>");
        //            sb.Append("</ram:IncludedSupplyChainTradeLineItem>");
        //        }

        //        sb.Append("</rsm:SupplyChainTradeTransaction>");
        //        sb.Append("</rsm:TaxInvoice_CrossIndustryInvoice>");

        //        TextReader textReader = new StringReader(sb.ToString());
        //        xmlDocument = XDocument.Load(textReader);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return xmlDocument;
        //}

        //public XDocument XMLDebitCreditNoteTemplate(CrossIndustryInvoice data)
        //{
        //    XDocument xmlDocument = new XDocument();
        //    try
        //    {
        //        StringBuilder sb = new StringBuilder();

        //        sb.Append("<rsm:DebitCreditNote_CrossIndustryInvoice xmlns:rsm='urn:etda:uncefact:data:standard:DebitCreditNote_CrossIndustryInvoice:2'");
        //        sb.Append("xmlns:ram='urn:etda:uncefact:data:standard:DebitCreditNote_ReusableAggregateBusinessInformationEntity:2'");
        //        sb.Append("xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'");
        //        sb.Append("xsi:schemaLocation='urn:etda:uncefact:data:standard:DebitCreditNote_CrossIndustryInvoice:2 file: ../data/standard/DebitCreditNote_CrossIndustryInvoice_2p0.xsd'>");
        //        sb.Append("<rsm:ExchangedDocumentContext>");
        //        sb.Append("<ram:GuidelineSpecifiedDocumentContextParameter>");
        //        sb.Append("<ram:ID schemeAgencyID='" + data.exchangedDocumentContext.guidelineSpecifiedDocumentContextParameter.schemeAgencyID + "' schemeVersionID='" + data.exchangedDocumentContext.guidelineSpecifiedDocumentContextParameter.schemeVersionID + "'>" + data.exchangedDocumentContext.guidelineSpecifiedDocumentContextParameter.id + "</ram:ID>");
        //        sb.Append("</ram:GuidelineSpecifiedDocumentContextParameter>");
        //        sb.Append("</rsm:ExchangedDocumentContext>");
        //        sb.Append("<rsm:ExchangedDocument>");
        //        sb.Append("<ram:ID>" + data.exchangedDocument.id + "</ram:ID>");
        //        sb.Append("<ram:Name>" + data.exchangedDocument.name + "</ram:Name>");
        //        sb.Append("<ram:TypeCode>" + data.exchangedDocument.typeCode + "</ram:TypeCode>");
        //        sb.Append("<ram:IssueDateTime>" + data.exchangedDocument.issueDateTime + "</ram:IssueDateTime>");
        //        sb.Append("<ram:Purpose>" + data.exchangedDocument.purpose + "</ram:Purpose>");
        //        sb.Append("<ram:PurposeCode>" + data.exchangedDocument.purposeCode + "</ram:PurposeCode>");
        //        sb.Append("<ram:CreationDateTime>" + data.exchangedDocument.createionDateTime + "</ram:CreationDateTime>");
        //        sb.Append("</rsm:ExchangedDocument>");
        //        sb.Append("<rsm:SupplyChainTradeTransaction>");
        //        sb.Append("<ram:ApplicableHeaderTradeAgreement>");
        //        sb.Append("<ram:SellerTradeParty>");
        //        sb.Append("<ram:Name>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.name + "</ram:Name>");
        //        sb.Append("<ram:SpecifiedTaxRegistration>");
        //        sb.Append("<ram:ID schemeAgencyID='" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeagencyid + "' schemeID='" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeID + "'>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id + "</ram:ID>");
        //        sb.Append("</ram:SpecifiedTaxRegistration>");
        //        sb.Append("<ram:PostalTradeAddress>");
        //        sb.Append("<ram:PostcodeCode>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.postcodeCode + "</ram:PostcodeCode>");
        //        sb.Append("<ram:StreetName>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.streetName + "</ram:StreetName>");
        //        sb.Append("<ram:CityName>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.cityName + "</ram:CityName>");
        //        sb.Append("<ram:CitySubDivisionName>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.citySubDivisionName + "</ram:CitySubDivisionName>");
        //        sb.Append("<ram:CountryID schemeID='3166-1alpha-2'>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.countryID + "</ram:CountryID>");
        //        sb.Append("<ram:CountrySubDivisionID>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.countrySubDivisionID + "</ram:CountrySubDivisionID>");
        //        sb.Append("<ram:BuildingNumber>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.buildingNumber + "</ram:BuildingNumber>");
        //        sb.Append("</ram:PostalTradeAddress>");
        //        sb.Append("</ram:SellerTradeParty>");
        //        sb.Append("<ram:BuyerTradeParty>");
        //        sb.Append("<ram:Name>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.name + "</ram:Name>");
        //        sb.Append("<ram:SpecifiedTaxRegistration>");
        //        sb.Append("<ram:ID schemeAgencyID='" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeagencyid + "' schemeID='" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeID + "'>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id + "</ram:ID>");
        //        sb.Append("</ram:SpecifiedTaxRegistration>");
        //        sb.Append("<ram:PostalTradeAddress>");
        //        sb.Append("<ram:PostcodeCode>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.postcodeCode + "</ram:PostcodeCode>");
        //        sb.Append("<ram:LineOne>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.line1 + "</ram:LineOne>");
        //        sb.Append("<ram:CountryID schemeID='3166-1alpha-2'>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.countryID + "</ram:CountryID>");
        //        sb.Append("</ram:PostalTradeAddress>");
        //        sb.Append("</ram:BuyerTradeParty>");
        //        sb.Append("<ram:AdditionalReferencedDocument>");
        //        sb.Append("<ram:IssuerAssignedID>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.additionalReferencedDocument.issuerAssignedID + "</ram:IssuerAssignedID>");
        //        sb.Append("<ram:IssueDateTime>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.additionalReferencedDocument.issueDateTime + "</ram:IssueDateTime>");
        //        sb.Append("<ram:ReferenceTypeCode>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.additionalReferencedDocument.referenceTypeCode + "</ram:ReferenceTypeCode>");
        //        sb.Append("</ram:AdditionalReferencedDocument>");
        //        sb.Append("</ram:ApplicableHeaderTradeAgreement>");
        //        sb.Append("<ram:ApplicableHeaderTradeDelivery/>");
        //        sb.Append("<ram:ApplicableHeaderTradeSettlement>");
        //        sb.Append("<ram:InvoiceCurrencyCode listID='" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoiceCurrencyCode.listID + "'>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoiceCurrencyCode.invoiceCurrencyCode + "</ram:InvoiceCurrencyCode>");
        //        sb.Append("<ram:ApplicableTradeTax>");
        //        sb.Append("<ram:TypeCode>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.typeCode + "</ram:TypeCode>");
        //        sb.Append("<ram:CalculatedRate>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.calculatedRate + "</ram:CalculatedRate>");
        //        sb.Append("<ram:BasisAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.basisAmount + "</ram:BasisAmount>");
        //        sb.Append("<ram:CalculatedAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.calculatedAmount + "</ram:CalculatedAmount>");
        //        sb.Append("</ram:ApplicableTradeTax>");
        //        sb.Append("<ram:SpecifiedTradeSettlementHeaderMonetarySummation>");
        //        sb.Append("<ram:OriginalInformationAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.originalInformationAmount + "</ram:OriginalInformationAmount>");
        //        sb.Append("<ram:LineTotalAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.lineTotalAmount + "</ram:LineTotalAmount>");
        //        sb.Append("<ram:DifferenceInformationAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.differenceSalesInformationAmount + "</ram:DifferenceInformationAmount>");
        //        sb.Append("<ram:AllowanceTotalAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.allowanceTotalAmount + "</ram:AllowanceTotalAmount>");
        //        sb.Append("<ram:ChargeTotalAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.chargeTotalAmount + "</ram:ChargeTotalAmount>");
        //        sb.Append("<ram:TaxBasisTotalAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.taxBasisTotalAmount + "</ram:TaxBasisTotalAmount>");
        //        sb.Append("<ram:TaxTotalAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.taxTotalAmount + "</ram:TaxTotalAmount>");
        //        sb.Append("<ram:GrandTotalAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.grandTotalAmount + "</ram:GrandTotalAmount>");
        //        sb.Append("</ram:SpecifiedTradeSettlementHeaderMonetarySummation>");
        //        sb.Append("</ram:ApplicableHeaderTradeSettlement>");
        //        foreach (var item in data.supplyChainTradeTransaction.includedSupplyChainTradeLineItem)
        //        {
        //            sb.Append("<ram:IncludedSupplyChainTradeLineItem>");
        //            sb.Append("<ram:AssociatedDocumentLineDocument>");
        //            sb.Append("<ram:LineID>" + item.associatedDocumentLineDocument.lineID + "</ram:LineID>");
        //            sb.Append("</ram:AssociatedDocumentLineDocument>");
        //            sb.Append("<ram:SpecifiedTradeProduct>");
        //            sb.Append("<ram:Name>" + item.specifiedTradeProduct.name + "</ram:Name>");
        //            sb.Append("</ram:SpecifiedTradeProduct>");
        //            sb.Append("<ram:SpecifiedLineTradeAgreement>");
        //            sb.Append("<ram:GrossPriceProductTradePrice>");
        //            sb.Append("<ram:ChargeAmount>" + item.specifiedLineTradeAgreement.grossPriceProductTradePrice.chargeAmount + "</ram:ChargeAmount>");
        //            sb.Append("</ram:GrossPriceProductTradePrice>");
        //            sb.Append("</ram:SpecifiedLineTradeAgreement>");
        //            sb.Append("<ram:SpecifiedLineTradeDelivery>");
        //            sb.Append("<ram:BilledQuantity unitCode='" + item.specifiedLineTradeDelivery.billedQuantity.unitCode + "'>" + item.specifiedLineTradeDelivery.billedQuantity.billedQuantity + "</ram:BilledQuantity>");
        //            sb.Append("</ram:SpecifiedLineTradeDelivery>");
        //            sb.Append("<ram:SpecifiedLineTradeSettlement>");
        //            sb.Append("<ram:SpecifiedTradeAllowanceCharge>");
        //            sb.Append("<ram:ChargeIndicator>" + item.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge.chargeIndicator + "</ram:ChargeIndicator>");
        //            sb.Append("<ram:ActualAmount>" + item.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge.actualAmount + "</ram:ActualAmount>");
        //            sb.Append("</ram:SpecifiedTradeAllowanceCharge>");
        //            //sb.Append("<ram:SpecifiedTradeAllowanceCharge>");
        //            //sb.Append("<ram:ChargeIndicator>" + item.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge.chargeIndicator + "</ram:ChargeIndicator>");
        //            //sb.Append("<ram:ActualAmount>" + item.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge.actualAmount + "</ram:ActualAmount>");
        //            //sb.Append("</ram:SpecifiedTradeAllowanceCharge>");
        //            sb.Append("<ram:SpecifiedTradeSettlementLineMonetarySummation>");
        //            sb.Append("<ram:TaxTotalAmount>" + item.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.taxTotalAmount + "</ram:TaxTotalAmount>");
        //            sb.Append("<ram:NetLineTotalAmount currencyID='" + item.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.netIncludingTaxesLineTotalAmount.currencyID + "'>" + item.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.NetLineTotalAmount + "</ram:NetLineTotalAmount>");
        //            sb.Append("<ram:NetIncludingTaxesLineTotalAmount currencyID='" + item.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.netIncludingTaxesLineTotalAmount.currencyID + "'>" + item.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.netIncludingTaxesLineTotalAmount.netIncludingTaxesLineTotalAmount + "</ram:NetIncludingTaxesLineTotalAmount>");
        //            sb.Append("</ram:SpecifiedTradeSettlementLineMonetarySummation>");
        //            sb.Append("</ram:SpecifiedLineTradeSettlement>");
        //            sb.Append("</ram:IncludedSupplyChainTradeLineItem>");
        //        }
        //        sb.Append("</rsm:SupplyChainTradeTransaction>");
        //        sb.Append("</rsm:DebitCreditNote_CrossIndustryInvoice>");

        //        TextReader textReader = new StringReader(sb.ToString());
        //        xmlDocument = XDocument.Load(textReader);


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return xmlDocument;
        //}

        //#endregion

        //#region Create/Update Transaction and manage file
        //public bool UpdateDataTransaction(List<string> errormessage, string billingNo)
        //{
        //    bool result = false;
        //    string errorText = "";
        //    Task<Response> res;
        //    TransactionDescription dataTran = new TransactionDescription();
        //    try
        //    {
        //        dataTran = _dbContext.transactionDescription.FirstOrDefault(t => t.BillingNumber == billingNo);
        //        if (dataTran != null)
        //        {
        //            if (errormessage.Count == 0)
        //            {
        //                dataTran.GenerateDateTime = DateTime.Now;
        //                dataTran.GenerateDetail = "XML was generated completely";
        //                dataTran.GenerateStatus = "Successful";
        //                dataTran.UpdateBy = "API";
        //                dataTran.UpdateDate = DateTime.Now;

        //                _dbContext.SaveChanges();
        //                result = true;
        //            }
        //            else
        //            {
        //                dataTran.GenerateDateTime = DateTime.Now;
        //                foreach (var error in errormessage)
        //                {
        //                    errorText = errorText + error + "|";
        //                }
        //                errorText = errorText.Substring(0, errorText.Length - 1);
        //                dataTran.GenerateDetail = errorText;
        //                dataTran.GenerateStatus = "Failed";
        //                dataTran.UpdateBy = "API";
        //                dataTran.UpdateDate = DateTime.Now;

        //                _dbContext.SaveChanges();
        //                result = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //log.InsertLog(pathlog, "Exception : " + ex.ToString());
        //    }
        //    return result;
        //}

        public string CreateFileFromBase64(string fileBase64, string fullpath)
        {
            try
            {
                // Delete old file
                if (File.Exists(fullpath))
                {
                    try
                    {
                        File.Delete(fullpath);
                    }
                    catch
                    {

                    }
                }

                string directoryPath = System.IO.Path.GetDirectoryName(fullpath);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                // create new file
                byte[] bytes = Convert.FromBase64String(fileBase64);
                FileStream stream = new FileStream(fullpath, FileMode.CreateNew);
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(bytes, 0, bytes.Length);
                writer.Close();

                return fullpath;
            }
            catch (Exception ex)
            {

            }
            return "";
        }
        //public bool MoveFile(string pathinput, string filename,string pathoutput)
        //{
        //    bool result = false;
        //    //string pathoutput = @"D:\sign\backupfile\";
        //    string output = "";
        //    try
        //    {
        //        output = pathoutput + "\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "\\";
        //        if (!File.Exists(pathinput))
        //        {
        //            // This statement ensures that the file is created,  
        //            // but the handle is not kept.  
        //            using (FileStream fs = File.Create(pathinput)) { }
        //        }
        //        // Ensure that the target does not exist.  
        //        if (!Directory.Exists(output))
        //        {
        //            Directory.CreateDirectory(output);
        //        }
        //        // Move the file.  
        //        if (!File.Exists(output + filename))
        //        {
        //            File.Move(pathinput, output + filename);
        //        }
        //        else
        //        {
        //            File.Delete(output + filename);
        //            File.Move(pathinput, output + filename);
        //        }

        //        //log.InsertLog(pathlog, pathinput + " was moved to " + output);

        //        // See if the original exists now.  
        //        if (File.Exists(pathinput))
        //        {
        //            //log.InsertLog(pathlog, "The original file still exists, which is unexpected.");
        //        }
        //        else
        //        {
        //            //log.InsertLog(pathlog, "The original file no longer exists, which is expected.");
        //        }
        //        result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //log.InsertLog(pathlog, "Exception : " + ex.ToString());
        //    }
        //    return result;
        //}
        //#endregion

        //#region Xml sign

        //#endregion

        //#region Pdf sign

        //#endregion
    }
}
