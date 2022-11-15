using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class RequestPath
    {
        [Key]
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public string PathXml { get; set; } = null!;
        public string PathPdf { get; set; } = null!;
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
    }
}
