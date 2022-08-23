using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class CertificateMaster
    {
        [Key]
        public int CertificateNo { get; set; }
        public string? CertificateName { get; set; }
        public string? CertificateHsmname { get; set; }
        public string? CertificateHsmserial { get; set; }
        public string? CertificateCertSerial { get; set; }
        public string? CertificateKeyAlias { get; set; }
        public string? CertificateSlotPassword { get; set; }
        public string? CertificateStartDate { get; set; }
        public string? CertificateEndDate { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? Isactive { get; set; }
    }
}
