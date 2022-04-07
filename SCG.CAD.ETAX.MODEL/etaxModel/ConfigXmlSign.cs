using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ConfigXmlSign
    {
        [Key]
        public int ConfigXmlsignNo { get; set; }
        public string? ConfigXmlsignCompanycode { get; set; }
        public string? ConfigXmlsignCompanyTax { get; set; }
        public string? ConfigXmlsignOnlineRecordNumber { get; set; }
        public string? ConfigXmlsignInputSource { get; set; }
        public string? ConfigXmlsignInputPath { get; set; }
        public string? ConfigXmlsignOutputSource { get; set; }
        public string? ConfigXmlsignOutputPath { get; set; }
        public string? ConfigXmlsignOutputConvertPath { get; set; }
        public string? ConfigXmlsignHsmSerial { get; set; }
        public string? ConfigXmlsignCertificateSerial { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
