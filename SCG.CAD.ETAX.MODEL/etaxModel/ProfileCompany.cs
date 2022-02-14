

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ProfileCompany
    {
        public int CompanyNo { get; set; }
        public int CompanyCode { get; set; }
        public string CompanyNameTh { get; set; } = null!;
        public string? CompanyNameEn { get; set; }
        public int CertificateProfileNo { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
