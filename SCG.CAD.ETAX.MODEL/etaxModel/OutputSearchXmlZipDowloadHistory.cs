using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class OutputSearchXmlZipDowloadHistory
    {
        [Key]
        public int OutputSearchXmlZipDowloadHistoryNo { get; set; }
        public int? OutputSearchXmlZipNo { get; set; }
        public DateTime? OutputSearchXmlZipDowloadHistoryTime { get; set; }
        public string? OutputSearchXmlZipDowloadHistoryBy { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? Isactive { get; set; }
    }
}
