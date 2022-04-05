using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ProductUnit
    {
        [Key]
        public int ProductUnitNo { get; set; }
        public string? ErpSource { get; set; }
        public string? ProductUnitErp { get; set; }
        public string? ProductUnitRd { get; set; }
        public string? ProductUnitDescription { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int? Isactive { get; set; }
    }
}
