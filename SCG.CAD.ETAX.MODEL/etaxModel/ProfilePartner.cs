using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ProfilePartner
    {
        [Key]
        public int PartnerProfileNo { get; set; }
        public string? CustomerId { get; set; }
        public string? CompanyCode { get; set; }
        public string? SellOrg { get; set; }
        public int? PartnerOutputType { get; set; }
        public string? NumberOfCopies { get; set; }
        public string? SoldToCode { get; set; }
        public string? SoldToEmail { get; set; }
        public string? SoldToCcemail { get; set; }
        public string? ShipToCode { get; set; }
        public string? ShipToEmail { get; set; }
        public string? ShipToCcemail { get; set; }
        public string? PartnerEmailType { get; set; }
        public int? EmailTemplateNo { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int Isactive { get; set; }
        public int? StatusPrint { get; set; }
        public int? StatusEmail { get; set; }
        public int? StatusSignPdf { get; set; }
        public int? StatusSignXml { get; set; }
    }
}
