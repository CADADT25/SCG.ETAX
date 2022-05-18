using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class OutputSearchEmailSendHistory
    {
        [Key]
        public int OutputSearchEmailSendHistoryNo { get; set; }
        public int? OutputSearchEmailSendNo { get; set; }
        public DateTime? OutputSearchEmailSendHistoryTime { get; set; }
        public string? OutputSearchEmailSendHistoryBy { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? Isactive { get; set; }
    }
}
