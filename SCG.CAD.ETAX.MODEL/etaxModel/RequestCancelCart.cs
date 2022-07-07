using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class RequestCancelCart
    {
        public int RequestCancelCartNo { get; set; }
        public string? RequestCancelCartBilling { get; set; }
        public string? RequestCancelCartBy { get; set; }
        public DateTime? RequestCancelCartDate { get; set; }
        public DateTime? RequestCancelCartExpire { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
    }
}
