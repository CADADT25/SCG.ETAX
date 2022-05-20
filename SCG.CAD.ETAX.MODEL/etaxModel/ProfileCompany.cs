using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ProfileCompany
    {
        [Key]
        public int CompanyNo { get; set; }
        public string CompanyCode { get; set; } = null!;
        public string? CompanyNameTh { get; set; }
        public string? CompanyNameEn { get; set; }
        public int? CertificateProfileNo { get; set; }
        public string? TaxNumber { get; set; }
        public int? IsEbill { get; set; }
        public string? EmailResponse { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
