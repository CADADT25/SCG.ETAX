using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class TransactionDescription
    {
        [Key]
        public int TransactionNo { get; set; }
        public string? BillingNumber { get; set; }
        public DateTime? BillingDate { get; set; }
        public double? BillingYear { get; set; }
        public DateTime? ProcessingDate { get; set; }
        public string? CompanyCode { get; set; }
        public string? CompanyName { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public double? SoldTo { get; set; }
        public double? ShipTo { get; set; }
        public double? BillTo { get; set; }
        public double? Payer { get; set; }
        public string? SourceName { get; set; }
        public double? Foc { get; set; }
        public double? Ic { get; set; }
        public string? PostingYear { get; set; }
        public double? FiDoc { get; set; }
        public string? ImageDocType { get; set; }
        public string? DocType { get; set; }
        public string? SellOrg { get; set; }
        public string? PoNumber { get; set; }
        public string? TypeInput { get; set; }
        public string? GenerateStatus { get; set; }
        public string? GenerateDetail { get; set; }
        public DateTime? GenerateDateTime { get; set; }
        public string? XmlSignStatus { get; set; }
        public string? XmlSignDetail { get; set; }
        public DateTime? XmlSignDateTime { get; set; }
        public string? PdfSignStatus { get; set; }
        public string? PdfSignDetail { get; set; }
        public DateTime? PdfSignDateTime { get; set; }
        public string? PrintStatus { get; set; }
        public string? PrintDetail { get; set; }
        public DateTime? PrintDateTime { get; set; }
        public string? EmailSendStatus { get; set; }
        public string? EmailSendDetail { get; set; }
        public DateTime? EmailSendDateTime { get; set; }
        public string? XmlCompressStatus { get; set; }
        public string? XmlCompressDetail { get; set; }
        public DateTime? XmlCompressDateTime { get; set; }
        public string? PdfIndexingStatus { get; set; }
        public string? PdfIndexingDetail { get; set; }
        public string? PdfIndexingDateTime { get; set; }
        public string? PdfSignLocation { get; set; }
        public string? XmlSignLocation { get; set; }
        public string? OutputXmlTransactionNo { get; set; }
        public string? OutputPdfTransactionNo { get; set; }
        public string? OutputMailTransactionNo { get; set; }
        public string? DmsAttachmentFileName { get; set; }
        public string? DmsAttachmentFilePath { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? Isactive { get; set; }
        public string? OneTimeEmail { get; set; }
    }
}
