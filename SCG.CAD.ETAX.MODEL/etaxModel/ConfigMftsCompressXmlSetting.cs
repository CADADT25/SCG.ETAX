using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ConfigMftsCompressXmlSetting
    {
        [Key]
        public int ConfigMftsCompressXmlSettingNo { get; set; }
        public string? ConfigMftsCompressXmlSettingCompanyCode { get; set; }
        public string? ConfigMftsCompressXmlSettingSourceName { get; set; }
        public string? ConfigMftsCompressXmlSettingCompressType { get; set; }
        public string? ConfigMftsCompressXmlSettingOneTime { get; set; }
        public string? ConfigMftsCompressXmlSettingAnyTime { get; set; }
        public DateTime? ConfigMftsCompressXmlSettingNextTime { get; set; }
        public string? ConfigMftsCompressXmlSettingInputFolder { get; set; }
        public string? ConfigMftsCompressXmlSettingOutputFolder { get; set; }
        public string? ConfigMftsCompressXmlSettingHost { get; set; }
        public string? ConfigMftsCompressXmlSettingPort { get; set; }
        public string? ConfigMftsCompressXmlSettingUsername { get; set; }
        public string? ConfigMftsCompressXmlSettingPassword { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
