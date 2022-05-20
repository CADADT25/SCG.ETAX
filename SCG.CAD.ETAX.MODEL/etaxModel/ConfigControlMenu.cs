using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ConfigControlMenu
    {
        [Key]
        public int ConfigControlMenuNo { get; set; }
        public string? ConfigControlMenuName { get; set; }
        public string? ConfigControlMenuValue { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
    }
}
