using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class Font
    {
        [Key]
        public int FontNo { get; set; }
        public string? FontName { get; set; }
    }
}
