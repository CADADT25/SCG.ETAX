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
        public string? ConfigPdfsignOperationMode { get; set; }
        public string? ConfigPdfsignInputType { get; set; }
        public string? ConfigPdfsignInputSource { get; set; }
        public string? ConfigPdfsignInputPath { get; set; }
        public string? ConfigPdfsignOutputType { get; set; }
        public string? ConfigPdfsignOutputSource { get; set; }
        public string? ConfigPdfsignOutputPath { get; set; }
        public string? ConfigPdfsignOneTime { get; set; }
        public string? ConfigPdfsignAnyTime { get; set; }
        public DateTime? ConfigPdfsignNextTime { get; set; }
        public string? ConfigPdfsignFtpHost { get; set; }
        public string? ConfigPdfsignFtpPort { get; set; }
        public string? ConfigPdfsignFtpUserName { get; set; }
        public string? ConfigPdfsignFtpPassword { get; set; }
        public string? ConfigPdfsignOnlineRecordNumber { get; set; }
        public string? ConfigPdfsignHsmModule { get; set; }
        public string? ConfigPdfsignHsmSlot { get; set; }
        public string? ConfigPdfsignHsmPassword { get; set; }
        public string? ConfigPdfsignHsmSerial { get; set; }
        public string? ConfigPdfsignKeyAlias { get; set; }
        public string? ConfigPdfsignVisibleDs { get; set; }
        public string? ConfigPdfsignFontName { get; set; }
        public string? ConfigPdfsignFontSize { get; set; }
        public string? ConfigPdfsignImage { get; set; }
        public string? ConfigPdfsignUlx { get; set; }
        public string? ConfigPdfsignUly { get; set; }
        public string? ConfigPdfsignPage { get; set; }
        public string? ConfigPdfsignDsLocation { get; set; }
        public string? ConfigPdfsignDsReason { get; set; }
        public string? ConfigPdfsignWithTimeStamp { get; set; }
        public string? ConfigPdfsignTimestampUrl { get; set; }
        public string? ConfigPdfsignTimestampUserName { get; set; }
        public string? ConfigPdfsignTimestampPassword { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
