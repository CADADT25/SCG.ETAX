using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ConfigGlobal
    {
        [Key]
        public int ConfigGlobalNo { get; set; }
        public string ConfigGlobalCategoryName { get; set; } = null!;
        public string? ConfigGlobalName { get; set; }
        public string? ConfigGlobalValue { get; set; }
        public string? ConfigGlobalDescription { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
