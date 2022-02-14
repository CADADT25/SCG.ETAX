

namespace SCG.CAD.ETAX.MODEL
{
    public partial class TransactionDescription
    {
        [Key]
        public int TransactionNo { get; set; }
        public string BillingNumber { get; set; } = null!;
        public DateTime BillingDate { get; set; }
        public string BillingYear { get; set; } = null!;
        public DateTime ProcessingDate { get; set; }
        public string CompanyCode { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string? SoldTo { get; set; }
        public string? ShipTo { get; set; }
        public string? BillTo { get; set; }
        public string? Payer { get; set; }
        public string SourceName { get; set; } = null!;
        public int Foc { get; set; }
        public int Ic { get; set; }
        public string PostingYear { get; set; } = null!;
        public string FiDoc { get; set; } = null!;
        public string ImageDocType { get; set; } = null!;
        public string? DocType { get; set; }
        public string? SellOrg { get; set; }
        public string? PoNumber { get; set; }
        public string? TypeInput { get; set; }
        public int? GenerateStatus { get; set; }
        public string? GenerateDetail { get; set; }
        public DateTime? GenerateDateTime { get; set; }
        public int? XmlSignStatus { get; set; }
        public string? XmlSignDetail { get; set; }
        public DateTime? XmlSignDateTime { get; set; }
        public int? PdfSignStatus { get; set; }
        public string? PdfSignDetail { get; set; }
        public DateTime? PdfSignDateTime { get; set; }
        public int? PrintStatus { get; set; }
        public string? PrintDetail { get; set; }
        public DateTime? PrintDateTime { get; set; }
        public int? EmailSendStatus { get; set; }
        public string? EmailSendDetail { get; set; }
        public DateTime? EmailSendDateTime { get; set; }
        public int? XmlCompressStatus { get; set; }
        public string? XmlCompressDetail { get; set; }
        public DateTime? XmlCompressDateTime { get; set; }
        public int? PdfIndexingStatus { get; set; }
        public string? PdfIndexingDetail { get; set; }
        public DateTime? PdfIndexingDateTime { get; set; }
        public string? PdfSignLocation { get; set; }
        public string? XmlSignLocation { get; set; }
        public int? ZipTransactionNo { get; set; }
        public string? DmsAttachmentFileName { get; set; }
        public string? DmsAttachmentFilePath { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
