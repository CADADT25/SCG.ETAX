using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ConfigControlFunction
    {
        [Key]
        public int ConfigControlFunctionNo { get; set; }
        public int? ConfigControlFunctionMenuNo { get; set; }
        public int? ConfigControlNo { get; set; }
        public string? ConfigControlFunctionRole { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int? Isactive { get; set; }
    }
}
