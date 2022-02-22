using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ZipFileType
    {
        [Key]
        public int ZipFileTypeNo { get; set; }
        public string? ZipFileTypeCode { get; set; }
        public string? ZipFileTypeName { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
