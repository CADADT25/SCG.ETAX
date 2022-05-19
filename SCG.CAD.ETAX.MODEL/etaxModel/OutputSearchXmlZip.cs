using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class OutputSearchXmlZip
    {
        [Key]
        public int OutputSearchXmlZipNo { get; set; }
        public string? OutputSearchXmlZipCompanyCode { get; set; }
        public string? OutputSearchXmlZipFileName { get; set; }
        public string? OutputSearchXmlZipFullPath { get; set; }
        public string? OutputSearchXmlZipDocType { get; set; }
        public int? OutputSearchXmlZipDowloadStatus { get; set; }
        public int? OutputSearchXmlZipDowloadCount { get; set; }
        public DateTime? OutputSearchXmlZipDowloadLastTime { get; set; }
        public string? OutputSearchXmlZipDowloadLastBy { get; set; }
        public string? OutputSearchXmlZipRequestCancelNo { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? Isactive { get; set; }
    }
}
