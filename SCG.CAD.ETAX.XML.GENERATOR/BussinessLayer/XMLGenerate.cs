

using System.Data;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.XML.GENERATOR.Controller;
using SCG.CAD.ETAX.XML.GENERATOR.Models;

namespace SCG.CAD.ETAX.XML.GENERATOR.BussinessLayer
{
    public class XMLGenerate
    {
        ConfigXMLGeneratorController configXMLGeneratorController = new ConfigXMLGeneratorController();
        TaxCodeController taxCodeController = new TaxCodeController();
        DocumentCodeController documentCodeController = new DocumentCodeController();
        //ErpDocumentController erpDocumentController = new ErpDocumentController();
        //RdDocumentController rdDocumentController = new RdDocumentController();
        ProfileController profileController = new ProfileController();
        TransactionDescriptionController transactionDescription = new TransactionDescriptionController();
        ProfileBranchController profileBranchController = new ProfileBranchController();
        ProductUnitController productUnitController = new ProductUnitController();


        List<ConfigXmlGenerator> configXMLGenerator = new List<ConfigXmlGenerator>();
        List<TaxCode> taxCode = new List<TaxCode>();
        List<DocumentCode> documentCode = new List<DocumentCode>();
        //List<ErpDocument> erpDocument = new List<ErpDocument>();
        //List<RdDocument> rdDocument = new List<RdDocument>();
        List<ProfileCompany> profileCompany = new List<ProfileCompany>();
        List<ProfileSeller> profileSeller = new List<ProfileSeller>();
        List<TransactionDescription> listdatatransactionDescription = new List<TransactionDescription>();
        List<ProfileBranch> profileBranches = new List<ProfileBranch>();
        List<ProductUnit> productUnit = new List<ProductUnit>();

