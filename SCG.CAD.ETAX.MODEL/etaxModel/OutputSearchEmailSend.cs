using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class OutputSearchEmailSend
    {
        [Key]
        public int OutputSearchEmailSendNo { get; set; }
        public string? OutputSearchEmailSendCompanyCode { get; set; }
        public string? OutputSearchEmailSendSubject { get; set; }
        public string? OutputSearchEmailSendFrom { get; set; }
        public string? OutputSearchEmailSendTo { get; set; }
        public string? OutputSearchEmailSendCc { get; set; }
        public string? OutputSearchEmailSendFileName { get; set; }
        public int? OutputSearchEmailSendStatus { get; set; }
        public DateTime? OutputSearchEmailSendLastTime { get; set; }
        public string? OutputSearchEmailSendLastBy { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? Isactive { get; set; }
    }
}
