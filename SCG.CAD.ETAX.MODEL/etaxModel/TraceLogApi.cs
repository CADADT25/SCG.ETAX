using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class TraceLogApi
    {
        [Key]
        public int Id { get; set; }
        public string? RequestContentType { get; set; }
        public string? RequestUri { get; set; }
        public string? RequestPath { get; set; }
        public string? RequestMethod { get; set; }
        public DateTime? RequestTimestamp { get; set; }
        public string? RequestBody { get; set; }

        public string? ResponseBody { get; set; }
        public string? ResponseContentType { get; set; }
        public string? ResponseStatusCode { get; set; }
        public DateTime? ResponseTimestamp { get; set; }
    }
}
