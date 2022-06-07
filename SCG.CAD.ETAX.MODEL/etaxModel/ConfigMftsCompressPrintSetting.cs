using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ConfigMftsCompressPrintSetting
    {
        [Key]
        public int ConfigMftsCompressPrintSettingNo { get; set; }
        public string? ConfigMftsCompressPrintSettingCompanyCode { get; set; }
        public string? ConfigMftsCompressPrintSettingDataSource { get; set; }
        public string? ConfigMftsCompressPrintSettingOneTime { get; set; }
        public string? ConfigMftsCompressPrintSettingAnyTime { get; set; }
        public DateTime? ConfigMftsCompressPrintSettingNextTime { get; set; }
        public string? ConfigMftsCompressPrintSettingInputPdf { get; set; }
        public string? ConfigMftsCompressPrintSettingOutputPdf { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
