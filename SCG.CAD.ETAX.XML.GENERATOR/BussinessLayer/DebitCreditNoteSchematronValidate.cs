using SCG.CAD.ETAX.XML.GENERATOR.Models;

namespace SCG.CAD.ETAX.XML.GENERATOR.BussinessLayer
{
    public class DebitCreditNoteSchematronValidate
    {
        LogicTool Tool = new LogicTool();
        public List<string> DebitCreditNoteChackSchematron(CrossIndustryInvoice dataXml)
        {
            List<string> errormessage = new List<string>();
            try
            {
                if (!Tool.CheckDataRule(dataXml.exchangedDocument.typeCode, "80|81"))
                {
                    errormessage.Add("DCN-Document-001 รหัสประเภทเอกสารไม่ถูกต้อง (80 = ใบเพิ่มหนี้,81 = ใบลดหนี้) (TypeCode must equal to 80 or 81.)");
                }

                if (Tool.CheckDataRule(dataXml.exchangedDocument.purposeCode, ""))
                {
                    errormessage.Add("DCN-Document-004 ต้องระบุข้อมูลรหัสสาเหตุการออกเอกสาร (PurposeCode) (PurposeCode must be present.)");
                }

                if (Tool.CheckDataRule(dataXml.exchangedDocument.typeCode, "80") &&
                    !Tool.CheckDataRule(dataXml.exchangedDocument.purposeCode, "DBNG01|DBNG02|DBNG99|DBNS01|DBNS02|DBNS02|DBNS99"))
                {
                    errormessage.Add("DCN-Document-005 กรณีใบเพิ่มหนี้ การระบุรหัสสาเหตุการออกเอกสาร (PurposeCode) ต้องมีค่าเท่ากับ DBNG01 หรือ DBNG02 หรือ DBNG99 หรือ DBNS01 หรือ DBNS02 หรือ DBNS99 เท่านั้น (In case of debit note, PurposeCode must equal to DBNG01, DBNG02, DBNG99, DBNS01, DBNS02, DBNS99)");
                }

                if (Tool.CheckDataRule(dataXml.exchangedDocument.typeCode, "81") &&
                    !Tool.CheckDataRule(dataXml.exchangedDocument.purposeCode, "CDNG01|CDNG02|CDNG03|CDNG04|CDNG05|CDNG99|CDNS01|CDNS02|CDNS03|CDNS04|CDNS99"))
                {
                    errormessage.Add("DCN-Document-006 กรณีใบลดหนี้ การระบุรหัสสาเหตุการออกเอกสาร (PurposeCode) ต้องมีค่าเท่ากับ CDNG01 หรือ CDNG02 หรือ CDNG03 หรือ CDNG04 หรือ CDNG05 หรือ CDNG99 หรือ CDNS01 หรือ CDNS02 หรือ CDNS03 หรือ CDNS04 หรือ CDNS99 เท่านั้น (In case of credit note, PurposeCode must equal to  CDNG01, CDNG02, CDNG03, CDNG04, CDNG05, CDNG99, CDNS01, CDNS02, CDNS03, CDNS04, CDNS99)");
                }

                if (!Tool.CheckDataRule(dataXml.exchangedDocument.purposeCode, "DBNG99|DBNS99|CDNG99") &&
                    Tool.CheckDataRule(dataXml.exchangedDocument.purpose, ""))
                {
                    errormessage.Add("DCN-Document-007 กรณีระบุรหัสสาเหตุการออกเอกสาร (PurposeCode) มีค่าเท่ากับ DBNG99 หรือ DBNS99 หรือ CDNG99 หรือ CDNS99 ต้องระบุสาเหตุการออกเอกสาร (Purpose) (Purpose must be present since PurposeCode equals to DBNG99, DBNS99, CDNG99, CDNS99)");
                }

                //if (dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id.Length >= 13 &&
                //    dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.id.Length >= 13 &&
                //    dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id.Substring(0,13) == dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.id.Substring(0,13))
                //{
                //    errormessage.Add("DCN-Document-008 เลขประจำตัวผู้เสียภาษีอากรของผู้ออกเอกสารแทน ต้องไม่เท่ากับเลขประจำตัวผู้เสียภาษีอากรของผู้ขาย (InvoicerTradeParty/SpecifiedTaxRegistration/ID must not equal to SellerTradeParty/SpecifiedTaxRegistration/ID)");
                //}

                if(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.additionalReferencedDocument != null)
                {
                    if (!Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.additionalReferencedDocument.referenceTypeCode, "388|T02|T03|T04|80|81"))
                    {
                        errormessage.Add("DCN-AdditionalReferencedDocument-001 ต้องระบุรหัสเอกสารอ้างอิง (AdditionalReferencedDocument) ที่มีค่าเป็น 80 หรือ 81 หรือ  388 หรือ T02 หรือ T03 หรือ T04  อย่างน้อย 1 เอกสาร (AdditionalReferencedDocument must be equal to 80 or 81 or 388 or T02 or T03 or T04 since PurposeCode is present.)");
                    }
                }

                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.name, ""))
                {
                    errormessage.Add("DCN-SellerTradeParty-001 ต้องระบุชื่อผู้ขาย (Name must be present since SellerTradeParty is present.)");
                }

