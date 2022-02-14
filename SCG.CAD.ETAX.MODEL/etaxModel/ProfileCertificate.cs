

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ProfileCertificate
    {
        public int CertificateNo { get; set; }
        public int CompanyCode { get; set; }
        public string CompanyCertificateSerial { get; set; } = null!;
        public string CompanyCertificateKeyAlias { get; set; } = null!;
        public DateTime CompanyCertificateStartDate { get; set; }
        public DateTime CompanyCertificateEndDate { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
