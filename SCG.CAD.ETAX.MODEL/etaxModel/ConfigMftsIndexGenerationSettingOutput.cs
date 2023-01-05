using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ConfigMftsIndexGenerationSettingOutput
    {
        [Key]
        public int ConfigMftsIndexGenerationSettingOutputNo { get; set; }
        public string? ConfigMftsIndexGenerationSettingOutputCompanyCode { get; set; }
        public string? ConfigMftsIndexGenerationSettingOutputSourceName { get; set; }
        public string? ConfigMftsIndexGenerationSettingOutputType { get; set; }
        public string? ConfigMftsIndexGenerationSettingOutputFolder { get; set; }
        public string? ConfigMftsIndexGenerationSettingOutputLogReceiveType { get; set; }
        public string? ConfigMftsIndexGenerationSettingOutputLogReceiveFolder { get; set; }
        public string? ConfigMftsIndexGenerationSettingOutputLogArchivedType { get; set; }
        public string? ConfigMftsIndexGenerationSettingOutputLogArchivedFolder { get; set; }
        public string? ConfigMftsIndexGenerationSettingOutputHost { get; set; }
        public string? ConfigMftsIndexGenerationSettingOutputPort { get; set; }
        public string? ConfigMftsIndexGenerationSettingOutputUsername { get; set; }
        public string? ConfigMftsIndexGenerationSettingOutputPassword { get; set; }
        public string? ConfigMftsIndexGenerationSettingOutputOneTime { get; set; }
        public string? ConfigMftsIndexGenerationSettingOutputAnyTime { get; set; }
        public DateTime? ConfigMftsIndexGenerationSettingOutputNextTime { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
