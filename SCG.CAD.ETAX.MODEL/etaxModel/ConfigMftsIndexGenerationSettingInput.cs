﻿using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ConfigMftsIndexGenerationSettingInput
    {
        [Key]
        public int ConfigMftsIndexGenerationSettingInputNo { get; set; }
        public string? ConfigMftsIndexGenerationSettingInputCompanyCode { get; set; }
        public string? ConfigMftsIndexGenerationSettingInputSourceName { get; set; }
        public string? ConfigMftsIndexGenerationSettingInputSourceNameOut { get; set; }
        public string? ConfigMftsIndexGenerationSettingInputOcType { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
