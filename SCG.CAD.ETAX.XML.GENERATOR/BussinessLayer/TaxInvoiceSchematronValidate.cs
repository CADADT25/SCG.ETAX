using SCG.CAD.ETAX.XML.GENERATOR.Models;

namespace SCG.CAD.ETAX.XML.GENERATOR.BussinessLayer
{
    public class TaxInvoiceSchematronValidate
    {
        LogicTool Tool = new LogicTool();
        public List<string> TaxInvoiceChackSchematron(CrossIndustryInvoice dataXml)
        {
            List<string> errormessage = new List<string>();
            try
            {
                //<!-- Check Document Type Code -->
                // "rsm:TaxInvoice_CrossIndustryInvoice/rsm:ExchangedDocument"
                if (!Tool.CheckDataRule(dataXml.exchangedDocument.typeCode, "388|T02|T03|T04"))
                {
                    errormessage.Add("TIV-Document-001 รหัสประเภทเอกสาร (TypeCode) ไม่ถูกต้อง (388 = ใบกำกับภาษี,T02 = ใบแจ้งหนี้ / ใบกำกับภาษี, T03 = ใบเสร็จรับเงิน / ใบกำกับภาษี, T04 = ใบส่งของ / ใบกำกับภาษี) (TypeCode must equal to 388, T02, T03, T04)");
                }
                //rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:AdditionalReferencedDocument
                if ((Tool.CheckDataRule(dataXml.exchangedDocument.purposeCode, "TIVC99")) && (Tool.CheckDataRule(dataXml.exchangedDocument.purpose, "")))
                {
                    errormessage.Add("TIV-Document-005 กรณีระบุรหัสสาเหตุการออกเอกสาร(PurposeCode) มีค่าเท่ากับ TIVC99 ต้องระบุสาเหตุการออกเอกสาร(Purpose)(Purpose must be present since PurposeCode equals to TIVC99");
                }
                //if ((dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id.Length >= 13 && dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.id.Length >= 13) && 
                //    ((dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id).Substring(0, 13) == (dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.id).Substring(0, 13)))
                //{
                //    errormessage.Add("TIV-Document-006 เลขประจำตัวผู้เสียภาษีอากรของผู้ออกเอกสารแทนต้องไม่เท่ากับเลขประจำตัวผู้เสียภาษีอากรของผู้ขาย (InvoicerTradeParty / SpecifiedTaxRegistration / ID must not equal to SellerTradeParty / SpecifiedTaxRegistration / ID.)");
                //}



                if (dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.name == "")
                {
                    errormessage.Add("TIV-SellerTradeParty-001 ต้องระบุชื่อผู้ขาย (Name must be present since SellerTradeParty is present.");
                }
                if (dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration == null)
                {
                    errormessage.Add("TIV-SellerTradeParty-002 ต้องระบุข้อมูลเลขประจำตัวผู้เสียภาษีอากรของผู้ขาย (SpecifiedTaxRegistration) (SpecifiedTaxRegistration must be present since SellerTradeParty is present.");
                }
                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id, ""))
                {
                    errormessage.Add("TIV-SellerTradeParty-003 ต้องระบุข้อมูลเลขประจำตัวผู้เสียภาษีอากรของผู้ขาย (SpecifiedTaxRegistration/ID) (ID must be present since SellerTradePart/SpecifiedTaxRegistration)");
                }
                if (!Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeID, "TXID|NIDN"))
                {
                    errormessage.Add("TIV-SellerTradeParty-004 ต้องระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ขาย (schemeID) และต้องมีค่าเป็น TXID หรือ NIDN เท่านั้น (schemeID must equal to TXID or NIDN.");
                }
                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeID, "TXID") && dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id.Length != 18)
                {
                    errormessage.Add("TIV-SellerTradeParty-005 กรณีระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ขาย (schemeID) เป็น TXID ต้องมีจำนวนตัวเลขเท่ากับ 18 หลัก (เลขประจำตัวผู้เสียภาษีอากร 13หลัก และเลขสาขา 5 หลัก) (SellerTradeParty/SpecifiedTaxRegistration/ID must length equal to 18 since schemeID is TXID.");
                }
                if (!Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id, "") &&
                    (!Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeID, "TXID") &&
                    (dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id.Length == 18) &&
                    !Tool.CheckDataType(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id, TypeData.Interger)))
                {
                    errormessage.Add("TIV-SellerTradeParty-013 กรณีระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ขาย (schemeID) เป็น TXID เลขสาขาต้องเป็นตัวเลขเท่านั้น (SellerTradeParty/SpecifiedTaxRegistration/ID must number only because schemeID is TXID.");
                }
                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeID, "NIDN") &&
                    !(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id.Length == 13) &&
                    !Tool.CheckDataType(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id, TypeData.Interger))
                {
                    errormessage.Add("TIV-SellerTradeParty-006 กรณีระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ขาย (schemeID) เป็น NIDN ต้องมีจำนวนตัวเลขเท่ากับ 13 หลัก (เลขประจำตัวประชาชน 13 หลัก) (SellerTradeParty/SpecifiedTaxRegistration/ID must length equal to 13 since schemeID is NIDN.");
                }
                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeID, "NIDN") &&
                    dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id.Length >= 13 &&
                    Tool.CheckDataRule((dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id).Substring(00, 13), "0000000000000|1111111111111"))
                {
                    errormessage.Add("TIV-SellerTradeParty-014 ระบุเลขประจำตัวผู้เสียภาษีอากรของผู้ขายไม่ถูกต้อง (SellerTradeParty/SpecifiedTaxRegistration/ID incorrect)");
                }
                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeID, "TXID") &&
                    dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id.Length >= 13 &&
                    Tool.CheckDataRule((dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id).Substring(0, 13), "0000000000000|1111111111111"))
                {
                    errormessage.Add("TIV-SellerTradeParty-014 ระบุเลขประจำตัวผู้เสียภาษีอากรของผู้ขายไม่ถูกต้อง (SellerTradeParty/SpecifiedTaxRegistration/ID incorrect)");
                }
                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.postcodeCode, "") ||
                    dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.postcodeCode.Length != 5)
                {
                    errormessage.Add("TIV-SellerTradeParty-007 ต้องระบุรหัสไปรษณีย์ผู้ขาย มีจำนวนตัวเลขเท่ากับ 5 หลัก (PostcodeCode must be present since SellerTradeParty/PostalAddress is present.)");
                }
                if (!Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.countryID, "TH"))
                {
                    errormessage.Add("TIV-SellerTradeParty-008  ต้องระบุรหัสประเทศผู้ขาย ต้องมีค่าเท่ากับ TH เท่านั้น (CountryID must be present since SellerTradeParty/PostalAddress is present.");
                }
                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.buildingNumber, ""))
                {
                    errormessage.Add("TIV-SellerTradeParty-009 ต้องระบุ บ้านเลขที่ (BuildingNumber) (BuildingNumber must be present since CountryID is TH.");
                }
                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.cityName, ""))
                {
                    errormessage.Add("TIV-SellerTradeParty-010 ต้องระบุ ชื่ออำเภอ (CityName) (CityName must be present since CountryID is TH.)");
                }
                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.citySubDivisionName, ""))
                {
                    errormessage.Add("TIV-SellerTradeParty-011 ต้องระบุ ชื่อตำบล (CitySubDivisionName) (CitySubDivisionName must be present since CountryID is TH.)");
                }
                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.countrySubDivisionID, ""))
                {
                    errormessage.Add("TIV-SellerTradeParty-012 ต้องระบุ รหัสจังหวัด (CountrySubDivisionID) (CountrySubDivisionID must be present since CountryID is TH.)");
                }
                if (dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration == null)
                {
                    errormessage.Add("TIV-BuyerTradeParty-001 ต้องระบุเลขประจำตัวผู้เสียภาษีอากรของผู้ซื้อ (ID must be present since BuyerTradeParty/SpecifiedTaxRegistration is present.)");
                }

                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id, ""))
                {
                    errormessage.Add("TIV-BuyerTradeParty-001 ต้องระบุเลขประจำตัวผู้เสียภาษีอากรของผู้ซื้อ (ID must be present since BuyerTradeParty/SpecifiedTaxRegistration is present)");
                }

                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeID, "TXID|NIDN|CCPT|OTHR"))
                {
                    errormessage.Add("TIV-BuyerTradeParty-002 ต้องระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ซื้อ (schemeID) และต้องมีค่าเป็น TXID หรือ NIDN หรือ CCPT หรือ OTHR เท่านั้น (schemeID must equal to TXID or NIDN or CCPT or OTHR.)");
                }

                if (!Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id, "") &&
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeID, "TXID") &&
                    dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id.Length != 18)
                {
                    errormessage.Add("TIV-BuyerTradeParty-003 กรณีระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ซื้อ (schemeID) เป็น TXID ต้องมีจำนวนตัวเลขเท่ากับ 18 หลัก (เลขประจำตัวผู้เสียภาษีอากร 13 หลัก และเลขสาขา 5 หลัก) (BuyerTradeParty/SpecifiedTaxRegistration/ID must length equal to 18 because schemeID is TXID.)");
                }

                if (!Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id, "") &&
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeID, "TXID") &&
                    dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id.Length == 18 &&
                    !Tool.CheckDataType(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id, TypeData.Interger))
                {
                    errormessage.Add("TIV-BuyerTradeParty-010 กรณีระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ซื้อ (schemeID) เป็น TXID เลขสาขาต้องเป็นตัวเลขเท่านั้น (BuyerTradeParty/SpecifiedTaxRegistration/ID must number only because schemeID is TXID.)");
                }

                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeID, "NIDN") &&
                    dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id.Length == 13 &&
                    !Tool.CheckDataType(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id, TypeData.Interger))
                {
                    errormessage.Add("TIV-BuyerTradeParty-004 กรณีระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ซื้อ (schemeID) เป็น NIDN ต้องมีจำนวนตัวเลขเท่ากับ 13 หลัก (เลขประจำตัวประชาชน 13 หลัก) (BuyerTradeParty/SpecifiedTaxRegistration/ID must length equal to 13 because schemeID is NIDN.)");
                }

                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeID, "CCPT") &&
                    dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id.Length > 35)
                {
                    errormessage.Add("TIV-BuyerTradeParty-005 กรณีระบุประภทเลขประจำตัวผู้เสียภาษีอากรของผู้ซื้อ (schemeID) เป็น CCPT ต้องมีจำนวนตัวอักษรไม่เกิน 35 ตัวอักษร (BuyerTradeParty/SpecifiedTaxRegistration/ID must length equal to 35 because schemeID is CCPT.)");
                }

                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeID, "OTHR") &&
                    !Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id, "N/A"))
                {
                    errormessage.Add("TIV-BuyerTradeParty-006 กรณีระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ซื้อ (schemeID) เป็น OTHR ต้องระบุค่าเป็น N/A (BuyerTradeParty/SpecifiedTaxRegistration/ID must be N/A because schemeID is OTHR.)");
                }

                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeID, "NIDN") &&
                    dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id.Length >= 13 &&
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id.Substring(0, 13), "0000000000000|1111111111111"))
                {
                    errormessage.Add("TIV-BuyerTradeParty-011 ระบุเลขประจำตัวผู้เสียภาษีอากรของผู้ซื้อไม่ถูกต้อง (BuyerTradeParty/SpecifiedTaxRegistration/ID incorrect)");
                }

                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeID, "TXID") &&
                    dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id.Length >= 13 &&
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id.Substring(0, 13), "0000000000000|1111111111111"))
                {
                    errormessage.Add("TIV-BuyerTradeParty-011 ระบุเลขประจำตัวผู้เสียภาษีอากรของผู้ซื้อไม่ถูกต้อง (BuyerTradeParty/SpecifiedTaxRegistration/ID incorrect)");
                }

                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.line1,"") &&
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.buildingNumber, "") &&
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.cityName, "") &&
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.citySubDivisionName, "") &&
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.countrySubDivisionID, ""))
                {
                    errormessage.Add("TIV-BuyerTradeParty-007 ต้องระบุที่อยู่ของผู้ซื้อ เป็นแบบมีโครงสร้าง (ประกอบด้วย บ้านเลขที่ ตำบล อำเภอ จังหวัด) หรือ แบบไม่มีโครงสร้าง (ประกอบด้วยข้อมูลที่อยู่บบรรทัดที่ 1 (LineOne) เป็นอย่างน้อย) (BuyerTradeParty/PostalTradeAddress must be specified in unstructured or structured format.)");
                }

                if ((Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.postcodeCode,"") ||
                    dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.postcodeCode.Length != 5) &&
                        Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.countryID, "TH"))
                {
                    errormessage.Add("TIV-BuyerTradeParty-008 กรณีระบุค่าของ CountryID เท่ากับ TH ต้องระบุรหัสไปรษณีย์ผู้ซื้อ จำนวนตัวเลขเท่ากับ 5 หลัก (PostcodeCode must be present since BuyerTradeParty/PostalAddress is present.)");
                }

                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.countryID,""))
                {
                    errormessage.Add("TIV-BuyerTradeParty-009 ต้องระบุรหัสประเทศของผู้ซื้อ (CountryID must be present since BuyerTradeParty/PostalAddress is present.)");
                }

                //if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.name,""))
                //{
                //    errormessage.Add("TIV-InvoicerTradeParty-001 ต้องระบุชื่อผู้ออกเอกสารแทน (Name must be present since InvoicerTradeParty is present.)");
                //}

                //if (dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration == null)
                //{
                //    errormessage.Add("TIV-InvoicerTradeParty-002 ต้องระบุข้อมูลเลขประจำตัวผู้เสียภาษีอากรของผู้ออกเอกสารแทน (SpecifiedTaxRegistration) (SpecifiedTaxRegistration must be present since InvoicerTradeParty is present.)");
                //}

                //if (dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.postalTradeAddress == null)
                //{
                //    errormessage.Add("TIV-InvoicerTradeParty-003 ต้องระบุที่อยู่ของผู้ออกเอกสารแทน (PostalTradeAddress must be present since InvoicerTradeParty is present.)");
                //}

                //if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.id, ""))
                //{
                //    errormessage.Add("TIV-InvoicerTradeParty-004 ต้องระบุข้อมูลเลขประจำตัวผู้เสียภาษีอากรของผู้ออกเอกสารแทน (SpecifiedTaxRegistration/ID) (ID must be present since InvoicerTradePart/SpecifiedTaxRegistration.)");
                //}

                //if (!Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.schemeID, "|TXID|NIDN"))
                //{
                //    errormessage.Add("TIV-InvoicerTradeParty-005 ต้องระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ออกเอกสารแทน (schemeID) และต้องมีค่าเป็น TXID หรือ NIDN เท่านั้น (schemeID must equal to TXID or NIDN.)");
                //}

                //if (!Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.schemeID, "|TXID") &&
                //    dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.id.Length != 18)
                //{
                //    errormessage.Add("TIV-InvoicerTradeParty-006 กรณีระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ออกเอกสารแทน (schemeID) เป็น TXID ต้องมีจำนวนตัวเลขเท่ากับ 18 หลัก (เลขประจำตัวผู้เสียภาษีอากร 13 หลัก และเลขสาขา 5 หลัก) (InvoicerTradeParty/SpecifiedTaxRegistration/ID must length equal to 18 since schemeID is TXID)");
                //}

                //if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.id, "") &&
                //    (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.schemeID, "|TXID") &&
                //    dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.id.Length == 18) &&
                //    !Tool.CheckDataType(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.id, TypeData.Interger))
                //{
                //    errormessage.Add("TIV-InvoicerTradeParty-014 กรณีระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ออกเอกสารแทน (schemeID) เป็น TXID เลขสาขาต้องเป็นตัวเลขเท่านั้น (InvoicerTradeParty/SpecifiedTaxRegistration/ID must number only because schemeID is TXID.)");
                //}

                //if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.schemeID, "NIDN") &&
                //    dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.id.Length != 13)
                //{
                //    errormessage.Add("TIV-InvoicerTradeParty-007 กรณีระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ออกเอกสารแทน (schemeID) เป็น NIDN ต้องมีจำนวนตัวเลขเท่ากับ 13 หลัก (เลขประจำตัวประชาชน 13 หลัก) (InvoicerTradeParty/SpecifiedTaxRegistration/ID must length equal to 13 since schemeID is NIDN.)");
                //}

                //if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.postalTradeAddress.postcodeCode,"") ||
                //    dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.postalTradeAddress.postcodeCode.Length != 5)
                //{
                //    errormessage.Add("TIV-InvoicerTradeParty-008 ต้องระบุรหัสไปรษณีย์ผู้ออกเอกสารแทน มีจำนวนตัวเลขเท่ากับ 5 หลัก (PostcodeCode must be present since InvoicerTradeParty/PostalAddress is present.)");
                //}

                //if (!Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.postalTradeAddress.countryID,"TH"))
                //{
                //    errormessage.Add("TIV-InvoicerTradeParty-009 ต้องระบุรหัสประเทศผู้ออกเอกสารแทน ต้องมีค่าเท่ากับ TH เท่านั้น (CountryID must be present since InvoicerTradeParty/PostalAddress is present.)");
                //}

                //if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.postalTradeAddress.buildingNumber, ""))
                //{
                //    errormessage.Add("TIV-InvoicerTradeParty-010 ต้องระบุ บ้านเลขที่ (BuildingNumber) (BuildingNumber must be present since CountryID is TH.)");
                //}

                //if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.postalTradeAddress.cityName, ""))
                //{
                //    errormessage.Add("TIV-InvoicerTradeParty-011 ต้องระบุ ชื่ออำเภอ(CityName)(CityName must be present since CountryID is TH.)");
                //}

                //if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.postalTradeAddress.citySubDivisionName, ""))
                //{
                //    errormessage.Add("TIV-InvoicerTradeParty-012 ต้องระบุ ชื่อตำบล (CitySubDivisionName) (CitySubDivisionName must be present since CountryID is TH.)");
                //}

                //if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.postalTradeAddress.countrySubDivisionID, ""))
                //{
                //    errormessage.Add("TIV-InvoicerTradeParty-013 ต้องระบุ รหัสจังหวัด (CountrySubDivisionID) (CountrySubDivisionID must be present since CountryID is TH.)");
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return errormessage;
        }

        

    }

}
