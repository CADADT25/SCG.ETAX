using SCG.CAD.ETAX.MODEL.CustomModel;
using SCG.CAD.ETAX.UTILITY;
using SCG.CAD.ETAX.UTILITY.Controllers;
using SCG.CAD.ETAX.UTILITY.XMLGenBussiness;
using SCG.CAD.ETAX.XML.GENERATOR.BussinessLayer;
using System.Xml.Linq;

namespace SCG.CAD.ETAX.API.Services
{
    public class XMLGenerateService : DatabaseExecuteController
    {
        ConfigXmlGeneratorService configXMLGeneratorService = new ConfigXmlGeneratorService();
        TaxCodeService taxCodeService = new TaxCodeService();
        DocumentCodeService documentCodeService = new DocumentCodeService();
        ProfileCompanyService profileCompanyService = new ProfileCompanyService();
        ProfileSellerService profileSellerService = new ProfileSellerService();
        TransactionDescriptionService transactionDescriptionService = new TransactionDescriptionService();
        ProfileBranchService profileBranchService = new ProfileBranchService();
        ProductUnitService productUnitService = new ProductUnitService();
        ConfigGlobalService configGlobalService = new ConfigGlobalService();
        UtilityProfileController profileController = new UtilityProfileController();
        LogicToolHelper logicToolHelper = new LogicToolHelper();

        public Response ProcessXMLGenerate(string textfile)
        {
            Response res = new Response();
            List<ConfigXmlGenerator> configXMLGenerator = new List<ConfigXmlGenerator>();
            List<TaxCode> taxCode = new List<TaxCode>();
            List<DocumentCode> documentCode = new List<DocumentCode>();
            List<ProfileCompany> profileCompany = new List<ProfileCompany>();
            List<ProfileSeller> profileSeller = new List<ProfileSeller>();
            List<TransactionDescription> listdatatransactionDescription = new List<TransactionDescription>();
            List<TransactionDescription> datatransactionDescription = new List<TransactionDescription>();
            List<ProfileBranch> profileBranches = new List<ProfileBranch>();
            List<ProductUnit> productUnit = new List<ProductUnit>();
            List<ConfigGlobal> configGlobal = new List<ConfigGlobal>();

            int row = 56;
            string pathoutput;
            string pathlog = @"D:\log\";
            string namepathlog = "PATHLOGFILE_XMLGENERATOR";
            string nametextfilefail = "";
            string companycode = "";
            List<string> errormessage = new List<string>();
            TextFileValidate textFileValidate = new TextFileValidate();
            ConfigXmlGenerator configXML = new ConfigXmlGenerator();
            ProfileCompany companydata = new ProfileCompany();
            List<string> listbillingno = new List<string>();
            List<string> listerror = new List<string>();

            try
            {
                var filename = Path.GetFileName(textfile);

                res = GetConfigXMLGenerator();
                if (res.STATUS == false)
                {
                    return res;
                }
                configXMLGenerator = (List<ConfigXmlGenerator>)res.OUTPUT_DATA;

                res = GetTaxCode();
                if (res.STATUS == false)
                {
                    return res;
                }
                taxCode = (List<TaxCode>)res.OUTPUT_DATA;

                res = GetDocumentCode();
                if (res.STATUS == false)
                {
                    return res;
                }
                documentCode = (List<DocumentCode>)res.OUTPUT_DATA;

                res = GetProfileCompany();
                if (res.STATUS == false)
                {
                    return res;
                }
                profileCompany = (List<ProfileCompany>)res.OUTPUT_DATA;

                res = GetProfileSeller();
                if (res.STATUS == false)
                {
                    return res;
                }
                profileSeller = (List<ProfileSeller>)res.OUTPUT_DATA;

                res = GetProductUnit();
                if (res.STATUS == false)
                {
                    return res;
                }
                productUnit = (List<ProductUnit>)res.OUTPUT_DATA;

                res = GetProfileBranch();
                if (res.STATUS == false)
                {
                    return res;
                }
                profileBranches = (List<ProfileBranch>)res.OUTPUT_DATA;

                res = GetConfigGlobal();
                if (res.STATUS == false)
                {
                    return res;
                }
                configGlobal = (List<ConfigGlobal>)res.OUTPUT_DATA;

                res = ConvertToDataTable(textfile, row);
                if (res.STATUS)
                {
                    var dt = (DataTable)res.OUTPUT_DATA;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        res = ConvertDTtoClass(dt);
                        if (res.STATUS)
                        {
                            var cs = (List<TextFileSchematic>)res.OUTPUT_DATA;
                            foreach (var classtextfile in cs)
                            {
                                listbillingno.Add(classtextfile.BILLING_NO);
                                res = transactionDescriptionService.GET_BILLING(classtextfile.BILLING_NO);
                                var dataTran = ((List<TransactionDescription>)res.OUTPUT_DATA).FirstOrDefault();
                                res = logicToolHelper.CheckCancelBillingOrSentRevenueDepartment(dataTran);
                                if (!res.STATUS)
                                {
                                    companydata = profileCompany.FirstOrDefault(x => x.TaxNumber == classtextfile.SELLER_TIN);
                                    if (companydata != null)
                                    {
                                        configXML = configXMLGenerator.FirstOrDefault(x => x.ConfigXmlGeneratorCompanyCode == companydata.CompanyCode);
                                        companycode = companydata.CompanyCode;
                                    }

                                    res = InsertDataTransactionDescription(classtextfile, configXML.ConfigXmlGeneratorInputSource, documentCode, profileCompany);
                                    if (res.STATUS)
                                    {
                                        errormessage = textFileValidate.ValidateTextFile(classtextfile, profileBranches, productUnit);
                                        if (errormessage.Count > 0)
                                        {
                                            res = UpdateDataTransaction(errormessage, classtextfile.BILLING_NO, "");
                                            if (res.STATUS)
                                            {
                                                nametextfilefail = filename.Replace(".txt", "") + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt";
                                                GenTextFileFail(nametextfilefail, classtextfile, configXML.ConfigXmlGeneratorOutputPath + "\\Fail");
                                            }
                                        }
                                        else
                                        {
                                            res = ConvertClasstoXMLFormat(classtextfile, documentCode, profileCompany, profileSeller, taxCode);
                                            if (res.STATUS)
                                            {
                                                var dataxml = (CrossIndustryInvoice)res.OUTPUT_DATA;
                                                string xmlfilename = companydata.CompanyCode + classtextfile.FISCAL_YEAR + classtextfile.BILLING_NO + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xml";
                                                res = GenXMLFromTemplate(dataxml, configXML.ConfigXmlGeneratorOutputPath + "\\Success\\", xmlfilename, classtextfile.BILLING_NO, classtextfile.BILLING_DATE);
                                            }
                                        }
                                    }
                                }
                                if (!res.STATUS)
                                {
                                    listerror.Add("BillingNo : " + classtextfile.BILLING_NO + " | Error : " + res.ERROR_MESSAGE);
                                }
                            }
                        }
                    }

                }

                pathoutput = configGlobal.FirstOrDefault(x => x.ConfigGlobalName == "PATHBACKUPTEXTFILE").ConfigGlobalValue;
                pathoutput = pathoutput + "\\" + companycode + "\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "\\";
                logicToolHelper.MoveFile(textfile, filename, DateTime.Now, pathoutput);
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.Message;
            }
            res.OUTPUT_DATA = listbillingno;
            res.OUTPUT_ERROR = listerror;
            return res;
        }

