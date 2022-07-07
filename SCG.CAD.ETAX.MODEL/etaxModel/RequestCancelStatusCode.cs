using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class RequestCancelStatusCode
    {
        public int RequestCancelStatusCodeNo { get; set; }
        public int? RequestCancelStatusCode1 { get; set; }
        public string? RequestCancelStatusCodeName { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
    }
}
