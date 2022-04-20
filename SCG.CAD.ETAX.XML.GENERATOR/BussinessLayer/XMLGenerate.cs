

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
        public void ReadXMLFile()
        {
            try
            {
                int row = 56;
                var allTextFile = ReadTextFile();
                var dt = ConvertToDataTable(allTextFile, row);
                var cs = ConvertDTtoClass(dt);
                List<string> errormessage = new List<string>();

                foreach (var dataxml in cs)
                {
                    InsertDataTransactionDescription(dataxml);
                }
                var data = ConvertClasstoXMLFormat(cs);
                foreach (var dataxml in data)
                {
                    errormessage.AddRange(ValidateSchematron(dataxml));
                    var xml = GenXMLFromTemplate(dataxml, errormessage);
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

        public string[] ReadTextFile()
        {
            string[] result = new string[0];
            try
            {
                StringBuilder sb = new StringBuilder();
                string pathFolder = @"D:\sign";
                string fileType = "*.txt";
                result = Directory.GetFiles(pathFolder, fileType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public DataTable ConvertToDataTable(string[] filePath, int numberOfColumns)
        {
            DataTable tbl = new DataTable();

            for (int col = 0; col < numberOfColumns; col++)
                tbl.Columns.Add(new DataColumn("Column" + (col + 1).ToString()));


            foreach (string path in filePath)
            {
                string[] lines = System.IO.File.ReadAllLines(path);

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

                        tbl.Rows.Add(dr);
                    }
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

        public List<CrossIndustryInvoice> ConvertClasstoXMLFormat(List<TextFileSchematic> data)
        {
            List<CrossIndustryInvoice> result = new List<CrossIndustryInvoice>();
            try
            {
                CrossIndustryInvoice xmldata;
                ExchangedDocument exchangedDocument;
                SupplyChainTradeTransaction supplyChainTradeTransaction;
                IncludedSupplyChainTradeLineItem includedSupplyChainTradeLineItem;


                TaxCodeController taxCodeController = new TaxCodeController();
                DocumentCodeController documentCodeController = new DocumentCodeController();
                ErpDocumentController erpDocumentController = new ErpDocumentController();
                ProductUnitController productUnitController = new ProductUnitController();
                RdDocumentController rdDocumentController = new RdDocumentController();
                ProfileController profileController = new ProfileController();
                List<TaxCode> taxCode = taxCodeController.List().Result;
                List<DocumentCode> documentCode = documentCodeController.List().Result;
                List<ErpDocument> erpDocument = erpDocumentController.List().Result;
                List<ProductUnit> productUnit = productUnitController.List().Result;
                List<RdDocument> rdDocument = rdDocumentController.List().Result;
                List<ProfileCompany> profileCompany = profileController.ProfileCompanyList().Result;
                List<ProfileSeller> profileSeller = profileController.ProfileSellerList().Result;
                ProfileSeller profile = new ProfileSeller();

                ProfileCompany profiledetail = new ProfileCompany();
                DocumentCode doccode = new DocumentCode();
                LogicTool Tool = new LogicTool();
                string schemeID = "OTHR";
                foreach (var item in data)
                {
                    xmldata = new CrossIndustryInvoice();
                    supplyChainTradeTransaction = new SupplyChainTradeTransaction();
                    doccode = documentCode.FirstOrDefault(x => x.DocumentCodeErp == item.FI_DOC_TYPE);
                    profiledetail = profileCompany.FirstOrDefault(x => x.TaxNumber == item.SELLER_TIN);

                    xmldata.exchangedDocumentContext = new ExchangedDocumentContext();
                    xmldata.exchangedDocumentContext.guidelineSpecifiedDocumentContextParameter = new GuidelineSpecifiedDocumentContextParameter();
                    xmldata.exchangedDocumentContext.guidelineSpecifiedDocumentContextParameter.schemeAgencyID = "ETDA";
                    xmldata.exchangedDocumentContext.guidelineSpecifiedDocumentContextParameter.id = "ER3-2560";
                    xmldata.exchangedDocumentContext.guidelineSpecifiedDocumentContextParameter.schemeVersionID = "v2.0";

                    xmldata.exchangedDocument = new ExchangedDocument();
                    xmldata.exchangedDocument.id = item.BILLING_NO ?? "";
                    xmldata.exchangedDocument.name = doccode.DocumentDescription ?? "";
                    xmldata.exchangedDocument.typeCode = doccode.DocumentCodeRd ?? "";
                    xmldata.exchangedDocument.issueDateTime = item.BILLING_DATE ?? "";
                    xmldata.exchangedDocument.createionDateTime = item.CREATE_DATE_TIME ?? "";

                    profile = profileController.ProfileAddress(profileSeller, profiledetail.CompanyCode, item.SELLER_BRANCH);
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement = new ApplicableHeaderTradeAgreement();
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty = new SellerTradeParty();
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.name = item.SELLER_NAME ?? "";
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration = new SpecifiedTaxRegistration();
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id = item.SELLER_TIN ?? "";
                    if((!Tool.CheckDataRule(item.BUYER_TAXID,"") && !Tool.CheckDataRule(item.BUYER_BRANCH, "")) && (item.BUYER_TAXID.Length == 13 && item.BUYER_BRANCH.Length == 5))
                    {
                        schemeID = "TXID";
                    }
                    else if ((!Tool.CheckDataRule(item.BUYER_TAXID, "")) && (item.BUYER_TAXID.Length == 13 && Tool.CheckDataRule(item.BUYER_BRANCH, "")))
                    {
                        schemeID = "NIDN";
                    }
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeID = schemeID ?? "";
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeagencyid = "RD";
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress = new PostalTradeAddress();
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.postcodeCode = item.SELLER_PSTLZ ?? "";
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.streetName = profile.Road ?? "";
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.cityName = profile.District ?? "";//code
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.citySubDivisionName = profile.SubDistrict ?? "";//code
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.countryID = item.SELLER_LAND1 ?? "";//schemeID="3166-1 alpha-2"
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.countrySubDivisionID = profile.Province ?? "";
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.buildingNumber = item.SELLER_BUILD_NO ?? "";

                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty = new BuyerTradeParty();
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.name = item.SELLER_NAME ?? "";
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration = new SpecifiedTaxRegistration();
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id = item.BUYER_TAXID ?? "";
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress = new PostalTradeAddress();
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.postcodeCode = item.BUYER_PSTLZ ?? "";
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.line1 = item.BUYER_ADDRESS1 ?? "";
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.countryID = item.BUYER_LAND1 ?? "";

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
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoiceCurrencyCode.invoiceCurrencyCode = item.DOC_CURRENCY ?? "";//listID="ISO 4217 3A"
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax = new ApplicableTradeTax();
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.typeCode = taxCode.Where(x => x.TaxCodeErp == item.TAX_CODE).Select(x => x.TaxCodeRd).FirstOrDefault() ?? "";
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.calculatedRate = item.TAX_RATE ?? "";
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.basisAmount = item.SALES_AMOUNT ?? "";
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.calculatedAmount = item.TAX_AMOUNT ?? "";
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation = new SpecifiedTradeSettlementHeaderMonetarySummation();
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.lineTotalAmount = item.SALES_AMOUNT ?? "";
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.allowanceTotalAmount = item.ALLOWANCE_AMOUNT ?? "";
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.taxBasisTotalAmount = item.TAX_BASIS_AMOUNT ?? "";
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.taxTotalAmount = item.TAX_TOTAL_AMOUNT ?? "";
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.grandTotalAmount = item.GRAND_TOTAL_AMOUNT ?? "";
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.chargeTotalAmount = item.CHARGE_AMOUNT ?? "";

                    supplyChainTradeTransaction.includedSupplyChainTradeLineItem = new IncludedSupplyChainTradeLineItem();
                    for (int i = 0; i < item.Item.Count; i++)
                    {
                        includedSupplyChainTradeLineItem = new IncludedSupplyChainTradeLineItem();
                        includedSupplyChainTradeLineItem.associatedDocumentLineDocument = new AssociatedDocumentLineDocument();
                        includedSupplyChainTradeLineItem.associatedDocumentLineDocument.lineID = (i + 1).ToString();
                        includedSupplyChainTradeLineItem.specifiedTradeProduct = new SpecifiedTradeProduct();
                        includedSupplyChainTradeLineItem.specifiedTradeProduct.name = item.Item[i].PRODUCT_NAME ?? "";
                        //includedSupplyChainTradeLineItem.specifiedTradeProduct.informationNote = new InformationNote();
                        //includedSupplyChainTradeLineItem.specifiedTradeProduct.informationNote.subject = "";//
                        includedSupplyChainTradeLineItem.specifiedLineTradeAgreement = new SpecifiedLineTradeAgreement();
                        includedSupplyChainTradeLineItem.specifiedLineTradeAgreement.grossPriceProductTradePrice = new GrossPriceProductTradePrice();
                        includedSupplyChainTradeLineItem.specifiedLineTradeAgreement.grossPriceProductTradePrice.chargeAmount = item.Item[i].CHARGE_AMOUNT ?? "";
                        includedSupplyChainTradeLineItem.specifiedLineTradeDelivery = new SpecifiedLineTradeDelivery();
                        includedSupplyChainTradeLineItem.specifiedLineTradeDelivery.billedQuantity = new BilledQuantity();//format
                        includedSupplyChainTradeLineItem.specifiedLineTradeDelivery.billedQuantity.unitCode = item.Item[i].QUANTITY ?? "";//format
                        includedSupplyChainTradeLineItem.specifiedLineTradeDelivery.billedQuantity.billedQuantity = item.Item[i].SALES_UNT ?? "";//format
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement = new SpecifiedLineTradeSettlement();
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge = new List<SpecifiedTradeAllowanceCharge>();
                        var specifiedTradeAllowanceCharge = new SpecifiedTradeAllowanceCharge();
                        if (Convert.ToDecimal(item.ALLOWANCE_AMOUNT) > 0)
                        {
                            specifiedTradeAllowanceCharge.chargeIndicator = "True";
                        }
                        else
                        {
                            specifiedTradeAllowanceCharge.chargeIndicator = "False";
                        }
                        specifiedTradeAllowanceCharge.actualAmount = item.ALLOWANCE_AMOUNT ?? "";
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge.Add(specifiedTradeAllowanceCharge);
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation = new SpecifiedTradeSettlementLineMonetarySummation();
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.taxTotalAmount = item.TAX_AMOUNT ?? "";
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.NetLineTotalAmount = item.TAX_BASIS_AMOUNT ?? "";
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.netIncludingTaxesLineTotalAmount = new NetIncludingTaxesLineTotalAmount();
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.netIncludingTaxesLineTotalAmount.currencyID = item.Item[i].CURRENCY ?? "";//format
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.netIncludingTaxesLineTotalAmount.netIncludingTaxesLineTotalAmount = item.Item[i].GRAND_TOTAL_AMOUNT ?? "";//format
                        //supplyChainTradeTransaction.includedSupplyChainTradeLineItem.Add(includedSupplyChainTradeLineItem);
                        supplyChainTradeTransaction.includedSupplyChainTradeLineItem = includedSupplyChainTradeLineItem;
                    }

                    xmldata.supplyChainTradeTransaction = supplyChainTradeTransaction;
                    result.Add(xmldata);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool GenXMLFromTemplate(CrossIndustryInvoice data, List<string> errormessage)
        {
            bool result = false;
            string errorText = "";
            string pathoutbound = @"D:\gen\gen.xml";
            bool issuccess = false;
            XDocument xml = new XDocument();
            try
            {
                //XmlSerializer serialiser = new XmlSerializer(typeof(CrossIndustryInvoice));
                //// Create the TextWriter for the serialiser to use
                //TextWriter filestream = new StreamWriter(@"C:\Code_Dev\output.xml");

                TransactionDescriptionController transactionDescription = new TransactionDescriptionController();
                TransactionDescription dataTran = new TransactionDescription();
                List<TransactionDescription> listdatatransactionDescription = new List<TransactionDescription>();
                Task<Response> res;

                switch (data.exchangedDocument.typeCode)
                {
                    case "388":
                    case "T02":
                    case "T03":
                    case "T04":
                        var taxinvoice = new Template_TaxInvoice();
                        xml = taxinvoice.XMLtemplate(data);
                        result = GenXMLFile(xml, pathoutbound);
                        break;
                    case "80":
                    case "81":
                        var debitcreditnote = new Template_DebitCreditNote();
                        xml = debitcreditnote.XMLtemplate(data);
                        result = GenXMLFile(xml, pathoutbound);
                        break;
                    case "T01":
                    default:
                        break;
                }

                listdatatransactionDescription = transactionDescription.List().Result;
                dataTran = listdatatransactionDescription.FirstOrDefault(x => x.BillingNumber.ToString() == data.exchangedDocument.id);
                if (errormessage.Count == 0 && result == true)
                {
                    issuccess = true;
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
                        //result = true;
                    }
                }
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

        public bool InsertDataTransactionDescription(TextFileSchematic dataxml)
        {
            TransactionDescriptionController transactionDescription = new TransactionDescriptionController();
            TransactionDescription data = new TransactionDescription();
            Task<Response> res;
            bool result = false;
            bool insert = false;
            try
            {
                string errorText = "";
                var listdatatransactionDescription = transactionDescription.List().Result;
                data = listdatatransactionDescription.FirstOrDefault(x => x.BillingNumber.ToString() == dataxml.BILLING_NO);

                ProfileCompany profiledetail = new ProfileCompany();
                DocumentCode doccode = new DocumentCode();
                ProfileController profileController = new ProfileController();
                DocumentCodeController documentCodeController = new DocumentCodeController();
                List<DocumentCode> documentCode = documentCodeController.List().Result;
                List<ProfileCompany> profileCompany = profileController.ProfileCompanyList().Result;

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
                data.BillingNumber = Convert.ToDouble(dataxml.BILLING_NO);
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
                data.SourceName = "";//
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

    }
}
