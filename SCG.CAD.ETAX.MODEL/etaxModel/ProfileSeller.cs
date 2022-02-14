

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ProfileSeller
    {
        public int SellerNo { get; set; }
        public string CompanyCode { get; set; } = null!;
        public string BranchCode { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string District { get; set; } = null!;
        public string SubDistrict { get; set; } = null!;
        public string? Road { get; set; }
        public string? Building { get; set; }
        public string? AddressNumber { get; set; }
        public string? SellerEmail { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
        public int? EmailTemplateNo { get; set; }
    }
}