        public void ReadXMLFile()
        {
            try
            {
                int round = 0;
                int row = 56;
                GetDataFromDataBase();
                var allTextFile = ReadTextFile();
                List<string> errormessage = new List<string>();
                TextFileValidate textFileValidate = new TextFileValidate();
                string nametextfilefail = "";
                foreach (var textfile in allTextFile)
                {
                    var filename = Path.GetFileName(textfile);
                    Console.WriteLine("Start Read TextFile : " + textfile);
                    var dt = ConvertToDataTable(textfile, row);
                    Console.WriteLine("ConvertToDataTable success");
                    var cs = ConvertDTtoClass(dt);
                    Console.WriteLine("ConvertToClass success");
                    round = 1;
                    foreach (var classtextfile in cs)
                    {

                        var companydata = profileCompany.FirstOrDefault(x => x.TaxNumber == classtextfile.SELLER_TIN);
                        var configXML = configXMLGenerator.FirstOrDefault(x => x.ConfigXmlGeneratorCompanyCode == companydata.CompanyCode);
                        Console.WriteLine("Start round : " + round);

                        Console.WriteLine("Start Insert Data TransactionDescription");
                        InsertDataTransactionDescription(classtextfile, configXML.ConfigXmlGeneratorInputSource);
                        Console.WriteLine("End Insert Data TransactionDescription");

                        Console.WriteLine("Start ValidateFileText");
                        errormessage = textFileValidate.ValidateTextFile(classtextfile, profileBranches, productUnit);
                        if (errormessage.Count > 0)
                        {
                            Console.WriteLine("ValidateFileText Fail");
                            UpdateDataTransaction(errormessage, classtextfile.BILLING_NO);
                            nametextfilefail = filename.Replace(".txt","") + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt";
                            GenTextFileFail(nametextfilefail, classtextfile, configXML.ConfigXmlGeneratorOutputPath + "\\Fail");
                        }
                        else
                        {
                            Console.WriteLine("ValidateFileText Success");
                            errormessage = new List<string>();

                            var dataxml = ConvertClasstoXMLFormat(classtextfile);
                            Console.WriteLine("ConvertClasstoXMLFormat success");

                            Console.WriteLine("Start ValidateSchematron");
                            errormessage.AddRange(ValidateSchematron(dataxml));
                            Console.WriteLine("End ValidateSchematron");

                            if (errormessage.Count > 0)
                            {
                                Console.WriteLine("ValidateSchematron Fail");
                                UpdateDataTransaction(errormessage, classtextfile.BILLING_NO);
                                nametextfilefail = filename.Replace(".txt", "") + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt";
                                GenTextFileFail(nametextfilefail, classtextfile, configXML.ConfigXmlGeneratorOutputPath + "\\Fail\\");
                            }
                            else
                            {
                                Console.WriteLine("Start Gen XML File");
                                string xmlfilename = companydata.CompanyCode + classtextfile.FISCAL_YEAR + classtextfile.BILLING_NO + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xml";
                                var xml = GenXMLFromTemplate(dataxml, configXML.ConfigXmlGeneratorOutputPath + "\\Success\\", xmlfilename, classtextfile.BILLING_NO);

                                Console.WriteLine("End Gen XML File");
                            }

                        }
                        round += 1;
                    }
                    Console.WriteLine("End Read TextFile : " + textfile);

                    Console.WriteLine("Start Move File");
                    MoveFile(textfile, "", filename);
                    Console.WriteLine("End Move File");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void XMLSchematronToList()
        {
            try
            {
                var path = @"C:\Code_Dev\SCG.CAD.ETAX\SCG.CAD.ETAX.XML.GENERATOR\XMLSchema\ETDA\data\standard\";
                var fileXml = "TaxInvoice_Schematron_2p0.sch";
                XmlReader rd = XmlReader.Create(path + fileXml);
                XDocument doc = XDocument.Load(rd);

                //XElement tempElement = doc.Descendants(XName.Get("schema", "sch")).FirstOrDefault();
                //var maps = from item in doc.Root.Elements(XName.Get("schema", "sch"))
                //           select (item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> ReadTextFile()
        {
            List<string> result = new List<string>();
            string[] fullpath = new string[0];
            string pathFolder = "";
            List<string> listpath;
            string fileType = "*.txt";
            //ConfigXmlGenerator config = new ConfigXmlGenerator();
            //config.ConfigXmlGeneratorInputPath = @"D:\sign";
            //config.ConfigXmlGeneratorOutputPath = @"D:\sign";
            //configXMLGenerator = new List<ConfigXmlGenerator>();
            //configXMLGenerator.Add(config);
            configXMLGenerator = configXMLGenerator.Where(x => x.ConfigXmlGeneratorCompanyCode == "0090").ToList();
            try
            {
                foreach (var path in configXMLGenerator)
                {
                    pathFolder = path.ConfigXmlGeneratorInputPath;
                    fullpath = Directory.GetFiles(pathFolder, fileType);
                    listpath = fullpath.ToList();
                    result.AddRange(listpath);
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public DataTable ConvertToDataTable(string filePath, int numberOfColumns)
        {
            DataTable tbl = new DataTable();

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
            return tbl;
        }

        public List<TextFileSchematic> ConvertDTtoClass(DataTable dt)
        {
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
                        row.HEADER_FLAG = dt.Rows[i][0].ToString();
                        row.FI_DOC_TYPE = dt.Rows[i][1].ToString();
                        row.FISCAL_YEAR = dt.Rows[i][2].ToString();
                        row.IC_FLAG = dt.Rows[i][3].ToString();
                        row.SELLER_TIN = dt.Rows[i][4].ToString();
                        row.SELLER_BRANCH = dt.Rows[i][5].ToString();
                        row.BUYER_TYPE_CODE = dt.Rows[i][6].ToString();
                        row.BUYER_TAXID = dt.Rows[i][7].ToString();
                        row.BUYER_BRANCH = dt.Rows[i][8].ToString();
                        row.BUYER_NAME = dt.Rows[i][9].ToString();
                        row.BUYER_CODE = dt.Rows[i][10].ToString();
                        row.BUYER_PSTLZ = dt.Rows[i][11].ToString();
                        row.BUYER_LAND1 = dt.Rows[i][12].ToString();
                        row.SELLER_NAME = dt.Rows[i][13].ToString();
                        row.SELLER_BUKRS = dt.Rows[i][14].ToString();
                        row.SELLER_MAIL = dt.Rows[i][15].ToString();
                        row.SELLER_PSTLZ = dt.Rows[i][16].ToString();
                        row.SELLER_BUILD_NAME = dt.Rows[i][17].ToString();
                        row.SELLER_STREET = dt.Rows[i][18].ToString();
                        row.SELLER_CITY = dt.Rows[i][19].ToString();
                        row.SELLER_SUBDIV = dt.Rows[i][20].ToString();
                        row.SELLER_LAND1 = dt.Rows[i][21].ToString();
                        row.SELLER_PROVINCE = dt.Rows[i][22].ToString();
                        row.SELLER_BUILD_NO = dt.Rows[i][23].ToString();
                        row.BUYER_ADDRESS1 = dt.Rows[i][24].ToString();
                        row.BUYER_ADDRESS2 = dt.Rows[i][25].ToString();
                        row.BILLING_NO = dt.Rows[i][26].ToString();
                        row.FI_DOC = dt.Rows[i][27].ToString();
                        row.DOC_CURRENCY = dt.Rows[i][28].ToString();
                        row.SALES_AMOUNT = dt.Rows[i][29].ToString();
                        row.TAX_CODE = dt.Rows[i][30].ToString();
                        row.TAX_RATE = dt.Rows[i][31].ToString();
                        row.TAX_AMOUNT = dt.Rows[i][32].ToString();
                        row.CHARGE_AMOUNT = dt.Rows[i][33].ToString();
                        row.ALLOWANCE_AMOUNT = dt.Rows[i][34].ToString();
                        row.ORIGINAL_AMOUNT = dt.Rows[i][35].ToString();
                        row.DIFFERENCE_AMOUNT = dt.Rows[i][36].ToString();
                        row.TAX_BASIS_AMOUNT = dt.Rows[i][37].ToString();
                        row.TAX_TOTAL_AMOUNT = dt.Rows[i][38].ToString();
                        row.GRAND_TOTAL_AMOUNT = dt.Rows[i][39].ToString();
                        row.CREATE_DATE_TIME = dt.Rows[i][40].ToString();
                        row.REF_DOC = dt.Rows[i][41].ToString();
                        row.REF_DOC_DATE = dt.Rows[i][42].ToString();
                        row.REF_FI_DOCTYPE = dt.Rows[i][43].ToString();
                        row.ORDER_REASON = dt.Rows[i][44].ToString();
                        row.ORDER_REASON_CODE = dt.Rows[i][45].ToString();
                        row.BILLING_DATE = dt.Rows[i][46].ToString();
                        row.INSTANCE = dt.Rows[i][47].ToString();
                        row.BILLING_TYPE = dt.Rows[i][48].ToString();
                        row.SALES_ORG = dt.Rows[i][49].ToString();
                        row.PRINT_DATE_TIME = dt.Rows[i][50].ToString();
                        row.Number_Sold_to = dt.Rows[i][51].ToString();
                        row.Number_Ship_to = dt.Rows[i][52].ToString();
                        row.Number_Bill_to = dt.Rows[i][53].ToString();
                        row.Number_Payer = dt.Rows[i][54].ToString();
                        row.CORRECT_AMOUNT = dt.Rows[i][55].ToString();
                        row.pathfile = dt.Rows[i][56].ToString();
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
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
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

        public CrossIndustryInvoice ConvertClasstoXMLFormat(TextFileSchematic data)
        {
            CrossIndustryInvoice result = new CrossIndustryInvoice();
            try
            {
                CrossIndustryInvoice xmldata;
                ExchangedDocument exchangedDocument;
                SupplyChainTradeTransaction supplyChainTradeTransaction;
                IncludedSupplyChainTradeLineItem includedSupplyChainTradeLineItem;
                ProfileSeller profile = new ProfileSeller();
                ProfileCompany profiledetail = new ProfileCompany();
                DocumentCode doccode = new DocumentCode();
                LogicTool Tool = new LogicTool();
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

                xmldata.exchangedDocument = new ExchangedDocument();
                xmldata.exchangedDocument.id = data.BILLING_NO ?? "";
                xmldata.exchangedDocument.name = doccode.DocumentDescription ?? "";
                xmldata.exchangedDocument.typeCode = doccode.DocumentCodeRd ?? "";
                xmldata.exchangedDocument.issueDateTime = data.BILLING_DATE ?? "";
                xmldata.exchangedDocument.createionDateTime = data.CREATE_DATE_TIME ?? "";

                profile = profileController.ProfileAddress(profileSeller, profiledetail.CompanyCode, data.SELLER_BRANCH);
                supplyChainTradeTransaction.applicableHeaderTradeAgreement = new ApplicableHeaderTradeAgreement();
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty = new SellerTradeParty();
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.name = data.SELLER_NAME ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration = new SpecifiedTaxRegistration();
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id = data.SELLER_TIN ?? "";
                if ((!Tool.CheckDataRule(data.BUYER_TAXID, "") && !Tool.CheckDataRule(data.BUYER_BRANCH, "")) && (data.BUYER_TAXID.Length == 13 && data.BUYER_BRANCH.Length == 5))
                {
                    schemeID = "TXID";
                }
                else if ((!Tool.CheckDataRule(data.BUYER_TAXID, "")) && (data.BUYER_TAXID.Length == 13 && Tool.CheckDataRule(data.BUYER_BRANCH, "")))
                {
                    schemeID = "NIDN";
                }
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeID = schemeID ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeagencyid = "RD";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress = new PostalTradeAddress();
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.postcodeCode = data.SELLER_PSTLZ ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.streetName = profile.Road ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.cityName = profile.District ?? "";//code
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.citySubDivisionName = profile.SubDistrict ?? "";//code
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.countryID = data.SELLER_LAND1 ?? "";//schemeID="3166-1 alpha-2"
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.countrySubDivisionID = profile.Province ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.buildingNumber = profile.Addressnumber ?? "";

                supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty = new BuyerTradeParty();
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.name = data.SELLER_NAME ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration = new SpecifiedTaxRegistration();
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id = data.BUYER_TAXID ?? "" + data.BUYER_BRANCH ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress = new PostalTradeAddress();
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.postcodeCode = data.BUYER_PSTLZ ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.line1 = data.BUYER_ADDRESS1 ?? "";
                supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.countryID = data.BUYER_LAND1 ?? "";

                //supplyChainTradeTransaction.applicableHeaderTradeDelivery = new ApplicableHeaderTradeDelivery();
                //supplyChainTradeTransaction.applicableHeaderTradeDelivery.shipToTradeParty = new ShipToTradeParty();
                //supplyChainTradeTransaction.applicableHeaderTradeDelivery.shipToTradeParty.postalTradeAddress = new PostalTradeAddress();
                //supplyChainTradeTransaction.applicableHeaderTradeDelivery.shipToTradeParty.postalTradeAddress.postcodeCode = "";
                //supplyChainTradeTransaction.applicableHeaderTradeDelivery.shipToTradeParty.postalTradeAddress.lineThree = item.BUYER_CITY;
                //supplyChainTradeTransaction.applicableHeaderTradeDelivery.shipToTradeParty.postalTradeAddress.streetName = item.BUYER_SUBDIV;
                //supplyChainTradeTransaction.applicableHeaderTradeDelivery.shipToTradeParty.postalTradeAddress.cityName = item.BUYER_LAND1;
                //supplyChainTradeTransaction.applicableHeaderTradeDelivery.shipToTradeParty.postalTradeAddress.citySubDivisionName = item.BUYER_LAND1;
                //supplyChainTradeTransaction.applicableHeaderTradeDelivery.shipToTradeParty.postalTradeAddress.countryID = item.BUYER_PROVINCE;
                //supplyChainTradeTransaction.applicableHeaderTradeDelivery.shipToTradeParty.postalTradeAddress.countrySubDivisionID = item.BUYER_PROVINCE;
                //supplyChainTradeTransaction.applicableHeaderTradeDelivery.shipToTradeParty.postalTradeAddress.buildingNumber = item.BUYER_BUILD_NO;

                supplyChainTradeTransaction.applicableHeaderTradeSettlement = new ApplicableHeaderTradeSettlement();
                supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoiceCurrencyCode = new InvoiceCurrencyCode();
                supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoiceCurrencyCode.invoiceCurrencyCode = data.DOC_CURRENCY ?? "";//listID="ISO 4217 3A"
                supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax = new ApplicableTradeTax();
                supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.typeCode = taxCode.Where(x => x.TaxCodeErp == data.TAX_CODE).Select(x => x.TaxCodeRd).FirstOrDefault() ?? "";
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
                    includedSupplyChainTradeLineItem.specifiedTradeProduct.informationNote.subject = data.Item[i].PO_NUMBER ?? "";//
                    includedSupplyChainTradeLineItem.specifiedLineTradeAgreement = new SpecifiedLineTradeAgreement();
                    includedSupplyChainTradeLineItem.specifiedLineTradeAgreement.grossPriceProductTradePrice = new GrossPriceProductTradePrice();
                    includedSupplyChainTradeLineItem.specifiedLineTradeAgreement.grossPriceProductTradePrice.chargeAmount = data.Item[i].CHARGE_AMOUNT ?? "";
                    includedSupplyChainTradeLineItem.specifiedLineTradeDelivery = new SpecifiedLineTradeDelivery();
                    includedSupplyChainTradeLineItem.specifiedLineTradeDelivery.billedQuantity = new BilledQuantity();//format
                    includedSupplyChainTradeLineItem.specifiedLineTradeDelivery.billedQuantity.unitCode = data.Item[i].QUANTITY ?? "";//format
                    includedSupplyChainTradeLineItem.specifiedLineTradeDelivery.billedQuantity.billedQuantity = data.Item[i].SALES_UNT ?? "";//format
                    includedSupplyChainTradeLineItem.specifiedLineTradeSettlement = new SpecifiedLineTradeSettlement();
                    includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge = new SpecifiedTradeAllowanceCharge();
                    if (Convert.ToDecimal(data.ALLOWANCE_AMOUNT) > 0)
                    {
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge.chargeIndicator = "True";
                    }
                    else
                    {
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge.chargeIndicator = "False";
                    }
                    includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge.actualAmount = data.ALLOWANCE_AMOUNT ?? "";
                    includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation = new SpecifiedTradeSettlementLineMonetarySummation();
                    includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.taxTotalAmount = data.TAX_AMOUNT ?? "";
                    includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.NetLineTotalAmount = data.TAX_BASIS_AMOUNT ?? "";
                    includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.netIncludingTaxesLineTotalAmount = new NetIncludingTaxesLineTotalAmount();
                    includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.netIncludingTaxesLineTotalAmount.currencyID = data.Item[i].CURRENCY ?? "";//format
                    includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.netIncludingTaxesLineTotalAmount.netIncludingTaxesLineTotalAmount = data.Item[i].GRAND_TOTAL_AMOUNT ?? "";//format
                    supplyChainTradeTransaction.includedSupplyChainTradeLineItem.Add(includedSupplyChainTradeLineItem);
                    //supplyChainTradeTransaction.includedSupplyChainTradeLineItem = includedSupplyChainTradeLineItem;
                }

                xmldata.supplyChainTradeTransaction = supplyChainTradeTransaction;
                result = xmldata;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool GenXMLFromTemplate(CrossIndustryInvoice data, string pathoutbound, string filename, string billingno)
        {
            bool result = false;
            //pathoutbound = @"D:\gen\gen.xml";
            XDocument xml = new XDocument();
            List<string> errormessage = new List<string>();
            try
            {
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
                        result = GenXMLFile(xml, pathoutbound + filename);
                        break;
                    case "80":
                    case "81":
                        var debitcreditnote = new Template_DebitCreditNote();
                        xml = debitcreditnote.XMLtemplate(data);
                        result = GenXMLFile(xml, pathoutbound + filename);
                        break;
                    case "T01":
                    default:
                        break;
                }
                UpdateDataTransaction(errormessage, billingno);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool GenXMLFile(XDocument xml, string fullpath)
        {
            bool result = false;
            try
            {

                TextWriter filestream = new StreamWriter(fullpath);
                //TextWriter filestream = new StreamWriter(@"C:\Code_Dev\output.xml");
                xml.Save(filestream);
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public List<string> ValidateSchematron(CrossIndustryInvoice data)
        {
            List<string> errormessage = new List<string>();
            TaxInvoiceSchematronValidate TaxInvoice = new TaxInvoiceSchematronValidate();
            DebitCreditNoteSchematronValidate DebitCreditNote = new DebitCreditNoteSchematronValidate();
            ReceiptSchematronValidate Receipt = new ReceiptSchematronValidate();
            try
            {
                switch (data.exchangedDocument.typeCode)
                {
                    case "388":
                    case "T02":
                    case "T03":
                    case "T04":
                        errormessage = TaxInvoice.TaxInvoiceChackSchematron(data);
                        break;
                    case "80":
                    case "81":
                        errormessage = DebitCreditNote.DebitCreditNoteChackSchematron(data);
                        break;
                    case "T01":
                        errormessage = Receipt.ReceiptChackSchematron(data);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return errormessage;
        }

        public bool InsertDataTransactionDescription(TextFileSchematic dataxml, string source)
        {
            TransactionDescription data = new TransactionDescription();
            Task<Response> res;
            bool result = false;
            bool insert = false;
            try
            {
                string errorText = "";
                listdatatransactionDescription = transactionDescription.GetBilling(dataxml.BILLING_NO).Result;
                data = listdatatransactionDescription.FirstOrDefault();

                ProfileCompany profiledetail = new ProfileCompany();
                DocumentCode doccode = new DocumentCode();

                doccode = documentCode.FirstOrDefault(x => x.DocumentCodeErp == dataxml.FI_DOC_TYPE);
                profiledetail = profileCompany.FirstOrDefault(x => x.TaxNumber == dataxml.SELLER_TIN);

                if (data == null)
                {
                    insert = true;
                    data = new TransactionDescription();
                }

                var billingdate = Convert.ToDateTime(dataxml.BILLING_DATE);
                string poNumber = "";
                data.BillingDate = billingdate;
                data.BillingNumber = Convert.ToString(dataxml.BILLING_NO);
                data.BillingYear = billingdate.Year;
                data.BillTo = Convert.ToDouble(dataxml.Number_Bill_to);
                data.CompanyCode = Convert.ToDouble(profiledetail.CompanyCode);
                data.CompanyName = profiledetail.CompanyNameTh;
                data.CreateBy = "Batch";
                data.CreateDate = DateTime.Now;
                data.CustomerId = null;
                data.CustomerName = null;
                data.DocType = doccode.DocumentCodeRd;
                data.FiDoc = Convert.ToDouble(dataxml.FI_DOC);
                data.Foc = (dataxml.FI_DOC_TYPE == "FOC") ? 1 : 0;
                data.Ic = (string.IsNullOrEmpty(dataxml.IC_FLAG)) ? 0 : 1;
                data.ImageDocType = null;// mapping
                data.Isactive = 1;
                data.Payer = Convert.ToDouble(dataxml.Number_Payer);

                foreach (var item in dataxml.Item)
                {
                    poNumber = poNumber + item.PO_NUMBER + ",";
                }
                poNumber = poNumber.Substring(0, poNumber.Length - 1);
                data.PoNumber = poNumber;
                data.SellOrg = Convert.ToDouble(dataxml.SALES_ORG);
                data.ShipTo = Convert.ToDouble(dataxml.Number_Ship_to);
                data.SoldTo = Convert.ToDouble(dataxml.Number_Sold_to);
                data.SourceName = source;
                data.TypeInput = "Batch";
                data.UpdateBy = "Batch";
                data.UpdateDate = DateTime.Now;


                var json = JsonSerializer.Serialize(data);
                if (insert == true)
                {
                    res = transactionDescription.Insert(json);
                }
                else
                {
                    res = transactionDescription.Update(json);
                }
                if (res.Result.MESSAGE == "Updated Success." || res.Result.MESSAGE == "Insert success.")
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

        public bool MoveFile(string pathinput, string pathoutput, string filename)
        {
            bool result = false;
            //pathinpput = @"c:\temp\MySample.txt";
            pathoutput = @"D:\sign\backupfile\";
            try
            {
                if (!File.Exists(pathinput))
                {
                    // This statement ensures that the file is created,  
                    // but the handle is not kept.  
                    using (FileStream fs = File.Create(pathinput)) { }
                }
                // Ensure that the target does not exist.  
                if (!Directory.Exists(pathoutput))
                {
                    Directory.CreateDirectory(pathoutput);
                }
                // Move the file.  
                File.Move(pathinput, pathoutput + filename);
                Console.WriteLine("{0} was moved to {1}.", pathinput, pathoutput);

                // See if the original exists now.  
                if (File.Exists(pathinput))
                {
                    Console.WriteLine("The original file still exists, which is unexpected.");
                }
                else
                {
                    Console.WriteLine("The original file no longer exists, which is expected.");
                }
                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            return result;
        }

        public bool GenTextFileFail(string filename, TextFileSchematic dataxml, string path)
        {
            bool result = false;
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
                        //write.WriteLine(Environment.NewLine);
                        write.WriteLine(item);
                    }
                    write.Flush();
                    write.Close();
                    fs.Close();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public void GetDataFromDataBase()
        {
            try
            {
                configXMLGenerator = configXMLGeneratorController.List().Result;
                taxCode = taxCodeController.List().Result;
                documentCode = documentCodeController.List().Result;
                //erpDocument = erpDocumentController.List().Result;
                //rdDocument = rdDocumentController.List().Result;
                profileCompany = profileController.ProfileCompanyList().Result;
                profileSeller = profileController.ProfileSellerList().Result;
                productUnit = productUnitController.List().Result;
                profileBranches = profileBranchController.List().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateDataTransaction(List<string> errormessage,string billingNo)
        {
            bool result = false;
            string errorText = "";
            Task<Response> res;
            TransactionDescription dataTran = new TransactionDescription();
            try
            {
                listdatatransactionDescription = transactionDescription.GetBilling(billingNo).Result;
                dataTran = listdatatransactionDescription.FirstOrDefault();
                if(dataTran != null)
                {
                    if (errormessage.Count == 0)
                    {
                        dataTran.GenerateDateTime = DateTime.Now;
                        dataTran.GenerateDetail = "XML was generated completely";
                        dataTran.GenerateStatus = "Successful";
                        dataTran.UpdateBy = "Batch";
                        dataTran.UpdateDate = DateTime.Now;

                        var json = JsonSerializer.Serialize(dataTran);
                        res = transactionDescription.Update(json);
                        if (res.Result.MESSAGE == "Updated Success.")
                        {
                            result = true;
                        }
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

                        var json = JsonSerializer.Serialize(dataTran);
                        res = transactionDescription.Update(json);
                        if (res.Result.MESSAGE == "Updated Success.")
                        {
                            result = true;
                        }
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
