using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ProfileSeller
    {
        [Key]
        public int SellerNo { get; set; }
        public string CompanyCode { get; set; } = null!;
        public string BranchCode { get; set; } = null!;
        public string? Province { get; set; }
        public string? District { get; set; }
        public string? SubDistrict { get; set; }
        public string? Road { get; set; }
        public string? Building { get; set; }
        public string? Addressnumber { get; set; }
        public string? SellerEmail { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
        public string? EmailTemplateNo { get; set; }
    }
}
