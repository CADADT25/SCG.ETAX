using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ConfigMftsEmailSetting
    {
        [Key]
        public int ConfigMftsEmailSettingNo { get; set; }
        public string? ConfigMftsEmailSettingCompanyCode { get; set; }
        public string? ConfigMftsEmailSettingOperation { get; set; }
        public string? ConfigMftsEmailSettingEmail { get; set; }
        public string? ConfigMftsEmailSettingEmailTemplate { get; set; }
        public string? ConfigMftsEmailSettingProtocol { get; set; }
        public string? ConfigMftsEmailSettingHost { get; set; }
        public string? ConfigMftsEmailSettingPort { get; set; }
        public string? ConfigMftsEmailSettingUsername { get; set; }
        public string? ConfigMftsEmailSettingPassword { get; set; }
        public string? ConfigMftsEmailSettingOneTime { get; set; }
        public string? ConfigMftsEmailSettingAnyTime { get; set; }
        public DateTime? ConfigMftsEmailSettingNextTime { get; set; }
        public string? ConfigMftsEmailSettingApiKey { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? Isactive { get; set; }
    }
}