                if (dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration == null)
                {
                    errormessage.Add("DCN-SellerTradeParty-002  ต้องระบุข้อมูลเลขประจำตัวผู้เสียภาษีอากรของผู้ขาย (SpecifiedTaxRegistration) (SpecifiedTaxRegistration must be present since SellerTradeParty is present.)");
                }

                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id, ""))
                {
                    errormessage.Add("DCN-SellerTradeParty-003 ต้องระบุข้อมูลเลขประจำตัวผู้เสียภาษีอากรของผู้ขาย (SpecifiedTaxRegistration/ID) (ID must be present since SellerTradePart/SpecifiedTaxRegistration)");
                }

                if (!Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeID, "TXID|NIDN"))
                {
                    errormessage.Add("DCN-SellerTradeParty-004 ต้องระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ขาย (schemeID)  และต้องมีค่าเป็น TXID หรือ NIDN เท่านั้น (schemeID must equal to TXID or NIDN..)");
                }

                if (!Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id, "") &&
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeID, "TXID") &&
                    dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id.Length != 18)
                {
                    errormessage.Add("DCN-SellerTradeParty-005 กรณีระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ขาย (schemeID) เป็น TXID ต้องมีจำนวนตัวเลขเท่ากับ 18 หลัก (เลขประจำตัวผู้เสียภาษีอากร 13 หลัก และเลขสาขา 5 หลัก) SellerTradeParty/SpecifiedTaxRegistration/ID must length equal to 18 since schemeID is TXID");
                }

                if (!Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id, "") &&
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeID, "TXID") &&
                    dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id.Length == 18 &&
                    !Tool.CheckDataType(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id,TypeData.Interger))
                {
                    errormessage.Add("DCN-SellerTradeParty-013 กรณีระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ขาย (schemeID) เป็น TXID เลขสาขาต้องเป็นตัวเลขเท่านั้น (SellerTradeParty/SpecifiedTaxRegistration/ID must number only because schemeID is TXID.)");
                }

                if (!Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id, "") &&
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeID, "NIDN") &&
                    dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id.Length == 13 &&
                    !Tool.CheckDataType(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id, TypeData.Interger))
                {
                    errormessage.Add("DCN-SellerTradeParty-006 กรณีระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ขาย (schemeID) เป็น NIDN ต้องมีจำนวนตัวเลขเท่ากับ 13 หลัก (เลขประจำตัวประชาชน 13 หลัก) (SellerTradeParty/SpecifiedTaxRegistration/ID must length equal to 13 because schemeID is NIDN)");
                }

                if (!Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id, "") &&
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeID, "NIDN") &&
                    dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id.Length >= 13 &&
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id.Substring(0,13), "0000000000000|1111111111111"))
                {
                    errormessage.Add("DCN-SellerTradeParty-014 ระบุเลขประจำตัวผู้เสียภาษีอากรของผู้ขายไม่ถูกต้อง (SellerTradeParty/SpecifiedTaxRegistration/ID incorrect)");
                }

                if (!Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id, "") &&
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeID, "TXID") &&
                    dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id.Length >= 13 &&
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id.Substring(0, 13), "0000000000000|1111111111111"))
                {
                    errormessage.Add("DCN-SellerTradeParty-014 ระบุเลขประจำตัวผู้เสียภาษีอากรของผู้ขายไม่ถูกต้อง (SellerTradeParty/SpecifiedTaxRegistration/ID incorrect)");
                }

                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.postcodeCode, "") ||
                    dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.postcodeCode.Length != 5)
                {
                    errormessage.Add("DCN-SellerTradeParty-007 ต้องระบุรหัสไปรษณีย์ผู้ขาย มีจำนวนตัวเลขเท่ากับ 5 หลัก (PostcodeCode must be present since SellerTradeParty/PostalAddress is present.)");
                }

                if (!Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.countryID, "TH"))
                {
                    errormessage.Add("DCN-SellerTradeParty-008 ต้องระบุรหัสประเทศผู้ขาย ต้องมีค่าเท่ากับ TH เท่านั้น (CountryID must be present since SellerTradeParty/PostalAddress is present.)");
                }

                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.buildingNumber, ""))
                {
                    errormessage.Add("DCN-SellerTradeParty-009 ต้องระบุ บ้านเลขที่ (BuildingNumber) (BuildingNumber must be present since CountryID is TH.)");
                }

                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.cityName, ""))
                {
                    errormessage.Add("DCN-SellerTradeParty-010 ต้องระบุ ชื่ออำเภอ (CityName) (CityName must be present since CountryID is TH.)");
                }

                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.citySubDivisionName, ""))
                {
                    errormessage.Add("DCN-SellerTradeParty-011 ต้องระบุ ชื่อตำบล (CitySubDivisionName) (CitySubDivisionName must be present since CountryID is TH.)");
                }

                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.countrySubDivisionID, ""))
                {
                    errormessage.Add("DCN-SellerTradeParty-012 ต้องระบุ รหัสจังหวัด (CountrySubDivisionID) (CountrySubDivisionID must be present since CountryID is TH.)");
                }

                if (dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration == null)
                {
                    errormessage.Add("DCN-BuyerTradeParty-001 ต้องระบุเลขประจำตัวผู้เสียภาษีอากรของผู้ซื้อ (SpecifiedTaxRegistration must be present since BuyerTradeParty is present.)");
                }

                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id, ""))
                {
                    errormessage.Add("DCN-BuyerTradeParty-001 ต้องระบุเลขประจำตัวผู้เสียภาษีอากรของผู้ซื้อ (ID must be present because BuyerTradeParty/SpecifiedTaxRegistration is present.)");
                }

                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeID, "TXID|NIDN|CCPT|OTHR"))
                {
                    errormessage.Add("DCN-BuyerTradeParty-002 ต้องระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ซื้อ (schemeID)  และต้องมีค่าเป็น TXID หรือ NIDN หรือ CCPT หรือ OTHR เท่านั้น (schemeID must equal to TXID or NIDN or CCPT or OTHR.)");
                }

                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeID, "TXID") &&
                    dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id.Length != 18)
                {
                    errormessage.Add("DCN-BuyerTradeParty-003 กรณีระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ซื้อ (schemeID) เป็น TXID ต้องมีจำนวนตัวเลขเท่ากับ 18 หลัก (เลขประจำตัวผู้เสียภาษีอากร 13 หลัก และเลขสาขา 5 หลัก) (BuyerTradeParty/SpecifiedTaxRegistration/ID must length equal to 18 since schemeID is TXID.)");
                }

                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeID, "NIDN") &&
                    dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id.Length == 13 &&
                    !Tool.CheckDataType(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id,TypeData.Interger))
                {
                    errormessage.Add("DCN-BuyerTradeParty-004 กรณีระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ซื้อ (schemeID) เป็น NIDN ต้องมีจำนวนตัวเลขเท่ากับ 13 หลัก (เลขประจำตัวประชาชน 13 หลัก) (BuyerTradeParty/SpecifiedTaxRegistration/ID must length equal to 13 since schemeID is NIDN)");
                }

                if (!Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id, "") &&
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeID, "TXID") &&
                    dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id.Length == 18 &&
                    !Tool.CheckDataType(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id, TypeData.Interger))
                {
                    errormessage.Add("DCN-BuyerTradeParty-010 กรณีระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ซื้อ (schemeID) เป็น TXID เลขสาขาต้องเป็นตัวเลขเท่านั้น (BuyerTradeParty/SpecifiedTaxRegistration/ID must number only because schemeID is TXID.)");
                }

                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeID, "CCPT") &&
                    dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id.Length > 35)
                {
                    errormessage.Add("DCN-BuyerTradeParty-005  กรณีระบุประภทเลขประจำตัวผู้เสียภาษีอากรของผู้ซื้อ (schemeID) เป็น CCPT ต้องมีจำนวนตัวอักษรไม่เกิน 35 ตัวอักษร (BuyerTradeParty/SpecifiedTaxRegistration/ID must length equal to 35 since schemeID is CCPT)");
                }

                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeID, "OTHR") &&
                    !Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id, "N/A")
                    )
                {
                    errormessage.Add("DCN-BuyerTradeParty-006 กรณีระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ซื้อ (schemeID) เป็น OTHR ต้องระบุค่าเป็น N/A (BuyerTradeParty/SpecifiedTaxRegistration/ID must be N/A since schemeID is OTHR)");
                }

                if (!Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id, "") &&
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeID, "NIDN") &&
                    dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id.Length >= 13 &&
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id.Substring(0, 13), "0000000000000|1111111111111"))
                {
                    errormessage.Add("DCN-BuyerTradeParty-011 ระบุเลขประจำตัวผู้เสียภาษีอากรของผู้ซื้อไม่ถูกต้อง (BuyerTradeParty/SpecifiedTaxRegistration/ID incorrect)");
                }

                if (!Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id, "") &&
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeID, "TXID") &&
                    dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id.Length >= 13 &&
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id.Substring(0, 13), "0000000000000|1111111111111"))
                {
                    errormessage.Add("DCN-BuyerTradeParty-011 ระบุเลขประจำตัวผู้เสียภาษีอากรของผู้ซื้อไม่ถูกต้อง (BuyerTradeParty/SpecifiedTaxRegistration/ID incorrect)");
                }

                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.line1, "") ||
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.countrySubDivisionID, "") ||
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.buildingNumber, "") ||
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.cityName, "") ||
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.citySubDivisionName, ""))
                {
                    errormessage.Add("DCN-BuyerTradeParty-007 ต้องระบุที่อยู่ของผู้ซื้อ เป็นแบบมีโครงสร้าง (ประกอบด้วย บ้านเลขที่ ตำบล อำเภอ จังหวัด) หรือ แบบไม่มีโครงสร้าง (ประกอบด้วยข้อมูล ที่อยู่บบรรทัดที่ 1 (LineOne) เป็นอย่างน้อย) (BuyerTradeParty/PostalTradeAddress must be specified in unstructured  or structured format.)");
                }

                if ((Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.postcodeCode, "") ||
                    dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.postcodeCode.Length != 5) &&
                    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.countryID,"TH")
                    )
                {
                    errormessage.Add("DCN-BuyerTradeParty-008 กรณีระบุค่าของ CountryID เท่ากับ TH ต้องระบุรหัสไปรษณีย์ผู้ซื้อ จำนวนตัวเลขเท่ากับ 5 หลัก (PostcodeCode must be present since BuyerTradeParty/PostalAddress is present.)");
                }

                if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.countryID, ""))
                {
                    errormessage.Add("DCN-BuyerTradeParty-009 ต้องระบุรหัสประเทศของผู้ซื้อ (CountryID must be present since BuyerTradeParty/PostalAddress is present.)");
                }

                //if (!Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.name, ""))
                //{
                //    errormessage.Add("DCN-InvoicerTradeParty-001 ต้องระบุชื่อผู้ออกเอกสาร (Name must be present since InvoicerTradeParty is present.)");
                //}

                //if (dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration == null)
                //{
                //    errormessage.Add("DCN-InvoicerTradeParty-002 ต้องระบุเลขประจำตัวผู้เสียภาษีของผู้ออกเอกสาร (SpecifiedTaxRegistration must be present since  InvoicerTradeParty is present.)");
                //}

                //if (dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.postalTradeAddress == null)
                //{
                //    errormessage.Add("DCN-InvoicerTradeParty-003 ต้องระบุที่อยู่ผู้ออกเอกสาร (PostalTradeAddress must be present since InvoicerTradeParty is present.)");
                //}

                //if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.id, ""))
                //{
                //    errormessage.Add("DCN-InvoicerTradeParty-004 ต้องระบุข้อมูลเลขประจำตัวผู้เสียภาษีอากรของผู้ออกเอกสารแทน (SpecifiedTaxRegistration/ID) (ID must be present since InvoicerTradePart/SpecifiedTaxRegistration)");
                //}

                //if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.schemeID, "TXID|NIDN"))
                //{
                //    errormessage.Add("DCN-InvoicerTradeParty-005 ต้องระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ออกเอกสารแทน (schemeID)  และต้องมีค่าเป็น TXID หรือ NIDN เท่านั้น (schemeID must equal to TXID or NIDN.)");
                //}

                //if (!Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.id, "") &&
                //    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.schemeID, "TXID") &&
                //    dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.id.Length != 18)
                //{
                //    errormessage.Add("DCN-InvoicerTradeParty-006  กรณีระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ออกเอกสารแทน (schemeID) เป็น TXID ต้องมีจำนวนตัวเลขเท่ากับ 18 หลัก (เลขประจำตัวผู้เสียภาษีอากร 13 หลัก และเลขสาขา 5 หลัก) (InvoicerTradeParty/SpecifiedTaxRegistration/ID must length equal to 18 since schemeID is TXID)");
                //}

                //if (!Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.id, "") &&
                //    Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.schemeID, "TXID") &&
                //    dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.id.Length == 18 &&
                //    !Tool.CheckDataType(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.id, TypeData.Interger))
                //{
                //    errormessage.Add("DCN-InvoicerTradeParty-014 กรณีระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ออกเอกสารแทน (schemeID) เป็น TXID เลขสาขาต้องเป็นตัวเลขเท่านั้น (InvoicerTradeParty/SpecifiedTaxRegistration/ID must number only because schemeID is TXID.)");
                //}

                //if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.schemeID, "NIDN") &&
                //    dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.specifiedTaxRegistration.id.Length != 13)
                //{
                //    errormessage.Add("DCN-InvoicerTradeParty-007 กรณีระบุประเภทเลขประจำตัวผู้เสียภาษีอากรของผู้ออกเอกสารแทน (schemeID) เป็น NIDN ต้องมีจำนวนตัวเลขเท่ากับ 13 หลัก (เลขประจำตัวประชาชน 13 หลัก) (InvoicerTradeParty/SpecifiedTaxRegistration/ID must length equal to 13 since schemeID is NIDN.)");
                //}

                //if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.postalTradeAddress.postcodeCode, "") ||
                //    dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.postalTradeAddress.postcodeCode.Length != 5)
                //{
                //    errormessage.Add("DCN-InvoicerTradeParty-008 ต้องระบุรหัสไปรษณีย์ผู้ออกเอกสารแทน มีจำนวนตัวเลขเท่ากับ 5 หลัก (PostcodeCode must be present since InvoicerTradeParty/PostalAddress is present.)");
                //}

                //if (!Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.postalTradeAddress.countryID, "TH"))
                //{
                //    errormessage.Add("DCN-InvoicerTradeParty-009 ต้องระบุรหัสประเทศผู้ออกเอกสารแทน ต้องมีค่าเท่ากับ TH เท่านั้น (CountryID must be present since InvoicerTradeParty/PostalAddress is present.)");
                //}

                //if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.postalTradeAddress.buildingNumber, ""))
                //{
                //    errormessage.Add("DCN-InvoicerTradeParty-010 ต้องระบุ บ้านเลขที่ (BuildingNumber) (BuildingNumber must be present since CountryID is TH.)");
                //}

                //if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.postalTradeAddress.cityName, ""))
                //{
                //    errormessage.Add("DCN-InvoicerTradeParty-011 ต้องระบุ ชื่ออำเภอ (CityName) (CityName must be present since CountryID is TH.)");
                //}

                //if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.postalTradeAddress.citySubDivisionName, ""))
                //{
                //    errormessage.Add("DCN-InvoicerTradeParty-012 ต้องระบุ ชื่อตำบล (CitySubDivisionName) (CitySubDivisionName must be present since CountryID is TH.)");
                //}

                //if (Tool.CheckDataRule(dataXml.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoicerTradeParty.postalTradeAddress.countrySubDivisionID, ""))
                //{
                //    errormessage.Add("DCN-InvoicerTradeParty-013 ต้องระบุ รหัสจังหวัด (CountrySubDivisionID) (CountrySubDivisionID must be present since CountryID is TH.)");
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
