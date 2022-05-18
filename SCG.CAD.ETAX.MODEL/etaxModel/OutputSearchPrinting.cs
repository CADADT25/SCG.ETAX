using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class OutputSearchPrinting
    {
        [Key]
        public int OutputSearchPrintingNo { get; set; }
        public string? OutputSearchPrintingCompanyCode { get; set; }
        public string? OutputSearchPrintingFileName { get; set; }
        public string? OutputSearchPrintingFullPath { get; set; }
        public int? OutputSearchPrintingDowloadStatus { get; set; }
        public int? OutputSearchPrintingDowloadCount { get; set; }
        public DateTime? OutputSearchPrintingDowloadLastTime { get; set; }
        public string? OutputSearchPrintingDowloadLastBy { get; set; }
        public string? OutputSearchPrintingRequestCancelNo { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? Isactive { get; set; }
    }
}
