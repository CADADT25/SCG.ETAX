using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ErpDocument
    {
        [Key]
        public int ErpDocumentNo { get; set; }
        public string ErpDocumentCode { get; set; } = null!;
        public string? ErpDocumentNameTh { get; set; }
        public string? ErpDocumentNameEn { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
