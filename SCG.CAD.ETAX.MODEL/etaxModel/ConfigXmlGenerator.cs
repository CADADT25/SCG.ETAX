using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ConfigXmlGenerator
    {
        [Key]
        public int ConfigXmlGeneratorNo { get; set; }
        public string CustomerCode { get; set; } = null!;
        public string? ConfigXmlGeneratorInputSource { get; set; }
        public string? ConfigXmlGeneratorInputType { get; set; }
        public string? ConfigXmlGeneratorInputPath { get; set; }
        public string? ConfigXmlGeneratorOutputSource { get; set; }
        public string? ConfigXmlGeneratorOutputType { get; set; }
        public string? ConfigXmlGeneratorOutputPath { get; set; }
        public string? ConfigXmlGeneratorOnlineRecordNumber { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int? Isactive { get; set; }
    }
}