        public Response ConvertToDataTable(string filePath, int numberOfColumns)
        {
            Response res = new Response();
            res.STATUS = false;
            DataTable tbl = new DataTable();
            try
            {

                for (int col = 0; col < numberOfColumns + 1; col++)
                    tbl.Columns.Add(new DataColumn("Column" + (col + 1).ToString()));

                string[] lines = System.IO.File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        var separator = @"\|";
                        var replateseparator = line.Replace(separator, "|");
                        var cols = replateseparator.Split('|');
                        DataRow dr = tbl.NewRow();
                        for (int cIndex = 0; cIndex < cols.Count(); cIndex++)
                        {
                            dr[cIndex] = cols[cIndex];
                        }
                        dr[numberOfColumns] = filePath;

                        tbl.Rows.Add(dr);
                    }
                }
                res.OUTPUT_DATA = tbl;
                res.STATUS = true;
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.Message;
            }

            return res;
        }

        public Response ConvertDTtoClass(DataTable dt)
        {
            Response res = new Response();
            res.STATUS = false;
            List<TextFileSchematic> result = new List<TextFileSchematic>();
            try
            {
                TextFileSchematic row = new TextFileSchematic();
                TextFileItemSchematic item;
                string headerflag;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    headerflag = dt.Rows[i][0].ToString();
                    if (headerflag.Equals("H", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (i != 0)
                        {
                            result.Add(row);
                        }
                        row = new TextFileSchematic();
                        row.Item = new List<TextFileItemSchematic>();
                        row.HEADER_FLAG = dt.Rows[i][0].ToString() ?? "";
                        row.FI_DOC_TYPE = dt.Rows[i][1].ToString() ?? "";
                        row.FISCAL_YEAR = dt.Rows[i][2].ToString() ?? "";
                        row.IC_FLAG = dt.Rows[i][3].ToString() ?? "";
                        row.SELLER_TIN = dt.Rows[i][4].ToString() ?? "";
                        row.SELLER_BRANCH = dt.Rows[i][5].ToString() ?? "";
                        row.BUYER_TYPE_CODE = dt.Rows[i][6].ToString() ?? "";
                        row.BUYER_TAXID = dt.Rows[i][7].ToString() ?? "";
                        row.BUYER_BRANCH = dt.Rows[i][8].ToString() ?? "";
                        row.BUYER_NAME = dt.Rows[i][9].ToString() ?? "";
                        row.BUYER_CODE = dt.Rows[i][10].ToString() ?? "";
                        row.BUYER_PSTLZ = dt.Rows[i][11].ToString() ?? "";
                        row.BUYER_LAND1 = dt.Rows[i][12].ToString() ?? "";
                        row.SELLER_NAME = dt.Rows[i][13].ToString() ?? "";
                        row.SELLER_BUKRS = dt.Rows[i][14].ToString() ?? "";
                        row.SELLER_MAIL = dt.Rows[i][15].ToString() ?? "";
                        row.SELLER_PSTLZ = dt.Rows[i][16].ToString() ?? "";
                        row.SELLER_BUILD_NAME = dt.Rows[i][17].ToString() ?? "";
                        row.SELLER_STREET = dt.Rows[i][18].ToString() ?? "";
                        row.SELLER_CITY = dt.Rows[i][19].ToString() ?? "";
                        row.SELLER_SUBDIV = dt.Rows[i][20].ToString() ?? "";
                        row.SELLER_LAND1 = dt.Rows[i][21].ToString() ?? "";
                        row.SELLER_PROVINCE = dt.Rows[i][22].ToString() ?? "";
                        row.SELLER_BUILD_NO = dt.Rows[i][23].ToString() ?? "";
                        row.BUYER_ADDRESS1 = dt.Rows[i][24].ToString() ?? "";
                        row.BUYER_ADDRESS2 = dt.Rows[i][25].ToString() ?? "";
                        row.BILLING_NO = dt.Rows[i][26].ToString() ?? "";
                        row.FI_DOC = dt.Rows[i][27].ToString() ?? "";
                        row.DOC_CURRENCY = dt.Rows[i][28].ToString() ?? "";
                        row.SALES_AMOUNT = dt.Rows[i][29].ToString() ?? "";
                        row.TAX_CODE = dt.Rows[i][30].ToString() ?? "";
                        row.TAX_RATE = dt.Rows[i][31].ToString() ?? "";
                        row.TAX_AMOUNT = dt.Rows[i][32].ToString() ?? "";
                        row.CHARGE_AMOUNT = dt.Rows[i][33].ToString() ?? "";
                        row.ALLOWANCE_AMOUNT = dt.Rows[i][34].ToString() ?? "";
                        row.ORIGINAL_AMOUNT = dt.Rows[i][35].ToString() ?? "";
                        row.DIFFERENCE_AMOUNT = dt.Rows[i][36].ToString() ?? "";
                        row.TAX_BASIS_AMOUNT = dt.Rows[i][37].ToString() ?? "";
                        row.TAX_TOTAL_AMOUNT = dt.Rows[i][38].ToString() ?? "";
                        row.GRAND_TOTAL_AMOUNT = dt.Rows[i][39].ToString() ?? "";
                        row.CREATE_DATE_TIME = dt.Rows[i][40].ToString() ?? "";
                        row.REF_DOC = dt.Rows[i][41].ToString() ?? "";
                        row.REF_DOC_DATE = dt.Rows[i][42].ToString() ?? "";
                        row.REF_FI_DOCTYPE = dt.Rows[i][43].ToString() ?? "";
                        row.ORDER_REASON = dt.Rows[i][44].ToString() ?? "";
                        row.ORDER_REASON_CODE = dt.Rows[i][45].ToString() ?? "";
                        row.BILLING_DATE = dt.Rows[i][46].ToString() ?? "";
                        row.INSTANCE = dt.Rows[i][47].ToString() ?? "";
                        row.BILLING_TYPE = dt.Rows[i][48].ToString() ?? "";
                        row.SALES_ORG = dt.Rows[i][49].ToString() ?? "";
                        row.PRINT_DATE_TIME = dt.Rows[i][50].ToString() ?? "";
                        row.Number_Sold_to = dt.Rows[i][51].ToString() ?? "";
                        row.Number_Ship_to = dt.Rows[i][52].ToString() ?? "";
                        row.Number_Bill_to = dt.Rows[i][53].ToString() ?? "";
                        row.Number_Payer = dt.Rows[i][54].ToString() ?? "";
                        row.CORRECT_AMOUNT = dt.Rows[i][55].ToString() ?? "";
                        row.pathfile = dt.Rows[i][56].ToString() ?? "";
                    }
                    else
                    {
                        var data = dt.Rows[i];
                        item = ConvertItemtoClass(data);
                        row.Item.Add(item);
                    }

                }
                // lastloop
                result.Add(row);
                res.OUTPUT_DATA = result;
                res.STATUS = true;
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.Message;
            }
            return res;
        }

        public TextFileItemSchematic ConvertItemtoClass(DataRow dr)
        {
            TextFileItemSchematic result = new TextFileItemSchematic();
            try
            {
                result.ITEM_FLAG = dr[0].ToString();
                result.FI_DOC_TYPE = dr[1].ToString();
                result.SELLER_TIN = dr[2].ToString();
                result.SELLER_BRANCH = dr[3].ToString();
                result.BILLING_NO = dr[4].ToString();
                result.ITEM_NO = dr[5].ToString();
                result.PRODUCT_NAME = dr[6].ToString();
                result.QUANTITY = dr[7].ToString();
                result.SALES_UNT = dr[8].ToString();
                result.UNIT_NAME = dr[9].ToString();
                result.CURRENCY = dr[10].ToString();
                result.UNIT_PRICT = dr[11].ToString();
                result.NETUNIT_PRICE = dr[12].ToString();
                result.TAX_AMOUNT = dr[13].ToString();
                result.CHARGE_AMOUNT = dr[14].ToString();
                result.ALLOWANCE_AMOUNT = dr[15].ToString();
                result.TAX_BASIS_AMOUNT = dr[16].ToString();
                result.TAX_TOTAL_AMOUNT = dr[17].ToString();
                result.GRAND_TOTAL_AMOUNT = dr[18].ToString();
                result.PO_NUMBER = dr[19].ToString();
                result.BILLING_DATE = dr[20].ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Response GetConfigXMLGenerator()
        {
            Response res = new Response();
            try
            {
                res = configXMLGeneratorService.GET_LIST();
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.ToString();
            }
            return res;
        }
        public Response GetTaxCode()
        {
            Response res = new Response();
            try
            {
                res = taxCodeService.GET_LIST();
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.ToString();
            }
            return res;
        }
        public Response GetDocumentCode()
        {
            Response res = new Response();
            try
            {
                res = documentCodeService.GET_LIST();
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.ToString();
            }
            return res;
        }
        public Response GetProfileCompany()
        {
            Response res = new Response();
            try
            {
                res = profileCompanyService.GET_LIST();
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.ToString();
            }
            return res;
        }
        public Response GetProfileSeller()
        {
            Response res = new Response();
            try
            {
                res = profileSellerService.GET_LIST();
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.ToString();
            }
            return res;
        }
        public Response GetProductUnit()
        {
            Response res = new Response();
            try
            {
                res = productUnitService.GET_LIST();
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.ToString();
            }
            return res;
        }
        public Response GetProfileBranch()
        {
            Response res = new Response();
            try
            {
                res = profileBranchService.GET_LIST();
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.ToString();
            }
            return res;
        }
        public Response GetConfigGlobal()
        {
            Response res = new Response();
            try
            {
                res = configGlobalService.GET_LIST();
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.ToString();
            }
            return res;
        }

        public Response InsertDataTransactionDescription(TextFileSchematic dataxml, string source, List<DocumentCode> documentCode, List<ProfileCompany> profileCompany)
        {
            Response res = new Response();
            TransactionDescription data = new TransactionDescription();
            List<TransactionDescription> listdatatransactionDescription = new List<TransactionDescription>();
            bool insert = false;
            double ic = 0;
            string imageDocType = "";
            string doctype = "";
            DateTime billingdate = DateTime.Now;
            string poNumber = "";
            try
            {
                res = transactionDescriptionService.GET_BILLING(dataxml.BILLING_NO);
                data = ((List<TransactionDescription>)res.OUTPUT_DATA).FirstOrDefault();

                ProfileCompany profiledetail = new ProfileCompany();
                DocumentCode doccode = new DocumentCode();

                doccode = documentCode.FirstOrDefault(x => x.DocumentCodeErp == dataxml.FI_DOC_TYPE);
                profiledetail = profileCompany.FirstOrDefault(x => x.TaxNumber == dataxml.SELLER_TIN);
                if (data == null)
                {
                    insert = true;
                    data = new TransactionDescription();
                }

                if (!String.IsNullOrEmpty(dataxml.BILLING_DATE))
                {
                    billingdate = Convert.ToDateTime(dataxml.BILLING_DATE);
                    data.BillingDate = billingdate;
                    data.BillingYear = billingdate.Year;
                }
                if (!String.IsNullOrEmpty(dataxml.BILLING_NO))
                {
                    data.BillingNumber = dataxml.BILLING_NO.ToString();
                }
                if (!String.IsNullOrEmpty(dataxml.Number_Bill_to))
                {
                    data.BillTo = dataxml.Number_Bill_to.ToString();
                }

                if (profiledetail != null)
                {
                    data.CompanyCode = profiledetail.CompanyCode;
                    data.CompanyName = profiledetail.CompanyNameTh;
                }
                data.CreateBy = "Batch";
                data.CreateDate = DateTime.Now;
                data.CustomerId = dataxml.BUYER_CODE;
                data.CustomerName = dataxml.BUYER_NAME;
                if (doccode != null)
                {
                    doctype = doccode.DocumentCodeRd;
                    data.DocType = doccode.DocumentCodeRd;
                }

                if (!String.IsNullOrEmpty(dataxml.FI_DOC))
                {
                    data.FiDoc = dataxml.FI_DOC.ToString();
                }
                data.Foc = (dataxml.FI_DOC_TYPE == "FOC") ? 1 : 0;
                if (!string.IsNullOrEmpty(doctype))
                {
                    ic = 1;
                    switch (doctype.ToUpper())
                    {
                        case "380":
                        case "T02":
                        case "T03":
                        case "T04":
                        case "T05":
                        case "T06":
                            imageDocType = "ZFI1060,ZFI1061";
                            break;
                        case "388":
                            imageDocType = "ZFI1060,ZFI1061";
                            break;
                        case "80":
                            imageDocType = "ZFI1040,ZFI1041";
                            break;
                        case "81":
                            imageDocType = "ZFI1050,ZFI1051";
                            break;
                        case "T01":
                        case "T07":
                        default:
                            break;
                    }
                }
                else
                {
                    switch (doctype.ToUpper())
                    {
                        case "380":
                        case "T02":
                        case "T03":
                        case "T04":
                        case "T05":
                        case "T06":
                            imageDocType = "ZFI1061";
                            break;
                        case "388":
                            imageDocType = "ZFI1061";
                            break;
                        case "80":
                            imageDocType = "ZFI1041";
                            break;
                        case "81":
                            imageDocType = "ZFI1051";
                            break;
                        case "T01":
                        case "T07":
                        default:
                            break;
                    }
                }
                data.Ic = ic;
                data.ImageDocType = imageDocType;// mapping
                data.Isactive = 1;
                if (!String.IsNullOrEmpty(dataxml.Number_Payer))
                {
                    data.Payer = dataxml.Number_Payer.ToString();
                }
                data.PostingYear = billingdate.Year.ToString();
                data.ProcessingDate = DateTime.Now;
                foreach (var item in dataxml.Item)
                {
                    poNumber = poNumber + item.PO_NUMBER + ",";
                }
                poNumber = poNumber.Substring(0, poNumber.Length - 1);
                data.PoNumber = poNumber;
                data.SellOrg = dataxml.SALES_ORG;
                if (!String.IsNullOrEmpty(dataxml.Number_Ship_to))
                {
                    data.ShipTo = dataxml.Number_Ship_to.ToString();
                }
                if (!String.IsNullOrEmpty(dataxml.Number_Sold_to))
                {
                    data.SoldTo = dataxml.Number_Sold_to.ToString();
                }
                data.SourceName = source;
                data.TypeInput = "Batch";
                data.UpdateBy = "Batch";
                data.UpdateDate = DateTime.Now;
                data.EmailSendStatus = "Waiting";
                data.PdfIndexingStatus = "Waiting";
                data.PrintStatus = "Waiting";
                data.XmlCompressStatus = "Waiting";
                data.XmlSignStatus = "Waiting";
                data.EmailSendStatus = "Waiting";
                data.DmsAttachmentFileName = null;
                data.DmsAttachmentFilePath = null;

                if (insert == true)
                {
                    res = transactionDescriptionService.INSERT(data);
                }
                else
                {
                    res = transactionDescriptionService.UPDATE(data);
                }
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.ToString();
            }
            return res;
        }

        public Response UpdateDataTransaction(List<string> errormessage, string billingNo, string pathfile)
        {
            Response res = new Response();
            res.STATUS = false;
            string errorText = "";
            TransactionDescription dataTran = new TransactionDescription();
            List<TransactionDescription> listdatatransactionDescription = new List<TransactionDescription>();
            try
            {
                res = transactionDescriptionService.GET_BILLING(billingNo);
                listdatatransactionDescription = (List<TransactionDescription>)res.OUTPUT_DATA;

                dataTran = listdatatransactionDescription.FirstOrDefault();
                if (dataTran != null)
                {
                    if (errormessage.Count == 0)
                    {
                        dataTran.GenerateDateTime = DateTime.Now;
                        dataTran.GenerateDetail = "XML was generated completely";
                        dataTran.GenerateStatus = "Successful";
                        dataTran.XmlBeforeSignLocation = pathfile;
                        dataTran.UpdateBy = "Batch";
                        dataTran.UpdateDate = DateTime.Now;

                        res = transactionDescriptionService.UPDATE(dataTran);
                    }
                    else
                    {
                        dataTran.GenerateDateTime = DateTime.Now;
                        foreach (var error in errormessage)
                        {
                            errorText = errorText + error + "|";
                        }
                        errorText = errorText.Substring(0, errorText.Length - 1);
                        dataTran.GenerateDetail = errorText;
                        dataTran.GenerateStatus = "Failed";
                        dataTran.UpdateBy = "Batch";
                        dataTran.UpdateDate = DateTime.Now;

                        res = transactionDescriptionService.UPDATE(dataTran);
                    }
                }
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.Message;
            }
            return res;
        }

        public Response GenTextFileFail(string filename, TextFileSchematic dataxml, string path)
        {
            Response res = new Response();
            res.STATUS = false;
            string filePath = path + filename;
            string lineheader = "";
            List<string> lineitem = new List<string>();
            string line = "";
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                lineheader += dataxml.HEADER_FLAG + "\\|";
                lineheader += dataxml.FI_DOC_TYPE + "\\|";
                lineheader += dataxml.FISCAL_YEAR + "\\|";
                lineheader += dataxml.IC_FLAG + "\\|";
                lineheader += dataxml.SELLER_TIN + "\\|";
                lineheader += dataxml.SELLER_BRANCH + "\\|";
                lineheader += dataxml.BUYER_TYPE_CODE + "\\|";
                lineheader += dataxml.BUYER_TAXID + "\\|";
                lineheader += dataxml.BUYER_BRANCH + "\\|";
                lineheader += dataxml.BUYER_NAME + "\\|";
                lineheader += dataxml.BUYER_CODE + "\\|";
                lineheader += dataxml.BUYER_PSTLZ + "\\|";
                lineheader += dataxml.BUYER_LAND1 + "\\|";
                lineheader += dataxml.SELLER_NAME + "\\|";
                lineheader += dataxml.SELLER_BUKRS + "\\|";
                lineheader += dataxml.SELLER_MAIL + "\\|";
                lineheader += dataxml.SELLER_PSTLZ + "\\|";
                lineheader += dataxml.SELLER_BUILD_NAME + "\\|";
                lineheader += dataxml.SELLER_STREET + "\\|";
                lineheader += dataxml.SELLER_CITY + "\\|";
                lineheader += dataxml.SELLER_SUBDIV + "\\|";
                lineheader += dataxml.SELLER_LAND1 + "\\|";
                lineheader += dataxml.SELLER_PROVINCE + "\\|";
                lineheader += dataxml.SELLER_BUILD_NO + "\\|";
                lineheader += dataxml.BUYER_ADDRESS1 + "\\|";
                lineheader += dataxml.BUYER_ADDRESS2 + "\\|";
                lineheader += dataxml.BILLING_NO + "\\|";
                lineheader += dataxml.FI_DOC + "\\|";
                lineheader += dataxml.DOC_CURRENCY + "\\|";
                lineheader += dataxml.SALES_AMOUNT + "\\|";
                lineheader += dataxml.TAX_CODE + "\\|";
                lineheader += dataxml.TAX_RATE + "\\|";
                lineheader += dataxml.TAX_AMOUNT + "\\|";
                lineheader += dataxml.CHARGE_AMOUNT + "\\|";
                lineheader += dataxml.ALLOWANCE_AMOUNT + "\\|";
                lineheader += dataxml.ORIGINAL_AMOUNT + "\\|";
                lineheader += dataxml.DIFFERENCE_AMOUNT + "\\|";
                lineheader += dataxml.TAX_BASIS_AMOUNT + "\\|";
                lineheader += dataxml.TAX_TOTAL_AMOUNT + "\\|";
                lineheader += dataxml.GRAND_TOTAL_AMOUNT + "\\|";
                lineheader += dataxml.CREATE_DATE_TIME + "\\|";
                lineheader += dataxml.REF_DOC + "\\|";
                lineheader += dataxml.REF_DOC_DATE + "\\|";
                lineheader += dataxml.REF_FI_DOCTYPE + "\\|";
                lineheader += dataxml.ORDER_REASON + "\\|";
                lineheader += dataxml.ORDER_REASON_CODE + "\\|";
                lineheader += dataxml.BILLING_DATE + "\\|";
                lineheader += dataxml.INSTANCE + "\\|";
                lineheader += dataxml.BILLING_TYPE + "\\|";
                lineheader += dataxml.SALES_ORG + "\\|";
                lineheader += dataxml.PRINT_DATE_TIME + "\\|";
                lineheader += dataxml.Number_Sold_to + "\\|";
                lineheader += dataxml.Number_Ship_to + "\\|";
                lineheader += dataxml.Number_Bill_to + "\\|";
                lineheader += dataxml.Number_Payer + "\\|";
                lineheader += dataxml.CORRECT_AMOUNT;

                foreach (var item in dataxml.Item)
                {
                    line += item.ITEM_FLAG + "\\|";
                    line += item.FI_DOC_TYPE + "\\|";
                    line += item.SELLER_TIN + "\\|";
                    line += item.SELLER_BRANCH + "\\|";
                    line += item.BILLING_NO + "\\|";
                    line += item.ITEM_NO + "\\|";
                    line += item.PRODUCT_NAME + "\\|";
                    line += item.QUANTITY + "\\|";
                    line += item.SALES_UNT + "\\|";
                    line += item.UNIT_NAME + "\\|";
                    line += item.CURRENCY + "\\|";
                    line += item.UNIT_PRICT + "\\|";
                    line += item.NETUNIT_PRICE + "\\|";
                    line += item.TAX_AMOUNT + "\\|";
                    line += item.CHARGE_AMOUNT + "\\|";
                    line += item.ALLOWANCE_AMOUNT + "\\|";
                    line += item.TAX_BASIS_AMOUNT + "\\|";
                    line += item.TAX_TOTAL_AMOUNT + "\\|";
                    line += item.GRAND_TOTAL_AMOUNT + "\\|";
                    line += item.PO_NUMBER + "\\|";
                    line += item.BILLING_DATE;
                    lineitem.Add(line);
                }

                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    StreamWriter write = new StreamWriter(fs);
                    write.BaseStream.Seek(0, SeekOrigin.End);
                    write.WriteLine(lineheader);
                    foreach (var item in lineitem)
                    {
                        write.WriteLine(item);
                    }
                    write.Flush();
                    write.Close();
                    fs.Close();
                    res.STATUS = true;
                }
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.Message;
            }
            return res;
        }

        public Response ConvertClasstoXMLFormat(TextFileSchematic data, List<DocumentCode> documentCode, List<ProfileCompany> profileCompany, List<ProfileSeller> profileSeller, List<TaxCode> taxCode)
        {
            Response res = new Response();
            res.STATUS = false;
            try
            {
                CrossIndustryInvoice xmldata;
                ExchangedDocument exchangedDocument;
                SupplyChainTradeTransaction supplyChainTradeTransaction;
                IncludedSupplyChainTradeLineItem includedSupplyChainTradeLineItem;
                ProfileSeller profile = new ProfileSeller();
                ProfileCompany profiledetail = new ProfileCompany();
                DocumentCode doccode = new DocumentCode();
                string schemeID = "OTHR";
                xmldata = new CrossIndustryInvoice();
                supplyChainTradeTransaction = new SupplyChainTradeTransaction();
                doccode = documentCode.FirstOrDefault(x => x.DocumentCodeErp == data.FI_DOC_TYPE);
                profiledetail = profileCompany.FirstOrDefault(x => x.TaxNumber == data.SELLER_TIN);

                xmldata.exchangedDocumentContext = new ExchangedDocumentContext();
                xmldata.exchangedDocumentContext.guidelineSpecifiedDocumentContextParameter = new GuidelineSpecifiedDocumentContextParameter();
                xmldata.exchangedDocumentContext.guidelineSpecifiedDocumentContextParameter.schemeAgencyID = "ETDA";
                xmldata.exchangedDocumentContext.guidelineSpecifiedDocumentContextParameter.id = "ER3-2560";
                xmldata.exchangedDocumentContext.guidelineSpecifiedDocumentContextParameter.schemeVersionID = "v2.0";

                string doccodetext = doccode.DocumentDescription ?? "";
                if (doccodetext.IndexOf('(') > 0)
                {
                    doccodetext = doccodetext.Substring(0, doccodetext.IndexOf('(') - 1);
                }

                xmldata.exchangedDocument = new ExchangedDocument();
                xmldata.exchangedDocument.id = data.BILLING_NO ?? "";
                xmldata.exchangedDocument.name = doccodetext;
                xmldata.exchangedDocument.typeCode = doccode.DocumentCodeRd ?? "";
                xmldata.exchangedDocument.issueDateTime = data.BILLING_DATE ?? "";
                xmldata.exchangedDocument.createionDateTime = data.CREATE_DATE_TIME ?? "";

                profile = profileController.ProfileAddress(profileSeller, profiledetail.CompanyCode, data.SELLER_BRANCH);
                supplyChainTradeTransaction.applicableHeaderTradeAgreement = new ApplicableHeaderTradeAgreement();
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty = new SellerTradeParty();
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.name = data.SELLER_NAME ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration = new SpecifiedTaxRegistration();
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id = (data.SELLER_TIN ?? "") + (data.SELLER_BRANCH ?? "00000");
                if ((!logicToolHelper.CheckDataRule(data.BUYER_TAXID, "") && !logicToolHelper.CheckDataRule(data.BUYER_BRANCH, "")) && (data.BUYER_TAXID.Length == 13 && data.BUYER_BRANCH.Length == 5))
                {
                    schemeID = "TXID";
                }
                else if ((!logicToolHelper.CheckDataRule(data.BUYER_TAXID, "")) && (data.BUYER_TAXID.Length == 13 && logicToolHelper.CheckDataRule(data.BUYER_BRANCH, "")))
                {
                    schemeID = "NIDN";
                }
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeID = schemeID ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeagencyid = "RD";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress = new PostalTradeAddress();
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.postcodeCode = data.SELLER_PSTLZ ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.streetName = profile.Road ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.cityName = profile.District ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.citySubDivisionName = profile.SubDistrict ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.countryID = data.SELLER_LAND1 ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.countrySubDivisionID = profile.Province ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.buildingNumber = profile.Addressnumber ?? "";

                supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty = new BuyerTradeParty();
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.name = data.SELLER_NAME ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration = new SpecifiedTaxRegistration();
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id = (data.BUYER_TAXID ?? "") + (data.BUYER_BRANCH ?? "00000");
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeagencyid = "RD";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeID = "TXID";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress = new PostalTradeAddress();
                if (data.BUYER_LAND1.ToUpper() != "TH" && String.IsNullOrEmpty(data.BUYER_PSTLZ))
                {
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.postcodeCode = "0";
                }
                else
                {
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.postcodeCode = data.BUYER_PSTLZ ?? "";
                }
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.line1 = data.BUYER_ADDRESS1 ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.countryID = data.BUYER_LAND1 ?? "";

                supplyChainTradeTransaction.applicableHeaderTradeAgreement.additionalReferencedDocument = new AdditionalReferencedDocument();
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.additionalReferencedDocument.referenceTypeCode = "";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.additionalReferencedDocument.issueDateTime = "";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.additionalReferencedDocument.issuerAssignedID = "";

                supplyChainTradeTransaction.applicableHeaderTradeSettlement = new ApplicableHeaderTradeSettlement();
                supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoiceCurrencyCode = new InvoiceCurrencyCode();
                supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoiceCurrencyCode.invoiceCurrencyCode = data.DOC_CURRENCY ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoiceCurrencyCode.listID = "ISO 4217 3A";
                supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax = new ApplicableTradeTax();
                supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.typeCode = taxCode.FirstOrDefault(x => x.TaxCodeErp == data.TAX_CODE).TaxCodeRd ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.calculatedRate = data.TAX_RATE ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.basisAmount = data.SALES_AMOUNT ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.calculatedAmount = data.TAX_AMOUNT ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation = new SpecifiedTradeSettlementHeaderMonetarySummation();
                supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.lineTotalAmount = data.SALES_AMOUNT ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.allowanceTotalAmount = data.ALLOWANCE_AMOUNT ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.taxBasisTotalAmount = data.TAX_BASIS_AMOUNT ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.taxTotalAmount = data.TAX_TOTAL_AMOUNT ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.grandTotalAmount = data.GRAND_TOTAL_AMOUNT ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.chargeTotalAmount = data.CHARGE_AMOUNT ?? "";

                supplyChainTradeTransaction.includedSupplyChainTradeLineItem = new List<IncludedSupplyChainTradeLineItem>();
                for (int i = 0; i < data.Item.Count; i++)
                {
                    includedSupplyChainTradeLineItem = new IncludedSupplyChainTradeLineItem();
                    includedSupplyChainTradeLineItem.associatedDocumentLineDocument = new AssociatedDocumentLineDocument();
                    includedSupplyChainTradeLineItem.associatedDocumentLineDocument.lineID = (i + 1).ToString();
                    includedSupplyChainTradeLineItem.specifiedTradeProduct = new SpecifiedTradeProduct();
                    includedSupplyChainTradeLineItem.specifiedTradeProduct.name = data.Item[i].PRODUCT_NAME ?? "";
                    includedSupplyChainTradeLineItem.specifiedTradeProduct.informationNote = new InformationNote();
                    includedSupplyChainTradeLineItem.specifiedTradeProduct.informationNote.subject = data.Item[i].PO_NUMBER ?? "test";
                    if (string.IsNullOrEmpty(data.Item[i].PO_NUMBER))
                    {
                        includedSupplyChainTradeLineItem.specifiedTradeProduct.informationNote.subject = "test";
                    }
                    includedSupplyChainTradeLineItem.specifiedLineTradeAgreement = new SpecifiedLineTradeAgreement();
                    includedSupplyChainTradeLineItem.specifiedLineTradeAgreement.grossPriceProductTradePrice = new GrossPriceProductTradePrice();
                    includedSupplyChainTradeLineItem.specifiedLineTradeAgreement.grossPriceProductTradePrice.chargeAmount = data.Item[i].CHARGE_AMOUNT ?? "";
                    includedSupplyChainTradeLineItem.specifiedLineTradeDelivery = new SpecifiedLineTradeDelivery();
                    includedSupplyChainTradeLineItem.specifiedLineTradeDelivery.billedQuantity = new BilledQuantity();
                    includedSupplyChainTradeLineItem.specifiedLineTradeDelivery.billedQuantity.unitCode = data.Item[i].SALES_UNT ?? "";
                    includedSupplyChainTradeLineItem.specifiedLineTradeDelivery.billedQuantity.billedQuantity = data.Item[i].QUANTITY ?? "";
                    includedSupplyChainTradeLineItem.specifiedLineTradeSettlement = new SpecifiedLineTradeSettlement();
                    includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge = new SpecifiedTradeAllowanceCharge();
                    if (Convert.ToDecimal(data.ALLOWANCE_AMOUNT) > 0)
                    {
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge.chargeIndicator = "true";
                    }
                    else
                    {
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge.chargeIndicator = "false";
                    }
                    includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge.actualAmount = data.ALLOWANCE_AMOUNT ?? "";
                    includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation = new SpecifiedTradeSettlementLineMonetarySummation();
                    includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.taxTotalAmount = data.TAX_AMOUNT ?? "";
                    includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.NetLineTotalAmount = data.TAX_BASIS_AMOUNT ?? "";
                    includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.netIncludingTaxesLineTotalAmount = new NetIncludingTaxesLineTotalAmount();
                    includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.netIncludingTaxesLineTotalAmount.currencyID = data.Item[i].CURRENCY ?? "";//format
                    includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.netIncludingTaxesLineTotalAmount.netIncludingTaxesLineTotalAmount = data.Item[i].GRAND_TOTAL_AMOUNT ?? "";//format
                    supplyChainTradeTransaction.includedSupplyChainTradeLineItem.Add(includedSupplyChainTradeLineItem);
                }

                xmldata.supplyChainTradeTransaction = supplyChainTradeTransaction;
                res.OUTPUT_DATA = xmldata;
                res.STATUS = true;
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.Message;
            }
            return res;
        }

        public Response GenXMLFromTemplate(CrossIndustryInvoice data, string pathoutbound, string filename, string billingno, string billingdate)
        {
            Response res = new Response();
            res.STATUS = false;
            //pathoutbound = @"D:\gen\gen.xml";
            XDocument xml = new XDocument();
            List<string> errormessage = new List<string>();
            try
            {
                DateTime billdate = Convert.ToDateTime(billingdate);
                //pathoutbound += "\\" + billdate.ToString("yyyy") + "\\" + billdate.ToString("MM") + "\\";
                if (!Directory.Exists(pathoutbound))
                {
                    Directory.CreateDirectory(pathoutbound);
                }
                switch (data.exchangedDocument.typeCode)
                {
                    case "388":
                    case "T02":
                    case "T03":
                    case "T04":
                        var taxinvoice = new Template_TaxInvoice();
                        xml = taxinvoice.XMLtemplate(data);
                        res = GenXMLFile(xml, pathoutbound + filename);
                        break;
                    case "80":
                    case "81":
                        var debitcreditnote = new Template_DebitCreditNote();
                        xml = debitcreditnote.XMLtemplate(data);
                        res = GenXMLFile(xml, pathoutbound + filename);
                        break;
                    case "T01":
                    default:
                        break;
                }
                if (res.STATUS)
                {
                    res = UpdateDataTransaction(errormessage, billingno, pathoutbound + filename);
                }

            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.Message;
            }
            return res;
        }

        public Response GenXMLFile(XDocument xml, string fullpath)
        {
            Response res = new Response();
            res.STATUS = false;
            try
            {

                TextWriter filestream = new StreamWriter(fullpath);
                //TextWriter filestream = new StreamWriter(@"C:\Code_Dev\output.xml");
                xml.Save(filestream);
                filestream.Close();
                res.STATUS = true;
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.Message;
            }
            return res;
        }

    }
}
