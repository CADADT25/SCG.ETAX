using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ProfileFiDoc
    {
        [Key]
        public int ProfileFiDocNo { get; set; }
        public string? ProfileFiDocName { get; set; }
        public string? ProfileImageDocType { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
