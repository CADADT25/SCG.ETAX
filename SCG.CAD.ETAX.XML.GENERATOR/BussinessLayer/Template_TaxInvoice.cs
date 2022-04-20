using SCG.CAD.ETAX.XML.GENERATOR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SCG.CAD.ETAX.XML.GENERATOR.BussinessLayer
{
    public class Template_TaxInvoice
    {
        public XDocument XMLtemplate(CrossIndustryInvoice data)
        {
            XDocument xmlDocument = new XDocument();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<rsm:TaxInvoice_CrossIndustryInvoice xmlns:rsm='urn:etda:uncefact:data:standard:TaxInvoice_CrossIndustryInvoice:2' ");
                sb.Append("xmlns:ram='urn:etda:uncefact:data:standard:TaxInvoice_ReusableAggregateBusinessInformationEntity:2' ");
                sb.Append("xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' ");
                sb.Append("xsi:schemaLocation='urn:etda:uncefact:data:standard:TaxInvoice_CrossIndustryInvoice:2 file:../data /standard/TaxInvoice_CrossIndustryInvoice_2p0.xsd'> ");
                sb.Append("<rsm:ExchangedDocumentContext>");
                sb.Append("<ram:GuidelineSpecifiedDocumentContextParameter>");
                sb.Append("<ram:ID schemeAgencyID='"+ data.exchangedDocumentContext.guidelineSpecifiedDocumentContextParameter.schemeAgencyID + "' schemeVersionID='" + data.exchangedDocumentContext.guidelineSpecifiedDocumentContextParameter.schemeVersionID + "'>" + data.exchangedDocumentContext.guidelineSpecifiedDocumentContextParameter.id + "</ram:ID>");
                sb.Append("</ram:GuidelineSpecifiedDocumentContextParameter>");
                sb.Append("</rsm:ExchangedDocumentContext>");
                sb.Append("<rsm:ExchangedDocument>");
                sb.Append("<ram:ID>"+ data.exchangedDocument.id + "</ram:ID>");
                sb.Append("<ram:Name>" + data.exchangedDocument.name + "</ram:Name>");
                sb.Append("<ram:TypeCode>" + data.exchangedDocument.typeCode + "</ram:TypeCode>");
                sb.Append("<ram:IssueDateTime>" + data.exchangedDocument.issueDateTime + "</ram:IssueDateTime>");
                sb.Append("<ram:CreationDateTime>" + data.exchangedDocument.createionDateTime + "</ram:CreationDateTime>");
                sb.Append("</rsm:ExchangedDocument>");
                sb.Append("<rsm:SupplyChainTradeTransaction>");
                sb.Append("<ram:ApplicableHeaderTradeAgreement>");
                sb.Append("<ram:SellerTradeParty>");
                sb.Append("<ram:Name>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.name + "</ram:Name>");
                sb.Append("<ram:SpecifiedTaxRegistration>");
                sb.Append("<ram:ID schemeAgencyID='" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeagencyid + "' schemeID='" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.schemeID + "'>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.specifiedTaxRegistration.id + "</ram:ID>");
                sb.Append("</ram:SpecifiedTaxRegistration>");
                sb.Append("<ram:PostalTradeAddress>");
                sb.Append("<ram:PostcodeCode>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.postcodeCode + "</ram:PostcodeCode>");
                sb.Append("<ram:StreetName>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.streetName + "</ram:StreetName>");
                sb.Append("<ram:CityName>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.cityName + "</ram:CityName>");
                sb.Append("<ram:CitySubDivisionName>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.citySubDivisionName + "</ram:CitySubDivisionName>");
                sb.Append("<ram:CountryID schemeID='3166-1 alpha-2'>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.countryID + "</ram:CountryID>");
                sb.Append("<ram:CountrySubDivisionID>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.countrySubDivisionID + "</ram:CountrySubDivisionID>");
                sb.Append("<ram:BuildingNumber>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.sellerTradeParty.postalTradeAddress.buildingNumber + "</ram:BuildingNumber>");
                sb.Append("</ram:PostalTradeAddress>");
                sb.Append("</ram:SellerTradeParty>");
                sb.Append("<ram:BuyerTradeParty>");
                sb.Append("<ram:Name>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.name + "</ram:Name>");
                sb.Append("<ram:SpecifiedTaxRegistration>");
                sb.Append("<ram:ID schemeAgencyID='" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeagencyid + "' schemeID='" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.schemeID + "'>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.specifiedTaxRegistration.id + "</ram:ID>");
                sb.Append("</ram:SpecifiedTaxRegistration>");
                sb.Append("<ram:PostalTradeAddress>");
                sb.Append("<ram:PostcodeCode>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.postcodeCode + "</ram:PostcodeCode>");
                sb.Append("<ram:LineOne>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.line1 + "</ram:LineOne>");
                sb.Append("<ram:CountryID schemeID='3166-1 alpha-2'>" + data.supplyChainTradeTransaction.applicableHeaderTradeAgreement.buyerTradeParty.postalTradeAddress.countryID + "</ram:CountryID>");
                sb.Append("</ram:PostalTradeAddress>");
                sb.Append("</ram:BuyerTradeParty>");
                sb.Append("</ram:ApplicableHeaderTradeAgreement>");
                sb.Append("<ram:ApplicableHeaderTradeDelivery/>");
                sb.Append("<ram:ApplicableHeaderTradeSettlement>");
                sb.Append("<ram:InvoiceCurrencyCode listID='" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoiceCurrencyCode.listID + "'>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.invoiceCurrencyCode.invoiceCurrencyCode + "</ram:InvoiceCurrencyCode>");
                sb.Append("<ram:ApplicableTradeTax>");
                sb.Append("<ram:TypeCode>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.typeCode + "</ram:TypeCode>");
                sb.Append("<ram:CalculatedRate>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.calculatedRate + "</ram:CalculatedRate>");
                sb.Append("<ram:BasisAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.basisAmount + "</ram:BasisAmount>");
                sb.Append("<ram:CalculatedAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.applicableTradeTax.calculatedAmount + "</ram:CalculatedAmount>");
                sb.Append("</ram:ApplicableTradeTax>");
                sb.Append("<ram:SpecifiedTradeSettlementHeaderMonetarySummation>");
                sb.Append("<ram:LineTotalAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.lineTotalAmount + "</ram:LineTotalAmount>");
                sb.Append("<ram:AllowanceTotalAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.allowanceTotalAmount + "</ram:AllowanceTotalAmount>");
                sb.Append("<ram:ChargeTotalAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.chargeTotalAmount + "</ram:ChargeTotalAmount>");
                sb.Append("<ram:TaxBasisTotalAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.taxBasisTotalAmount + "</ram:TaxBasisTotalAmount>");
                sb.Append("<ram:TaxTotalAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.taxTotalAmount + "</ram:TaxTotalAmount>");
                sb.Append("<ram:GrandTotalAmount>" + data.supplyChainTradeTransaction.applicableHeaderTradeSettlement.specifiedTradeSettlementHeaderMonetarySummation.grandTotalAmount + "</ram:GrandTotalAmount>");
                sb.Append("</ram:SpecifiedTradeSettlementHeaderMonetarySummation>");
                sb.Append("</ram:ApplicableHeaderTradeSettlement>");
                sb.Append("<ram:IncludedSupplyChainTradeLineItem>");
                sb.Append("<ram:AssociatedDocumentLineDocument>");
                sb.Append("<ram:LineID>" + data.supplyChainTradeTransaction.includedSupplyChainTradeLineItem.associatedDocumentLineDocument.lineID + "</ram:LineID>");
                sb.Append("</ram:AssociatedDocumentLineDocument>");
                sb.Append("<ram:SpecifiedTradeProduct>");
                sb.Append("<ram:Name>" + data.supplyChainTradeTransaction.includedSupplyChainTradeLineItem.specifiedTradeProduct.name + "</ram:Name>");
                if(data.supplyChainTradeTransaction.includedSupplyChainTradeLineItem.specifiedTradeProduct.informationNote != null)
                {
                    sb.Append("<ram:InformationNote>");
                    sb.Append("<ram:Subject>" + data.supplyChainTradeTransaction.includedSupplyChainTradeLineItem.specifiedTradeProduct.informationNote.subject + "</ram:Subject>");
                    sb.Append("</ram:InformationNote>");
                }
                sb.Append("</ram:SpecifiedTradeProduct>");
                sb.Append("<ram:SpecifiedLineTradeAgreement>");
                sb.Append("<ram:GrossPriceProductTradePrice>");
                sb.Append("<ram:ChargeAmount>" + data.supplyChainTradeTransaction.includedSupplyChainTradeLineItem.specifiedLineTradeAgreement.grossPriceProductTradePrice.chargeAmount + "</ram:ChargeAmount>");
                sb.Append("</ram:GrossPriceProductTradePrice>");
                sb.Append("</ram:SpecifiedLineTradeAgreement>");
                sb.Append("<ram:SpecifiedLineTradeDelivery>");
                sb.Append("<ram:BilledQuantity unitCode='" + data.supplyChainTradeTransaction.includedSupplyChainTradeLineItem.specifiedLineTradeDelivery.billedQuantity.unitCode + "'>" + data.supplyChainTradeTransaction.includedSupplyChainTradeLineItem.specifiedLineTradeDelivery.billedQuantity.billedQuantity + "</ram:BilledQuantity>");
                sb.Append("</ram:SpecifiedLineTradeDelivery>");
                sb.Append("<ram:SpecifiedLineTradeSettlement>");
                sb.Append("<ram:SpecifiedTradeAllowanceCharge>");
                sb.Append("<ram:ChargeIndicator>" + data.supplyChainTradeTransaction.includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge[0].chargeIndicator + "</ram:ChargeIndicator>");
                sb.Append("<ram:ActualAmount>" + data.supplyChainTradeTransaction.includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge[0].actualAmount + "0</ram:ActualAmount>");
                sb.Append("</ram:SpecifiedTradeAllowanceCharge>");
                sb.Append("<ram:SpecifiedTradeAllowanceCharge>");
                sb.Append("<ram:ChargeIndicator>" + data.supplyChainTradeTransaction.includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge[0].chargeIndicator + "</ram:ChargeIndicator>");
                sb.Append("<ram:ActualAmount>" + data.supplyChainTradeTransaction.includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeAllowanceCharge[0].actualAmount + "</ram:ActualAmount>");
                sb.Append("</ram:SpecifiedTradeAllowanceCharge>");
                sb.Append("<ram:SpecifiedTradeSettlementLineMonetarySummation>");
                sb.Append("<ram:TaxTotalAmount>" + data.supplyChainTradeTransaction.includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.taxTotalAmount + "</ram:TaxTotalAmount>");
                sb.Append("<ram:NetLineTotalAmount currencyID='" + data.supplyChainTradeTransaction.includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.netIncludingTaxesLineTotalAmount.currencyID + "'>" + data.supplyChainTradeTransaction.includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.NetLineTotalAmount + "</ram:NetLineTotalAmount>");
                sb.Append("<ram:NetIncludingTaxesLineTotalAmount currencyID='" + data.supplyChainTradeTransaction.includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.netIncludingTaxesLineTotalAmount.currencyID + "'>" + data.supplyChainTradeTransaction.includedSupplyChainTradeLineItem.specifiedLineTradeSettlement.specifiedTradeSettlementLineMonetarySummation.netIncludingTaxesLineTotalAmount.netIncludingTaxesLineTotalAmount + "</ram:NetIncludingTaxesLineTotalAmount>");
                sb.Append("</ram:SpecifiedTradeSettlementLineMonetarySummation>");
                sb.Append("</ram:SpecifiedLineTradeSettlement>");
                sb.Append("</ram:IncludedSupplyChainTradeLineItem>");
                sb.Append("</rsm:SupplyChainTradeTransaction>");
                sb.Append("</rsm:TaxInvoice_CrossIndustryInvoice>");

                TextReader textReader = new StringReader(sb.ToString());
                xmlDocument = XDocument.Load(textReader);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return xmlDocument;
        }
    }

}
