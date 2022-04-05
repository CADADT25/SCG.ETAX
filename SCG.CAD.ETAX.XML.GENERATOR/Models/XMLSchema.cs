using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.XML.GENERATOR.Models
{
    public class CrossIndustryInvoice
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
        public string id { get; set; }
        public string schemeAgencyID { get; set; }
        public string schemeVersionID { get; set; }
    }
    public class ExchangedDocument
    {
        public string id { get; set; }
        public string name { get; set; }
        public string typeCode { get; set; }
        public string issueDateTime { get; set; }
        public string purpose { get; set; }
        public string purposeCode { get; set; }
        public string globalID { get; set; }
        public string createionDateTime { get; set; }
        public IncludedNote includedNote { get; set; }
    }

    public class IncludedNote
    {
        public string subject { get; set; }
        public string content { get; set; }
    }

    public class SupplyChainTradeTransaction
    {
        public ApplicableHeaderTradeAgreement applicableHeaderTradeAgreement { get; set; }
        public ApplicableHeaderTradeDelivery applicableHeaderTradeDelivery { get; set; }
        public ApplicableHeaderTradeSettlement applicableHeaderTradeSettlement { get; set; }
        public IncludedSupplyChainTradeLineItem includedSupplyChainTradeLineItem { get; set; }
    }

    public class ApplicableHeaderTradeDelivery
    {
        public ShipToTradeParty shipToTradeParty { get; set; }
        public ShipFromTradeParty shipFromTradeParty { get; set; }
        public ActualDeliverySupplyChain actualDeliverySupplyChain { get; set; }
    }
    public class ShipToTradeParty
    {
        public string id { get; set; }
        public string globalId { get; set; }
        public string name { get; set; }
        public DefinedTradeContact definedTradeContact { get; set; }
        public PostalTradeAddress postalTradeAddress { get; set; }
    }
    public class ShipFromTradeParty
    {
        public string id { get; set; }
        public string globalId { get; set; }
        public string name { get; set; }
        public DefinedTradeContact definedTradeContact { get; set; }
        public PostalTradeAddress postalTradeAddress { get; set; }
    }
    public class ActualDeliverySupplyChain
    {
        public string occurrenceDateTime { get; set; }
    }
    public class ApplicableHeaderTradeAgreement
    {
        public SellerTradeParty sellerTradeParty { get; set; }
        public BuyerTradeParty buyerTradeParty { get; set; }
        public ApplicableTradeDeliveryTerms applicableTradeDeliveryTerms { get; set; }
        public BuyerOrderReferencedDocument buyerOrderReferencedDocument { get; set; }
        public AdditionalReferencedDocument additionalReferencedDocument { get; set; }
    }

    public class SellerTradeParty
    {
        public string id { get; set; }
        public string globalid { get; set; }
        public string name { get; set; }
        public SpecifiedTaxRegistration specifiedTaxRegistration { get; set; }
        public DefinedTradeContact definedTradeContact { get; set; }
        public PostalTradeAddress postalTradeAddress { get; set; }
    }
    public class SpecifiedTaxRegistration
    {
        public string id { get; set; }
        public string schemeagencyid { get; set; }
        public string schemeID { get; set; }
    }
    public class PostalTradeAddress
    {
        public string postcodeCode { get; set; }
        public string buildingName { get; set; }
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string line3 { get; set; }
        public string line4 { get; set; }
        public string line5 { get; set; }
        public string streetName { get; set; }
        public string cityName { get; set; }
        public string citySubDivisionName { get; set; }
        public string countryID { get; set; }
        public string countrySubDivisionID { get; set; }
        public string buildingNumber { get; set; }
    }
    public class BuyerTradeParty
    {
        public string id { get; set; }
        public string globalId { get; set; }
        public string name { get; set; }
        public SpecifiedTaxRegistration specifiedTaxRegistration { get; set; }
        public DefinedTradeContact definedTradeContact { get; set; }
        public PostalTradeAddress postalTradeAddress { get; set; }
    }
    public class ApplicableTradeDeliveryTerms
    {
        public string deliveryTypeCode { get; set; }
    }
    public class BuyerOrderReferencedDocument
    {
        public string issuerAssignedID { get; set; }
        public string issueDateTime { get; set; }
        public string referenceTypeCode { get; set; }
    }
    public class AdditionalReferencedDocument
    {
        public string issuerAssignedID { get; set; }
        public string issueDateTime { get; set; }
        public string referenceTypeCode { get; set; }
    }

    public class ApplicableHeaderTradeSettlement
    {
        public InvoiceCurrencyCode invoiceCurrencyCode { get; set; }
        public ApplicableTradeTax applicableTradeTax { get; set; }
        public SpecifiedTradeAllowanceCharge specifiedTradeAllowanceCharge { get; set; }
        public SpecifiedTradePaymentTerms specifiedTradePaymentTerms { get; set; }
        public SpecifiedTradeSettlementHeaderMonetarySummation specifiedTradeSettlementHeaderMonetarySummation { get; set; }
        public InvoicerTradeParty invoicerTradeParty { get; set; }
        public InvoiceeTradeParty invoiceeTradeParty { get; set; }
        public PayerTradeParty payerTradeParty { get; set; }
        public PayeeTradeParty payeeTradeParty { get; set; }
        //public DefinedTradeContact definedTradeContact { get; set; }
        //public EmailURIUniversalCommunication emailURIUniversalCommunication { get; set; }
        //public TelephoneUniversalCommunication telephoneUniversalCommunication { get; set; }
        //public PostalTradeAddress postalTradeAddress  { get; set; }
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
    public class SpecifiedTradePaymentTerms
    {
        public string description { get; set; }
        public string dueDateTime { get; set; }
        public string typecode { get; set; }
    }
    public class SpecifiedTradeSettlementHeaderMonetarySummation
    {
        public string originalInformationAmount { get; set; }
        public string lineTotalAmount { get; set; }
        public string differenceSalesInformationAmount { get; set; }
        public string allowanceTotalAmount { get; set; }
        public string chargeTotalAmount { get; set; }
        public string taxBasisTotalAmount { get; set; }
        public string taxTotalAmount { get; set; }
        public string grandTotalAmount { get; set; }
    }
    public class InvoicerTradeParty
    {
        public string id { get; set; }
        public string globalID { get; set; }
        public string name { get; set; }
        public SpecifiedTaxRegistration specifiedTaxRegistration { get; set; }
        public DefinedTradeContact definedTradeContact { get; set; }
        public EmailURIUniversalCommunication emailURIUniversalCommunication { get; set; }
        public TelephoneUniversalCommunication telephoneUniversalCommunication { get; set; }
        public PostalTradeAddress postalTradeAddress { get; set; }
    }
    public class InvoiceeTradeParty
    {
        public string id { get; set; }
        public string globalID { get; set; }
        public string name { get; set; }
        public SpecifiedTaxRegistration specifiedTaxRegistration { get; set; }
        public DefinedTradeContact definedTradeContact { get; set; }
        public EmailURIUniversalCommunication emailURIUniversalCommunication { get; set; }
        public TelephoneUniversalCommunication telephoneUniversalCommunication { get; set; }
        public PostalTradeAddress postalTradeAddress { get; set; }
    }
    public class PayerTradeParty
    {
        public string id { get; set; }
        public string globalID { get; set; }
        public string name { get; set; }
        public SpecifiedTaxRegistration specifiedTaxRegistration { get; set; }
        public DefinedTradeContact definedTradeContact { get; set; }
        public EmailURIUniversalCommunication emailURIUniversalCommunication { get; set; }
        public TelephoneUniversalCommunication telephoneUniversalCommunication { get; set; }
        public PostalTradeAddress postalTradeAddress { get; set; }
    }
    public class PayeeTradeParty
    {
        public string id { get; set; }
        public string globalID { get; set; }
        public string name { get; set; }
        public SpecifiedTaxRegistration specifiedTaxRegistration { get; set; }
        public DefinedTradeContact definedTradeContact { get; set; }
        public EmailURIUniversalCommunication emailURIUniversalCommunication { get; set; }
        public TelephoneUniversalCommunication telephoneUniversalCommunication { get; set; }
        public PostalTradeAddress postalTradeAddress { get; set; }
    }
    public class DefinedTradeContact
    {
        public string personName { get; set; }
        public string departmentName { get; set; }
        public EmailURIUniversalCommunication emailURIUniversalCommunication { get; set; }
        public TelephoneUniversalCommunication telephoneUniversalCommunication { get; set; }
    }
    public class EmailURIUniversalCommunication
    {
        public string uRIID { get; set; }
    }
    public class TelephoneUniversalCommunication
    {
        public string phoneNumber { get; set; }
        public string shipToPhoneNumber { get; set; }
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
        public string globalid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public IndividualTradeProductInstance individualTradeProductInstance { get; set; }
        public DesignatedProductClassification designatedProductClassification { get; set; }
        public OriginTradeCountry originTradeCountry { get; set; }
        public InformationNote informationNote { get; set; }
    }
    public class InformationNote
    {
        public string subject { get; set; }
        public string content { get; set; }
    }
    public class IndividualTradeProductInstance
    {
        public string batchid { get; set; }
        public string expiryDateTime { get; set; }
    }
    public class DesignatedProductClassification
    {
        public string classCode { get; set; }
        public string claseName { get; set; }
    }
    public class OriginTradeCountry
    {
        public string id { get; set; }
    }
    public class SpecifiedLineTradeAgreement
    {
        public GrossPriceProductTradePrice grossPriceProductTradePrice { get; set; }
    }
    public class GrossPriceProductTradePrice
    {
        public string chargeAmount { get; set; }
        public AppliedTradeAllowanceCharge appliedTradeAllowanceCharge { get; set; }
    }
    public class AppliedTradeAllowanceCharge
    {
        public string chargeIndicator { get; set; }
        public string actualAmount { get; set; }
        public string reasonCode { get; set; }
        public string reason { get; set; }
        public string typeCode { get; set; }
    }
    public class SpecifiedLineTradeDelivery
    {
        public BilledQuantity billedQuantity { get; set; }
        public string perPackageUnitQuantity { get; set; }
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
        public string reasonCode { get; set; }
        public string reason { get; set; }
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

    public enum TypeData
    {
        String = 0,
        Interger = 1,
        Double = 2,
    }
}
