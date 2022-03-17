using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ProfileBranch
    {
        [Key]
        public int ProfileBranchNo { get; set; }
        public string ProfileBranchCode { get; set; } = null!;
        public string? ProfileCompanyCode { get; set; }
        public string? ProfileBranchNameTh { get; set; }
        public string? ProfileBranchNameEn { get; set; }
        public string? ProfileBranchDescrition { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
