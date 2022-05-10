using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MODEL
{
    public class Response
    {
        public bool STATUS { get; set; }
        public string? CODE { get; set; }
        public string? MESSAGE { get; set; }
        public string? ERROR_MESSAGE { get; set; }
        public string? ERROR_STACK { get; set; }
        public string? INNER_EXCEPTION { get; set; }
        public string? CURRENT_METHOD { get; set; }
        public object? OUTPUT_DATA { get; set; }
        public object? OUTPUT_ERROR { get; set; }
    }
}
