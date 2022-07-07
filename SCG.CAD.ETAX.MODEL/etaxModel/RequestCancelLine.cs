using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class RequestCancelLine
    {
        public int RequestCancelLineNo { get; set; }
        public int? RequestCancelLineHeaderNo { get; set; }
        public string? RequestCancelLineBilling { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
    }
}
