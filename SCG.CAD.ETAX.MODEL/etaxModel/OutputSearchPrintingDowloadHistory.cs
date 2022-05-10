using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class OutputSearchPrintingDowloadHistory
    {
        [Key]
        public int OutputSearchPrintingDowloadHistoryNo { get; set; }
        public int? OutputSearchPrintingNo { get; set; }
        public DateTime? OutputSearchPrintingDowloadHistoryTime { get; set; }
        public string? OutputSearchPrintingDowloadHistoryBy { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? Isactive { get; set; }
    }
}
