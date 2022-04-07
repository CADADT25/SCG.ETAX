using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ConfigPdfSign
    {
        [Key]
        public int ConfigPdfsignNo { get; set; }
        public string? ConfigPdfsignCompanyCode { get; set; }
        public string? ConfigPdfsignCompanyTax { get; set; }
        public string? ConfigPdfsignUlx { get; set; }
        public string? ConfigPdfsignUly { get; set; }
        public string? ConfigPdfsignPage { get; set; }
        public string? ConfigPdfsignOnlineRecordNumber { get; set; }
        public string? ConfigPdfsignInputSource { get; set; }
        public string? ConfigPdfsignInputType { get; set; }
        public string? ConfigPdfsignInputPath { get; set; }
        public string? ConfigPdfsignOutputSource { get; set; }
        public string? ConfigPdfsignOutputType { get; set; }
        public string? ConfigPdfsignOutputPath { get; set; }
        public string? ConfigPdfsignHsmSerial { get; set; }
        public string? ConfigPdfsignKeyAlias { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
