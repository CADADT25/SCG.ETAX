using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ZipFileConfig
    {
        [Key]
        public int ZipFileConfigNo { get; set; }
        public string? ZipFileConfigName1 { get; set; }
        public string? ZipFileConfigValue1 { get; set; }
        public string? ZipFileConfigName2 { get; set; }
        public string? ZipFileConfigValue2 { get; set; }
        public string? ZipFileConfigName3 { get; set; }
        public string? ZipFileConfigValue3 { get; set; }
        public string? ZipFileConfigName4 { get; set; }
        public string? ZipFileConfigValue4 { get; set; }
        public string? ZipFileConfigName5 { get; set; }
        public string? ZipFileConfigValue5 { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
