using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ReqeustCancelHeader
    {
        public int ReqeustCancelHeaderNo { get; set; }
        public string? ReqeustCancelHeaderBy { get; set; }
        public DateTime? ReqeustCancelHeaderDate { get; set; }
        public int? ReqeustCancelHeaderStatusCode { get; set; }
        public string? RequestCancelHeaderReason { get; set; }
        public string? RequestCancelHeaderActionEventBy { get; set; }
        public DateTime? RequestCancelHeaderActionEventDate { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
    }
}
