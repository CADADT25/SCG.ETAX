using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ProfileCompanyCode
    {
        [Key]
        public int CompanyCodeNo { get; set; }
        public string CompanyCode { get; set; } = null!;
        public string? CompanyCodeDescription { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
