using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PDFSign.Class
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
                var data = ConvertClasstoSMLFormat(cs);
                GenXMLFile(data);

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
                string pathFolder = @"C:\Code_Dev\sign";
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

        public List<TaxInvoice_CrossIndustryInvoice> ConvertClasstoSMLFormat(List<TextFileSchematic> data)
        {
            List<TaxInvoice_CrossIndustryInvoice> result = new List<TaxInvoice_CrossIndustryInvoice>();
            try
            {
                TaxInvoice_CrossIndustryInvoice xmldata;
                ExchangedDocument exchangedDocument;
                SupplyChainTradeTransaction supplyChainTradeTransaction;
                IncludedSupplyChainTradeLineItem includedSupplyChainTradeLineItem;
                foreach (var item in data)
                {
                    xmldata = new TaxInvoice_CrossIndustryInvoice();
                    supplyChainTradeTransaction = new SupplyChainTradeTransaction();

                    xmldata.exchangedDocument = new ExchangedDocument();
                    xmldata.exchangedDocument.id = item.BILLING_NO;
                    xmldata.exchangedDocument.name = "ใบกำกับภาษี";//
                    xmldata.exchangedDocument.typeCode = "";//
                    xmldata.exchangedDocument.issueDateTime = item.REF_DOC_DATE;
                    xmldata.exchangedDocument.createionDateTime = item.CREATE_DATE_TIME;

                    supplyChainTradeTransaction.applicableHeaderTradeAgreement = new ApplicableHeaderTradeAgreement();
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty = new SellerTradeParty();
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.name = item.SELLER_NAME;
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration = new SpecifiedTaxRegistration();
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id = item.SELLER_TIN;// schemeAgencyID="RD" schemeID="TXID"
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress = new PostalTradeAddress();
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.postcodeCode = item.SELLER_PSTLZ;
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.streetName = item.SELLER_STREET;
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.cityName = item.SELLER_CITY;//code
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.citySubDivisionName = item.SELLER_SUBDIV;//code
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.countryID = item.SELLER_LAND1;//schemeID="3166-1 alpha-2"
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.countrySubDivisionID = item.SELLER_PROVINCE;
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.buildingNumber = item.SELLER_BUILD_NO;

                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty = new BuyerTradeParty();
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.name = item.SELLER_NAME;
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration = new SpecifiedTaxRegistration();
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id = item.BUYER_TAXID;
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress = new PostalTradeAddress();
                    supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.postcodeCode = item.BUYER_PSTLZ;
                    //supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.lineThree = item.BUYER_CITY;
                    //supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.streetName = item.BUYER_SUBDIV;
                    //supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.cityName = item.BUYER_LAND1;
                    //supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.citySubDivisionName = item.BUYER_LAND1;
                    //supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.countryID = item.BUYER_PROVINCE;
                    //supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.countrySubDivisionID = item.BUYER_PROVINCE;
                    //supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.buildingNumber = item.BUYER_BUILD_NO;

                    supplyChainTradeTransaction.applicableHeaderTradeDelivery = new ApplicableHeaderTradeDelivery();
                    supplyChainTradeTransaction.applicableHeaderTradeDelivery.shipToTradeParty = new ShipToTradeParty();
                    supplyChainTradeTransaction.applicableHeaderTradeDelivery.shipToTradeParty.postalTradeAddress = new PostalTradeAddress();
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
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoiceCurrencyCode.invoiceCurrencyCode = item.DOC_CURRENCY;//listID="ISO 4217 3A"
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax = new ApplicableTradeTax();
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.typeCode = item.TAX_CODE;// code to text
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.calculatedRate = item.TAX_RATE;
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.basisAmount = item.SALES_AMOUNT;
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.calculatedAmount = item.TAX_AMOUNT;
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeAllowanceCharge = new SpecifiedTradeAllowanceCharge();
                    //supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeAllowanceCharge.chargeIndicator = new SpecifiedTradeAllowanceCharge();
                    //supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeAllowanceCharge.actualAmount = new SpecifiedTradeAllowanceCharge();
                    //supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeAllowanceCharge.typecode = new SpecifiedTradeAllowanceCharge();
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation = new SpecifiedTradeSettlementHeaderMonetarySummation();
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.lineTotalAmount = item.SALES_AMOUNT;
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.allowanceTotalAmount = item.ALLOWANCE_AMOUNT;
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.taxBasisTotalAmount = item.TAX_BASIS_AMOUNT;
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.taxTotalAmount = item.TAX_TOTAL_AMOUNT;
                    supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.grandTotalAmount = item.GRAND_TOTAL_AMOUNT;

                    supplyChainTradeTransaction.includedSupplyChainTradeLineItem = new List<IncludedSupplyChainTradeLineItem>();
                    for(int i = 0; i < item.Item.Count; i++)
                    {
                        includedSupplyChainTradeLineItem = new IncludedSupplyChainTradeLineItem();
                        includedSupplyChainTradeLineItem.associatedDocumentLineDocument = new AssociatedDocumentLineDocument();
                        includedSupplyChainTradeLineItem.associatedDocumentLineDocument.lineID = (i + 1).ToString();
                        includedSupplyChainTradeLineItem.specifiedTradeProduct = new SpecifiedTradeProduct();
                        includedSupplyChainTradeLineItem.specifiedTradeProduct.name = item.Item[i].PRODUCT_NAME;
                        includedSupplyChainTradeLineItem.specifiedTradeProduct.informationNote = new InformationNote();
                        includedSupplyChainTradeLineItem.specifiedTradeProduct.informationNote.subject = "";//
                        includedSupplyChainTradeLineItem.specifiedLineTradeAgreement = new SpecifiedLineTradeAgreement();
                        includedSupplyChainTradeLineItem.specifiedLineTradeAgreement.grossPriceProductTradePrice = new GrossPriceProductTradePrice();
                        includedSupplyChainTradeLineItem.specifiedLineTradeAgreement.grossPriceProductTradePrice.chargeAmount = item.Item[i].CHARGE_AMOUNT;
                        includedSupplyChainTradeLineItem.specifiedLineTradeDelivery = new SpecifiedLineTradeDelivery();
                        includedSupplyChainTradeLineItem.specifiedLineTradeDelivery.billedQuantity = new BilledQuantity();//format
                        includedSupplyChainTradeLineItem.specifiedLineTradeDelivery.billedQuantity.unitCode = item.Item[i].QUANTITY;//format
                        includedSupplyChainTradeLineItem.specifiedLineTradeDelivery.billedQuantity.billedQuantity = item.Item[i].SALES_UNT;//format
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement = new SpecifiedLineTradeSettlement();
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.applicableTradeTax = new ApplicableTradeTax();
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.applicableTradeTax.typeCode = "VAT";//
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.applicableTradeTax.calculatedRate = "7";//
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.applicableTradeTax.basisAmount = "10700";//
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.applicableTradeTax.calculatedAmount = "749";//
                        //includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge = new List<SpecifiedTradeAllowanceCharge>();
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation = new SpecifiedTradeSettlementLineMonetarySummation();
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.taxTotalAmount = "";//
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.NetLineTotalAmount = "";//
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.netIncludingTaxesLineTotalAmount = new NetIncludingTaxesLineTotalAmount();
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.netIncludingTaxesLineTotalAmount.currencyID = item.Item[i].CURRENCY;//format
                        includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.netIncludingTaxesLineTotalAmount.netIncludingTaxesLineTotalAmount = item.Item[i].GRAND_TOTAL_AMOUNT;//format
                        supplyChainTradeTransaction.includedSupplyChainTradeLineItem.Add(includedSupplyChainTradeLineItem);
                    }
                    //supplyChainTradeTransaction.includedSupplyChainTradeLineItem.associatedDocumentLineDocument = new AssociatedDocumentLineDocument();
                    //supplyChainTradeTransaction.includedSupplyChainTradeLineItem.associatedDocumentLineDocument.lineID = "";

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

        public void GenXMLFile(List<TaxInvoice_CrossIndustryInvoice> data)
        {
            try
            {
                TaxInvoice_CrossIndustryInvoice dataXML = new TaxInvoice_CrossIndustryInvoice();
                XmlSerializer serialiser = new XmlSerializer(typeof(TaxInvoice_CrossIndustryInvoice));
                // Create the TextWriter for the serialiser to use
                TextWriter filestream = new StreamWriter(@"C:\Code_Dev\output.xml");
                dataXML = data[0];
                //write to the file
                serialiser.Serialize(filestream, dataXML);
                //  <Foos>
                //  <foo Id="1" property1="someprop1" property2="someprop2" />
                //  <foo Id="1" property1="another" property2="third" />
                //  </Foos>
                //var xml = new XElement("Foos", foos.Select(x => new XElement("foo",
                //                               new XAttribute("Id", x.Id),
                //                               new XAttribute("property1", x.property1),
                //                               new XAttribute("property2", x.property2))));


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //        XNamespace ns1 = "http://example.com/ns1";
        //        string prefix = "pf1";
        //        string localName = "foo";
        //        XElement el = new XElement(ns1 + localName, new XAttribute(XNamespace.Xmlns + prefix, ns1));
        //        Console.WriteLine(el);
        //          Its output is
        //          <pf1:foo xmlns:pf1="http://example.com/ns1" />

        public class TaxInvoice_CrossIndustryInvoice
        {
            public ExchangedDocumentContext exchangedDocumentContext { get; set; }
            public ExchangedDocument exchangedDocument { get; set; }
            public SupplyChainTradeTransaction supplyChainTradeTransaction { get; set; }
        }

        public class ExchangedDocumentContext
        {
            public GuidelineSpecifiedDocumentContextParameter guidelineSpecifiedDocumentContextParameter { get; set; }
        }

        public class GuidelineSpecifiedDocumentContextParameter
        {
            public string value { get; set; }
            public string schemeAgencyID { get; set; }
            public string schemeVersionID { get; set; }
        }
        public class ExchangedDocument
        {
            public string id { get; set; }
            public string name { get; set; }
            public string typeCode { get; set; }
            public string issueDateTime { get; set; }
            public string createionDateTime { get; set; }
            public IncludedNote includedNote { get; set; }
        }

        public class IncludedNote
        {
            public string subject { get; set; }
        }

        public class SupplyChainTradeTransaction
        {
            public ApplicableHeaderTradeAgreement applicableHeaderTradeAgreement { get; set; }
            public ApplicableHeaderTradeDelivery applicableHeaderTradeDelivery { get; set; }
            public ApplicableHeaderTradeSettlement applicableHeaderTradeSettlement { get; set; }
            public List<IncludedSupplyChainTradeLineItem> includedSupplyChainTradeLineItem { get; set; }
        }

        public class ApplicableHeaderTradeDelivery
        {
            public ShipToTradeParty shipToTradeParty { get; set; }
        }
        public class ShipToTradeParty
        {
            public PostalTradeAddress postalTradeAddress { get; set; }
        }
        public class ApplicableHeaderTradeAgreement
        {
            public SellerTradeParty sellerTradeParty { get; set; }
            public BuyerTradeParty buyerTradeParty { get; set; }
        }

        public class SellerTradeParty
        {
            public string name { get; set; }
            public SpecifiedTaxRegistration specifiedTaxRegistration { get; set; }
            public PostalTradeAddress postalTradeAddress { get; set; }
        }
        public class SpecifiedTaxRegistration
        {
            public string id { get; set; }
            public string schemeAgencyID { get; set; }
            public string schemeID { get; set; }
        }
        public class PostalTradeAddress
        {
            public string postcodeCode { get; set; }
            public string lineThree { get; set; }
            public string streetName { get; set; }
            public string cityName { get; set; }
            public string citySubDivisionName { get; set; }
            public string countryID { get; set; }
            public string countrySubDivisionID { get; set; }
            public string buildingNumber { get; set; }
            public string lineOne { get; set; }
        }
        public class BuyerTradeParty
        {
            public string name { get; set; }
            public SpecifiedTaxRegistration specifiedTaxRegistration { get; set; }
            public PostalTradeAddress postalTradeAddress { get; set; }
        }

        public class ApplicableHeaderTradeSettlement
        {
            public InvoiceCurrencyCode invoiceCurrencyCode { get; set; }
            public ApplicableTradeTax applicableTradeTax { get; set; }
            public SpecifiedTradeAllowanceCharge specifiedTradeAllowanceCharge { get; set; }
            public SpecifiedTradeSettlementHeaderMonetarySummation specifiedTradeSettlementHeaderMonetarySummation { get; set; }
        }
        public class InvoiceCurrencyCode
        {
            public string invoiceCurrencyCode { get; set; }
            public string listID { get; set; }
        }
        public class ApplicableTradeTax
        {
            public string typeCode { get; set; }
            public string calculatedRate { get; set; }
            public string basisAmount { get; set; }
            public string calculatedAmount { get; set; }
        }
        public class SpecifiedTradeSettlementHeaderMonetarySummation
        {
            public string lineTotalAmount { get; set; }
            public string allowanceTotalAmount { get; set; }
            public string chargeTotalAmount { get; set; }
            public string taxBasisTotalAmount { get; set; }
            public string taxTotalAmount { get; set; }
            public string grandTotalAmount { get; set; }
        }
        public class IncludedSupplyChainTradeLineItem
        {
            public AssociatedDocumentLineDocument associatedDocumentLineDocument { get; set; }
            public SpecifiedTradeProduct specifiedTradeProduct { get; set; }
            public SpecifiedLineTradeAgreement specifiedLineTradeAgreement { get; set; }
            public SpecifiedLineTradeDelivery specifiedLineTradeDelivery { get; set; }
            public SpecifiedLineTradeSettlement specifiedLineTradeSettlement { get; set; }
        }
        public class AssociatedDocumentLineDocument
        {
            public string lineID { get; set; }
        }
        public class SpecifiedTradeProduct
        {
            public string id { get; set; }
            public string name { get; set; }
            public InformationNote informationNote { get; set; }
        }
        public class InformationNote
        {
            public string subject { get; set; }
        }
        public class SpecifiedLineTradeAgreement
        {
            public GrossPriceProductTradePrice grossPriceProductTradePrice { get; set; }
        }
        public class GrossPriceProductTradePrice
        {
            public string chargeAmount { get; set; }
        }
        public class SpecifiedLineTradeDelivery
        {
            public BilledQuantity billedQuantity { get; set; }
        }
        public class BilledQuantity
        {
            public string billedQuantity { get; set; }
            public string unitCode { get; set; }
        }
        public class SpecifiedLineTradeSettlement
        {
            public ApplicableTradeTax applicableTradeTax { get; set; }
            public List<SpecifiedTradeAllowanceCharge> specifiedTradeAllowanceCharge { get; set; }
            public SpecifiedTradeSettlementLineMonetarySummation specifiedTradeSettlementLineMonetarySummation { get; set; }
        }
        public class SpecifiedTradeAllowanceCharge
        {
            public string chargeIndicator { get; set; }
            public string actualAmount { get; set; }
            public string typecode { get; set; }
        }
        public class SpecifiedTradeSettlementLineMonetarySummation
        {
            public string taxTotalAmount { get; set; }
            public string NetLineTotalAmount { get; set; }
            public NetIncludingTaxesLineTotalAmount netIncludingTaxesLineTotalAmount { get; set; }
        }
        public class NetLineTotalAmount
        {
            public string netLineTotalAmount { get; set; }
            public string currencyID { get; set; }
        }
        public class NetIncludingTaxesLineTotalAmount
        {
            public string netIncludingTaxesLineTotalAmount { get; set; }
            public string currencyID { get; set; }
        }

        public class TextFileSchematic
        {
            public string HEADER_FLAG { get; set; }
            public string FI_DOC_TYPE { get; set; }
            public string FISCAL_YEAR { get; set; }
            public string IC_FLAG { get; set; }
            public string SELLER_TIN { get; set; }
            public string SELLER_BRANCH { get; set; }
            public string BUYER_TYPE_CODE { get; set; }
            public string BUYER_TAXID { get; set; }
            public string BUYER_BRANCH { get; set; }
            public string BUYER_NAME { get; set; }
            public string BUYER_CODE { get; set; }
            public string BUYER_PSTLZ { get; set; }
            public string BUYER_LAND1 { get; set; }
            public string SELLER_NAME { get; set; }
            public string SELLER_BUKRS { get; set; }
            public string SELLER_MAIL { get; set; }
            public string SELLER_PSTLZ { get; set; }
            public string SELLER_BUILD_NAME { get; set; }
            public string SELLER_STREET { get; set; }
            public string SELLER_CITY { get; set; }
            public string SELLER_SUBDIV { get; set; }
            public string SELLER_LAND1 { get; set; }
            public string SELLER_PROVINCE { get; set; }
            public string SELLER_BUILD_NO { get; set; }
            public string BUYER_ADDRESS1 { get; set; }
            public string BUYER_ADDRESS2 { get; set; }
            public string BILLING_NO { get; set; }
            public string FI_DOC { get; set; }
            public string DOC_CURRENCY { get; set; }
            public string SALES_AMOUNT { get; set; }
            public string TAX_CODE { get; set; }
            public string TAX_RATE { get; set; }
            public string TAX_AMOUNT { get; set; }
            public string CHARGE_AMOUNT { get; set; }
            public string ALLOWANCE_AMOUNT { get; set; }
            public string ORIGINAL_AMOUNT { get; set; }
            public string DIFFERENCE_AMOUNT { get; set; }
            public string TAX_BASIS_AMOUNT { get; set; }
            public string TAX_TOTAL_AMOUNT { get; set; }
            public string GRAND_TOTAL_AMOUNT { get; set; }
            public string CREATE_DATE_TIME { get; set; }
            public string REF_DOC { get; set; }
            public string REF_DOC_DATE { get; set; }
            public string REF_FI_DOCTYPE { get; set; }
            public string ORDER_REASON { get; set; }
            public string ORDER_REASON_CODE { get; set; }
            public string BILLING_DATE { get; set; }
            public string INSTANCE { get; set; }
            public string BILLING_TYPE { get; set; }
            public string SALES_ORG { get; set; }
            public string PRINT_DATE_TIME { get; set; }
            public string Number_Sold_to { get; set; }
            public string Number_Ship_to { get; set; }
            public string Number_Bill_to { get; set; }
            public string Number_Payer { get; set; }
            public string CORRECT_AMOUNT { get; set; }
            public List<TextFileItemSchematic> Item { get; set; }
        }
        public class TextFileItemSchematic
        {
            public string ITEM_FLAG { get; set; }
            public string FI_DOC_TYPE { get; set; }
            public string SELLER_TIN { get; set; }
            public string SELLER_BRANCH { get; set; }
            public string BILLING_NO { get; set; }
            public string ITEM_NO { get; set; }
            public string PRODUCT_NAME { get; set; }
            public string QUANTITY { get; set; }
            public string SALES_UNT { get; set; }
            public string UNIT_NAME { get; set; }
            public string CURRENCY { get; set; }
            public string UNIT_PRICT { get; set; }
            public string NETUNIT_PRICE { get; set; }
            public string TAX_AMOUNT { get; set; }
            public string CHARGE_AMOUNT { get; set; }
            public string ALLOWANCE_AMOUNT { get; set; }
            public string TAX_BASIS_AMOUNT { get; set; }
            public string TAX_TOTAL_AMOUNT { get; set; }
            public string GRAND_TOTAL_AMOUNT { get; set; }
            public string PO_NUMBER { get; set; }
            public string BILLING_DATE { get; set; }
        }
    }
}
