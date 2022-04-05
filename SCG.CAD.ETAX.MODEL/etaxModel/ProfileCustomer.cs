using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ProfileCustomer
    {
        [Key]
        public int CustomerProfileNo { get; set; }
        public string? CustomerId { get; set; }
        public string? CompanyCode { get; set; }
        public int? OutputType { get; set; }
        public int? NumberOfCopies { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerCcemail { get; set; }
        public int? EmailType { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? Isactive { get; set; }
        public string? EmailTemplateNo { get; set; }
        public int? StatusPrint { get; set; }
        public int? StatusEmail { get; set; }
        public int? StatusSignPdf { get; set; }
        public int? StatusSignXml { get; set; }
    }
}
