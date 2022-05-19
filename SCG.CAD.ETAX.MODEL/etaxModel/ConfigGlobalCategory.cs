using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ConfigGlobalCategory
    {
        [Key]
        public int ConfigGlobalCategoryNo { get; set; }
        public string? ConfigGlobalCategoryName { get; set; }
        public string? ConfigGlobalCategoryDescription { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
