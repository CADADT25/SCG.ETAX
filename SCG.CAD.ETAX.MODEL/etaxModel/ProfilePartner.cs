

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ProfilePartner
    {
        public int PartnerProfileNo { get; set; }
        public string CustomerId { get; set; } = null!;
        public string CompanyCode { get; set; } = null!;
        public string SellOrg { get; set; } = null!;
        public string PartnerOutputType { get; set; } = null!;
        public string NumberOfCopies { get; set; } = null!;
        public string SoldToCode { get; set; } = null!;
        public string? SoldToEmail { get; set; }
        public string? SoldToCcemail { get; set; }
        public string ShipToCode { get; set; } = null!;
        public string? ShipToEmail { get; set; }
        public string? ShipToCcemail { get; set; }
        public string? PartnerEmailType { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
