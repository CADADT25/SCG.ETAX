using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ProfileIsActive
    {
        [Key]
        public int IsActiveNo { get; set; }
        public int IsActiveCode { get; set; }
        public string IsActiveNameTh { get; set; } = null!;
        public string? IsActiveNameEn { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
    }
}
