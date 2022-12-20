using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public class RequestActionDataModel
    {
        public Guid RequestId { get; set; }
        public string Action { get; set; }
        public string User { get; set; }
        public string? Reason { get; set; }
        //public string? PdfOutputPath { get; set; }
        //public string? XmlOutputPath { get; set; }
        public int? ConfigXmlSignNo { get; set; }
        public int? ConfigPdfSignNo { get; set; }
    }
}
